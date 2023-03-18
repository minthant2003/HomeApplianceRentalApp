using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplianceRentalApplication
{
    public class Account //To be consistent with CustomerAccount child class
    {
        private string username;
        private string password;

        public Account(string un,string pass)
        {
            username = un;
            password = pass;
        }

        public String Username
        {
            get { return username; }
            set { username = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        public Boolean CheckUname(string u)
        {
            if (Username == u)
            {
                return true;
            }
            return false;
        }

        public Boolean CheckPass(string p)
        {
            if (Password == p)
            {
                return true;
            }
            return false;
        }
    }

    public class CustomerAccount : Account //To be consistent with customer sign in form
    {
        public CustomerAccount(string un, string pass) : base(un, pass)
        {

        }
    }

    class AdministratorAccount : Account
    {        
        public AdministratorAccount(string un,string pass) : base(un, pass)
        {

        }
    }
}
