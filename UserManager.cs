using Mission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Manager
{
    public class UserManager
    {
        missionTimeEntities missionEntities = new missionTimeEntities();

        public users LoginCheck(string username, string pass)
        {
            users user;
            user = missionEntities.users.Where(m => m.username == username && m.pass == pass).FirstOrDefault();
            return user;
        }

        public List<users> UserList()
        {
            List<users> userList;
            userList = missionEntities.users.OrderBy(m => m.id).ToList();
            return userList;

        }
        public List<users> UserListRate(int rate, bool statu)
        {
            List<users> userList;
            userList = missionEntities.users.Where(m => m.rate == rate && m.statu == statu).ToList();
            return userList;

        }

        public users GetUser(decimal selectedID)
        {
            users user;
            user = missionEntities.users.Where(m => m.id == selectedID).FirstOrDefault();
            return user;
        }

        public bool Add(string nametb, string lastnametb, decimal deptID, int rate, bool statu)
        {
            users user = new users();

            user.name = nametb;
            user.lastname = lastnametb;
            user.username = user.name.ToLower() + "_" + user.lastname.ToLower();
            user.pass = "1234";
            user.departmantId = deptID;
            user.rate = rate;
            user.statu = statu;

            missionEntities.users.Add(user);
            missionEntities.SaveChanges();

            return true;

        }

        public bool Update(string nametb, string lastnametb, decimal deptID, int rate, bool statu, users userOld)
        {

            userOld.name = nametb;
            userOld.lastname = lastnametb;
            userOld.departmantId = deptID;
            userOld.rate = rate;
            userOld.statu = statu;
            userOld.username = (userOld.name + "_" + userOld.lastname).ToLower();
            missionEntities.Entry(userOld).State = System.Data.Entity.EntityState.Modified;
            missionEntities.SaveChanges();

            return true;
        }

        public bool Delete(users user)
        {
            missionEntities.Entry(user).State = System.Data.Entity.EntityState.Deleted;
            missionEntities.SaveChanges();

            return true;
        }

        public users ChangePass(string oldPass, string newPass, string newPassR, users user)
        {
            users existentuser = missionEntities.users.Where(m => m.pass == oldPass).FirstOrDefault();

            if (existentuser != null && user.pass == existentuser.pass && newPass == newPassR)
            {
                existentuser.pass = newPass;
                missionEntities.Entry(existentuser).State = System.Data.Entity.EntityState.Modified;
                missionEntities.SaveChanges();
                return existentuser;
            }
            return null;
        }

    }
}
