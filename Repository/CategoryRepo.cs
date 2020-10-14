using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DoAnTinHoc
{
    public class CategoryRepo:ICategory
    {
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
            string[] categoryList = File.ReadLines(@"C:\NhatTruong\Project\DoAnTinHoc\Data\CategoryData.txt").First().Split(',');
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
            string[] categoryList = File.ReadAllLines(@"C:\NhatTruong\Project\DoAnTinHoc\Data\CategoryData.txt");
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
            // đọc file dữ liệu
            string[] lastCategory = File.ReadAllLines(@"C:\NhatTruong\Project\DoAnTinHoc\Data\CategoryData.txt").Last().Split(',');
            Console.Write("Enter your category: ");
            string nameCategory = Console.ReadLine();
            int idCategory = Int32.Parse(lastCategory[0]);
            Category returnCategory = new Category(idCategory, nameCategory);
            return returnCategory;
        }

        #endregion
    }
}
