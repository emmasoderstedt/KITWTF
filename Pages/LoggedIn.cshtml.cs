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

       public int daysGone { get; set; }

       public int lastContact { get; set; }

       public int setDays { get; set; }

       public int ContactID { get; set; }

       public int PersonID { get; set; }

       public int userID { get; set; }

      
        public List<Person_PersonTable> personList = new List<Person_PersonTable>();
        public  void OnGet(int PersonID)
        {
           
            try
            {
                string todaysDate = DateTime.Now.ToString("yyyy-MM-dd");
                DatabaseHandler dbhandler = new DatabaseHandler();
                Person_PersonTable ppt = new Person_PersonTable();
                personList = dbhandler.ListRelation(dbhandler.GetIDNonUser(DatabaseHandler.userName));
                PersonID= dbhandler.GetIDNonUser(DatabaseHandler.userName);
                Console.WriteLine(PersonID+"logedin");
                DatabaseHandler.userID = PersonID;
                string connectionString="server=40.85.84.155;Database=student29;User Id=student29;Password=YH-student@2019";
                 using (SqlConnection connection = new SqlConnection(connectionString))
                 {       
                       
                            var dateFromDB = connection.Query<Person_PersonTable >($"select * from person_person where PersonID ='{PersonID}'").ToList();
                    
                        foreach (var item in personList)
                        {
                            
                            var dateFromContact = dateFromDB.FirstOrDefault(x => x.ContactID ==item.PersonID);
                            item.lastContact =  ppt.DayCounter(dateFromContact.lastCommunication.Substring(0,10),todaysDate.Substring(0,10));
                            
                        }    
                 }


            }
            catch (System.Exception)
            {
                    
            }
                
        }
        public  IActionResult OnPost(int ContactID)
        {
               string date = DateTime.Now.ToString("yyyy-MM-dd");
               try
               {     
                    DatabaseHandler dbhandler = new DatabaseHandler();
                    Person_PersonTable ppt = new Person_PersonTable();
                     ppt.lastCommunication= date;
                     ppt.ContactID = ContactID;
                     Console.WriteLine(ContactID+ "Tryin to update date with "+date);
                     dbhandler.UpdateDatetime(ppt);
                    return Redirect("/LoggedIn?id="+PersonID);
               }
               catch (System.Exception)
               {             
                    return Redirect("/LoggedIn?id="+PersonID);
               }     
        }    
    }
}
