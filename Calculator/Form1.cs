/**
 * @name - #Calculator
 * @description Calculator that implements basic math functions ('+', '-', 'X', '÷', '√', '^') using hash table logic.
 * @author - Emils Seflers
 * @date - 22/09/2022
 * github - 
**/

using System;
using System.Collections;

namespace Calculator
{
    public partial class MyCalculator : Form
    {
        char[] operators = { '+', '-', 'X', '÷', '√', '^' };
        string tempOperator;
        string expression;
        Hashtable exprData = new Hashtable();
        public MyCalculator()
        {
            exprData.Add("rightNum", "");
            exprData.Add("leftNum", "");

            InitializeComponent();
        }
        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text = "";
            textBoxResult.Text = "";
            exprData.Clear();
            exprData.Add("rightNum", "");
            exprData.Add("leftNum", "");
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            int rightNumLength = exprData["rightNum"].ToString().Length;
            int leftNumLength = exprData["leftNum"].ToString().Length;

            if (!exprData["rightNum"].ToString().Equals("")) {
                exprData["rightNum"] = exprData["rightNum"].ToString().Remove(rightNumLength - 1, 1);
                textBoxResult.Text = exprData["rightNum"].ToString();
            }
            else if (exprData.ContainsKey("mathOperator"))
            {
                exprData.Remove("mathOperator");
                textBoxExpression.Text = "";
                textBoxResult.Text = exprData["leftNum"].ToString();
            }
            else if (!exprData["leftNum"].Equals(""))
            {
                exprData["leftNum"] = exprData["leftNum"].ToString().Remove(leftNumLength - 1, 1);
                textBoxResult.Text = exprData["leftNum"].ToString();
            }

            
        }
        private void buttonNum_Click(object sender, EventArgs e)
        {
            if (textBoxExpression.Text == "ERROR")
                textBoxExpression.Text = "";

            if (exprData.ContainsKey("mathOperator"))
            {
                if (exprData["rightNum"].Equals("0"))
                    exprData["rightNum"] = (sender as Button).Text;
                else
                    exprData["rightNum"] += (sender as Button).Text;
                textBoxResult.Text = exprData["rightNum"].ToString();
            }
            else
            {
                if (exprData["leftNum"].Equals("0"))
                    exprData["leftNum"] = (sender as Button).Text;
                else
                    exprData["leftNum"] += (sender as Button).Text;
                textBoxResult.Text = exprData["leftNum"].ToString();
            }

            //(sender as Button).Text;
        }
        private void buttonOperator_Click(object sender, EventArgs e)
        {   // TODO ja nospiez operatoru, kad tads jau ir, tad parmaina ieprieksejo
            if (textBoxExpression.Text == "ERROR")
                textBoxExpression.Text = "";

            tempOperator = (sender as Button).Text;
            if (tempOperator == "√")
            {
                double leftNum = double.Parse(exprData["leftNum"].ToString());
                if (!exprData["leftNum"].Equals("") && leftNum > 0)
                {
                    textBoxExpression.Text = $"√({exprData["leftNum"]})=";
                    exprData["leftNum"] = Math.Sqrt(leftNum);
                    textBoxResult.Text = exprData["leftNum"].ToString();
                }
                else if(leftNum < 0)
                {
                    textBoxExpression.Text = "ERROR";
                }
            }  
            else
            {
                if(exprData.ContainsKey("mathOperator") && !exprData["mathOperator"].Equals("-")  && exprData["rightNum"].Equals("") && tempOperator.Equals("-"))
                {
                    exprData["rightNum"] += tempOperator;
                    textBoxResult.Text = exprData["leftNum"].ToString();
                }
                if (!exprData.ContainsKey("mathOperator"))
                {
                    if (!exprData["leftNum"].Equals(""))
                    {
                        exprData.Add("mathOperator", (sender as Button).Text);
                        textBoxExpression.Text = exprData["leftNum"].ToString() + exprData["mathOperator"].ToString();
                    }
                    else
                    {
                        if (tempOperator.Equals("-"))
                        {
                            exprData["leftNum"] += tempOperator;
                            textBoxResult.Text = exprData["leftNum"].ToString();
                        }
                    }
                }
            }   
        }
        private void Calculate(object sender, EventArgs e)
        {
            float leftNum, rightNum, result;
            if (!exprData["leftNum"].Equals("") && !exprData["rightNum"].Equals(""))
            {
                leftNum = float.Parse(exprData["leftNum"].ToString());
                rightNum = float.Parse(exprData["rightNum"].ToString());
                switch (exprData["mathOperator"]) 
                {
                    case "+":
                        textBoxExpression.Text = $"{leftNum}+{rightNum}=";
                        exprData["leftNum"] = leftNum + rightNum;
                        break;
                    case "-":
                        textBoxExpression.Text = $"{leftNum}-{rightNum}=";  
                        exprData["leftNum"] = leftNum - rightNum;
                        break;
                    case "X":
                        if(rightNum >= 0)
                            textBoxExpression.Text = $"{leftNum}x{rightNum}=";
                        else
                            textBoxExpression.Text = $"{leftNum}x({rightNum})=";
                        exprData["leftNum"] = leftNum * rightNum;
                        break;
                    case "÷":
                        if(rightNum != 0)
                        {
                            if(rightNum >= 0)
                                textBoxExpression.Text = $"{leftNum}/{rightNum}=";
                            else
                                textBoxExpression.Text = $"{leftNum}/({rightNum})=";
                            exprData["leftNum"] = leftNum / rightNum;
                        }
                        else
                        {
                            textBoxExpression.Text = "ERROR";
                        }
                        break;
                    case "√":
                        // see buttonOperator_Click 2nd if statement (line 80);
                        break;
                    case "^":
                        if(rightNum >= 0)
                            textBoxExpression.Text = $"{leftNum}^{rightNum}=";
                        else
                            textBoxExpression.Text = $"{leftNum}^({rightNum})=";
                        exprData["leftNum"] = Math.Pow(leftNum,rightNum);
                        break;
                    default:
                        break;
                }
                exprData["rightNum"] = "";
                exprData.Remove("mathOperator");
                textBoxResult.Text = exprData["leftNum"].ToString();
            }
        }

        private void buttonDecPoint(object sender, EventArgs e)
        {
            if (!exprData["rightNum"].Equals("") && !exprData["rightNum"].ToString().Contains("."))
            {
                exprData["rightNum"] += ".";
                textBoxResult.Text = exprData["rightNum"].ToString();
            }
            else if (!exprData["leftNum"].Equals("") && !exprData["leftNum"].ToString().Contains("."))
            {
                exprData["leftNum"] += ".";
                textBoxResult.Text = exprData["leftNum"].ToString();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}