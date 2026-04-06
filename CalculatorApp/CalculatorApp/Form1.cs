using System;
using System.Globalization;
using System.Windows.Forms;

namespace CalculatorApp
{
    public partial class Form1 : Form
    {
        private double _firstNumber;
        private Operation _operation = Operation.None;

        private enum Operation
        {
            None,
            Add,
            Subtract,
            Multiply,
            Divide
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
    
        }

        // --- Digit buttons ---
        private void button1_Click(object sender, EventArgs e) => AppendDigit("1");
        private void button2_Click(object sender, EventArgs e) => AppendDigit("2");
        private void button3_Click(object sender, EventArgs e) => AppendDigit("3");
        private void button4_Click(object sender, EventArgs e) => AppendDigit("4");
        private void button5_Click(object sender, EventArgs e) => AppendDigit("5");
        private void button6_Click(object sender, EventArgs e) => AppendDigit("6");
        private void button7_Click(object sender, EventArgs e) => AppendDigit("7");
        private void button8_Click(object sender, EventArgs e) => AppendDigit("8");
        private void button9_Click(object sender, EventArgs e) => AppendDigit("9");
        private void button10_Click(object sender, EventArgs e) => AppendDigit("0");

        private void AppendDigit(string digit)
        {
            if (txtDisplay.Text == "0")
                txtDisplay.Text = digit;
            else
                txtDisplay.Text += digit;
        }

        // --- Operator buttons ---
        private void button11_Click(object sender, EventArgs e) => SetOperation(Operation.Add);
        private void button12_Click(object sender, EventArgs e) => SetOperation(Operation.Subtract);
        private void button13_Click(object sender, EventArgs e) => SetOperation(Operation.Multiply);
        private void button14_Click(object sender, EventArgs e) => SetOperation(Operation.Divide);

        private void SetOperation(Operation op)
        {
            if (!TryParseDisplay(out _firstNumber))
                return;

            _operation = op;
            txtDisplay.Clear();
            txtDisplay.Focus();
        }

        // --- Clear ---
        private void button16_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            txtDisplay.Clear();
            _operation = Operation.None;
            _firstNumber = 0;
        }

        // --- Equals ---
        private void button15_Click(object sender, EventArgs e)
        {
            if (_operation == Operation.None)
            {
                MessageBox.Show("No operation selected.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!TryParseDisplay(out double secondNumber))
                return;

            double result;

            switch (_operation)
            {
                case Operation.Add:
                    result = _firstNumber + secondNumber;
                    break;
                case Operation.Subtract:
                    result = _firstNumber - secondNumber;
                    break;
                case Operation.Multiply:
                    result = _firstNumber * secondNumber;
                    break;
                case Operation.Divide:
                    if (Math.Abs(secondNumber) < double.Epsilon)
                    {
                        MessageBox.Show("Cannot divide by zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    result = _firstNumber / secondNumber;
                    break;
                default:
                    MessageBox.Show("Unknown operation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            txtDisplay.Text = result.ToString(CultureInfo.CurrentCulture);

            _firstNumber = result;
            _operation = Operation.None;
        }

        // --- Helpers ---
        private bool TryParseDisplay(out double value)
        {
            value = 0;

            if (string.IsNullOrWhiteSpace(txtDisplay.Text))
            {
                MessageBox.Show("Please enter a number first.", "Input required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!double.TryParse(txtDisplay.Text, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out value))
            {
                MessageBox.Show("Invalid number format.", "Parse error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
