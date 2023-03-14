using System;
using System.Collections.Generic;

namespace DL;

public partial class MovimientoTipo
{
    public byte IdMoviminetoTipo { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; } = new List<Movimiento>();
}
