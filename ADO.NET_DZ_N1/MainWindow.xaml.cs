using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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
        private SqlConnection con;

        public MainWindow()
        {
            InitializeComponent();
            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString; ;
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
    }
}
