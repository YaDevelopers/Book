using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
    class TechnicalBook : Book, Description//техническая литература
    {
        private static int id = 0;
        public int ID;
        public typeLit tL;
        public string Level;//сложность освоения

        public TechnicalBook(string Name, int Size, int Popular, typeLit tL, string Level) : base(Name,Size,Popular, id, TypeSender.Technical)
        {
            this.tL = tL;
            this.Level = Level;
            ID = id;
            id++;

            inDataBase();
        }

        public TechnicalBook()
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
                int Popular = this.Popular;
                string typeLiterature = tL.ToString();
                string Level = this.Level;


                string sql = string.Format("Insert Into TechnoBook1" +
                       "(Id,  Name, Size, Popular, TypeLiterature, Level) Values(@Id, @Name, @Size, @Popular, @typeLiterature, @Level)");

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    // Добавить параметры
                    cmd.Parameters.AddWithValue("@Id", Id);

                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Size", Size);
                    cmd.Parameters.AddWithValue("@Popular", Popular);
                    cmd.Parameters.AddWithValue("@typeLiterature", typeLiterature);
                    cmd.Parameters.AddWithValue("@Level", Level);

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

        public void readme()
        {
            Console.WriteLine("Предназначена для освоения сторонних дисциплин связанных с техническими предметами");
        }

        public override string ToString()
        {
            return $"!!!\nName - {Name}\nSize - {Size}\nPopular - {Popular}\nLVL - {Level}\nType Technical Literature - {tL}\n!!!";
        }
    }
}
