using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using menu;

namespace KITWTF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            databaseHandler dbHandler = new databaseHandler();
            // dbHandler.AddUser("Emma","456@654.com", "EÄrNice", "Säkert", "112");
            // dbHandler.AddRelation("Kompis", 2, 3, 30);

            Debug.WriteLine(dbHandler.GetIdentity());

            string startMenuHeader = "Välj vad du vill köra:";
            string[] startMenuContent = new string[] {"Webbsida", "Konsolapplikation"};

            var startMenu = new Menu(startMenuContent);
            startMenu = startMenu.GetMenu(startMenu, startMenuHeader);

            switch (startMenu.SelectedIndex)
            {
                case 0: //Starta webserver
                    break;
                case 1:  //Starta konsollapp

                    string mainMenuHeader = "KITWTF";
                    string[] mainMenuContent = new string[] {"Logga in", "Skapa nytt konto"};

                    var mainMenu = new Menu(mainMenuContent);
                    mainMenu = mainMenu.GetMenu(mainMenu, mainMenuHeader);

                    switch(mainMenu.SelectedIndex)
                    {
                        case 0://Logga in

                        Console.Write("Skriv in ditt användarnamn: ");
                        CurrentUser currentUser = new CurrentUser();
                        currentUser.Username = Console.ReadLine();
                        dbHandler.LoginUsername(currentUser.Username, currentUser.Password);                  

                        break;

                        case 1: //Skapa nytt konto

                            User user = new User();

                            Console.Write("Skriv in ditt namn:");
                            user.Name =  Console.ReadLine();

                            Console.Write("Skriv in användarnamn:");
                            user.Username = Console.ReadLine();

                            Console.Write("Skriv in lösenord:");
                            user.Password = Console.ReadLine();

                            Console.Write("Skriv in e-post adress");
                            user.Email = Console.ReadLine();

                            Console.Write("Skriv in telefonnummer:");
                            user.Phonenumber = Console.ReadLine();

                        break;
                    }
                    break;
            }
        }
    }
}
