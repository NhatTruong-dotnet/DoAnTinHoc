using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public class ProductService
    {
        private ProductRepo _productRepo;

        public ProductService()
        {
            _productRepo = new ProductRepo();
        }

        public Product LoadProductList(string filepath)
        {
            //dự phòng biến trả về 
            //init Repo của Product
            return _productRepo.loadProductList(filepath);
        }

        public void AddNewProduct(ref Product headOfProduct, string filePathProduct)
        {
            Product newProduct = _productRepo.getNewProduct(filePathProduct);
            if (_productRepo.validateProductName(newProduct.Name,filePathProduct ))
            {
                Console.WriteLine("Item already inserted");
            }
            else if (_productRepo.validateProduct(newProduct.Amount, newProduct.Price))
            {
                _productRepo.insertProduct(ref headOfProduct, newProduct);
                _productRepo.updateFile(ref headOfProduct, filePathProduct);
                Console.WriteLine("Insert success");
            }
            else
            {
                Console.WriteLine("Vui long nhap so duong");
            }
        }

        public void DeleteProduct(ref Category headOfCategory)
        {
            if (headOfCategory == null)
            {
                Console.WriteLine("This list is empty now");
            }
            else
            {
                Console.Write("Enter name of Product: ");
                string deleteProductName = Console.ReadLine().Trim().ToLower(); ;
                Category cloneHeadOfCategory = headOfCategory;
                int theFirstID = headOfCategory.IdCategory;
                bool isNotFind = true;
                do
                {

                    if (_productRepo.validateProductName(deleteProductName, cloneHeadOfCategory.filePathProduct))
                    {
                        Product headOfProduct = LoadProductList(cloneHeadOfCategory.filePathProduct);
                        _productRepo.deleteProduct(ref headOfProduct, deleteProductName);
                        _productRepo.updateFile(ref headOfProduct, cloneHeadOfCategory.filePathProduct);
                        isNotFind = false;
                    }
                    cloneHeadOfCategory = cloneHeadOfCategory.NextCategory;
                } while (cloneHeadOfCategory.IdCategory != theFirstID);
                if (isNotFind)
                {
                    Console.WriteLine("Product not in the list");
                }
                else
                {
                    Console.WriteLine("Deleted successful");
                }
            }
        }

        public void UpdateProduct(ref Category headOfCategory)
        {
            if (headOfCategory == null)
            {
                Console.WriteLine("This list is empty now");
            }
            else
            {
                Console.Write("The Product want to update: ");
                string needUpdateProduct = Console.ReadLine().Trim().ToLower();
                Category cloneHeadOfCategory = headOfCategory;
                int theFirstId = headOfCategory.IdCategory;
                bool isNotFind = true;
                do
                {
                    if (_productRepo.validateProductName(needUpdateProduct, cloneHeadOfCategory.filePathProduct))
                    {
                         Product headOfProduct = LoadProductList(cloneHeadOfCategory.filePathProduct);
                         Product updateProduct = _productRepo.getNewProduct(cloneHeadOfCategory.filePathProduct);
                        _productRepo.updateProduct(ref headOfProduct, updateProduct, needUpdateProduct);
                        _productRepo.updateFile(ref headOfProduct, cloneHeadOfCategory.filePathProduct);
                        isNotFind = false;
                    }
                    cloneHeadOfCategory = cloneHeadOfCategory.NextCategory;
                } while (cloneHeadOfCategory.IdCategory != theFirstId);
                if (isNotFind)
                {
                    Console.WriteLine("Product not in the list");
                }
                else
                {
                    Console.WriteLine("Updated successful");
                }
            }
        }

        public void GetProduct(ref Category headOfCategory, string inputFromUser)
        {
            if (headOfCategory != null)
            {
                int productID = 0;
                if (Int32.TryParse(inputFromUser, out productID))
                {
                    _productRepo.GetProductByID(ref headOfCategory, productID);
                }
                else
                {
                    LinkedList<Product> productList = _productRepo.GetProductByName(ref headOfCategory, inputFromUser);
                    if (productList.Count == 0)
                    {
                        Console.WriteLine("This Product not in the list");
                    }
                    else
                    {
                        foreach (var item in productList)
                        {
                            _productRepo.ShowProduct(item);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("The List is empty");
            }
        }

        public void ShowProduct(Product product)
        {
            _productRepo.ShowProduct(product);
        }
     }
}

