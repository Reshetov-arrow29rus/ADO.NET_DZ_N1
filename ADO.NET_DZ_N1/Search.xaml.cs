using ADO.NET_DZ_N1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ADO.NET_DZ_N1
{
    /// <summary>
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        private SqlConnection conn;

        Dictionary<Button, string> buttonsQuery = new Dictionary<Button, string>();
        Dictionary<RadioButton, string> radioBbuttonsQuery = new Dictionary<RadioButton, string>();
        public Search(SqlConnection connection)
        {
            InitializeComponent();
            this.conn = connection;
            InitialButton();
        }

        private void Show_Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;

                ListQuery.Items.Clear();

                if (sender is RadioButton)
                {
                    command.CommandText = radioBbuttonsQuery[sender as RadioButton];

                    if ((sender as RadioButton) == Show_Count_Veg_Fr_Color)
                    {
                        /*
                        //Вариант1
                        WindowInput windowInputColor = new WindowInput();
                        windowInputColor.TextReturned += ColorTextReturned;
                        windowInputColor.ShowDialog();
                        command.Parameters.AddWithValue("@Цвет", str);
                        */

                        WindowInput windowInputColor = new WindowInput("Show_Count_Veg_Fr_Color");
                        // TextReturned - событие этого окна, мы создаем анонимную функцию (лямбда-выражение)
                        // как обработчик этого события. Внутри этой функции присваиваем значение returnedText
                        // переменной str и добавляем значение str в параметр команды SQL.
                        windowInputColor.TextReturned += (returnedText) =>
                        {
                            if (returnedText !=null) 
                                command.Parameters.AddWithValue("@Цвет", returnedText);
                            else 
                                MessageBox.Show("Значение цвета задано не верно, попробуйте еще раз!");
                        };

                        windowInputColor.ShowDialog();
                    }

                    else if ((sender as RadioButton) == Show_Veg_Fr_Less_Calorie)
                    {
                        WindowInput windowInputColor = new WindowInput("Show_Veg_Fr_Less_Calorie");
                        windowInputColor.TextReturned += (returnedText) =>
                        {   
                            if (int.TryParse(returnedText, out int value1))
                                command.Parameters.AddWithValue("@УказаннаяКалорийность", value1);
                            else
                                MessageBox.Show("Значение каллорий задано не верно, попробуйте еще раз!");
                        };
                        windowInputColor.ShowDialog();
                    }

                    else if ((sender as RadioButton) == Show_Veg_Fr_Range_Calorie)
                    {
                        WindowInput windowInput = new WindowInput("Show_Veg_Fr_Range_Calorie");
                        windowInput.TwoTextReturned += (returnedText1, returnedText2) =>
                        {
                            string lowerLimit, upperLimit;
                            //проверка строки содержат числовое значение
                            if (int.TryParse(returnedText1, out int value1) && int.TryParse(returnedText2, out int value2))
                            {
                                // устанавливаем нижний и верхний предел
                                if (value1 < value2)
                                {
                                    lowerLimit = returnedText1;
                                    upperLimit = returnedText2;
                                }
                                else
                                {
                                    lowerLimit = returnedText2;
                                    upperLimit = returnedText1;
                                }
                                command.Parameters.AddWithValue("@MinКалорийность", lowerLimit);
                                command.Parameters.AddWithValue("@MaxКалорийность", upperLimit);
                            }
                            else
                                MessageBox.Show("Диапазон задан не верно, попробуйте еще раз!");
                        };
                        windowInput.ShowDialog();
                    }
                }

                else if (sender is Button)
                    command.CommandText = buttonsQuery[sender as Button];


                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    string temp = string.Empty;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        temp += reader[i].ToString() + " ";
                    }
                    ListQuery.Items.Add(temp);
                }
                reader.Close();
            }
        }

        //Инициализация словарей buttonsQuery и radioBbuttonsQuery
        private void InitialButton()
        {
            buttonsQuery.Add(Show_all_information_Button, ConfigurationManager.AppSettings["SelectAllVegAndFr"]);
            buttonsQuery.Add(Show_all_title_Button, ConfigurationManager.AppSettings["SelectAllTitles"]);
            buttonsQuery.Add(Show_all_colors_Button, ConfigurationManager.AppSettings["SelectAllColors"]);
            buttonsQuery.Add(Show_max_calorie_Button, ConfigurationManager.AppSettings["MaxCalories"]);
            buttonsQuery.Add(Show_min_calorie_Button, ConfigurationManager.AppSettings["MinCalories"]);
            buttonsQuery.Add(Show_Average_Calorie_Button, ConfigurationManager.AppSettings["AvgCalories"]);

            radioBbuttonsQuery.Add(Show_Count_Vegetables, ConfigurationManager.AppSettings["CountVegetables"]);
            radioBbuttonsQuery.Add(Show_Count_Fruits, ConfigurationManager.AppSettings["CountFruits"]);
            radioBbuttonsQuery.Add(Show_Count_Veg_Fr_Color, ConfigurationManager.AppSettings["CountVegFrByColor"]);
            radioBbuttonsQuery.Add(Show_Count_Veg_Fr_Color_Everyone, ConfigurationManager.AppSettings["CountVegAndFrColor"]);
            radioBbuttonsQuery.Add(Show_Veg_Fr_Less_Calorie, ConfigurationManager.AppSettings["SelectVegAndFrByCalories"]);
            radioBbuttonsQuery.Add(Show_Veg_Fr_Range_Calorie, ConfigurationManager.AppSettings["SelectVegAndFrByCaloriesRange"]);
            radioBbuttonsQuery.Add(Show_All_Veg_Fr_Color_yellow_red, ConfigurationManager.AppSettings["SelectVegAndFrByColor"]);
        }

        //закрытие окна
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        
        /*
        private void ColorTextReturned(string returnedText)
        {
            str = returnedText;
        }*/
    }
}
