using Repository.Models;
using Repository.ViewModel;

namespace Repository.Interfaces;

public interface IMenuRepository
{
    Task<List<CategoryViewModel>> GetCategories();
    Task<List<ItemViewModel>> GetItems(int categoryId);
    Task<List<ModifierGroupViewModel>> GetModifierGroups();
    Task AddCategory(Menucategory menucategory);
    Menucategory GetCategoryForEdit(int categoryId);
}
