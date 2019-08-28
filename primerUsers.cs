using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.CalculationManager
{
   struct PrimerUsers
    {
        private List< missions> _missonDay;

        public List< missions> MissonDay
        {
            get { return _missonDay; }
            set { _missonDay = value; }
        }

        private users users;
        public users User
        {
            get { return users; }
            set { users = value; }
        }

        private List<demandDays> _demandDay;
        public List<demandDays> DemandDay
        {
            get { return _demandDay; }
            set { _demandDay = value; }
        }

        private List<dayOffs> _offDay;
        public List<dayOffs> Dayoffs
        {
            get { return _offDay; }
            set { _offDay = value; }
        }

    }
}
