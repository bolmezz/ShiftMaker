using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Manager
{
    public class DaysManager
    {
        missionTimeEntities missionEntities = new missionTimeEntities();

        //günün durumu false ise icapçı günüdür.
        //günün durumu true olarak girilmişse primer' nöbetçi olan gündür. yanına sekonder' gelmeli

        public void AssignReqDay(string day1, string day2)
        {
            days record1 = new days();
            days record2 = new days();

            DeleteCurrentDays();

            if (day1 != null)
            {
                record1.dateT = Convert.ToDateTime(day1);
                record1.status = false;
            }
            if (day2 != null)
            {
                record2.dateT = Convert.ToDateTime(day2);
                record2.status = false;
            }

            if (day1 == day2)
            {
                missionEntities.days.Add(record1);

            }
            else if (day1 != null && day2 != null)
            {
                missionEntities.days.Add(record1);
                missionEntities.days.Add(record2);
            }
            else
            {
                missionEntities.days.Add(record1);
            }
            missionEntities.SaveChanges();
        }

        public List<days> ListDays(int month)
        {
            List<days> reqs = missionEntities.days.Where(m => m.dateT.Value.Month == month).ToList();
            //if (reqs.Count == 0)
            //{
            //    reqs = missionEntities.days.Take(2).OrderByDescending(m => m.dateT).ToList();
            //}
            return reqs;
        }

        public void AssignPrimerDay(DateTime missionDay)
        {
            days record1 = new days();
            record1.dateT = missionDay;
            record1.isPrimer = true;
            missionEntities.days.Add(record1);
            missionEntities.SaveChanges();

        }

        public bool PrimeDayCheck(DateTime date)
        {
            days d;
            d = missionEntities.days.Where(m => m.dateT == date).FirstOrDefault();

            if (d == null)
            {
                return false;
            }

            return true;
        }

        public void DeleteCurrentDays()
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

            List<days> reqs = missionEntities.days.Where(m => m.dateT.Value.Month == nextMonth).ToList();

            foreach (var x in reqs)
            {
                missionEntities.Entry(x).State = System.Data.Entity.EntityState.Deleted;
            }
            missionEntities.SaveChanges();
        }

       /* public void ChangeDay()
        {
            days record1 = new days();
            days record2 = new days();

            List<days> reqs = missionEntities.days.OrderBy(m => m.dateT).ToList();
            string date;

            foreach (var x in reqs)
            {
                if (x.dateT.Value.Month < DateTime.Today.Month)
                {
                    date = x.dateT.Value.Day + "." + DateTime.Today.Month + "." + x.dateT.Value.Year;
                    x.dateT = Convert.ToDateTime(date);
                    missionEntities.Entry(x).State = System.Data.Entity.EntityState.Modified;
                }          
            }

            missionEntities.SaveChanges();
        }*/
    }
}