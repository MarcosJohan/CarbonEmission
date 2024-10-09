using System.Linq.Expressions;
using Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Context;

public class CarbonEmissionDbContext(DbContextOptions<CarbonEmissionDbContext> options) 
    : DbContext(options)
{
    public DbSet<CarbonEmission> CarbonEmissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarbonEmission>().ToTable("CarbonEmission");
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType)) continue;
            
            var parameter = Expression.Parameter(entityType.ClrType, "e");
            var filter = Expression.Lambda(
                Expression.Equal(
                    Expression.Property(parameter, nameof(ISoftDelete.Deleted)),
                    Expression.Constant(false)
                ),
                parameter
            );

            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
        }

    }
}