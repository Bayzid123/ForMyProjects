using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ApiForMyProjects.Models;

namespace ApiForMyProjects.DbContexts
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<TblTrack> TblTracks { get; set; }
        public virtual DbSet<TblUser> TblUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-TFFNGL1\\SQLEXPRESS;Initial Catalog=ForMyProjects;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblTrack>(entity =>
            {
                entity.HasKey(e => e.IntTrackId);

                entity.ToTable("tblTrack");

                entity.Property(e => e.IntTrackId).HasColumnName("intTrackId");

                entity.Property(e => e.DteCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("dteCreatedAt");

                entity.Property(e => e.IntAlbumId).HasColumnName("intAlbumId");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.StrComposer)
                    .HasMaxLength(50)
                    .HasColumnName("strComposer");

                entity.Property(e => e.StrTrackName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("strTrackName");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.IntUserId)
                    .HasName("PK_User");

                entity.ToTable("tblUser");

                entity.Property(e => e.IntUserId).HasColumnName("intUserId");

                entity.Property(e => e.DteCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("dteCreatedAt");

                entity.Property(e => e.DteUpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("dteUpdatedAt");

                entity.Property(e => e.IntCreatedBy).HasColumnName("intCreatedBy");

                entity.Property(e => e.IntUpdatedBy).HasColumnName("intUpdatedBy");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.StrEmail)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("strEmail");

                entity.Property(e => e.StrPassword)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("strPassword");

                entity.Property(e => e.StrUserName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("strUserName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
