using System.Threading.Tasks;
using Repository.Interfaces;
using Repository.Models;
using Repository.ViewModel;
using Service.Interfaces;

namespace Service.Implementations;

public class MenuService : IMenuService
{
    private IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<MenuItemViewModel> GetMenuModel(int categoryId)
    {
        var categories = await _menuRepository.GetCategories();
        var items = await _menuRepository.GetItems(categoryId);
        var modifierGroups = await _menuRepository.GetModifierGroups();

        return new MenuItemViewModel
        {
            Categories = categories,
            Items = items,
            ModifierGroups = modifierGroups
        };
    }

    public bool AddCategory(CategoryViewModel model)
    {
        try
        {
            var category = new Menucategory
            {
                Menucategoryid = model.Categoryid,
                Categoryname = model.Name,
                Description = model.Description,
                Isdeleted = false
            };
             _menuRepository.AddCategory(category);
            return true;
        }
        catch
        {
            return false;
        }

    }

    public Menucategory GetCategoryForEdit(int categoryId){
        return _menuRepository.GetCategoryForEdit(categoryId);
    }



}
