using Ado.Net_Dapper.Models;
using Ado.Net_Dapper.Repositories.Abstracts;
using Ado.Net_Dapper.Repositories.Implements;

namespace Ado.Net_Dapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            IRepository<Category> categoryrepository = new CategoryRepository();
            //categoryrepository.GetAll();
            IRepository<Product> productrepository = new ProductRepository();
            //productrepository.GetAll();
            //productrepository.Add(new Product
            //{
            //    Name = "Şalvar",
            //    Price = 54,
            //    CategoryId = 2
            //});
            //var product = new Product
            //{
            //    Name = "Şor",
            //    Price = 12,
            //    CategoryId = 1 
            //};

            //productrepository.GetById(2);
            //categoryrepository.GetById(1);
            productrepository.Update(5, new Product
            {
                Name = "Koynek",
                Price=320,
                CategoryId=2
            });
            //productrepository.Delete(4);

            //categoryrepository.Add(new Category
            //{
            //    Name = "Mekteb levazimatlari",
            //});
            //categoryrepository.Delete(5);




        }
    }
}
