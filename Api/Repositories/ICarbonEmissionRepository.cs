using Api.Models.Dtos;
using Api.Models.Entities;

namespace Api.Repositories;

public interface ICarbonEmissionRepository
{
    Task<List<CarbonEmission>> GetAll();
    
    Task<CarbonEmission> GetById(int id);
    
    Task<List<CarbonEmission>> GetByCompanyId(int companyId);
    
    Task<int> Add(CarbonEmissionDto carbonEmission);
 
    Task Update(int id,  CarbonEmissionDto carbonEmission);
    
    Task Delete(int id);
}