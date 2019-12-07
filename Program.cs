using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using menu;
using System.Data.SqlClient;
namespace KITWTF1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool loop = true;
            DatabaseHandler dbHandler = new DatabaseHandler();

            Debug.WriteLine(dbHandler.GetIDNonUserentity());

            string startMenuHeader = "Välj vad du vill köra:";
            string[] startMenuContent = new string[] { "Webbsida", "Konsolapplikation" };

            var startMenu = new Menu(startMenuContent);
            while (loop)
            {
                startMenu = startMenu.GetMenu(startMenu, startMenuHeader);
                switch (startMenu.SelectedIndex)
                {
                    case 0: //Starta webserver
                        CreateWebHostBuilder(args).Build().Run();
                        loop = false;
                        break;
                    case 1:  //Starta konsollapp
                        loop = false;
                        string mainMenuHeader = "KITWTF";
                        string[] mainMenuContent = new string[] { "Logga in", "Skapa nytt konto", "Avsluta konsolapplikation" };

                        var mainMenu = new Menu(mainMenuContent);
                        bool konsolMenu = true;
                        do
                        {
                            mainMenu = mainMenu.GetMenu(mainMenu, mainMenuHeader);
                            switch (mainMenu.SelectedIndex)
                            {
                                case 0://Logga in

                                    Console.Write("Skriv in ditt användarnamn: ");
                                    var username = Console.ReadLine();

                                    Console.Write("Skriv in ditt lösenord : ");
                                    var password = Console.ReadLine();

                                    bool loggedIn = dbHandler.LoginUsername(username, password);//om godkänt skicka koden vidare till Dashboard.cs
                                    if (loggedIn)
                                    {
                                        Dashboard dashboard = new Dashboard();
                                        int ID = dbHandler.GetIDNonUser(username);
                                        dashboard.dashboard(ID);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Felaktigt lösenord eller användarnamn. \n Tryck valfri tangent för att försöka igen");
                                        Console.ReadKey();
                                    }
                                    break;

                                case 1: //Skapa nytt konto

                                    User newUser = new User();

                                    Console.Write("Skriv in ditt namn: ");
                                    newUser.Name = Console.ReadLine();

                                    Console.Write("Skriv in användarnamn: ");
                                    newUser.Username = Console.ReadLine();

                                    Console.Write("Skriv in lösenord: ");
                                    newUser.Password = Console.ReadLine();

                                    Console.Write("Skriv in e-post adress: ");
                                    newUser.Email = Console.ReadLine();


                                    try
                                    {
                                        dbHandler.AddUser(newUser);
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Otillåten inmatning");
                                        Console.ReadLine();
                                    }

                                    break;

                                case 2: // Backa till val om konsoll eller web
                                    konsolMenu = false;
                                    return;
                            }
                        } while (konsolMenu);
                        break;
                }
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
