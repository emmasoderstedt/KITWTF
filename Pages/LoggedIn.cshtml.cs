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


        public IActionResult OnGet(int? id=null)
        {

            try
            {
                string connectionString="server=40.85.84.155;Database=student8;User Id=student8;Password=YH-student@2019";
                 using (SqlConnection connection = new SqlConnection(connectionString))
                 {       

 
                                            if(userLogged==null)
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
