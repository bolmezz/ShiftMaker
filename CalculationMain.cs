using Mission.GUI;
using Mission.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.CalculationManager
{
    public class CalculationMain
    {
        private UserManager _usermanager = new UserManager();
        private DayOffManager _dayoffmanager = new DayOffManager();
        private DemandDayManager _demandmanager = new DemandDayManager();
        private MissionsManager _missionmanager = new MissionsManager();
        private DaysManager _daysmanager = new DaysManager();
        missionTimeEntities missionEntities = new missionTimeEntities();

        public static int r = 0;
        public int month = 0;
        public static int year = DateTime.Today.Year;
        public static double result1 = 0;
        public static double result2 = 0;
        public static double result3 = 0;
        public static double result4 = 0;
        public static double result6 = 0;
        public static double result7 = 0;
        public static double result8 = 0;
        public static double maxVal = 0;
        public static List<days> reqDays;
        public List<missions> list;
        public static int calculationDateCount = 0;
        public static double[][] goalTable;
        wait waiter = new wait();
        List<PrimerUsers> primerUserList;
        List<users> userList;
        Random random = new Random();
        List<users> backupUsers;
        int[] nums = new int[4];



        public CalculationMain(int x, string selected_month)
        {
            r = x;
            month = DateTime.ParseExact(selected_month, "MMMM", CultureInfo.CurrentCulture).Month;
            //if (r == 2)
            //{
            //    backupUsers = _usermanager.UserListRate(1, true);
            //    for (int i = 0; i < 4; i++)
            //    {
            //        nums[i] = random.Next(backupUsers.Count);
            //    }
            //}

            LoadPrimers();
        }

        public void LoadPrimers()
        {
            primerUserList = new List<PrimerUsers>();
            userList = _usermanager.UserListRate(r, true);
            if (r == 1)
            {
                userList.AddRange(_usermanager.UserListRate(11, true));
            }
            else if (r == 2)
            {
                userList.AddRange(_usermanager.UserListRate(22, true));
            }

            //if (r == 2 && userList.Count < 5)
            //{
            //    int d = 5 - userList.Count;// seconderler en az 5 kişi olmalı, eksiklerse fark primerler ile kapatılmalı
            //    for (int i = 0; i < d; i++)//primer listesinin içinden ihtiyaç olan kadar eleman alınacak
            //    {        
            //        userList.Add(backupUsers[nums[i]]);
            //    }
            //}

            foreach (users user in userList)
            {
                PrimerUsers primeruser = new PrimerUsers();
                primeruser.User = user;
                primeruser.MissonDay = missionEntities.missions.Where(m => m.attendantID == user.id).OrderBy(m => m.dateT).ToList();
                primeruser.DemandDay = missionEntities.demandDays.Where(m => m.userID == user.id).ToList();
                primeruser.Dayoffs = missionEntities.dayOffs.Where(m => m.userID == user.id).ToList();

                primerUserList.Add(primeruser);
            }
        }

        public List<missions> preMonth(int m, decimal id)// önceki ayın (varsa) son  (userlist.count -1) günü 
        {
            DateTime startDate = new DateTime(year, m, 1);
            startDate = startDate.AddDays(-5);
            list = _missionmanager.GetMiss(startDate, id);

            return list;
        }

        public void CalculatePrimer()
        {
            createGoalTable();

            for (int daysToMission = 0; daysToMission < calculationDateCount; daysToMission++)
            {
                LoadPrimers();
                DateTime missionDate = new DateTime(year, month, daysToMission + 1);// Nöbet günü

                for (int primerIndex = 0; primerIndex < primerUserList.Count; primerIndex++)
                {
                    result1 = 0;
                    result2 = 0;
                    result3 = 0;
                    result4 = 0;
                    result6 = 0;
                    result7 = 0;
                    result8 = 0;
                    maxVal = 0;

                    for (int j = 0; j < primerUserList.Count; j++)//karşılaştırılacak kullanıcı (j)
                    {
                        if (j == primerIndex)//kendisiyle(primerIndex) karşılaştırılmayacak
                        {
                            j++;

                            if (j == primerUserList.Count)
                            {
                                break;
                            }
                        }
                        missionCount(primerIndex, j, missionDate); // result1 ve result7 haftaiçi/sonu nöbet sayısı farkları
                    }//j

                    demandDayCheck(missionDate, primerIndex); // result3 / istek gününe nöbet yazılması

                    missiondaysDiff(missionDate, primerIndex);//result4 / nöbetler arası gün sayısı kontrolü

                    dayoffCheck(missionDate, primerIndex); //result6 /izin gününe nöbet yazılmamalı

                    //seconderCheck(missionDate, primerIndex);

                    if (missionDate.DayOfWeek == DayOfWeek.Saturday || missionDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        goalTable[daysToMission][primerIndex] = result1 - result3 + result4 + result6 + result8;//result1=haftasonu nöbet sayısı farkı
                    }
                    else
                    {
                        goalTable[daysToMission][primerIndex] = -result3 + result4 + result6 + result7 + result8;//result7=haftaiçi nöbet sayısı farkı
                    }
                }//primerindex


                assignMission(daysToMission, goalTable, missionDate);//nöbet yaz

                // en yeniler En kıdemlilerle tutmalı primer'=11 ve sekonder'=22 var

               /* if (r == 1)//primer' atandığı gün işaretleniyor böylece yanına sekonder eklenebilecek
                {
                    double minVal = findMinVal(goalTable, daysToMission);
                    int personIndex = Array.IndexOf(goalTable[daysToMission], minVal);
                    if (primerUserList[personIndex].User.rate == 11) // en kıdemli primer ise
                    {
                        _daysmanager.AssignPrimerDay(missionDate);
                    }
                }*/

                ///

                if (r == 1)//icapçı günü için 2 primer eklenmeli
                {
                    for (int i = 0; i < reqDays.Count; i++)
                    {
                        if (missionDate == reqDays[i].dateT && reqDays[i].count == 0)//icapçı gününe 1 primer eklendiyse tekrar o gün için primer seçilmeli
                        {
                            daysToMission--;
                            reqDays[i].count++;
                            break;
                        }
                    }
                }
            }//dayTomission (günler)
        }//calculatePrimer


        public void createGoalTable()
        {
            calculationDateCount = DateTime.DaysInMonth(year, month);//nöbet oluşturulacak ayın gün sayısı

            goalTable = new double[calculationDateCount][];
            double[][] array = new double[calculationDateCount][];

            for (int index = 0; index < calculationDateCount; index++)
            {
                goalTable[index] = new double[primerUserList.Count];
                array[index] = new double[primerUserList.Count];
            }

            reqDays = _daysmanager.ListDays(month);// nöbet yazılcak ay için icapçı günlerini al
            foreach (var day in reqDays)
            {
                day.count = 0;// icapçı günündeki primer sayısı(count)
            }
        }

        public void seconderCheck(DateTime missionDate, int primerIndex)
        {
            if (r == 2)
            {
                if (primerUserList[primerIndex].User.rate == 22) // en kıdemsiz sekonder ise
                {
                    if (_daysmanager.PrimeDayCheck(missionDate))//eğer atanacak günde bir primer' varsa oraya sekonder' atanabilir
                    {
                        result8 += 3 * (Math.Pow(2, primerUserList.Count * 2));// en kıdemsizin en kıdemlinin yaına atanması için
                    }
                }
            }
        }

        public void missionCount(int primerIndex, int j, DateTime missionDate)
        {
            List<missions> listPrimer = _missionmanager.MissionList(missionDate.Month, primerUserList[primerIndex].User.id);
            int wePrimer = 0;
            int wdPrimer = 0;
            if (listPrimer.Count > 0)
            {
                for (int i = 0; i < listPrimer.Count; i++)
                {
                    if (listPrimer[i].dateT.Value.DayOfWeek == DayOfWeek.Sunday || listPrimer[i].dateT.Value.DayOfWeek == DayOfWeek.Saturday)
                    {
                        wePrimer++;
                    }
                    else
                    {
                        wdPrimer++;
                    }
                }
            }

            List<missions> list2 = _missionmanager.MissionList(missionDate.Month, primerUserList[j].User.id);

            int wePrimerNext = 0;
            int wdPrimerNext = 0;
            if (list2.Count > 0)
            {
                for (int i = 0; i < list2.Count; i++)
                {
                    if (list2[i].dateT.Value.DayOfWeek == DayOfWeek.Sunday || list2[i].dateT.Value.DayOfWeek == DayOfWeek.Saturday)
                    {
                        wePrimerNext++;
                    }
                    else
                    {
                        wdPrimerNext++;
                    }
                }
            }

            int Vij = (wePrimer - wePrimerNext) * (int)EnumUserWeight.Primer;//haftasonu nöbet sayısı farkları
            int Vij2 = (wdPrimer - wdPrimerNext) * (int)EnumUserWeight.Primer;//haftaiçi nöbet sayısı farkları

            result1 += Vij * (Math.Pow(2, primerUserList.Count));
            result7 += Vij2 * (Math.Pow(2, primerUserList.Count));
        }

        public void demandDayCheck(DateTime missionDate, int primerIndex)
        {
            int Vi = 0;
            if (_demandmanager.DemanddayCheck(missionDate, primerUserList[primerIndex].User.id))
            {
                Vi++;
                int sum = Vi * (int)EnumUserWeight.Primer;
                result3 = sum * (Math.Pow(2, primerUserList.Count - 2));//max//istek gününe nöbet yazılması
            }
        }

        public void missiondaysDiff(DateTime missionDate, int primerIndex)
        {
            List<int> V_Values = new List<int>(primerUserList.Count);
            List<missions> missiondaysList = preMonth(month, primerUserList[primerIndex].User.id);//bir önceki ayın son nöbetleri+geçerli ayın nöbetleri

            for (int i = 0; i < V_Values.Capacity; i++)
            {
                V_Values.Insert(i, 0);
            }

            if (missiondaysList.Count != 0)
            {
                if (missiondaysList[missiondaysList.Count - 1].dateT.Value.Month < missionDate.Month)
                {
                    DateTime d = new DateTime(year, month, 1);
                    d = d.AddDays(-1);//bir önceki ayın son günü

                    for (int index = 0; index < V_Values.Capacity; index++)
                    {
                        if (d.Day - missiondaysList[missiondaysList.Count - 1].dateT.Value.Day + missionDate.Day <= index + 1)
                        {
                            V_Values[index]++;

                            if (index == 0)
                            {
                                result4 += (V_Values[index] * (int)EnumUserWeight.Primer) * (Math.Pow(2, primerUserList.Count * 2));//art arda nöbet yazılmamalı
                            }
                            else
                            {
                                result4 += (V_Values[index] * (int)EnumUserWeight.Primer) * (Math.Pow(2, primerUserList.Count - index));//+1-index
                            }
                            break;
                        }
                    }
                }
                else
                {
                    for (int index = 0; index < V_Values.Capacity; index++)
                    {
                        if (missionDate.Day - missiondaysList[missiondaysList.Count - 1].dateT.Value.Day <= index + 1)//3+index //nöbet yazılcak gün - en son nöbet günü
                        {
                            V_Values[index]++;

                            if (index == 0)
                            {
                                result4 += (V_Values[index] * (int)EnumUserWeight.Primer) * (Math.Pow(2, primerUserList.Count * 2));//art arda nöbet yazılmamalı
                            }
                            else
                            {
                                result4 += (V_Values[index] * (int)EnumUserWeight.Primer) * (Math.Pow(2, primerUserList.Count - index));//+1-index
                            }
                            break;
                        }
                    }
                }
            }
        }

        public void dayoffCheck(DateTime missionDate, int primerIndex)
        {
            int Vl = 0;
            if (_dayoffmanager.DayoffCheck(missionDate, primerUserList[primerIndex].User.id))
            {
                Vl++;
                int sum2 = Vl * (int)EnumUserWeight.Primer;
                result6 += sum2 * (Math.Pow(2, primerUserList.Count * 2));//max//izin gününe nöbet yazılmamalı
            }
        }

        public double findMinVal(double[][] goalTable, int daysToMission)
        {
            double minVal = goalTable[daysToMission].Min();
            return minVal;
        }

        public void assignMission(int daysToMission, double[][] goalTable, DateTime missionDate)
        {
            double minVal = findMinVal(goalTable, daysToMission);
            maxVal = goalTable[daysToMission].Max();
            int personIndex = Array.IndexOf(goalTable[daysToMission], minVal);

            _missionmanager.AssignMission(primerUserList[personIndex].User.id, missionDate);//nöbet yaz
        }
    }
}
