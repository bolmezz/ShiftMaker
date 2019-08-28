using Mission;
using Mission.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Mission.BM
{
    public partial class LoginBM : Window
    {
        private UserManager _userManager = new UserManager();
        //public MainBM main = new MainBM();

        private users _activeUser;
        public string username;
        public string pass;



        public LoginBM()
        {
            InitializeComponent();

        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }


        public users ActiveUser
        {
            get { return _activeUser; }
            set { _activeUser = value; }
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            username = txt_username.Text;
            pass = txt_pass.Password;

            _activeUser = _userManager.LoginCheck(username, pass);
            if (_activeUser == null)
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı", "Hatalı Giriş");
                txt_pass.Clear();
                txt_username.Clear();
            }
            else
            {
                this.Close();
            }

        }

   
        public void btnCancel_Click(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            this.Hide();
           // this.Close();
           
        }
    }
}