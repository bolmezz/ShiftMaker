using Mission;
using Mission.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Mission.BM
{
    public partial class SelectDayBM : Window
    {

        private DayOffManager _dayoffmanager = new DayOffManager();
        private DemandDayManager _demandmanager = new DemandDayManager();

        List<DateTime> demands = new List<DateTime>();
        private users _user;

        public SelectDayBM()
        {
            InitializeComponent();
            blockedDays();
        }

        public void getUser(users user)
        {
            _user = user;
            userDemands();
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

        public void userDemands()
        {
            List<demandDays> days = _demandmanager.GetDemandDays(_user.id);

            for (int i = 0; i < days.Count; i++)
            {
                demands.Add(days[i].dateT.Value);
            }

        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            //string day1 = calendar1.SelectedDate.ToString().Substring(0, calendar1.SelectedDate.ToString().IndexOf(" "));


            if (calendar1.SelectedDate != null)
            {
                string day1 = calendar1.SelectedDate.Value.ToShortDateString();
                DateTime day = calendar1.SelectedDate.Value;

                if (txt_firstdate.Text == "" && demands.Contains(day) == false)
                {
                    txt_firstdate.Text = day1;
                }
                else if(demands.Contains(day) == false)
                {
                    txt_secondate.Text = day1;
                }
                else
                {
                    MessageBox.Show("Nöbet istek gününüzü seçemezsiniz");
                    this.Show();
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

            if (txt_firstdate.Text != "" && txt_secondate.Text == "")
            {
                _dayoffmanager.AssignDayoffs(txt_firstdate.Text, null, _user);

                MessageBox.Show("" + txt_firstdate.Text + " assigned to " + _user.name + " " + _user.lastname);
                this.Close();
            }
            else if (txt_firstdate.Text != "" && txt_secondate.Text != "")
            {
                _dayoffmanager.AssignDayoffs(txt_firstdate.Text, txt_secondate.Text, _user);

                MessageBox.Show("" + txt_firstdate.Text + " - " + txt_secondate.Text + " assigned to " + _user.name + " " + _user.lastname);
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
