using Microsoft.AspNetCore.Http;

namespace Repository.ViewModel;

public class ItemViewModel
{
    public int Itemid { get; set; }
    public string? Name { get; set; }
    public decimal? Rate { get; set; }
    public string? Itemtype { get; set; }

    public string? Unit { get; set; }
    public decimal? Quantity { get; set; }
    public bool Isavailable { get; set; }
    public string? Itemimage { get; set; } = "";
    public IFormFile? ItemPhoto { get; set; }
    public decimal? Tax { get; set; }
    public string? ItemShortCode { get; set; }
    public string? Description { get; set; }
    public int? CategoryId { get; set; }

    public bool Isdeleted { get; set; }

    public static implicit operator List<object>(ItemViewModel v)
    {
        throw new NotImplementedException();
    }

      public string ItemTypeIcon
        {
            get
            {
                switch (Itemtype)
                {
                    case "Veg":
                        return "/images/icons/veg-icon.svg";
                    case "Non-Veg":
                        return "/images/icons/non-veg-icon.svg";
                    case "Vegan":
                        return "/images/icons/vegan-icon.svg";
                    default:
                        return "/images/icons/unknown-icon.svg";
                }
            }
        }
}
