﻿using System;
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
        public string Username { get; set; }
        public string password {get;set;}
        public int id {get;set;}
        public string nameTest { get; set; }

      
   
        public void OnGet()
        {
                //nameTest = HttpContext.Session.GetString(SessionKeyName);
        }
        
        public IActionResult OnPost(string email,string password)
        {
               try
               { 
                        DatabaseHandler dbh = new DatabaseHandler();
                       
                                            if(dbh.LoginUsername(email,password))
                                            {
                                                    DatabaseHandler.userName = email; 
                                                     return Redirect("/LoggedIn?id="+id);
                                            }                                 
                                           return null;   
               }
               catch (System.Exception)
               {             
                 return null;
               }
        }
    }
}
