namespace SenLogic
{
    partial class StaticWeighment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StaticWeighment));
            this.gbuserlist = new System.Windows.Forms.GroupBox();
            this.com2 = new AxMSCommLib.AxMSComm();
            this.txtTotalWeight = new System.Windows.Forms.TextBox();
            this.txtWagonWt = new System.Windows.Forms.TextBox();
            this.txtWslno = new System.Windows.Forms.TextBox();
            this.staticDataGrid = new System.Windows.Forms.DataGridView();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnEndofWeighment = new System.Windows.Forms.Button();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtRakeNo = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbuserlist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.com2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staticDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbuserlist
            // 
            this.gbuserlist.Controls.Add(this.com2);
            this.gbuserlist.Controls.Add(this.txtTotalWeight);
            this.gbuserlist.Controls.Add(this.txtWagonWt);
            this.gbuserlist.Controls.Add(this.txtWslno);
            this.gbuserlist.Controls.Add(this.staticDataGrid);
            this.gbuserlist.Controls.Add(this.txtWeight);
            this.gbuserlist.Controls.Add(this.btnUpdate);
            this.gbuserlist.Controls.Add(this.btnExit);
            this.gbuserlist.Controls.Add(this.btnDelete);
            this.gbuserlist.Controls.Add(this.btnClear);
            this.gbuserlist.Controls.Add(this.btnPrint);
            this.gbuserlist.Controls.Add(this.btnEndofWeighment);
            this.gbuserlist.Controls.Add(this.txtTime);
            this.gbuserlist.Controls.Add(this.txtDate);
            this.gbuserlist.Controls.Add(this.txtRakeNo);
            this.gbuserlist.Controls.Add(this.lblTitle);
            this.gbuserlist.Controls.Add(this.pictureBox1);
            this.gbuserlist.Location = new System.Drawing.Point(1, -8);
            this.gbuserlist.Margin = new System.Windows.Forms.Padding(4);
            this.gbuserlist.Name = "gbuserlist";
            this.gbuserlist.Padding = new System.Windows.Forms.Padding(4);
            this.gbuserlist.Size = new System.Drawing.Size(1338, 758);
            this.gbuserlist.TabIndex = 6;
            this.gbuserlist.TabStop = false;
            // 
            // com2
            // 
            this.com2.Enabled = true;
            this.com2.Location = new System.Drawing.Point(798, 23);
            this.com2.Name = "com2";
            this.com2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("com2.OcxState")));
            this.com2.Size = new System.Drawing.Size(38, 38);
            this.com2.TabIndex = 62;
            // 
            // txtTotalWeight
            // 
            this.txtTotalWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.txtTotalWeight.Location = new System.Drawing.Point(582, 689);
            this.txtTotalWeight.Name = "txtTotalWeight";
            this.txtTotalWeight.Size = new System.Drawing.Size(161, 38);
            this.txtTotalWeight.TabIndex = 15;
            this.txtTotalWeight.Text = "0";
            this.txtTotalWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWagonWt
            // 
            this.txtWagonWt.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.txtWagonWt.Location = new System.Drawing.Point(407, 689);
            this.txtWagonWt.Name = "txtWagonWt";
            this.txtWagonWt.Size = new System.Drawing.Size(160, 38);
            this.txtWagonWt.TabIndex = 14;
            this.txtWagonWt.Text = "0";
            this.txtWagonWt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtWslno
            // 
            this.txtWslno.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.txtWslno.Location = new System.Drawing.Point(255, 689);
            this.txtWslno.Name = "txtWslno";
            this.txtWslno.Size = new System.Drawing.Size(127, 38);
            this.txtWslno.TabIndex = 13;
            this.txtWslno.Text = "1";
            this.txtWslno.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // staticDataGrid
            // 
            this.staticDataGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.staticDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.staticDataGrid.Location = new System.Drawing.Point(32, 96);
            this.staticDataGrid.Name = "staticDataGrid";
            this.staticDataGrid.ReadOnly = true;
            this.staticDataGrid.Size = new System.Drawing.Size(567, 512);
            this.staticDataGrid.TabIndex = 12;
            // 
            // txtWeight
            // 
            this.txtWeight.BackColor = System.Drawing.Color.Black;
            this.txtWeight.Font = new System.Drawing.Font("Times New Roman", 40F, System.Drawing.FontStyle.Bold);
            this.txtWeight.ForeColor = System.Drawing.Color.Lime;
            this.txtWeight.Location = new System.Drawing.Point(716, 85);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(260, 69);
            this.txtWeight.TabIndex = 1;
            this.txtWeight.Text = "0";
            this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Lime;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(781, 680);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(146, 50);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(759, 551);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(177, 42);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(759, 503);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(177, 42);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(759, 455);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(177, 42);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(759, 407);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(177, 42);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnEndofWeighment
            // 
            this.btnEndofWeighment.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnEndofWeighment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEndofWeighment.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEndofWeighment.Location = new System.Drawing.Point(759, 359);
            this.btnEndofWeighment.Name = "btnEndofWeighment";
            this.btnEndofWeighment.Size = new System.Drawing.Size(177, 42);
            this.btnEndofWeighment.TabIndex = 5;
            this.btnEndofWeighment.Text = "&End of Weighment";
            this.btnEndofWeighment.UseVisualStyleBackColor = false;
            this.btnEndofWeighment.Click += new System.EventHandler(this.btnEndofWeighment_Click);
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtTime.Font = new System.Drawing.Font("Times New Roman", 22F, System.Drawing.FontStyle.Bold);
            this.txtTime.ForeColor = System.Drawing.Color.White;
            this.txtTime.Location = new System.Drawing.Point(727, 264);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(241, 41);
            this.txtTime.TabIndex = 4;
            this.txtTime.Text = "19:44:52";
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtDate.Font = new System.Drawing.Font("Times New Roman", 22F, System.Drawing.FontStyle.Bold);
            this.txtDate.ForeColor = System.Drawing.Color.White;
            this.txtDate.Location = new System.Drawing.Point(727, 215);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(241, 41);
            this.txtDate.TabIndex = 3;
            this.txtDate.Text = "13/11/2015";
            // 
            // txtRakeNo
            // 
            this.txtRakeNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtRakeNo.Font = new System.Drawing.Font("Times New Roman", 22F, System.Drawing.FontStyle.Bold);
            this.txtRakeNo.ForeColor = System.Drawing.Color.White;
            this.txtRakeNo.Location = new System.Drawing.Point(727, 160);
            this.txtRakeNo.Name = "txtRakeNo";
            this.txtRakeNo.Size = new System.Drawing.Size(241, 41);
            this.txtRakeNo.TabIndex = 2;
            this.txtRakeNo.Text = "2015000130";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Italic);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(463, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(81, 21);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "SenLogic";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SenLogic.Properties.Resources.static_Weigment;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1367, 750);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = " ";
            this.dataGridViewImageColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewImageColumn1.Image")));
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 21;
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
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StaticWeighment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 741);
            this.Controls.Add(this.gbuserlist);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "StaticWeighment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.StaticWeighment_Load);
            this.gbuserlist.ResumeLayout(false);
            this.gbuserlist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.com2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staticDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbuserlist;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnEndofWeighment;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.TextBox txtRakeNo;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.TextBox txtTotalWeight;
        private System.Windows.Forms.TextBox txtWagonWt;
        private System.Windows.Forms.TextBox txtWslno;
        private System.Windows.Forms.DataGridView staticDataGrid;
        private AxMSCommLib.AxMSComm com2;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.Timer timer1;
    }
}