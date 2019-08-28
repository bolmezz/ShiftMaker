using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Manager
{
    public class MissionViewManager
    {
        private users _user;

        public users User
        {
            get { return _user; }
            set { _user = value; }
        }

        private List<DateTime> _dateTimes = new List<DateTime>();

        public List<DateTime> dateTimes
        {
            get { return _dateTimes; }
            set { _dateTimes = value; }
        }

        //private List<bool> _dateTimes;

        //public List<bool> dateTimes
        //{
        //    get { return _dateTimes; }
        //    set { _dateTimes = value; }
        //}

        public  string username
        {
            get
            {
                return _user.name + " " + _user.lastname;
            }
        }

        private bool _dates;
        public bool selectedMissions
        {
            get { return _dates; }
            set { _dates = value; }
        }


    }
}
