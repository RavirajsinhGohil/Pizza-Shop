using System;
using System.Collections;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Section
{
    public int Sectionid { get; set; }

    public string Sectionname { get; set; } = null!;

    public string? Description { get; set; }

    public BitArray? Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public int? Createdby { get; set; }

    public DateTime? Updatedat { get; set; }

    public int? Updatedby { get; set; }

    public virtual User? CreatedbyNavigation { get; set; }

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();

    public virtual User? UpdatedbyNavigation { get; set; }
}
