using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            //объект - файл
            StreamReader sr = new StreamReader(@"111.txt");
            //строка файла 
            String line = sr.ReadLine();
            //количество строк в файле
            int n = 0;
            numericUpDown1.Maximum = 0;
            //пока файл не пустой добавляем данные в списки
            while (line!=null)
            {
                string[] s = line.Split(';');
                comboBox1.Items.Add(s[0]); //фамилия и имя
                comboBox2.Items.Add(s[1]); //должность
                comboBox3.Items.Add(s[2]); //оклад
                line = sr.ReadLine();
                n++;
                numericUpDown1.Maximum++;
            }
            //определяем максимальное количество элементов
            numericUpDown1.Maximum = --n;
            numericUpDown2.Maximum = numericUpDown1.Maximum;
            sr.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            button5.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String[] line = File.ReadAllLines("111.txt");
            if ((comboBox1.Text == "") & (comboBox2.Text == "") & (comboBox3.Text == ""))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                //File.AppendAllText("111.txt", "\n");
                File.AppendAllText("111.txt", "\n"+comboBox1.Text + "; " + comboBox2.Text + "; " + comboBox3.Text);
                button5.Visible = false;
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                Form1_Load(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"111.txt");
            String line = sr.ReadLine();
            int k = 0;
            while (line != null)
            {

                if (k == numericUpDown1.Value)
                {
                    MessageBox.Show(Convert.ToString(numericUpDown1.Value) + " " + line);
                };

                line = sr.ReadLine();
                k++;
            }
            sr.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //выводим содержимое списков
            MessageBox.Show(Convert.ToString(numericUpDown2.Value) + " "
                + comboBox1.Items[(int)numericUpDown2.Value] + " "
                + comboBox2.Items[(int)numericUpDown2.Value] + " "
                + comboBox3.Items[(int)numericUpDown2.Value]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы уверены что хотите удалить строку?" + " " + comboBox1.Items[(int)numericUpDown2.Value] + " "
                + comboBox2.Items[(int)numericUpDown2.Value] + " "
                + comboBox3.Items[(int)numericUpDown2.Value]);
            String[] line = File.ReadAllLines("111.txt");

            int n = (int)numericUpDown2.Value;
            for (n = (int)numericUpDown2.Value; n <= numericUpDown2.Maximum; n++)
            {
                line[n] = line[n++];
                if (n == numericUpDown2.Maximum)
                {
                    System.Array.Resize(ref line, line.Length - 1);
                    line[n] = line[n++];
                    continue;
                }
            }

            System.Array.Resize(ref line, line.Length - 1);
            numericUpDown2.Maximum--;
            numericUpDown1.Maximum = numericUpDown2.Maximum;
            File.Delete("111.txt");
            File.AppendAllLines("111.txt", line);
            Form1_Load(sender, e);
        }
    }
}
