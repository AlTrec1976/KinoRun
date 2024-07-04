namespace KinoRun
{
    partial class frmTimes
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnComplete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTimes = new System.Windows.Forms.TextBox();
            this.mtxtTime = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(22, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Калькулятор времени";
            // 
            // btnComplete
            // 
            this.btnComplete.Location = new System.Drawing.Point(197, 197);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(75, 23);
            this.btnComplete.TabIndex = 1;
            this.btnComplete.Text = "Завершить";
            this.btnComplete.UseVisualStyleBackColor = true;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 197);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Отменить";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtTimes
            // 
            this.txtTimes.Location = new System.Drawing.Point(12, 88);
            this.txtTimes.Multiline = true;
            this.txtTimes.Name = "txtTimes";
            this.txtTimes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTimes.Size = new System.Drawing.Size(260, 57);
            this.txtTimes.TabIndex = 3;
            this.txtTimes.TextChanged += new System.EventHandler(this.txtTimes_TextChanged);
            // 
            // mtxtTime
            // 
            this.mtxtTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mtxtTime.Location = new System.Drawing.Point(102, 151);
            this.mtxtTime.Mask = "99:99:99";
            this.mtxtTime.Name = "mtxtTime";
            this.mtxtTime.PromptChar = '0';
            this.mtxtTime.ReadOnly = true;
            this.mtxtTime.ResetOnSpace = false;
            this.mtxtTime.Size = new System.Drawing.Size(79, 29);
            this.mtxtTime.SkipLiterals = false;
            this.mtxtTime.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 45);
            this.label2.TabIndex = 54;
            this.label2.Text = "Внесите в поле продолжительность частей фильма в формате \"чч:мм:сс\", разделяя их " +
    "между собой пробелом";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(106, 197);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 55;
            this.btnCalculate.Text = "Посчитать";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // frmTimes
            // 
            this.AcceptButton = this.btnComplete;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(284, 226);
            this.ControlBox = false;
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mtxtTime);
            this.Controls.Add(this.txtTimes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(300, 264);
            this.MinimumSize = new System.Drawing.Size(300, 264);
            this.Name = "frmTimes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Калькулятор времени";
            this.Load += new System.EventHandler(this.frmTimes_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtTimes;
        private System.Windows.Forms.MaskedTextBox mtxtTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCalculate;
    }
}