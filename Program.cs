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

<<<<<<< HEAD
            Product headOfProduct = new Product();
            ProductService productService = new ProductService();
            productService.LoadProductList(ref headOfProduct);
            productService.AddNewProduct(ref headOfProduct);
            productService.UpdateProduct(ref headOfProduct);
           // Console.WriteLine(headOfProduct.Name);
=======
            //Product headOfProduct = new Product();
            //ProductService productService = new ProductService();
            //productService.LoadProductList(ref headOfProduct);
            //productService.UpdateCategory(ref headOfProduct);
            //Console.WriteLine(headOfProduct.Name);
>>>>>>> 25b7c4747951d9b8863fea56f2389f4f3a46f950

            Category headOfCategory = new Category();
            CategoryService categoryService = new CategoryService();
            categoryService.LoadCategoryList(ref headOfCategory);
            categoryService.UpdateCategory(ref headOfCategory);
            //Console.WriteLine(headOfCategory.Name);
            Console.ReadLine();
        }
    }
}