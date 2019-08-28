using Mission;
using Mission.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mission.BM
{
    public partial class PassChangeBM : Window
    {
        private UserManager _userManager = new UserManager();
        private users _user;
        public event EventHandler UserUpdated;

        public users User
        {
            get { return _user; }
            set { _user = value; }
        }

        public PassChangeBM()
        {
            InitializeComponent();

        }

        public void getUser(users user)
        {
            _user = user;
        }


        public void btnSave_Click(object sender, EventArgs e)
        {
            string oldPass = txt_oldpass.Text.ToString();
            string newPass = txt_newpass.Password.ToString();
            string newPassR = txt_newpass2.Password.ToString();


            if (oldPass != null && newPass != "" && newPassR != "")
            {
                users change = _userManager.ChangePass(oldPass, newPass, newPassR, _user);

                if (change != null)
                {
                    MessageBox.Show(this, "Şifreniz değiştirildi", "Bilgi");
                    _user = change;
                    EventArgs ev = new EventArgs();
                    UserUpdated.Invoke(this, ev);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(this, "Hatalı işlem", "Bilgi");
                    txt_oldpass.Clear();
                    txt_newpass.Clear();
                    txt_newpass2.Clear();
                }

            }
            else
            {
                MessageBox.Show(this, "Alanlar boş bırakılamaz", "Bilgi");
                txt_oldpass.Clear();
                txt_newpass.Clear();
                txt_newpass2.Clear();
                this.Show();
            }


        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

    }
}
