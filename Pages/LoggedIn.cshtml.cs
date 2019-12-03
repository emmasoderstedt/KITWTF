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

        public List<Person_PersonTable> personList = new List<Person_PersonTable>();
        public void OnGet(int? id=null)
        {

            try
            {
                DatabaseHandler dbhandler = new DatabaseHandler();
               
                dbhandler.getData(DatabaseHandler.userName);
                
                personList = dbhandler.ListRelation(dbhandler.GetID(DatabaseHandler.userName));


            }
            catch (System.Exception)
            {
                
              
            }
                
        }
    }
}
