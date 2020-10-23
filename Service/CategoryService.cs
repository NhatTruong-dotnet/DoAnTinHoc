using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public class CategoryService
    {
        private CategoryRepo _categoryRepo;
        private ProductService _productService;

        public CategoryService()
        {
            _categoryRepo = new CategoryRepo();
            _productService = new ProductService();
        }
        public void LoadCategoryList(ref Category headOfCategory)
        {
            //dự phòng biến trả về 
            //init Repo của Category
            _categoryRepo.loadCategoryList(ref headOfCategory);
        }

        public void AddNewCategory(ref Category headOfCategory)
        {


            Category newCategory = _categoryRepo.getNewCategory();
            if (_categoryRepo.validateCategory(newCategory.Name))
            {
                Console.WriteLine("Item already inserted");
            }
            else
            {
                _categoryRepo.generateFilePathOfCategoryProduct(newCategory.Name, ref newCategory);
                _categoryRepo.insertCategory(ref headOfCategory, newCategory);
                File.Create(newCategory.filePathProduct);
                _categoryRepo.updateFile(ref headOfCategory);
                Console.WriteLine("Insert success");
            }
        }

        public void DeleteCategory(ref Category headOfCategory)
        {

            if (headOfCategory == null)
            {
                Console.WriteLine("This list is empty now");
            }
            else
            {
                Console.Write("Enter category name: ");
                string deleteCategoryName = Console.ReadLine().Trim();

                if (_categoryRepo.validateCategory(deleteCategoryName))
                {
                    _categoryRepo.deleteCategory(ref headOfCategory, deleteCategoryName);
                    File.Delete(GetCategory(deleteCategoryName).filePathProduct);
                    _categoryRepo.updateFile(ref headOfCategory);
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

                Console.Write("The category want to update: ");
                string needUpdateCategory = Console.ReadLine().Trim();
                Console.WriteLine("Update content");
                Category updatedCategory = _categoryRepo.getNewCategory();
                if (_categoryRepo.validateCategory(needUpdateCategory))
                {
                    string fileNeedToRename = _categoryRepo.generateFilePathOfCategoryProduct(needUpdateCategory, ref updatedCategory);
                    _categoryRepo.generateFilePathOfCategoryProduct(updatedCategory.Name, ref updatedCategory);
                    _categoryRepo.updateCategory(ref headOfCategory, updatedCategory, needUpdateCategory);
                    File.Move(fileNeedToRename, updatedCategory.filePathProduct);
                    _categoryRepo.updateFile(ref headOfCategory);
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
            Console.WriteLine("***********************************************************************************");
            Console.Write(String.Join(" ", spaceFunction));
            Console.Write(" 1. Show Category List");
            Console.Write(String.Join(" ", spaceFunction));
            Console.WriteLine("    6. Show Product List ");
            Console.Write(String.Join(" ", spaceFunction));
            Console.Write(" 2. Add New Category");
            Console.Write(String.Join(" ", spaceFunction));
            Console.WriteLine("      7. Add New Product   ");
            Console.Write(String.Join(" ", spaceFunction));
            Console.Write(" 3. Update Category");
            Console.Write(String.Join(" ", spaceFunction));
            Console.WriteLine("       8. Update Product    ");
            Console.Write(String.Join(" ", spaceFunction));
            Console.Write(" 4. Delete Category");
            Console.Write(String.Join(" ", spaceFunction));
            Console.WriteLine("       9. Delete Product    ");
            Console.Write(String.Join(" ", spaceFunction));
            Console.Write(" 5. Get Category");
            Console.WriteLine("                  10. Get Product       ");
            Console.Write(String.Join(" ", spaceFunction));
            Console.WriteLine(String.Join(" ", spaceFunction));
            Console.WriteLine("***********************************************************************************");

            Console.Write("*Your Choose: ");
            #endregion

            if (Int32.TryParse(Console.ReadLine(), out inputFromUser))
            {
                switch (inputFromUser)
                {
                    #region case 1
                    case 1:
                        ShowCategoryList(headOfCategory);
                        break;
                    #endregion

                    #region case 2
                    case 2:
                        AddNewCategory(ref headOfCategory);
                        break;
                    #endregion

                    #region case 3
                    case 3:
                        UpdateCategory(ref headOfCategory);
                        break;
                    #endregion

                    #region case 4
                    case 4:
                        DeleteCategory(ref headOfCategory);
                        break;
                    #endregion

                    #region case 5
                    case 5:
                        Console.Write("Enter Name or ID of category: ");
                        string input = Console.ReadLine();
                        Category temp = GetCategory(input.Trim().ToLower());
                        break;
                    #endregion

                    #region case 6
                    case 6:
                        Console.Write("Enter your category u want to show product list: ");
                        string categoryName = Console.ReadLine();
                        Category categoryToShow = GetCategory(categoryName);
                        Product productList = _productService.LoadProductList(categoryToShow.filePathProduct);
                        int theFirstID = productList.IdProduct;
                        do
                        {
                            _productService.ShowProduct(productList);
                            productList = productList.NextProduct;
                        } while (productList.IdProduct != theFirstID);
                        break;
                    #endregion

                    #region case 7
                    case 7:
                        Console.Write("Enter your category u want to add product list: ");
                        categoryName = Console.ReadLine().Trim();
                        Category tempCategory = GetCategory(categoryName);
                        Product headOfProduct = _productService.LoadProductList(tempCategory.filePathProduct);
                        _productService.AddNewProduct(ref headOfProduct, tempCategory.filePathProduct);
                        break;
                    #endregion

                    #region case 8
                    case 8:
                        _productService.UpdateProduct(ref headOfCategory);
                        break;
                    #endregion

                    #region case 9
                    case 9:
                        _productService.DeleteProduct(ref headOfCategory);
                        break;
                    #endregion

                    #region case 10
                    case 10:
                        Console.Write("Enter Name or ID of Product: ");
                        input = Console.ReadLine().Trim().ToLower();
                        _productService.GetProduct(ref headOfCategory, input);
                        break;
                        #endregion
                }
            }
            else
            {
                Console.WriteLine("Failed");
                Console.WriteLine(inputFromUser);
            }
        }

        public Category GetCategory(string inputFromUser)
        {
            CategoryRepo categoryRepo = new CategoryRepo();
            Category returned = new Category();
            int categoryID = 0;
            if (Int32.TryParse(inputFromUser, out categoryID))
            {
                returned = categoryRepo.GetCategoryByID(categoryID);
            }
            else
            {
                returned = categoryRepo.GetCategoryByName(inputFromUser);
            }
            return returned;
        }
    }
}
