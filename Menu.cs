using System;
using System.Collections.Generic;
using System.Linq;

namespace menu
{
    public class Menu
    {
        public Menu(IEnumerable<string> items)
        {
            Items = items.ToArray();            // Gör om items till en array    
        }
        public IReadOnlyList<string> Items { get; }
        public int SelectedIndex { get; private set; } = 0; // Vilket index du står på i menyn (Första värdet är 0)
        public string SelectedOption => SelectedIndex != -1 ? Items[SelectedIndex] : null; // Värdet på valet i sträng form
        public void MoveUp() => SelectedIndex = Math.Max(SelectedIndex - 1, 0);      // Rör dig ett steg uppåt om inte man redan står längst upp
        public void MoveDown() => SelectedIndex = Math.Min(SelectedIndex + 1, Items.Count - 1);   // Rör dig ett steg neråt om inte man redan står längst ner


        public Menu GetMenu(Menu menu, string header)
        {
            var menuPainter = new ConsoleMenuPainter(menu);

            Console.Clear();

            Console.WriteLine(header);

            for (int i = 0; i <= header.Length; i++)
            {
                Console.Write('-');
            }

            bool done = false;

            do
            {
                menuPainter.Paint();
                var keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        menu.MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        menu.MoveDown();
                        break;
                    case ConsoleKey.Enter:
                        done = true;
                        break;
                }
            }
            while (!done);
            return menu;
        }

    }
    public class ConsoleMenuPainter
    {
        readonly Menu menu;
        public ConsoleMenuPainter(Menu menu)
        {
            this.menu = menu;
        }
        public void Paint()
        {
            for (int i = 0; i < menu.Items.Count; i++)
            {

                var color = menu.SelectedIndex == i ? ConsoleColor.Yellow : ConsoleColor.Gray;
                Console.ForegroundColor = color;
                Console.SetCursorPosition(0, 3 + i);

                if (i == menu.SelectedIndex)
                {
                    Console.Write("->");
                }
                else
                {
                    Console.Write("  ");
                }

                Console.SetCursorPosition(2, 3 + i);
            }
        }
    }
    public class InputMenu : Menu
    {
        internal bool isInputMenu = false;
        public List<string> InputItems { get; private set; } = new List<string> { };
        public InputMenu(IEnumerable<string> items, bool isInputMenu) : base(items)
        {
            InputItems = items.ToList();

            if (isInputMenu)
            {
                this.isInputMenu = isInputMenu;

                for (int i = 0; i < items.Count(); i++)
                {
                    InputItems[i] = "";
                }
            }
        }
        public InputMenu GetInputMenu(InputMenu menu, string header)
        {
            var menuPainter = new ConsoleInputMenuPainter(menu);

            Console.Clear();
            Console.WriteLine(header);

            for (int i = 0; i <= header.Length; i++)
            {
                Console.Write('-');
            }
            bool done = false;
            do
            {
                menuPainter.Paint();
                var keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        menu.MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        menu.MoveDown();
                        break;
                    case ConsoleKey.Enter:
                        if (isFilled())
                        {
                            done = true;
                        }

                        break;

                    default:
                        char firstChar = keyInfo.KeyChar;
                        int headerOffset = 2;
                        int leftOffset = 4 + Items[menu.SelectedIndex].Length; // +2 from "->"

                        Console.SetCursorPosition(0, Items.Count + headerOffset);
                        Console.Write(' ');

                        Console.SetCursorPosition(leftOffset, headerOffset + menu.SelectedIndex);
                        Console.Write(keyInfo.KeyChar);

                        InputItems[menu.SelectedIndex] = keyInfo.KeyChar + Console.ReadLine();
                        break;
                }
            }
            while (!done);
            return menu;
        }
        public bool isFilled()
        {
            foreach (var item in InputItems)
            {
                if (item == "")
                {
                    return false;
                }
            }
            return true;
        }
    }
    public class ConsoleInputMenuPainter
    {
        readonly InputMenu menu;
        public ConsoleInputMenuPainter(InputMenu menu)
        {
            this.menu = menu;
        }
        public void Paint()
        {
            for (int i = 0; i < menu.Items.Count; i++)
            {
                var color = menu.SelectedIndex == i ? ConsoleColor.Yellow : ConsoleColor.Gray;
                Console.ForegroundColor = color;
                int topOffset = 2 + i;
                Console.SetCursorPosition(0, topOffset);

                if (i == menu.SelectedIndex)
                {
                    Console.Write("->");
                }
                else
                {
                    Console.Write("  ");
                }

                Console.SetCursorPosition(2, topOffset);
                Console.Write(menu.Items[i] + ": ");
                Console.WriteLine(menu.InputItems[i]);
            }
        }
    }
}