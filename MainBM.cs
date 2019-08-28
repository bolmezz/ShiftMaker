using Mission;
using Mission.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Mission.BM
{
    public partial class MainBM : Window
    {

        private UserManager _userManager = new UserManager();
        private DaysManager _daysManager = new DaysManager();
        public UserListBM userList;
        public PassChangeBM passChange;
        public LoginBM logView;
        public DayoffListBM dayofflist;
        public SelectDayBM selectDayoff;
        public CreateListBM listView;
        public ReqDaysBM reqdayview;
        public SelectDemandDayBM selectDemand;
    
        private users _activeUser;
        public string username;
        public string pass;

        /* public users ActiveUser
         {
             get { return _activeUser; }
             set { _activeUser = value; }
         }*/

        public MainBM()
        {
            InitializeComponent();
            
            SetForLogin();
        }

        private void SetForLogin()
        {
            this.Hide();
            logView = new LoginBM();
            logView.Closing += LoginClosing;
            logView.ShowDialog();
            _activeUser = logView.ActiveUser;
            if (_activeUser != null)
            {
                txt_user.Text = "   " + _activeUser.name.Substring(0, 1).ToUpper() + _activeUser.name.Substring(1) + " " + _activeUser.lastname.ToUpper();
            }
            this.ShowDialog();
        }

        private void LoginClosing(/*object sender, ConsoleCancelEventArgs e*/object sender, System.ComponentModel.CancelEventArgs e)
        {
            _activeUser = logView.ActiveUser;
            if (_activeUser == null)
            {
                /*System.Windows.Application.Current.Shutdown();
                this.Close();*/

                typeof(Window).GetField("_isClosing", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this, false);
                e.Cancel = true;
                this.Hide();
            }
        }

        public void btnUsers_Click(object sender, EventArgs e)
        {
            this.Hide();
            userList = new UserListBM();
            userList.ShowDialog();
            this.ShowDialog();
        }

        public void btnChangePass_Click(object sender, EventArgs e)
        {
            this.Hide();
            passChange = new PassChangeBM();
            passChange.getUser(_activeUser);
            passChange.UserUpdated += ChangeForm_UserUpdated;
            passChange.ShowDialog();
            this.ShowDialog();
        }

        private void ChangeForm_UserUpdated(object sender, EventArgs e)
        {
            _activeUser = passChange.User;
        }

        public void btnDayoff_Click(object sender, EventArgs e)
        {
            this.Hide();
            selectDayoff = new SelectDayBM();
            selectDayoff.getUser(_activeUser);
            selectDayoff.ShowDialog();
            this.ShowDialog();
        }

        public void btnDemandDay_Click(object sender, EventArgs e)
        {
            this.Hide();
            selectDemand = new SelectDemandDayBM();
            selectDemand.getUser(_activeUser);
            selectDemand.ShowDialog();
            this.ShowDialog();
        }

        public void btnListDayoff_Click(object sender, EventArgs e)
        {
            this.Hide();
            dayofflist = new DayoffListBM();
            dayofflist.ShowDialog();
            this.ShowDialog();
        }

        public void btnList_Click(object sender, EventArgs e)
        {
            this.Hide();
            listView = new CreateListBM();
            listView.ShowDialog();
            this.ShowDialog();

        }

        public void btnReqdays_Click(object sender, EventArgs e)
        {
            this.Hide();
            reqdayview = new ReqDaysBM();
            reqdayview.ShowDialog();
            this.ShowDialog();
        }

        public void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
            this.Hide();
              
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            typeof(Window).GetField("_isClosing", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(this, false);
            e.Cancel = true;
            this.Hide();
        }

    }
}
