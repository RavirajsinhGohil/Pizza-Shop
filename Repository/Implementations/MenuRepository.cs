using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Interfaces;
using Repository.Models;
using Repository.ViewModel;

namespace Repository.Implementations;

public class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _dbo;
    public MenuRepository(ApplicationDbContext dbo)
    {
        _dbo = dbo;
    }

    public async Task<List<CategoryViewModel>> GetCategories()
        {
            return await _dbo.Menucategories
                // .Where(c => !c.Isdeleted)
                .Select(c => new CategoryViewModel
                {
                    Categoryid = c.Menucategoryid,
                    Name = c.Categoryname,
                    Description = c.Description
                })
                .ToListAsync();
        }

        public async Task<List<ItemViewModel>> GetItems(int categoryId)
        {
            return await _dbo.Items
                .Where(i => i.Categoryid == categoryId && !i.Isdeleted  && !i.Ismodifiable)
                .Select(i => new ItemViewModel
                {
                    Itemid = i.Itemid,
                    Name = i.Itemname,
                    Rate = i.Rate ?? 0,
                    Quantity = i.Quantity,
                    Itemtype = i.Itemtype,
                    Isavailable = i.Available,
                    Itemimage = i.Itemimage ?? "~/images/dinning-menu.png"
                })
                .ToListAsync();
        }

        public async Task<List<ModifierGroupViewModel>> GetModifierGroups()
        {
            return await _dbo.Modifiergroups
                .Where(m => !m.Isdeleted)
                .Select(m => new ModifierGroupViewModel
                {
                    ModifierGroupId = m.Modifiergroupid,
                    modifierGroupName = m.Modifiername,
                    modifierGroupDescription = m.Description
                })
                .ToListAsync();
        }

        public async Task AddCategory(Menucategory menucategory)
        {
            await _dbo.Menucategories.AddAsync(menucategory);
            await _dbo.SaveChangesAsync();
        }

        public Menucategory GetCategoryForEdit(int categoryId){
            return _dbo.Menucategories.FirstOrDefault(c => c.Menucategoryid == categoryId && c.Isdeleted == false);
        }




    



}
