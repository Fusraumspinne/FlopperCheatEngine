namespace FlopperCheat
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxProcessesList = new System.Windows.Forms.ComboBox();
            labelProcess = new System.Windows.Forms.Label();
            textBoxScanValue = new System.Windows.Forms.TextBox();
            btnAddScanValue = new System.Windows.Forms.Button();
            btnSubScanValue = new System.Windows.Forms.Button();
            btnNewScan = new System.Windows.Forms.Button();
            btnNextScan = new System.Windows.Forms.Button();
            labelFoundAdresses = new System.Windows.Forms.Label();
            labelValue = new System.Windows.Forms.Label();
            listAdresses = new System.Windows.Forms.ListView();
            labelPages = new System.Windows.Forms.Label();
            textBoxWriteValue = new System.Windows.Forms.TextBox();
            btnSubWriteValue = new System.Windows.Forms.Button();
            btnAddWriteValue = new System.Windows.Forms.Button();
            labelWriteValue = new System.Windows.Forms.Label();
            btnWrite = new System.Windows.Forms.Button();
            btnNextPage = new System.Windows.Forms.Button();
            btnPrevPage = new System.Windows.Forms.Button();
            listSavedAdresses = new System.Windows.Forms.ListView();
            labelSavedAddresses = new System.Windows.Forms.Label();
            btnNextPageSavedAddresses = new System.Windows.Forms.Button();
            btnPrevPageSavedAddresses = new System.Windows.Forms.Button();
            labelPagesSavedAddresses = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // comboBoxProcessesList
            // 
            comboBoxProcessesList.FormattingEnabled = true;
            comboBoxProcessesList.Location = new System.Drawing.Point(14, 14);
            comboBoxProcessesList.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            comboBoxProcessesList.Name = "comboBoxProcessesList";
            comboBoxProcessesList.Size = new System.Drawing.Size(354, 23);
            comboBoxProcessesList.TabIndex = 1;
            comboBoxProcessesList.SelectedIndexChanged += comboBoxProcessesList_SelectedIndexChanged;
            // 
            // labelProcess
            // 
            labelProcess.AutoSize = true;
            labelProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelProcess.Location = new System.Drawing.Point(376, 20);
            labelProcess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelProcess.Name = "labelProcess";
            labelProcess.Size = new System.Drawing.Size(127, 13);
            labelProcess.TabIndex = 2;
            labelProcess.Text = "Wähle einen Prozess aus";
            // 
            // textBoxScanValue
            // 
            textBoxScanValue.Location = new System.Drawing.Point(14, 46);
            textBoxScanValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxScanValue.Name = "textBoxScanValue";
            textBoxScanValue.Size = new System.Drawing.Size(292, 23);
            textBoxScanValue.TabIndex = 3;
            textBoxScanValue.Text = "0";
            // 
            // btnAddScanValue
            // 
            btnAddScanValue.Location = new System.Drawing.Point(342, 45);
            btnAddScanValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnAddScanValue.Name = "btnAddScanValue";
            btnAddScanValue.Size = new System.Drawing.Size(27, 27);
            btnAddScanValue.TabIndex = 4;
            btnAddScanValue.Text = "+";
            btnAddScanValue.UseVisualStyleBackColor = true;
            btnAddScanValue.Click += btnAddScanValue_Click;
            // 
            // btnSubScanValue
            // 
            btnSubScanValue.Location = new System.Drawing.Point(312, 45);
            btnSubScanValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSubScanValue.Name = "btnSubScanValue";
            btnSubScanValue.Size = new System.Drawing.Size(27, 27);
            btnSubScanValue.TabIndex = 5;
            btnSubScanValue.Text = "-";
            btnSubScanValue.UseVisualStyleBackColor = true;
            btnSubScanValue.Click += btnSubScanValue_Click;
            // 
            // btnNewScan
            // 
            btnNewScan.Location = new System.Drawing.Point(14, 76);
            btnNewScan.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnNewScan.Name = "btnNewScan";
            btnNewScan.Size = new System.Drawing.Size(117, 27);
            btnNewScan.TabIndex = 6;
            btnNewScan.Text = "Neuer Scan";
            btnNewScan.UseVisualStyleBackColor = true;
            btnNewScan.Click += NewScanMemory;
            // 
            // btnNextScan
            // 
            btnNextScan.Location = new System.Drawing.Point(138, 76);
            btnNextScan.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnNextScan.Name = "btnNextScan";
            btnNextScan.Size = new System.Drawing.Size(117, 27);
            btnNextScan.TabIndex = 7;
            btnNextScan.Text = "Nächster Scan";
            btnNextScan.UseVisualStyleBackColor = true;
            btnNextScan.Click += NextScanMemory;
            // 
            // labelFoundAdresses
            // 
            labelFoundAdresses.AutoSize = true;
            labelFoundAdresses.Location = new System.Drawing.Point(10, 106);
            labelFoundAdresses.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelFoundAdresses.Name = "labelFoundAdresses";
            labelFoundAdresses.Size = new System.Drawing.Size(135, 15);
            labelFoundAdresses.TabIndex = 8;
            labelFoundAdresses.Text = "Gefundende Adressen: 0";
            // 
            // labelValue
            // 
            labelValue.AutoSize = true;
            labelValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelValue.Location = new System.Drawing.Point(376, 53);
            labelValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelValue.Name = "labelValue";
            labelValue.Size = new System.Drawing.Size(171, 13);
            labelValue.TabIndex = 9;
            labelValue.Text = "Gebe einen Wert zum scannen ein";
            // 
            // listAdresses
            // 
            listAdresses.FullRowSelect = true;
            listAdresses.GridLines = true;
            listAdresses.Location = new System.Drawing.Point(14, 125);
            listAdresses.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listAdresses.Name = "listAdresses";
            listAdresses.Size = new System.Drawing.Size(354, 217);
            listAdresses.TabIndex = 10;
            listAdresses.UseCompatibleStateImageBehavior = false;
            listAdresses.SelectedIndexChanged += listAdresses_SelectedIndexChanged;
            listAdresses.MouseDoubleClick += btnSaveAddress_Click;
            // 
            // labelPages
            // 
            labelPages.AutoSize = true;
            labelPages.Location = new System.Drawing.Point(103, 356);
            labelPages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelPages.Name = "labelPages";
            labelPages.Size = new System.Drawing.Size(73, 15);
            labelPages.TabIndex = 11;
            labelPages.Text = "Seite 1 von 0";
            // 
            // textBoxWriteValue
            // 
            textBoxWriteValue.Location = new System.Drawing.Point(14, 384);
            textBoxWriteValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBoxWriteValue.Name = "textBoxWriteValue";
            textBoxWriteValue.Size = new System.Drawing.Size(292, 23);
            textBoxWriteValue.TabIndex = 12;
            textBoxWriteValue.Text = "0";
            // 
            // btnSubWriteValue
            // 
            btnSubWriteValue.Location = new System.Drawing.Point(314, 384);
            btnSubWriteValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSubWriteValue.Name = "btnSubWriteValue";
            btnSubWriteValue.Size = new System.Drawing.Size(27, 27);
            btnSubWriteValue.TabIndex = 13;
            btnSubWriteValue.Text = "-";
            btnSubWriteValue.UseVisualStyleBackColor = true;
            btnSubWriteValue.Click += btnSubWriteValue_Click;
            // 
            // btnAddWriteValue
            // 
            btnAddWriteValue.Location = new System.Drawing.Point(342, 384);
            btnAddWriteValue.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnAddWriteValue.Name = "btnAddWriteValue";
            btnAddWriteValue.Size = new System.Drawing.Size(27, 27);
            btnAddWriteValue.TabIndex = 14;
            btnAddWriteValue.Text = "+";
            btnAddWriteValue.UseVisualStyleBackColor = true;
            btnAddWriteValue.Click += btnAddWriteValue_Click;
            // 
            // labelWriteValue
            // 
            labelWriteValue.AutoSize = true;
            labelWriteValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelWriteValue.Location = new System.Drawing.Point(376, 391);
            labelWriteValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelWriteValue.Name = "labelWriteValue";
            labelWriteValue.Size = new System.Drawing.Size(138, 13);
            labelWriteValue.TabIndex = 15;
            labelWriteValue.Text = "Gebe einen neuen Wert ein";
            // 
            // btnWrite
            // 
            btnWrite.Location = new System.Drawing.Point(14, 413);
            btnWrite.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnWrite.Name = "btnWrite";
            btnWrite.Size = new System.Drawing.Size(117, 27);
            btnWrite.TabIndex = 16;
            btnWrite.Text = "Überschreiben";
            btnWrite.UseVisualStyleBackColor = true;
            btnWrite.Click += btnWriteValue_Click;
            // 
            // btnNextPage
            // 
            btnNextPage.Location = new System.Drawing.Point(55, 350);
            btnNextPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnNextPage.Name = "btnNextPage";
            btnNextPage.Size = new System.Drawing.Size(40, 27);
            btnNextPage.TabIndex = 18;
            btnNextPage.Text = "->";
            btnNextPage.UseVisualStyleBackColor = true;
            btnNextPage.Click += GetToNextPage;
            // 
            // btnPrevPage
            // 
            btnPrevPage.Location = new System.Drawing.Point(14, 350);
            btnPrevPage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnPrevPage.Name = "btnPrevPage";
            btnPrevPage.Size = new System.Drawing.Size(40, 27);
            btnPrevPage.TabIndex = 17;
            btnPrevPage.Text = "<-";
            btnPrevPage.UseVisualStyleBackColor = true;
            btnPrevPage.Click += GetToPrevPage;
            // 
            // listSavedAdresses
            // 
            listSavedAdresses.FullRowSelect = true;
            listSavedAdresses.GridLines = true;
            listSavedAdresses.Location = new System.Drawing.Point(15, 471);
            listSavedAdresses.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listSavedAdresses.Name = "listSavedAdresses";
            listSavedAdresses.Size = new System.Drawing.Size(354, 122);
            listSavedAdresses.TabIndex = 19;
            listSavedAdresses.UseCompatibleStateImageBehavior = false;
            listSavedAdresses.SelectedIndexChanged += listSavedAdresses_SelectedIndexChanged;
            listSavedAdresses.DoubleClick += listSavedAdresses_DoubleClick;
            // 
            // labelSavedAddresses
            // 
            labelSavedAddresses.AutoSize = true;
            labelSavedAddresses.Location = new System.Drawing.Point(10, 453);
            labelSavedAddresses.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelSavedAddresses.Name = "labelSavedAddresses";
            labelSavedAddresses.Size = new System.Drawing.Size(138, 15);
            labelSavedAddresses.TabIndex = 20;
            labelSavedAddresses.Text = "Gespeicherte Adressen: 0";
            // 
            // btnNextPageSavedAddresses
            // 
            btnNextPageSavedAddresses.Location = new System.Drawing.Point(55, 597);
            btnNextPageSavedAddresses.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnNextPageSavedAddresses.Name = "btnNextPageSavedAddresses";
            btnNextPageSavedAddresses.Size = new System.Drawing.Size(40, 27);
            btnNextPageSavedAddresses.TabIndex = 23;
            btnNextPageSavedAddresses.Text = "->";
            btnNextPageSavedAddresses.UseVisualStyleBackColor = true;
            btnNextPageSavedAddresses.Click += btnSavedNextPage_Click;
            // 
            // btnPrevPageSavedAddresses
            // 
            btnPrevPageSavedAddresses.Location = new System.Drawing.Point(14, 597);
            btnPrevPageSavedAddresses.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnPrevPageSavedAddresses.Name = "btnPrevPageSavedAddresses";
            btnPrevPageSavedAddresses.Size = new System.Drawing.Size(40, 27);
            btnPrevPageSavedAddresses.TabIndex = 22;
            btnPrevPageSavedAddresses.Text = "<-";
            btnPrevPageSavedAddresses.UseVisualStyleBackColor = true;
            btnPrevPageSavedAddresses.Click += btnSavedPrevPage_Click;
            // 
            // labelPagesSavedAddresses
            // 
            labelPagesSavedAddresses.AutoSize = true;
            labelPagesSavedAddresses.Location = new System.Drawing.Point(103, 603);
            labelPagesSavedAddresses.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelPagesSavedAddresses.Name = "labelPagesSavedAddresses";
            labelPagesSavedAddresses.Size = new System.Drawing.Size(73, 15);
            labelPagesSavedAddresses.TabIndex = 21;
            labelPagesSavedAddresses.Text = "Seite 1 von 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(554, 636);
            Controls.Add(btnNextPageSavedAddresses);
            Controls.Add(btnPrevPageSavedAddresses);
            Controls.Add(labelPagesSavedAddresses);
            Controls.Add(labelSavedAddresses);
            Controls.Add(listSavedAdresses);
            Controls.Add(btnNextPage);
            Controls.Add(btnPrevPage);
            Controls.Add(btnWrite);
            Controls.Add(labelWriteValue);
            Controls.Add(btnAddWriteValue);
            Controls.Add(btnSubWriteValue);
            Controls.Add(textBoxWriteValue);
            Controls.Add(labelPages);
            Controls.Add(listAdresses);
            Controls.Add(labelValue);
            Controls.Add(labelFoundAdresses);
            Controls.Add(btnNextScan);
            Controls.Add(btnNewScan);
            Controls.Add(btnSubScanValue);
            Controls.Add(btnAddScanValue);
            Controls.Add(textBoxScanValue);
            Controls.Add(labelProcess);
            Controls.Add(comboBoxProcessesList);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "Form1";
            Text = "FlopperCheat";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxProcessesList;
        private System.Windows.Forms.Label labelProcess;
        private System.Windows.Forms.TextBox textBoxScanValue;
        private System.Windows.Forms.Button btnAddScanValue;
        private System.Windows.Forms.Button btnSubScanValue;
        private System.Windows.Forms.Button btnNewScan;
        private System.Windows.Forms.Button btnNextScan;
        private System.Windows.Forms.Label labelFoundAdresses;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.ListView listAdresses;
        private System.Windows.Forms.Label labelPages;
        private System.Windows.Forms.TextBox textBoxWriteValue;
        private System.Windows.Forms.Button btnSubWriteValue;
        private System.Windows.Forms.Button btnAddWriteValue;
        private System.Windows.Forms.Label labelWriteValue;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.ListView listSavedAdresses;
        private System.Windows.Forms.Label labelSavedAddresses;
        private System.Windows.Forms.Button btnNextPageSavedAddresses;
        private System.Windows.Forms.Button btnPrevPageSavedAddresses;
        private System.Windows.Forms.Label labelPagesSavedAddresses;
    }
}

