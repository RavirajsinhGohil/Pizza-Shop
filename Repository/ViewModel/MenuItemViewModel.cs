using Repository.Models;

namespace Repository.ViewModel;

public class MenuItemViewModel
{
    public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
    public List<ModifierGroupViewModel> ModifierGroups { get; set; } = new List<ModifierGroupViewModel>();

    public List<ModifiersViewModel> Modifiers { get; set; } = new List<ModifiersViewModel>();
    
}
