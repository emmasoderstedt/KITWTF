using Dapper;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlClient;

namespace KITWTF1.Pages
{
    class DataBaseHandler
    {
        int PersonID;
        private string connectionString="server=40.85.84.155;Database=student29;User Id=student29;Password=YH-student@2019";

/* -------------------------------- Add User -------------------------------- */
        public void AddUser(string Name, string Email, string Username, string Password, string Phonenumber)
        {
            PersonTable personTemplate = new PersonTable()
            {
                Name = Name
            };
            
            LoginDetailsTable LoginDetailsTemplate = new LoginDetailsTable()
            {
                Email = Email,
                Username = Username,
                Password = Password,
                Phonenumber = Phonenumber
            };
        }
        public void AddUserToDatabase(LoginDetailsTable loginDetailsTable)
        {
            string executeString = string.Format(
            "INSERT INTO Student29.dbo.LoginDetails VALUES ('{0}','{1}','{2}','{3}','{4}')", 
                                                                        loginDetailsTable.PersonID, 
                                                                        loginDetailsTable.Email, 
                                                                        loginDetailsTable.Username, 
                                                                        loginDetailsTable.Password, 
                                                                        loginDetailsTable.Phonenumber); //TODO 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = connection.Query<LoginDetailsTable>(executeString); 
                Debug.WriteLine("Successfully sent: " + executeString);
            }
        }
        private void AddUserToDatabase(PersonTable personTable)
        {
            string executeString = "INSERT INTO Student29.dbo.Person(PersonName) OUTPUT INSERTED.PersonID VALUES ('" + personTable.Name + "')";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                IEnumerable<PersonTable> query = connection.Query<PersonTable>(executeString);
                Debug.WriteLine("Successfully sent: " + executeString);
                
                foreach (var items in query)
                {
                    PersonID = items.PersonID;
                    Debug.WriteLine("PersonID: " + items.PersonID);
                }
            }
        }

/* ------------------------------ Add Relation ------------------------------ */
        public void AddRelation(string alias, int PersonID, int ContactID, int RemainingTime)
        {
            Person_PersonTable contactTable = new Person_PersonTable()
            {
                Alias = alias,
                PersonID = PersonID,
                ContactID = ContactID,
                RemainingTime = RemainingTime                
            };
            string executeString = string.Format("INSERT INTO Studenet29.dbo.Person_Person VALUES ('{0}', {1}, {2}, {3})", 
                                                                                                                    alias, 
                                                                                                                    PersonID, 
                                                                                                                    ContactID, 
                                                                                                                    RemainingTime);

            Debug.WriteLine("Successfully sent: " + executeString);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = connection.Query<Person_PersonTable>(executeString);
                Debug.WriteLine("Successfully sent: " + executeString);
            }
        }
/* ------------------------------ List Relation ----------------------------- */
        public List<Person_PersonTable> ListRelation()
        {
            string executeString = string.Format("SELECT ");
            return null;
        }
        
    }










/* --------------------------------- Tables --------------------------------- */

    class DatabaseTable
    {
        public int PersonID {get; set;}
    }
    class PersonTable : DatabaseTable
    {
        public string Name {get; set;}
    }

    class LoginDetailsTable : DatabaseTable
    {
        public string Username {get; set;}
        public string Password {get; set;}
        public string Email {get; set;}
        public string Phonenumber {get; set;}


        public override string ToString() 
        {
                return Username+Password+Email+Phonenumber;
        }
    }

    class Person_PersonTable : DatabaseTable
    {
        public int RelationID {get; set;}
        public string Alias {get; set;}
        public int ContactID {get; set;}
        public int RemainingTime {get; set;}

    }
}