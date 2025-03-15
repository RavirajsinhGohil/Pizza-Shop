using System;
using System.Collections;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Taxesandfee
{
    public int Taxid { get; set; }

    public int? Taxname { get; set; }

    public int Taxtype { get; set; }

    public decimal Taxvalue { get; set; }

    public BitArray? Isenabled { get; set; }

    public BitArray? Isdefault { get; set; }

    public BitArray? Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual User? UpdatedbyNavigation { get; set; }
}
