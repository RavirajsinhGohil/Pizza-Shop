using System;
using System.Collections;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Order
{
    public int Orderid { get; set; }

    public int? Customerid { get; set; }

    public int? Tableid { get; set; }

    public int Status { get; set; }

    public int Paymentmode { get; set; }

    public decimal Totalamount { get; set; }

    public string? Admincomment { get; set; }

    public string? Customerfeedback { get; set; }

    public decimal? Avgrating { get; set; }

    public BitArray? Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();

    public virtual ICollection<Orderrating> Orderratings { get; set; } = new List<Orderrating>();

    public virtual ICollection<Orderstatus> Orderstatuses { get; set; } = new List<Orderstatus>();

    public virtual Table? Table { get; set; }

    public virtual ICollection<Tablegrouping> Tablegroupings { get; set; } = new List<Tablegrouping>();

    public virtual User? UpdatedbyNavigation { get; set; }
}
