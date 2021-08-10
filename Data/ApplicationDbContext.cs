using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RockLike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RockLike.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<SiteLike> SiteLike { get; set; }
        public DbSet<Site> Site { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SiteLike>()
                   .HasKey(c => new { c.IdSite, c.IdUser });

            builder.Entity<SiteLike>()
                   .HasOne(bc => bc.Site)
                   .WithMany(b => b.ListUserSiteLike)
                   .HasForeignKey(bc => bc.IdSite);

            builder.Entity<SiteLike>()
                   .HasOne(bc => bc.User)
                   .WithMany(c => c.ListaSiteLike)
                   .HasForeignKey(bc => bc.IdUser);
            base.OnModelCreating(builder);
        }

    }
}
