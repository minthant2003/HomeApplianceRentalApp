using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeApplianceRentalApplication
{
    public partial class frmUserType : Form
    {
        public frmUserType()
        {
            InitializeComponent();
        }

        private void cmdAdmin_Click(object sender, EventArgs e)
        {
            frmAdminSignIn frmAdmin = new frmAdminSignIn();
            this.Hide();
            frmAdmin.Show();
        }

        private void cmdCustomer_Click(object sender, EventArgs e)
        {
            frmCustomerSignIn frmCustomer = new frmCustomerSignIn(null);
            this.Hide();
            frmCustomer.Show();
        }
    }
}
