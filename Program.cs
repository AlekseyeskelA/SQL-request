using System;
using System.Data.SqlClient;
using static System.Console;

namespace SQL_request
{
    internal class Program
    {
        /* Предварительный скритп для создания таблиц базы данных:
        
        CREATE TABLE [dbo].[Products]
        (
            [Id] INT NOT NULL PRIMARY KEY,
            [Name] NVARCHAR (50) NOT NULL
        )
        
        CREATE TABLE [dbo].[Categories]
        (
            [Id] INT NOT NULL PRIMARY KEY,
            [Name] NVARCHAR (50) NOT NULL
        )

        CREATE TABLE [dbo].[ProductsCategories]
        (
            [ProductsId] INT NOT NULL ,
            [CategoriesId] INT NOT NULL,
            PRIMARY KEY ([ProductsId], [CategoriesId]),
            FOREIGN KEY (ProductsId) REFERENCES Products (Id),
            FOREIGN KEY (CategoriesId) REFERENCES Categories (Id)
        )*/
        static void Main(string[] args)
        {
            string connStr = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = usersbdProdCat;";

            using (var conn = new SqlConnection(connStr))
            {                
                conn.Open();

                string sqlString;
                SqlCommand command;


                // Заполнение таблиц данными:
                sqlString = @"
                INSERT INTO Products (Id, Name)
                VALUES  ('1', 'Oranges'),
                        ('2', 'Watermelons'),
                        ('3', 'Mandarins'),
                        ('4', 'Bananas'),
                        ('5', 'Potato'),
                        ('6', 'Onion'),
                        ('7', 'Garlic'),
                        ('8', 'Matches');";
                command = new SqlCommand(sqlString, conn);
                int rows = command.ExecuteNonQuery();
                WriteLine($"Products Updated {rows}");

                sqlString = @"
                INSERT INTO Categories (Id, Name)
                VALUES  ('1', 'Citrus fruits'),
                        ('2', 'Fruit'),
                        ('3', 'Vegetables');";
                command = new SqlCommand(sqlString, conn);
                rows = command.ExecuteNonQuery();
                WriteLine($"Categories Updated {rows}");

                sqlString = @"
                INSERT INTO ProductsCategories (ProductsId, CategoriesId)
                VALUES  ('1', '1'),
                        ('1', '2'),
                        ('2', '2'),
                        ('3', '1'),
                        ('3', '2'),
                        ('4', '2'),
                        ('5', '3'),
                        ('6', '3'),
                        ('7', '3');";
                command = new SqlCommand(sqlString, conn);
                rows = command.ExecuteNonQuery();
                WriteLine($"ProductsCategories Updated {rows}");
                WriteLine("--------------------");


                // Получение результата:
                sqlString = @"
                SELECT P.Name, C.Name
                FROM Products AS P
                LEFT JOIN ProductsCategories AS PC
                ON P.Id = PC.ProductsId
                LEFT JOIN Categories AS C
                ON PC.CategoriesId = C.Id;";
                command = new SqlCommand(sqlString, conn);
                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    WriteLine($"{reader[0]} - {reader[1]}");
                }
            }
            ReadLine();
        }
    }
}
