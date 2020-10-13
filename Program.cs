using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    // hang hoa
    internal class Product 
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
        #endregion
    }

    // loai hang hoa
    internal class Category 
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
        // Thêm một mặt hàng mới vào danh sách
        internal static void insertCategory(ref Category headOfCategory ,Category newCategory) 
        {
            // do thằng headOfCategory dùng nó làm con trỏ cho nguyên danh sách nên cần phải clone thằng khác để không thay đổi danh sách
            Category cloneOfHeadCategory = headOfCategory;
            // khi mà thằng kế tiếp nó vẫn còn
            while (cloneOfHeadCategory.NextCategory != null) 
            {
                // thì dịch con trỏ clone này sang thằng kế tiếp
                cloneOfHeadCategory = cloneOfHeadCategory.NextCategory; 
            }
            // khi mà thằng kế bên nó đã trống thì thêm mặt hàng mới vào (nextCategory là kiểu Category)
            cloneOfHeadCategory.NextCategory = newCategory; 
        }

        internal static void initCategoryList(ref Category headOfCategory)
        {
            // lấy dữ liệu các mặt hàng có sẵn trong danh sách
            string[] categoryList = File.ReadAllLines(@"C:\NhatTruong\Project\DoAnTinHoc\Data.txt");
            // sure là thằng đầu tiên sẽ là con trỏ rồi nên lấy nó ra khỏi cái list mặt hàng
            string[] headOfCategoryContent = categoryList.First().Split(',');
            // bỏ đi thằng head lúc nãy, danh sách giờ chỉ còn những thằng tiếp theo của nó thôi
            categoryList = categoryList.Skip(1).ToArray();
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
