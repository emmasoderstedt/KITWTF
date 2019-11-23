using Dapper;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace KITWTF1
{
    public class Account
    {    //users table
        public int id { get; set; }
        public string userName { get; set; }

        public string passWord { get; set; }

        public string lastLogin {get;set;}

    
public IEnumerable<Account> AddAccount(Account acc)
{
         try
        { 
                    string connectionString="server=40.85.84.155;Database=student8;User Id=student8;Password=YH-student@2019";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {       
                                   return connection.Query<Account>($"insert into users (userName,passWord,lastLogin) values ('{acc.userName}' ,  '{acc.passWord}','{DateTime.Now}')");                
                    }
               
               
        } 
        catch (System.Exception)
        {             
                  return  null;
        }
    
}
public IEnumerable<Account> ShowUsers(Account acc ,int id)
{
         try
        { 
                    string connectionString="server=40.85.84.155;Database=student8;User Id=student8;Password=YH-student@2019";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {       
                                   return connection.Query<Account>($"select * from users where not id ='{id}'");                
                    }
        }
        catch (System.Exception)
        {             
                  return  null;
        }
        
    
}






    }   
}

