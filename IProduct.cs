using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public interface IProduct
    {
        void insertProduct(ref Product headOfProduct, Product newProduct);
        void initProductList(ref Product headOfProduct);
    }
}
