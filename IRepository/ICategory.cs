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
        void deleteCategory(ref Category headOfCategory, Category deleteCategory);
        #endregion

        #region File
        void updateFile(ref Category headOfCategory);
        #endregion

        bool validateCategory(string newCategoryName);
        Category getNewCategory();
    }
}
