using System;
using KITWTF1.Pages;
using System.Collections.Generic;
using System.Linq;
namespace KITWTF1
{
    public class Menus
    {
        public void MainMenu()
        {   Console.Clear();
            bool mainLoop = true;
            bool loop = true;
            while(mainLoop)
            {

            
                Console.WriteLine("Välkommen till KITWTF!"+
                "\n[1] Logga in"+
                "\n[2] Skapa ny användare"+
                "\n[3] Avsluta");
                Int32.TryParse(Console.ReadLine(), out int choice);
                switch(choice)
                {
                    case 1:

                           Console.Clear();
                            while(loop){
                            Console.Write("Användarnamn: ");
                            string usr=Console.ReadLine();
                            Console.Write("Lösenord: ");
                            string psw =Console.ReadLine();

                            IndexModel index = new IndexModel();
                            index.id=0;

                            index.OnPost(usr,psw);
                            if(index.id!=0)
                            {
                                   
                                    mainLoop = false;
                                    loop = false;
                                    LoggedIn(index.id);
                            }
                            else
                            {
                                    Console.Clear();

                                    Console.WriteLine("Anv eller lösen felaktigt");
                            }

                            }
                        break;
                    case 2:
                        Account acc = new Account();

                        acc.AddAccount(MakeUser());
                        break;
                    case 3:
                        mainLoop = false;
                        loop = false;
                        return;
                   
                       
                    
                }
            }

        }
        void LoggedIn(int id)
        {
            Console.Clear();
            Console.WriteLine("Inloggad");
            LoggedInModel loggedIn = new LoggedInModel();

            loggedIn.OnGet(id);
            bool menuLoop = true;
            while(menuLoop){
            
                Console.WriteLine("Välkommen! "+loggedIn.userLogged+
                "\n[1] Visa släktingar: "+ 
                "\n[2] Lägg till vän:" +
                "\n[3] Gå tillbaka. ");
                Int32.TryParse(Console.ReadLine(), out int choice);
                switch(choice)
                {
                        case 1:
                            Console.Clear();
                            ShowMyRelatives(id);
                            break;
                        
                        case 2:
                            AddFriend(id);
                            break;
                        case 3:
                            IndexModel index = new IndexModel();
                            Console.Clear();
                            MainMenu();      
                            menuLoop= false;
                            break;
                       
                }
            

            }


        }
        void ShowMyRelatives(int id)
        {
            LoggedInModel loggedIn = new LoggedInModel();
            loggedIn.OnGet(id);
            Console.Clear();
            foreach (var item in loggedIn.friends)
            {

            Console.WriteLine(item);
            

            
            }
            Console.WriteLine("Tryck en tangent för att fortsätta!");
            Console.ReadKey();
            Console.Clear();
        }

      Account MakeUser()
        {
            bool rightPassword = false;
            Console.WriteLine("Välj användarnamn: ");
            string userName = Console.ReadLine();
            Console.WriteLine("Välj lösenord: ");
            string password = Console.ReadLine();
            while(!rightPassword)
            {
            Console.WriteLine("Upprepa lösenord: ");
            string passwordTwo= Console.ReadLine();
            if(password ==passwordTwo)
            {

                Account db = new Account();
                db.userName = userName;
                db.passWord = password;
                return db;
            }

            }
            return null;
           
        }
        void AddFriend(int id)
        { 
            List<Account> users = new List<Account>();
            List<Relatives> relatives = new List<Relatives>();
            
            
            
            Account ac = new Account();
            users= ac.ShowUsers(ac,id).ToList();
            Console.Clear();
            Console.WriteLine("Vem vill du lägga till:");
            foreach (var item in users)
            {
                Console.WriteLine(item.userName);
            }
            string friend = Console.ReadLine();
            DBTables dbt= new DBTables();
            foreach (var item in users)
            {
                if(friend ==item.userName)
                {
                      dbt.friendsUserId = item.id;
                      dbt.friendWoApp=" ";
                      dbt.lastCommunication= DateTime.Now.ToString();

                      dbt.myUserName = id;
                     
                      
                     
                
                       break;
                }
            }
            Relatives r = new Relatives();
            relatives = r.ShowRelatives().ToList();
            Console.Clear();
            Console.WriteLine("Vilken relation har ni?");
            
            foreach (var item in relatives)
            {
                Console.WriteLine(item.type);
            
            }
             string rel = Console.ReadLine();
            foreach (var item in relatives)
            {
                 if(rel ==item.type)
                 {
                    dbt.RelativeId = item.id;
                    dbt.AddFriend(dbt);
                    break;
                 }
            }
            
            
        }



    }
}