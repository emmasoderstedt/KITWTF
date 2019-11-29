using System;
using System.Collections.Generic;
using menu;

namespace KITWTF1
{
    public class Dashboard
    {
        string dashboardHeader = "Dashboard";
        string[] dashboardContent = new string[] { "Se kontakter", "Lägg till kontakt", "Logga ut" };
        public void dashboard(int userID)
        {
            while (true)
            {
                DatabaseHandler DBHandler = new DatabaseHandler();
                var dashboardMenu = new Menu(dashboardContent);
                dashboardMenu = dashboardMenu.GetMenu(dashboardMenu, dashboardHeader);
                switch (dashboardMenu.SelectedIndex) //dasboard menu
                {
                    case 0: //se kontakter 
                        List<Person_PersonTable> relations = new List<Person_PersonTable>();
                        relations = DBHandler.ListRelation(userID);
                        

                        foreach (var relation in relations)
                        {
                            Console.WriteLine("Namn: " + relation.Alias);
                            Console.WriteLine("Tid kvar: " + relation.RemainingTime);
                            Console.WriteLine("--------------------------------------");
                        }
                        Console.ReadKey();
                        break;

                    case 1:  //lägg till kontakt

                        string menuHeader = "Lägg till kontakt";
                        string[] menuContent = new string[] { "Lägg till befintlig användare", "Lägg till kontakt utan konto" };

                        var addContactMenu = new Menu(menuContent);
                        addContactMenu = addContactMenu.GetMenu(addContactMenu, menuHeader);

                        switch (addContactMenu.SelectedIndex)
                        {
                            case 0: //Lägg till befrintlig användare
                                while (true)
                                {
                                    int userFriendID;
                                    Console.WriteLine("Skriv in din väns användarnamn: ");
                                    string friendUsername = Console.ReadLine();
                                    userFriendID = DBHandler.GetID(friendUsername);
                                    if (userFriendID != 0)
                                    {
                                        Console.WriteLine("Skriv in ett namn på er relation:");
                                        string alias = Console.ReadLine();

                                        Console.WriteLine("Skriv in antal dagar du ska ha på dig att kontakta personen: ");
                                        int remaningTime = Console.Read();

                                        DBHandler.AddRelation(alias, userID, userFriendID, remaningTime);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Angivet användarnamn finnns ej registrerat.");
                                        break;
                                    }
                                }
                                break;

                            case 1: //Lägg till användare (utan konto)

                                User newUser = new User();

                                Console.Write("Skriv in personens namn:");
                                newUser.Name = Console.ReadLine();

                                Console.Write("Skriv in telefonnummer: ");
                                newUser.Phonenumber = Console.ReadLine();

                                DBHandler.AddUser(newUser);
                                int friendID = DBHandler.GetIdentity();

                                Console.Write("Skriv in namn på relationen: ");
                                string relationName = Console.ReadLine();

                                Console.Write("Hur ofta vill du kontakta denna personen? ");
                                int contactTime = Console.Read();

                                DBHandler.AddRelation(relationName, userID, friendID, contactTime);

                                break;
                        }

                        break;
                    case 2:
                        {
                            Console.WriteLine("Tack för idag!");
                            return;
                        }
                }
            }
        }

    }
}
