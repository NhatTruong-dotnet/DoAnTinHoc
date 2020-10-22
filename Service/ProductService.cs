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
        public Product LoadProductList(string filepath)
        {
            //dự phòng biến trả về 
            //init Repo của Product
            ProductRepo ProductRepo = new ProductRepo();
            return ProductRepo.loadProductList(filepath);
        }

        public void AddNewProduct(ref Product headOfProduct)
        {
            ProductRepo ProductRepo = new ProductRepo();
            Product newProduct = ProductRepo.getNewProduct();
            if (ProductRepo.validateProductName(newProduct.Name))
            {
                Console.WriteLine("Item already inserted");
            }
            else if (ProductRepo.validateProduct(newProduct.Amount, newProduct.Price))
            {
                ProductRepo.insertProduct(ref headOfProduct, newProduct);
                ProductRepo.updateFile(ref headOfProduct);
                Console.WriteLine("Insert success");
            }
            else
            {
                Console.WriteLine("Vui long nhap so duong");
            }
        }

        public void DeleteProduct(ref Product headOfProduct)
        {
            ProductRepo ProductRepo = new ProductRepo();
            if (headOfProduct == null)
            {
                Console.WriteLine("This list is empty now");
            }
            else
            {
                Console.Write("Enter name of Product: ");
                string deleteProductName = Console.ReadLine().Trim();
                if (ProductRepo.validateProductName(deleteProductName))
                {
                    ProductRepo.deleteProduct(ref headOfProduct, deleteProductName);
                    ProductRepo.updateFile(ref headOfProduct);
                }
                else
                {
                    Console.WriteLine("This Product not in list");
                }
            }
        }

        public void UpdateProduct(ref Product headOfProduct)
        {
            if (headOfProduct == null)
            {
                Console.WriteLine("This list is empty now");
            }
            else
            {
                ProductRepo productRepo = new ProductRepo();
                Console.Write("The Product want to update: ");
                string needUpdateProduct = Console.ReadLine().Trim().ToLower();
                Console.WriteLine("Update Product content: ");
                Product updatedProduct = productRepo.getNewProduct();
                if (productRepo.validateProductName(needUpdateProduct))
                {
                    productRepo.updateProduct(ref headOfProduct, updatedProduct, needUpdateProduct);
                    productRepo.updateFile(ref headOfProduct);
                }
                else
                {
                    Console.WriteLine("This Product not in list");
                }
            }
        }

        public void GetProduct(ref Category headOfCategory,string inputFromUser)
        {
            if (headOfCategory != null)
            {
                ProductRepo productRepo = new ProductRepo();
                int productID = 0;
                if (Int32.TryParse(inputFromUser, out productID))
                {
                     productRepo.GetProductByID(productID);
                }
                else
                {
                    LinkedList<Product> productList = productRepo.GetProductByName(ref headOfCategory, inputFromUser);
                    if (productList.Count == 0)
                    {
                        Console.WriteLine("This Product not in the list");
                    }
                    else
                    {
                        foreach (var item in productList)
                        {
                            productRepo.ShowProduct(item);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("The List is empty");
            }
        }

    }
}
