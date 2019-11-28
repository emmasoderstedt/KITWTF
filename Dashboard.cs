using System;
using System.Collections.Generic;
using menu;
namespace KITWTF
{
    public class Dashboard
    {
        string dashboardHeader = "Dashboard";
        string[] dashboardContent = new string[] { "Se kontakter", "Lägg till kontakt" };
        public void dashboard()
        {
            databaseHandler DBHandler = new databaseHandler();
            int id = 1;
            var dashboardMenu = new Menu(dashboardContent);
            dashboardMenu = dashboardMenu.GetMenu(dashboardMenu, dashboardHeader);

            switch (dashboardMenu.SelectedIndex)
            {
                case 0: //se kontakter 
                    List<Person_PersonTable> relations = new List<Person_PersonTable>();
                    relations = DBHandler.ListRelation(id);

                    foreach (var relation in relations)
                    {
                        Console.WriteLine("Namn: " + relation.Alias);
                        Console.WriteLine("Tid kvar: " + relation.RemainingTime);
                        Console.WriteLine("--------------------------------------");
                    }
                    break;

                case 1:  //lägg till kontakt

                    databaseHandler dbHandler = new databaseHandler();

                    string menuHeader = "Lägg till kontakt";
                    string[] menuContent = new string[] { "Lägg till befintlig användare", "Lägg till kontakt (utan konto)" };

                    var addContactMenu = new Menu(menuContent);
                    addContactMenu = addContactMenu.GetMenu(addContactMenu, menuHeader);

                    switch (addContactMenu.SelectedIndex)
                    {
                        case 0: //Lägg till befrintlig användare
                            Console.WriteLine("Skriv in din väns användarnamn: ");
                            string friendUsername = Console.ReadLine();
                            int friendId = dbHandler.GetID(friendUsername);
                            if (friendId != 0)
                            {
                                Console.WriteLine("Skriv in ett namn på er relation:");
                                string alias = Console.ReadLine();

                                Console.WriteLine("Skriv in antal dagar du ska ha på dig att kontakta personen: ");
                                int remaningTime = Console.Read();

                                DBHandler.AddRelation(alias, id, friendId, remaningTime);
                            } else {
                                Console.WriteLine("Angivet användarnamn finnns ej registrerat.");
                            }
                            break;

                        case 1: //Lägg till användare (utan konto)

                            User newUser = new User();

                            Console.Write("Skriv in personens namn:");
                            newUser.Name = Console.ReadLine();

                            Console.Write("Skriv in telefonnummer: ");
                            newUser.Phonenumber = Console.ReadLine();
                            DBHandler.AddUser(newUser);
                            
                            Console.Write("Skriv in namn på relationen: ");
                            string relationName = Console.ReadLine();
                            
                            break;
                    }

                break;
            }
        }

    }
}
