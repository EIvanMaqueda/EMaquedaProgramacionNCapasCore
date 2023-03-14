using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class EmaquedaProgramacionNcapasContext : DbContext
{
    public EmaquedaProgramacionNcapasContext()
    {
    }

    public EmaquedaProgramacionNcapasContext(DbContextOptions<EmaquedaProgramacionNcapasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aseguradora> Aseguradoras { get; set; }

    public virtual DbSet<Colonium> Colonia { get; set; }

    public virtual DbSet<Dependiente> Dependientes { get; set; }

    public virtual DbSet<DependienteTipo> DependienteTipos { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EmpresaPoliza> EmpresaPolizas { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<MovimientoDetalle> MovimientoDetalles { get; set; }

    public virtual DbSet<MovimientoTipo> MovimientoTipos { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Poliza> Polizas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Subpoliza> Subpolizas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vigencium> Vigencia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= EMaquedaProgramacionNCapas;TrustServerCertificate=True; Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aseguradora>(entity =>
        {
            entity.HasKey(e => e.IdAseguradora).HasName("PK__Asegurad__8FA1C597714C7AF6");

            entity.ToTable("Aseguradora");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Aseguradoras)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Asegurado__IdUsu__2D27B809");
        });

        modelBuilder.Entity<Colonium>(entity =>
        {
            entity.HasKey(e => e.IdColonia).HasName("PK__Colonia__A1580F6672DD4BA9");

            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Colonia)
                .HasForeignKey(d => d.IdMunicipio)
                .HasConstraintName("FK__Colonia__IdMunic__38996AB5");
        });

        modelBuilder.Entity<Dependiente>(entity =>
        {
            entity.HasKey(e => e.IdDependiente).HasName("PK__Dependie__366D07713FAB34E6");

            entity.ToTable("Dependiente");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Genero)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rfc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RFC");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDependienteTipoNavigation).WithMany(p => p.Dependientes)
                .HasForeignKey(d => d.IdDependienteTipo)
                .HasConstraintName("FK__Dependien__IdDep__245D67DE");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Dependientes)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__Dependien__IdEmp__236943A5");
        });

        modelBuilder.Entity<DependienteTipo>(entity =>
        {
            entity.HasKey(e => e.IdDependienteTipo).HasName("PK__Dependie__2C220C623DFDB9DE");

            entity.ToTable("DependienteTipo");

            entity.Property(e => e.IdDependienteTipo).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__Direccio__1F8E0C76A0601FE5");

            entity.ToTable("Direccion");

            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroExterior)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroInterior)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdColonia)
                .HasConstraintName("FK__Direccion__IdCol__3B75D760");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Direccion__IdUsu__3C69FB99");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9EB6F97DBA");

            entity.ToTable("Empleado");

            entity.HasIndex(e => e.Email, "UQ__Empleado__A9D10534E01466F7").IsUnique();

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("date");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Foto).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nss)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NSS");
            entity.Property(e => e.NumeroEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rfc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RFC");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEmpresa)
                .HasConstraintName("FK__Empleado__IdEmpr__208CD6FA");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmpresa).HasName("PK__Empresa__5EF4033EB5104889");

            entity.ToTable("Empresa");

            entity.Property(e => e.DireccionWeb)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.Logo).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmpresaPoliza>(entity =>
        {
            entity.HasKey(e => e.IdEmpresaPoliza).HasName("PK__EmpresaP__482AADC569F1C7C4");

            entity.ToTable("EmpresaPoliza");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

            entity.HasOne(d => d.IdAseguradoraNavigation).WithMany(p => p.EmpresaPolizas)
                .HasForeignKey(d => d.IdAseguradora)
                .HasConstraintName("FK__EmpresaPo__IdAse__619B8048");

            entity.HasOne(d => d.IdPolizaNavigation).WithMany(p => p.EmpresaPolizas)
                .HasForeignKey(d => d.IdPoliza)
                .HasConstraintName("FK__EmpresaPo__IdPol__628FA481");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.EmpresaPolizas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__EmpresaPo__IdUsu__6383C8BA");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC155159F6D");

            entity.ToTable("Estado");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Estados)
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("FK__Estado__IdPais__31EC6D26");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimien__881A6AE0B1560C6A");

            entity.ToTable("Movimiento");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

            entity.HasOne(d => d.IdMovimientoTipoNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdMovimientoTipo)
                .HasConstraintName("FK__Movimient__IdMov__778AC167");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Movimient__IdUsu__787EE5A0");
        });

        modelBuilder.Entity<MovimientoDetalle>(entity =>
        {
            entity.HasKey(e => e.IdMovimientoDetalle).HasName("PK__Movimien__91770702611ADC1D");

            entity.ToTable("MovimientoDetalle");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.MovimientoDetalles)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK__Movimient__IdSta__74AE54BC");
        });

        modelBuilder.Entity<MovimientoTipo>(entity =>
        {
            entity.HasKey(e => e.IdMoviminetoTipo).HasName("PK__Movimien__812871634E4F6952");

            entity.ToTable("MovimientoTipo");

            entity.Property(e => e.IdMoviminetoTipo).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PK__Municipi__610059780DEB3EA2");

            entity.ToTable("Municipio");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Municipio__IdEst__34C8D9D1");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PK__Pais__FC850A7B675F8282");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Poliza>(entity =>
        {
            entity.HasKey(e => e.Idpoliza).HasName("PK__Poliza__606E4A94FCDCA001");

            entity.ToTable("Poliza");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroPoliza)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdSubpolizaNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdSubpoliza)
                .HasConstraintName("FK__Poliza__IdSubpol__5DCAEF64");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Polizas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Poliza__IdUsuari__5EBF139D");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584CE99653CA");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.IdStatus).HasName("PK__Status__B450643ADC9424C9");

            entity.ToTable("Status");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subpoliza>(entity =>
        {
            entity.HasKey(e => e.IdSubPoliza).HasName("PK__Subpoliz__FF74BEB57DE96DEC");

            entity.ToTable("Subpoliza");

            entity.Property(e => e.IdSubPoliza)
                .ValueGeneratedOnAdd()
                .HasColumnName("idSubPoliza");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97BBBBEA33");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534E6C9E34A").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Usuario__C9F28456AB3D444A").IsUnique();

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Imagen).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__IdRol__29572725");
        });

        modelBuilder.Entity<Vigencium>(entity =>
        {
            entity.HasKey(e => e.IdVigencia).HasName("PK__Vigencia__C8CFC77291E6CBD4");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaFin).HasColumnType("date");
            entity.Property(e => e.FechaInicio).HasColumnType("date");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

            entity.HasOne(d => d.IdPolizaNavigation).WithMany(p => p.Vigencia)
                .HasForeignKey(d => d.IdPoliza)
                .HasConstraintName("FK__Vigencia__IdPoli__6D0D32F4");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Vigencia)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Vigencia__IdUsua__6C190EBB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
