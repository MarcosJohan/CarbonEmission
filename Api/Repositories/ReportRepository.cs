using Api.Models.Entities;
using Api.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories;

public class ReportRepository(CarbonEmissionDbContext context) : IReportRepository
{
    public async Task<List<CarbonEmission>> GetByType(string type)
    {
        return await context.CarbonEmissions.Where(e => e.Type.Equals(type))
            .ToListAsync();
    }

    public async Task<List<CarbonEmission>> GetByRangeDate(DateTime startDate, DateTime endDate)
    {
        return await context.CarbonEmissions.Where(e => e.Date >= startDate && e.Date <= endDate)
            .ToListAsync();
    }
}