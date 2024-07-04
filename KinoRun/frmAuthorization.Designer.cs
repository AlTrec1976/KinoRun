namespace KinoRun
{
    partial class frmAuthorization
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
            this.grpSite = new System.Windows.Forms.GroupBox();
            this.pnlEmail = new System.Windows.Forms.Panel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkReg = new System.Windows.Forms.CheckBox();
            this.pnlAuth = new System.Windows.Forms.Panel();
            this.chkSave = new System.Windows.Forms.CheckBox();
            this.chkPass = new System.Windows.Forms.CheckBox();
            this.picTest = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.grpSite.SuspendLayout();
            this.pnlEmail.SuspendLayout();
            this.pnlAuth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTest)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSite
            // 
            this.grpSite.Controls.Add(this.pnlEmail);
            this.grpSite.Controls.Add(this.chkReg);
            this.grpSite.Controls.Add(this.pnlAuth);
            this.grpSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.grpSite.Location = new System.Drawing.Point(2, 3);
            this.grpSite.Name = "grpSite";
            this.grpSite.Size = new System.Drawing.Size(382, 124);
            this.grpSite.TabIndex = 1;
            this.grpSite.TabStop = false;
            this.grpSite.Text = "PicPicture.com";
            // 
            // pnlEmail
            // 
            this.pnlEmail.Controls.Add(this.txtEmail);
            this.pnlEmail.Controls.Add(this.label1);
            this.pnlEmail.Location = new System.Drawing.Point(107, 9);
            this.pnlEmail.Name = "pnlEmail";
            this.pnlEmail.Size = new System.Drawing.Size(269, 29);
            this.pnlEmail.TabIndex = 8;
            this.pnlEmail.Visible = false;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(40, 3);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(227, 20);
            this.txtEmail.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "E-Mail";
            // 
            // chkReg
            // 
            this.chkReg.AutoSize = true;
            this.chkReg.Location = new System.Drawing.Point(10, 19);
            this.chkReg.Name = "chkReg";
            this.chkReg.Size = new System.Drawing.Size(91, 17);
            this.chkReg.TabIndex = 7;
            this.chkReg.Text = "Регистрация";
            this.chkReg.UseVisualStyleBackColor = true;
            this.chkReg.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // pnlAuth
            // 
            this.pnlAuth.Controls.Add(this.chkSave);
            this.pnlAuth.Controls.Add(this.chkPass);
            this.pnlAuth.Controls.Add(this.picTest);
            this.pnlAuth.Controls.Add(this.label6);
            this.pnlAuth.Controls.Add(this.txtLogin);
            this.pnlAuth.Controls.Add(this.txtPass);
            this.pnlAuth.Controls.Add(this.label7);
            this.pnlAuth.Location = new System.Drawing.Point(6, 42);
            this.pnlAuth.Name = "pnlAuth";
            this.pnlAuth.Size = new System.Drawing.Size(370, 76);
            this.pnlAuth.TabIndex = 6;
            // 
            // chkSave
            // 
            this.chkSave.AutoSize = true;
            this.chkSave.Checked = true;
            this.chkSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSave.Location = new System.Drawing.Point(49, 53);
            this.chkSave.Name = "chkSave";
            this.chkSave.Size = new System.Drawing.Size(187, 17);
            this.chkSave.TabIndex = 6;
            this.chkSave.Text = "сохранить данные авторизации";
            this.chkSave.UseVisualStyleBackColor = true;
            // 
            // chkPass
            // 
            this.chkPass.AutoSize = true;
            this.chkPass.Location = new System.Drawing.Point(284, 30);
            this.chkPass.Name = "chkPass";
            this.chkPass.Size = new System.Drawing.Size(75, 17);
            this.chkPass.TabIndex = 5;
            this.chkPass.Text = "Показать";
            this.chkPass.UseVisualStyleBackColor = true;
            this.chkPass.CheckedChanged += new System.EventHandler(this.chkPass_CheckedChanged);
            // 
            // picTest
            // 
            this.picTest.Image = global::KinoRun.Properties.Resources._false;
            this.picTest.Location = new System.Drawing.Point(0, 3);
            this.picTest.Name = "picTest";
            this.picTest.Size = new System.Drawing.Size(40, 40);
            this.picTest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTest.TabIndex = 2;
            this.picTest.TabStop = false;
            this.picTest.Tag = "pic";
            this.picTest.Click += new System.EventHandler(this.picTest_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Логин";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(90, 1);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(278, 20);
            this.txtLogin.TabIndex = 1;
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(90, 27);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(188, 20);
            this.txtPass.TabIndex = 2;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(46, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Пароль";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(237, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(137, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Применить";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnHide
            // 
            this.btnHide.Location = new System.Drawing.Point(40, 3);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(75, 23);
            this.btnHide.TabIndex = 5;
            this.btnHide.Text = "Тест";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnCancel);
            this.pnlButtons.Controls.Add(this.btnHide);
            this.pnlButtons.Controls.Add(this.btnOk);
            this.pnlButtons.Location = new System.Drawing.Point(12, 127);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(364, 35);
            this.pnlButtons.TabIndex = 7;
            // 
            // frmAuthorization
            // 
            this.AcceptButton = this.btnHide;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(388, 162);
            this.ControlBox = false;
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.grpSite);
            this.MaximumSize = new System.Drawing.Size(404, 200);
            this.MinimumSize = new System.Drawing.Size(404, 200);
            this.Name = "frmAuthorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Авторизация на сайте ";
            this.Text = "frmAuthorization";
            this.Load += new System.EventHandler(this.frmAuthorization_Load);
            this.grpSite.ResumeLayout(false);
            this.grpSite.PerformLayout();
            this.pnlEmail.ResumeLayout(false);
            this.pnlEmail.PerformLayout();
            this.pnlAuth.ResumeLayout(false);
            this.pnlAuth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTest)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSite;
        private System.Windows.Forms.PictureBox picTest;
        private System.Windows.Forms.CheckBox chkPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Panel pnlEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkReg;
        private System.Windows.Forms.Panel pnlAuth;
        private System.Windows.Forms.Panel pnlButtons;
    }
}