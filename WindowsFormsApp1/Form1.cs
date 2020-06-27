using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    // read more: http://www.albahari.com/threading/part2.aspx
    // https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskscheduler?view=netcore-3.1
    public partial class Form1 : Form
    {
        private Thread thread1;
        private Task task1;
        public Form1()
        {
            InitializeComponent();
        }

        private int RecursiveFibonacci(int n)
        {
            if (n <= 1)
            {
                return n;
            }

            var t1 = RecursiveFibonacci(n - 1);
            var t2 = RecursiveFibonacci(n - 2);
            return t1 + t2;

        }

        private void RunRecursiveFibonacci(int n)
        {
            string result = RecursiveFibonacci(n).ToString();
            this.Invoke((MethodInvoker)delegate () { textBox2.Text = result; });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //thread1 = new Thread(RunRecursiveFibonacci);
            textBox2.Text = "Computing...";
            //thread1.Start(int.Parse(textBox1.Text));
            //task1 = new Task(() => RunRecursiveFibonacci(int.Parse(textBox1.Text)));
            //task1.Start();
            var t = Task.Factory.StartNew(() => RecursiveFibonacci(int.Parse(textBox1.Text)));
            int result = await t;
            textBox2.Text = result.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            thread1.Abort();
            textBox2.Text = "Cancelled.";
        }
    }
}
