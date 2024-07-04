using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinoRun
{
    public partial class frmInput : Form
    {
        public frmInput()
        {
            InitializeComponent();
        }

        bool t; // Если была нажата кнопка Ok тогда t = true
                // если была нажата кнопка Cancel или в текстовое поле ничего не введено, то t = false
        String temp;
        bool chk;

        public static bool Input(String IBhead, // заголовок формы
        String IBlabel, // текст, который будет отображен в lable1
        String IBcheck, // текст, который будет отображен в lable1
        out String s, bool c // значение введенное в текстовое поле, вернется из метода
        )
        {
            frmInput IBform = new frmInput(); // создаём форму
            IBform.Text = IBhead; // меняем текст заголовка формы
            IBform.label1.Text = IBlabel; // меняем текст метки
            if (IBcheck != "")
            {
                IBform.checkBox1.Text = IBcheck;
                IBform.checkBox1.Visible = true;
            }
            else
            {
                IBform.checkBox1.Visible = false;
            }

            IBform.ShowDialog(); // показываем форму
            s = IBform.temp; // возвращаем введнное значение в s
            return IBform.t;
        }

        private void btnOk_Click(object sender, EventArgs e) // Ok
        {
            temp = this.textBox1.Text;
            chk = this.checkBox1.Checked;
            t = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите отменить данное действие?", "Действие отменено", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                t = false;
                this.Close();
            }
        }
    }
}
