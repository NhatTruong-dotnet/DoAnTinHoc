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
            if (headOfCategory == null)
            {
                Console.WriteLine("This list is empty now");
            }
            else
            {
                Console.Write("Enter category name: ");
                string deleteCategoryName = Console.ReadLine().Trim();

                if (categoryRepo.validateCategory(deleteCategoryName))
                {
                    categoryRepo.deleteCategory(ref headOfCategory, deleteCategoryName);
                    categoryRepo.updateFile(ref headOfCategory);
                }
                else
                {
                    Console.WriteLine("This category not in list");
                }
            }
           
           
        }

        public void UpdateCategory(ref Category headOfCategory)
        {
            if (headOfCategory == null)
            {
                Console.WriteLine("This list is empty now");
            }
            else
            {
                CategoryRepo categoryRepo = new CategoryRepo();
                Console.Write("The category want to update: ");
                string needUpdateCategory = Console.ReadLine().Trim();
                Console.WriteLine("Update content");
                Category updatedCategory = categoryRepo.getNewCategory();
                if (categoryRepo.validateCategory(needUpdateCategory))
                {
                    categoryRepo.updateCategory(ref headOfCategory, updatedCategory, needUpdateCategory);
                    categoryRepo.updateFile(ref headOfCategory);
                }
                else
                {
                    Console.WriteLine("This category not in list");
                }
            }
        }

        public void ShowCategoryList(Category headOfCategory)
        {
            if (headOfCategory == null)
            {
                Console.WriteLine("This list is empty now");
            }
            else
            {
                int theFirstID = headOfCategory.IdCategory;
                Console.Write("The List of Category: ");
                Console.Write(headOfCategory.Name);
                headOfCategory = headOfCategory.NextCategory;
                while (headOfCategory.IdCategory != theFirstID)
                {
                    Console.Write(", " + headOfCategory.Name);
                    headOfCategory = headOfCategory.NextCategory;
                }
            }
            
        }

    }
}
