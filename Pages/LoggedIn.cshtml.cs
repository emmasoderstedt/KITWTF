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

       public bool contact { get; set; }

       public string daysGone { get; set; }

       public string lastContact { get; set; }

      



        public List<Person_PersonTable> personList = new List<Person_PersonTable>();
        public void OnGet(int? id=null)
        {

            try
            {
             
                string todaysDate = DateTime.Now.ToString("yyyy-MM-dd");
                DatabaseHandler dbhandler = new DatabaseHandler();
                Person_PersonTable ppt = new Person_PersonTable();
                personList = dbhandler.ListRelation(dbhandler.GetID(DatabaseHandler.userName));
                int PersonID= dbhandler.GetID(DatabaseHandler.userName);
                string connectionString="server=40.85.84.155;Database=student29;User Id=student29;Password=YH-student@2019";
                 using (SqlConnection connection = new SqlConnection(connectionString))
                 {       
                       var dateFromDB = connection.Query<Person_PersonTable >($"select * from person_person where PersonID ='{PersonID}'").FirstOrDefault().lastCommunication;
           
                    string date  =  dateFromDB.Substring(0,10);
                    lastContact = date;
                    daysGone = Convert.ToString(ppt.DayCounter(date,todaysDate.Substring(0,10)));
                    if(ppt.DayCounter(todaysDate.Substring(0,10),date)<ppt.RemainingTime)
                    {
                                contact = true;
                    }
                
                 }


            }
            catch (System.Exception)
            {
                
              
            }
                
        }
        public void OnPost(int id)
        {


        }
    }
}
