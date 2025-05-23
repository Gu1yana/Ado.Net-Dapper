﻿using Ado.Net_Dapper.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net_Dapper.Models;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public List<Product> Products { get; set; }= new List<Product>();
    public Category() { }
    public Category(string name)
    { Name = name; }
}
