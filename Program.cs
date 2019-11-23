﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KITWTF1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Välj vad du vill köra:"+
            "\n[1] Webbsida"+
            "\n[2] Console Applikation");
            Int32.TryParse(Console.ReadLine(),out int choice);
            switch(choice)
            {
                    case 1:
                        CreateWebHostBuilder(args).Build().Run();
                        break;
                    case 2:
                        Menus menu = new Menus();
                        menu.MainMenu();
                        break;
            }
           
            
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
