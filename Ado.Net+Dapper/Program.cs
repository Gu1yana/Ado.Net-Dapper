using Ado.Net_Dapper.Exceptions;
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
            IRepository<Product> productrepository = new ProductRepository();
            try
            {
                while (true)
                {
                    Console.WriteLine("=====Product Menu=====\r\n[1] Product elave et\r\n[2] Product update et\r\n[3] Product sil\r\n[4] Butun Product-lari goster\r\n[5] Product-u Id-e gore tap\r\n[6] Category elave et\r\n[7] Category update et\r\n[8] Category sil\r\n[9] Butun Category-leri goster\r\n[10] Category-ni Id-e gore tap\r\n[0] Cixish\r\nSecim edin:");
                    int input = int.Parse(Console.ReadLine());
                    //Console.Clear();
                    switch (input)
                    {
                        case 0:
                            Console.WriteLine("Proqram dayandirildi.");
                            return;
                        case 1:
                            AppProductShortCut(categoryrepository, productrepository);
                            break;
                        case 2:
                            UpdateProductShorCut(categoryrepository, productrepository);
                            break;
                        case 3:
                            ShortcutShow(categoryrepository, productrepository);
                            Console.WriteLine("Silmek istediyiniz mehsulun Id-ni daxil edin:");
                            int DeleteId = int.Parse(Console.ReadLine());
                            productrepository.Delete(DeleteId);
                            Console.WriteLine("Mehsul ugurla silindi.");
                            break;
                        case 4:
                            ShortcutShow(categoryrepository, productrepository);
                            break;
                        case 5:
                            Console.WriteLine("Mehsul Id-si daxil edin:");
                            int prodId = int.Parse(Console.ReadLine());
                            productrepository.GetById(prodId);
                            break;
                        case 6:
                            AddCategoryShortcut(categoryrepository);
                            break;
                        case 7:
                            UpdateCategoryShortCut(categoryrepository);
                            break;
                        case 8:
                            break;
                        case 9:
                            categoryrepository.GetAll();
                            break;
                        case 10:
                            Console.WriteLine("Id-i daxil edin:");
                            int findCategory = int.Parse(Console.ReadLine());
                            try
                            {
                                var category = categoryrepository.GetById(findCategory);
                                Console.WriteLine($"Kateqoriya : {category.Name}");
                            }
                            catch
                            {
                                throw new CategoryNotFoundException("Bu Id-de kateqoriya movcud deyil.");
                            }
                            break;
                        default:
                            throw new InvalidChoiceException("Yanlish secim, zehmet olmasa bir dahakine 0 - 10 arasi secim edin.");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void UpdateCategoryShortCut(IRepository<Category> categoryrepository)
        {
            categoryrepository.GetAll();
            Console.WriteLine("Yenilemek istediyiniz kateqoriya Id-ni daxil edin:");
            int upCategory = int.Parse(Console.ReadLine());
            Console.WriteLine("Yeni kateqoriya adi daxil edin:");
            string categoryName = Console.ReadLine();
            categoryrepository.Update(upCategory, new Category(categoryName));
            Console.WriteLine("Kateqoriya ugurla yenilendi.");
        }
        private static void AddCategoryShortcut(IRepository<Category> categoryrepository)
        {
            Console.WriteLine("Yeni kateqoriya elave edin:");
            Console.WriteLine("Kateqoriya adini daxil edin:");
            string cname = Console.ReadLine();
            {
                if (String.IsNullOrEmpty(cname))
                {
                    throw new EmptyCategoryNameException("Bosh kateqoriya adi daxil edile bilmez!!!");
                }
                else
                {
                    var existingCategory = categoryrepository.GetAll().FirstOrDefault(c => c.Name == cname);
                    if (existingCategory != null)
                    {
                        throw new CategoryAlreadyException("Eyni adda ikinci bir kateqoriya daxil edile bilmez!!!");
                    }
                    else
                    {
                        var category = new Category(cname);
                        Console.WriteLine("Kateqoriya ugurla elave edildi.");
                        categoryrepository.Add(category);
                    }
                }
            }
        }
        private static void UpdateProductShorCut(IRepository<Category> categoryrepository, IRepository<Product> productrepository)
        {
            Console.WriteLine("Yenilemek istediyiniz mehsulun Id-ni daxil edin:");
            int productId = int.Parse(Console.ReadLine());
            var productToUpdate = productrepository.GetAll().FirstOrDefault(p => p.Id == productId);
            if (productToUpdate == null)
            {
                Console.WriteLine($"Id {productId} ilə məhsul tapılmadı.");
            }
            else
            {
                Console.WriteLine("Yeni mehsul adi daxil edin:");
                string upName = Console.ReadLine();
                if (String.IsNullOrEmpty(upName))
                {
                    throw new EmptyProductNameException("Bosh mehsul adi daxil edile bilmez!!!");
                }
                else
                {
                    Console.WriteLine("Qiymet daxil edin:");
                    decimal upPrice = decimal.Parse(Console.ReadLine());
                    Console.WriteLine("Movcud Kateqoriyalar:");
                    var categoriesUp = categoryrepository.GetAll();
                    if (categoriesUp.Count == 0)
                    {
                        Console.WriteLine("Heç bir kateqoriya movcud deyil.");
                        return;
                    }
                    Console.WriteLine("Mehsulun aid oldugu kateqoriyanin Id-ni secin:");
                    int categoriIdUp = int.Parse(Console.ReadLine());
                    if (categoriIdUp > categoriesUp.Count)
                    {
                        Console.WriteLine("Bu Id-li kateqoriya yoxdur!!!");
                    }
                    else
                    {
                        var product = new Product(upName, upPrice, categoriIdUp);
                        productrepository.Update(productId, product);
                    }
                }
            }
        }
        private static void AppProductShortCut(IRepository<Category> categoryrepository, IRepository<Product> productrepository)
        {
            Console.WriteLine("Yeni mehsul elave edin:");
            Console.WriteLine("Mehsulun adini daxil edin:");
            string name = Console.ReadLine();
            if (String.IsNullOrEmpty(name))
            {
                throw new EmptyProductNameException("Bosh mehsul adi daxil edile bilmez!!!");
            }
            else
            {
                Console.WriteLine("Mehsulun qiymetini daxil edin:");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Movcud Kateqoriyalar:");
                var categories = categoryrepository.GetAll();
                if (categories.Count == 0)
                {
                    Console.WriteLine("Heç bir kateqoriya movcud deyil.");
                    return;
                }
                Console.WriteLine("Mehsulun aid oldugu kateqoriyanin Id-ni secin:");
                int categoriId = int.Parse(Console.ReadLine());
                if (!categories.Any(c => c.Id == categoriId))
                {
                    Console.WriteLine("Bu Id-li kateqoriya yoxdur!!!\r\nSecime yeniden bashlayin:\r\n");
                }
                else
                {
                    var product = new Product(name, price, categoriId);
                    productrepository.Add(product);
                    Console.WriteLine("Mehsul ugurla elave edildi.");
                }
            }
        }
        private static void ShortcutShow(IRepository<Category> categoryrepository, IRepository<Product> productrepository)
        {
            var products = productrepository.GetAll();
            foreach (var product in products)
            {
                var category = categoryrepository.GetById(product.CategoryId);
                Console.WriteLine($"{product.Id}-{product.Name}-{product.Price}-{category.Name}");
            }
        }
    }
}
