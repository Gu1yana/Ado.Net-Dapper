using Ado.Net_Dapper.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net_Dapper.Models;

public class Product:BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public Product() { }
    public Product(string name, decimal price, int categoryId)
    { 
        Name = name;
        Price = price;
        CategoryId = categoryId;
    }
}
