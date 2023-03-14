using System;
using System.Collections.Generic;

namespace DL;

public partial class Subpoliza
{
    public byte IdSubPoliza { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Poliza> Polizas { get; } = new List<Poliza>();
}
