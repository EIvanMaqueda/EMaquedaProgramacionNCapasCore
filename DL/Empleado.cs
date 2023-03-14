using System;
using System.Collections.Generic;

namespace DL;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string? NumeroEmpleado { get; set; }

    public string? Rfc { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string Email { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string? Nss { get; set; }

    public DateTime FechaIngreso { get; set; }

    public string? Foto { get; set; }

    public int? IdEmpresa { get; set; }

    public virtual ICollection<Dependiente> Dependientes { get; } = new List<Dependiente>();

    public virtual Empresa? IdEmpresaNavigation { get; set; }
    public string EmpresaNombre { get; set; }
    public string EmpresaTelefono { get; set; }
    public string EmpresaMail { get; set; }
    public string DireccionWeb { get; set; }
    public string Logo { get; set; }
}
