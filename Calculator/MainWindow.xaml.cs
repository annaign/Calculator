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
    public partial class MainWindow : Window
    {
        public string calculatingText = "";
        public string mathSymbol = "";
        public string copyMathSymbol = "";
        public string calculatedValue = "";
        public string copyCalculatedValue = "";
        public string newNumber = "";
        public string copyNewNumber = "";
        public string sign = "+";
        public double result = 0D;
        public bool keySwitch = false;
        public bool keyPointPushed = false;
        public bool keySqrtPushed = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForBegin();
            Show("ButtonClear_Click");
        }

        private void ButtonNumber_Click(object sender, RoutedEventArgs e)
        {
            if (!keySqrtPushed)
            {
                if (newNumber.Length == 0) keySwitch = false;

                string tmpNumber = ((Button)e.OriginalSource).Content.ToString();

                if (!keyPointPushed || (keyPointPushed && tmpNumber != ","))
                {
                    newNumber = newNumber + tmpNumber;
                    Show("ButtonNumber_Click");
                }

                if (tmpNumber == ",") keyPointPushed = true;
            }
        }

        private void ButtonMathSymbol_Click(object sender, RoutedEventArgs e)
        {
            if (newNumber.Length > 0)
            {
                if (sign == "-")
                {
                    newNumber = sign + newNumber;
                    if (!keySqrtPushed) calculatingText = calculatingText + "(" + newNumber + ")";

                    sign = "+";
                }
                else
                {
                    if (!keySqrtPushed) calculatingText = calculatingText + newNumber;
                }

                if (keySwitch == false)
                {
                    if (mathSymbol.Length > 0)
                        newNumber = Calc().ToString();

                    mathSymbol = ((Button)e.OriginalSource).Content.ToString();
                    calculatingText = calculatingText + mathSymbol;

                    if (mathSymbol != "=")
                    {
                        calculatedValue = newNumber;
                        newNumber = "";
                        keySwitch = true;

                        Show("ButtonMathSymbol_Click");
                    }
                    else
                    {
                        calculatingText = calculatingText + " " + newNumber;

                        Show("ButtonMathSymbol_Click");
                        ClearForBegin();
                    }
                }

                keyPointPushed = false;
                keySqrtPushed = false;
            }
        }

        private void ButtonSqrt_Click(object sender, RoutedEventArgs e)
        {
            if (!keySqrtPushed && (sign == "+" && newNumber.Length > 0))
            {
                keySqrtPushed = true;
                keyPointPushed = false;
                calculatingText = calculatingText + "√(" + newNumber + ")";

                string tmpOperatorValue = mathSymbol;
                mathSymbol = ((Button)e.OriginalSource).Content.ToString();
                newNumber = Calc().ToString();

                Show("ButtonSqrt_Click");

                if (mathSymbol.Length > 0) mathSymbol = tmpOperatorValue;
            }
        }

        private void ButtonSign_Click(object sender, RoutedEventArgs e)
        {
            if (!keySqrtPushed)
            {
                if (sign == "+") sign = "-";
                else sign = "+";

                Show("ButtonSign_Click");
            }
        }

        public void Show(string text)
        {
            switch (text)
            {
                case "ButtonNumber_Click":
                    if (sign == "-")
                        this.ScreenCalculator.Text = calculatingText + "(" + sign + newNumber + ")";
                    else
                        this.ScreenCalculator.Text = calculatingText + newNumber;

                    break;

                case "ButtonMathSymbol_Click":
                    if (mathSymbol != "=")
                    {
                        this.ScreenCalculator.Text = calculatingText;
                        this.SolutionCalculator.Text = "= " + calculatedValue;
                    }
                    else
                    {
                        this.ScreenCalculator.Text = "";
                        this.SolutionCalculator.Text = calculatingText;
                    }
                    break;

                case "ButtonSqrt_Click":
                    this.ScreenCalculator.Text = calculatingText;
                    break;

                case "ButtonSign_Click":
                    if (sign == "-")
                        this.ScreenCalculator.Text = calculatingText + "(" + sign + newNumber + ")";
                    else
                        this.ScreenCalculator.Text = calculatingText + newNumber;

                    break;

                case "ButtonClear_Click":
                    this.ScreenCalculator.Text = "";
                    this.SolutionCalculator.Text = "";
                    break;

                default:
                    this.ScreenCalculator.Text = "Ошибка";
                    this.SolutionCalculator.Text = "Ошибка";
                    break;
            }
        }

        public double Calc()
        {
            repeatCalc:

            switch (mathSymbol)
            {
                case "+":
                    result = (Double.Parse(calculatedValue) + Double.Parse(newNumber));
                    MakeCopyCalcData();
                    break;

                case "-":
                    result = (Double.Parse(calculatedValue) - Double.Parse(newNumber));
                    MakeCopyCalcData();
                    break;

                case "*":
                    if (copyCalculatedValue.Length == 0)
                    {
                        result = (Double.Parse(calculatedValue) * Double.Parse(newNumber));
                    }
                    else
                    {
                        result = (Double.Parse(copyNewNumber) * Double.Parse(newNumber));
                        ExtractCopiedCalcData();

                        goto repeatCalc;
                    }
                    break;

                case "/":
                    if (copyCalculatedValue == "")
                    {
                        result = (Double.Parse(calculatedValue) / Double.Parse(newNumber));
                    }
                    else
                    {
                        result = (Double.Parse(copyNewNumber) / Double.Parse(newNumber));
                        ExtractCopiedCalcData();

                        goto repeatCalc;
                    }
                    break;

                case "√":
                    result = Math.Sqrt(Double.Parse(newNumber));
                    break;
            }
            return result;
        }

        public void MakeCopyCalcData()
        {
            copyMathSymbol = mathSymbol;
            copyCalculatedValue = calculatedValue;
            copyNewNumber = newNumber;
        }

        public void ExtractCopiedCalcData()
        {
            newNumber = result.ToString();
            mathSymbol = copyMathSymbol;
            calculatedValue = copyCalculatedValue;
            copyCalculatedValue = "";
        }
        public void ClearForBegin()
        {
            calculatingText = "";
            mathSymbol = "";
            copyMathSymbol = "";
            calculatedValue = "";
            copyCalculatedValue = "";
            newNumber = "";
            copyNewNumber = "";
            sign = "+";
            result = 0D;
            keySwitch = false;
            keyPointPushed = false;
            keySqrtPushed = false;
        }

    }
}
