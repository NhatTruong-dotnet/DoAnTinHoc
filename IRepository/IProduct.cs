using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public interface IProduct
    {
        #region UI
        void insertProduct(ref Product headOfProduct, Product newProduct);
        void loadProductList(ref Product headOfProduct);
        void deleteProduct(ref Product headOfProduct, Product deleteProduct);
        void updateProduct(ref Product headOfProduct, Product updatedProduct, string needToUpdateProduct);
        #endregion

        #region File
        void updateFile(ref Product headOfProduct);
        #endregion

        bool validateProduct(string IdProductName);
        Product getNewProduct();
    }
}
