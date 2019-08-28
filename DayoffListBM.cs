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
    public partial class DayoffListBM : Window
    {
        private DayOffManager _dayoffmanager = new DayOffManager();
        private UserManager _usermanager = new UserManager();
        List<dayOffs> dayoffList;
        List<users> userlist = new List<users>();



        public DayoffListBM()
        {
            InitializeComponent();
            this.SizeToContent = SizeToContent.WidthAndHeight;
            loadDayoff();
            lvDayoff.Loaded += LvDayoff_Loaded;

        }

        private void LvDayoff_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public void loadDayoff()
        {
            dayoffList = _dayoffmanager.DayoffList();

            foreach (var x in dayoffList)
            {

                users user = _usermanager.GetUser(Convert.ToDecimal(x.userID));

                userlist.Add(user);

            }
            for (int index = 0; index < dayoffList.Count; index++)
            {
                dayoffList[index].user = _usermanager.GetUser(Convert.ToDecimal(dayoffList[index].userID));
            }

            lvDayoff.ItemsSource = dayoffList;

        }
        public void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
