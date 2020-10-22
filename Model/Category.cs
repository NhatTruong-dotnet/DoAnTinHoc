using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public class Category
    {
        #region Properties
        public int IdCategory { get; set; }

        public string Name { get; set; }
        // ma hang tiep theo
        public Category NextCategory { get; set; }
        // con tro san pham dau
        public Product HeadOfProduct { get; set; }
        public string filePathProduct { get; set; }
        #endregion

        #region Constructor
        public Category() // hàm khởi tạo constructor mặc định thì nó sẽ là một cục riêng biệt không tham chiếu đến cục khác cũng chưa có danh sách sản phẩm của nó
        {
            NextCategory = this;
            HeadOfProduct = new Product();
        }

        public Category(int idCategory) // tương tự ở trên nhưng với id cho trước 
        {
            this.IdCategory = idCategory;
            this.NextCategory = this;
            this.HeadOfProduct = null;
        }
        public Category(int idCategory, string name, string filePath) // tương tự ở trên nhưng với id và name được, này được tận dụng trong hàm insert
        {
            this.IdCategory = idCategory;
            this.Name = name;
            this.NextCategory = this;
            this.filePathProduct = filePath;
            this.HeadOfProduct = null;
        }
        #endregion
    }
}
