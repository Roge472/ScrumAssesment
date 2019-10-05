using BaseModelLibrary.Lib.Models;
using BaseModelLibrary.Lib.Models.UserModels;
using BaseModelLibrary.Models.CloudModels;
using BaseModelLibrary.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternetShopDBContext.Lib.Contexts
{
    public class InternetShopContext : DbContext
    {
        #region AuthModels

        //Email login is not implemented
        public DbSet<EmailLogin> EmailLogins { get; set; }
        public DbSet<LoginProvider> LoginProviders { get; set; }
        public DbSet<SocialLogin> SocialLogins { get; set; }

        #endregion

        #region CloudModels

        public DbSet<ImageModel> ImageModels { get; set; }

        #endregion

        #region Users
        
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Secrets.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Auth

            // email
            modelBuilder.Entity<EmailLogin>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Hash).IsRequired();
                entity.Property(e => e.Salt).IsRequired();
                entity.HasOne(e => e.User).WithOne(e=>e.EmailLogin).OnDelete(DeleteBehavior.Cascade);
            });

            //social
            modelBuilder.Entity<LoginProvider>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<SocialLogin>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProviderKey).IsRequired();
                entity.HasOne(s => s.User).WithMany(u => u.SocialLogins).OnDelete(DeleteBehavior.Cascade);
            });
            #endregion

            #region CLOUD
            // Image model set through attribute

            #endregion

            #region User

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(u => u.Role).WithMany(r => r.Users);
            });

            #endregion


        }
    }
}
