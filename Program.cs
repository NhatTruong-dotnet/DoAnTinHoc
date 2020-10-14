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
    // hang hoa
    public class Product : IProduct
    {
        #region Properties
        public int IdProduct { get; set; }
        public string Name { get; set; }
        // so luong
        public int Amount { get; set; }
        public float Price { get; set; }
        // san pham tiep theo
        public Product NextProduct { get; set; }
        #endregion

        #region Constructor
        public Product()
        {
            NextProduct = null;
        }
        public Product(int idProduct, string name, int amount, float price)
        {
            this.IdProduct = idProduct;
            this.Name = name;
            this.Amount = amount;
            this.Price = price;
            this.NextProduct = this;
        }
        #endregion

        #region Function
        public void insertProduct(ref Product headOfProduct, Product newProduct)
        {
            // do thằng headOfCategory dùng nó làm con trỏ cho nguyên danh sách nên cần phải clone thằng khác để không thay đổi danh sách
            Product cloneOfHeadProduct = headOfProduct;
            // khi mà thằng kế tiếp nó vẫn còn
            while (cloneOfHeadProduct.NextProduct != null)
            {
                // thì dịch con trỏ clone này sang thằng kế tiếp
                cloneOfHeadProduct = cloneOfHeadProduct.NextProduct;
            }
            // khi mà thằng kế bên nó đã trống thì thêm mặt hàng mới vào (nextCategory là kiểu Category)
            cloneOfHeadProduct.NextProduct = newProduct;

        }

        public void initProductList(ref Product headOfProduct)
        {
            // lấy dữ liệu các mặt hàng có sẵn trong danh sách
            string[] productList = File.ReadAllLines(@"C:\NhatTruong\Project\DoAnTinHoc\Data.txt");
            // sure là thằng đầu tiên sẽ là con trỏ rồi nên lấy nó ra khỏi cái list mặt hàng
            string[] headOfProductContent = productList.First().Split(',');
            // bỏ đi thằng head lúc nãy, danh sách giờ chỉ còn những thằng tiếp theo của nó thôi
            productList = productList.Skip(1).ToArray();
            // nếu thằng đầu tiên rỗng thì chưa có danh sách
            if (headOfProductContent.Length == 0)
            {
                // làm luôn trường hợp khởi tạo rỗng nha
                Console.WriteLine("File empty");
            }
            else
            {
                // khởi tạo head
                headOfProduct = new Product
                {
                    IdProduct = Int32.Parse(headOfProductContent[0]),
                    Name = headOfProductContent[1].ToString(),
                    Amount = Int32.Parse(headOfProductContent[2]),
                    Price = float.Parse(headOfProductContent[3])
                };

                // nếu những thằng phía sau nó có tồn tại
                if (productList.Length != 0)
                {
                    // duyệt danh sách, mỗi dòng trong file txt sẽ tương đương với một item
                    foreach (var item in productList)
                    {
                        // tách chuỗi, để lấy thông tin
                        string[] tempCategoryContent = item.Split(',');
                        // Khởi tạo thằng category mà head liên kết tới
                        Product temp = new Product
                            (
                                Int32.Parse(tempCategoryContent[0]),
                                tempCategoryContent[1].ToString(),
                                Int32.Parse(tempCategoryContent[2]),
                                float.Parse(tempCategoryContent[3])
                            );

                        // thêm nó vào danh sách đã có
                        insertProduct(ref headOfProduct, temp);
                    }
                }
            }
        }
        #endregion 
    }

    // loai hang hoa
    public class Category : ICategory
    {

        #region Properties
        public int IdCategory { get; set; }
        public string Name { get; set; }
        // ma hang tiep theo
        public Category NextCategory { get; set; }
        // con tro san pham dau
        public Product HeadOfProduct { get; set; }
        #endregion

        #region Constructor
        public Category() // hàm khởi tạo constructor mặc định thì nó sẽ là một cục riêng biệt không tham chiếu đến cục khác cũng chưa có danh sách sản phẩm của nó
        {
            NextCategory = this;
            HeadOfProduct = null;
        }

        public Category(int idCategory) // tương tự ở trên nhưng với id cho trước 
        {
            this.IdCategory = idCategory;
            this.NextCategory = this;
            this.HeadOfProduct = null;
        }
        public Category(int idCategory, string name) // tương tự ở trên nhưng với id và name được, này được tận dụng trong hàm insert
        {
            this.IdCategory = idCategory;
            this.Name = name;
            this.NextCategory = this;
            this.HeadOfProduct = null;
        }
        #endregion

        #region Function
        // Thêm một mặt hàng mới vào danh sách
        public void insertCategory(ref Category headOfCategory, Category newCategory)
        {
            // do thằng headOfCategory dùng nó làm con trỏ cho nguyên danh sách nên cần phải clone thằng khác để không thay đổi danh sách
            Category cloneOfHeadCategory = headOfCategory;
            while (cloneOfHeadCategory.NextCategory != headOfCategory)
            {
                // thì dịch con trỏ clone này sang thằng kế tiếp
                cloneOfHeadCategory = cloneOfHeadCategory.NextCategory;
            }
            cloneOfHeadCategory.NextCategory = newCategory;
            cloneOfHeadCategory.NextCategory.NextCategory = headOfCategory;
        }

        public bool validateCategory(string newCategoryName)
        {
            bool result = false;
            string[] categoryList = File.ReadLines(@"C:\Users\Dell\Desktop\ConsoleApp2\Data.txt").First().Split(',');
            foreach (var item in categoryList)
            {
                if (newCategoryName != item)
                {
                    result = true;
                }
            }
            return result;
        }

        public void initCategoryList(ref Category headOfCategory)
        {
            // lấy dữ liệu các mặt hàng có sẵn trong danh sách
            string[] categoryList = File.ReadAllLines(@"C:\Users\Dell\Desktop\ConsoleApp2\Data.txt");
            // sure là thằng đầu tiên sẽ là con trỏ rồi nên lấy nó ra khỏi cái list mặt hàng
            string[] headOfCategoryContent = categoryList[1].Split(',');
            // bỏ đi thằng head lúc nãy, danh sách giờ chỉ còn những thằng tiếp theo của nó thôi
            categoryList = categoryList.Skip(2).ToArray();
            // nếu thằng đầu tiên rỗng thì chưa có danh sách
            if (headOfCategoryContent.Length == 0)
            {
                // làm luôn trường hợp khởi tạo rỗng nha
                Console.WriteLine("File empty");
            }
            else
            {
                // khởi tạo head
                headOfCategory = new Category { IdCategory = Int32.Parse(headOfCategoryContent[0]), Name = headOfCategoryContent[1].ToString() };
                // nếu những thằng phía sau nó có tồn tại
                if (categoryList.Length != 0)
                {
                    // duyệt danh sách, mỗi dòng trong file txt sẽ tương đương với một item
                    foreach (var item in categoryList)
                    {
                        // tách chuỗi, để lấy thông tin
                        string[] tempCategoryContent = item.Split(',');
                        // Khởi tạo thằng category mà head liên kết tới
                        Category temp = new Category(Int32.Parse(tempCategoryContent[0]), tempCategoryContent[1].ToString());
                        // thêm nó vào danh sách đã có
                        insertCategory(ref headOfCategory, temp);
                    }
                }
            }
        }

        public Category getNewCategory()
        {
            string[] lastCategory = File.ReadAllLines(@"C:\Users\Dell\Desktop\ConsoleApp2\Data.txt").Last().Split(',');
            Console.Write("Enter your category: ");
            string nameCategory = Console.ReadLine();
            int idCategory = Int32.Parse(lastCategory[0]);
            Category returnCategory = new Category(idCategory, nameCategory);
            return returnCategory;
        }

        public void addNewCategory(ref Category headOfCategory)
        {
            Category newCategory = new Category();
            newCategory = newCategory.getNewCategory();
            if (!newCategory.validateCategory(newCategory.Name))
            {
                headOfCategory.insertCategory(ref headOfCategory,newCategory);
            }
            else
            {
                Console.WriteLine("This category had been inserted");
            }
        }

        #endregion
    }
    internal class Mainclass
    {
        public static void Main()
        {
            //init CategoryList
            Category headOfCategoryList = new Category();
            headOfCategoryList.initCategoryList(ref headOfCategoryList);
            Console.WriteLine(headOfCategoryList.NextCategory.NextCategory.Name);
            headOfCategoryList.addNewCategory(ref headOfCategoryList);
            Console.ReadLine();
        }
    }
}