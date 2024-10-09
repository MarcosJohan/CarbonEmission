using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Dtos;
using Api.Models.Entities;
using Api.Repositories;

namespace Api.Tests.Helper;

public class CarbonEmissionInMemoryRepository : ICarbonEmissionRepository
{
    private readonly List<CarbonEmission> _carbonEmissions =
    [
        new CarbonEmission
        {
            Id = 1,
            CompanyId = 2,
            Date = DateTime.Today,
            Description = "Test Description",
            Deleted = false,
            Quantity = 5,
            Type = "TEST"
        },

        new CarbonEmission
        {
            Id = 2,
            CompanyId = 7,
            Date = DateTime.Today.AddDays(2),
            Description = "Test Description 2",
            Deleted = false,
            Quantity = 1,
            Type = "TEST 2"
        }
    ];
    public Task<List<CarbonEmission>> GetAll()
    {
        return Task.FromResult(_carbonEmissions);
    }

    public Task<CarbonEmission> GetById(int id)
    {
        return Task.FromResult(_carbonEmissions.FirstOrDefault(e => e.Id == id));
    }

    public Task<List<CarbonEmission>> GetByCompanyId(int companyId)
    {
        return Task.FromResult(_carbonEmissions.Where(e => e.CompanyId == companyId).ToList());
    }

    public Task<int> Add(CarbonEmissionDto carbonEmission)
    {
        var newCarbonEmission = carbonEmission.ToEntity();
        newCarbonEmission.Id = _carbonEmissions.Max(e => e.Id) + 1;
        
        _carbonEmissions.Add(newCarbonEmission);
        
        return Task.FromResult(newCarbonEmission.Id);
    }

    public async Task Update(int id, CarbonEmissionDto newCarbonEmission)
    {
        var carbonEmission = await GetById(id);
        _carbonEmissions.Remove(carbonEmission);
        
        carbonEmission.Date = newCarbonEmission?.Date ?? carbonEmission.Date;
        carbonEmission.CompanyId = newCarbonEmission.CompanyId.Equals(0) ? carbonEmission.CompanyId : newCarbonEmission.CompanyId;
        carbonEmission.Description = newCarbonEmission?.Description ?? carbonEmission.Description;
        carbonEmission.Type = newCarbonEmission?.Type ?? carbonEmission.Type;
        carbonEmission.Quantity = newCarbonEmission.Quantity.Equals(0) ? carbonEmission.Quantity : newCarbonEmission.Quantity;
        
        _carbonEmissions.Add(carbonEmission);
        
    }

    public Task Delete(int id)
    {
        _carbonEmissions.RemoveAll(e => e.Id == id);
        return Task.CompletedTask;
    }
}