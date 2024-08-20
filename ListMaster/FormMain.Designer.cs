namespace ListMaster
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.button_StartWork = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.textBoxStartRowNumber = new System.Windows.Forms.TextBox();
            this.labelCurrentRow = new System.Windows.Forms.Label();
            this.button_chooseFile = new System.Windows.Forms.Button();
            this.comboBoxOptions = new System.Windows.Forms.ComboBox();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.lbTotalRows = new System.Windows.Forms.Label();
            this.labelFileName = new System.Windows.Forms.Label();
            this.labelSettings = new System.Windows.Forms.Label();
            this.lbRamainsRows = new System.Windows.Forms.Label();
            this.labelTotalRows = new System.Windows.Forms.Label();
            this.labelRamainsRows = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_StartWork
            // 
            this.button_StartWork.Location = new System.Drawing.Point(12, 78);
            this.button_StartWork.Name = "button_StartWork";
            this.button_StartWork.Size = new System.Drawing.Size(146, 23);
            this.button_StartWork.TabIndex = 5;
            this.button_StartWork.Text = "Начать";
            this.button_StartWork.UseVisualStyleBackColor = true;
            this.button_StartWork.Click += new System.EventHandler(this.Button_StartWork_Click);
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(12, 113);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(146, 23);
            this.button_Stop.TabIndex = 6;
            this.button_Stop.Text = "Остановить";
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // textBoxStartRowNumber
            // 
            this.textBoxStartRowNumber.Location = new System.Drawing.Point(227, 79);
            this.textBoxStartRowNumber.Name = "textBoxStartRowNumber";
            this.textBoxStartRowNumber.Size = new System.Drawing.Size(45, 20);
            this.textBoxStartRowNumber.TabIndex = 4;
            this.textBoxStartRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCurrentRow
            // 
            this.labelCurrentRow.AutoSize = true;
            this.labelCurrentRow.Location = new System.Drawing.Point(164, 81);
            this.labelCurrentRow.Name = "labelCurrentRow";
            this.labelCurrentRow.Size = new System.Drawing.Size(57, 13);
            this.labelCurrentRow.TabIndex = 2;
            this.labelCurrentRow.Text = "со строки";
            // 
            // button_chooseFile
            // 
            this.button_chooseFile.Location = new System.Drawing.Point(12, 47);
            this.button_chooseFile.Name = "button_chooseFile";
            this.button_chooseFile.Size = new System.Drawing.Size(90, 23);
            this.button_chooseFile.TabIndex = 3;
            this.button_chooseFile.Text = "Выбрать файл";
            this.button_chooseFile.UseVisualStyleBackColor = true;
            this.button_chooseFile.Click += new System.EventHandler(this.Button_ChooseFile_Click);
            // 
            // comboBoxOptions
            // 
            this.comboBoxOptions.FormattingEnabled = true;
            this.comboBoxOptions.Location = new System.Drawing.Point(12, 10);
            this.comboBoxOptions.Name = "comboBoxOptions";
            this.comboBoxOptions.Size = new System.Drawing.Size(260, 21);
            this.comboBoxOptions.Sorted = true;
            this.comboBoxOptions.TabIndex = 0;
            this.comboBoxOptions.SelectedIndexChanged += new System.EventHandler(this.ComboBoxOptions_SelectedIndexChanged);
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Location = new System.Drawing.Point(12, 152);
            this.textBoxStatus.MaxLength = 1000;
            this.textBoxStatus.Multiline = true;
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxStatus.Size = new System.Drawing.Size(260, 58);
            this.textBoxStatus.TabIndex = 7;
            // 
            // lbTotalRows
            // 
            this.lbTotalRows.Location = new System.Drawing.Point(227, 102);
            this.lbTotalRows.Name = "lbTotalRows";
            this.lbTotalRows.Size = new System.Drawing.Size(45, 13);
            this.lbTotalRows.TabIndex = 9;
            this.lbTotalRows.Text = "rowsCount";
            this.lbTotalRows.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFileName
            // 
            this.labelFileName.AutoSize = true;
            this.labelFileName.Location = new System.Drawing.Point(104, 52);
            this.labelFileName.Name = "labelFileName";
            this.labelFileName.Size = new System.Drawing.Size(171, 13);
            this.labelFileName.TabIndex = 10;
            this.labelFileName.Text = "labelFileNamel1111111111111111";
            // 
            // labelSettings
            // 
            this.labelSettings.AutoSize = true;
            this.labelSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSettings.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelSettings.Location = new System.Drawing.Point(210, 34);
            this.labelSettings.Name = "labelSettings";
            this.labelSettings.Size = new System.Drawing.Size(62, 13);
            this.labelSettings.TabIndex = 2;
            this.labelSettings.Text = "Настройки";
            this.labelSettings.Click += new System.EventHandler(this.labelSettings_Click);
            // 
            // lbRamainsRows
            // 
            this.lbRamainsRows.Location = new System.Drawing.Point(227, 123);
            this.lbRamainsRows.Name = "lbRamainsRows";
            this.lbRamainsRows.Size = new System.Drawing.Size(45, 13);
            this.lbRamainsRows.TabIndex = 12;
            this.lbRamainsRows.Text = "ramains";
            this.lbRamainsRows.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTotalRows
            // 
            this.labelTotalRows.AutoSize = true;
            this.labelTotalRows.Location = new System.Drawing.Point(164, 102);
            this.labelTotalRows.Name = "labelTotalRows";
            this.labelTotalRows.Size = new System.Drawing.Size(51, 13);
            this.labelTotalRows.TabIndex = 13;
            this.labelTotalRows.Text = "в работе";
            // 
            // labelRamainsRows
            // 
            this.labelRamainsRows.AutoSize = true;
            this.labelRamainsRows.Location = new System.Drawing.Point(164, 123);
            this.labelRamainsRows.Name = "labelRamainsRows";
            this.labelRamainsRows.Size = new System.Drawing.Size(54, 13);
            this.labelRamainsRows.TabIndex = 13;
            this.labelRamainsRows.Text = "осталось";
            // 
            // FormMain
            // 
            this.AccessibleName = "";
            this.ClientSize = new System.Drawing.Size(284, 221);
            this.Controls.Add(this.labelRamainsRows);
            this.Controls.Add(this.labelTotalRows);
            this.Controls.Add(this.lbRamainsRows);
            this.Controls.Add(this.labelSettings);
            this.Controls.Add(this.labelFileName);
            this.Controls.Add(this.lbTotalRows);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.comboBoxOptions);
            this.Controls.Add(this.button_chooseFile);
            this.Controls.Add(this.labelCurrentRow);
            this.Controls.Add(this.textBoxStartRowNumber);
            this.Controls.Add(this.button_Stop);
            this.Controls.Add(this.button_StartWork);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 260);
            this.MinimumSize = new System.Drawing.Size(300, 260);
            this.Name = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_StartWork;
        private System.Windows.Forms.Button button_Stop;
        private System.Windows.Forms.TextBox textBoxStartRowNumber;
        private System.Windows.Forms.Label labelCurrentRow;
        private System.Windows.Forms.Button button_chooseFile;
        private System.Windows.Forms.ComboBox comboBoxOptions;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.Label lbTotalRows;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.Label labelSettings;
        private System.Windows.Forms.Label lbRamainsRows;
        private System.Windows.Forms.Label labelTotalRows;
        private System.Windows.Forms.Label labelRamainsRows;
    }
}

