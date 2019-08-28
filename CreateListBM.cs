using Jarloo.Calendar;
using Mission.CalculationManager;
using Mission.GUI;
using Mission.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Mission.BM
{
    public partial class CreateListBM : Window
    {
        private UserManager _usermanager = new UserManager();
        private DayOffManager _dayoffmanager = new DayOffManager();
        private MissionsManager _missionmanager = new MissionsManager();
        missionTimeEntities entities = new missionTimeEntities();
        private CalculationMain _cal;
        private CalculationMain _cal2;

        public List<users> userlist;
        public List<missions> missionList;

        public CreateListBM()
        {
            InitializeComponent();
            // loadDefault();//tüm nöbet listelerini getir
        }

        private void load_months(object sender, RoutedEventArgs e)
        {
            List<string> months = new List<string>();
            for (int i = 0; i < 12; i++)
            {
                months.Add(new DateTime(2010, i + 1, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("tr")));
            }

            foreach (var x in months)
            {
                combo_months.Items.Add(x);
            }
            combo_months.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void LvMissions_Loaded(object sender, RoutedEventArgs e)
        {
        }
      

        public void btnList_Click(object sender, EventArgs e)//nöbet listesi oluşturma
        {
            string month = "";
            if (combo_months.SelectedIndex == -1)
            {
                MessageBox.Show(this, " Nöbet listesi için ay seçiniz", "Bilgi");
                return;
            }
            else
            {
                month = combo_months.Items[combo_months.SelectedIndex].ToString();
            }

            wait waiter = new wait();

            int slc_month = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;
            if (_missionmanager.MissionListCheck(slc_month))//seçilen ay için önceden oluşturulmuş liste varsa onu göster
            {
                loadMissions(month);
            }
            else
            {
                _cal = new CalculationMain(1, month);
                Thread primerThread = new Thread(_cal.CalculatePrimer);

                Stopwatch watch = new Stopwatch();
                watch.Start();
                // _cal.CalculatePrimer(); 
                primerThread.IsBackground = true;
                primerThread.Start();
                this.IsEnabled = false;
                waiter.Show();
                primerThread.Join();

                _cal2 = new CalculationMain(2, month);
                Thread seconderThread = new Thread(_cal2.CalculatePrimer);
                seconderThread.IsBackground = true;
                seconderThread.Start();
                seconderThread.Join();       
                waiter.Close();
                this.IsEnabled = true;

                watch.Stop();
                MessageBox.Show(this, watch.Elapsed.Minutes + " dk " + watch.Elapsed.Seconds + " sn", "Bilgi");
                loadMissions(month);
            }

        }

        public void loadMissions(string month)
        {
            int slc_month = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;
            List<missionListView> missionList = entities.missionListView.Where(m => m.dateT.Value.Month == slc_month).OrderBy(m => m.dateT).ToList();

            lvMissions.ItemsSource = missionList;
            lvMissions.Loaded += LvMissions_Loaded;
        }

        public void loadDefault()
        {
            List<missionListView> missionList = entities.missionListView.OrderBy(m => m.dateT).ToList();

            string[][] userMission = new string[missionList.Count][];

            for (int i = 0; i < userMission.Count(); i++)
            {
                userMission[i] = new string[4];// ->[0]date->[1]user_name->[2]user_name
            }

            int index2 = 0;
            for (int index = 0; index < missionList.Count; index++)
            {
                if (index > 0)
                {
                    if (missionList[index].dateT != missionList[index - 1].dateT)
                    {
                        index2++;
                    }
                }

                for (int i = 0; i < userMission[index2].Count(); i++)
                {
                    if (userMission[index2][1] != missionList[index].name + "_" + missionList[index].lastname)
                    {
                        if (i > 1 && userMission[index2][i - 1] != missionList[index].name + "_" + missionList[index].lastname && userMission[index2][i] == null)
                        {
                            userMission[index2][i] = missionList[index].name + "_" + missionList[index].lastname;
                        }
                        if (i == 0 && userMission[index2][i] == null)
                        {
                            userMission[index2][i] = missionList[index].dateT.ToString();
                        }
                        else if (i == 1 && userMission[index2][i] == null)
                        {
                            userMission[index2][i] = missionList[index].name + "_" + missionList[index].lastname;
                        }
                    }

                }
            }

            /* lvMissions.ItemsSource = missionList;
             lvMissions.Loaded += LvMissions_Loaded;*/

            userMissionList.UserMission(userMission);

            CalendarWin calendar = new CalendarWin();
            calendar.ShowDialog();
        }

        public void btnMissions_Click(object sender, EventArgs e)
        {
            loadDefault();
        }

        public void btnDelete_Click(object sender, EventArgs e)
        {
            wait waiter = new wait();
            string month = combo_months.Items[combo_months.SelectedIndex].ToString();
            int slc_month = DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month;
            waiter.Show();
            _missionmanager.DeleteMissionsMonth(slc_month);   
            loadMissions(month);
            waiter.Close();
            MessageBox.Show(this, month + " ayı kayıtları silindi", "Bilgi");
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
