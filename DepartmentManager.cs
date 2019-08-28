using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mission;


namespace Mission.Manager
{

    public class DepartmentManager
    {
        missionTimeEntities missionEntities = new missionTimeEntities();

        public List<departments> GetDepartments()
        {
            List<departments> depts;

            depts = missionEntities.departments.OrderBy(m => m.department_name).ToList();

            return depts;

        }

        public decimal DepartmetnID(string name)
        {
            departments dept = missionEntities.departments.Where(m => m.department_name == name).FirstOrDefault();

            return dept.id;

        }

        public string DepartmetName(decimal? deptID)
        {
            departments dept = missionEntities.departments.Where(m => m.id == deptID).FirstOrDefault();

            return dept.department_name;
        }

        public departments GetDept(decimal? id)
        {
            departments dept = missionEntities.departments.Where(m => m.id == id).FirstOrDefault();

            return dept;

        }
    }
}
