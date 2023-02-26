using Microsoft.Data.SqlClient;
using System.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace _26_02_2023_sql_consol_library
{
    public class Sql_connect
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
                    Console.WriteLine("Connection is closed..."+"\n");
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
                        Console.WriteLine(data_cw = dataReader[0].ToString()+ " "+
                            dataReader[1].ToString()+ " "+ dataReader[2].ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine (e.Message);
                }
                finally
                {
                    // Закрыть соединение
                    connection.Close();
                }
            }
        }

    }
}