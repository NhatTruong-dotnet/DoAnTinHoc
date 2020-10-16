using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public class CategoryService
    {
        public void addNewCategory(ref Category headOfCategory)
        {
            //dự phòng biến trả về 
            Category newCategory = new Category();
            //init Repo của Category
            CategoryRepo categoryRepo = new CategoryRepo(); 
            //Gọi hàm lấy một category mới
            newCategory = categoryRepo.getNewCategory();
            //check xem category đã tồn tại rồi hay chưa
            if (!categoryRepo.validateCategory(newCategory.Name))
            {
                categoryRepo.insertCategory(ref headOfCategory, newCategory);
                Console.WriteLine("Insert success");
            }
            else
            {
                Console.WriteLine("This category had been inserted");
            }
        }

    }
}
