using Jarloo.Calendar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Mission.GUI
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class CalendarWin : Window
    {
        missionTimeEntities missionEntities = new missionTimeEntities();
        public CalendarWin()
        {
            InitializeComponent();

            List<string> months = new List<string>(); // { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            for (int i = 0; i < 12; i++)
            {
                months.Add(new DateTime(2010, i + 1, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("tr")));
            }
            cboMonth.ItemsSource = months;
            cboMonth.SelectedIndex = DateTime.Now.Month - 1;

            for (int i = -50; i < 50; i++)
            {
                cboYear.Items.Add(DateTime.Today.AddYears(i).Year);
            }

            List<users> userList = missionEntities.users.ToList();
            cboNames.Items.Add("Genel");
            for (int i = 0; i < userList.Count; i++)
            {
                cboNames.Items.Add(userList[i].name + "_" + userList[i].lastname);
            }
            

            cboMonth.SelectedItem = months.FirstOrDefault(w => w == DateTime.Today.ToString("MMMM"));
            cboYear.SelectedItem = DateTime.Today.Year;
            cboNames.SelectedItem = "Genel";

            cboMonth.SelectionChanged += (o, e) => RefreshCalendar();
            cboYear.SelectionChanged += (o, e) => RefreshCalendar();
            cboNames.SelectionChanged += (o, e) => RefreshCalendar();
        }

        private void RefreshCalendar()
        {
            if (cboYear.SelectedItem == null) return;
            if (cboMonth.SelectedItem == null) return;

            int year = (int)cboYear.SelectedItem;

            int month = cboMonth.SelectedIndex + 1;

            string name = (string)cboNames.SelectedItem;

            DateTime targetDate = new DateTime(year, month, 1);

            if (cboNames == null || (string)cboNames.SelectedItem == "Genel")
            {
                Calendar.BuildCalendar(targetDate);
            }
            else
            {
                Calendar.BuildCalendarUser(targetDate, name);
            }

        }

        private void Calendar_DayChanged(object sender, DayChangedEventArgs e)
        {
            //save the text edits to persistant storage
        }


    }
}
