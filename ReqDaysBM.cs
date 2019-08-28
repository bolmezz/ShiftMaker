using Mission.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Mission.BM
{
    public partial class ReqDaysBM : Window
    {

        private DaysManager _daysmanager = new DaysManager();
        public ReqDaysBM()
        {
            InitializeComponent();

          //  blockedDays();
        }

        public void blockedDays()
        {
            var currentYear = DateTime.Now.Year;
            var currentMonth = DateTime.Now.Month;
            var nextMonth = DateTime.Now.Month;

          /*  if (currentMonth < 12)
            {
                nextMonth = currentMonth;//+1
            }
            else
            {
                currentYear += 1;
                nextMonth = 1;
            }*/

            int firstDayInNextMonth = 1;
            int lastDayInNextMonth = DateTime.DaysInMonth(currentYear, currentMonth);//nextMonth

            DateTime startDate = new DateTime(currentYear, currentMonth, firstDayInNextMonth);//nextMonth
            DateTime endDate = new DateTime(currentYear, currentMonth, lastDayInNextMonth);//nextMonth

            calendar1.DisplayDateStart = startDate;
            calendar1.DisplayDateEnd = endDate;

        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            //string day1 = calendar1.SelectedDate.ToString().Substring(0, calendar1.SelectedDate.ToString().IndexOf(" "));

            if (calendar1.SelectedDate != null)
            {
                string day1 = calendar1.SelectedDate.Value.ToShortDateString();

                if (txt_firstdate.Text == "")
                {
                    txt_firstdate.Text = day1;
                }
                else
                {
                    txt_secondate.Text = day1;
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
                _daysmanager.AssignReqDay(txt_firstdate.Text, null);

                MessageBox.Show("" + txt_firstdate.Text);
                this.Close();
            }
            else if (txt_firstdate.Text != "" && txt_secondate.Text != "")
            {
                _daysmanager.AssignReqDay(txt_firstdate.Text, txt_secondate.Text);

                MessageBox.Show("" + txt_firstdate.Text + " - " + txt_secondate.Text);
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
