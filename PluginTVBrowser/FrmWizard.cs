using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Crazysoft.OTRRemote
{
    public partial class FrmWizard : Form
    {
        private int curWizardPage = 1;

        public FrmWizard()
        {
            InitializeComponent();

            this.Text = Lang.TVBrowser.FrmWizard_Title;

            foreach (Control ctl in this.Controls)
            {
                TranslateControl(ctl);
            }
        }

        private void TranslateControl(Control ctl)
        {
            if (ctl.GetType() == typeof(PictureBox))
            {
                PictureBox pb = (PictureBox)ctl;
                pb.Image = (Image)Lang.TVBrowser.ResourceManager.GetObject("FrmWizard_" + ctl.Name);
            }
            else
            {
                ctl.Text = Lang.TVBrowser.ResourceManager.GetString("FrmWizard_" + ctl.Name);
            }

            foreach (Control subCtl in ctl.Controls)
            {
                TranslateControl(subCtl);
            }
        }

        private void FrmWizard_Load(object sender, EventArgs e)
        {
            pnlPage2.Hide();
            pnlPage3.Hide();
            pnlPage4.Hide();
            pnlPage5.Hide();
            pnlPage6.Hide();
            pnlPage7.Hide();

            // Set the path to OTR Remote
            tbPage3Path.Text = System.Reflection.Assembly.GetEntryAssembly().Location;

            // If the program is running on the Mono Framework, add "mono" before the path
            if (Type.GetType("Mono.Runtime") != null)
            {
                tbPage3Path.Text = String.Concat("mono ", tbPage3Path.Text);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (curWizardPage)
            {
                case 1:
                    lblHeader.Text = Lang.TVBrowser.FrmWizard_lblHeader_Step2;
                    pnlPage2.Show();
                    btnPrevious.Enabled = true;
                    break;
                case 2:
                    lblHeader.Text = Lang.TVBrowser.FrmWizard_lblHeader_Step3;
                    pnlPage3.Show();
                    break;
                case 3:
                    lblHeader.Text = Lang.TVBrowser.FrmWizard_lblHeader_Step4;
                    pnlPage4.Show();
                    break;
                case 4:
                    lblHeader.Text = Lang.TVBrowser.FrmWizard_lblHeader_Step5;
                    pnlPage5.Show();
                    break;
                case 5:
                    lblHeader.Text = Lang.TVBrowser.FrmWizard_lblHeader_Step6;
                    pnlPage6.Show();
                    break;
                case 6:
                    lblHeader.Text = Lang.TVBrowser.FrmWizard_lblHeader_Step7;
                    pnlPage7.Show();
                    btnNext.Enabled = false;
                    break;
                default:
                    return;
            }

            curWizardPage++;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            switch (curWizardPage)
            {
                case 2:
                    lblHeader.Text = Lang.TVBrowser.FrmWizard_lblHeader;
                    pnlPage2.Hide();
                    btnPrevious.Enabled = false;
                    break;
                case 3:
                    lblHeader.Text = Lang.TVBrowser.FrmWizard_lblHeader_Step2;
                    pnlPage3.Hide();
                    break;
                case 4:
                    lblHeader.Text = Lang.TVBrowser.FrmWizard_lblHeader_Step3;
                    pnlPage4.Hide();
                    break;
                case 5:
                    lblHeader.Text = Lang.TVBrowser.FrmWizard_lblHeader_Step4;
                    pnlPage5.Hide();
                    break;
                case 6:
                    lblHeader.Text = Lang.TVBrowser.FrmWizard_lblHeader_Step5;
                    pnlPage6.Hide();
                    break;
                case 7:
                    lblHeader.Text = Lang.TVBrowser.FrmWizard_lblHeader_Step6;
                    pnlPage7.Hide();
                    btnNext.Enabled = true;
                    break;
                default:
                    return;
            }

            curWizardPage--;
        }
    }
}