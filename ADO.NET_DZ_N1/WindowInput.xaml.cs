using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace ADO.NET_DZ_N1
{
    /// <summary>
    /// Логика взаимодействия для WindowInput.xaml
    /// </summary>
    public partial class WindowInput : Window
    {
        //событие, которое объявлено с использованием делегата Action<string>
        public event Action<string> TextReturned;
        // второе событие, которое объявлено с использованием делегата Action<string, string>
        public event Action<string, string> TwoTextReturned;
        //хранить строку с названием кнопки которое вызывает окно
        public string ButtonName { get; set; }
        // объявляем TextBox в качестве члена класса
        private TextBox inputTextBox;

        public WindowInput(string buttonName)
        {
            InitializeComponent();
            ButtonName = buttonName;

            if (ButtonName == "Show_Count_Veg_Fr_Color")
            {
                LabelWindow.Content = "Укажите, какой цвет хотите задать.\r И нажмите кнопку \"Ok\"";
            }
            else if (ButtonName == "Show_Veg_Fr_Less_Calorie")
            {
                LabelWindow.Content = "Укажите ниже какой коллорийности отобразить.\r И нажмите кнопку \"Ok\"";
            }
            else if (ButtonName == "Show_Veg_Fr_Range_Calorie")
            {
                LabelWindow.Content = "Укажите какой диапазон калорийности отобразить.\r И нажмите кнопку \"Ok\"";

                inputTextBox = new TextBox();
                inputTextBox.Name = "InputTextBox";
                Canvas.SetLeft(inputTextBox, 10);
                Canvas.SetTop(inputTextBox, 102);
                inputTextBox.Width = 380;
                inputTextBox.Height = 30;
                inputTextBox.FontSize = 18;
                inputTextBox.TextChanged += InputTextBox_TextChanged;

                myCanvas.Children.Add(inputTextBox);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            if (ButtonName == "Show_Count_Veg_Fr_Color")
            {
            //Invoke - это метод делегата, который вызывает все методы,
            //зарегистрированные для выполнения события.
            //Если делегат не имеет ни одного метода-подписчика,
            //вызов метода Invoke не произойдет.
                TextReturned?.Invoke(InputColorTextBox.Text);
            }
            else if (ButtonName == "Show_Veg_Fr_Less_Calorie")
                TextReturned?.Invoke(InputColorTextBox.Text);

            else if (ButtonName == "Show_Veg_Fr_Range_Calorie") 
                TwoTextReturned?.Invoke(InputColorTextBox.Text, inputTextBox.Text);

            Close();
        }
        //проверка что поля TextBox'ов заполнены, активируем кнопку "Ок"
        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputColorTextBox.Text != string.Empty && (inputTextBox == null || inputTextBox.Text != string.Empty))
                OkButton.IsEnabled = true;
        }
    }
}
