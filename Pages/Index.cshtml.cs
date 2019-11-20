using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dapper;
using System.Data.SqlClient;


namespace KITWTF1.Pages
{
    public class IndexModel : PageModel
    {
        public string email {get; set;}
        
        public string password {get;set;}
        public int id {get; set;}
      
         public  List<DBConnection> users = new List<DBConnection>();
        public void OnGet()
        {
                
     
        }

     int GetUsers(string email,string password)
        {
                 string connectionString="server=40.85.84.155;Database=skk8;User Id=student8;Password=YH-student@2019";
                 using (SqlConnection connection = new SqlConnection(connectionString))
                 {       
                    
                                     var query =   connection.Query<DBConnection>($"select id,userName,passWord from dbo.[users] where userName='{email}'and passWord ='{password}'");
                                     users= new List<DBConnection>(query);  
                                     
                                     foreach (var item in query)
                                     {
                                           id = item.id;
                                          
                                     }
                                            Console.WriteLine("id" +email+password+id);
                                      return id;
                 }


        }
         public void OnPost()
        {
               
            GetUsers(email,password);
        }
    }
}
