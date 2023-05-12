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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly char[] _operators = new char[4]
        {
            '+',
            '-',
            '*',
            '/'
        };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            // Validation case
            //var text = button.Content.ToString();

            //if (tb.Text.Contains("="))
            //{
            //    tb.Text = _operators.Contains(text.Single()) ? "" : text;
            //    return;
            //}

            //if (_operators.Except(new[] { '-' }).Contains(text.Single()) && tb.Text.Length == 0)
            //{
            //    return;
            //}

            //if (tb.Text.Length == 0)
            //{
            //    tb.Text += button.Content.ToString();
            //    return;
            //}

            //if (_operators.Contains(tb.Text.Last()) && _operators.Contains(text.Single()))
            //{
            //    return;
            //}
            tb.Text += button.Content.ToString();
        }

        private void Result_click(object sender, RoutedEventArgs e)
        {
            // Validation case
            //if (tb.Text.Contains("="))
            //{
            //    tb.Text = "";
            //    return;
            //}
            try
            {
                result();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                tb.Text = "";
            }
        }

        private void result()
        {
            int num = 0;
            if (tb.Text.Contains("+"))
            {
                num = tb.Text.IndexOf("+");
            }
            else if (tb.Text.Contains("-"))
            {
                num = tb.Text.IndexOf("-");
            }
            else if (tb.Text.Contains("*"))
            {
                num = tb.Text.IndexOf("*");
            }
            else if (tb.Text.Contains("/"))
            {
                num = tb.Text.IndexOf("/");
            }
            string text = tb.Text.Substring(num, 1);
            double num2 = Convert.ToDouble(tb.Text.Substring(0, num));
            double num3 = Convert.ToDouble(tb.Text.Substring(num + 1, tb.Text.Length - num - 1));
            switch (text)
            {
                case "+":
                    {
                        TextBox textBox = tb;
                        textBox.Text = textBox.Text + "=" + (num2 + num3);
                        break;
                    }
                case "-":
                    {
                        TextBox textBox = tb;
                        textBox.Text = textBox.Text + "=" + (num2 - num3);
                        break;
                    }
                case "*":
                    {
                        TextBox textBox = tb;
                        textBox.Text = textBox.Text + "=" + num2 * num3;
                        break;
                    }
                default:
                    {
                        TextBox textBox = tb;
                        textBox.Text = textBox.Text + "=" + num2 / num3;
                        break;
                    }
            }
        }

        private void Off_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            tb.Text = "";
        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            if (tb.Text.Length > 0)
            {
                tb.Text = tb.Text.Substring(0, tb.Text.Length - 1);
            }
        }
    }
}
