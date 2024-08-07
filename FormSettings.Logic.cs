using System;
using System.Windows.Forms;

namespace ListMaster
{
    public partial class FormSettings : Form
    {
        private void tbFullname_TextChanged(object sender, EventArgs e)
        {
            if (tbFullname.Text != "0")
            {
                DisableFIO();
            }
            else
            {
                EnableFIO();
            }
        }

        private void tbDocumentSeriesNumber_TextChanged(object sender, EventArgs e)
        {
            if (tbDocumentSeriesNumber.Text != "0")
            {
                DisableDocSN();
                DisableFIO();
                DisableFullname();
                DisableBDate();
            }
            else
            {
                EnableDocSN();
                EnableFIO();
                EnableFullname();
                EnableBDate();
            }
        }



        private void tbLastname_TextChanged(object sender, EventArgs e)
        {
            if (tbLastname.Text != "0")
            {
                DisableFullname();
            }
            else
            {
                if (tbFirstname.Text == "0" && tbOthername.Text == "0")
                {
                    EnableFullname(); 
                }
            }
        }

        private void tbFirstname_TextChanged(object sender, EventArgs e)
        {
            if (tbFirstname.Text != "0")
            {
                DisableFullname();
            }
            else
            {
                if (tbLastname.Text == "0" && tbOthername.Text == "0")
                {
                    EnableFullname(); 
                }
            }
        }

        private void tbOthername_TextChanged(object sender, EventArgs e)
        {
            if (tbOthername.Text != "0")
            {
                DisableFullname();
            }
            else
            {
                if (tbLastname.Text == "0" && tbFirstname.Text == "0")
                {
                    EnableFullname(); 
                }
            }
        }
        private void tbDocumentSeries_TextChanged(object sender, EventArgs e)
        {
            if (tbDocumentSeries.Text != "0")
            {
                DisableDocWhole();
            }
            else
            {
                if (tbDocumentNumber.Text == "0")
                {
                    EnableDocWhole();
                }
            }
        }

        private void tbDocumentNumber_TextChanged(object sender, EventArgs e)
        {
            if (tbDocumentNumber.Text != "0")
            {
                DisableDocWhole();
            }
            else
            {
                if (tbDocumentSeries.Text == "0")
                {
                    EnableDocWhole();
                }
            }
        }

        private void DisableFullname()
        {
            tbFullname.Enabled = false;
            tbFullname.Text = "0";
        }

        private void DisableDocWhole()
        {
            tbDocumentSeriesNumber.Enabled = false;
            tbDocumentSeriesNumber.Text = "0";
        }

        private void DisableBDate()
        {
            tbBdate.Enabled = false;
            tbBdate.Text = "0";
        }

        private void DisableDocSN()
        {
            tbDocumentSeries.Enabled = false;
            tbDocumentSeries.Text = "0";
            tbDocumentNumber.Enabled = false;
            tbDocumentNumber.Text = "0";
        }

        private void DisableFIO()
        {
            tbLastname.Enabled = false;
            tbLastname.Text = "0";
            tbFirstname.Enabled = false;
            tbFirstname.Text = "0";
            tbOthername.Enabled = false;
            tbOthername.Text = "0";
        }

        private void EnableFIO()
        {
            tbLastname.Enabled = true;
            tbFirstname.Enabled = true;
            tbOthername.Enabled = true;
        }

        private void EnableDocSN()
        {
            tbDocumentSeries.Enabled = true;
            tbDocumentNumber.Enabled = true;
        }

        private void EnableFullname()
        {
            tbFullname.Enabled = true;
        }

        private void EnableBDate()
        {
            tbBdate.Enabled = true;
        }

        private void EnableDocWhole()
        {
            tbDocumentSeriesNumber.Enabled = true;
        }
    }
}
