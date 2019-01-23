using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
    public class Journal
    {
        private static int id = 0;
        public int ID;
        public TypeEvents TEv;//тип события 
        public DateTime TimeEvent;
        public int IdBook;
        public TypeSender typeSender;//тип объекта

        public Journal()
        {
            ID = id;
            id++;
            TimeEvent = DateTime.Now;

        }

        public override string ToString()
        {
            return string.Format($"TypeEvents - {TEv}\nDateTime - {TimeEvent}\nTypeSender - {typeSender}");
        }
    }
}
