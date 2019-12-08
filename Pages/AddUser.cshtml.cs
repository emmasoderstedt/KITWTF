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

        public void OnGet()
        {
                
     
        }
      
        public IActionResult OnPost(string name,string email,string Username,string Userpassword, string PhoneNumber )
        {
               try
               { 
                   User user = new User();
                   
                   user.Email = email;
                   
                   user.Username = Username;
                   user.Name = user.Username;
                   user.Password = Userpassword;
                   user.Phonenumber = PhoneNumber;

                   DatabaseHandler dbt = new DatabaseHandler();
                   //.AddUser(name,email,Username,UserPassword,PhoneNumber);

                   dbt.AddUser(user);
                   return Redirect("/Index");

                    



               }
               catch (System.Exception)
               {             
                        return Redirect("/Index");
               }
        }
    }
}
