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
using System.Windows.Shapes;

namespace ReSCat.View
{
    /// <summary>
    /// Logika interakcji dla klasy Calculator.xaml
    /// </summary>
    public partial class CalculatorView : Window
    {
        private string currentValue;
        private double calculateValue(string twoCharacters)
        {
            try
            {
                if (twoCharacters.Contains('+'))
                {                    
                    var elements = twoCharacters.Split('+');
                    currentValue = (Convert.ToDouble(elements[0]) + Convert.ToDouble(elements[1])).ToString();
                    return Convert.ToDouble(elements[0]) + Convert.ToDouble(elements[1]);
                }
                if (twoCharacters.Contains('-'))
                {
                    var elements = twoCharacters.Split('-');
                    currentValue = (Convert.ToDouble(elements[0]) - Convert.ToDouble(elements[1])).ToString();
                    return Convert.ToDouble(elements[0]) - Convert.ToDouble(elements[1]);
                }
                if (twoCharacters.Contains('*'))
                {
                    var elements = twoCharacters.Split('*');
                    currentValue = (Convert.ToDouble(elements[0]) * Convert.ToDouble(elements[1])).ToString();
                    return Convert.ToDouble(elements[0]) * Convert.ToDouble(elements[1]);
                }
                if (twoCharacters.Contains('/'))
                {
                    var elements = twoCharacters.Split('/');
                    currentValue = (Convert.ToDouble(elements[0]) / Convert.ToDouble(elements[1])).ToString();
                    return Convert.ToDouble(elements[0]) / Convert.ToDouble(elements[1]);
                }
            }
            catch (Exception)
            {
                currentValue = string.Empty;
                Display.Text = string.Empty;
            }
            
            return 0;
        }

        private bool containsCharacter(string currentValue)
        {
            return currentValue.Contains('+') ||
                currentValue.Contains('-') ||
                currentValue.Contains('*') ||
                currentValue.Contains('/');
        }

        public CalculatorView()
        {
            InitializeComponent();

            Display.Text = string.Empty;
            currentValue = string.Empty;
        }

        private void SevenButton_Click(object sender, RoutedEventArgs e)
        {
            currentValue += "7";
            Display.Text = currentValue;
        }

        private void EightButton_Click(object sender, RoutedEventArgs e)
        {
            currentValue += "8";
            Display.Text = currentValue;
        }

        private void NineButton_Click(object sender, RoutedEventArgs e)
        {
            currentValue += "9";
            Display.Text = currentValue;
        }



        private void FourButton_Click(object sender, RoutedEventArgs e)
        {
            currentValue += "4";
            Display.Text = currentValue;
        }

        private void FiveButton_Click(object sender, RoutedEventArgs e)
        {
            currentValue += "5";
            Display.Text = currentValue;
        }

        private void SixButton_Click(object sender, RoutedEventArgs e)
        {
            currentValue += "6";
            Display.Text = currentValue;
        }


        private void OneButton_Click(object sender, RoutedEventArgs e)
        {
            currentValue += "1";
            Display.Text = currentValue;
        }

        private void TwoButton_Click(object sender, RoutedEventArgs e)
        {
            currentValue += "2";
            Display.Text = currentValue;
        }

        private void ThreeButton_Click(object sender, RoutedEventArgs e)
        {
            currentValue += "3";
            Display.Text = currentValue;
        }
        private void ZeroButton_Click(object sender, RoutedEventArgs e)
        {
            currentValue += "0";
            Display.Text = currentValue;
        }
        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            Display.Text += "*";
            if (containsCharacter(currentValue))
            {
                Display.Text = calculateValue(currentValue).ToString() + "*";
            }
            currentValue += "*";
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            Display.Text += "-";
            if (containsCharacter(currentValue))
            {
                Display.Text = calculateValue(currentValue).ToString() + "-";
            }
            currentValue += "-";
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            Display.Text += "+";
            if (containsCharacter(currentValue))
            {
                Display.Text = calculateValue(currentValue).ToString() + "+";
            }
            currentValue += "+";
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            Display.Text += "/";
            if (containsCharacter(currentValue))
            {
                Display.Text = calculateValue(currentValue).ToString() + "/";
            }
            currentValue += "/";
        }



        private void CommaButton_Click(object sender, RoutedEventArgs e)
        {   
            currentValue += ",";
            Display.Text += ",";
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            
            Display.Text += "/";
            Display.Text = calculateValue(currentValue).ToString();

            currentValue = string.Empty;
        }

        private void DragnMove(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            currentValue = string.Empty;
            Display.Text = currentValue;
        }

    }
}
