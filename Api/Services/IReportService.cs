namespace Api.Services;

public interface IReportService
{
    Task<byte[]> GetByType(string type);
    
    Task<byte[]> GetByRangeDate(DateTime startDate, DateTime endDate);
}