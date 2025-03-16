using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Repository;
using Repository.Data;
using Repository.ViewModel;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Web.Controllers;

public class MenuController : Controller
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;
    private readonly IMenuService _menuService;
    private readonly ApplicationDbContext _db;

    public MenuController(IAuthService authService, IUserService userService, ApplicationDbContext db, IMenuService menuService)
    {
        _authService = authService;
        _userService = userService;
        _db = db;
        _menuService = menuService;
    }
    [HttpGet("Menu")]
    public async Task<IActionResult> Menu(int categoryid = 1)
    {
        var viewModel = await _menuService.GetMenuModel(categoryid);
            return View(viewModel);
        // var categories = _menuService.GetCategories;
        // var categories = await _db.Menucategories
        //     .Where(c => c.Isdeleted == false)
        //     .Select(c => new CategoryViewModel
        //     {
        //         Categoryid = c.Menucategoryid,
        //         Name = c.Categoryname,
        //         Description = c.Description
        //     })
        //     .ToListAsync();

        // var items = await _db.Items
        //     .Where(c => c.Isdeleted == false && c.Ismodifiable == false && c.Categoryid == categoryid)
        //     .Select(i => new ItemViewModel
        //     {
        //         Itemid = i.Itemid,
        //         Name = i.Itemname,
        //         Rate = i.Rate ?? 0,
        //         Quantity = i.Quantity,
        //         Itemtype = i.Itemtype,
        //         Isavailable = i.Available,
        //         Itemimage = i.Itemimage ?? "~/images/dinning-menu.png"
        //     })
        //     .ToListAsync();

        // var modifierGroup = await _db.Modifiergroups
        //     .Where(c => c.Isdeleted == false)
        //     .Select(c => new ModifierGroupViewModel
        //     {
        //         ModifierGroupId = c.Modifiergroupid,
        //         modifierGroupName = c.Modifiername,
        //         modifierGroupDescription = c.Description
        //     })
        //     .ToListAsync();

        // var modifiers = await _db.Items
        //     .Where(c => c.Isdeleted == false && c.Ismodifiable == true && c.Categoryid == categoryid)
        //     .Select(i => new ItemViewModel
        //     {
        //         Itemid = i.Itemid,
        //         Name = i.Itemname,
        //         Rate = i.Rate ?? 0,
        //         Quantity = i.Quantity,
        //         Itemtype = i.Itemtype,
        //         Isavailable = i.Available,
        //         Itemimage = i.Itemimage ?? "~/images/dinning-menu.png"
        //     })
        //     .ToListAsync();

        // var viewmodel = new MenuItemViewModel
        // {
        //     Categories = categories,
        //     Items = items,
        //     ModifierGroups = modifierGroup
        // };

        // return View(viewmodel);
    }

    public async Task<IActionResult> ItemList(int categoryid, string searchTerm = "", int page = 1, int pageSize = 5)
    {
        // var categories = await _db.Menucategories
        //     .Where(c => c.Isdeleted == false)
        //     .Select(c => new CategoryViewModel
        //     {
        //         Categoryid = c.Menucategoryid,
        //         Name = c.Categoryname,
        //         Description = c.Description
        //     })
        //     .ToListAsync();

        // var items = await _db.Items
        //     .Where(c => c.Isdeleted == false && c.Ismodifiable == false && c.Categoryid == categoryid)
        //     .Select(i => new ItemViewModel
        //     {
        //         Itemid = i.Itemid,
        //         Name = i.Itemname,
        //         Rate = i.Rate ?? 0,
        //         Quantity = i.Quantity,
        //         Itemtype = i.Itemtype,
        //         Isavailable = i.Available,
        //         Itemimage = i.Itemimage ?? "~/images/dinning-menu.png"
        //     })
        //     .ToListAsync();

        // var viewmodel = new MenuItemViewModel
        // {
        //     Categories = categories,
        //     Items = items
        // };


        // IQueryable<Item> query = System.Linq.IQueryable<_db.Items.FirstOrDefault(i => i.Categoryid ==categoryid)>.AsQueryable();

        // Apply search filter
        // if (!string.IsNullOrEmpty(searchTerm))
        // {
        //     query = query.Where(u => u.Firstname.Contains(searchTerm) || u.Lastname.Contains(searchTerm) && u.Isdeleted == false);
        // }

        // int totalItems = query.Count();
        // var users = query.Skip((page - 1) * pageSize)
        //                  .Take(pageSize)
        //                  .ToList();

        // return new UserPaginationViewModel
        // {
        //     Users = users,
        //     CurrentPage = page,
        //     PageSize = pageSize,
        //     TotalItems = totalItems,
        // };

        return View();
    }

    [HttpGet]
    public IActionResult GetItemsByCategory(int categoryid = 1)
    {
        var items = _db.Items
                        .Where(c => c.Categoryid == categoryid && c.Isdeleted == false && c.Ismodifiable == false)
                        .ToList();

        var itemViewModels = items.Select(item => new ItemViewModel
        {
            Itemid = item.Itemid,
            Name = item.Itemname,
            Itemtype = item.Itemtype,
            Rate = item.Rate ?? 0,
            Quantity = item.Quantity,
            Itemimage = item.Itemimage,
            Isavailable = item.Available
        }).ToList();

        return PartialView("_MenuItems", itemViewModels);
    }





    [HttpGet("AddCategory")]
    public IActionResult AddCategory()
    {
        return View();
    }

    [HttpPost("AddCategory")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCategory(CategoryViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var category = _menuService.AddCategory(model);
                TempData["success"] = "Category is Added!";
                return RedirectToAction("Menu", "Menu");
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while adding the category. Please try again.";
                return View(model);
            }
        }

        return View(model);
    }


    // [HttpGet("EditCategory")]
    // public IActionResult EditCategory(int id)
    // {
    //     var category = _menuService.GetCategoryForEdit(id);
    //     if (category == null)
    //     {
    //         return NotFound();
    //     }
        // var categoryViewModel = new CategoryViewModel
        // {
        //     Categoryid = category.Menucategoryid,
        //     Name = category.Categoryname,
        //     Description = category.Description
        // };
    //     return View(category);
    // }

    [HttpPost("EditCategory")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCategory(CategoryViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var category = _db.Menucategories.FirstOrDefault(u => u.Menucategoryid == model.Categoryid);
                if (category == null)
                {
                    TempData["error"] = "Category not found.";
                    return RedirectToAction("Menu");
                }
                category.Categoryname = model.Name;
                category.Description = model.Description;
                _db.Menucategories.Update(category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category Updated Successfully!";
                return RedirectToAction("Menu");
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while editing the category. Please try again.";
                return RedirectToAction("Menu");
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult DeleteCategory(int categoryId)
    {
        var category = _db.Menucategories.FirstOrDefault(c => c.Menucategoryid == categoryId);
        if (category == null)
        {
            return NotFound();
        }
        category.Isdeleted = true;
        _db.SaveChanges();
        TempData["success"] = "Category is Deleted";
        return RedirectToAction("Menu");
    }

    [HttpPost("AddNewItem")]
    public async Task<IActionResult> AddNewItem(ItemViewModel model)
    {
        try
        {
            var menuItem = new Item
            {
                Categoryid = model.CategoryId,
                Itemname = model.Name,
                Itemtype = model.Itemtype,
                Rate = model.Rate,
                Quantity = model.Quantity,
                Unit = model.Unit,
                // Available = model.Isavailable,
                Tax = model.Tax,
                Itemshortcode = model.ItemShortCode,
                Description = model.Description,
                Available = true,
                // Itemimage = _userService.ImageUpload(model.ItemPhoto)
                Isdeleted = false,
                Createdat = DateTime.Now,

                // Createdby = 
            };

            _db.Items.Add(menuItem);
            await _db.SaveChangesAsync();

            TempData["success"] = "Menu item added successfully!";
            return RedirectToAction("Menu");
        }
        catch (Exception ex)
        {
            TempData["error"] = "An error occurred while adding the item. Please try again.";
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult EditMenuItem(int id)
    {
        try
        {
            var item = _db.Items
                          .Where(m => m.Itemid == id)
                          .FirstOrDefault();

            if (item == null)
            {
                return NotFound();
            }

            var categories = _db.Menucategories
                                .Select(c => new CategoryViewModel
                                {
                                    Categoryid = c.Menucategoryid,
                                    Name = c.Categoryname
                                }).ToList();

            var viewModel = new MenuItemViewModel
            {
                Items = new List<ItemViewModel>
                {
                    new ItemViewModel
                    {
                        Itemid = item.Itemid,
                        Name = item.Itemname,
                        CategoryId = item.Categoryid,
                        Itemtype = item.Itemtype,
                        Rate = item.Rate,
                        Quantity = item.Quantity,
                        Unit = item.Unit,
                        Isavailable = item.Available,
                        Tax = item.Tax,
                        ItemShortCode = item.Itemshortcode,
                        Description = item.Description,
                        Itemimage = item.Itemimage,
                        Isdeleted = item.Isdeleted,

                    }
                },
                Categories = categories
            };
            return PartialView("_EditMenuItemModal", viewModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error.");
        }
    }


    // POST: Edit Menu Item (update the item)
    [HttpPost]
    public IActionResult EditMenuItem(int id, MenuItemViewModel model)
    {
        try
        {
            var categories = _db.Menucategories
                               .Select(c => new CategoryViewModel
                               {
                                   Categoryid = c.Menucategoryid,
                                   Name = c.Categoryname
                               }).ToList();
            model.Categories = categories;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _db.Items.FirstOrDefault(c => c.Itemid == id);
            if (item == null)
            {
                return NotFound();
            }
            item.Itemname = model.Items.FirstOrDefault()?.Name;
            item.Categoryid = model.Items.FirstOrDefault()?.CategoryId ?? item.Categoryid;
            item.Itemtype = model.Items.FirstOrDefault()?.Itemtype;
            item.Rate = model.Items.FirstOrDefault()?.Rate ?? item.Rate;
            item.Quantity = model.Items.FirstOrDefault()?.Quantity ?? item.Quantity;
            item.Unit = model.Items.FirstOrDefault()?.Unit;
            item.Available = model.Items.FirstOrDefault()?.Isavailable ?? item.Available;
            item.Tax = model.Items.FirstOrDefault()?.Tax ?? item.Tax;
            item.Itemshortcode = model.Items.FirstOrDefault()?.ItemShortCode;
            item.Description = model.Items.FirstOrDefault()?.Description;
            item.Itemimage = model.Items.FirstOrDefault()?.Itemimage;

            _db.Items.Update(item);
            _db.SaveChanges();

            return Json(new { success = true, message = "Item updated successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error.");
        }
    }

    [HttpGet]
    public IActionResult DeleteMenuItem(int itemId)
    {
        var item = _db.Items.FirstOrDefault(c => c.Itemid == itemId);
        if (item == null)
        {
            return NotFound();
        }
        item.Isdeleted = true;
        _db.SaveChanges();
        TempData["success"] = "Item is Deleted";
        return RedirectToAction("Menu");
    }

    [HttpPost("AddModifierGroup")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddModifierGroup(ModifierGroupViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var modifiergroup = new Modifiergroup
                {
                    Modifiergroupid = model.ModifierGroupId,
                    Menucategoryid = 1,
                    Modifiername = model.modifierGroupName,
                    Description = model.modifierGroupDescription,
                    Isdeleted = false
                };
                await _db.Modifiergroups.AddAsync(modifiergroup);
                await _db.SaveChangesAsync();
                TempData["success"] = "Modifier Group is Added!";
                return RedirectToAction("Menu", "Menu");
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while adding the Modifier Group. Please try again.";
                return View(model);
            }
        }

        return View(model);
    }

    [HttpGet("EditModifierGroup")]
    public IActionResult EditModifierGroup(int id)
    {
        var modifiergroup = _db.Modifiergroups.FirstOrDefault(c => c.Menucategoryid == id && c.Isdeleted == false);
        if (modifiergroup == null)
        {
            return NotFound();
        }
        var modifierGroupViewModel = new ModifierGroupViewModel
        {
            ModifierGroupId = modifiergroup.Modifiergroupid,
            modifierGroupName = modifiergroup.Modifiername,
            modifierGroupDescription = modifiergroup.Description
        };
        return View(modifierGroupViewModel);
    }

    [HttpPost("EditModifierGroup")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditModifierGroup(ModifierGroupViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var modifiergroup = _db.Modifiergroups.FirstOrDefault(u => u.Modifiergroupid == model.ModifierGroupId);
                if (modifiergroup == null)
                {
                    TempData["error"] = "Modifier Group not found.";
                    return RedirectToAction("Menu");
                }
                modifiergroup.Modifiername = model.modifierGroupName;
                modifiergroup.Description = model.modifierGroupDescription;
                _db.Modifiergroups.Update(modifiergroup);
                await _db.SaveChangesAsync();
                TempData["success"] = "Modifier Group Updated Successfully!";
                return RedirectToAction("Menu", "Menu");
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while editing the category. Please try again.";
                return RedirectToAction("Menu");
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult DeleteModifierGroup(int modifierGroupId)
    {
        var modifiergroup = _db.Modifiergroups.FirstOrDefault(c => c.Modifiergroupid == modifierGroupId);
        if (modifiergroup == null)
        {
            return NotFound();
        }
        modifiergroup.Isdeleted = true;
        _db.SaveChanges();
        TempData["success"] = "Modifier Group is Deleted";
        return RedirectToAction("Menu");
    }

    [HttpPost("AddNewModifier")]
    public async Task<IActionResult> AddNewModifier(ModifiersViewModel model)
    {
        try
        {
            var menuItem = new Item
            {
                Categoryid = model.ModifierGroupId,
                Itemname = model.Name,
                Rate = model.Rate,
                Quantity = model.Quantity,
                Unit = model.Unit,
                // Available = model.Isavailable,
                Description = model.Description,
                Available = true,
                Ismodifiable = true,
                // Itemimage = _userService.ImageUpload(model.ItemPhoto)
                Isdeleted = false,
                Createdat = DateTime.Now,
                // Createdby = 
            };

            // var modifierMapping = new Itemmodifiergroupmapping
            // {
            //     Itemmodifiergroupmappingid = 1,
            //     Itemid = model.ModifierId,
            //     Modifiergroupid = model.ModifierGroupId
            // };
            
            // _db.Itemmodifiergroupmappings.Add(modifierMapping);
            _db.Items.Add(menuItem);
            await _db.SaveChangesAsync();

            TempData["success"] = "Menu item added successfully!";
            return RedirectToAction("Menu");
        }
        catch (Exception ex)
        {
            TempData["error"] = "An error occurred while adding the item. Please try again.";
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult GetModifiersByModifierGroup(int modifierid = 1)
    {
        var items = _db.Items
                        .Where(c => c.Categoryid == modifierid && c.Isdeleted == false && c.Ismodifiable == true)
                        .ToList();

        var itemViewModels = items.Select(item => new ModifiersViewModel
        {
            ModifierId = item.Itemid,
            ModifierGroupId = item.Categoryid,
            Name = item.Itemname,
            Rate = item.Rate ?? 0,
            Quantity = item.Quantity,
            Isdeleted = false
        }).ToList();

        return PartialView("_Modifiers", itemViewModels);
    }

    [HttpGet]
    public IActionResult EditModifier(int id)
    {
        try
        {
            var item = _db.Items
                          .Where(m => m.Itemid == id)
                          .FirstOrDefault();

            if (item == null)
            {
                return NotFound();
            }

            var modifierGroups = _db.Modifiergroups
                                .Select(m => new ModifierGroupViewModel
                                {
                                    ModifierGroupId = m.Modifiergroupid,
                                    modifierGroupName = m.Modifiername
                                }).ToList();

            var viewModel = new MenuItemViewModel
            {
                Modifiers = new List<ModifiersViewModel>
                {
                    new ModifiersViewModel
                    {
                        ModifierId = item.Itemid,
                        Name = item.Itemname,
                        ModifierGroupId = item.Categoryid,
                        Rate = item.Rate,
                        Quantity = item.Quantity,
                        Unit = item.Unit,
                        Description = item.Description,
                        Isdeleted = item.Isdeleted,
                    }
                },
                ModifierGroups = modifierGroups
            };
            return PartialView("_EditModifierModal", viewModel);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error.");
        }
    }


    




}
