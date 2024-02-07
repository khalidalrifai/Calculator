using System;
using System.Windows;
using System.Windows.Controls;

namespace Simple_Calculator
{
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();
            txtDisplay.Text = "0";
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && !string.IsNullOrEmpty(button.Content?.ToString()))
            {
                // TryParse is used here for safer parsing to handle cases where the content might not be a valid integer
                if (int.TryParse(button.Content.ToString(), out int selectedValue))
                {
                    if (txtDisplay.Text == "0")
                    {
                        txtDisplay.Text = selectedValue.ToString();
                    }
                    else
                    {
                        txtDisplay.Text += selectedValue.ToString();
                    }
                }
            }
        }


        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                lastNumber = double.Parse(txtDisplay.Text);
                txtDisplay.Text = "0";

                switch (button.Content.ToString())
                {
                    case "+":
                        selectedOperator = SelectedOperator.Addition;
                        break;
                    case "-":
                        selectedOperator = SelectedOperator.Subtraction;
                        break;
                    case "×":
                        selectedOperator = SelectedOperator.Multiplication;
                        break;
                    case "÷":
                        selectedOperator = SelectedOperator.Division;
                        break;
                }
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                txtDisplay.Text = (value / 100).ToString();
            }
        }

        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = "0";
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = "0";
            // Reset any stored values or operation states here
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (txtDisplay.Text.Length > 1)
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            }
            else
            {
                txtDisplay.Text = "0";
            }
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (!txtDisplay.Text.Contains("."))
            {
                txtDisplay.Text += ".";
            }
        }


        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber = double.Parse(txtDisplay.Text);

            switch (selectedOperator)
            {
                case SelectedOperator.Addition:
                    result = SimpleMath.Add(lastNumber, newNumber);
                    break;
                case SelectedOperator.Subtraction:
                    result = SimpleMath.Subtract(lastNumber, newNumber);
                    break;
                case SelectedOperator.Multiplication:
                    result = SimpleMath.Multiply(lastNumber, newNumber);
                    break;
                case SelectedOperator.Division:
                    // Check to prevent division by zero
                    result = newNumber != 0 ? SimpleMath.Divide(lastNumber, newNumber) : double.NaN;
                    break;
            }

            txtDisplay.Text = result.ToString();
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Add(double n1, double n2) => n1 + n2;
        public static double Subtract(double n1, double n2) => n1 - n2;
        public static double Multiply(double n1, double n2) => n1 * n2;
        public static double Divide(double n1, double n2) => n1 / n2;

    }
}
