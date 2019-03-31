namespace SenLogic
{
    partial class frmManualTareEdit
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstRakeNumber = new System.Windows.Forms.ListBox();
            this.dgRakeView = new System.Windows.Forms.DataGridView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtSlno = new System.Windows.Forms.TextBox();
            this.txtWagonNo = new System.Windows.Forms.TextBox();
            this.txtTareWt = new System.Windows.Forms.MaskedTextBox();
            this.txtCCWt = new System.Windows.Forms.MaskedTextBox();
            this.txtRakeNo = new System.Windows.Forms.TextBox();
            this.pict1 = new System.Windows.Forms.PictureBox();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRakeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lstRakeNumber);
            this.groupBox3.Controls.Add(this.dgRakeView);
            this.groupBox3.Controls.Add(this.btnExit);
            this.groupBox3.Controls.Add(this.btnUpdate);
            this.groupBox3.Controls.Add(this.txtSlno);
            this.groupBox3.Controls.Add(this.txtWagonNo);
            this.groupBox3.Controls.Add(this.txtTareWt);
            this.groupBox3.Controls.Add(this.txtCCWt);
            this.groupBox3.Controls.Add(this.txtRakeNo);
            this.groupBox3.Controls.Add(this.pict1);
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1219, 774);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // lstRakeNumber
            // 
            this.lstRakeNumber.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstRakeNumber.FormattingEnabled = true;
            this.lstRakeNumber.ItemHeight = 21;
            this.lstRakeNumber.Location = new System.Drawing.Point(11, 37);
            this.lstRakeNumber.Name = "lstRakeNumber";
            this.lstRakeNumber.ScrollAlwaysVisible = true;
            this.lstRakeNumber.Size = new System.Drawing.Size(174, 571);
            this.lstRakeNumber.TabIndex = 52;
            this.lstRakeNumber.Visible = false;
            this.lstRakeNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstRakeNumber_KeyPress);
            // 
            // dgRakeView
            // 
            this.dgRakeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRakeView.Location = new System.Drawing.Point(36, 40);
            this.dgRakeView.Name = "dgRakeView";
            this.dgRakeView.ReadOnly = true;
            this.dgRakeView.Size = new System.Drawing.Size(941, 548);
            this.dgRakeView.TabIndex = 51;
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(536, 709);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(132, 36);
            this.btnExit.TabIndex = 50;
            this.btnExit.Text = "&EXIT";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Teal;
            this.btnUpdate.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(341, 709);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(132, 38);
            this.btnUpdate.TabIndex = 49;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtSlno
            // 
            this.txtSlno.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlno.Location = new System.Drawing.Point(273, 636);
            this.txtSlno.Name = "txtSlno";
            this.txtSlno.Size = new System.Drawing.Size(108, 32);
            this.txtSlno.TabIndex = 48;
            this.txtSlno.Text = "1";
            this.txtSlno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSlno_KeyPress);
            // 
            // txtWagonNo
            // 
            this.txtWagonNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtWagonNo.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWagonNo.Location = new System.Drawing.Point(424, 636);
            this.txtWagonNo.Name = "txtWagonNo";
            this.txtWagonNo.Size = new System.Drawing.Size(212, 32);
            this.txtWagonNo.TabIndex = 47;
            this.txtWagonNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWagonNo_KeyPress);
            // 
            // txtTareWt
            // 
            this.txtTareWt.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTareWt.Location = new System.Drawing.Point(670, 636);
            this.txtTareWt.Mask = "00.00";
            this.txtTareWt.Name = "txtTareWt";
            this.txtTareWt.Size = new System.Drawing.Size(107, 32);
            this.txtTareWt.TabIndex = 46;
            this.txtTareWt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTareWt_KeyPress);
            // 
            // txtCCWt
            // 
            this.txtCCWt.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCWt.Location = new System.Drawing.Point(794, 636);
            this.txtCCWt.Mask = "00.00";
            this.txtCCWt.Name = "txtCCWt";
            this.txtCCWt.Size = new System.Drawing.Size(91, 32);
            this.txtCCWt.TabIndex = 45;
            this.txtCCWt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCCWt_KeyPress);
            // 
            // txtRakeNo
            // 
            this.txtRakeNo.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRakeNo.Location = new System.Drawing.Point(59, 636);
            this.txtRakeNo.Name = "txtRakeNo";
            this.txtRakeNo.Size = new System.Drawing.Size(176, 32);
            this.txtRakeNo.TabIndex = 44;
            this.txtRakeNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRakeNo_KeyPress);
            // 
            // pict1
            // 
            this.pict1.Image = global::SenLogic.Properties.Resources.ManualTareEdit;
            this.pict1.Location = new System.Drawing.Point(6, 9);
            this.pict1.Name = "pict1";
            this.pict1.Size = new System.Drawing.Size(1213, 738);
            this.pict1.TabIndex = 0;
            this.pict1.TabStop = false;
            // 
            // frmManualTareEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1234, 741);
            this.Controls.Add(this.groupBox3);
            this.KeyPreview = true;
            this.Name = "frmManualTareEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manual Tare Edit";
            this.Load += new System.EventHandler(this.frmManualTareEdit_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRakeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pict1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox pict1;
        private System.Windows.Forms.TextBox txtSlno;
        private System.Windows.Forms.TextBox txtWagonNo;
        private System.Windows.Forms.MaskedTextBox txtTareWt;
        private System.Windows.Forms.MaskedTextBox txtCCWt;
        private System.Windows.Forms.TextBox txtRakeNo;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView dgRakeView;
        private System.Windows.Forms.ListBox lstRakeNumber;
    }
}