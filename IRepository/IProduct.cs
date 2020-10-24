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
        Product loadProductList(string filepathFromHead);
        void deleteProduct(ref Product headOfProduct, string deleteProductName);
        void updateProduct(ref Product headOfProduct, Product updatedProduct, string needToUpdateProduct);
        #endregion

        #region File
        void updateFile(ref Product headOfProduct, string filePathProduct);
        #endregion

        #region Validate
        bool validateProductName(string IdProductName, string filePathProduct);

        bool validateProduct(int Amount, double Price);
        #endregion

        #region Search
        LinkedList<Product> GetProductByID(ref Category headOfCategory, int IDProduct);
        LinkedList<Product> GetProductByName(ref Category headOfCategory, string productName);

        #endregion
        Product getNewProduct(string filePathProduct);

    }
}
