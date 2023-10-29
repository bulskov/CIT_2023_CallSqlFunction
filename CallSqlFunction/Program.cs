

using CallSqlFunction;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var connectionString = "host=localhost;db=northwind;uid=bulskov;pwd=henrik";

//UseAdoFechting(connectionString);
//UseAdoProc(connectionString);
//UseAdoFromEntityFramework(connectionString);
//UseEntityFramework(connectionString);
UseEntityFrameworkToCallProcedure(connectionString);


static void UseAdoFechting(string connectionString)
{
    Console.WriteLine("Plain ADO");

    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    using var cmd = connection.CreateCommand();
    cmd.Connection = connection;
    cmd.CommandText = "select * from search('%ab%')";

    using var rdr = cmd.ExecuteReader();

    while(rdr.Read())
    {
        Console.WriteLine($"Id: {rdr.GetInt32(0)}, Name: {rdr.GetString(1)}");
    }
}

static void UseAdoProc(string connectionString)
{
    Console.WriteLine("Plain ADO - Procedure");

    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();

    using var cmd = connection.CreateCommand();
    cmd.Connection = connection;
    cmd.CommandText = "select insertcategory(101, 'name', 'desc')";

    cmd.ExecuteNonQuery();

    cmd.CommandText = "delete from categories where id = 101";
    cmd.ExecuteNonQuery();
}

static void UseAdoFromEntityFramework(string connectionString)
{
    Console.WriteLine("ADO from Entity Framework");

    using var db = new NorthwindContex(connectionString);

    using var connection = (NpgsqlConnection)db.Database.GetDbConnection();

    connection.Open();

    using var cmd = connection.CreateCommand();
    cmd.Connection = connection;
    cmd.CommandText = "select * from search('%ab%')";

    using var rdr = cmd.ExecuteReader();

    while (rdr.Read())
    {
        Console.WriteLine($"Id: {rdr.GetInt32(0)}, Name: {rdr.GetString(1)}");
    }
}

static void UseEntityFramework(string connectionString)
{
    using var db = new NorthwindContex(connectionString);


    var search = "%ab%";

    var result = db.SerchResults.FromSqlInterpolated($"select * from search({search})");

    foreach (var item in result)
    {
        Console.WriteLine(item);
    }
}

static void UseEntityFrameworkToCallProcedure(string connectionString)
{
    using var db = new NorthwindContex(connectionString);

    var id = 101;
    var name = "testing";
    var desc = "desc";

    db.Database.ExecuteSqlInterpolated($"select insertcategory({id}, {name}, {desc})");

    var category = db.Categories.Find(id);

    Console.WriteLine($"{category.Id}, {category.Name}");

    db.Remove(category);

    db.SaveChanges();
}