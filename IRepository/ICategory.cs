using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public interface ICategory
    {
        #region UI
        void insertCategory(ref Category headOfCategory, Category newCategory);
        void loadCategoryList(ref Category headOfCategory);
        void deleteCategory(ref Category headOfCategory, string deleteCategoryName);
        void updateCategory(ref Category headOfCategory, Category updateCategory, string needUpdateProduct);
        #endregion

        #region File
        void updateFile(ref Category headOfCategory);
        #endregion

        bool validateCategory(string newCategoryName);

        Category getNewCategory();

        string generateFilePathOfCategoryProduct(string categoryName, ref Category category);

        Category GetCategoryByName(string categoryName);

        Category GetCategoryByID(int categoryID);

    }
}
