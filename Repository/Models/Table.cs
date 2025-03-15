using System;
using System.Collections;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Table
{
    public int Tableid { get; set; }

    public int? Sectionid { get; set; }

    public string? Tablename { get; set; }

    public int Capacity { get; set; }

    public BitArray? Status { get; set; }

    public BitArray? Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Section? Section { get; set; }

    public virtual ICollection<Tablegrouping> Tablegroupings { get; set; } = new List<Tablegrouping>();

    public virtual User? UpdatedbyNavigation { get; set; }
}
