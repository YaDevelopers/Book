using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
    public class LinkedList<T> : IEnumerable<T>
    {
        private Item<T> _head = null;   //головной элемент списка

        private Item<T> _tail = null;   //крайний элемент списка

        
        /// Количество элементов списка.
       
        private int _count = 0;         //количество элементов списка

        public int Count                //количество элементов списка
        {
            get => _count;
        }

        public void Add(T data)//добавление данных в связный список 
        {
            if (data == null)//проверка входного элемента на null
            {
                throw new ArgumentNullException(nameof(data));  //вылетает исключение если null
            }

            var item = new Item<T>(data);   //создание нового элемента связного списка

            if (_head == null)  //проверка на пустоту
            {
                _head = item;   //положили в голову
            }
            else
            {
                _tail.Next = item;  //положили в следующий элемент
            }

            _tail = item;   //установка этого элемента последним

            _count++;   //увеличиваем счетчик
        }

        public void Delete(T data)
        {

            if (data == null)   //проверка на пустое значение
            {
                throw new ArgumentNullException(nameof(data));  //исключение!
            }


            var current = _head;    //текущий обозреваемый элемент

            Item<T> previous = null;//предыдущий элемент 

            while (current != null)
            {
                // Если данные обозреваемого элемента совпадают с удаляемыми данными,
                // то выполняем удаление текущего элемента учитывая его положение в цепочке.
                if (current.Data.Equals(data))
                {
                    // Если элемент находится в середине или в конце списка,
                    // выкидываем текущий элемент из списка.
                    // Иначе это первый элемент списка,
                    // выкидываем первый элемент из списка.
                    if (previous != null)
                    {
                        // Устанавливаем у предыдущего элемента указатель на следующий элемент от текущего.
                        previous.Next = current.Next;


                        // Если это был последний элемент списка, 
                        // то изменяем указатель на крайний элемент списка.
                        if (current.Next == null)
                        {
                            _tail = previous;
                        }
                    }
                    else
                    {
                        // Устанавливаем головной элемент следующим.
                        _head = _head.Next;

                        // Если список оказался пустым,
                        // то обнуляем и крайний элемент.
                        if (_head == null)
                        {
                            _tail = null;
                        }
                    }

                    // Элемент был удален.
                    // Уменьшаем количество элементов и выходим из цикла.
                    // Для того, чтобы удалить все вхождения данных из списка
                    // необходимо не выходить из цикла, а продолжать до его завершения.
                    _count--;
                    break;
                }

                // Переходим к следующему элементу списка.
                previous = current;
                current = current.Next;
            }
        }

        
        /// Очистить список.
       
        public void Clear()
        {
            _head = null;
            _tail = null;
            _count = 0;
        }

        
        /// Вернуть перечислитель, выполняющий перебор всех элементов в связном списке.
       
        /// <returns> Перечислитель, который можно использовать для итерации по коллекции. </returns>
        public IEnumerator<T> GetEnumerator()
        {
            // Перебираем все элементы связного списка, для представления в виде коллекции элементов.
            var current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        
        /// Вернуть перечислитель, который осуществляет итерационный переход по связному списку.
        
        /// <returns> Объект IEnumerator, который используется для прохода по коллекции. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            //возвращается перечислитель для перебора элементов через foreach .
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException();
        }


    }
}
