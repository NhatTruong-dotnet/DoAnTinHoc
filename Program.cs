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

        #region Properties
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public Category NextCategory { get; set; } // ma hang tiep theo
        public Product HeadOfProduct { get; set; } // con tro san pham dau
        #endregion

        #region Contrustor
        public Category()
        {
            NextCategory = null;
            HeadOfProduct = null;
        }

        public Category(int idCategory)
        {
            this.IdCategory = idCategory;
            this.NextCategory = null;
            this.HeadOfProduct = null;
        }
        public Category(int idCategory, string name)
        {
            this.IdCategory = idCategory;
            this.Name = name;
            this.NextCategory = null;
            this.HeadOfProduct = null;
        }

        public Category(int idCategory, string nameCategory, Category nextCategory=null, Product headOfProduct=null)
        {
            this.IdCategory = idCategory;
            this.Name = nameCategory;
            this.NextCategory = nextCategory;
            this.HeadOfProduct = headOfProduct;
        }
        #endregion

        #region Function
        internal static void insertCategory(ref Category headOfCategory ,Category newCategory) 
        {
            Category cloneOfHeadCategory = headOfCategory;
            while(cloneOfHeadCategory.NextCategory != null)
            {
                cloneOfHeadCategory = cloneOfHeadCategory.NextCategory;
            }
            cloneOfHeadCategory.NextCategory = newCategory;
        }
        internal static void initCategoryList(ref Category headOfCategory)
        {
            string[] categoryList = File.ReadAllLines(@"C:\NhatTruong\Project\DoAnTinHoc\Data.txt");
            string[] headOfCategoryContent = categoryList.First().Split(',');
            categoryList = categoryList.Skip(1).ToArray();
            if (headOfCategoryContent.Length == 0)
            {
                Console.WriteLine("File empty");
            }
            else
            {
                headOfCategory = new Category { IdCategory = Int32.Parse(headOfCategoryContent[0]), Name = headOfCategoryContent[1].ToString() };
                if (categoryList.Length != 0)
                {
                    foreach (var item in categoryList)
                    {
                        string[] tempCategoryContent = item.Split(',');
                        Category temp = new Category(Int32.Parse(tempCategoryContent[0]), tempCategoryContent[1].ToString());
                        insertCategory(ref headOfCategory, temp);
                    }
                }
            }

        }
    }
    #endregion

    internal class Mainclass
    {
        public static void Main()
        {
            Category headOfCategoryList = new Category() ;
            Category.initCategoryList(ref headOfCategoryList);
            Console.WriteLine(headOfCategoryList.Name);                                        
            Console.WriteLine(headOfCategoryList.NextCategory.Name);                                        
            Console.WriteLine(headOfCategoryList.NextCategory.NextCategory.Name);
            Console.WriteLine(headOfCategoryList.NextCategory.NextCategory.NextCategory.Name);
            Console.WriteLine(headOfCategoryList.NextCategory.NextCategory.NextCategory.NextCategory.Name);
            Console.WriteLine(headOfCategoryList.NextCategory.NextCategory.NextCategory.NextCategory.Name);
            Console.ReadLine();
        }
    }
}
