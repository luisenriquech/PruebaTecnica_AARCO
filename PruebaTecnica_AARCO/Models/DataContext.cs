using Microsoft.EntityFrameworkCore;
using PruebaTecnica_AARCO.DTO.RequestBody;

namespace PruebaTecnica_AARCO.Models
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                var cnn = config.GetConnectionString("cnn");
                if (cnn != null)
                    optionsBuilder.UseSqlServer(cnn);

            }
        }

        public virtual DbSet<ResponseRequestDto> uspRequest { get; set; } = null!;

        /// <summary>
        /// //////////////////////////////////// Contenido creado por Entity Framework
        /// </summary>
        /// 



        public virtual DbSet<Descripcion> Descripcions { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<ModeloPorSubmarca> ModeloPorSubmarcas { get; set; } = null!;
        public virtual DbSet<Submarca> Submarcas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Descripcion>(entity =>
            {
                entity.ToTable("Descripcion");

                entity.Property(e => e.DescripcionId).HasColumnName("descripcionId");

                entity.Property(e => e.DescripcionTexto)
                    .HasMaxLength(255)
                    .HasColumnName("descripcionTexto");

                entity.Property(e => e.ModeloId).HasColumnName("modeloId");

                entity.HasOne(d => d.Modelo)
                    .WithMany(p => p.Descripcions)
                    .HasForeignKey(d => d.ModeloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Descripci__model__3F466844");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("Marca");

                entity.Property(e => e.MarcaId).HasColumnName("marcaId");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<ModeloPorSubmarca>(entity =>
            {
                entity.HasKey(e => e.ModeloId)
                    .HasName("PK__ModeloPo__E83E4CC5A7569241");

                entity.ToTable("ModeloPorSubmarca");

                entity.Property(e => e.ModeloId).HasColumnName("modeloId");

                entity.Property(e => e.Anio).HasColumnName("anio");

                entity.Property(e => e.SubmarcaId).HasColumnName("submarcaId");

                entity.HasOne(d => d.Submarca)
                    .WithMany(p => p.ModeloPorSubmarcas)
                    .HasForeignKey(d => d.SubmarcaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ModeloPor__subma__3C69FB99");
            });

            modelBuilder.Entity<Submarca>(entity =>
            {
                entity.ToTable("Submarca");

                entity.Property(e => e.SubmarcaId).HasColumnName("submarcaId");

                entity.Property(e => e.MarcaId).HasColumnName("marcaId");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.Submarcas)
                    .HasForeignKey(d => d.MarcaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Submarca__marcaI__398D8EEE");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Usuario");

                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("fechaCreacion");

                entity.Property(e => e.IdUsuario)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idUsuario");

                entity.Property(e => e.Password)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usuario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
