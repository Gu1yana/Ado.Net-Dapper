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
        var products= db.Query<Product>("Select * From Products").ToList();
        products.ForEach(p => Console.WriteLine($"{p.Id} - {p.Name} - {p.Price} AZN"));
        return products;
    }
    public Product GetById(int id)
    {
        using var db = _connection;
        var product = db.QueryFirstOrDefault<Product>("SELECT * FROM Products WHERE Id=@id", new { id });
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
            _connection.Execute($"UPDATE Products SET Name=@Name, Price=@Price", model);
        }
    }
}
