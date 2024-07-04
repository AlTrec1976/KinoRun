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
    public partial class frmImpControl : Form
    {
        public frmImpControl()
        {
            InitializeComponent();
        }
        bool t; // Если была нажата кнопка Ok тогда t = true
                // если была нажата кнопка Cancel или в текстовое поле ничего не введено, то t = false
        int temp;
        static int[] _temp = new int[1];

        public static bool Input(String IBhead, // заголовок формы
        String IBlabel, // текст, который будет отображен в верхней метке
        String IBlblText, // текст, который будет отображен в нижней метке
        string[] IBFields, // Массив свободных значений 
        int[] IBArray, // Массив свободных значений 
        out int s // значение введенное в текстовое поле, вернется из метода
        )
        {
            frmImpControl IBform = new frmImpControl(); // создаём форму
            IBform.Text = IBhead; // меняем текст заголовка формы
            IBform.lblInfo.Text = IBlabel; // меняем текст верхней метки
            IBform.lblLine.Text = IBlblText; // меняем текст нижней метки
            IBform.cmbField.Items.Clear();
            _temp = IBArray;
            for(int i = 0; i < IBArray.Length; i++)
            {
                //IBform.cmbField.Items.Add(IBFields[IBArray[i]]);
                string str = IBFields[IBArray[i]];
                IBform.cmbField.Items.Add(str);
            }

            IBform.ShowDialog(); // показываем форму
            s = IBform.temp; // возвращаем введнное значение в s
            return IBform.t;
        }

        private void btnOk_Click(object sender, EventArgs e) // Ok
        {
            if (this.cmbField.SelectedIndex > -1)
            {
                temp = _temp[this.cmbField.SelectedIndex];
                t = true;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите отменить данное действие?\nВ случае отмены указанная строка будет добавлена в поле Описание.", "Действие отменено", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                t = false;
                this.Close();
            }
        }
    }
}
