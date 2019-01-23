using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
    class FictionBook : Book,Description//Художественная литература
    {
        private static int id = 0;
        public int ID;
        public string Author { get; set; }//автор
        public int Year { get; set; }//год написания


        public FictionBook(string Name, int Size, int Popular, string Author, int Year) : base(Name, Size, Popular, id, TypeSender.Fiction)
        {
            this.Author = Author;
            this.Year = Year;
            ID = id;
            id++;

            inDataBase();
        }

        public void readme()
        {
            Console.WriteLine("Предназначена для поднятия духа, для принятия другого мира в свою душу)))))");
        }

        public FictionBook()
        {
            throw new Exception("Не хватает данных");
        }

        public void inDataBase()
        {
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename ='C:\Users\Nikitka\source\repos\Book\Book\BookDB.mdf'; Integrated Security = True");
            try
            {
                // Открываем подключение
                connection.Open();
                Console.WriteLine("Подключение открыто");



                int ID = Id;
                
                string Name = this.Name;
                int Size = this.Size;
                string Author = this.Author;
                int Year = this.Year;


                string sql = string.Format("Insert Into FictionBook" +
                       "(Id,  Name, Size, Author, Year) Values(@Id, @Name, @Size, @Author, @Year)");

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    // Добавить параметры
                    cmd.Parameters.AddWithValue("@Id", Id);
              
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Size", Size);
                    cmd.Parameters.AddWithValue("@Author", Author);
                    cmd.Parameters.AddWithValue("@Year", Year);

                    cmd.ExecuteNonQuery();
                }



            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // закрываем подключение
                connection.Close();
                Console.WriteLine("Подключение закрыто...");
            }
        }

        public override string ToString()
        {
            return $"!!!\nName - {Name}\nSize - {Size}\nPopular - {Popular}\nAuthor - {Author}\nYear - {Year}\n!!!";
        }
    }
}
