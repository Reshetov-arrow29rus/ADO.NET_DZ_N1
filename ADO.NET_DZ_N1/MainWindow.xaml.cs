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
        SqlDataReader reader;

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

            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;

        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            Add_a_position add_A_Position = new Add_a_position();
            add_A_Position.ShowDialog();
        }
    }
}
