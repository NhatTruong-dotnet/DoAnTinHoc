﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DoAnTinHoc
{
    public class ProductRepo : IProduct
    {

        const string filePath = @"C:\NhatTruong\Project\DoAnTinHoc\Data\ProductData.txt";
        #region Function
        // Thêm một mặt hàng mới vào danh sách

        #region UI
        public void insertProduct(ref Product headOfProduct, Product newProduct)
        {
            // do thằng headOfProduct dùng nó làm con trỏ cho nguyên danh sách nên cần phải clone thằng khác để không thay đổi danh sách
            Product cloneOfHeadProduct = headOfProduct;
            while (cloneOfHeadProduct.NextProduct != headOfProduct)
            {
                // thì dịch con trỏ clone này sang thằng kế tiếp
                cloneOfHeadProduct = cloneOfHeadProduct.NextProduct;
            }
            cloneOfHeadProduct.NextProduct = newProduct;
            cloneOfHeadProduct.NextProduct.NextProduct = headOfProduct;
        }

        public Product loadProductList(string filepathFromHead)
        {
            Product headOfProduct = new Product();
            // lấy dữ liệu các mặt hàng có sẵn trong danh sách
            string[] ProductList = File.ReadAllLines(filepathFromHead);
            if (ProductList.Length == 0)
            {
                Console.WriteLine("-->File Product empty");
            }
            else
            {
                // sure là thằng đầu tiên sẽ là con trỏ rồi nên lấy nó ra khỏi cái list mặt hàng
                string[] headOfProductContent = ProductList[0].Split(',');
                // bỏ đi thằng head lúc nãy, danh sách giờ chỉ còn những thằng tiếp theo của nó thôi
                ProductList = ProductList.Skip(1).ToArray();
                // nếu thằng đầu tiên rỗng thì chưa có danh sách
                // khởi tạo head
                headOfProduct = new Product
                {
                    IdProduct = Int32.Parse(headOfProductContent[0]),
                    Name = headOfProductContent[1].ToString().Trim(),
                    Amount = Int32.Parse(headOfProductContent[2]),
                    Price = Convert.ToDouble(headOfProductContent[3])
                };
                // nếu những thằng phía sau nó có tồn tại
                if (ProductList.Length != 0)
                {
                    // duyệt danh sách, mỗi dòng trong file txt sẽ tương đương với một item
                    foreach (var item in ProductList)
                    {
                        // tách chuỗi, để lấy thông tin
                        string[] tempProductContent = item.Split(',');
                        // Lấy thông tin

                        // Khởi tạo thằng product mà head liên kết tới
                        Product temp = new Product
                        {
                            IdProduct = Int32.Parse(tempProductContent[0]),
                            Name = tempProductContent[1].ToString().Trim(),
                            Amount = Int32.Parse(tempProductContent[2]),
                            Price = Convert.ToDouble(tempProductContent[3])
                        };
                        // thêm nó vào danh sách đã có
                        insertProduct(ref headOfProduct, temp);
                    }
                }
            }
            return headOfProduct;
        }

        public void deleteProduct(ref Product headOfProduct, string deleteProductName)
        {
            if (headOfProduct == null)
                Console.WriteLine("-->This List is Empty");
            else
            {
                // sure là thằng đầu tiên sẽ là con trỏ rồi nên lấy nó ra khỏi cái list mặt hàng
                Product cloneOfProduct = headOfProduct;
                Product pointerInLoop = new Product();
                // Kiểm tra xem nút có phải là nút duy nhất không
                if (cloneOfProduct.NextProduct == headOfProduct)
                    headOfProduct = null;
                else
                {
                    if (cloneOfProduct.Name == deleteProductName)
                    {
                        int theFirstId = headOfProduct.IdProduct;
                        headOfProduct = cloneOfProduct.NextProduct;
                        cloneOfProduct = headOfProduct;
                        while (cloneOfProduct.NextProduct.IdProduct != theFirstId)
                        {
                            cloneOfProduct = cloneOfProduct.NextProduct;
                        }
                        cloneOfProduct.NextProduct = headOfProduct;
                    }
                    else
                    {
                        pointerInLoop = cloneOfProduct;
                        cloneOfProduct = cloneOfProduct.NextProduct;
                        while (cloneOfProduct.NextProduct.Name != headOfProduct.NextProduct.Name)
                        {
                            if (cloneOfProduct.Name == deleteProductName)
                            {
                                pointerInLoop.NextProduct = cloneOfProduct.NextProduct;
                                break;
                            }
                            cloneOfProduct = cloneOfProduct.NextProduct;
                            pointerInLoop = pointerInLoop.NextProduct;
                        }
                    }
                }

            }
        }
        #endregion

        public bool validateProductName(string productName, string filePathProduct)
        {
            bool result = false;
            string[] ProductList = File.ReadAllLines(filePathProduct);
            foreach (var item in ProductList)
            {
                string[] itemContent = item.Split(',');
                if (itemContent[1].Equals(productName))
                {
                    return result = true;
                }
            }
            return result;
        }

        public bool validateProduct(int Amount, double Price)
        {
            bool result = false;
            if (Amount > 0 && Price > 0)
            {
                return result = true;
            }
            return result;
        }

        public Product getNewProduct(string filePathProduct)
        {
            // đọc file dữ liệu
            string nameProduct = "";
            int idProduct = 1;
            int amount = 1;
            double price = 1.0;
            Product returnProduct = new Product();
            try
            {
                File.ReadAllLines(filePathProduct).Last().Split(',');
            }
            catch (Exception)
            {
                Console.Write("-->Enter your Product: ");
                nameProduct = Console.ReadLine().ToLower().Trim();
                Console.Write("-->Enter your amount: ");
                amount = Int32.Parse(Console.ReadLine());
                Console.Write("-->Enter your price: ");
                price = Convert.ToDouble(Console.ReadLine());
                return returnProduct = new Product
                {
                    IdProduct = idProduct,
                    Name = nameProduct,
                    Amount = amount,
                    Price = price
                };
            }
            Console.Write("-->Enter your Product: ");
            nameProduct = Console.ReadLine().ToLower().Trim();
            Console.Write("-->Enter your amount: ");
            amount = Int32.Parse(Console.ReadLine());
            Console.Write("-->Enter your price: ");
            price = Convert.ToDouble(Console.ReadLine());

            string[] lastProduct = File.ReadAllLines(filePathProduct).Last().Split(',');
            idProduct = Int32.Parse(lastProduct[0]) + 1;
            return returnProduct = new Product
            {
                IdProduct = idProduct,
                Name = nameProduct,
                Amount = amount,
                Price = price
            };
        }

        public void updateFile(ref Product headOfProduct, string filePathProduct)
        {
            Product cloneOfHeadProduct = headOfProduct;
            File.WriteAllText(filePathProduct, String.Empty);
            string Product =
                    cloneOfHeadProduct.IdProduct.ToString() + ','
                    + cloneOfHeadProduct.Name + ','
                    + cloneOfHeadProduct.Amount.ToString() + ','
                    + cloneOfHeadProduct.Price.ToString() + ','
                    + cloneOfHeadProduct.NextProduct.IdProduct.ToString();
            using (StreamWriter datafile = File.AppendText(filePathProduct))
            {
                datafile.WriteLine(Product);
            }
            cloneOfHeadProduct = cloneOfHeadProduct.NextProduct;
            while (cloneOfHeadProduct.IdProduct != headOfProduct.IdProduct)
            {
                Product =
                    cloneOfHeadProduct.IdProduct.ToString() + ','
                    + cloneOfHeadProduct.Name + ','
                    + cloneOfHeadProduct.Amount.ToString() + ','
                    + cloneOfHeadProduct.Price.ToString() + ','
                    + cloneOfHeadProduct.NextProduct.IdProduct.ToString();
                using (StreamWriter datafile = File.AppendText(filePathProduct))
                {
                    datafile.WriteLine(Product);
                }
                cloneOfHeadProduct = cloneOfHeadProduct.NextProduct;
            }
        }

        public void updateProduct(ref Product headOfProduct, Product updatedProduct, string needToUpdateProduct)
        {
            Product cloneOfHeadProduct = headOfProduct;

            if (cloneOfHeadProduct.Name.Equals(needToUpdateProduct))
            {
                cloneOfHeadProduct.Name = updatedProduct.Name;
                cloneOfHeadProduct.Amount = updatedProduct.Amount;
                cloneOfHeadProduct.Price = updatedProduct.Price;
            }
            else
            {
                int theFirstId = headOfProduct.IdProduct;
                cloneOfHeadProduct = cloneOfHeadProduct.NextProduct;
                while (cloneOfHeadProduct.IdProduct != theFirstId)
                {
                    if (cloneOfHeadProduct.Name.Equals(needToUpdateProduct))
                    {
                        cloneOfHeadProduct.Name = updatedProduct.Name;
                        cloneOfHeadProduct.Amount = updatedProduct.Amount;
                        cloneOfHeadProduct.Price = updatedProduct.Price;
                    }
                    else if (validateProduct(cloneOfHeadProduct.Amount, cloneOfHeadProduct.Price))
                    {
                        cloneOfHeadProduct = cloneOfHeadProduct.NextProduct;
                    }
                }
            }
        }

        public LinkedList<Product> GetProductByName(ref Category headOfCategory, string productName)
        {
            LinkedList<Product> returnProductList = new LinkedList<Product>();

            Category cloneOfHeadCategory = headOfCategory;

            Product productList = new Product();
            int theFlatOfProduct = productList.IdProduct;
            do
            {
                productList = loadProductList(cloneOfHeadCategory.filePathProduct);
                do
                {
                    if (productList.NextProduct != productList)
                    {
                        if (productList.Name.Contains(productName))
                        {
                            returnProductList.AddLast(productList);
                        }
                        productList = productList.NextProduct;
                    }

                } while (productList.IdProduct != theFlatOfProduct);

                cloneOfHeadCategory = cloneOfHeadCategory.NextCategory;
            } while (cloneOfHeadCategory.NextCategory != headOfCategory);

            return returnProductList;
        }

        public LinkedList<Product> GetProductByID(ref Category headOfCategory, int IDProduct)
        {
            LinkedList<Product> returnProductList = new LinkedList<Product>();

            Category cloneOfHeadCategory = headOfCategory;

            Product productList = new Product();
            int theFlatOfProduct = productList.IdProduct;
            do
            {
                productList = loadProductList(cloneOfHeadCategory.filePathProduct);
                do
                {
                    if (productList.IdProduct.Equals(IDProduct))
                    {
                        returnProductList.AddLast(productList);
                    }
                    productList = productList.NextProduct;
                } while (productList.IdProduct != theFlatOfProduct);

                cloneOfHeadCategory = cloneOfHeadCategory.NextCategory;
            } while (cloneOfHeadCategory.NextCategory != headOfCategory);

            return returnProductList;
        }

        public void ShowProduct(Product product)
        {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("ID: {0}", product.IdProduct);
                Console.WriteLine("Name: {0}", product.Name);
                Console.WriteLine("Amout: {0}", product.Amount);
                Console.WriteLine("Price: {0}", product.Price);
                Console.WriteLine("-----------------------------------------------");
        }
    }
    #endregion
}