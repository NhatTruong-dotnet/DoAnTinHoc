using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    internal class Product // hang hoa
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; } // so luong
        public float Price { get; set; }
        public Product NextProduct { get; set; } // san pham tiep theo

        public Product()
        {
            NextProduct = this;
        }
        
    }


    internal class Category // loai hang hoa
    {
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public Category NextCategory { get; set; } // ma hang tiep theo
        public Product HeadOfProduct { get; set; } // con tro san pham dau

        public Category()
        {
            NextCategory = this;
            HeadOfProduct = null;
        }

        public Category(int idCategory, string name)
        {
            this.IdCategory = idCategory;
            this.Name = name;
            this.NextCategory = this;
            this.HeadOfProduct = null;
        }
        internal static void initCategoryList(ref Category headOfCategory)
        {
            string text = File.ReadAllText(@"C:\NhatTruong\Project\DoAnTinHoc\Data.txt");
            if (String.IsNullOrEmpty(text))
            {
                Console.WriteLine("File empty");
            }
            else
            {
                string[] textArr = text.Split('\n');
                string[] headOfCategoryContent = textArr[0].Split(',');
                Console.WriteLine(typeof (headOfCategory));
                headOfCategory = new Category();
                
            }
        }
    }

    internal class Mainclass
    {
        public static void Main()
        {
            Category headOfCategoryList = new Category() ;
            Category.initCategoryList(ref headOfCategoryList);
            Console.ReadLine();
        }
    }
}
