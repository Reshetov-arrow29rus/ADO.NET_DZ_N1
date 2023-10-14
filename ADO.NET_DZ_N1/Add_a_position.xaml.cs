using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ADO.NET_DZ_N1
{
    /// <summary>
    /// Логика взаимодействия для Add_a_position.xaml
    /// </summary>
    public partial class Add_a_position : Window
    {
        private SqlConnection conn;


        public Add_a_position(SqlConnection connection)
        {
            InitializeComponent();
            this.conn = connection;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            try 
            { 
                AddObject addObject = new AddObject(conn);
                addObject.TitleObject = titleTextBox.Text;
                addObject.TypeObject = typeTextBox.Text;
                addObject.ColorObject = colourTextBox.Text;
                int result;
                if (int.TryParse(сaloriesTextBox.Text, out result))
                    addObject.CaloriesObject = result;

                addObject.InsertData();

                MessageBox.Show("Объект успешно добавлен в таблицу!");

                // Очистка полей ввода в Add_a_position
                titleTextBox.Text = string.Empty;
                typeTextBox.Text = string.Empty;
                colourTextBox.Text = string.Empty;
                сaloriesTextBox.Text = string.Empty;
            }
            catch (Exception ex)
            {
                // Обработка ошибки и отображение сообщения
                MessageBox.Show("Произошла ошибка, объект не может быть добавлен: " + ex.Message);
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            // Проверяем пустые ли значения в TextBox
            if (string.IsNullOrEmpty(titleTextBox.Text) || string.IsNullOrEmpty(typeTextBox.Text) ||
                string.IsNullOrEmpty(colourTextBox.Text) || string.IsNullOrEmpty(сaloriesTextBox.Text))
            {
                // Если хотя бы одно значение пустое, делаем кнопку добавления некликабельной
                AddButton.IsEnabled = false;
            }
            else
            {
                // Проверяем корректность данных в TextBox, где это необходимо
                int value;
                if (int.TryParse(сaloriesTextBox.Text, out value))
                {
                    // Если все значения заполнены и корректны, делаем кнопку добавления кликабельной
                    AddButton.IsEnabled = true;
                }
                else
                {
                    // Если значение некорректное, делаем кнопку добавления некликабельной
                    AddButton.IsEnabled = false;
                }
            }

        }

    }
}
