namespace ListMaster
{
    partial class FormSettings
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
            this.groupBoxResource = new System.Windows.Forms.GroupBox();
            this.cbBrowserType = new System.Windows.Forms.ComboBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbURL = new System.Windows.Forms.TextBox();
            this.tbScriptName = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.lbBrowserType = new System.Windows.Forms.Label();
            this.lbUsername = new System.Windows.Forms.Label();
            this.lbURL = new System.Windows.Forms.Label();
            this.lbScriptName = new System.Windows.Forms.Label();
            this.groupBoxColumnNumbers = new System.Windows.Forms.GroupBox();
            this.lbTargetColumn = new System.Windows.Forms.Label();
            this.tbTargetColumn = new System.Windows.Forms.TextBox();
            this.lbSeriesNumber = new System.Windows.Forms.Label();
            this.tbDocumentSeriesNumber = new System.Windows.Forms.TextBox();
            this.lbNumber = new System.Windows.Forms.Label();
            this.tbDocumentNumber = new System.Windows.Forms.TextBox();
            this.lbSeries = new System.Windows.Forms.Label();
            this.tbDocumentSeries = new System.Windows.Forms.TextBox();
            this.lbBdate = new System.Windows.Forms.Label();
            this.tbBdate = new System.Windows.Forms.TextBox();
            this.lbOthername = new System.Windows.Forms.Label();
            this.tbOthername = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.tbFirstname = new System.Windows.Forms.TextBox();
            this.lbLastname = new System.Windows.Forms.Label();
            this.tbLastname = new System.Windows.Forms.TextBox();
            this.lbFullname = new System.Windows.Forms.Label();
            this.tbFullname = new System.Windows.Forms.TextBox();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.groupBoxResource.SuspendLayout();
            this.groupBoxColumnNumbers.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxResource
            // 
            this.groupBoxResource.Controls.Add(this.cbBrowserType);
            this.groupBoxResource.Controls.Add(this.tbPassword);
            this.groupBoxResource.Controls.Add(this.tbUsername);
            this.groupBoxResource.Controls.Add(this.tbURL);
            this.groupBoxResource.Controls.Add(this.tbScriptName);
            this.groupBoxResource.Controls.Add(this.lbPassword);
            this.groupBoxResource.Controls.Add(this.lbBrowserType);
            this.groupBoxResource.Controls.Add(this.lbUsername);
            this.groupBoxResource.Controls.Add(this.lbURL);
            this.groupBoxResource.Controls.Add(this.lbScriptName);
            this.groupBoxResource.Location = new System.Drawing.Point(12, 2);
            this.groupBoxResource.Name = "groupBoxResource";
            this.groupBoxResource.Size = new System.Drawing.Size(312, 118);
            this.groupBoxResource.TabIndex = 0;
            this.groupBoxResource.TabStop = false;
            this.groupBoxResource.Text = "Ресурс";
            // 
            // cbBrowserType
            // 
            this.cbBrowserType.FormattingEnabled = true;
            this.cbBrowserType.Location = new System.Drawing.Point(120, 32);
            this.cbBrowserType.Name = "cbBrowserType";
            this.cbBrowserType.Size = new System.Drawing.Size(178, 21);
            this.cbBrowserType.TabIndex = 2;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(120, 90);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(178, 20);
            this.tbPassword.TabIndex = 5;
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(120, 71);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(178, 20);
            this.tbUsername.TabIndex = 4;
            // 
            // tbURL
            // 
            this.tbURL.Location = new System.Drawing.Point(120, 52);
            this.tbURL.Name = "tbURL";
            this.tbURL.Size = new System.Drawing.Size(178, 20);
            this.tbURL.TabIndex = 3;
            // 
            // tbScriptName
            // 
            this.tbScriptName.Location = new System.Drawing.Point(120, 13);
            this.tbScriptName.Name = "tbScriptName";
            this.tbScriptName.Size = new System.Drawing.Size(178, 20);
            this.tbScriptName.TabIndex = 1;
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(0, 93);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(45, 13);
            this.lbPassword.TabIndex = 0;
            this.lbPassword.Text = "Пароль";
            // 
            // lbBrowserType
            // 
            this.lbBrowserType.AutoSize = true;
            this.lbBrowserType.Location = new System.Drawing.Point(0, 35);
            this.lbBrowserType.Name = "lbBrowserType";
            this.lbBrowserType.Size = new System.Drawing.Size(76, 13);
            this.lbBrowserType.TabIndex = 0;
            this.lbBrowserType.Text = "Тип браузера";
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(0, 74);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(80, 13);
            this.lbUsername.TabIndex = 0;
            this.lbUsername.Text = "Пользователь";
            // 
            // lbURL
            // 
            this.lbURL.AutoSize = true;
            this.lbURL.Location = new System.Drawing.Point(0, 55);
            this.lbURL.Name = "lbURL";
            this.lbURL.Size = new System.Drawing.Size(29, 13);
            this.lbURL.TabIndex = 0;
            this.lbURL.Text = "URL";
            // 
            // lbScriptName
            // 
            this.lbScriptName.AutoSize = true;
            this.lbScriptName.Location = new System.Drawing.Point(0, 16);
            this.lbScriptName.Name = "lbScriptName";
            this.lbScriptName.Size = new System.Drawing.Size(108, 13);
            this.lbScriptName.TabIndex = 0;
            this.lbScriptName.Text = "Название сценария";
            // 
            // groupBoxColumnNumbers
            // 
            this.groupBoxColumnNumbers.Controls.Add(this.lbTargetColumn);
            this.groupBoxColumnNumbers.Controls.Add(this.tbTargetColumn);
            this.groupBoxColumnNumbers.Controls.Add(this.lbSeriesNumber);
            this.groupBoxColumnNumbers.Controls.Add(this.tbDocumentSeriesNumber);
            this.groupBoxColumnNumbers.Controls.Add(this.lbNumber);
            this.groupBoxColumnNumbers.Controls.Add(this.tbDocumentNumber);
            this.groupBoxColumnNumbers.Controls.Add(this.lbSeries);
            this.groupBoxColumnNumbers.Controls.Add(this.tbDocumentSeries);
            this.groupBoxColumnNumbers.Controls.Add(this.lbBdate);
            this.groupBoxColumnNumbers.Controls.Add(this.tbBdate);
            this.groupBoxColumnNumbers.Controls.Add(this.lbOthername);
            this.groupBoxColumnNumbers.Controls.Add(this.tbOthername);
            this.groupBoxColumnNumbers.Controls.Add(this.lbName);
            this.groupBoxColumnNumbers.Controls.Add(this.tbFirstname);
            this.groupBoxColumnNumbers.Controls.Add(this.lbLastname);
            this.groupBoxColumnNumbers.Controls.Add(this.tbLastname);
            this.groupBoxColumnNumbers.Controls.Add(this.lbFullname);
            this.groupBoxColumnNumbers.Controls.Add(this.tbFullname);
            this.groupBoxColumnNumbers.Location = new System.Drawing.Point(12, 125);
            this.groupBoxColumnNumbers.Name = "groupBoxColumnNumbers";
            this.groupBoxColumnNumbers.Size = new System.Drawing.Size(312, 194);
            this.groupBoxColumnNumbers.TabIndex = 1;
            this.groupBoxColumnNumbers.TabStop = false;
            this.groupBoxColumnNumbers.Text = "Номера колонок";
            // 
            // lbTargetColumn
            // 
            this.lbTargetColumn.AutoSize = true;
            this.lbTargetColumn.Location = new System.Drawing.Point(0, 170);
            this.lbTargetColumn.Name = "lbTargetColumn";
            this.lbTargetColumn.Size = new System.Drawing.Size(96, 13);
            this.lbTargetColumn.TabIndex = 23;
            this.lbTargetColumn.Text = "Целевая колонка";
            // 
            // tbTargetColumn
            // 
            this.tbTargetColumn.Location = new System.Drawing.Point(120, 167);
            this.tbTargetColumn.Name = "tbTargetColumn";
            this.tbTargetColumn.Size = new System.Drawing.Size(178, 20);
            this.tbTargetColumn.TabIndex = 22;
            // 
            // lbSeriesNumber
            // 
            this.lbSeriesNumber.AutoSize = true;
            this.lbSeriesNumber.Location = new System.Drawing.Point(0, 151);
            this.lbSeriesNumber.Name = "lbSeriesNumber";
            this.lbSeriesNumber.Size = new System.Drawing.Size(96, 13);
            this.lbSeriesNumber.TabIndex = 19;
            this.lbSeriesNumber.Text = "Серия с номером";
            // 
            // tbDocumentSeriesNumber
            // 
            this.tbDocumentSeriesNumber.Location = new System.Drawing.Point(120, 148);
            this.tbDocumentSeriesNumber.Name = "tbDocumentSeriesNumber";
            this.tbDocumentSeriesNumber.Size = new System.Drawing.Size(178, 20);
            this.tbDocumentSeriesNumber.TabIndex = 14;
            this.tbDocumentSeriesNumber.TextChanged += new System.EventHandler(this.tbDocumentSeriesNumber_TextChanged);
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.Location = new System.Drawing.Point(0, 132);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(41, 13);
            this.lbNumber.TabIndex = 17;
            this.lbNumber.Text = "Номер";
            // 
            // tbDocumentNumber
            // 
            this.tbDocumentNumber.Location = new System.Drawing.Point(120, 129);
            this.tbDocumentNumber.Name = "tbDocumentNumber";
            this.tbDocumentNumber.Size = new System.Drawing.Size(178, 20);
            this.tbDocumentNumber.TabIndex = 13;
            this.tbDocumentNumber.TextChanged += new System.EventHandler(this.tbDocumentNumber_TextChanged);
            // 
            // lbSeries
            // 
            this.lbSeries.AutoSize = true;
            this.lbSeries.Location = new System.Drawing.Point(0, 113);
            this.lbSeries.Name = "lbSeries";
            this.lbSeries.Size = new System.Drawing.Size(38, 13);
            this.lbSeries.TabIndex = 15;
            this.lbSeries.Text = "Серия";
            // 
            // tbDocumentSeries
            // 
            this.tbDocumentSeries.Location = new System.Drawing.Point(120, 110);
            this.tbDocumentSeries.Name = "tbDocumentSeries";
            this.tbDocumentSeries.Size = new System.Drawing.Size(178, 20);
            this.tbDocumentSeries.TabIndex = 12;
            this.tbDocumentSeries.TextChanged += new System.EventHandler(this.tbDocumentSeries_TextChanged);
            // 
            // lbBdate
            // 
            this.lbBdate.AutoSize = true;
            this.lbBdate.Location = new System.Drawing.Point(0, 94);
            this.lbBdate.Name = "lbBdate";
            this.lbBdate.Size = new System.Drawing.Size(23, 13);
            this.lbBdate.TabIndex = 13;
            this.lbBdate.Text = "ДР";
            // 
            // tbBdate
            // 
            this.tbBdate.Location = new System.Drawing.Point(120, 91);
            this.tbBdate.Name = "tbBdate";
            this.tbBdate.Size = new System.Drawing.Size(178, 20);
            this.tbBdate.TabIndex = 11;
            // 
            // lbOthername
            // 
            this.lbOthername.AutoSize = true;
            this.lbOthername.Location = new System.Drawing.Point(0, 75);
            this.lbOthername.Name = "lbOthername";
            this.lbOthername.Size = new System.Drawing.Size(54, 13);
            this.lbOthername.TabIndex = 11;
            this.lbOthername.Text = "Отчество";
            // 
            // tbOthername
            // 
            this.tbOthername.Location = new System.Drawing.Point(120, 72);
            this.tbOthername.Name = "tbOthername";
            this.tbOthername.Size = new System.Drawing.Size(178, 20);
            this.tbOthername.TabIndex = 10;
            this.tbOthername.TextChanged += new System.EventHandler(this.tbOthername_TextChanged);
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(0, 57);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(29, 13);
            this.lbName.TabIndex = 9;
            this.lbName.Text = "Имя";
            // 
            // tbFirstname
            // 
            this.tbFirstname.Location = new System.Drawing.Point(120, 54);
            this.tbFirstname.Name = "tbFirstname";
            this.tbFirstname.Size = new System.Drawing.Size(178, 20);
            this.tbFirstname.TabIndex = 9;
            this.tbFirstname.TextChanged += new System.EventHandler(this.tbFirstname_TextChanged);
            // 
            // lbLastname
            // 
            this.lbLastname.AutoSize = true;
            this.lbLastname.Location = new System.Drawing.Point(0, 38);
            this.lbLastname.Name = "lbLastname";
            this.lbLastname.Size = new System.Drawing.Size(56, 13);
            this.lbLastname.TabIndex = 7;
            this.lbLastname.Text = "Фамилия";
            // 
            // tbLastname
            // 
            this.tbLastname.Location = new System.Drawing.Point(120, 35);
            this.tbLastname.Name = "tbLastname";
            this.tbLastname.Size = new System.Drawing.Size(178, 20);
            this.tbLastname.TabIndex = 8;
            this.tbLastname.TextChanged += new System.EventHandler(this.tbLastname_TextChanged);
            // 
            // lbFullname
            // 
            this.lbFullname.AutoSize = true;
            this.lbFullname.Location = new System.Drawing.Point(0, 19);
            this.lbFullname.Name = "lbFullname";
            this.lbFullname.Size = new System.Drawing.Size(68, 13);
            this.lbFullname.TabIndex = 7;
            this.lbFullname.Text = "Полное имя";
            // 
            // tbFullname
            // 
            this.tbFullname.Location = new System.Drawing.Point(120, 16);
            this.tbFullname.Name = "tbFullname";
            this.tbFullname.Size = new System.Drawing.Size(178, 20);
            this.tbFullname.TabIndex = 7;
            this.tbFullname.TextChanged += new System.EventHandler(this.tbFullname_TextChanged);
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.Location = new System.Drawing.Point(13, 324);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(99, 23);
            this.buttonSaveAs.TabIndex = 17;
            this.buttonSaveAs.Text = "Сохранить как";
            this.buttonSaveAs.UseVisualStyleBackColor = true;
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            // 
            // tbFileName
            // 
            this.tbFileName.Location = new System.Drawing.Point(119, 324);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(205, 20);
            this.tbFileName.TabIndex = 16;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 356);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.buttonSaveAs);
            this.Controls.Add(this.groupBoxColumnNumbers);
            this.Controls.Add(this.groupBoxResource);
            this.Name = "FormSettings";
            this.Text = "Настройки сценария";
            this.groupBoxResource.ResumeLayout(false);
            this.groupBoxResource.PerformLayout();
            this.groupBoxColumnNumbers.ResumeLayout(false);
            this.groupBoxColumnNumbers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxResource;
        private System.Windows.Forms.GroupBox groupBoxColumnNumbers;
        private System.Windows.Forms.Button buttonSaveAs;
        private System.Windows.Forms.ComboBox cbBrowserType;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbURL;
        private System.Windows.Forms.TextBox tbScriptName;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.Label lbBrowserType;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.Label lbURL;
        private System.Windows.Forms.Label lbScriptName;
        private System.Windows.Forms.Label lbFullname;
        private System.Windows.Forms.TextBox tbFullname;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label lbSeriesNumber;
        private System.Windows.Forms.TextBox tbDocumentSeriesNumber;
        private System.Windows.Forms.Label lbNumber;
        private System.Windows.Forms.TextBox tbDocumentNumber;
        private System.Windows.Forms.Label lbSeries;
        private System.Windows.Forms.TextBox tbDocumentSeries;
        private System.Windows.Forms.Label lbBdate;
        private System.Windows.Forms.TextBox tbBdate;
        private System.Windows.Forms.Label lbOthername;
        private System.Windows.Forms.TextBox tbOthername;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbFirstname;
        private System.Windows.Forms.Label lbLastname;
        private System.Windows.Forms.TextBox tbLastname;
        private System.Windows.Forms.Label lbTargetColumn;
        private System.Windows.Forms.TextBox tbTargetColumn;
    }
}