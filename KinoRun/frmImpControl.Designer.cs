namespace KinoRun
{
    partial class frmImpControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(189, 83);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(66, 83);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(63, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(201, 13);
            this.lblInfo.TabIndex = 5;
            this.lblInfo.Text = "Неудалось определить поле строки: 9";
            // 
            // lblLine
            // 
            this.lblLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLine.ForeColor = System.Drawing.Color.Crimson;
            this.lblLine.Location = new System.Drawing.Point(9, 30);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(308, 13);
            this.lblLine.TabIndex = 8;
            this.lblLine.Text = "Проверьте правильность введения числа и нажмите ОК";
            // 
            // cmbField
            // 
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Items.AddRange(new object[] {
            "Русское название",
            "Оригинальное название",
            "Год выпуска",
            "Жанр",
            "Режисcёр",
            "Студия",
            "В ролях",
            "Продолжительность",
            "Страна",
            "Язык",
            "Описание"});
            this.cmbField.Location = new System.Drawing.Point(15, 56);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(288, 21);
            this.cmbField.TabIndex = 27;
            this.cmbField.Tag = "0";
            // 
            // frmImpControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 112);
            this.ControlBox = false;
            this.Controls.Add(this.cmbField);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblInfo);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(345, 150);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(345, 150);
            this.Name = "frmImpControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmImpControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.ComboBox cmbField;
    }
}