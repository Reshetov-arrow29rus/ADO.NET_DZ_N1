using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ADO.NET_DZ_N1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con;
        SqlCommand com;

        public MainWindow()
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            com = new SqlCommand();
            com.Connection = con;
        }

        private void Open_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Open();
                Show.IsEnabled = true;
                Add_a_position.IsEnabled = true;
                Search.IsEnabled = true;
                Close.IsEnabled = true;
                Open.IsEnabled = false;
                MessageBox.Show("Подключено!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                con.Close();
                Show.IsEnabled = false;
                Add_a_position.IsEnabled = false;
                Search.IsEnabled = false;
                Close.IsEnabled = false;
                Open.IsEnabled = true;
                MessageBox.Show("Отключено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Show_Button_Click(object sender, RoutedEventArgs e)
        {
            string inserString = "SELECT * FROM Table_Vegetables_and_Fruits";
            com.CommandText = inserString;

            try
            {
                using (SqlDataReader reader = com.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    dataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                // Обработка исключения
                MessageBox.Show("Произошла ошибка: " + ex.Message);
            }


        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            // Создаем второе окно и вызываем его.
            // передаем права доступа в класс Add_a_position
            Add_a_position add_A_Position = new Add_a_position(con);
            add_A_Position.ShowDialog();
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            // Создаем второе окно и вызываем его.
            // передаем права доступа в класс Search
            Search search = new Search(con);
            search.ShowDialog();
        }
    }
}
