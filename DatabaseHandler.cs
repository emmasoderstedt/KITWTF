using Dapper;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SqlClient;

namespace KITWTF
{
    class databaseHandler
    {
        static int PersonID;
        private static string connectionString = "server=40.85.84.155;Database=Student29;User Id=student29;Password=YH-student@2019";

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

            // Creates and assign values
            LoginDetailsTable LoginDetailsTemplate = new LoginDetailsTable()
            {
                PersonID = PersonID,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password,
                Phonenumber = user.Phonenumber
            };
            // Add user to the LoginDetails table
            AddUserToDatabase(LoginDetailsTemplate);
        }
        private void AddUserToDatabase(LoginDetailsTable loginDetailsTable)
        {
            /// <summary> Add a user to the LoginDetails table
            string executeString = string.Format(
            "INSERT INTO Student29.dbo.LoginDetails VALUES ('{0}','{1}','{2}','{3}','{4}')",
                                                                        loginDetailsTable.PersonID,
                                                                        loginDetailsTable.Email,
                                                                        loginDetailsTable.Username,
                                                                        loginDetailsTable.Password,
                                                                        loginDetailsTable.Phonenumber);
            LoginDetailsTable.SendQuery(executeString);
            Debug.WriteLine("Successfully sent: " + executeString);
        }
        private void AddUserToDatabase(PersonTable personTable)
        {
            /// <summary> Add a user to the Person table
            string executeString = "INSERT INTO Student29.dbo.Person(PersonName) OUTPUT INSERTED.PersonID VALUES ('" + personTable.Name + "')";

            var query = PersonTable.SendAndGetQuery(executeString);
            Debug.WriteLine("Successfully sent: " + executeString);
            foreach (var items in query)
            {
                PersonID = items.PersonID;
                Debug.WriteLine("PersonID: " + items.PersonID);
            }
        }

        /* ------------------------------ Add Relation ------------------------------ */
        public void AddRelation(string Alias, int PersonID, int ContactID, int RemainingTime)
        {
            /// <summary> Adds an relation between two people
            /// <para> Doing this by adding the connection to Person_Person table

            Person_PersonTable contactTable = new Person_PersonTable()
            {
                Alias = Alias,
                PersonID = PersonID,
                ContactID = ContactID,
                RemainingTime = RemainingTime
            };
            string executeString = string.Format("INSERT INTO Student29.dbo.Person_Person VALUES ('{0}', {1}, {2}, {3})",
                                                                                                                    Alias,
                                                                                                                    PersonID,
                                                                                                                    ContactID,
                                                                                                                    RemainingTime);
            Person_PersonTable.SendQuery(executeString);
            Debug.WriteLine("Successfully sent: " + executeString);
        }
        /* ---------------------------------- Login --------------------------------- */
        public void LoginUsername(string Username, string Password)
        {
            /// <summary> Allows login with Username/Password combination
            string executeString = string.Format(
                "SELECT PersonID FROM Student29.dbo.LoginDetails WHERE Username = '{0}' AND Password = '{1}",
                Username,
                Password);
            var query = LoginDetailsTable.SendAndGetQuery(executeString);
            LoginDetailsTable[] queryArray = query.AsList().ToArray();
            Verify(queryArray);
        }
        public void LoginEmail(string Email, string Password)
        {
            /// <summary> Allows login with Email/Password combination
            string executeString = string.Format(
                "SELECT PersonID FROM Student29.dbo.LoginDetails WHERE Email = '{0}' AND Password = '{1}",
                Email,
                Password);
            var query = LoginDetailsTable.SendAndGetQuery(executeString);
            LoginDetailsTable[] queryArray = query.AsList().ToArray();
            Verify(queryArray);
        }
        private void Verify(LoginDetailsTable[] queryArray)
        {
            /// <summary> Internal system that verify that there are matched logins
            if (queryArray == null || queryArray.Length == 0)
            {
                throw new System.Exception("No matching credentials");
            }
        }

        /* ------------------------------ List Relation ----------------------------- */
        public List<Person_PersonTable> ListRelation(int id)
        {
            /// <summary> Outputs an list where all the elements are open
            string executeString = string.Format("EXEC SelectAllPersonRelation @Id = {0}", id);
            var query = Person_PersonTable.SendAndGetQuery(executeString);
            return query.AsList();
        }

        /* --------------------------------- Search --------------------------------- */
        public List<LoginDetailsTable> GetData(string Username)
        {
            /// <summary> Returns Username, UserPassword, Email, PhoneNumber as a array for the matching Username
            string executeString = string.Format("EXEC SearchForUsername @Username = {0}", Username);
            var query = LoginDetailsTable.SendAndGetQuery(executeString);
            return query.AsList();
        }
        public int GetID(string Username)
        {   /// <summary> Returns the ID of the matching Username combination
            string executeString = string.Format("EXEC GetID @Username = {0}", Username);
            var query = LoginDetailsTable.SendAndGetQuery(executeString);
            Debug.WriteLine(query);
            return 1;
        }
    }

    /* --------------------------------- Tables --------------------------------- */

    class DatabaseTable
    {
        internal static string connectionString = "server=40.85.84.155;Database=student29;User Id=student29;Password=YH-student@2019";
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

    class LoginDetailsTable : DatabaseTable
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

    class Person_PersonTable : DatabaseTable
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
