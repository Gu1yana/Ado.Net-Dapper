using Ado.Net_Dapper.Constants;
using Ado.Net_Dapper.Models;
using Ado.Net_Dapper.Repositories.Abstracts;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Ado.Net_Dapper.Repositories.Implements;

public class CategoryRepository : IRepository<Category>
{
    private SqlConnection _connection { get => new(ConnectionStrings.connStr); }

    public void Add(Category model)
    {
        var query ="Insert Into Categorys Values (@Name)";
        _connection.Execute(query, model);
    }
    public void Delete(int id)
    { 

        using (var db = _connection)
        {
            db.Execute("DELETE FROM Categorys WHERE Id = @Id", new { Id = id });
        }
    }

    public List<Category> GetAll()
    {
        using var db = _connection;
        var categorys = db.Query<Category>("Select * From Categorys ").ToList();
        categorys.ForEach(c => Console.WriteLine($"{c.Id} - {c.Name}"));
        return categorys;
    }
    public Category GetById(int id)
    {
        using var db = _connection;
        var category = db.QueryFirstOrDefault<Category>("SELECT * FROM Categorys WHERE Id=@id", new { id });
        if (category == null)
        {
            Console.WriteLine("Heç bir kateqoriya tapilmadi");
        }
        else
        {
            Console.WriteLine($"Kateqoriya tapildi: {category.Name}");
        }
        return category;
    }

    public void Update(int id, Category model)
    {



    }
}
