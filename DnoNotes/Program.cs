using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Console;

namespace DnoNotes
{
    internal class Note
    {
        public DateTime date;
        public string name;
        public string text;
    }
    internal class DnoNotes
    {
        public DateTime DateTime;
        DateTime dateTime = DateTime.Today;
        List<Note> notes = new List<Note>();
        public string Name;
        public string Text;

        public void Note(DateTime dateTime, string name, string text)
        {
            DateTime = dateTime;
            Name = name;
            Text = text;
        }
    }
    internal class Program
    {
        public static Dictionary<DateTime, string> notessync = new Dictionary<DateTime, string>();
        public static DateTime dateTime = DateTime.Today;
        public static string[,] noteslist2 = new string[0,3];
        public static List<Note> notes = new List<Note>();
        static void Main(string[] args)
        {
            CursorVisible = false;
            mainmenu();

        }
        public static void mainmenu()
        {
            Clear();
            SetCursorPosition(0, 0);
            SetWindowSize(125, 25);
            ForegroundColor = ConsoleColor.Green;
            WriteLine("Dnotes");
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Сегодня " + DateTime.Now.ToString("d' 'MMMM' 'yyyy"));
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("    List");
            WriteLine("    Add");
            WriteLine("    Delete");
            ForegroundColor = ConsoleColor.Cyan;
            Control.arrows(3, 2);
            switch (Control.selected)
            {
                case 0: PrintNotesList();  break;
                case 1: addnote(); break;
                case 2: break;
            }
        }
        public static void addnote()
        {
            Clear();
            Console.WriteLine("Введите имя заметки: ");
            string name = Console.ReadLine();
            Console.WriteLine("Введите текст заметки: ");
            string text = Console.ReadLine();
            DateTime date = DateTime.Now.Date;
            WriteLine("Введите дату:");
            int fortop = CursorTop;
            int forleft = CursorLeft;
            WriteLine($"{date.ToShortDateString()} {date.ToShortTimeString()}\n");

            bool run = true;
            while (run)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                string choosekey = Convert.ToString(key.Key);

                switch (choosekey)
                {
                    case "DownArrow":
                        {
                            date = date.AddDays(1);

                            break;
                        }
                    case "UpArrow":
                        {
                            date = date.AddDays(-1);
                            break;
                        }
                    case "LeftArrow":
                        {
                            date = date.AddMinutes(-1);
                            break;
                        }
                    case "RightArrow":
                        {
                            date = date.AddMinutes(1);
                            break;
                        }
                    case "Enter":
                        {
                            run = false;
                            break;
                        }
                }
                SetCursorPosition(forleft, fortop);
                notes.Add(new Note() { date = date, name = $"{date.ToShortDateString()} {date.ToShortTimeString()}\n", text = text });
                Write($"{date.ToShortDateString()} {date.ToShortTimeString()}\n");


            }
            notessync[date] = $"{date.ToShortTimeString()} | name \ntext\n";
            Console.WriteLine($"Заметка на {date.ToShortDateString()} время {date.ToShortTimeString()} создана");
            Console.ReadKey();
            mainmenu();
        }

        public static List<Note> notesfind(DateTime date)
        {
            List<Note> found = new List<Note>();
            foreach (Note note in notes)
            {
                if (note.date == date.Date)
                {
                    found.Add(note);
                }
            }
            return found;
        }
        public static void PrintNotesList()
        {

            Console.Clear();
            Console.ResetColor();

            DateTime date = DateTime.Now.Date;
            ConsoleKey keyPressed;
            ConsoleKeyInfo keyInfo;
            do
            {
                Console.WriteLine(date.ToString("dd'.'MM'.'yyyy"));

                List<Note> notesF = new List<Note>();

                notesF = notesfind(date);
                if (notesF.Count() > 0)
                {
                    Console.WriteLine("Заметки найдены: ");
                    notesoutput(date, false);

                    keyInfo = Console.ReadKey(true);
                    keyPressed = keyInfo.Key;
                    if (keyPressed == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Console.WriteLine("Заметки найдены: ");
                        notesoutput(date, true);
                    }
                }
                else
                    Console.WriteLine("На это число нет заметок.");

                keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.RightArrow)
                {
                    date = date.AddDays(1);
                }
                else if (keyPressed == ConsoleKey.LeftArrow)
                {
                    date = date.AddDays(-1);
                }
                Console.Clear();
            } while (keyPressed != ConsoleKey.Escape);

            Console.Clear();
            mainmenu();
        }
        public static void notesoutput(DateTime date, bool ShowText)
        {
            List<Note> notes = notesfind(date);

            foreach (Note note in notes)
            {
                Console.WriteLine(" ");
                Console.Write(" *");
                Console.ResetColor();
                Console.WriteLine(note.name + " " + note.date.ToShortDateString());

                if (ShowText)
                {
                    Console.WriteLine(note.name +"    " + note.text.Replace(@"\n", "\n    "));
                    Console.ResetColor();
                }
            }
        }
        public static class Control
        {
            public static int selector = 0;
            public static int selected;
            public static int damount = 0;
            public static List<string> options = new List<string>();
            public static void arrows(int damount, int startheight)
            {
                Console.SetCursorPosition(1, startheight);
                Console.SetCursorPosition(1, startheight);
                bool run = false;
                while (run != true)
                {
                    ConsoleKeyInfo menuchoosekey = Console.ReadKey();
                    string choosekey = (menuchoosekey.Key.ToString());
                    switch (choosekey)
                    {
                        case "UpArrow":
                            Console.SetCursorPosition(1, selector+startheight);
                            selector--;
                            break;
                        case "DownArrow":
                            Console.SetCursorPosition(1, selector+startheight);
                            selector++;
                            break;
                        case "Enter":
                            selected = selector;
                            run = true;
                            break;
                    }
                    if (selector < 0)
                    {
                        Console.Write("   ");
                        selector = damount - 1;
                    }

                    if (selector > damount - 1)
                    {
                        Console.Write("   ");
                        selector = 0;
                        Console.SetCursorPosition(1, 0);
                        Console.SetCursorPosition(1, startheight);
                    }
                    Console.Write("   ");
                    Console.SetCursorPosition(1, startheight);
                    Console.SetCursorPosition(1, selector+startheight);
                    Console.Write(selector + 1);
                }
            }
        }
    }
}
