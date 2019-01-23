using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Book
{
    public delegate void DelReading(string message);
    public delegate void eventBook(Journal EvBook);

    class EventBook
    {
        public event DelReading readed;
        public event eventBook BookReaded;

        public bool Active { get; set; }
        TechnicalBook TB;
        FictionBook FB;
        LinkedList<Journal> journals;
        Queue<TechnicalBook> TBook;
        Queue<FictionBook> FBook;

        public EventBook()
        {
            Active = false;
            TBook = new Queue<TechnicalBook>();
            FBook = new Queue<FictionBook>();
            journals = new LinkedList<Journal>();

            BookReaded += OnEventList;
        }

        public void OnEventList(Journal j)//добавление в отчет журнала
        {
            journals.Add(j);
        }

        public void AddBook(FictionBook current)
        {
            FBook.Enqueue(current);
            Journal JI = new Journal();
            JI.TEv = TypeEvents.Open;
            //Console.WriteLine("АЙДИШНИК " + id);
            JI.typeSender = TypeSender.Fiction;
            JI.IdBook = current.Id;
            JI.TimeEvent = DateTime.Now;
            if (BookReaded != null)
            {
                BookReaded(JI);
            }
            if (readed != null)
            {
                readed($"{current.Name} In Open");
            }
        }

        public void AddBook(TechnicalBook current)
        {
            TBook.Enqueue(current);
            Journal JI = new Journal();
            JI.TEv = TypeEvents.Open;
            //Console.WriteLine("АЙДИШНИК " + id);
            JI.typeSender = TypeSender.Technical;
            JI.IdBook = current.Id;
            JI.TimeEvent = DateTime.Now;
            if (BookReaded != null)
            {
                BookReaded(JI);
            }
            if (readed != null)
            {
                readed($"{current.Name} In Open");
            }
        }

        public void Open()                                          //начало работы с событиями
        {
            Active = true;                                          //показательная активация действий
            Thread thr;
            thr = new Thread(new ThreadStart(Reading));            //добавляем функцию в делегат
            thr.Start();                                            //запускаем делегат

        }

        public void Reading()
        {
            while (Active)
            {
                if (TBook.Count > 0)
                {
                    
                    TB = TBook.Dequeue();
                    //Thread.Sleep(timeBuilding*1000);
                    /*Thread threadBuild1 = new Thread(new ThreadStart(Build));
                    threadBuild1.Start();*/
                    Journal JIo = new Journal();
                    //Console.WriteLine("АЙДИШНИК " + id);
                   // id++;
                    JIo.IdBook = TB.Id;
                    JIo.TEv = TypeEvents.Read;
                    //JIo.TimeEvent = DateTime.Now;
                    JIo.typeSender = TypeSender.Technical;
                    BookReaded(JIo);
                    if (readed != null)
                    {
                        readed($"{TB.Name} Read");
                    }
                    Thread threadCl = new Thread(new ThreadStart(CloseBookTechno));
                    threadCl.Start();
                }
                if (FBook.Count > 0)
                {

                    FB = FBook.Dequeue();
                    //Thread.Sleep(timeBuilding*1000);
                    /*Thread threadBuild1 = new Thread(new ThreadStart(Build));
                    threadBuild1.Start();*/
                    Journal JIo = new Journal();
                    //Console.WriteLine("АЙДИШНИК " + id);
                    // id++;
                    JIo.IdBook = FB.Id;
                    JIo.TEv = TypeEvents.Read;
                    //JIo.TimeEvent = DateTime.Now;
                    JIo.typeSender = TypeSender.Fiction;
                    if (BookReaded != null)
                        BookReaded(JIo);
                    if (readed != null)
                    {
                        readed($"{FB.Name} Read");
                    }
                     Thread threadBuild = new Thread(new ThreadStart(CloseBookFict));
                     threadBuild.Start();
                }

            }
            if (readed != null)
            {
                readed("END READ");
            }
        }

        public void CloseBookTechno()
        {
            Journal JIo = new Journal();

            JIo.IdBook = TB.Id;
            JIo.TEv = TypeEvents.Close;

            JIo.typeSender = TypeSender.Technical;
            if (BookReaded != null)
                BookReaded(JIo);
            if (readed != null)
            {
                readed($"{FB.Name} Close");
            }
        }

        public void CloseBookFict()
        {
            Journal JIo = new Journal();
            
            JIo.IdBook = FB.Id;
            JIo.TEv = TypeEvents.Close;
            
            JIo.typeSender = TypeSender.Fiction;
            if(BookReaded != null)
                BookReaded(JIo);
            if (readed != null)
            {
                readed($"{FB.Name} Close");
            }
        }

        public void Close()
        {
            Active = false;
            if (readed != null)
                readed("EventBook close");
        }

        public void InXML(Journal q)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("XML.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            // создаем новый элемент user
            XmlElement JournalElem = xDoc.CreateElement("Book");
            // создаем атрибут name
            XmlAttribute nameAttr = xDoc.CreateAttribute("Event");
            XmlAttribute DateAttr = xDoc.CreateAttribute("Date");

            //  XmlElement IdElem = xDoc.CreateElement("Id");
            // XmlElement TimeOldElem = xDoc.CreateElement("TimeOld");
            //XmlElement TimeWateringElem = xDoc.CreateElement("TimeWatering");
            XmlElement TimeElem = xDoc.CreateElement("DateTime");
            // создаем текстовые значения для элементов и атрибута
            //   XmlText idText = xDoc.CreateTextNode(Convert.ToString(br.id));
            XmlText nameText = xDoc.CreateTextNode(q.TEv.ToString());
            XmlText DateText = xDoc.CreateTextNode(q.TimeEvent.ToString());
            // XmlText TimeOldText = xDoc.CreateTextNode(Convert.ToString(fl.TimeOld));
            // XmlText TimeWateringText = xDoc.CreateTextNode(Convert.ToString(fl.TimeWatering));
            //  XmlText priceText = xDoc.CreateTextNode(Convert.ToString(br.Price));

            //добавляем узлы
            nameAttr.AppendChild(nameText);
            DateAttr.AppendChild(DateText);
            //IdElem.AppendChild(idText);
            // TimeOldElem.AppendChild(TimeOldText);
            //   priceElem.AppendChild(priceText);
            JournalElem.Attributes.Append(nameAttr);
            JournalElem.Attributes.Append(DateAttr);
            //   flowerElem.AppendChild(IdElem);
            // flowerElem.AppendChild(TimeOldElem);
            // flowerElem.AppendChild(TimeWateringElem);
            //   flowerElem.AppendChild(priceElem);
            xRoot.AppendChild(JournalElem);
            xDoc.Save("XML.xml");
        }


        public void ViewEvents()
        {
            Console.WriteLine("View Journal");
            foreach (var q in journals)
            {
                InXML(q);
                SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename ='C:\Users\Nikitka\source\repos\Book\Book\BookDB.mdf'; Integrated Security = True");
                try
                {
                    
                    // Открываем подключение
                    connection.Open();
                    Console.WriteLine("Подключение открыто");
                    
                    DateTime date1 = new DateTime(2018, 12, 24);
                    DateTime date2 = new DateTime(2018, 12, 21);



                    SqlDataAdapter daAuthors = new SqlDataAdapter(
                        "select Journal_Info.TimeEvent, Journal_Info.TypeEvents, Book.Name, FictionBook.Author, FictionBook.Year from Journal_Info,FictionBook,Book where (Journal_Info.TimeEvent > date2) and (Journal_Info.TimeEvent < date1) and (Journal_Info.IdBook = Book.Id) and (FictionBook.Id = Book.IdNode)  ", connection);
                    //данные за определенный период 
                    //по типу объекта
                    DataSet dsPubs = new DataSet("BookDB");
                    daAuthors.FillSchema(dsPubs, SchemaType.Source, "Journal_Info");
                    daAuthors.Fill(dsPubs, "Journal_Info");

                    DataTable tblAuthors;
                    tblAuthors = dsPubs.Tables["Journal_Info"];

                    foreach (DataRow drCurrent in tblAuthors.Rows)
                    {
                        Console.WriteLine("{0} {1}",
                        drCurrent["Id"].ToString(),
                        drCurrent["TypeSender"].ToString());
                    }
                    
                    int Id = q.ID;
                    string TypeEvents = q.TEv.ToString();
                    DateTime TimeEvent = q.TimeEvent;
                    int IdBook = q.IdBook;
                    string TypeSender = q.typeSender.ToString();

                    string sql = string.Format("Insert Into Journal_Info" +
                           "(Id, TypeEvents, TimeEvent, IdBook, TypeSender) Values(@Id, @TypeEvents, @TimeEvent, @IdBook, @TypeSender)");

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        // Добавить параметры
                        cmd.Parameters.AddWithValue("@Id", Id);

                        cmd.Parameters.AddWithValue("@TypeEvents", TypeEvents);
                        cmd.Parameters.AddWithValue("@TimeEvent", TimeEvent);
                        cmd.Parameters.AddWithValue("@IdBook", IdBook);
                        cmd.Parameters.AddWithValue("@Typesender", TypeSender);
                   

                        cmd.ExecuteNonQuery();
                    }

                    //////////////////////////////
                    



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
                Console.WriteLine(q.ToString());
            }
        }

    }
}