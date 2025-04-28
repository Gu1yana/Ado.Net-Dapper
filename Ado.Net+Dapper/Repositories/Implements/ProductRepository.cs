using Ado.Net_Dapper.Constants;
using Ado.Net_Dapper.Models;
using Ado.Net_Dapper.Repositories.Abstracts;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net_Dapper.Repositories.Implements;

public class ProductRepository : IRepository<Product>
{
    private SqlConnection _connection { get => new(ConnectionStrings.connStr); }
    public void Add(Product model)
    {
        using var db = _connection;
        {
            db.Execute("Insert Into Products Values (@Name,@Price, @CategoryId)", model);
        }
    }
    public void Delete(int id)
    {
        using var db = _connection;
        db.Execute("Delete From Products WHERE Id = @id", new { id });
    }
    public List<Product> GetAll()
    {
        using var db = _connection;
        var products= db.Query<Product>(
            @"Select p.Id, p.Name, p.Price, p.CategoryId, c.Name as CategoryName From Products p Join Categorys c On p.CategoryId=c.Id").ToList();
        return products;
    }
    public Product GetById(int id)
    {
        using var db = _connection;
        var product = db.QueryFirstOrDefault<Product>(@"SELECT p.Id, p.Name, p.Price, p.CategoryId, c.Name as CategoryName
                    FROM Products p JOIN Categorys c ON p.CategoryId = c.Id WHERE p.Id = @Id", new { Id=id });
        if (product == null)
        {
            Console.WriteLine("Heç bir mehsul tapilmadi");
        }
        else
        {
            Console.WriteLine($"Mehsul tapildi: {product.Name}, Qiymet: {product.Price}AZN");
        }
        return product;
    }
    public void Update(int id, Product model)
    {
        using (_connection)
        {
            _connection.Execute($"UPDATE Products SET Name=@Name, Price=@Price, CategoryId = @CategoryId Where Id=@Id", new { Name = model.Name, Price = model.Price, Id = id, CategoryId= model.CategoryId});
        }
    }
}
