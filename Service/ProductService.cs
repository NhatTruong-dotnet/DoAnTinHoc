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
            if (ProductRepo.validateProduct(newProduct.Name))
            {
                Console.WriteLine("Item already inserted");
            }
            else
            {
                ProductRepo.insertProduct(ref headOfProduct, newProduct);
                ProductRepo.updateFile(ref headOfProduct);
                Console.WriteLine("Insert success");
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
                Product deleteProduct = ProductRepo.getNewProduct();
                if (ProductRepo.validateProduct(deleteProduct.Name))
                {
                    ProductRepo.deleteProduct(ref headOfProduct, deleteProduct);
                    ProductRepo.updateFile(ref headOfProduct);
                }
                else
                {
                    Console.WriteLine("This Product not in list");
                }
            }
        }
        public void UpdateCategory(ref Product headOfProduct)
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
                if (productRepo.validateProduct(needUpdateProduct))
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
