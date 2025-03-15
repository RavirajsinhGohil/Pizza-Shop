using System;
using System.Collections;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Customer
{
    public int Customerid { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public BitArray? Isdeleted { get; set; }

    public DateTime Createdat { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual Role CustomerNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? UpdatedbyNavigation { get; set; }

    public virtual ICollection<Waitingticket> Waitingtickets { get; set; } = new List<Waitingticket>();
}
