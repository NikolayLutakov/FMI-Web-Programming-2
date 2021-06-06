using Microsoft.EntityFrameworkCore;
using SmallAds.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SmallAds.Entities.Addresess;

namespace SmallAds.Database
{

    public class AdsDbContext : DbContext
    {
        public AdsDbContext()
        {

        }

        public virtual DbSet<Ad> Ads { get; set; }
        public virtual DbSet<Addresess> Addresesses { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=192.168.0.116,1433;Initial Catalog=SmallAds;User ID=nlyutakov;Password=A123456a;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Ad>(entity =>
            {
                entity.Property(e => e.Title).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Ads)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ads_CreatorId");
            });

            modelBuilder.Entity<Addresess>(entity =>
            {
                entity.ToTable("Addresess");

                entity.Property(e => e.AddressText).IsRequired();
                entity.Property(e => e.TownId).IsRequired();

                entity.HasOne(d => d.Town)
                    .WithMany(p => p.Addresesses)
                    .HasForeignKey(d => d.TownId)
                    .HasConstraintName("FK_Addresess_TownI");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.HasOne(d => d.Ad)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.AdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Likes_AdId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Likes_UserId");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(20);
            });



            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username, "UC_Username").IsUnique(true);
                entity.HasIndex(u => u.Email, "UC_Email").IsUnique(true);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30);

                

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                

                entity.Property(e => e.AddressId)
                    .IsRequired();

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Users_AddressId");

            });
        }

    }
}
