using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeApplianceRentalApplication
{
    public partial class frmCustomerHome : Form
    {
        //Datagridview values for selected row during adding to shopping cart
        DataGridViewRow row; 
        string selectedBrand, selectedModel;
        decimal itemFee = 1;

        decimal period; //For Numeric Up Down control value (Only decimal allowed)
        decimal item1Fee, item2Fee, item3Fee; //For calculation (itemFee * period)
        int times = 0; //For shopping cart's fullness condition

        int timesCal = 0; //For only one time calculation
        Boolean cmdCalClick = false; //For checking if the total fee is calculated before ordering

        public frmCustomerHome()
        {
            InitializeComponent();
        }

        //Function for checking if combo box or row selected is empty
        private Boolean Check()
        {
            if (cmbBoxCustomer.SelectedItem != null)
            {
                if (row != null)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Please select the Desired Row first, and Add to the Shopping Cart.");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Please choose an Appliance Type from Combo Box.");
                return false;
            }
        }

        private void cmbBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Displaying minimum rental contract of each type in label control and numericUpDown control
            if (cmbBoxCustomer.Text == "TV")
            {
                lblContract.Text = "Minimum Rental Contract of " + cmbBoxCustomer.Text + " : 2 months";
                nmUpDown.Minimum = 2;
                nmUpDown.Value = 2;
            }
            else if (cmbBoxCustomer.Text == "Fridge")
            {
                lblContract.Text = "Minimum Rental Contract of " + cmbBoxCustomer.Text + " : 4 months";
                nmUpDown.Minimum = 4;
                nmUpDown.Value = 4;
            }
            else if (cmbBoxCustomer.Text == "WashMachine")
            {
                lblContract.Text = "Minimum Rental Contract of " + cmbBoxCustomer.Text + " : 3 months";
                nmUpDown.Minimum = 3;
                nmUpDown.Value = 3;
            }
            else if (cmbBoxCustomer.Text == "Dryer")
            {
                lblContract.Text = "Minimum Rental Contract of " + cmbBoxCustomer.Text + " : 6 months";
                nmUpDown.Minimum = 6;
                nmUpDown.Value = 6;
            }
            else
            {
                lblContract.Text = "Minimum Rental Contract of " + cmbBoxCustomer.Text + " : 5 months";
                nmUpDown.Minimum = 5;
                nmUpDown.Value = 5;
            }

            //Displaying items
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Appliances.mdb");
            con.Open();

            OleDbCommand command = new OleDbCommand("SELECT * FROM " + cmbBoxCustomer.Text , con);
            OleDbDataAdapter ad = new OleDbDataAdapter(command);
            DataTable table = new DataTable();
            ad.Fill(table);
            dtGridView.DataSource = table;

            con.Close();
        }

        private void dtGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            row = dtGridView.Rows[e.RowIndex];
            selectedBrand = row.Cells[1].Value.ToString();
            selectedModel = row.Cells[2].Value.ToString();
            itemFee = decimal.Parse(row.Cells[6].Value.ToString());
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            Boolean rt = Check();            
                                     
            if (rt == true)
            {
                if (times < 3)
                {
                    period = nmUpDown.Value;

                    //Calculation of total rental cost of each item, and displaying in the shopping cart
                    if (times == 0)
                    {
                        item1Fee = itemFee * period;
                        lblOutput.Text += "\n\n" + "1. " + cmbBoxCustomer.Text + " \t " + selectedBrand + " \t " + selectedModel + " \t " + period.ToString() + "months " + " \t " + "\u00A3" + item1Fee.ToString();
                    }
                    else if (times == 1)
                    {
                        item2Fee = itemFee * period;
                        lblOutput.Text += "\n" + "2. " + cmbBoxCustomer.Text + " \t " + selectedBrand + " \t " + selectedModel + " \t " + period.ToString() + "months " + " \t " + "\u00A3" + item2Fee.ToString();
                    }
                    else
                    {
                        item3Fee = itemFee * period;
                        lblOutput.Text += "\n" + "3. " + cmbBoxCustomer.Text + " \t " + selectedBrand + " \t " + selectedModel + " \t " + period.ToString() + "months " + " \t " + "\u00A3" + item3Fee.ToString();
                    }

                    times++;
                }
                else
                {
                    MessageBox.Show("Shopping Cart is full.");
                    return;
                }
            }
        }

        private void cmdCalculate_Click(object sender, EventArgs e)
        {            
            Boolean rt = Check();

            if (timesCal == 0)
            {
                if (rt == true)
                {
                    lblOutput.Text += "\n\n" + "Total Rental Fee is: " + "\u00A3" + (item1Fee + item2Fee + item3Fee).ToString();
                    cmdCalClick = true;
                    timesCal++;
                }
            }
        }

        private void cmdOrder_Click(object sender, EventArgs e)
        {
            Boolean rt = Check();

            if (rt == true)
            {
                if (cmdCalClick == true)
                {
                    DialogResult result = MessageBox.Show("Feel confident with what you have chosen?", "Confirmation", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        MessageBox.Show("Thank you for choosing our services");
                        lblOutput.Text = "";
                        lblOutput.Text = "Selected appliances will appear here";
                        times = 0;
                        timesCal = 0;
                        cmdCalClick = false;
                    }
                }
                else
                {
                    MessageBox.Show("Please Calculate the Total Fee first.");
                    return;
                }
            }
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            Boolean rt = Check();

            if (rt == true)
            {
                lblOutput.Text = "";
                lblOutput.Text = "Selected appliances will appear here";
                times = 0;
                timesCal = 0;
                cmdCalClick = false;
            }
        }

        private void lnkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmCustomerSignIn frmCustomerSignIn = new frmCustomerSignIn(null);
            this.Hide();
            frmCustomerSignIn.Show();
        }
    }
}
