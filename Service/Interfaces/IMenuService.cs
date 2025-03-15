using Repository.Models;
using Repository.ViewModel;

namespace Service.Interfaces;

public interface IMenuService
{
    Task<MenuItemViewModel> GetMenuModel(int categoryId);
    bool AddCategory(CategoryViewModel model);
    Menucategory GetCategoryForEdit(int categoryId);
}
