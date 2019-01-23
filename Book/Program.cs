using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Book
{
    class Program
    {
        public static void OnCreating(string message)
        {
            Console.WriteLine($"Event {message} {DateTime.Now}");
        }

        static void Main(string[] args)
        {
            Random rnd = new Random();
            EventBook read = new EventBook();
            read.readed += OnCreating;
            read.Open();
            for (int i = 0; i <= 1; i++)
            {
                Thread.Sleep(rnd.Next(3000));
                if (i % 2 == 0)
                {
                    read.AddBook(new FictionBook("Fbook", 12,23,"ret", 1989));
                }
                else

                {
                    read.AddBook(new TechnicalBook("Tbook", 12213, 2123, typeLit.Math,"ds"));
                    Console.WriteLine("input else");
                }
                Thread.Sleep(5000);
                read.Close();
                read.ViewEvents();


                //Console.Read();

            }
        }
    }
}
