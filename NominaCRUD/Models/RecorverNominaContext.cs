using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NominaCRUD.Models;

public partial class RecorverNominaContext : DbContext
{
    public RecorverNominaContext()
    {
    }

    public RecorverNominaContext(DbContextOptions<RecorverNominaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Deduccione> Deducciones { get; set; }

    public virtual DbSet<DeduccionesEmpleado> DeduccionesEmpleados { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<IngresosEmpleado> IngresosEmpleados { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<TiposIngreso> TiposIngresos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deduccione>(entity =>
        {
            entity.HasKey(e => e.DeduccionId).HasName("PK__Deduccio__7CA7AF31761BEC8A");

            entity.Property(e => e.DeduccionId).HasColumnName("deduccion_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreDeduccion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_deduccion");
        });

        modelBuilder.Entity<DeduccionesEmpleado>(entity =>
        {
            entity.HasKey(e => e.DeduccionEmpleadoId).HasName("PK__Deduccio__B8800CAE968A4C6D");

            entity.ToTable("DeduccionesEmpleado");

            entity.Property(e => e.DeduccionEmpleadoId).HasColumnName("deduccion_empleado_id");
            entity.Property(e => e.DeduccionId).HasColumnName("deduccion_id");
            entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");
            entity.Property(e => e.FechaDeduccion).HasColumnName("fecha_deduccion");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");

            entity.HasOne(d => d.Deduccion).WithMany(p => p.DeduccionesEmpleados)
                .HasForeignKey(d => d.DeduccionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Deduccion__deduc__2F10007B");

            entity.HasOne(d => d.Empleado).WithMany(p => p.DeduccionesEmpleados)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Deduccion__emple__2E1BDC42");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.EmpleadoId).HasName("PK__Empleado__6FBB65FD92673996");

            entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");
            entity.Property(e => e.Cedula)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Codigo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Departamento)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("departamento");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.SalarioBase)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salario_base");
        });

        modelBuilder.Entity<IngresosEmpleado>(entity =>
        {
            entity.HasKey(e => e.IngresoId).HasName("PK__Ingresos__4E42CFD9925DE6EB");

            entity.ToTable("IngresosEmpleado");

            entity.Property(e => e.IngresoId).HasColumnName("ingreso_id");
            entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");
            entity.Property(e => e.TipoIngresoId).HasColumnName("tipo_ingreso_id");

            entity.HasOne(d => d.Empleado).WithMany(p => p.IngresosEmpleados)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__IngresosE__emple__286302EC");

            entity.HasOne(d => d.TipoIngreso).WithMany(p => p.IngresosEmpleados)
                .HasForeignKey(d => d.TipoIngresoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__IngresosE__tipo___29572725");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__Pagos__FFF0A58EBF039C15");

            entity.Property(e => e.PagoId).HasColumnName("pago_id");
            entity.Property(e => e.EmpleadoId).HasColumnName("empleado_id");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.SalarioNeto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salario_neto");
            entity.Property(e => e.TotalDeducciones)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_deducciones");
            entity.Property(e => e.TotalIngresos)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_ingresos");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.EmpleadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__empleado___31EC6D26");
        });

        modelBuilder.Entity<TiposIngreso>(entity =>
        {
            entity.HasKey(e => e.TipoIngresoId).HasName("PK__TiposIng__BECB7C0524566E5F");

            entity.Property(e => e.TipoIngresoId).HasColumnName("tipo_ingreso_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreTipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
