using System;
using GunShop.Models;
using Microsoft.EntityFrameworkCore;

namespace GunShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
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
            // -------- User --------
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .IsRequired()
                .HasMaxLength(150);
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // -------- Category --------
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Weapons)
                .WithOne(w => w.Category)
                .HasForeignKey(w => w.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Knives)
                .WithOne(k => k.Category)
                .HasForeignKey(k => k.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Ammunitions)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // -------- Weapon --------
            modelBuilder.Entity<Weapon>()
                .HasKey(w => w.Id);
            modelBuilder.Entity<Weapon>()
                .Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(150);
            modelBuilder.Entity<Weapon>()
                .Property(w => w.Manufacturer)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Weapon>()
                .Property(w => w.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Weapon>()
                .HasMany(w => w.OrderItems)
                .WithOne(oi => oi.Weapon)
                .HasForeignKey(oi => oi.WeaponId)
                .OnDelete(DeleteBehavior.Restrict);

            // -------- Knife --------
            modelBuilder.Entity<Knife>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<Knife>()
                .Property(k => k.Name)
                .IsRequired()
                .HasMaxLength(150);
            modelBuilder.Entity<Knife>()
                .Property(k => k.BladeMaterial)
                .HasMaxLength(100);
            modelBuilder.Entity<Knife>()
                .Property(k => k.BladeLength)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Knife>()
                .Property(k => k.Price)
                .HasColumnType("decimal(18,2)");

            // -------- Ammunition --------
            modelBuilder.Entity<Ammunition>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<Ammunition>()
                .Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(150);
            modelBuilder.Entity<Ammunition>()
                .Property(a => a.Manufacturer)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Ammunition>()
                .Property(a => a.Caliber)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Ammunition>()
                .Property(a => a.Price)
                .HasColumnType("decimal(18,2)");

            // -------- Order --------
            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // -------- OrderItem --------
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.Id);
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Quantity)
                .IsRequired();
        }
    }
}