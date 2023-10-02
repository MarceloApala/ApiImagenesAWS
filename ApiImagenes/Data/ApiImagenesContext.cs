using System;
using System.Collections.Generic;
using ApiImagenes.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiImagenes.Data;

public partial class ApiImagenesContext : DbContext
{
    public ApiImagenesContext()
    {
    }

    public ApiImagenesContext(DbContextOptions<ApiImagenesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Imagen> Imagens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=M4RC3L0\\SQLEXPRESS; Database=ApiImagenes ;User=sa; Password=Univalle; Trusted_Connection=true; Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
