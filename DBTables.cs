using System;
using Dapper;
using System.Data.SqlClient;
using System.Collections.Generic;
namespace KITWTF1.Pages
{
    public class DBTables 
    {
        //Friends table
        public int id { get; set; }

        public string userName { get; set; }

        public string passWord { get; set; }

        public string lastLogin { get; set; }

        public string friendWoApp{ get; set; }

        public string lastCommunication { get; set; }

        public int RelativeId { get; set; }

        public int myUserName{ get; set; }

        public int friendsUserId { get; set; }

        public string friendsUserName { get; set; }

        public string MyId { get; set; }

        public string friendsId { get; set; }

       public string myRelative { get; set; }


       public override string ToString()
       {


           return 
           "Namn: "+friendsId+
           "\nRelation: "+myRelative+
           "\nSenast kontakt: "+lastCommunication+

           "\n--------------------------------------------------------------------------------------------------------";
       }
       public IEnumerable<DBTables> AddFriend(DBTables dbt)
{       //TODO
         try
        { 
            //Funkar ej
                    string connectionString="server=40.85.84.155;Database=student8;User Id=student8;Password=YH-student@2019";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {               Console.WriteLine(dbt.friendsUserId+ dbt.friendWoApp+dbt.lastCommunication+ dbt.myUserName+dbt.RelativeId);
                                   return connection.Query<DBTables>($"insert into friends (friendWoApp,lastCommunication,relativeId,myUserName,friendsUserId) values ('','{dbt.lastCommunication}','{dbt.RelativeId}','{dbt.myUserName}' ,'{dbt.friendsUserId}')");                
                    }
               
               
        } 
        catch (System.Exception)
        {             
                  return  null;
        }
    
}
       



        
    }
        
    
}