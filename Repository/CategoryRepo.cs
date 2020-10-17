using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DoAnTinHoc
{
    public class CategoryRepo : ICategory
    {

        const string filePath = @"C:\NhatTruong\Project\DoAnTinHoc\Data\CategoryData.txt";
        #region Function
        // Thêm một mặt hàng mới vào danh sách

        #region UI
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

        public void loadCategoryList(ref Category headOfCategory)
        {
            // lấy dữ liệu các mặt hàng có sẵn trong danh sách
            string[] categoryList = File.ReadAllLines(filePath);
            if (categoryList.Length == 0)
            {
                Console.WriteLine("File empty");
            }
            else
            {
                // sure là thằng đầu tiên sẽ là con trỏ rồi nên lấy nó ra khỏi cái list mặt hàng
                string[] headOfCategoryContent = categoryList[0].Split(',');
                // bỏ đi thằng head lúc nãy, danh sách giờ chỉ còn những thằng tiếp theo của nó thôi
                categoryList = categoryList.Skip(1).ToArray();
                // nếu thằng đầu tiên rỗng thì chưa có danh sách
                // khởi tạo head
                headOfCategory = new Category
                {
                    IdCategory = Int32.Parse(headOfCategoryContent[0]),
                    Name = headOfCategoryContent[1].ToString().Trim()
                };
                // nếu những thằng phía sau nó có tồn tại
                if (categoryList.Length != 0)
                {
                    // duyệt danh sách, mỗi dòng trong file txt sẽ tương đương với một item
                    foreach (var item in categoryList)
                    {
                        // tách chuỗi, để lấy thông tin
                        string[] tempCategoryContent = item.Split(',');
                        // Lấy thông tin

                        // Khởi tạo thằng product mà head liên kết tới
                        Category temp = new Category
                        {
                            IdCategory = Int32.Parse(tempCategoryContent[0]),
                            Name = tempCategoryContent[1].ToString().Trim()
                        };
                        // thêm nó vào danh sách đã có
                        insertCategory(ref headOfCategory, temp);
                    }
                }
            }
        }

        public void deleteCategory(ref Category headOfCategory, string deleteCategoryName)
        {
            if (headOfCategory == null)
                Console.WriteLine("This List is Empty");
            else
            {
                // sure là thằng đầu tiên sẽ là con trỏ rồi nên lấy nó ra khỏi cái list mặt hàng
                Category cloneOfProduct = headOfCategory;
                Category pointerInLoop = new Category();
                // Kiểm tra xem nút có phải là nút duy nhất không
                if (cloneOfProduct.NextCategory == headOfCategory)
                    headOfCategory = null;
                else
                {
                    if (cloneOfProduct.Name == deleteCategoryName)
                    {
                        int theFirstId = headOfCategory.IdCategory;
                        headOfCategory = cloneOfProduct.NextCategory;
                        cloneOfProduct = headOfCategory;
                        while (cloneOfProduct.NextCategory.IdCategory != theFirstId)
                        {
                            cloneOfProduct = cloneOfProduct.NextCategory;
                        }
                        cloneOfProduct.NextCategory = headOfCategory;
                    }
                    else
                    {
                        pointerInLoop = cloneOfProduct;
                        cloneOfProduct = cloneOfProduct.NextCategory;
                        while (cloneOfProduct.NextCategory.Name != headOfCategory.NextCategory.Name)
                        {
                            if (cloneOfProduct.Name == deleteCategoryName)
                            {
                                pointerInLoop.NextCategory = cloneOfProduct.NextCategory;
                                Console.WriteLine("Deleted");
                                break;
                            }
                            cloneOfProduct = cloneOfProduct.NextCategory;
                            pointerInLoop = pointerInLoop.NextCategory;
                        }
                    }
                }

            }

        }

        public void updateCategory(ref Category headOfCategory, Category updateCategory, string needUpdatedCategory)
        {
            Category cloneOfHeadCategory = headOfCategory;

            if (cloneOfHeadCategory.Name.Equals(needUpdatedCategory))
            {
                cloneOfHeadCategory.Name = updateCategory.Name;
            }
            else
            {
                int theFirstId = headOfCategory.IdCategory;
                cloneOfHeadCategory = cloneOfHeadCategory.NextCategory;
                while (cloneOfHeadCategory.IdCategory != theFirstId)
                {
                    if (cloneOfHeadCategory.Name.Equals(needUpdatedCategory))
                    {
                        cloneOfHeadCategory.Name = updateCategory.Name;
                    }
                    else
                    {
                        cloneOfHeadCategory = cloneOfHeadCategory.NextCategory;
                    }
                }
            }

        }
        #endregion
        public bool validateCategory(string newCategoryName)
        {
            bool result = false;
            string[] categoryList = File.ReadAllLines(filePath);
            foreach (var item in categoryList)
            {
                if (item.Contains(newCategoryName))
                {
                    return result = true;
                }
            }
            return result;
        }

        public Category getNewCategory()
        {
            // đọc file dữ liệu
            string nameCategory ="";
            int idCategory = 1;
            Category returnCategory = new Category();
            try
            {
                File.ReadAllLines(filePath).Last().Split(',');
            }
            catch (Exception)
            {
                Console.Write("Enter your category: ");
                nameCategory = Console.ReadLine().ToLower().Trim();
                return returnCategory = new Category { IdCategory = idCategory, Name = nameCategory };
            }
            Console.Write("Enter your category: ");
            nameCategory = Console.ReadLine().ToLower().Trim();
                
            string[] lastCategory = File.ReadAllLines(filePath).Last().Split(',');
            idCategory = Int32.Parse(lastCategory[0]) + 1;
            return returnCategory = new Category { IdCategory = idCategory, Name = nameCategory };
        }

        public void updateFile(ref Category headOfCategory)
        {
            Category cloneOfHeadCategory = headOfCategory;
            File.WriteAllText(filePath, String.Empty);
            string category =
                    cloneOfHeadCategory.IdCategory.ToString() + ',' 
                    + cloneOfHeadCategory.Name + ',' 
                    + cloneOfHeadCategory.NextCategory.IdCategory.ToString();

            using (StreamWriter datafile = File.AppendText(filePath))
            {
                datafile.WriteLine(category);
            }
            cloneOfHeadCategory = cloneOfHeadCategory.NextCategory;
            while (cloneOfHeadCategory.IdCategory != headOfCategory.IdCategory)
            {
                category =
                    cloneOfHeadCategory.IdCategory.ToString() + ',' 
                    + cloneOfHeadCategory.Name + ',' 
                    + cloneOfHeadCategory.NextCategory.IdCategory.ToString();

                using (StreamWriter datafile = File.AppendText(filePath))
                {
                    datafile.WriteLine(category);
                }
                cloneOfHeadCategory = cloneOfHeadCategory.NextCategory;
            }
        }
    }
    #endregion
}
