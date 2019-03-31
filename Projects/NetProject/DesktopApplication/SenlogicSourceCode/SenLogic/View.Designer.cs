namespace SenLogic
{
    partial class View
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(View));
            this.txtstatus = new System.Windows.Forms.TextBox();
            this.txtRakeArrivalDate = new System.Windows.Forms.TextBox();
            this.txtRakeArrivalTime = new System.Windows.Forms.TextBox();
            this.txtWagonType = new System.Windows.Forms.TextBox();
            this.txtDirection = new System.Windows.Forms.TextBox();
            this.txtRakeNo = new System.Windows.Forms.TextBox();
            this.txtDateTime = new System.Windows.Forms.TextBox();
            this.radGross = new System.Windows.Forms.RadioButton();
            this.radTare = new System.Windows.Forms.RadioButton();
            this.btnEndofWeighment = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.txtweight = new System.Windows.Forms.TextBox();
            this.labSlno = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblWeight = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCommulativeWeight = new System.Windows.Forms.Label();
            this.pnlManualGross = new System.Windows.Forms.Panel();
            this.txtSlno = new System.Windows.Forms.TextBox();
            this.txtSpd = new System.Windows.Forms.MaskedTextBox();
            this.txtGrsWt = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSlCount = new System.Windows.Forms.TextBox();
            this.txtFromTime = new System.Windows.Forms.MaskedTextBox();
            this.txtEndTime = new System.Windows.Forms.MaskedTextBox();
            this.com2 = new AxMSCommLib.AxMSComm();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnlManualGross.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.com2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtstatus
            // 
            this.txtstatus.BackColor = System.Drawing.Color.Black;
            this.txtstatus.Font = new System.Drawing.Font("Times New Roman", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtstatus.ForeColor = System.Drawing.Color.Lime;
            this.txtstatus.Location = new System.Drawing.Point(19, 67);
            this.txtstatus.Multiline = true;
            this.txtstatus.Name = "txtstatus";
            this.txtstatus.ReadOnly = true;
            this.txtstatus.Size = new System.Drawing.Size(541, 59);
            this.txtstatus.TabIndex = 39;
            this.txtstatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRakeArrivalDate
            // 
            this.txtRakeArrivalDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtRakeArrivalDate.Font = new System.Drawing.Font("Times New Roman", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRakeArrivalDate.ForeColor = System.Drawing.Color.White;
            this.txtRakeArrivalDate.Location = new System.Drawing.Point(244, 159);
            this.txtRakeArrivalDate.Name = "txtRakeArrivalDate";
            this.txtRakeArrivalDate.ReadOnly = true;
            this.txtRakeArrivalDate.Size = new System.Drawing.Size(167, 34);
            this.txtRakeArrivalDate.TabIndex = 38;
            // 
            // txtRakeArrivalTime
            // 
            this.txtRakeArrivalTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtRakeArrivalTime.Font = new System.Drawing.Font("Times New Roman", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRakeArrivalTime.ForeColor = System.Drawing.Color.White;
            this.txtRakeArrivalTime.Location = new System.Drawing.Point(244, 208);
            this.txtRakeArrivalTime.Name = "txtRakeArrivalTime";
            this.txtRakeArrivalTime.ReadOnly = true;
            this.txtRakeArrivalTime.Size = new System.Drawing.Size(167, 34);
            this.txtRakeArrivalTime.TabIndex = 2;
            // 
            // txtWagonType
            // 
            this.txtWagonType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtWagonType.Font = new System.Drawing.Font("Times New Roman", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWagonType.ForeColor = System.Drawing.Color.White;
            this.txtWagonType.Location = new System.Drawing.Point(243, 255);
            this.txtWagonType.Name = "txtWagonType";
            this.txtWagonType.ReadOnly = true;
            this.txtWagonType.Size = new System.Drawing.Size(169, 34);
            this.txtWagonType.TabIndex = 3;
            // 
            // txtDirection
            // 
            this.txtDirection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtDirection.Font = new System.Drawing.Font("Times New Roman", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDirection.ForeColor = System.Drawing.Color.White;
            this.txtDirection.Location = new System.Drawing.Point(770, 159);
            this.txtDirection.Name = "txtDirection";
            this.txtDirection.ReadOnly = true;
            this.txtDirection.Size = new System.Drawing.Size(202, 34);
            this.txtDirection.TabIndex = 4;
            // 
            // txtRakeNo
            // 
            this.txtRakeNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtRakeNo.Font = new System.Drawing.Font("Times New Roman", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRakeNo.ForeColor = System.Drawing.Color.White;
            this.txtRakeNo.Location = new System.Drawing.Point(770, 208);
            this.txtRakeNo.Name = "txtRakeNo";
            this.txtRakeNo.ReadOnly = true;
            this.txtRakeNo.Size = new System.Drawing.Size(202, 34);
            this.txtRakeNo.TabIndex = 5;
            // 
            // txtDateTime
            // 
            this.txtDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtDateTime.Font = new System.Drawing.Font("Times New Roman", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateTime.ForeColor = System.Drawing.Color.White;
            this.txtDateTime.Location = new System.Drawing.Point(770, 255);
            this.txtDateTime.Name = "txtDateTime";
            this.txtDateTime.ReadOnly = true;
            this.txtDateTime.Size = new System.Drawing.Size(202, 34);
            this.txtDateTime.TabIndex = 6;
            // 
            // radGross
            // 
            this.radGross.AutoSize = true;
            this.radGross.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.radGross.Checked = true;
            this.radGross.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radGross.ForeColor = System.Drawing.Color.Lime;
            this.radGross.Location = new System.Drawing.Point(652, 348);
            this.radGross.Name = "radGross";
            this.radGross.Size = new System.Drawing.Size(101, 35);
            this.radGross.TabIndex = 1;
            this.radGross.TabStop = true;
            this.radGross.Text = "&Gross";
            this.radGross.UseVisualStyleBackColor = false;
            // 
            // radTare
            // 
            this.radTare.AutoSize = true;
            this.radTare.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.radTare.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radTare.ForeColor = System.Drawing.Color.Red;
            this.radTare.Location = new System.Drawing.Point(827, 348);
            this.radTare.Name = "radTare";
            this.radTare.Size = new System.Drawing.Size(86, 35);
            this.radTare.TabIndex = 54;
            this.radTare.TabStop = true;
            this.radTare.Text = "&Tare";
            this.radTare.UseVisualStyleBackColor = false;
            // 
            // btnEndofWeighment
            // 
            this.btnEndofWeighment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnEndofWeighment.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndofWeighment.Location = new System.Drawing.Point(652, 417);
            this.btnEndofWeighment.Name = "btnEndofWeighment";
            this.btnEndofWeighment.Size = new System.Drawing.Size(160, 38);
            this.btnEndofWeighment.TabIndex = 55;
            this.btnEndofWeighment.Text = "&End Of Weighment";
            this.btnEndofWeighment.UseVisualStyleBackColor = false;
            this.btnEndofWeighment.Click += new System.EventHandler(this.btnEndofWeighment_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnPrint.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(818, 417);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(155, 38);
            this.btnPrint.TabIndex = 56;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRefresh.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(650, 487);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(160, 43);
            this.btnRefresh.TabIndex = 57;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(818, 487);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(156, 41);
            this.btnExit.TabIndex = 58;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridView1.Location = new System.Drawing.Point(21, 314);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Size = new System.Drawing.Size(564, 420);
            this.dataGridView1.TabIndex = 60;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // txtweight
            // 
            this.txtweight.BackColor = System.Drawing.Color.Black;
            this.txtweight.Font = new System.Drawing.Font("Times New Roman", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtweight.ForeColor = System.Drawing.Color.Lime;
            this.txtweight.Location = new System.Drawing.Point(589, 67);
            this.txtweight.Multiline = true;
            this.txtweight.Name = "txtweight";
            this.txtweight.ReadOnly = true;
            this.txtweight.Size = new System.Drawing.Size(413, 59);
            this.txtweight.TabIndex = 62;
            this.txtweight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labSlno
            // 
            this.labSlno.AutoSize = true;
            this.labSlno.BackColor = System.Drawing.Color.Black;
            this.labSlno.Font = new System.Drawing.Font("Times New Roman", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labSlno.ForeColor = System.Drawing.Color.White;
            this.labSlno.Location = new System.Drawing.Point(595, 75);
            this.labSlno.Name = "labSlno";
            this.labSlno.Size = new System.Drawing.Size(0, 43);
            this.labSlno.TabIndex = 63;
            this.labSlno.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(659, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 43);
            this.label1.TabIndex = 64;
            this.label1.Text = ":";
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.BackColor = System.Drawing.Color.Black;
            this.lblWeight.Font = new System.Drawing.Font("Times New Roman", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeight.ForeColor = System.Drawing.Color.Lime;
            this.lblWeight.Location = new System.Drawing.Point(686, 75);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(0, 43);
            this.lblWeight.TabIndex = 65;
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(796, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 43);
            this.label2.TabIndex = 66;
            this.label2.Text = ":";
            // 
            // lblCommulativeWeight
            // 
            this.lblCommulativeWeight.AutoSize = true;
            this.lblCommulativeWeight.BackColor = System.Drawing.Color.Black;
            this.lblCommulativeWeight.Font = new System.Drawing.Font("Times New Roman", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCommulativeWeight.ForeColor = System.Drawing.Color.White;
            this.lblCommulativeWeight.Location = new System.Drawing.Point(852, 75);
            this.lblCommulativeWeight.Name = "lblCommulativeWeight";
            this.lblCommulativeWeight.Size = new System.Drawing.Size(0, 43);
            this.lblCommulativeWeight.TabIndex = 67;
            this.lblCommulativeWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlManualGross
            // 
            this.pnlManualGross.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnlManualGross.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlManualGross.Controls.Add(this.txtSlno);
            this.pnlManualGross.Controls.Add(this.txtSpd);
            this.pnlManualGross.Controls.Add(this.txtGrsWt);
            this.pnlManualGross.Controls.Add(this.label5);
            this.pnlManualGross.Controls.Add(this.label4);
            this.pnlManualGross.Controls.Add(this.label3);
            this.pnlManualGross.Location = new System.Drawing.Point(621, 637);
            this.pnlManualGross.Name = "pnlManualGross";
            this.pnlManualGross.Size = new System.Drawing.Size(351, 63);
            this.pnlManualGross.TabIndex = 68;
            // 
            // txtSlno
            // 
            this.txtSlno.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlno.Location = new System.Drawing.Point(16, 26);
            this.txtSlno.Name = "txtSlno";
            this.txtSlno.Size = new System.Drawing.Size(91, 26);
            this.txtSlno.TabIndex = 12;
            this.txtSlno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSlno_KeyPress);
            // 
            // txtSpd
            // 
            this.txtSpd.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpd.Location = new System.Drawing.Point(263, 24);
            this.txtSpd.Mask = "00.00";
            this.txtSpd.Name = "txtSpd";
            this.txtSpd.Size = new System.Drawing.Size(71, 26);
            this.txtSpd.TabIndex = 14;
            this.txtSpd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSpd_KeyPress);
            // 
            // txtGrsWt
            // 
            this.txtGrsWt.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrsWt.Location = new System.Drawing.Point(136, 26);
            this.txtGrsWt.Mask = "00.00";
            this.txtGrsWt.Name = "txtGrsWt";
            this.txtGrsWt.Size = new System.Drawing.Size(83, 26);
            this.txtGrsWt.TabIndex = 13;
            this.txtGrsWt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGrsWt_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(263, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Speed";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(147, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Weight";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "SLNo";
            // 
            // txtSlCount
            // 
            this.txtSlCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlCount.Location = new System.Drawing.Point(885, 594);
            this.txtSlCount.Name = "txtSlCount";
            this.txtSlCount.Size = new System.Drawing.Size(77, 26);
            this.txtSlCount.TabIndex = 11;
            this.txtSlCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSlCount_KeyPress);
            // 
            // txtFromTime
            // 
            this.txtFromTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromTime.Location = new System.Drawing.Point(636, 595);
            this.txtFromTime.Mask = "00:00:00";
            this.txtFromTime.Name = "txtFromTime";
            this.txtFromTime.Size = new System.Drawing.Size(83, 26);
            this.txtFromTime.TabIndex = 9;
            this.txtFromTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromTime_KeyPress);
            // 
            // txtEndTime
            // 
            this.txtEndTime.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEndTime.Location = new System.Drawing.Point(758, 594);
            this.txtEndTime.Mask = "00:00:00";
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(83, 26);
            this.txtEndTime.TabIndex = 10;
            this.txtEndTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEndTime_KeyPress);
            // 
            // com2
            // 
            this.com2.Enabled = true;
            this.com2.Location = new System.Drawing.Point(893, 23);
            this.com2.Name = "com2";
            this.com2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("com2.OcxState")));
            this.com2.Size = new System.Drawing.Size(38, 38);
            this.com2.TabIndex = 61;
            this.com2.OnComm += new System.EventHandler(this.com2_OnComm);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SenLogic.Properties.Resources.dynamicweighing;
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1229, 734);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(-1, 1);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(157, 31);
            this.lblTitle.TabIndex = 69;
            this.lblTitle.Text = "SENLOGIC";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Location = new System.Drawing.Point(180, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(664, 34);
            this.panel1.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLabel1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.Lime;
            this.linkLabel1.Location = new System.Drawing.Point(649, 554);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(294, 22);
            this.linkLabel1.TabIndex = 69;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "SHIFT+CTRL+F4 - Reset DS505";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Location = new System.Drawing.Point(621, 705);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(352, 29);
            this.panel2.TabIndex = 70;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 609);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.txtFromTime);
            this.Controls.Add(this.txtSlCount);
            this.Controls.Add(this.pnlManualGross);
            this.Controls.Add(this.lblCommulativeWeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblWeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labSlno);
            this.Controls.Add(this.txtweight);
            this.Controls.Add(this.com2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnEndofWeighment);
            this.Controls.Add(this.radTare);
            this.Controls.Add(this.radGross);
            this.Controls.Add(this.txtDateTime);
            this.Controls.Add(this.txtRakeNo);
            this.Controls.Add(this.txtDirection);
            this.Controls.Add(this.txtWagonType);
            this.Controls.Add(this.txtRakeArrivalTime);
            this.Controls.Add(this.txtRakeArrivalDate);
            this.Controls.Add(this.txtstatus);
            this.Controls.Add(this.pictureBox1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.View_FormClosed);
            this.Load += new System.EventHandler(this.View_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnlManualGross.ResumeLayout(false);
            this.pnlManualGross.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.com2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtstatus;
        private AxMSCommLib.AxMSComm com2;
        private System.Windows.Forms.TextBox txtRakeArrivalDate;
        private System.Windows.Forms.TextBox txtRakeArrivalTime;
        private System.Windows.Forms.TextBox txtWagonType;
        private System.Windows.Forms.TextBox txtDirection;
        private System.Windows.Forms.TextBox txtRakeNo;
        private System.Windows.Forms.TextBox txtDateTime;
        private System.Windows.Forms.RadioButton radGross;
        private System.Windows.Forms.RadioButton radTare;
        private System.Windows.Forms.Button btnEndofWeighment;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer1;        
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.TextBox txtweight;
        private System.Windows.Forms.Label labSlno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCommulativeWeight;
        private System.Windows.Forms.Panel pnlManualGross;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtSpd;
        private System.Windows.Forms.MaskedTextBox txtGrsWt;
        private System.Windows.Forms.TextBox txtSlno;
        private System.Windows.Forms.TextBox txtSlCount;
        private System.Windows.Forms.MaskedTextBox txtFromTime;
        private System.Windows.Forms.MaskedTextBox txtEndTime;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel panel2;
    }
}