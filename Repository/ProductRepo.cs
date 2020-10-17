using System;
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

        public void loadProductList(ref Product headOfProduct)
        {
            // lấy dữ liệu các mặt hàng có sẵn trong danh sách
            string[] ProductList = File.ReadAllLines(filePath);
            if (ProductList.Length == 0)
            {
                Console.WriteLine("File empty");
            }
            else
            {
                // sure là thằng đầu tiên sẽ là con trỏ rồi nên lấy nó ra khỏi cái list mặt hàng
                string[] headOfProductContent = ProductList[0].Split(',');
                // bỏ đi thằng head lúc nãy, danh sách giờ chỉ còn những thằng tiếp theo của nó thôi
                ProductList = ProductList.Skip(1).ToArray();
                // nếu thằng đầu tiên rỗng thì chưa có danh sách
                // khởi tạo head
                headOfProduct = new Product {
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
                        Product temp = new Product { 
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
        }

        public void deleteProduct(ref Product headOfProduct, Product deleteProduct)
        {
            if (headOfProduct == null)
                Console.WriteLine("This List is Empty");
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
                    if (cloneOfProduct.Name == deleteProduct.Name)
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
                            if (cloneOfProduct.Name == deleteProduct.Name)
                            {
                                pointerInLoop.NextProduct = cloneOfProduct.NextProduct;
                                Console.WriteLine("Deleted");
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
        public bool validateProduct(string productName)
        {
            bool result = false;
            string[] ProductList = File.ReadAllLines(filePath);
            foreach (var item in ProductList)
            {
                if (item.Contains(productName))
                {
                    return result = true;
                }
            }
            return result;
        }

        public Product getNewProduct()
        {
            // đọc file dữ liệu
            string[] lastProduct = File.ReadAllLines(filePath).Last().Split(',');
            Console.Write("Enter your Product: ");
            string nameProduct = Console.ReadLine().ToLower().Trim();
            int idProduct = Int32.Parse(lastProduct[0]);
            Console.Write("Amount:");
            int amoutProduct = Int32.Parse(Console.ReadLine());
            Console.Write("Price:");
            float priceProduct = float.Parse(Console.ReadLine());
            Product returnProduct = new Product(idProduct + 1, nameProduct, amoutProduct, priceProduct);
            return returnProduct;
        }

        public void updateFile(ref Product headOfProduct)
        {
            Product cloneOfHeadProduct = headOfProduct;
            File.WriteAllText(filePath, String.Empty);
            string Product =
                    cloneOfHeadProduct.IdProduct.ToString() + ','
                    + cloneOfHeadProduct.Name + ','
                    + cloneOfHeadProduct.Amount.ToString() + ','
                    + cloneOfHeadProduct.Price.ToString() + ','
                    + cloneOfHeadProduct.NextProduct.IdProduct.ToString();
            using (StreamWriter datafile = File.AppendText(filePath))
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
                using (StreamWriter datafile = File.AppendText(filePath))
                {
                    datafile.WriteLine(Product);
                }
                cloneOfHeadProduct = cloneOfHeadProduct.NextProduct;
            }
        }

        public void updateProduct(ref Product headOfProduct, Product updatedProduct, Product needToUpdateProduct)
        {
            Product cloneOfHeadProduct = headOfProduct;
            while (cloneOfHeadProduct.NextProduct != headOfProduct)
            {
                if (cloneOfHeadProduct.Name != needToUpdateProduct.Name)
                {
                    cloneOfHeadProduct = cloneOfHeadProduct.NextProduct;
                }
                else
                {
                    cloneOfHeadProduct.Name = updatedProduct.Name;
                    cloneOfHeadProduct.Amount = updatedProduct.Amount;
                    cloneOfHeadProduct.Price = updatedProduct.Price;
                    break;
                }
            }
        }
    }
    #endregion
}
