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

namespace Audio
{
   public  class User
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string detect { get; set; }

        public User()
        {
                
        }
        public User(string l, string p)
        {
            login = l;
            password = p;
        }
        public static bool ExistUser(User user)
        {
            using (IDbConnection cnn = new SQLiteConnection(Load()))
            {

                var res = cnn.QueryFirstOrDefault<User>("select * from Users where login=@login and password=@password",new { login = user.login ,password = user.password});
                if (res != null)
                {
                    User u = (User)res;
                    if (u != null)
                        return true;
                    else return false;
                }
                else return false;
            }
            
        }
        public static void AddUser(User user)
        {
            using (IDbConnection cnn = new SQLiteConnection(Load()))
            {
                cnn.Execute("insert into Users (login,password) values(@login,@password)",user);  
            }
        }

        private static string Load(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static User find(string sms)
        {
            using (IDbConnection cnn = new SQLiteConnection(Load()))
            {
                var res = cnn.QueryFirstOrDefault<User>("select * from Users where login=@login", new { login = sms });
                if (res != null)
                    return (User)res;
                else
                    return null;
            }
        }
        public static User findID(int sms)
        {
            using (IDbConnection cnn = new SQLiteConnection(Load()))
            {
                var res = cnn.QueryFirstOrDefault<User>("select * from Users where id=@id", new { id = sms });
                if (res != null)
                    return (User)res;
                else
                    return null;
            }
        }
    }
}
