using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SenLogic
{
    public partial class SystemSettings : Form
    {
      

        #region " Form event/method "
        /// <summary>
        /// From init method
        /// </summary>
        public SystemSettings()
        {
            InitializeComponent();
            
        }

               #endregion

        #region " user define functions"
        /// <summary>
        /// On Fomm load the getting called and fetch all records from user_Access table
        /// </summary>
        private void Load_AccessSetting()
        {
           
        }
        /// <summary>
        /// On Fomm load the getting called , fetch all records from active users table and fill combo box
        /// </summary>
        private void Load_Users()
        {
            

        }
        #endregion


        private void LoadPortingValue()
        {
            
            var dtPorting = Shared_Class.LoadPortingValue();            
            if(dtPorting.Tables[0]!=null && dtPorting.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dtPorting.Tables[0].Rows[0][0].ToString()) && !string.IsNullOrEmpty(dtPorting.Tables[0].Rows[0][1].ToString()))
            {
                cmbSettingPort1.Text = dtPorting.Tables[0].Rows[0][0].ToString();
                txtSettingBaudRate1.Text = dtPorting.Tables[0].Rows[0][1].ToString();
                if (!string.IsNullOrEmpty(dtPorting.Tables[0].Rows[0][2].ToString()))
                {
                    bool isStatic = Convert.ToBoolean(dtPorting.Tables[0].Rows[0][2].ToString());
                    if (isStatic)
                        radStaicWeighing.Checked = true;
                    else
                        radInMotionDS505.Checked = true;
                }
            }              
        
        }

        private int ScreenCommonRecordCount()
        {
            string selectQuery = "select count(*) from ScreenCommon";
            int cnt = 0;
            using (var db = new DBEngine())
            {
                cnt = (int)db.ExecuteScalar(string.Format(selectQuery), null);
            }
            return cnt;
        }
        private void btnSettingPort1_Click(object sender, EventArgs e)
        {
            if (cmbSettingPort1.SelectedItem == null)
                return;

            string query = string.Empty;
            string value = string.Empty;
            string insertQuery = "insert into ScreenCommon(Commtrack1,commBaud1) values('{0}','{1}')";
            string updateQuery = "update ScreenCommon set Commtrack1='{0}',commBaud1='{1}'";

            if (ScreenCommonRecordCount() > 0)
                query = updateQuery;
            else
                query = insertQuery;
            using (var db = new DBEngine())
            {             

                db.ExecuteNonQuery(string.Format(query,cmbSettingPort1.SelectedItem.ToString(),txtSettingBaudRate1.Text));
            }
            MessageBox.Show("Records Saved Successfully");
        }

        private void SystemSettings_Load(object sender, EventArgs e)
        {
            EnableDisblePanel(0);
            LoadPortingValue();
        }

        private void btnRpcOk_Click(object sender, EventArgs e)
        {
            //radCh0, radCh1, radCh01, chkAxle, chkBogic
        }

        private void btnWBOk_Click(object sender, EventArgs e)
        {
            //chkWB1, chkWB2, chkWB12
        }

        private void btnRailSystemOk_Click(object sender, EventArgs e)
        {
            //chkCh0, chkCh01, chkCh1
        }

        private void btnSetWeightUpdate_Click(object sender, EventArgs e)
        {
            bool value = false;
            
            //chkSettingMultiEnable, chkSettingMultiDisable

            //IsMultiWeigh
            if (chkSettingMultiEnable.Checked)
                value = true;

            if (chkSettingMultiDisable.Checked)
                value = true;

            bool motionValue=false;

            if (radInMotionDS505.Checked)
                motionValue = false;

            if (radStaicWeighing.Checked)
                motionValue = true;

            string query = string.Empty;

            string insertQuery = "insert into ScreenCommon(IsMultiWeigh,IsStaticDynamic) values('{0}','{1}')";
            string updateQuery = "update ScreenCommon set IsMultiWeigh='{0}', IsStaticDynamic='{1}'";

            

            if (ScreenCommonRecordCount() > 0)
                query = updateQuery;
            else
                query = insertQuery;


            using (var db = new DBEngine())
            {

                db.ExecuteNonQuery(string.Format(query, value,motionValue));
            }
            MessageBox.Show("Record Saved");


        }

        private void btnSetWeightOk_Click(object sender, EventArgs e)
        {
            //radSettingWeighDisable, radSettingWeighEnable, chkSettingWeighRs485
        }

        private void radInMotionDS505_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisblePanel(0);
        }

        private void radComboRailSystem_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisblePanel(1);
        }

        private void radStaicWeighing_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisblePanel(0);
        }

        private void radDualStaticWeighing_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisblePanel(2);
        }

        private void radDualInMotionDS505_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisblePanel(3);
        }

        private void radComboRPC_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisblePanel(4);
        }

        private void radFullDraftComboR_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisblePanel(5);
        }

        private void EnableDisblePanel(int panelNo)
        {
            switch(panelNo)
            {
                case 1:
                    pnlComboRailSyste.Visible = true;
                    pnlRC.Visible = false;
                    pnlWeighBridge.Visible = false;
                    pnlWeight.Visible = false;
                    pnlSecondPort.Visible = false;
                break;
                case 2:
                    pnlComboRailSyste.Visible = false;
                    pnlRC.Visible = false;
                    pnlWeighBridge.Visible = false;
                    pnlWeight.Visible = true;
                    pnlSecondPort.Visible = true;
                break;
                case 3:
                    pnlComboRailSyste.Visible = false;
                    pnlRC.Visible = false;
                    pnlWeighBridge.Visible = false;
                    pnlWeight.Visible = false;
                    pnlSecondPort.Visible = true;
                break;
                case 4:
                    pnlComboRailSyste.Visible = false;
                    pnlRC.Visible = true;
                    pnlWeighBridge.Visible = false;
                    pnlWeight.Visible = false;
                    pnlSecondPort.Visible = true;
                break;
                case 5:
                    pnlComboRailSyste.Visible = false;
                    pnlRC.Visible = false;
                    pnlWeighBridge.Visible = true;
                    pnlWeight.Visible = false;
                    pnlSecondPort.Visible = true;
                break;
                default:
                    pnlComboRailSyste.Visible = false;
                    pnlRC.Visible = false;
                    pnlWeighBridge.Visible = false;
                    pnlWeight.Visible = false;
                    pnlSecondPort.Visible = false;
                break;

            }
        }

        private void chkCh0_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCh0.Checked)
            {
                chkCh01.Enabled = false;
                chkCh1.Enabled = false;
            }
            else
            {
                chkCh01.Enabled = true;
                chkCh1.Enabled = true;
            }
        }

        private void chkCh1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCh1.Checked)
            {
                chkCh0.Enabled = false;
                chkCh01.Enabled = false;
            }
            else
            {
                chkCh0.Enabled = true;
                chkCh01.Enabled = true;
            }

        }

        private void chkCh01_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCh01.Checked)
            {
                chkCh0.Enabled = false;
                chkCh1.Enabled = false;
            }
            else
            {
                chkCh0.Enabled = true;
                chkCh1.Enabled = true;
            }
        }

        private void chkAxle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAxle.Checked)
                chkBogic.Enabled = false;
            else
                chkBogic.Enabled = true;
        }

        private void chkBogic_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBogic.Checked)
                chkAxle.Enabled = false;
            else
                chkAxle.Enabled = true;
        }

        private void chkWB1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWB1.Checked)
            {
                chkWB12.Enabled = false;
                chkWB2.Enabled = false;
            }
            else
            {
                chkWB12.Enabled = true;
                chkWB2.Enabled = true;
            }
        }

        private void chkWB2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWB2.Checked)
            {
                chkWB1.Enabled = false;
                chkWB12.Enabled = false;
            }
            else
            {
                chkWB1.Enabled = true;
                chkWB12.Enabled = true;
            }
        }

        private void chkWB12_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWB12.Checked)
            {
                chkWB1.Enabled = false;
                chkWB2.Enabled = false;
            }
            else
            {
                chkWB1.Enabled = true;
                chkWB2.Enabled = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            new SettingPage().Show();
        }

    }
}
