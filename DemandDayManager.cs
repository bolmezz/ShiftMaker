using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Manager
{
    public class DemandDayManager
    {
        missionTimeEntities missionEntities = new missionTimeEntities();

        public void AssignDays(List<string> list, users user)
        {
            List<demandDays> currentlist = missionEntities.demandDays.Where(m => m.userID == user.id).ToList();

            foreach(var days in currentlist)
            {
                missionEntities.Entry(days).State = System.Data.Entity.EntityState.Deleted;

            }

            demandDays record1;

            if (list != null)
            {
                foreach(var day1 in list)
                {
                    record1 = new demandDays();
                    record1.dateT = Convert.ToDateTime(day1);
                    record1.userID = user.id;
                    missionEntities.demandDays.Add(record1);
                } 
            }

            missionEntities.SaveChanges();
        }

        public void Delete(int month)
        {
            List<demandDays> currentDay = missionEntities.demandDays.OrderBy(m=> m.dateT.Value.Month == month ).ToList();

            foreach (var days in currentDay)
            {
                missionEntities.Entry(days).State = System.Data.Entity.EntityState.Deleted;
            }

            missionEntities.SaveChanges();
        }

        public List<demandDays> GetDemandDays(decimal userid)
        {
            List<demandDays> list;
            list = missionEntities.demandDays.Where(m => m.userID == userid).ToList();

            return list;
        }

        public List<demandDays> DemandList()
        {
            List<demandDays> demands;
            demands = missionEntities.demandDays.OrderBy(m => m.id).ToList();
            return demands;
        }

        public bool DemanddayCheck(DateTime date, decimal id)
        {
            demandDays d;
            d = missionEntities.demandDays.Where(m => m.dateT == date && m.userID == id).FirstOrDefault();

            if(d == null)
            {
                return false;
            }

            return true;
        }
            

    }
}
