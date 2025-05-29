using System;
using System.Collections.Generic;
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

namespace kalCOOLaTHOR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double lastNumber;
        private string selectedOperator;
        private bool isNewNumber;

        public MainWindow()
        {
            InitializeComponent();
            ResetCalculator();
        }

        private void ResetCalculator()
        {
            lastNumber = 0;
            selectedOperator = "";
            isNewNumber = true;
            Display.Text = "0";
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string number = button.Content.ToString();

            if (Display.Text == "0" || isNewNumber)
            {
                Display.Text = number;
                isNewNumber = false;
            }
            else
            {
                Display.Text += number;
            }
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            selectedOperator = button.Content.ToString();
            lastNumber = double.Parse(Display.Text);
            isNewNumber = true;
        }

        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(selectedOperator))
                return;

            double newNumber = double.Parse(Display.Text);
            double result = 0;

            switch (selectedOperator)
            {
                case "+":
                    result = lastNumber + newNumber;
                    break;
                case "-":
                    result = lastNumber - newNumber;
                    break;
                case "*":
                    result = lastNumber * newNumber;
                    break;
                case "/":
                    if (newNumber == 0)
                    {
                        MessageBox.Show("Деление на ноль невозможно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        ResetCalculator();
                        return;
                    }
                    result = lastNumber / newNumber;
                    break;
                case "^":
                    result = Math.Pow(lastNumber, newNumber);
                    break;
            }

            Display.Text = result.ToString();
            selectedOperator = "";
            isNewNumber = true;
            lastNumber = result;
        }

        private void BtnDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (isNewNumber)
            {
                Display.Text = "0,";
                isNewNumber = false;
                return;
            }

            if (!Display.Text.Contains(","))
            {
                Display.Text += ",";
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ResetCalculator();
        }

        private void BtnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text.Length == 1 || (Display.Text.Length == 2 && Display.Text.StartsWith("-")))
            {
                Display.Text = "0";
            }
            else
            {
                Display.Text = Display.Text.Substring(0, Display.Text.Length - 1);
            }
        }

        private void BtnSqrt_Click(object sender, RoutedEventArgs e)
        {
            double number = double.Parse(Display.Text);
            if (number < 0)
            {
                MessageBox.Show("Нельзя извлечь корень из отрицательного числа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            double result = Math.Sqrt(number);
            Display.Text = result.ToString();
            isNewNumber = true;
        }

        private void BtnPlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (Display.Text == "0")
                return;

            if (Display.Text.StartsWith("-"))
            {
                Display.Text = Display.Text.Substring(1);
            }
            else
            {
                Display.Text = "-" + Display.Text;
            }
        }
    }
}
