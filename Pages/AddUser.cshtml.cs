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
    public class AddUserModel : PageModel
    {
        public string email {get; set;}
        
     
        public string UserPassword { get; set; }

        public string PhoneNumber { get; set; }

        public string Username { get; set; }
        public int id {get;set;}

        public string Name { get; set; }

        public  List<DBTables> users = new List<DBTables>();
      
           public void OnGet()
        {
                //nameTest = HttpContext.Session.GetString(SessionKeyName);
     
        }
        
      
        public void OnPost(string name,string email,string Username,string Userpassword, string PhoneNumber )
        {
               try
               { 
                   LoginDetailsTable ldt = new LoginDetailsTable();
                   DataBaseHandler dbth = new DataBaseHandler();
                   dbth.AddUser(name,email,Username,UserPassword,PhoneNumber);
                   dbth.AddUserToDatabase(ldt);
                  

               }
               catch (System.Exception)
               {             
                   
               }
        }
    }
}
