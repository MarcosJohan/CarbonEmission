using Api.Models.Dtos;
using Api.Models.Entities;
using Api.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class CarbonEmissionRepository(CarbonEmissionDbContext context) : ICarbonEmissionRepository
{
    public async Task<List<CarbonEmission>> GetAll()
    {
        return await context.CarbonEmissions.ToListAsync();
    }

    public async Task<CarbonEmission> GetById(int id)
    {
        return await context.CarbonEmissions
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<CarbonEmission>> GetByCompanyId(int companyId)
    {
        return await context.CarbonEmissions.Where(e => e.CompanyId == companyId)
            .ToListAsync();  
    }

    public async Task<int> Add(CarbonEmissionDto carbonEmission)
    {
        var entity = await context.CarbonEmissions.AddAsync(carbonEmission.ToEntity());
        await context.SaveChangesAsync();
        
        return entity.Entity.Id;
    }

    public async Task Update(int id, CarbonEmissionDto carbonEmission)
    {
        await context.CarbonEmissions.Where(e => e.Id == id)
            .ExecuteUpdateAsync(p =>
                p.SetProperty(n => n.CompanyId, carbonEmission.CompanyId)
                    .SetProperty(n => n.Description, carbonEmission.Description)
                    .SetProperty(n => n.Quantity, carbonEmission.Quantity)
                    .SetProperty(n => n.Type, carbonEmission.Type)
            );
        
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        await context.CarbonEmissions.Where(e => e.Id == id)
            .ExecuteUpdateAsync(p =>
                p.SetProperty(n => n.Deleted, true));
        await context.SaveChangesAsync();
    }
}