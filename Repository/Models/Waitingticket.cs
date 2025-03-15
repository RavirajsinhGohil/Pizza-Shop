using System;
using System.Collections;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Waitingticket
{
    public int Waitingticketid { get; set; }

    public int? Customerid { get; set; }

    public int? Sectionid { get; set; }

    public int? People { get; set; }

    public BitArray? Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual User? Section { get; set; }

    public virtual User? UpdatedbyNavigation { get; set; }
}
