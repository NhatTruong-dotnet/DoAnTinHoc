﻿using System;
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
            categoryService.AddNewCategory(ref headOfCategory);
            Console.ReadLine();
        }
    }
}