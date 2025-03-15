using System;
using System.Collections;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Ordermodifierdetail
{
    public int Ordermodifierdetailid { get; set; }

    public int? Orderdetailid { get; set; }

    public int? Itemid { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public BitArray? Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual Item? Item { get; set; }

    public virtual User? UpdatedbyNavigation { get; set; }
}
