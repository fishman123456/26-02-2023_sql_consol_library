// начал 26.02.2023 в 15-31
//Создать базу данных Library в localDB, далее использую запрос
//"sqlLibrary" создать две таблицы в этой базе данных.

//Написать для этой базы данных консольное приложение, в которому есть следующие методы:

// 1.  добавляние данных в таблицы, которые ввел пользователь в консоль;
// 2.  подсчет количества авторов и количества книг в таблице Authors;
// 3.  вычисление суммы всех книг и суммы страниц всех книг в таблице Books.
// 4.  Методы вычисления суммы всех книг и страниц должны выводить значения
//всех полей на каждой итерации цикла, а в конце вывести суммарные значения.
// 5.  Используйте не только ExecuteNonQuery(), но и другие методы.
// Сброс набирания 1000 к id
//ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = OFF

using _26_02_2023_sql_consol_library;

using Microsoft.Data.SqlClient;
using System.Data;

Sql_work.Create_SQL_Connection();
Sql_work.Get_Data_From_Data_Base();
Sql_work.Add_data_for_table();
Sql_work.Del_data_for_table();