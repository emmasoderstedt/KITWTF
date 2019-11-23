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
    public class LoggedInModel : PageModel
    {
        public string friend { get; set; }

        public List <DBConnection> friends = new List<DBConnection>();
        public IActionResult OnGet(int? id=null)
        {
                string connectionString="server=40.85.84.155;Database=student8;User Id=student8;Password=YH-student@2019";
                 using (SqlConnection connection = new SqlConnection(connectionString))
                 {       

                                     var user =   connection.Query<DBConnection>($"EXEC showFamily @id ='{id}'");
                                                  friends = new List<DBConnection>(user);  

                                            if(user==null)
                                            {
                                                     return Redirect("/Index?Error=fel"); 
                                            }                   
                                          return null;                     
                 }

        }
    }
}
