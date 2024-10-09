using Api.Models.Entities;

namespace Api.Repositories;

public interface IReportRepository
{
    Task<List<CarbonEmission>> GetByType(string type);
    
    Task<List<CarbonEmission>> GetByRangeDate(DateTime startDate, DateTime endDate);
}