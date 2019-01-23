using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
    public abstract class Book//ребята приходят и читают книги 
    {
        private static int id = 0;
        public int Id;
        public int IdNode;
        public string Name;
        public int Size;
        public int Popular;
        public TypeSender ts;

        public Book(string Name, int Size, int Popular, int IdNode, TypeSender ts)
        {
            this.Name = Name;
            this.Size = Size;
            this.Popular = Popular;
            this.IdNode = IdNode;
            this.ts = ts;
            Id = id;
            id++;
            AddDB();
        }

        public Book()
        {
            throw new Exception("Нет необходимых значений");
        }

        public void AddDB()
        {
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename ='C:\Users\Nikitka\source\repos\Book\Book\BookDB.mdf'; Integrated Security = True");
            try
            {
                // Открываем подключение
                connection.Open();
                Console.WriteLine("Подключение открыто");



                int ID = Id;
                int IdNode = this.IdNode;
                string Name = this.Name;
                int Size = this.Size;
                int Popular = this.Popular;
                string typeSender = ts.ToString();


                string sql = string.Format("Insert Into Book" +
                       "(Id, IdNode, Name, Size, Popular, typeSender) Values(@Id, @IdNode, @Name, @Size, @Popular, @typeSender)");

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    // Добавить параметры
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@IdNode", IdNode);
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Size", Size);
                    cmd.Parameters.AddWithValue("@Popular", Popular);
                    cmd.Parameters.AddWithValue("@typeSender", typeSender);

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
            return $"Name - {Name}\nSize - {Size}\nPopular - {Popular}";
        }
    }
}
