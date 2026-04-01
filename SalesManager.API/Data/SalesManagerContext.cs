using Microsoft.EntityFrameworkCore;
using Models;

namespace SalesManager.API.Data
{
    public class SalesManagerContext : DbContext
    {
        public SalesManagerContext(DbContextOptions<SalesManagerContext> options) : base(options) { }
        public DbSet<Department> Department { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<StockMovement> StockMovement { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<FinancialManager> FinancialManager { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Department>(d =>
            {
                d.HasKey(d => d.Id);
                d.Property(d => d.Id).ValueGeneratedOnAdd();

                d.Property(d => d.DepartmentName).IsRequired().HasMaxLength(150);
                d.Property(d => d.CreatedAt).IsRequired();

                d.HasOne(d => d.User).WithMany().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });

            builder.Entity<Product>(p =>
            {
                p.HasKey(p => p.Id);
                p.Property(p => p.Id).ValueGeneratedOnAdd();

                p.Property(p => p.ProductName).IsRequired().HasMaxLength(150);
                p.Property(p => p.Price).IsRequired();
                p.Property(p => p.MinimumStock).IsRequired();

                p.HasOne(p => p.Department).WithMany().HasForeignKey(p => p.DepartmentId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                p.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });

            builder.Entity<StockMovement>(sm =>
            {
                sm.HasKey(sm => sm.Id);
                sm.Property(sm => sm.Id).ValueGeneratedOnAdd();

                sm.Property(sm => sm.Quantity).IsRequired();
                sm.Property(sm => sm.MovementType).IsRequired();
                sm.Property(sm => sm.CreatedAt).IsRequired();
                sm.Property(sm => sm.Quantity).IsRequired(); 

                sm.HasOne(sm => sm.Product).WithMany().HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                sm.HasOne(sm => sm.Client).WithMany().HasForeignKey(p => p.ClientID).OnDelete(DeleteBehavior.NoAction);
                sm.HasOne(sm => sm.User).WithMany().HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });

            builder.Entity<Client>(c =>
            {
                c.HasKey(c => c.Id);
                c.Property(c => c.Id).ValueGeneratedOnAdd();

                c.Property(c => c.ClientName).IsRequired();
                c.Property(c => c.ClientEmail).IsRequired();
                c.Property(c => c.ClientAddressCity).IsRequired();
                c.Property(c => c.ClientAddressState).IsRequired();
                c.Property(c => c.ClientCEP).IsRequired();

                c.HasOne(c => c.User).WithMany().HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });

            builder.Entity<FinancialManager>(fm =>
            {
                fm.HasKey(fm => fm.Id);
                fm.Property(fm => fm.Id).ValueGeneratedOnAdd();

                fm.Property(fm => fm.GainSalesOfProduct).IsRequired();
                fm.Property(fm => fm.LossOrExpenseOfProduct).IsRequired();
                fm.Property(fm => fm.ProfitOfProduct).IsRequired();

                fm.HasOne(fm => fm.Product).WithMany().HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.NoAction).IsRequired();
                fm.HasOne(fm => fm.User).WithMany().HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction).IsRequired();
            });

            builder.Entity<User>(r =>
            {
                r.HasKey(r => r.Id);
                r.Property(r => r.Id).ValueGeneratedOnAdd();

                r.Property(r => r.CompleteName).IsRequired();
                r.Property(r => r.Email).IsRequired();
                r.Property(r => r.Password).IsRequired();
            });
        }
    }
}
