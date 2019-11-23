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
        public int id {get;set;}

        public string nameTest { get; set; }

        public  List<DBTables> users = new List<DBTables>();
      
   
        public void OnGet()
        {
                //nameTest = HttpContext.Session.GetString(SessionKeyName);
     
        }
        
        public IActionResult OnPost(string email,string password)
        {
               try
               { 
                    string connectionString="server=40.85.84.155;Database=student8;User Id=student8;Password=YH-student@2019";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {       
                                     var query =   connection.Query<DBTables>($"select id from users where userName = '{email}' and passWord= '{password}'");
                                     users = new List<DBTables>(query);
                                            id  = query.FirstOrDefault().id;                      
                                            if(id!=null)
                                            {
                                                     return Redirect("/LoggedIn?id="+id);
                                            }                                 
                                            return Redirect("/Index?Error=fel"+id);
                    }
               }
               catch (System.Exception)
               {             
                   return Redirect("/Index?Error=fel"+id);
               }
        }
    }
}
