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
            CategoryService categoryService = new CategoryService();
            Category headOfCategory = new Category();
            categoryService.LoadCategoryList(ref headOfCategory);
            categoryService.AddNewCategory(ref headOfCategory);
            Console.Read();
        }
    }
}
