using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
    public class Item<T>
    {
        /// <summary>
        /// Хранимые данные.
        /// </summary>
        public T Data { get; set; } //для хранения данных

        public Item<T> Next { get; set; }   //следующий элемент связного списка

        public Item(T data) //создание нового экземпляра связного списка
        {
            if (data == null)   //проверка на null
            {
                throw new ArgumentNullException(nameof(data));  //вылетает исключение
            }

            Data = data;
        }

        public override string ToString()   //приведение объекта к строке
        {
            return Data.ToString();
        }
    }
}
