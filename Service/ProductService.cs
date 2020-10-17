﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public class ProductService
    {
        public void LoadProductList(ref Product headOfProduct)
        {
            //dự phòng biến trả về 
            //init Repo của Product
            ProductRepo ProductRepo = new ProductRepo();
            ProductRepo.loadProductList(ref headOfProduct);
        }

        public void AddNewProduct(ref Product headOfProduct)
        {
            ProductRepo ProductRepo = new ProductRepo();
            Product newProduct = ProductRepo.getNewProduct();
            if (ProductRepo.validateProductName(newProduct.Name))
            {
                Console.WriteLine("Item already inserted");
            }
            else if (ProductRepo.validateProductAmountPrice(newProduct.Amount, newProduct.Price))
            {
                ProductRepo.insertProduct(ref headOfProduct, newProduct);
                ProductRepo.updateFile(ref headOfProduct);
                Console.WriteLine("Insert success");
            }
            else
            {
                Console.WriteLine("Vui long nhap so duong");
                AddNewProduct(ref headOfProduct);
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
<<<<<<< HEAD

=======
>>>>>>> 4cdcb1ac3dbcc431ab4a6221d0ad57a786237520
        public void UpdateProduct(ref Product headOfProduct)
        {
            if (headOfProduct == null)
            {
                Console.WriteLine("This list is empty now");
            }
            else
            {
                ProductRepo productRepo = new ProductRepo();
                Console.WriteLine("The Product want to update");
                string needUpdateProduct = Console.ReadLine().Trim().ToLower();
                Console.WriteLine("Update content");
                Product updatedProduct = productRepo.getNewProduct();
                if (productRepo.validateProductName(needUpdateProduct))
                {
                    productRepo.updateProduct(ref headOfProduct, updatedProduct, needUpdateProduct);
                    productRepo.updateFile(ref headOfProduct);
                }
                else
                {
                    Console.WriteLine("This category not in list");
                }
            }
        }
    }
}
