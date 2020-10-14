using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public class Product
    {
        #region Properties
        public int IdProduct { get; set; }
        public string Name { get; set; }
        // so luong
        public int Amount { get; set; }
        public float Price { get; set; }
        // san pham tiep theo
        public Product NextProduct { get; set; }
        #endregion

        #region Constructor
        public Product()
        {
            NextProduct = null;
        }
        public Product(int idProduct, string name, int amount, float price)
        {
            this.IdProduct = idProduct;
            this.Name = name;
            this.Amount = amount;
            this.Price = price;
            this.NextProduct = this;
        }
        #endregion
    }
}
