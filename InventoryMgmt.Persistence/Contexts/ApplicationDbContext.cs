using InventoryMgmt.Domain.Commons;
using InventoryMgmt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryMgmt.Persistence.Contexts
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added: 
                        entry.Entity.IsActive = true;
                        entry.Entity.IsDeleted = false;
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedIPAddress = "SYSTEM";
                        break;
                    case EntityState.Modified: 
                        entry.Entity.ModifiedDate = DateTime.UtcNow;
                        entry.Entity.ModifiedIPAddress = "SYSTEM";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }



        public DbSet<Login> Login { get; set; }
        public DbSet<CarDetails> CarDetails { get; set; }

    }
}
