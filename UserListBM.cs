using Mission;
using Mission.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mission.BM
{
    public partial class UserListBM : Window
    {

        private UserManager _userManager = new UserManager();
        private DepartmentManager _deptManager = new DepartmentManager();
        List<users> userList;
        List<departments> deptList = new List<departments>();
        private AddnewBM addnew;
        public UpdateBM update;

        private users _selectedUser;

        public users User
        {
            get { return _selectedUser; }
            set { _selectedUser = value; }
        }

        private string _name;
        private string _lastname;
        private string _username;

        public string Names
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Lastname
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public UserListBM()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.WidthAndHeight;
            lvUsers.MouseDoubleClick += new MouseButtonEventHandler(MyListView_MouseDoubleClick);
            loadUsers();

        }

        public void loadUsers()
        {
            userList = _userManager.UserList();

            foreach (var x in userList)
            {
                departments dept = _deptManager.GetDept(x.departmantId);
                deptList.Add(dept);

            }

            for (int index = 0; index < userList.Count; index++)
            {
                userList[index].deptsName = _deptManager.GetDept(Convert.ToDecimal(userList[index].departmantId));
            }

            lvUsers.ItemsSource = userList;
        }

        public void MyListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            users item = (users)lvUsers.ItemContainerGenerator.ItemFromContainer(dep); //?
            _selectedUser = item;

            this.Hide();
            update = new UpdateBM();
            update.loadUser(_selectedUser);
            update.UserUpdated += UpdateForm_UserUpdated;
            update.ShowDialog();
            this.ShowDialog();

        }


        public void btnAddnew_Click(object sender, EventArgs e)
        {
            this.Hide();
            addnew = new AddnewBM();
            addnew.UserUpdated += AddForm_UserUpdated;
            addnew.ShowDialog();
            this.ShowDialog();

        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateForm_UserUpdated(object sender, EventArgs e)
        {
            loadUsers();
        }

        private void AddForm_UserUpdated(object sender, EventArgs e)
        {
            loadUsers();
        }

    }

}
