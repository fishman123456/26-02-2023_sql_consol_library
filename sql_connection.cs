using Microsoft.Data.SqlClient;
using System.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.ComponentModel;

namespace _26_02_2023_sql_consol_library
{
    public class Sql_work
    {
        // Метод, который создает SQL-соединение с базой данных
        public static void Create_SQL_Connection()
        {
            Console.WriteLine("запущен метод Create_SQL_Connection()");
            // Create SQL Connection
            // 1. Строка соединения с базой данных
            // первая проблема не было скобок решал 1 час пиииии
            string connStr = @"Data Source = (localdb)\MSSQLLocalDB; " +
        "Initial Catalog = Library; Integrated Security = True";
            //"Connect Timeout=30;Encrypt=False;" +
            //"TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
            //"MultiSubnetFailover=False";

            // 2. Создать подключение на основе строки соединения
            using (SqlConnection connection =
              new SqlConnection(connStr))
            {

                try
                {
                    // Открыть подключение
                    connection.Open();
                    Console.WriteLine("Connection is open. ");
                    Console.WriteLine("Database: " + connection.Database);
                    Console.WriteLine("DataSource: " + connection.DataSource);
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    // Закрыть подключение
                    connection.Close();
                    Console.WriteLine("Connection is closed..." + "\n");
                }

            }
        }

        public static void Get_Data_From_Data_Base()
        {
            Console.WriteLine("запущен метод Get_Data_From_Data_Base()");
            // Читает строки из базы данных
            // 1. Строка запроса
            string queryStr = "SELECT * FROM [dbo].[Authors]";

            // 2. Строка подключения
            string connStr = @"Data Source = (localdb)\MSSQLLocalDB; " +
         "Initial Catalog = Library; Integrated Security = True";

            // 3. Получить данные
            using (SqlConnection connection =
              new SqlConnection(connStr))
            {
                try
                {
                    // создать команду на языке SQL
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = queryStr;
                    command.CommandType = CommandType.Text;

                    // Открыть соединение
                    connection.Open();

                    // Получить данные - класс DDataReader
                    DbDataReader dataReader = command.ExecuteReader();

                    // Вывести данные в listBox1
                    //listBox1.Items.Clear();
                    while (dataReader.Read())
                    {

                        string data_cw;
                        //listBox1.Items.Add(dataReader[0].ToString() + " - " +
                        //  dataReader[1].ToString());
                        Console.WriteLine(data_cw = dataReader[0].ToString() + " " +
                            dataReader[1].ToString() + " " + dataReader[2].ToString());

                        //Console.WriteLine(dataReader.ToString());
                        //выводит в консоль Microsoft.Data.SqlClient.SqlDataReader
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    // Закрыть соединение
                    connection.Close();
                }
            }
        }

        public static void Add_data_for_table()
        {
            /*Объявляем строковую переменную и записываем в нее
             строку подключения 
             Data Source - имя сервера, по стандарту (local)\SQLEXPRESS
             Initial Catalog - имя БД 
             Integrated Security=-параметры безопасности
             */

            Console.WriteLine("запущен метод Add_data_for_table() \n");
            // Читает строки из базы данных
            // 1. Строка запроса
            string queryStr = "SELECT * FROM [dbo].[Authors]";
            string not_thousand = "ALTER DATABASE SCOPED CONFIGURATION " +
                "SET IDENTITY_CACHE = OFF";

            // 2. Строка подключения
            string connStr = @"Data Source = (localdb)\MSSQLLocalDB; " +
         "Initial Catalog = Library; Integrated Security = True";
            /*Здесь указал имя БД(хотя для создания БД его указывать не нужно)
              для того, чтобы проверить, может данная БД уже создана
            Создаем экземпляр класса  SqlConnection по имени conn
            и передаем конструктору этого класса, строку подключения
             */
            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                //пробуем подключится
                conn.Open();
            }
            catch (SqlException se)
            {
                Console.WriteLine("Ошибка подключения:{0}", se.Message);
                return;
            }

            Console.WriteLine("Соедение успешно произведено");
            /*Создаем экземпляр класса  SqlCommand по имени cmdCreateTable
             и передаем конструктору этого класса, запрос на 
             добавление строки в  таблицу Students
             и объект типа SqlConnection
            */
            SqlCommand cmd_n = new SqlCommand(not_thousand, conn);
            SqlCommand cmd = new SqlCommand("Insert into Authors" +
            "( FirstName, LastName) Values" +
            " ( @FirstName, @LastName)", conn);
            /*Работаем с параметрами(SqlParameter), эта техника позволяет уменьшить
            кол-во ошибок и достичь большего быстродействия
             но требует и больших усилий в написании кода*/
            //объявляем объект класса SqlParameter
            SqlParameter param = new SqlParameter();

            //переопределяем объект класса SqlParameter
            param = new SqlParameter();
            //задаем имя параметра
            param.ParameterName = "@FirstName";
            //задаем значение параметра
            Console.WriteLine("Введите имя для добавления в таблицу:  ");
            param.Value = Console.ReadLine();
            //задаем тип параметра
            param.SqlDbType = SqlDbType.NVarChar;
            //передаем параметр объекту класса SqlCommand
            cmd.Parameters.Add(param);

            //переопределяем объект класса SqlParameter
            param = new SqlParameter();
            //задаем имя параметра
            param.ParameterName = "@LastName";
            //задаем значение параметра
            Console.WriteLine("Введите фамилию для добавления в таблицу:  ");
            param.Value = Console.ReadLine();
            //задаем тип параметра
            param.SqlDbType = SqlDbType.NVarChar;
            //передаем параметр объекту класса SqlCommand
            cmd.Parameters.Add(param);

            Console.WriteLine("Вставляем запись");
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Ошибка, при выполнении запроса на добавление записи");
                return;
            }
            //Выводим значение на экран
            cmd = new SqlCommand("Select * From Authors", conn);

            /*Метод ExecuteReader() класса SqlCommand возврашает
             объект типа SqlDataReader, с помошью которого мы можем
             прочитать все строки, возврашенные в результате выполнения запроса
             CommandBehavior.CloseConnection - закрываем соединение после запроса
             */
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                //цикл по всем столбцам полученной в результате запроса таблицы
                for (int i = 0; i < dr.FieldCount; i++)
                    /*метод GetName() класса SqlDataReader позволяет получить имя столбца
                     по номеру, который передается в качестве параметра, данному методу
                     и озночает номер столбца в таблице(начинается с 0)
                     */
                    Console.Write("{0}\t", dr.GetName(i).ToString().Trim());
                /*читаем данные из таблицы
                 чтение происходит только в прямом направлении
                 все прочитаные строки отбрасываюся */
                Console.WriteLine();
                while (dr.Read())
                {
                    /*метод GetValue() класса SqlDataReader позволяет получить значение столбца
                                            по номеру, который передается в качестве параметра, данному методу
                                            и озночает номер столбца в таблице(начинается с 0)
                                            */
                    Console.WriteLine("{0}\t{1}\t{2}", dr.GetValue(0).ToString().Trim(),
                     dr.GetValue(1).ToString().Trim(),
                     dr.GetValue(2).ToString().Trim());
                }
            }
            //закрвываем соединение
            conn.Close();
            conn.Dispose();
            Console.WriteLine();
        }

        public static void Del_data_for_table()
        {
            Console.WriteLine("запущен метод Del_data_for_table() \n");

            string connStr = @"Data Source = (localdb)\MSSQLLocalDB; " +
      "Initial Catalog = Library; Integrated Security = True";

            SqlConnection conn = new SqlConnection(connStr);
            try
            {
                //пробуем подключится
                conn.Open();
            }
            catch (SqlException se)
            {
                Console.WriteLine("Ошибка подключения:{0}", se.Message);
                return;
            }

            Console.WriteLine("Соедение успешно произведено");
            /*Создаем экземпляр класса  SqlCommand по имени cmdCreateTable
             и передаем конструктору этого класса, запрос на 
             удаление строк таблицы Students, которые отвечают условиям
             и объект типа SqlConnection
            */

            SqlCommand cmd = new SqlCommand("Delete From Authors" +
                " where Id = @Id ", conn);
            /*Работаем с параметрами(SqlParameter), эта техника позволяет уменьшить
              кол-во ошибок и достичь большего быстродействия
               но требует и больших усилий в написании кода*/
            //объявляем объект класса SqlParameter
            SqlParameter param = new SqlParameter();
            //задаем имя параметра
            param.ParameterName = "@Id";
            //задаем значение параметра
            Console.WriteLine("\n Введите номер записи для удаления: ");
            int id_db;
            
            while (!int.TryParse(Console.ReadLine(), out id_db))
                {
                Console.WriteLine("Введите число которое в списке Id");
                //Console.ReadLine();
            }
            param.Value = id_db;

            //задаем тип параметра
            param.SqlDbType = SqlDbType.Int;
            //передаем параметр объекту класса SqlCommand


            cmd.Parameters.Add(param);

            Console.WriteLine("Удаляем запись");
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Ошибка, при выполнении запроса на удаление записи");
                Console.WriteLine("Возможно запись уже удалена");

                return;
            }
            //Выводим значение на экран
            cmd = new SqlCommand("Select * From Authors", conn);
            /*Метод ExecuteReader() класса SqlCommand возврашает
             объект типа SqlDataReader, с помошью которого мы можем
             прочитать все строки, возврашенные в результате выполнения запроса
             CommandBehavior.CloseConnection - закрываем соединение после запроса
             */
            using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                //цикл по всем столбцам полученной в результате запроса таблицы
                for (int i = 0; i < dr.FieldCount; i++)
                    /*метод GetName() класса SqlDataReader позволяет получить имя столбца
                     по номеру, который передается в качестве параметра, данному методу
                     и озночает номер столбца в таблице(начинается с 0)
                     */
                    Console.Write("{0}\t", dr.GetName(i).ToString().Trim());
                /*читаем данные из таблицы
                 чтение происходит только в прямом направлении
                 все прочитаные строки отбрасываюся */
                Console.WriteLine();
                while (dr.Read())
                {
                    /*метод GetValue() класса SqlDataReader позволяет 
                     * получить значение столбца
                      по номеру, который передается в качестве параметра, данному методу
                      и озночает номер столбца в таблице(начинается с 0)
                                            */
                    Console.WriteLine("{0}\t{1}\t{2}", dr.GetValue(0).ToString().Trim(),
                     dr.GetValue(1).ToString().Trim(),
                     dr.GetValue(2).ToString().Trim());
                }
            }
            //закрвываем соединение
            conn.Close();
            conn.Dispose();
            Console.WriteLine();
        }

    }
}

