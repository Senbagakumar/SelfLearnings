namespace SenLogic
{
    partial class ManualTareEntry
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
            this.gbuserlist = new System.Windows.Forms.GroupBox();
            this.lstRakeNumber = new System.Windows.Forms.ListBox();
            this.dgRakeView = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtSlno = new System.Windows.Forms.TextBox();
            this.txtWagonNo = new System.Windows.Forms.TextBox();
            this.txtTareWt = new System.Windows.Forms.MaskedTextBox();
            this.txtCCWt = new System.Windows.Forms.MaskedTextBox();
            this.txtRakeNo = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.gbuserlist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRakeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbuserlist
            // 
            this.gbuserlist.Controls.Add(this.lstRakeNumber);
            this.gbuserlist.Controls.Add(this.dgRakeView);
            this.gbuserlist.Controls.Add(this.btnExit);
            this.gbuserlist.Controls.Add(this.btnSave);
            this.gbuserlist.Controls.Add(this.txtSlno);
            this.gbuserlist.Controls.Add(this.txtWagonNo);
            this.gbuserlist.Controls.Add(this.txtTareWt);
            this.gbuserlist.Controls.Add(this.txtCCWt);
            this.gbuserlist.Controls.Add(this.txtRakeNo);
            this.gbuserlist.Controls.Add(this.pictureBox1);
            this.gbuserlist.Location = new System.Drawing.Point(0, 0);
            this.gbuserlist.Margin = new System.Windows.Forms.Padding(4);
            this.gbuserlist.Name = "gbuserlist";
            this.gbuserlist.Padding = new System.Windows.Forms.Padding(4);
            this.gbuserlist.Size = new System.Drawing.Size(1124, 788);
            this.gbuserlist.TabIndex = 0;
            this.gbuserlist.TabStop = false;
            // 
            // lstRakeNumber
            // 
            this.lstRakeNumber.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRakeNumber.FormattingEnabled = true;
            this.lstRakeNumber.ItemHeight = 21;
            this.lstRakeNumber.Location = new System.Drawing.Point(12, 35);
            this.lstRakeNumber.Name = "lstRakeNumber";
            this.lstRakeNumber.ScrollAlwaysVisible = true;
            this.lstRakeNumber.Size = new System.Drawing.Size(174, 550);
            this.lstRakeNumber.TabIndex = 1;
            this.lstRakeNumber.Visible = false;
            this.lstRakeNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstRakeNumber_KeyPress);
            // 
            // dgRakeView
            // 
            this.dgRakeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRakeView.Location = new System.Drawing.Point(30, 39);
            this.dgRakeView.Name = "dgRakeView";
            this.dgRakeView.ReadOnly = true;
            this.dgRakeView.Size = new System.Drawing.Size(952, 528);
            this.dgRakeView.TabIndex = 49;
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(542, 703);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(132, 42);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "&EXIT";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btnSave.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(375, 703);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 42);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "&SAVE";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtSlno
            // 
            this.txtSlno.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlno.Location = new System.Drawing.Point(251, 630);
            this.txtSlno.Name = "txtSlno";
            this.txtSlno.Size = new System.Drawing.Size(108, 32);
            this.txtSlno.TabIndex = 3;
            this.txtSlno.Text = "1";
            this.txtSlno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSlno_KeyPress);
            // 
            // txtWagonNo
            // 
            this.txtWagonNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWagonNo.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWagonNo.Location = new System.Drawing.Point(402, 630);
            this.txtWagonNo.Name = "txtWagonNo";
            this.txtWagonNo.Size = new System.Drawing.Size(212, 32);
            this.txtWagonNo.TabIndex = 4;
            this.txtWagonNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWagonNo_KeyPress);
            // 
            // txtTareWt
            // 
            this.txtTareWt.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTareWt.Location = new System.Drawing.Point(693, 630);
            this.txtTareWt.Mask = "00.00";
            this.txtTareWt.Name = "txtTareWt";
            this.txtTareWt.Size = new System.Drawing.Size(91, 32);
            this.txtTareWt.TabIndex = 5;
            this.txtTareWt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTareWt_KeyPress);
            // 
            // txtCCWt
            // 
            this.txtCCWt.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCWt.Location = new System.Drawing.Point(851, 630);
            this.txtCCWt.Mask = "00.00";
            this.txtCCWt.Name = "txtCCWt";
            this.txtCCWt.Size = new System.Drawing.Size(91, 32);
            this.txtCCWt.TabIndex = 6;
            this.txtCCWt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCCWt_KeyPress);
            // 
            // txtRakeNo
            // 
            this.txtRakeNo.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRakeNo.Location = new System.Drawing.Point(55, 630);
            this.txtRakeNo.Name = "txtRakeNo";
            this.txtRakeNo.Size = new System.Drawing.Size(176, 32);
            this.txtRakeNo.TabIndex = 2;
            this.txtRakeNo.Text = "2011000128";
            this.txtRakeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRakeNo_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SenLogic.Properties.Resources.ManualTareEntry;
            this.pictureBox1.Location = new System.Drawing.Point(7, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1117, 738);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = " ";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 21;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = " ";
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Width = 21;
            // 
            // ManualTareEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1054, 741);
            this.Controls.Add(this.gbuserlist);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ManualTareEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manual Tare Entry";
            this.Load += new System.EventHandler(this.ManualTareEntry_Load);
            this.gbuserlist.ResumeLayout(false);
            this.gbuserlist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRakeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbuserlist;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtSlno;
        private System.Windows.Forms.TextBox txtWagonNo;
        private System.Windows.Forms.MaskedTextBox txtTareWt;
        private System.Windows.Forms.MaskedTextBox txtCCWt;
        private System.Windows.Forms.TextBox txtRakeNo;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgRakeView;
        private System.Windows.Forms.ListBox lstRakeNumber;
    }
}