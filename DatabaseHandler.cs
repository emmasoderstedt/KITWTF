using Dapper;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlClient;

namespace KITWTF1
{
    public class DatabaseHandler
    {
       public static string userName;

        /* -------------------------------- Add User -------------------------------- */
        public void AddUser(User user)
        {
            /// <summary> Add a predefined user to the database
            // Creates and assign values
            PersonTable personTemplate = new PersonTable()
            {
                Name = user.Name
            };
            // Add user to the Person table
            AddUserToDatabase(personTemplate);
            
            int personID = GetID(personTemplate.Name);

            // Creates and assign values
            LoginDetailsTable LoginDetailsTemplate = new LoginDetailsTable()
            {
                PersonID = personID,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                Phonenumber = user.Phonenumber
            };
            // Add user to the LoginDetails table
            AddUserToDatabase(LoginDetailsTemplate);
        }

        public void AddPerson(string Name)
        {
            string executeString = string.Format("INSERT INTO Person (PersonName) VALUES ('{0}')", Name);
            PersonTable.SendQuery(executeString);
        }
        private void AddUserToDatabase(LoginDetailsTable loginDetailsTable)
        {
            /// <summary> Add a user to the LoginDetails table
            string executeString = string.Format(
            "INSERT INTO Student29.dbo.LoginDetails VALUES ('{0}','{1}','{2}','{3}','{4}')",
                                                                        loginDetailsTable.PersonID,
                                                                        loginDetailsTable.Username,
                                                                        loginDetailsTable.Password,
                                                                        loginDetailsTable.Email,
                                                                        loginDetailsTable.Phonenumber);
            LoginDetailsTable.SendQuery(executeString);
            Debug.WriteLine("Successfully sent: " + executeString);
        }
        private void AddUserToDatabase(PersonTable personTable)
        {
            /// <summary> Add a user to the Person table
            string executeString = string.Format("INSERT INTO Student29.dbo.Person(PersonName) OUTPUT INSERTED.PersonID VALUES ('{0}')", personTable.Name);

            var query = PersonTable.SendAndGetQuery(executeString);
            Debug.WriteLine("Successfully sent: " + executeString);
            foreach (var items in query)
            {
                //PersonID = items.PersonID;
                Debug.WriteLine("PersonID: " + items.PersonID);
            }
        }

        /* ------------------------------ Add Relation ------------------------------ */
        
        public void AddRelation(string Alias, int PersonID, int ContactID, int RemainingTime)
        {
            /// <summary> Adds an relation between two people
            /// <para> Doing this by adding the connection to Person_Person table

            string executeString = string.Format("INSERT INTO Student29.dbo.Person_Person (Alias, PersonID, ContactID, RemainingTime) VALUES ('{0}', {1}, {2}, {3})", Alias, PersonID, ContactID, RemainingTime);
            Person_PersonTable.SendQuery(executeString);
            Debug.WriteLine("Successfully sent: " + executeString);
        }
        public void AddRelation(string Alias, int RemainingTime, User User, User ContactUser)
        {
            /// <summary> Adds an relation between two people
            /// <para> Doing this by adding the connection to Person_Person table

            string executeString = string.Format("INSERT INTO Student29.dbo.Person_Person VALUES ('{0}', {1}, {2}, {3})",
                                                                                                                    Alias,
                                                                                                                    User.PersonID,
                                                                                                                    ContactUser.PersonID,
                                                                                                                    RemainingTime);
            Person_PersonTable.SendQuery(executeString);
            Debug.WriteLine("Successfully sent: " + executeString);
        }
        public void AddRelation(Person_PersonTable person_PersonTable)
        {
            /// <summary> Adds an relation between two people
            /// <para> Doing this by adding the connection to Person_Person table

            string executeString = string.Format("INSERT INTO Student29.dbo.Person_Person VALUES ('{0}', {1}, {2}, {3})",
                                                                                                                    person_PersonTable.Alias,
                                                                                                                    person_PersonTable.PersonID,
                                                                                                                    person_PersonTable.ContactID,
                                                                                                                    person_PersonTable.RemainingTime);
            Person_PersonTable.SendQuery(executeString);
            Debug.WriteLine("Successfully sent: " + executeString);
        }
        /* ---------------------------------- Login --------------------------------- */
        public bool LoginUsername(string Username, string Password)//return bool pls
        {
            /// <summary> Allows login with Username/Password combination
            string executeString = string.Format(
                "SELECT PersonID FROM Student29.dbo.LoginDetails WHERE Username = '{0}' AND UserPassword = '{1}'",
                Username,
                Password);
            var query = LoginDetailsTable.SendAndGetQuery(executeString);
            LoginDetailsTable[] queryArray = query.AsList().ToArray();
            return isCorrectCredentials(queryArray);
        }   
        public bool LoginEmail(string Email, string Password)
        {
            /// <summary> Allows login with Email/Password combination
            string executeString = string.Format(
                "SELECT PersonID FROM Student29.dbo.LoginDetails WHERE Email = '{0}' AND Password = '{1}",
                Email,
                Password);
            var query = LoginDetailsTable.SendAndGetQuery(executeString);
            LoginDetailsTable[] queryArray = query.AsList().ToArray();
            return isCorrectCredentials(queryArray);
        }
        private bool isCorrectCredentials(LoginDetailsTable[] queryArray)
        {
            /// <summary> Internal system that verify that there are matched logins
            /// <para> Writes to the debuger for the different stages
            if (queryArray == null || queryArray.Length == 0)
            {
                Debug.WriteLine("No matching credentials");
                return false;
            }
            else if (queryArray.Length == 1)
            {
                Debug.WriteLine("The credentials matched!");
                return true;
            }
            else if (queryArray.Length > 1)
            {
                Debug.WriteLine("Multiple matching accounts");
                return false;
            }
            else
            {
                Debug.WriteLine("Unkown Error!");
                return false;
            }
        }

        /* ------------------------------ List Relation ----------------------------- */
        public List<Person_PersonTable> ListRelation(int id)
        {
            /// <summary> Outputs an list where the query is returned
            string executeString = string.Format("EXEC SelectAllPersonRelation @Id = {0}", id);
            var query = Person_PersonTable.SendAndGetQuery(executeString);
            
            return query.AsList();
        }

        /* --------------------------------- Search --------------------------------- */
        public List<string> GetInfo (string Username){//gets trimmed down info
            getData(Username);
            List <string> relationInfo = new List <string> ();
            return relationInfo;
        }
        public List<LoginDetailsTable> getData(string Username)
        {
            /// <summary> Returns Username, UserPassword, Email, PhoneNumber as a array for the matching Username
            string executeString = string.Format("EXEC SearchForUsername @Username = '{0}'", Username);
            var query = LoginDetailsTable.SendAndGetQuery(executeString);
            return query.AsList();
        }
        public int GetID(string Username)
        {   /// <summary> Returns the ID of the matching Username combination
            string executeString = string.Format("EXEC GetID @Username = '{0}'", Username);
            var query = LoginDetailsTable.SendAndGetQuery(executeString);
            Debug.WriteLine(query);

            foreach (var item in query)
            {
                return item.PersonID;
            }
            return 0;
        }
        public int GetIdentity() //Returns the ID of the latest submission to the DB
        {
            string connectionString = "server=40.85.84.155;Database=student29;User Id=student29;Password=YH-student@2019";
            SqlConnection connection = new SqlConnection(connectionString);

            string executeString = string.Format("SELECT Max(PersonID) FROM Student29.dbo.Person");

                using (connection)
                {
                    return connection.ExecuteScalar<int>(executeString);
                }
        }

    /* ------------------------ Change Remaining Time ------------------------ */
        public void ChangeRemainingTime(int PersonID)
        {
            string executeString = string.Format("UPDATE Student29.dbo.Person_Person SET RemainingTime =- 1", PersonID);
            Person_PersonTable.SendQuery(executeString);
        }

        public int GetRemainingTime(int PersonID, int ContactID)
        {
        string executeString = string.Format("SELECT RemainingTime FROM Student29.dbo.Person_Person WHERE PersonID = {0} AND ContactID = {1}", PersonID, ContactID);
            var query = Person_PersonTable.SendAndGetQuery(executeString);
            
            return query.AsList()[0].RemainingTime;
            
        }
    }

    /* --------------------------------- Tables --------------------------------- */

   public class DatabaseTable
    {
        internal static string connectionString = @"server=40.85.84.155;Database=student29;User Id=student29;Password=YH-student@2019";
        // internal static SqlConnection connection = new SqlConnection(connectionString);
        public int PersonID { get; set; }
    }
    class PersonTable : DatabaseTable
    {
        public string Name { get; set; }
        public static void SendQuery(string ExecuteString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Query<PersonTable>(ExecuteString);
                
            }
        }
        public static IEnumerable<PersonTable> SendAndGetQuery(string ExecuteString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = connection.Query<PersonTable>(ExecuteString);
                return query;
            }
        }
    }
     public class LoginDetailsTable : DatabaseTable
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public static void SendQuery(string ExecuteString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Query<LoginDetailsTable>(ExecuteString);
            }
        }
        public static IEnumerable<LoginDetailsTable> SendAndGetQuery(string ExecuteString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = connection.Query<LoginDetailsTable>(ExecuteString);
                return query;
            }
        }
    }
     public class Person_PersonTable : DatabaseTable
    {
        public int RelationID { get; set; }
        public string Alias { get; set; }
        public int ContactID { get; set; }
        public int RemainingTime { get; set; }
        public static void SendQuery(string ExecuteString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Query<Person_PersonTable>(ExecuteString);
            }
        }
        public static IEnumerable<Person_PersonTable> SendAndGetQuery(string ExecuteString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var query = connection.Query<Person_PersonTable>(ExecuteString);
                return query;
            }
        }
    }
}
