using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project0.DataAccess
{
    public partial class Project0Context : DbContext
    {
        public Project0Context()
        {
        }

        public Project0Context(DbContextOptions<Project0Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Cupcake> Cupcake { get; set; }
        public virtual DbSet<CupcakeOrder> CupcakeOrder { get; set; }
        public virtual DbSet<CupcakeOrderItem> CupcakeOrderItem { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<LocationInventory> LocationInventory { get; set; }
        public virtual DbSet<RecipeItem> RecipeItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Cupcake>(entity =>
            {
                entity.ToTable("Cupcake", "Project0");

                entity.HasIndex(e => e.Type)
                    .HasName("UQ__Cupcake__F9B8A48BD820A887")
                    .IsUnique();

                entity.Property(e => e.Cost)
                    .HasColumnType("decimal(8, 2)")
                    .HasDefaultValueSql("((6.00))");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CupcakeOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__CupcakeO__C3905BCF66D6EB89");

                entity.ToTable("CupcakeOrder", "Project0");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CupcakeOrder)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.CupcakeOrder)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Location");
            });

            modelBuilder.Entity<CupcakeOrderItem>(entity =>
            {
                entity.ToTable("CupcakeOrderItem", "Project0");

                entity.HasIndex(e => new { e.OrderId, e.CupcakeId })
                    .HasName("OrderToCupcake")
                    .IsUnique();

                entity.Property(e => e.CupcakeId).HasColumnName("CupcakeID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Cupcake)
                    .WithMany(p => p.CupcakeOrderItem)
                    .HasForeignKey(d => d.CupcakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_Cupcake");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CupcakeOrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_CupcakeOrder");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "Project0");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.DefaultLocationNavigation)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.DefaultLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Default_Location");
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient", "Project0");

                entity.HasIndex(e => e.Type)
                    .HasName("UQ__Ingredie__F9B8A48B558B5A5A")
                    .IsUnique();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Units)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location", "Project0");
            });

            modelBuilder.Entity<LocationInventory>(entity =>
            {
                entity.ToTable("LocationInventory", "Project0");

                entity.HasIndex(e => new { e.LocationId, e.IngredientId })
                    .HasName("InventoryIngredient")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.IngredientId).HasColumnName("IngredientID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.LocationInventory)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingredient");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.LocationInventory)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location");
            });

            modelBuilder.Entity<RecipeItem>(entity =>
            {
                entity.ToTable("RecipeItem", "Project0");

                entity.HasIndex(e => new { e.CupcakeId, e.IngredientId })
                    .HasName("CupcakeIngredient")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 6)");

                entity.Property(e => e.CupcakeId).HasColumnName("CupcakeID");

                entity.Property(e => e.IngredientId).HasColumnName("IngredientID");

                entity.HasOne(d => d.Cupcake)
                    .WithMany(p => p.RecipeItem)
                    .HasForeignKey(d => d.CupcakeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipe_Cupcake");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.RecipeItem)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recipe_Ingredient");
            });
        }
    }
}
