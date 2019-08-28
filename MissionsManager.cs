using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Manager
{
    class MissionsManager
    {
        missionTimeEntities missionEntities = new missionTimeEntities();

        public void AssignMission(decimal userID, DateTime date)
        {
            missions record = new missions();
            record.attendantID = userID;
            record.dateT = date;

            missionEntities.missions.Add(record);
            missionEntities.SaveChanges();
        }

        public void DeleteMissions()
        {
            List<missions> mis = new List<missions>();

            mis = missionEntities.missions.OrderBy(m => m.dateT).ToList();

            foreach(var x in mis)
            {
                missionEntities.Entry(x).State = System.Data.Entity.EntityState.Deleted;
            }

            missionEntities.SaveChanges();
        }

        public void DeleteMissionsMonth(int month)
        {
            List<missions> mis = new List<missions>();

            mis = missionEntities.missions.Where(m => m.dateT.Value.Month == month).ToList();

            foreach (var x in mis)
            {
                missionEntities.Entry(x).State = System.Data.Entity.EntityState.Deleted;
            }

            missionEntities.SaveChanges();
        }

        public List<missions> GetMiss(DateTime startDate,decimal id)
        {
            List<missions> mis = new List<missions>();
            mis = missionEntities.missions.Where(m => m.dateT >= startDate && m.attendantID == id).Where(m => m.dateT.Value.Month == startDate.Month || m.dateT.Value.Month == startDate.Month + 1).ToList();
            return mis;
        }

        //public List<missions> GetPreMonth(int start, int month, int year,decimal id)
        //{
        //    List<missions> mis = new List<missions>();
        //    DateTime date = new DateTime(year,month,start);
        //    mis = missionEntities.missions.Where(m => m.dateT > date && m.attendantID == id).ToList();//> return mis

        //    return mis;      
        //}

        public List<missions> MissionList(int month,decimal id)
        {
            List<missions> missions;
            missions = missionEntities.missions.Where(m => m.dateT.Value.Month == month && m.attendantID == id).OrderBy(m=> m.dateT).ToList();
            return missions;
        }

        public List<missions> MissionList()
        {
            List<missions> missions;
            missions = missionEntities.missions.OrderBy(m => m.dateT).ToList();
            return missions;
        }

        public bool MissionListCheck(int month)
        {
            var mission = missionEntities.missions.Where(m => m.dateT.Value.Month == month ).FirstOrDefault();
            if(mission == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
