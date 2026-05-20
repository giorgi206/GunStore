using System;
using GunShop.Models;
using Microsoft.EntityFrameworkCore;

namespace GunShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Knife> Knives { get; set; }
        public DbSet<Ammunition> Ammunitions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------- User --------
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.FullName).IsRequired().HasMaxLength(150);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
                entity.HasIndex(u => u.Email).IsUnique();
            });

            // -------- Category --------
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);

                entity.HasMany(c => c.Weapons)
                    .WithOne(w => w.Category)
                    .HasForeignKey(w => w.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(c => c.Knives)
                    .WithOne(k => k.Category)
                    .HasForeignKey(k => k.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(c => c.Ammunitions)
                    .WithOne(a => a.Category)
                    .HasForeignKey(a => a.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // -------- Weapon --------
            modelBuilder.Entity<Weapon>(entity =>
            {
                entity.HasKey(w => w.Id);
                entity.Property(w => w.Name).IsRequired().HasMaxLength(150);
                entity.Property(w => w.Manufacturer).IsRequired().HasMaxLength(100);
                entity.Property(w => w.Price).HasColumnType("decimal(18,2)");

                entity.HasMany(w => w.OrderItems)
                    .WithOne(oi => oi.Weapon)
                    .HasForeignKey(oi => oi.WeaponId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // -------- Knife --------
            modelBuilder.Entity<Knife>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.Property(k => k.Name).IsRequired().HasMaxLength(150);

                // BladeType-ის კონფიგურაცია
                entity.Property(k => k.BladeType).HasMaxLength(100);

                entity.Property(k => k.BladeMaterial).HasMaxLength(100);
                entity.Property(k => k.BladeLength).HasColumnType("decimal(18,2)");
                entity.Property(k => k.Price).HasColumnType("decimal(18,2)");
            });

            // -------- Ammunition --------
            modelBuilder.Entity<Ammunition>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(150);
                entity.Property(a => a.Manufacturer).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Caliber).IsRequired().HasMaxLength(50);
                entity.Property(a => a.Price).HasColumnType("decimal(18,2)");
            });

            // -------- Order --------
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.TotalPrice).HasColumnType("decimal(18,2)");

                entity.HasMany(o => o.Items)
                    .WithOne(i => i.Order)
                    .HasForeignKey(i => i.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(o => o.User)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // -------- OrderItem --------
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.Id);
                entity.Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(oi => oi.Quantity).IsRequired();
            });
        }
    }
}