using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    public interface ICategory
    {
        void insertCategory(ref Category headOfCategory, Category newCategory);
        void initCategoryList(ref Category headOfCategory);
        bool validateCategory(string newCategoryName);
        Category getNewCategory();
        void updateCategory(ref Category headOfCategory, Category updateCategory);
    }
}
