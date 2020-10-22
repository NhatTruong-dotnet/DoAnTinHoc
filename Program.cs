using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DoAnTinHoc
{

    internal class Mainclass
    {
        public static void Main()
        {
            Category headOfCategory = new Category();
            CategoryService categoryService = new CategoryService();
            categoryService.LoadCategoryList(ref headOfCategory);
            ProductRepo productRepo = new ProductRepo();
            LinkedList<Product> productList = productRepo.GetProductByName(ref headOfCategory, "bikeee");
            foreach (var item in productList)
            {
                Console.WriteLine(item.IdProduct);
                Console.WriteLine(item.Name);
                Console.WriteLine(item.Amount);
                Console.WriteLine(item.Price);
            }
            Console.Read();
        }
    }
}
