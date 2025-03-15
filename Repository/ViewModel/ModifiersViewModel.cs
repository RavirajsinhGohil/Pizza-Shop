using Microsoft.AspNetCore.Http;

namespace Repository.ViewModel;

public class ModifiersViewModel
{
    public int ModifierId { get; set; }
    public int? ModifierGroupId { get; set; }
    public string? Name { get; set; }
    public string? Unit { get; set; }
    public decimal? Rate { get; set; }
    public decimal? Quantity { get; set; }

    // public decimal? Tax { get; set; }
    // public string? ItemShortCode { get; set; }
    
    public string? Description { get; set; }

    public bool? Isdeleted { get; set; }

    // public static implicit operator List<object>(ModifiersViewModel v)
    // {
    //     throw new NotImplementedException();
    // }

}
