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

                switch (dashboardMenu.SelectedIndex)
                {
                    case 0: // Se kontakter 
                        OutputContactList(DBHandler.ListRelation(userID));
                        Console.WriteLine("\nTryck valfri tangent för att gå vidare");
                        break;

                    case 1:  // Lägg till kontakt

                        string menuHeader = "Lägg till kontakt";
                        string[] menuContent = new string[] { "Lägg till befintlig användare", "Lägg till kontakt utan konto" , "Tillbaka"};

                        var addContactMenu = new Menu(menuContent);
                        addContactMenu = addContactMenu.GetMenu(addContactMenu, menuHeader);

                        switch (addContactMenu.SelectedIndex)
                        {
                            case 0: 
                            // Skapa relation med befintlig användare
                                while (true)
                                {
                                    int userFriendID;
                                    Console.WriteLine("Skriv in din väns användarnamn: ");
                                    string friendUsername = Console.ReadLine();

                                    userFriendID = DBHandler.GetIDExistingUser(friendUsername);
                                    if (userFriendID != 0) 
                                    {
                                        Console.WriteLine("Skriv in ett namn på er relation:");
                                        string alias = Console.ReadLine();

                                        Console.WriteLine("Skriv in antal dagar du ska ha på dig att kontakta personen: ");
                                        string remaningTime = Console.ReadLine();
                                        int RemainingTime = 0;
                                        try
                                        {
                                            RemainingTime = Convert.ToInt32(remaningTime);

                                            DBHandler.AddRelation(alias, userID, userFriendID, RemainingTime);

                                            Console.WriteLine("Kontakten är tillagd!");
                                            Console.WriteLine("\nTryck valfri tangent för att gå vidare");
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Skriv endast antal dagar i siffror");
                                        }
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Angivet användarnamn finnns ej registrerat.");
                                        Console.WriteLine("Vill du gå tillbaka [J/n]");
                                        var keyInfo = Console.ReadKey();
                                        if (keyInfo.Key == ConsoleKey.Enter || keyInfo.KeyChar.ToString().ToLower() == "j")
                                        {
                                            break;
                                        }
                                    }
                                }
                            break;

                            case 1: 
                            // Skapa relation med användare utan konto (icke användare)
                                Console.Write("Skriv in personens namn: ");
                                string Name = Console.ReadLine();

                                DBHandler.AddPerson(Name);
                                int friendID = DBHandler.GetIDNonUserentity();

                                Console.Write("Skriv in namn på relationen: ");
                                string relationName = Console.ReadLine();

                                Console.Write("Antal dagar mellan kontakt: ");
                                int contactTime = 0;
                                bool exitLoop = true;
                                while (exitLoop)
                                {
                                    try
                                    {
                                        contactTime = Convert.ToInt32(Console.ReadLine());
                                        exitLoop = false;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Enbart siffor");
                                    }
                                }
                                Console.WriteLine("User id: " + userID);
                                Console.WriteLine("Friend id: " + friendID);

                                DBHandler.AddRelation(relationName, userID, friendID, contactTime);
                                Console.WriteLine("Kontakten är tillagd!");
                                Console.ReadKey();
                                break;

                                case 2:

                                break;

                        }
                        break;
                    case 2:
                    // Logga ut
                        return;
                }

            }
        }
        public static void OutputContactList(List<Person_PersonTable> relations)
        {
            foreach (var relation in relations)
            {
                
                Console.WriteLine();
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("Namn: " + relation.PersonName);
                Console.WriteLine("AKA: " + relation.Alias);
                Console.WriteLine("Tid kvar: " + relation.RemainingTime + " dagar");
                Console.WriteLine("Senaste kontakt:"+ relation.lastCommunication.Substring(0,10));
                Console.WriteLine("--------------------------------------");
            }
            Console.WriteLine("\nTryck valfri tangent för att gå vidare");
            Console.ReadKey();
        }

    }
}
