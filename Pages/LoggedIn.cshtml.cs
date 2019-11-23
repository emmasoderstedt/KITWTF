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
        public string userLogged { get; set; }

        public List <DBTables> friends = new List<DBTables>();

        public IActionResult OnGet(int? id=null)
        {

            try
            {
                string connectionString="server=40.85.84.155;Database=student8;User Id=student8;Password=YH-student@2019";
                 using (SqlConnection connection = new SqlConnection(connectionString))
                 {       
                                     friends =   connection.Query<DBTables>($"EXEC showFamily @id ='{id}'").ToList();

                                     userLogged =   connection.Query<DBTables>($"EXEC showFamily @id ='{id}'").FirstOrDefault().MyId;
 
                                            if(userLogged==null||friends==null)
                                            {
                                                     return Redirect("/Index?Error=fel"); 
                                            }                   
                                          return null;                     
                 }
            }
            catch (System.Exception)
            {
                
               return Redirect("/Index?Error=fel"); 
            }
                
        }
    }
}
