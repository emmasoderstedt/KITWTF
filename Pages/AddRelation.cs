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
    public class AddRelationModel : PageModel
    {
        public string name {get; set;}
        
     
        public string relation { get; set; }

        public string timeSpan { get; set; }

        
       
        public int PersonID { get; set; }

        public int id { get; set; }
       

      
           public void OnGet(int PersonID)
           {
                //nameTest = HttpContext.Session.GetString(SessionKeyName);
            Console.WriteLine(PersonID+"adduser");
           }
        
      
        public IActionResult OnPost(int PersonID ,string name ,string relation,int timeSpan)
        {
             PersonID=9982;
             //PersonID = id;
            Console.WriteLine(id);
            if(name!=""&&relation!=""&&timeSpan!=0&&PersonID!=0)
            {

         
               try
               { 
                   
                   Person_PersonTable ppt = new Person_PersonTable();
                   DatabaseHandler dbh = new DatabaseHandler();

                      dbh.AddPerson(name);
                      int friendID = dbh.GetIDNonUserentity();

                      dbh.AddRelation(relation, PersonID, friendID, timeSpan);
                  
                      return Redirect("/LoggedIn?id="+PersonID);
                 





               }
               
               catch (System.Exception)
               {             
                    return Redirect("/Error"); 
               }
                
            }
            else
            {
                    return Redirect("/Error"); 
            }
        }
            
        
    }
}
