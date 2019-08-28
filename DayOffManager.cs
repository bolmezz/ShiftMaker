using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Mission;

namespace Mission.Manager
{

    public class DayOffManager
    {

        missionTimeEntities missionEntities = new missionTimeEntities();

        public void AssignDayoffs(string day1, string day2, users user)
        {
            dayOffs record1 = new dayOffs();
            dayOffs record2 = new dayOffs();

            List<dayOffs> currentDay = missionEntities.dayOffs.Where(m => m.userID == user.id).ToList();

            foreach (var days in currentDay)
            {
                missionEntities.Entry(days).State = System.Data.Entity.EntityState.Deleted;
            }

            if (day1 != null)
            {
                record1.dateT = Convert.ToDateTime(day1);
                record1.userID = user.id;
            }

            if (day2 != null)
            {
                record2.dateT = Convert.ToDateTime(day2);
                record2.userID = user.id;

            }

            if (day1 == day2)
            {
                missionEntities.dayOffs.Add(record1);
     
            }
            else if (day1 != null && day2 != null)
            {
                missionEntities.dayOffs.Add(record1);
                missionEntities.dayOffs.Add(record2);
            }
            else
            {
                missionEntities.dayOffs.Add(record1);
            }

            missionEntities.SaveChanges();
        }


        public List<dayOffs> DayoffList()
        {
            List<dayOffs> dayoffList;
            dayoffList = missionEntities.dayOffs.OrderBy(m => m.dateT).ToList();

            return dayoffList;
        }

        public List<dayOffs> GetDayoff(decimal id)
        {
            List<dayOffs> dayoffList;
            dayoffList = missionEntities.dayOffs.Where(m => m.userID == id).ToList();

            return dayoffList;
        }

        public bool DayoffCheck(DateTime date, decimal id)
        {
            dayOffs d;
            d = missionEntities.dayOffs.Where(m => m.dateT == date && m.userID == id).FirstOrDefault();
            if(d == null)
            {
                return false;
            }
            return true;
        }


    }
}
