using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Simple_Calculator
{
    // Main Window class responsible for calculator functionality
    public partial class MainWindow : Window
    {
        // Fields to store calculator state
        double lastNumber, result, memoryValue = 0;
        SelectedOperator selectedOperator;
        private bool isResultDisplayed = false;

        // Constructor
        public MainWindow()
        {
            InitializeComponent();
            InitializeCalculator();
        }

        private void InitializeCalculator()
        {
            txtDisplay.Text = "0";
            // Setting focus to the window to ensure it can receive keyboard input
            Loaded += (s, e) => Focus();
        }


        // EVENT HANDLERS FOR BUTTON CLICKS:

        // 1. Number Button Clicks
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && !string.IsNullOrEmpty(button.Content?.ToString()))
            {
                if (int.TryParse(button.Content.ToString(), out int selectedValue))
                {
                    // Reset flag only if the current display is the result of a calculation
                    if (isResultDisplayed)
                    {
                        txtDisplay.Text = selectedValue.ToString();
                        isResultDisplayed = false; // Reset the flag here as we're starting a new number entry
                    }
                    else
                    {
                        txtDisplay.Text = txtDisplay.Text == "0" ? selectedValue.ToString() : txtDisplay.Text += selectedValue.ToString();
                    }
                }
            }
        }

        // 2. Operation Button Clicks
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // Ensure content is not null by providing a fallback value
                string content = button.Content?.ToString()! ?? string.Empty;
                if (double.TryParse(txtDisplay.Text, out lastNumber))
                {
                    // Determine the selected operator based on button content
                    SelectedOperator op = GetOperatorFromContent(content);

                    // Perform the operation using the refactored method
                    PerformOperation(op);
                }
            }
        }

        // 3.
        private void MemoryButton_Click(object sender, RoutedEventArgs e) 
        {
            string message = "Memory operation error or not implemented.";
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // 4. Keyboard input support
        private void KeyboardInputHandler(object sender, KeyEventArgs e) 
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                case Key.D1:
                case Key.NumPad1:
                case Key.D2:
                case Key.NumPad2:
                case Key.D3:
                case Key.NumPad3:
                case Key.D4:
                case Key.NumPad4:
                case Key.D5:
                case Key.NumPad5:
                case Key.D6:
                case Key.NumPad6:
                case Key.D7:
                case Key.NumPad7:
                case Key.D8:
                case Key.NumPad8:
                case Key.D9:
                case Key.NumPad9:
                    AppendNumber(e.Key.ToString().Substring(e.Key.ToString().Length - 1));
                    break;
                case Key.OemPlus:
                case Key.Add:
                    PerformOperation(SelectedOperator.Addition);
                    break;
                case Key.OemMinus:
                case Key.Subtract:
                    PerformOperation(SelectedOperator.Subtraction);
                    break;
                case Key.Multiply:
                    PerformOperation(SelectedOperator.Multiplication);
                    break;
                case Key.Divide:
                case Key.OemQuestion:
                    PerformOperation(SelectedOperator.Division);
                    break;
                case Key.Enter:
                    CalculateResult();
                    break;
                case Key.Back:
                    Backspace();
                    break;
                case Key.Decimal:
                case Key.OemPeriod:
                    AddDecimalPoint();
                    break;
            }
        }

        
        // UTILITY METHODS:
        
        // 1. Method to append a number to the current display
        private void AppendNumber(string number) 
        {
            if (txtDisplay.Text == "0" || isResultDisplayed)
            {
                txtDisplay.Text = number;
                isResultDisplayed = false; // This resets the flag if it was set
            }
            else
            {
                txtDisplay.Text += number;
            }
        }

        // 2. Method to perform the selected operation
        private void PerformOperation(SelectedOperator operation) 
        {
            if (double.TryParse(txtDisplay.Text, out lastNumber))
            {
                // Add current number and operator to history
                txtHistory.Text = lastNumber + " " + GetOperatorSymbol(operation);
                txtDisplay.Text = "0";
                selectedOperator = operation;
                isResultDisplayed = false; // This allows a new number entry after operation
            }
        }

        // 3. Method to calculate the result
        private void CalculateResult() 
        {
            if (!isResultDisplayed && double.TryParse(txtDisplay.Text, out double newNumber))
            {
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
                        if (newNumber == 0)
                        {
                            txtDisplay.Text = "Cannot divide by zero";
                            isResultDisplayed = true; // Prevent further input until cleared
                            return; // Early return to avoid setting `txtDisplay.Text` to `result` at the end
                        }
                        else
                        {
                            result = SimpleMath.Divide(lastNumber, newNumber);
                        }
                        break;
                }

                // Update history and display result only if an operation was performed
                if (selectedOperator != SelectedOperator.None)
                {
                    txtHistory.Text += " " + newNumber + " =";
                    txtDisplay.Text = result.ToString();
                    isResultDisplayed = true; // The result is now displayed
                }
            }
        }

        // 4. Method to handle backspace functionality
        private void Backspace() 
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

        // 5. Method to add a decimal point to the current display text
        private void AddDecimalPoint() 
        {
            // Directly check if the current display text does not contain a decimal point
            if (!txtDisplay.Text.Contains("."))
            {
                // Add a decimal point to the current display text
                txtDisplay.Text += ".";
            }
        }

        // 6. Utility method to get the symbol of the operator for display
        private string GetOperatorSymbol(SelectedOperator selectedOp) 
        {
            switch (selectedOp)
            {
                case SelectedOperator.Addition: return "+";
                case SelectedOperator.Subtraction: return "-";
                case SelectedOperator.Multiplication: return "×";
                case SelectedOperator.Division: return "÷";
                default: return string.Empty;
            }
        }

        // 7. Method to clear the history
        private void ClearHistory() 
        {
            txtHistory.Text = string.Empty;
        }

        // 8."MC" (clear memory), "MR" (recall memory), "MS" (store memory), "M+" (add to memory), and "M-" (subtract from memory)
        private void DisplayErrorMessage(object sender, string message)
        {
            // Attempt to cast sender to Button, allowing for the possibility that it might be null
            Button? button = sender as Button;

            // Check if the cast was successful (i.e., button is not null) before proceeding
            if (button != null)
            {
                // Safely access the Content property of the button, providing a fallback empty string if it's null
                string buttonText = button.Content?.ToString() ?? string.Empty;

                // Switch on the buttonText to determine the appropriate memory operation
                switch (buttonText)
                {
                    case "MC": // Clear memory
                        memoryValue = 0;
                        break;
                    case "MR": // Recall memory
                        txtDisplay.Text = memoryValue.ToString();
                        isResultDisplayed = true;
                        break;
                    case "MS": // Store memory
                        if (double.TryParse(txtDisplay.Text, out double value))
                        {
                            memoryValue = value;
                        }
                        break;
                    case "M+": // Add to memory
                        if (double.TryParse(txtDisplay.Text, out double addValue))
                        {
                            memoryValue += addValue;
                        }
                        break;
                    case "M-": // Subtract from memory
                        if (double.TryParse(txtDisplay.Text, out double subtractValue))
                        {
                            memoryValue -= subtractValue;
                        }
                        break;
                }
            }
            else
            {
                LogError("The sender was not a Button or was null.");
                MessageBox.Show("An unexpected error occurred: " + message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Logging Method
        private void LogError(string message)
        {
            // Implement your logging logic here
            // For example, you might write to a log file, use a logging library, or write to the console
            Console.WriteLine(message);
        }



        // EVENT HANDLERS FOR SPECIFIC BUTTON CLICKS:

        // 1. Event handler for percentage button clicks
        private void PercentageButton_Click(object sender, RoutedEventArgs e) 
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                txtDisplay.Text = (value / 100).ToString();
            }
        }

        // 2. Event handler for clear entry button clicks
        private void ClearEntryButton_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = "0";
        }

        //3. Event handler for clear button clicks
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = "0";
            lastNumber = 0;
            result = 0;
            selectedOperator = SelectedOperator.None;
            isResultDisplayed = false;
            ClearHistory(); // Clear the history display
        }

        // 4. Event handler for backspace button clicks
        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            Backspace();
        }

        // 5. Event handler for decimal button clicks
        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            AddDecimalPoint();
        }

        // 6. Event handler for reciprocal button clicks
        private void ReciprocalButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value) && value != 0)
            {
                txtDisplay.Text = (1 / value).ToString();
            }
        }

        // 7. Event handler for square button clicks
        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                txtDisplay.Text = (value * value).ToString();
            }
        }

        // 8. Event handler for square root button clicks
        private void SquareRootButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                if (value < 0)
                {
                    txtDisplay.Text = "Invalid input for square root";
                    isResultDisplayed = true; // Prevent further input until cleared
                }
                else
                {
                    txtDisplay.Text = Math.Sqrt(value).ToString();
                    isResultDisplayed = true;
                }
            }
        }

        // 9. Event handler for +/- button clicks
        private void PlusMinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value))
            {
                value = -value; // Negate the value
                txtDisplay.Text = value.ToString();
            }
        }

        // 10. Event handler for equal button clicks
        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateResult();
        }


        // MEMORY FUNCTION IMPLEMENTATIONS:
        
        // 1. 
        private void MemoryClearButton_Click(object sender, RoutedEventArgs e) => memoryValue = 0;

        // 2. 
        private void MemoryRecallButton_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = memoryValue.ToString();
            isResultDisplayed = true;
        }
        
        // 3. 
        private void MemoryStoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value)) memoryValue = value;
        }
        
        // 4. 
        private void MemoryAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value)) memoryValue += value;
        }
        
        // 5. 
        private void MemorySubtractButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtDisplay.Text, out double value)) memoryValue -= value;
        }


        // ENUMERATION FOR SELECTED OPERATOR:
        public enum SelectedOperator { None, Addition, Subtraction, Multiplication, Division }

        private SelectedOperator GetOperatorFromContent(string content)
        {
            switch (content)
            {
                case "+":
                    return SelectedOperator.Addition;
                case "-":
                    return SelectedOperator.Subtraction;
                case "*":
                    return SelectedOperator.Multiplication;
                case "×":
                    return SelectedOperator.Multiplication;
                case "/":
                    return SelectedOperator.Division;
                case "÷":
                    return SelectedOperator.Division;

                default:
                    throw new ArgumentException("Invalid operator", nameof(content));
            }
        }

        // SUPPORTING CLASS FOR SIMPLE MATHEMATICAL OPERATIONS:
        public class SimpleMath
        {
            public static double Add(double n1, double n2) => n1 + n2;
            public static double Subtract(double n1, double n2) => n1 - n2;
            public static double Multiply(double n1, double n2) => n1 * n2;
            public static double Divide(double n1, double n2) => n1 / n2;
        }
    }
}
