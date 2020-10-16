using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public class CategoryService
    {
        public void LoadCategoryList(ref Category headOfCategory)
        {
            //dự phòng biến trả về 
            //init Repo của Category
            CategoryRepo categoryRepo = new CategoryRepo(); 
            categoryRepo.loadCategoryList(ref headOfCategory);
        }

        public void AddNewCategory(ref Category headOfCategory)
        {
            CategoryRepo categoryRepo = new CategoryRepo();
            Category newCategory = categoryRepo.getNewCategory();
            if (categoryRepo.validateCategory(newCategory.Name))
            {
                Console.WriteLine("Item already inserted");
            }
            else
            {
                categoryRepo.insertCategory(ref headOfCategory, newCategory);
                categoryRepo.updateFile(ref headOfCategory);
                Console.WriteLine("Insert success");
            }
        }

        public void DeleteCategory(ref Category headOfCategory)
        {
            CategoryRepo categoryRepo = new CategoryRepo();
            Category deleteCategory = categoryRepo.getNewCategory();
            if (!categoryRepo.validateCategory(deleteCategory.Name))
            {
                Console.WriteLine("This category not in list");
            }
            else
            {
                categoryRepo.deleteCategory(ref headOfCategory,deleteCategory);
                categoryRepo.updateFile(ref headOfCategory);
            }
           
        }
    }
}
