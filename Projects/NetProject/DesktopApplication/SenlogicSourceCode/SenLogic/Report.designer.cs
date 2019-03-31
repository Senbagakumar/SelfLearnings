namespace SenLogic
{
    partial class Report
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labEsc = new System.Windows.Forms.Label();
            this.txtToDate = new System.Windows.Forms.MaskedTextBox();
            this.txtFromDate = new System.Windows.Forms.MaskedTextBox();
            this.labToDate = new System.Windows.Forms.Label();
            this.labFromDate = new System.Windows.Forms.Label();
            this.cmbMaterialName = new System.Windows.Forms.ComboBox();
            this.txtRakeNo = new System.Windows.Forms.TextBox();
            this.labRakeNo = new System.Windows.Forms.Label();
            this.labMaterialName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lstRakeNumber = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chkExcelFile = new System.Windows.Forms.CheckBox();
            this.chkTextFile = new System.Windows.Forms.CheckBox();
            this.dgRakeView = new System.Windows.Forms.DataGridView();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnWeighBill = new System.Windows.Forms.Button();
            this.btnDateWiseReport = new System.Windows.Forms.Button();
            this.btnDesReport = new System.Windows.Forms.Button();
            this.btnMaterialReport = new System.Windows.Forms.Button();
            this.btnRakeSummary = new System.Windows.Forms.Button();
            this.btnRakeReport = new System.Windows.Forms.Button();
            this.pict1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRakeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.labEsc);
            this.panel1.Controls.Add(this.txtToDate);
            this.panel1.Controls.Add(this.txtFromDate);
            this.panel1.Controls.Add(this.labToDate);
            this.panel1.Controls.Add(this.labFromDate);
            this.panel1.Controls.Add(this.cmbMaterialName);
            this.panel1.Controls.Add(this.txtRakeNo);
            this.panel1.Controls.Add(this.labRakeNo);
            this.panel1.Controls.Add(this.labMaterialName);
            this.panel1.Location = new System.Drawing.Point(5, 602);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1255, 147);
            this.panel1.TabIndex = 17;
            // 
            // labEsc
            // 
            this.labEsc.AutoSize = true;
            this.labEsc.BackColor = System.Drawing.Color.Teal;
            this.labEsc.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labEsc.ForeColor = System.Drawing.Color.Yellow;
            this.labEsc.Location = new System.Drawing.Point(17, 113);
            this.labEsc.Name = "labEsc";
            this.labEsc.Size = new System.Drawing.Size(305, 25);
            this.labEsc.TabIndex = 40;
            this.labEsc.Text = "&esc -Clear The Rake Information";
            // 
            // txtToDate
            // 
            this.txtToDate.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToDate.Location = new System.Drawing.Point(1019, 75);
            this.txtToDate.Mask = "00/00/0000";
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(136, 32);
            this.txtToDate.TabIndex = 39;
            this.txtToDate.ValidatingType = typeof(System.DateTime);
            this.txtToDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtToDate_KeyPress);
            // 
            // txtFromDate
            // 
            this.txtFromDate.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromDate.Location = new System.Drawing.Point(1019, 30);
            this.txtFromDate.Mask = "00/00/0000";
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(136, 32);
            this.txtFromDate.TabIndex = 38;
            this.txtFromDate.ValidatingType = typeof(System.DateTime);
            this.txtFromDate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromDate_KeyPress);
            // 
            // labToDate
            // 
            this.labToDate.AutoSize = true;
            this.labToDate.BackColor = System.Drawing.Color.Teal;
            this.labToDate.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labToDate.ForeColor = System.Drawing.Color.White;
            this.labToDate.Location = new System.Drawing.Point(877, 75);
            this.labToDate.Name = "labToDate";
            this.labToDate.Size = new System.Drawing.Size(82, 25);
            this.labToDate.TabIndex = 37;
            this.labToDate.Text = "To Date";
            // 
            // labFromDate
            // 
            this.labFromDate.AutoSize = true;
            this.labFromDate.BackColor = System.Drawing.Color.Teal;
            this.labFromDate.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFromDate.ForeColor = System.Drawing.Color.White;
            this.labFromDate.Location = new System.Drawing.Point(876, 25);
            this.labFromDate.Name = "labFromDate";
            this.labFromDate.Size = new System.Drawing.Size(109, 25);
            this.labFromDate.TabIndex = 36;
            this.labFromDate.Text = "From Date";
            // 
            // cmbMaterialName
            // 
            this.cmbMaterialName.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMaterialName.FormattingEnabled = true;
            this.cmbMaterialName.Location = new System.Drawing.Point(589, 69);
            this.cmbMaterialName.Name = "cmbMaterialName";
            this.cmbMaterialName.Size = new System.Drawing.Size(187, 31);
            this.cmbMaterialName.TabIndex = 35;
            this.cmbMaterialName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbMaterialName_KeyPress);
            // 
            // txtRakeNo
            // 
            this.txtRakeNo.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRakeNo.Location = new System.Drawing.Point(6, 54);
            this.txtRakeNo.Name = "txtRakeNo";
            this.txtRakeNo.Size = new System.Drawing.Size(176, 32);
            this.txtRakeNo.TabIndex = 34;
            this.txtRakeNo.Text = "2011000128";
            this.txtRakeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRakeNo_KeyPress);
            // 
            // labRakeNo
            // 
            this.labRakeNo.AutoSize = true;
            this.labRakeNo.BackColor = System.Drawing.Color.Teal;
            this.labRakeNo.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labRakeNo.ForeColor = System.Drawing.Color.White;
            this.labRakeNo.Location = new System.Drawing.Point(17, 11);
            this.labRakeNo.Name = "labRakeNo";
            this.labRakeNo.Size = new System.Drawing.Size(143, 25);
            this.labRakeNo.TabIndex = 33;
            this.labRakeNo.Text = "Enter Rake No";
            // 
            // labMaterialName
            // 
            this.labMaterialName.AutoSize = true;
            this.labMaterialName.BackColor = System.Drawing.Color.Teal;
            this.labMaterialName.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labMaterialName.ForeColor = System.Drawing.Color.White;
            this.labMaterialName.Location = new System.Drawing.Point(584, 22);
            this.labMaterialName.Name = "labMaterialName";
            this.labMaterialName.Size = new System.Drawing.Size(205, 25);
            this.labMaterialName.TabIndex = 32;
            this.labMaterialName.Text = "Select Material Name";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lstRakeNumber);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.chkExcelFile);
            this.panel2.Controls.Add(this.chkTextFile);
            this.panel2.Controls.Add(this.dgRakeView);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Controls.Add(this.btnWeighBill);
            this.panel2.Controls.Add(this.btnDateWiseReport);
            this.panel2.Controls.Add(this.btnDesReport);
            this.panel2.Controls.Add(this.btnMaterialReport);
            this.panel2.Controls.Add(this.btnRakeSummary);
            this.panel2.Controls.Add(this.btnRakeReport);
            this.panel2.Controls.Add(this.pict1);
            this.panel2.Location = new System.Drawing.Point(5, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1255, 596);
            this.panel2.TabIndex = 18;
            // 
            // lstRakeNumber
            // 
            this.lstRakeNumber.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRakeNumber.FormattingEnabled = true;
            this.lstRakeNumber.ItemHeight = 21;
            this.lstRakeNumber.Location = new System.Drawing.Point(22, 11);
            this.lstRakeNumber.Name = "lstRakeNumber";
            this.lstRakeNumber.Size = new System.Drawing.Size(174, 571);
            this.lstRakeNumber.TabIndex = 46;
            this.lstRakeNumber.Visible = false;
            this.lstRakeNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstRakeNumber_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SenLogic.Properties.Resources.OneReport;
            this.pictureBox1.Location = new System.Drawing.Point(12, 458);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(190, 86);
            this.pictureBox1.TabIndex = 45;
            this.pictureBox1.TabStop = false;
            // 
            // chkExcelFile
            // 
            this.chkExcelFile.AutoSize = true;
            this.chkExcelFile.BackColor = System.Drawing.Color.Teal;
            this.chkExcelFile.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExcelFile.ForeColor = System.Drawing.Color.White;
            this.chkExcelFile.Location = new System.Drawing.Point(22, 513);
            this.chkExcelFile.Name = "chkExcelFile";
            this.chkExcelFile.Size = new System.Drawing.Size(155, 25);
            this.chkExcelFile.TabIndex = 43;
            this.chkExcelFile.Text = "Create Excel File";
            this.chkExcelFile.UseVisualStyleBackColor = false;
            this.chkExcelFile.Visible = false;
            this.chkExcelFile.CheckedChanged += new System.EventHandler(this.chkExcelFile_CheckedChanged);
            // 
            // chkTextFile
            // 
            this.chkTextFile.AutoSize = true;
            this.chkTextFile.BackColor = System.Drawing.Color.Teal;
            this.chkTextFile.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTextFile.ForeColor = System.Drawing.Color.White;
            this.chkTextFile.Location = new System.Drawing.Point(22, 471);
            this.chkTextFile.Name = "chkTextFile";
            this.chkTextFile.Size = new System.Drawing.Size(147, 25);
            this.chkTextFile.TabIndex = 44;
            this.chkTextFile.Text = "Create Text File";
            this.chkTextFile.UseVisualStyleBackColor = false;
            this.chkTextFile.Visible = false;
            this.chkTextFile.CheckedChanged += new System.EventHandler(this.chkTextFile_CheckedChanged);
            // 
            // dgRakeView
            // 
            this.dgRakeView.AllowUserToAddRows = false;
            this.dgRakeView.AllowUserToDeleteRows = false;
            this.dgRakeView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgRakeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRakeView.Location = new System.Drawing.Point(218, 19);
            this.dgRakeView.Name = "dgRakeView";
            this.dgRakeView.ReadOnly = true;
            this.dgRakeView.Size = new System.Drawing.Size(780, 562);
            this.dgRakeView.TabIndex = 38;
            // 
            // btnOk
            // 
            this.btnOk.AutoEllipsis = true;
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(26, 544);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(147, 37);
            this.btnOk.TabIndex = 36;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Visible = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(35, 401);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(145, 41);
            this.btnExit.TabIndex = 33;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AutoEllipsis = true;
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(33, 348);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(147, 37);
            this.btnPrint.TabIndex = 32;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClear
            // 
            this.btnClear.AutoEllipsis = true;
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(33, 276);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(147, 37);
            this.btnClear.TabIndex = 31;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnWeighBill
            // 
            this.btnWeighBill.AutoEllipsis = true;
            this.btnWeighBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnWeighBill.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnWeighBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeighBill.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeighBill.Location = new System.Drawing.Point(33, 237);
            this.btnWeighBill.Name = "btnWeighBill";
            this.btnWeighBill.Size = new System.Drawing.Size(147, 37);
            this.btnWeighBill.TabIndex = 30;
            this.btnWeighBill.Text = "&Weigh Bill";
            this.btnWeighBill.UseVisualStyleBackColor = false;
            this.btnWeighBill.Click += new System.EventHandler(this.btnWeighBill_Click);
            // 
            // btnDateWiseReport
            // 
            this.btnDateWiseReport.AutoEllipsis = true;
            this.btnDateWiseReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDateWiseReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnDateWiseReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDateWiseReport.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDateWiseReport.Location = new System.Drawing.Point(33, 197);
            this.btnDateWiseReport.Name = "btnDateWiseReport";
            this.btnDateWiseReport.Size = new System.Drawing.Size(147, 37);
            this.btnDateWiseReport.TabIndex = 29;
            this.btnDateWiseReport.Text = "D&ate Wise Rep";
            this.btnDateWiseReport.UseVisualStyleBackColor = false;
            this.btnDateWiseReport.Click += new System.EventHandler(this.btnDateWiseReport_Click);
            // 
            // btnDesReport
            // 
            this.btnDesReport.AutoEllipsis = true;
            this.btnDesReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnDesReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnDesReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDesReport.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDesReport.Location = new System.Drawing.Point(33, 158);
            this.btnDesReport.Name = "btnDesReport";
            this.btnDesReport.Size = new System.Drawing.Size(147, 37);
            this.btnDesReport.TabIndex = 28;
            this.btnDesReport.Text = "&Des.Report";
            this.btnDesReport.UseVisualStyleBackColor = false;
            this.btnDesReport.Click += new System.EventHandler(this.btnDesReport_Click);
            // 
            // btnMaterialReport
            // 
            this.btnMaterialReport.AutoEllipsis = true;
            this.btnMaterialReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnMaterialReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnMaterialReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaterialReport.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMaterialReport.Location = new System.Drawing.Point(33, 118);
            this.btnMaterialReport.Name = "btnMaterialReport";
            this.btnMaterialReport.Size = new System.Drawing.Size(147, 39);
            this.btnMaterialReport.TabIndex = 27;
            this.btnMaterialReport.Text = "&Material Report";
            this.btnMaterialReport.UseVisualStyleBackColor = false;
            this.btnMaterialReport.Click += new System.EventHandler(this.btnMaterialReport_Click);
            // 
            // btnRakeSummary
            // 
            this.btnRakeSummary.AutoEllipsis = true;
            this.btnRakeSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRakeSummary.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRakeSummary.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRakeSummary.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRakeSummary.Location = new System.Drawing.Point(33, 77);
            this.btnRakeSummary.Name = "btnRakeSummary";
            this.btnRakeSummary.Size = new System.Drawing.Size(147, 40);
            this.btnRakeSummary.TabIndex = 26;
            this.btnRakeSummary.Text = "Rake &Summary";
            this.btnRakeSummary.UseVisualStyleBackColor = false;
            this.btnRakeSummary.Click += new System.EventHandler(this.btnRakeSummary_Click);
            // 
            // btnRakeReport
            // 
            this.btnRakeReport.AutoEllipsis = true;
            this.btnRakeReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRakeReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRakeReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRakeReport.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRakeReport.Location = new System.Drawing.Point(33, 36);
            this.btnRakeReport.Name = "btnRakeReport";
            this.btnRakeReport.Size = new System.Drawing.Size(147, 40);
            this.btnRakeReport.TabIndex = 25;
            this.btnRakeReport.Text = "&Rake Report";
            this.btnRakeReport.UseVisualStyleBackColor = false;
            this.btnRakeReport.Click += new System.EventHandler(this.btnRakeReport_Click);
            // 
            // pict1
            // 
            this.pict1.Image = global::SenLogic.Properties.Resources.Report1;
            this.pict1.Location = new System.Drawing.Point(-1, -1);
            this.pict1.Name = "pict1";
            this.pict1.Size = new System.Drawing.Size(1255, 595);
            this.pict1.TabIndex = 0;
            this.pict1.TabStop = false;
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 741);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logger Report";
            this.Load += new System.EventHandler(this.Report_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRakeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pict1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnWeighBill;
        private System.Windows.Forms.Button btnDateWiseReport;
        private System.Windows.Forms.Button btnDesReport;
        private System.Windows.Forms.Button btnMaterialReport;
        private System.Windows.Forms.Button btnRakeSummary;
        private System.Windows.Forms.Button btnRakeReport;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label labRakeNo;
        private System.Windows.Forms.Label labMaterialName;
        private System.Windows.Forms.TextBox txtRakeNo;
        private System.Windows.Forms.ComboBox cmbMaterialName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label labToDate;
        private System.Windows.Forms.Label labFromDate;
        private System.Windows.Forms.MaskedTextBox txtToDate;
        private System.Windows.Forms.MaskedTextBox txtFromDate;
        private System.Windows.Forms.Label labEsc;
        private System.Windows.Forms.CheckBox chkTextFile;
        private System.Windows.Forms.CheckBox chkExcelFile;
        private System.Windows.Forms.DataGridView dgRakeView;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox lstRakeNumber;

    }
}