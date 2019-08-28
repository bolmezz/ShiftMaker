using Mission.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mission.BM
{
    public partial class SelectDemandDayBM : Window
    {
        private DemandDayManager _demandmanager = new DemandDayManager();
        private DayOffManager _dayoffmanager = new DayOffManager();
        private users _user;
        List<DateTime> offdays = new List<DateTime>();

        public SelectDemandDayBM()
        {
            InitializeComponent();

            blockedDays();
            
        }

        public void getUser(users user)
        {
            _user = user;
            userDayoffs();
        }

        public void blockedDays()
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var nextMonth = DateTime.Now.Month;


            if (currentMonth < 12)
            {
                nextMonth = currentMonth + 1;
            }
            else
            {
                currentYear += 1;
                nextMonth = 1;
            }

            int firstDayInNextMonth = 1;
            int lastDayInNextMonth = DateTime.DaysInMonth(currentYear, nextMonth);

            DateTime startDate = new DateTime(currentYear, nextMonth, firstDayInNextMonth);
            DateTime endDate = new DateTime(currentYear, nextMonth, lastDayInNextMonth);

            calendar1.DisplayDateStart = startDate;
            calendar1.DisplayDateEnd = endDate;

        }

        public void userDayoffs()
        {
            List<dayOffs> days = _dayoffmanager.GetDayoff(_user.id);
        
            for (int i = 0; i < days.Count; i++)
            {
                offdays.Add(days[i].dateT.Value);
            }

        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
       
            if (calendar1.SelectedDate != null)
            {
                string day1 = calendar1.SelectedDate.Value.ToShortDateString();
                DateTime day = calendar1.SelectedDate.Value;

                if (list_days.Items.Count == 0 && offdays.Contains(day) == false)
                {
                    list_days.Items.Add(day1);
                }
                else
                {
                    if (list_days.Items.Contains(day1) == false && offdays.Contains(day) == false)
                    {
                        list_days.Items.Add(day1);
                    }
                    else
                    {
                        MessageBox.Show("İzin gününüzü veya daha önceden seçtiğiniz günü seçemezsiniz");
                        this.Show();
                    }

                }

            }
            else
            {
                MessageBox.Show("Gün seçin");
                this.Show();
            }

        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            if (list_days != null)
            {
                 foreach (var x in list_days.Items)
                 {

                    // _demandmanager.AssignDays(x.ToString(), _user);
                    list.Add(x.ToString());

                 }

                _demandmanager.AssignDays(list , _user);

                this.Close();
            }

            else
            {
                MessageBox.Show("Gün seçin");
                this.Show();
            }
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }











    }
}
