using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Real_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Nullable<double> a = null, b = null, lastnum = null;
        char op = (char)0;
        bool clear_res = false;
        bool last = true;
        bool clearlastnum = false;
        bool putina = false;

        private Nullable<double> Calculate(Nullable<double> num1, Nullable<double> num2)
        {
            switch (op)
            {
                case '+':
                    textBox1.Text = (a = num1 + num2).ToString();
                    break;

                case '-':
                    textBox1.Text = (a = num1 - num2).ToString();
                    break;

                case '*':
                    textBox1.Text = (a = num1 * num2).ToString();
                    break;

                case '/':
                    if (num2 == 0) textBox1.Text = "error";
                    else textBox1.Text = (a = num1 / num2).ToString();
                    break;
            }

            return a;
        }

        private void Assign_to_var()
        {
            if (op == 0 || putina)
            {
                a = Convert.ToDouble(textBox1.Text);
            }
            else
            {
                if (a != null && lastnum == null)
                {
                    b = Convert.ToDouble(textBox1.Text);
                }
                else
                {
                    if (last)
                    {
                        lastnum = 0;
                        last = false;
                    }
                    lastnum = Convert.ToDouble(textBox1.Text);
                }
            }
        }

        private void Calculate()
        {
            if ((lastnum == null && op == 0) && (a == null && b == null))
                return;

            textBox1.Text = ((lastnum != null && op != 0) ? Calculate(a, lastnum) : Calculate(a, b)).ToString();

            if (b != null) lastnum = b;
            b = null;
            last = true;
        }

        private void Add_to_display(double digit , bool c)
        {
            if (c)
            {
                textBox1.Text = "";
                clear_res = false;
            }

            textBox1.Text += digit.ToString();


            Assign_to_var();
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            if (clearlastnum)
                lastnum = null;

            if (a != null && b != null) Calculate();
            else if (a != null && lastnum != null) Calculate();
            op = Convert.ToChar(((Button)sender).Tag);
            clear_res = true;
            putina = false;
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            if (a != null && b == null && op != 0 && lastnum == null)
                b = a;


            Calculate();
            clearlastnum = true;
            putina = true;
        }

        private void Reset_calc()
        {
            lastnum = a = b = null;
            op = (char)0;
            textBox1.Text = "";
            clear_res = false;
            last = true;
            clearlastnum = false;
            putina = false;
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            Reset_calc();
        }

        private void button19_MouseHover(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.LightGray;
        }

        private void button19_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.DarkGray;
        }

        private void Operation(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0) return;
            double d = Convert.ToDouble(textBox1.Text);
            
            switch (((Button)sender).Text)
            {
                case "1/x":
                    textBox1.Text = (1/d).ToString();
                    break;

                case "√":
                    textBox1.Text = (Math.Sqrt(d)).ToString();
                    break;

                case "x²":
                    textBox1.Text = (d*d).ToString();
                    break;

                case "±":
                    textBox1.Text = (-d).ToString();
                    break;

                case "%":
                    textBox1.Text = (d / 100).ToString();
                    break;
            }
            
            Assign_to_var();
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            Assign_to_var();
            textBox1.Text = "";
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if(((Button)sender).Text == ".")
            {
                if(!textBox1.Text.Contains("."))
                    textBox1.Text += ".";
                Assign_to_var();
            }
            else
                Add_to_display(Convert.ToByte(((Button)sender).Text), clear_res);
        }
    }
}