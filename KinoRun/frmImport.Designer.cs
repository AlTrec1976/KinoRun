namespace KinoRun
{
    partial class frmImport
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImport));
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkAuto = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.chkScreenshots = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.cmbField11 = new System.Windows.Forms.ComboBox();
            this.cmbField10 = new System.Windows.Forms.ComboBox();
            this.cmbField9 = new System.Windows.Forms.ComboBox();
            this.cmbField8 = new System.Windows.Forms.ComboBox();
            this.cmbField7 = new System.Windows.Forms.ComboBox();
            this.cmbField6 = new System.Windows.Forms.ComboBox();
            this.cmbField5 = new System.Windows.Forms.ComboBox();
            this.cmbField4 = new System.Windows.Forms.ComboBox();
            this.cmbField3 = new System.Windows.Forms.ComboBox();
            this.cmbField2 = new System.Windows.Forms.ComboBox();
            this.cmbField1 = new System.Windows.Forms.ComboBox();
            this.rtxtInfo_Video = new System.Windows.Forms.RichTextBox();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuContext_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext_Past = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext_Select = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext_Sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContext_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext_Del = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContext_Sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuContext_Translate = new System.Windows.Forms.ToolStripMenuItem();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtInfo_Images = new System.Windows.Forms.TextBox();
            this.txtInfo_Tech = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtInfo_Video = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCencel = new System.Windows.Forms.Button();
            this.mnuContext_View = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(2, -2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(825, 53);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkAuto);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.chkScreenshots);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.cmbField11);
            this.panel1.Controls.Add(this.cmbField10);
            this.panel1.Controls.Add(this.cmbField9);
            this.panel1.Controls.Add(this.cmbField8);
            this.panel1.Controls.Add(this.cmbField7);
            this.panel1.Controls.Add(this.cmbField6);
            this.panel1.Controls.Add(this.cmbField5);
            this.panel1.Controls.Add(this.cmbField4);
            this.panel1.Controls.Add(this.cmbField3);
            this.panel1.Controls.Add(this.cmbField2);
            this.panel1.Controls.Add(this.cmbField1);
            this.panel1.Controls.Add(this.rtxtInfo_Video);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txtInfo_Images);
            this.panel1.Controls.Add(this.txtInfo_Tech);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtInfo_Video);
            this.panel1.Location = new System.Drawing.Point(2, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(825, 560);
            this.panel1.TabIndex = 1;
            // 
            // chkAuto
            // 
            this.chkAuto.AutoSize = true;
            this.chkAuto.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAuto.Location = new System.Drawing.Point(7, 302);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(116, 17);
            this.chkAuto.TabIndex = 38;
            this.chkAuto.Text = "Автоопределение";
            this.chkAuto.UseVisualStyleBackColor = true;
            this.chkAuto.CheckedChanged += new System.EventHandler(this.chkAuto_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(98, 492);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 13);
            this.label16.TabIndex = 25;
            this.label16.Text = "Скриншоты";
            // 
            // chkScreenshots
            // 
            this.chkScreenshots.AutoSize = true;
            this.chkScreenshots.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkScreenshots.Checked = true;
            this.chkScreenshots.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkScreenshots.Location = new System.Drawing.Point(33, 491);
            this.chkScreenshots.Name = "chkScreenshots";
            this.chkScreenshots.Size = new System.Drawing.Size(130, 17);
            this.chkScreenshots.TabIndex = 24;
            this.chkScreenshots.Text = "Скриншоты в строку";
            this.chkScreenshots.UseVisualStyleBackColor = true;
            this.chkScreenshots.Visible = false;
            this.chkScreenshots.CheckedChanged += new System.EventHandler(this.chkScreenshots_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(63, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 37;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Visible = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cmbField11
            // 
            this.cmbField11.FormattingEnabled = true;
            this.cmbField11.Items.AddRange(new object[] {
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
            this.cmbField11.Location = new System.Drawing.Point(7, 275);
            this.cmbField11.Name = "cmbField11";
            this.cmbField11.Size = new System.Drawing.Size(153, 21);
            this.cmbField11.TabIndex = 36;
            this.cmbField11.Tag = "10";
            this.cmbField11.TextChanged += new System.EventHandler(this.cmbField_TextChanged);
            // 
            // cmbField10
            // 
            this.cmbField10.FormattingEnabled = true;
            this.cmbField10.Items.AddRange(new object[] {
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
            this.cmbField10.Location = new System.Drawing.Point(6, 250);
            this.cmbField10.Name = "cmbField10";
            this.cmbField10.Size = new System.Drawing.Size(153, 21);
            this.cmbField10.TabIndex = 35;
            this.cmbField10.Tag = "9";
            this.cmbField10.TextChanged += new System.EventHandler(this.cmbField_TextChanged);
            // 
            // cmbField9
            // 
            this.cmbField9.FormattingEnabled = true;
            this.cmbField9.Items.AddRange(new object[] {
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
            this.cmbField9.Location = new System.Drawing.Point(7, 225);
            this.cmbField9.Name = "cmbField9";
            this.cmbField9.Size = new System.Drawing.Size(153, 21);
            this.cmbField9.TabIndex = 34;
            this.cmbField9.Tag = "8";
            this.cmbField9.TextChanged += new System.EventHandler(this.cmbField_TextChanged);
            // 
            // cmbField8
            // 
            this.cmbField8.FormattingEnabled = true;
            this.cmbField8.Items.AddRange(new object[] {
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
            this.cmbField8.Location = new System.Drawing.Point(6, 201);
            this.cmbField8.Name = "cmbField8";
            this.cmbField8.Size = new System.Drawing.Size(153, 21);
            this.cmbField8.TabIndex = 33;
            this.cmbField8.Tag = "7";
            this.cmbField8.TextChanged += new System.EventHandler(this.cmbField_TextChanged);
            // 
            // cmbField7
            // 
            this.cmbField7.FormattingEnabled = true;
            this.cmbField7.Items.AddRange(new object[] {
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
            this.cmbField7.Location = new System.Drawing.Point(6, 177);
            this.cmbField7.Name = "cmbField7";
            this.cmbField7.Size = new System.Drawing.Size(153, 21);
            this.cmbField7.TabIndex = 32;
            this.cmbField7.Tag = "6";
            this.cmbField7.TextChanged += new System.EventHandler(this.cmbField_TextChanged);
            // 
            // cmbField6
            // 
            this.cmbField6.FormattingEnabled = true;
            this.cmbField6.Items.AddRange(new object[] {
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
            this.cmbField6.Location = new System.Drawing.Point(6, 152);
            this.cmbField6.Name = "cmbField6";
            this.cmbField6.Size = new System.Drawing.Size(153, 21);
            this.cmbField6.TabIndex = 31;
            this.cmbField6.Tag = "5";
            this.cmbField6.TextChanged += new System.EventHandler(this.cmbField_TextChanged);
            // 
            // cmbField5
            // 
            this.cmbField5.FormattingEnabled = true;
            this.cmbField5.Items.AddRange(new object[] {
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
            this.cmbField5.Location = new System.Drawing.Point(6, 128);
            this.cmbField5.Name = "cmbField5";
            this.cmbField5.Size = new System.Drawing.Size(153, 21);
            this.cmbField5.TabIndex = 30;
            this.cmbField5.Tag = "4";
            this.cmbField5.TextChanged += new System.EventHandler(this.cmbField_TextChanged);
            // 
            // cmbField4
            // 
            this.cmbField4.FormattingEnabled = true;
            this.cmbField4.Items.AddRange(new object[] {
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
            this.cmbField4.Location = new System.Drawing.Point(6, 104);
            this.cmbField4.Name = "cmbField4";
            this.cmbField4.Size = new System.Drawing.Size(153, 21);
            this.cmbField4.TabIndex = 29;
            this.cmbField4.Tag = "3";
            this.cmbField4.TextChanged += new System.EventHandler(this.cmbField_TextChanged);
            // 
            // cmbField3
            // 
            this.cmbField3.FormattingEnabled = true;
            this.cmbField3.Items.AddRange(new object[] {
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
            this.cmbField3.Location = new System.Drawing.Point(6, 79);
            this.cmbField3.Name = "cmbField3";
            this.cmbField3.Size = new System.Drawing.Size(153, 21);
            this.cmbField3.TabIndex = 28;
            this.cmbField3.Tag = "2";
            this.cmbField3.TextChanged += new System.EventHandler(this.cmbField_TextChanged);
            // 
            // cmbField2
            // 
            this.cmbField2.FormattingEnabled = true;
            this.cmbField2.Items.AddRange(new object[] {
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
            this.cmbField2.Location = new System.Drawing.Point(6, 54);
            this.cmbField2.Name = "cmbField2";
            this.cmbField2.Size = new System.Drawing.Size(153, 21);
            this.cmbField2.TabIndex = 27;
            this.cmbField2.Tag = "1";
            this.cmbField2.TextChanged += new System.EventHandler(this.cmbField_TextChanged);
            // 
            // cmbField1
            // 
            this.cmbField1.FormattingEnabled = true;
            this.cmbField1.Items.AddRange(new object[] {
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
            this.cmbField1.Location = new System.Drawing.Point(6, 31);
            this.cmbField1.Name = "cmbField1";
            this.cmbField1.Size = new System.Drawing.Size(153, 21);
            this.cmbField1.TabIndex = 26;
            this.cmbField1.Tag = "0";
            this.cmbField1.TextChanged += new System.EventHandler(this.cmbField_TextChanged);
            // 
            // rtxtInfo_Video
            // 
            this.rtxtInfo_Video.ContextMenuStrip = this.mnuContext;
            this.rtxtInfo_Video.EnableAutoDragDrop = true;
            this.rtxtInfo_Video.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtxtInfo_Video.Location = new System.Drawing.Point(165, 29);
            this.rtxtInfo_Video.Name = "rtxtInfo_Video";
            this.rtxtInfo_Video.Size = new System.Drawing.Size(650, 294);
            this.rtxtInfo_Video.TabIndex = 23;
            this.rtxtInfo_Video.Text = "";
            this.rtxtInfo_Video.WordWrap = false;
            this.rtxtInfo_Video.TextChanged += new System.EventHandler(this.rtxtInfo_Video_TextChanged);
            this.rtxtInfo_Video.Enter += new System.EventHandler(this.rtxtInfo_Video_Enter);
            this.rtxtInfo_Video.Leave += new System.EventHandler(this.rtxtInfo_Video_Leave);
            this.rtxtInfo_Video.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.rtxtInfo_Video_PreviewKeyDown);
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuContext_Copy,
            this.mnuContext_Past,
            this.mnuContext_Select,
            this.mnuContext_Sep1,
            this.mnuContext_Cut,
            this.mnuContext_Del,
            this.mnuContext_Sep2,
            this.mnuContext_Translate,
            this.mnuContext_View});
            this.mnuContext.Name = "mnuContext";
            this.mnuContext.Size = new System.Drawing.Size(234, 170);
            // 
            // mnuContext_Copy
            // 
            this.mnuContext_Copy.Name = "mnuContext_Copy";
            this.mnuContext_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuContext_Copy.Size = new System.Drawing.Size(233, 22);
            this.mnuContext_Copy.Text = "Копировать";
            this.mnuContext_Copy.Click += new System.EventHandler(this.mnuContext_Copy_Click);
            // 
            // mnuContext_Past
            // 
            this.mnuContext_Past.Name = "mnuContext_Past";
            this.mnuContext_Past.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuContext_Past.Size = new System.Drawing.Size(233, 22);
            this.mnuContext_Past.Text = "Вставить";
            this.mnuContext_Past.Click += new System.EventHandler(this.mnuContext_Past_Click);
            // 
            // mnuContext_Select
            // 
            this.mnuContext_Select.Name = "mnuContext_Select";
            this.mnuContext_Select.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuContext_Select.Size = new System.Drawing.Size(233, 22);
            this.mnuContext_Select.Text = "Выделить всё";
            this.mnuContext_Select.Click += new System.EventHandler(this.mnuContext_Select_Click);
            // 
            // mnuContext_Sep1
            // 
            this.mnuContext_Sep1.Name = "mnuContext_Sep1";
            this.mnuContext_Sep1.Size = new System.Drawing.Size(230, 6);
            // 
            // mnuContext_Cut
            // 
            this.mnuContext_Cut.Name = "mnuContext_Cut";
            this.mnuContext_Cut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuContext_Cut.Size = new System.Drawing.Size(233, 22);
            this.mnuContext_Cut.Text = "Вырезать";
            this.mnuContext_Cut.Click += new System.EventHandler(this.mnuContext_Cut_Click);
            // 
            // mnuContext_Del
            // 
            this.mnuContext_Del.Name = "mnuContext_Del";
            this.mnuContext_Del.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.mnuContext_Del.Size = new System.Drawing.Size(233, 22);
            this.mnuContext_Del.Text = "Очистить";
            this.mnuContext_Del.Click += new System.EventHandler(this.mnuContext_Del_Click);
            // 
            // mnuContext_Sep2
            // 
            this.mnuContext_Sep2.Name = "mnuContext_Sep2";
            this.mnuContext_Sep2.Size = new System.Drawing.Size(230, 6);
            // 
            // mnuContext_Translate
            // 
            this.mnuContext_Translate.Name = "mnuContext_Translate";
            this.mnuContext_Translate.Size = new System.Drawing.Size(233, 22);
            this.mnuContext_Translate.Text = "Перевести в Google Translate";
            this.mnuContext_Translate.Visible = false;
            this.mnuContext_Translate.Click += new System.EventHandler(this.mnuContext_Translate_Click);
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label19.Location = new System.Drawing.Point(4, 443);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(811, 23);
            this.label19.TabIndex = 21;
            this.label19.Text = "Ссылки на постер и скриншоты";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label18.Location = new System.Drawing.Point(4, 326);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(811, 23);
            this.label18.TabIndex = 20;
            this.label18.Text = "Информация о файле";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label17.Location = new System.Drawing.Point(4, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(811, 23);
            this.label17.TabIndex = 19;
            this.label17.Text = "Информация о фильме";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(7, 472);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(156, 13);
            this.label15.TabIndex = 17;
            this.label15.Text = "Постер";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtInfo_Images
            // 
            this.txtInfo_Images.AllowDrop = true;
            this.txtInfo_Images.ContextMenuStrip = this.mnuContext;
            this.txtInfo_Images.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtInfo_Images.Location = new System.Drawing.Point(169, 469);
            this.txtInfo_Images.Multiline = true;
            this.txtInfo_Images.Name = "txtInfo_Images";
            this.txtInfo_Images.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInfo_Images.Size = new System.Drawing.Size(646, 88);
            this.txtInfo_Images.TabIndex = 16;
            this.txtInfo_Images.WordWrap = false;
            this.txtInfo_Images.TextChanged += new System.EventHandler(this.txtInfo_Images_TextChanged);
            this.txtInfo_Images.Enter += new System.EventHandler(this.txtInfo_Images_Enter);
            this.txtInfo_Images.Leave += new System.EventHandler(this.txtInfo_Images_Leave);
            // 
            // txtInfo_Tech
            // 
            this.txtInfo_Tech.AllowDrop = true;
            this.txtInfo_Tech.ContextMenuStrip = this.mnuContext;
            this.txtInfo_Tech.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtInfo_Tech.Location = new System.Drawing.Point(169, 352);
            this.txtInfo_Tech.Multiline = true;
            this.txtInfo_Tech.Name = "txtInfo_Tech";
            this.txtInfo_Tech.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInfo_Tech.Size = new System.Drawing.Size(646, 88);
            this.txtInfo_Tech.TabIndex = 15;
            this.txtInfo_Tech.WordWrap = false;
            this.txtInfo_Tech.Enter += new System.EventHandler(this.txtInfo_Tech_Enter);
            this.txtInfo_Tech.Leave += new System.EventHandler(this.txtInfo_Tech_Leave);
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(7, 369);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(156, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Аудио";
            this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(7, 352);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(156, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Видео";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtInfo_Video
            // 
            this.txtInfo_Video.AllowDrop = true;
            this.txtInfo_Video.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtInfo_Video.Location = new System.Drawing.Point(165, 29);
            this.txtInfo_Video.Multiline = true;
            this.txtInfo_Video.Name = "txtInfo_Video";
            this.txtInfo_Video.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInfo_Video.Size = new System.Drawing.Size(652, 201);
            this.txtInfo_Video.TabIndex = 0;
            this.txtInfo_Video.Visible = false;
            this.txtInfo_Video.WordWrap = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(315, 564);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Готово";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCencel
            // 
            this.btnCencel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCencel.Location = new System.Drawing.Point(442, 564);
            this.btnCencel.Name = "btnCencel";
            this.btnCencel.Size = new System.Drawing.Size(75, 23);
            this.btnCencel.TabIndex = 3;
            this.btnCencel.Text = "Отмена";
            this.btnCencel.UseVisualStyleBackColor = true;
            this.btnCencel.Click += new System.EventHandler(this.btnCencel_Click);
            // 
            // mnuContext_View
            // 
            this.mnuContext_View.Name = "mnuContext_View";
            this.mnuContext_View.Size = new System.Drawing.Size(233, 22);
            this.mnuContext_View.Text = "Обзор...";
            this.mnuContext_View.Visible = false;
            this.mnuContext_View.Click += new System.EventHandler(this.mnuContext_View_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "openFileDialog1";
            this.dlgOpen.Filter = "Описание раздачи (*.knr)|*.knr";
            // 
            // frmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCencel;
            this.ClientSize = new System.Drawing.Size(829, 599);
            this.ControlBox = false;
            this.Controls.Add(this.btnCencel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(845, 637);
            this.MinimumSize = new System.Drawing.Size(845, 637);
            this.Name = "frmImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Импорт текста";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmImport_FormClosed);
            this.Load += new System.EventHandler(this.frmImport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtInfo_Video;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCencel;
        private System.Windows.Forms.CheckBox chkScreenshots;
        private System.Windows.Forms.ContextMenuStrip mnuContext;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_Copy;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_Past;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_Select;
        private System.Windows.Forms.ToolStripSeparator mnuContext_Sep1;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_Cut;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_Del;
        public System.Windows.Forms.TextBox txtInfo_Images;
        public System.Windows.Forms.TextBox txtInfo_Tech;
        public System.Windows.Forms.RichTextBox rtxtInfo_Video;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbField11;
        private System.Windows.Forms.ComboBox cmbField10;
        private System.Windows.Forms.ComboBox cmbField9;
        private System.Windows.Forms.ComboBox cmbField8;
        private System.Windows.Forms.ComboBox cmbField7;
        private System.Windows.Forms.ComboBox cmbField6;
        private System.Windows.Forms.ComboBox cmbField5;
        private System.Windows.Forms.ComboBox cmbField4;
        private System.Windows.Forms.ComboBox cmbField3;
        private System.Windows.Forms.ComboBox cmbField2;
        private System.Windows.Forms.ComboBox cmbField1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolStripSeparator mnuContext_Sep2;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_Translate;
        private System.Windows.Forms.CheckBox chkAuto;
        private System.Windows.Forms.ToolStripMenuItem mnuContext_View;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
    }
}