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

            Product headOfProduct = new Product();
            ProductService productService = new ProductService();
            productService.LoadProductList(ref headOfProduct);
<<<<<<< HEAD
            productService.ShowProduct();
            productService.UpdateProduct(ref headOfProduct);
           // Console.WriteLine(headOfProduct.Name);
=======
            productService.AddNewProduct(ref headOfProduct);

            Console.WriteLine(headOfProduct.Name);
            productService.UpdateProduct(ref headOfProduct);
           // Console.WriteLine(headOfProduct.Name);

            //Product headOfProduct = new Product();
            //ProductService productService = new ProductService();
            //productService.LoadProductList(ref headOfProduct);
            //productService.UpdateCategory(ref headOfProduct);
            //Console.WriteLine(headOfProduct.Name);
>>>>>>> 2adf36ebb52976d404c8b896225e55fd9389913c

            //Category headOfCategory = new Category();
            //CategoryService categoryService = new CategoryService();
            //categoryService.LoadCategoryList(ref headOfCategory);
<<<<<<< HEAD
            //categoryService.UpdateCategory(ref headOfCategory);
            ////Console.WriteLine(headOfCategory.Name);
=======
            //categoryService.AddNewCategory(ref headOfCategory);
            //Console.WriteLine(headOfCategory.Name);
>>>>>>> 2adf36ebb52976d404c8b896225e55fd9389913c
            Console.ReadLine();
        }
    }
}