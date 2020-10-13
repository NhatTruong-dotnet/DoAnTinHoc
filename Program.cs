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
        #region Properties
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; } // so luong
        public float Price { get; set; }
        public Product NextProduct { get; set; } // san pham tiep theo
        #endregion

        #region Constructor
        public Product()
        {
            NextProduct = this;
        }
        #endregion
    }

    internal class Category // loai hang hoa
    {

        #region Properties
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public Category NextCategory { get; set; } // ma hang tiep theo
        public Product HeadOfProduct { get; set; } // con tro san pham dau
        #endregion

        #region Constructor
        public Category() // hàm khởi tạo constructor mặc định thì nó sẽ là một cục riêng biệt không tham chiếu đến cục khác cũng chưa có danh sách sản phẩm của nó
        {
            NextCategory = null;
            HeadOfProduct = null;
        }

        public Category(int idCategory) // tương tự ở trên nhưng với id cho trước 
        {
            this.IdCategory = idCategory;
            this.NextCategory = null;
            this.HeadOfProduct = null;
        }
        public Category(int idCategory, string name) // tương tự ở trên nhưng với id và name được, này được tận dụng trong hàm insert
        {
            this.IdCategory = idCategory;
            this.Name = name;
            this.NextCategory = null;
            this.HeadOfProduct = null;
        }
        #endregion

        #region Function
        internal static void insertCategory(ref Category headOfCategory ,Category newCategory) // Thêm một mặt hàng mới vào danh sách
        {
            Category cloneOfHeadCategory = headOfCategory; // do thằng headOfCategory dùng nó làm con trỏ cho nguyên danh sách nên cần phải clone thằng khác để không thay đổi danh sách
            while (cloneOfHeadCategory.NextCategory != null) // khi mà thằng kế tiếp nó vẫn còn
            {
                cloneOfHeadCategory = cloneOfHeadCategory.NextCategory; // thì dịch con trỏ clone này sang thằng kế tiếp
            }
            cloneOfHeadCategory.NextCategory = newCategory; // khi mà thằng kế bên nó đã trống thì thêm mặt hàng mới vào (nextCategory là kiểu Category)
        }

        internal static void initCategoryList(ref Category headOfCategory)
        {
            string[] categoryList = File.ReadAllLines(@"C:\NhatTruong\Project\DoAnTinHoc\Data.txt"); // lấy dữ liệu các mặt hàng có sẵn trong danh sách
            string[] headOfCategoryContent = categoryList.First().Split(','); // sure là thằng đầu tiên sẽ là con trỏ rồi nên lấy nó ra khỏi cái list mặt hàng
            categoryList = categoryList.Skip(1).ToArray(); // bỏ đi thằng head lúc nãy, danh sách giờ chỉ còn những thằng tiếp theo của nó thôi
            if (headOfCategoryContent.Length == 0) // nếu thằng đầu tiên rỗng thì chưa có danh sách
            {
                Console.WriteLine("File empty"); // làm luôn trường hợp khởi tạo rỗng nha
            }
            else
            {
                headOfCategory = new Category { IdCategory = Int32.Parse(headOfCategoryContent[0]), Name = headOfCategoryContent[1].ToString() }; // khởi tạo head
                if (categoryList.Length != 0) // nếu những thằng phía sau nó có tồn tại
                {
                    foreach (var item in categoryList) // duyệt danh sách, mỗi dòng trong file txt sẽ tương đương với một item
                    {
                        string[] tempCategoryContent = item.Split(','); // tách chuỗi, để lấy thông tin
                        Category temp = new Category(Int32.Parse(tempCategoryContent[0]), tempCategoryContent[1].ToString()); // Khởi tạo thằng category mà head liên kết tới
                        insertCategory(ref headOfCategory, temp); // thêm nó vào danh sách đã có
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
            //init CategoryList
            Category headOfCategoryList = new Category() ;
            Category.initCategoryList(ref headOfCategoryList);
            
            Console.ReadLine();
        }
    }
}
