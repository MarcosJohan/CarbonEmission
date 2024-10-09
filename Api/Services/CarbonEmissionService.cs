using Api.Models.Dtos;
using Api.Models.Entities;
using Api.Repositories;

namespace Api.Services;

public class CarbonEmissionService(ICarbonEmissionRepository repository) : ICarbonEmissionService
{
    public async Task<List<CarbonEmission>> GetAll()
    {
        return await repository.GetAll();
    }

    public async Task<CarbonEmission> GetById(int id)
    {
        return await repository.GetById(id);
    }

    public async Task<List<CarbonEmission>> GetByCompanyId(int companyId)
    {
        return await repository.GetByCompanyId(companyId);
    }
    
    public async Task<int> Add(CarbonEmissionDto carbonEmission)
    {
        return await repository.Add(carbonEmission);
    }

    public async Task Update(int id, CarbonEmissionDto carbonEmission)
    {
        await repository.Update(id, carbonEmission);
    }

    public async Task Delete(int id)
    {
        await repository.Delete(id);
    }
}