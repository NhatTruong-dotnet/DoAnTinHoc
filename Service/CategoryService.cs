using System;
using System.Collections.Generic;
using System.IO;
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
                categoryRepo.generateFilePathOfCategoryProduct(newCategory.Name, ref newCategory);
                categoryRepo.insertCategory(ref headOfCategory, newCategory);
                File.Create(newCategory.filePathProduct);
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
                    string fileNeedToRename = categoryRepo.generateFilePathOfCategoryProduct(needUpdateCategory, ref updatedCategory);
                    categoryRepo.generateFilePathOfCategoryProduct(updatedCategory.Name, ref updatedCategory);
                    categoryRepo.updateCategory(ref headOfCategory, updatedCategory, needUpdateCategory);
                    File.Move(fileNeedToRename, updatedCategory.filePathProduct);
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

        public void MenuList(Category headOfCategory)
        {
            string[] space = new string[30];
            string[] spaceFunction = new string[10];
            int inputFromUser = 0;
            #region UI
            Console.Write(String.Join(" ", space));
            Console.WriteLine("Manage Category and Product");
            Console.WriteLine(String.Join(" ", spaceFunction));
            Console.WriteLine("1. Show Category List");
            Console.WriteLine(String.Join(" ", spaceFunction));
            Console.WriteLine("2. Add New Category");
            Console.WriteLine(String.Join(" ", spaceFunction));
            Console.WriteLine("3. Update Category");
            Console.WriteLine(String.Join(" ", spaceFunction));
            Console.WriteLine("4. Delete Category");
            Console.WriteLine(String.Join(" ", spaceFunction));
            Console.Write("*Your Choose: ");
            #endregion

            if (Int32.TryParse(Console.ReadLine(), out inputFromUser))
            {
                switch (inputFromUser)
                {
                    case 1:
                        ShowCategoryList(headOfCategory);
                        break;
                    case 2:
                        AddNewCategory(ref headOfCategory);
                        break;
                    case 3:
                        UpdateCategory(ref headOfCategory);
                        break;
                    case 4:
                        DeleteCategory(ref headOfCategory);
                        break;
                }
            }
            else
            {
                Console.WriteLine("Failed");
                Console.WriteLine(inputFromUser);
            }
            Console.Read();
        }
    }
}
