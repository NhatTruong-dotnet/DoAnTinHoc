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
=======
<<<<<<< HEAD
>>>>>>> 4cdcb1ac3dbcc431ab4a6221d0ad57a786237520
            Product headOfProduct = new Product();
            ProductService productService = new ProductService();
            productService.LoadProductList(ref headOfProduct);
            productService.AddNewProduct(ref headOfProduct);
<<<<<<< HEAD
            Console.WriteLine(headOfProduct.Name);
=======
            productService.UpdateProduct(ref headOfProduct);
           // Console.WriteLine(headOfProduct.Name);
=======
            //Product headOfProduct = new Product();
            //ProductService productService = new ProductService();
            //productService.LoadProductList(ref headOfProduct);
            //productService.UpdateCategory(ref headOfProduct);
            //Console.WriteLine(headOfProduct.Name);
>>>>>>> 25b7c4747951d9b8863fea56f2389f4f3a46f950
>>>>>>> 4cdcb1ac3dbcc431ab4a6221d0ad57a786237520

            //Category headOfCategory = new Category();
            //CategoryService categoryService = new CategoryService();
            //categoryService.LoadCategoryList(ref headOfCategory);
            //categoryService.AddNewCategory(ref headOfCategory);
            //Console.WriteLine(headOfCategory.Name);
            Console.ReadLine();
        }
    }
}