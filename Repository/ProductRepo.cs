using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public class ProductRepo
    {
        #region Function
        public void insertProduct(ref Product headOfProduct, Product newProduct)
        {
            // do thằng headOfCategory dùng nó làm con trỏ cho nguyên danh sách nên cần phải clone thằng khác để không thay đổi danh sách
            Product cloneOfHeadProduct = headOfProduct;
            // khi mà thằng kế tiếp nó vẫn còn
            while (cloneOfHeadProduct.NextProduct != null)
            {
                // thì dịch con trỏ clone này sang thằng kế tiếp
                cloneOfHeadProduct = cloneOfHeadProduct.NextProduct;
            }
            // khi mà thằng kế bên nó đã trống thì thêm mặt hàng mới vào (nextCategory là kiểu Category)
            cloneOfHeadProduct.NextProduct = newProduct;

        }

        public void initProductList(ref Product headOfProduct)
        {
            // lấy dữ liệu các mặt hàng có sẵn trong danh sách
            string[] productList = File.ReadAllLines(@"C:\NhatTruong\Project\DoAnTinHoc\Data.txt");
            // sure là thằng đầu tiên sẽ là con trỏ rồi nên lấy nó ra khỏi cái list mặt hàng
            string[] headOfProductContent = productList.First().Split(',');
            // bỏ đi thằng head lúc nãy, danh sách giờ chỉ còn những thằng tiếp theo của nó thôi
            productList = productList.Skip(1).ToArray();
            // nếu thằng đầu tiên rỗng thì chưa có danh sách
            if (headOfProductContent.Length == 0)
            {
                // làm luôn trường hợp khởi tạo rỗng nha
                Console.WriteLine("File empty");
            }
            else
            {
                // khởi tạo head
                headOfProduct = new Product
                {
                    IdProduct = Int32.Parse(headOfProductContent[0]),
                    Name = headOfProductContent[1].ToString(),
                    Amount = Int32.Parse(headOfProductContent[2]),
                    Price = float.Parse(headOfProductContent[3])
                };

                // nếu những thằng phía sau nó có tồn tại
                if (productList.Length != 0)
                {
                    // duyệt danh sách, mỗi dòng trong file txt sẽ tương đương với một item
                    foreach (var item in productList)
                    {
                        // tách chuỗi, để lấy thông tin
                        string[] tempCategoryContent = item.Split(',');
                        // Khởi tạo thằng category mà head liên kết tới
                        Product temp = new Product
                            (
                                Int32.Parse(tempCategoryContent[0]),
                                tempCategoryContent[1].ToString(),
                                Int32.Parse(tempCategoryContent[2]),
                                float.Parse(tempCategoryContent[3])
                            );

                        // thêm nó vào danh sách đã có
                        insertProduct(ref headOfProduct, temp);
                    }
                }
            }
        }
        #endregion 
    }
}
