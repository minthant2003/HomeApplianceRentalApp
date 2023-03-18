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
    public partial class frmAdminHome : Form
    {
        public frmAdminHome()
        {
            InitializeComponent();
        }        

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Appliances.mdb");
                con.Open();

                OleDbCommand command = new OleDbCommand("SELECT * FROM " + cmbBoxAdmin.Text, con);
                OleDbDataAdapter ad = new OleDbDataAdapter(command);
                DataTable table = new DataTable();
                ad.Fill(table);

                dtGridView.DataSource = table;
                con.Close();

                txtBoxID.Text = "";
                txtBoxBrand.Text = "";
                txtBoxModel.Text = "";
                txtBoxDim.Text = "";
                txtBoxColour.Text = "";
                txtBoxEnergy.Text = "";
                txtBoxFee.Text = "";
            }            
            catch
            {
                MessageBox.Show("Please choose an Appliance Type from Combo Box.");
                return;
            }            

            //Renaming the columns' name
            dtGridView.Columns[0].HeaderText = "ID";
            dtGridView.Columns[1].HeaderText = "BRAND";
            dtGridView.Columns[2].HeaderText = "MODEL";
            dtGridView.Columns[3].HeaderText = "DIMENSIONS";
            dtGridView.Columns[4].HeaderText = "COLOUR";
            dtGridView.Columns[5].HeaderText = "ENERGY CONSUMPTION";
            dtGridView.Columns[6].HeaderText = "MONTHLY RENTAL FEE IN POUNDS";
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtBoxID.Text = "";
            txtBoxBrand.Text = "";
            txtBoxModel.Text = "";
            txtBoxDim.Text = "";
            txtBoxColour.Text = "";
            txtBoxEnergy.Text = "";
            txtBoxFee.Text = "";
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (cmbBoxAdmin.SelectedItem != null)
            {
                try
                {
                    OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Appliances.mdb");
                    con.Open();

                    OleDbCommand command = new OleDbCommand("INSERT INTO " + cmbBoxAdmin.Text + " VALUES (@ID,@Brand,@Model,@Dimensions,@Colour,@EnergyCon,@MonthlyFee)", con);
                    command.Parameters.AddWithValue("@ID", int.Parse(txtBoxID.Text.ToString()));
                    command.Parameters.AddWithValue("@Brand", txtBoxBrand.Text.ToString());
                    command.Parameters.AddWithValue("@Model", txtBoxModel.Text.ToString());
                    command.Parameters.AddWithValue("@Dimensions", txtBoxDim.Text.ToString());
                    command.Parameters.AddWithValue("@Colour", txtBoxColour.Text.ToString());
                    command.Parameters.AddWithValue("@EnergyCon", txtBoxEnergy.Text.ToString());
                    command.Parameters.AddWithValue("@MonthlyFee", txtBoxFee.Text.ToString());
                    command.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("Please re-load to view the added values");

                    txtBoxID.Text = "";
                    txtBoxBrand.Text = "";
                    txtBoxModel.Text = "";
                    txtBoxDim.Text = "";
                    txtBoxColour.Text = "";
                    txtBoxEnergy.Text = "";
                    txtBoxFee.Text = "";
                }
                catch
                {
                    MessageBox.Show("Please enter the values into the text boxes to be added.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please Choose an Appliance Type from Combo Box.");
                return;
            }
        }

        private void lnkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAdminSignIn frmAdminSignIn = new frmAdminSignIn();
            this.Hide();
            frmAdminSignIn.Show();
        }

        private void dtGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Making selected row values appeared in Text Boxes
            DataGridViewRow row = dtGridView.Rows[e.RowIndex];

            txtBoxID.Text = row.Cells[0].Value.ToString();
            txtBoxBrand.Text = row.Cells[1].Value.ToString();
            txtBoxModel.Text = row.Cells[2].Value.ToString();
            txtBoxDim.Text = row.Cells[3].Value.ToString();
            txtBoxColour.Text = row.Cells[4].Value.ToString();
            txtBoxEnergy.Text = row.Cells[5].Value.ToString();
            txtBoxFee.Text = row.Cells[6].Value.ToString();
        }

        private void cmdEdit_Click(object sender, EventArgs e)
        {
            if (cmbBoxAdmin.SelectedItem != null)
            {
                try
                {
                    OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Appliances.mdb");
                    con.Open();

                    OleDbCommand command = new OleDbCommand("UPDATE " + cmbBoxAdmin.Text + " SET Brand='" + txtBoxBrand.Text + "', Model='" + txtBoxModel.Text + "', Dimensions='" + txtBoxDim.Text + "', Colour='" + txtBoxColour.Text + "', EnergyCon='" + txtBoxEnergy.Text + "', MonthlyFee='" + txtBoxFee.Text + "' WHERE ID=@ID", con);
                    command.Parameters.AddWithValue("@ID", int.Parse(txtBoxID.Text));
                    command.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("Please re-reload to view the edited values.");

                    txtBoxID.Text = "";
                    txtBoxBrand.Text = "";
                    txtBoxModel.Text = "";
                    txtBoxDim.Text = "";
                    txtBoxColour.Text = "";
                    txtBoxEnergy.Text = "";
                    txtBoxFee.Text = "";
                }
                catch
                {
                    MessageBox.Show("Please select the row, and edit the desired values using Text Boxes.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please Choose an Appliance Type from Combo Box.");
                return;
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (cmbBoxAdmin.SelectedItem != null)
            {
                try
                {
                    OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Appliances.mdb");
                    con.Open();

                    OleDbCommand command = new OleDbCommand("DELETE FROM " + cmbBoxAdmin.Text + " WHERE ID=@ID", con);
                    command.Parameters.AddWithValue("@ID", int.Parse(txtBoxID.Text));
                    command.ExecuteNonQuery();

                    con.Close();

                    MessageBox.Show("The selected row has been deleted. Please re-load again.");

                    txtBoxID.Text = "";
                    txtBoxBrand.Text = "";
                    txtBoxModel.Text = "";
                    txtBoxDim.Text = "";
                    txtBoxColour.Text = "";
                    txtBoxEnergy.Text = "";
                    txtBoxFee.Text = "";
                }
                catch
                {
                    MessageBox.Show("Please select the desired row to be deleted.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please Choose an Appliance Type from Combo Box.");
                return;
            }
        }
    }
}
