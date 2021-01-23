using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace DataAccess.Models
{
    public partial class whatsappContext : DbContext
    {
        public whatsappContext()
        {
        }

        public whatsappContext(DbContextOptions<whatsappContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Import> Imports { get; set; }
        public virtual DbSet<ImportStatus> ImportStatuses { get; set; }
        public virtual DbSet<RawDatum> RawData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBConn"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Import>(entity =>
            {
                entity.ToTable("import");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasColumnName("file_name");

                entity.Property(e => e.ImportStatusId).HasColumnName("import_status_id");

                entity.Property(e => e.ImportedBy)
                    .IsRequired()
                    .HasColumnName("imported_by");

                entity.Property(e => e.ImpotedDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("impoted_date_time");

                entity.HasOne(d => d.ImportStatus)
                    .WithMany(p => p.Imports)
                    .HasForeignKey(d => d.ImportStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_import_import_status");
            });

            modelBuilder.Entity<ImportStatus>(entity =>
            {
                entity.ToTable("import_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<RawDatum>(entity =>
            {
                entity.ToTable("raw_data");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Chat).HasColumnName("chat");

                entity.Property(e => e.ChatDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("chat_date_time");

                entity.Property(e => e.ImportId).HasColumnName("import_id");

                entity.HasOne(d => d.Import)
                    .WithMany(p => p.RawData)
                    .HasForeignKey(d => d.ImportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_raw_data_import");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
