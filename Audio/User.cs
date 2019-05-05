using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Audio
{
   public  class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string Detect { get; set; }

        public User()
        {
                
        }
        public User(string l, string p)
        {
            Login = l;
            Password = p;
        }
        public static bool ExistUser(User user)
        {
            using (Db db = new Db())
            {

                var res = db.Users.Where(x => x.Login == user.Login && x.Password==user.Password).FirstOrDefault();
                if (res != null)
                {
                    
                    if (res != null)
                        return true;
                    else return false;
                }
                else return false;
            }
            
        }
        public static void AddUser(User user)
        {
            using (Db db = new Db())
            {
                db.Users.Add(user);
                db.SaveChangesAsync();
            }
        }

        

        public static User find(string sms)
        {
            using (Db db = new Db())
            {
               var res = db.Users.Where(x => x.Login == sms).FirstOrDefault();
                return res;
            }
        }
        public static User findID(int sms)
        {
            using (Db db = new Db())
            {
                var res = db.Users.Where(x => x.Id == sms).FirstOrDefault();
                return res;
            }
        }
    }
}
