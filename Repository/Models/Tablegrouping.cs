using System;
using System.Collections;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Tablegrouping
{
    public int Tablegroupingid { get; set; }

    public int? Orderid { get; set; }

    public int? Tableid { get; set; }

    public BitArray? Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Table? Table { get; set; }

    public virtual User? UpdatedbyNavigation { get; set; }
}
