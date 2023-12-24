using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using NodinSoftProject.Domain.Models.Products;
using NodinSoftProject.Domain.Models.ProductUser;
using NodinSoftProject.Domain.Models.User;

namespace NodinSoftProject.Infrastructure.EFcore.Context
{
    public class NodinSoftProjectDBContext: IdentityDbContext<ApplicationUser>
    {
        public NodinSoftProjectDBContext(DbContextOptions<NodinSoftProjectDBContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect())
                        databaseCreator.Create();
                    if (!databaseCreator.HasTables())
                        databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductUser> ProductUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });

            modelBuilder.Entity<ProductUser>()
                .HasKey(bc => new { bc.Id });
            modelBuilder.Entity<ProductUser>()
                .HasOne(bc => bc.Product)
                .WithMany(b => b.UserProducts)
                .HasForeignKey(bc => bc.ProductId);
            modelBuilder.Entity<ProductUser>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.UserProducts)
                .HasForeignKey(bc => bc.UserId);
            #region ChangeTableName(Identity)
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            #endregion


        }
    }
}
