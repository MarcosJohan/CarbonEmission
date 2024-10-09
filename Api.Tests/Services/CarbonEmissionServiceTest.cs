using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Dtos;
using Api.Services;
using Api.Tests.Helper;
using JetBrains.Annotations;
using Xunit;

namespace Api.Tests.Services;

[TestSubject(typeof(CarbonEmissionService))]
public class CarbonEmissionServiceTest
{
    private readonly CarbonEmissionService _service = new(new CarbonEmissionInMemoryRepository());

    [Fact]
    public async Task GetAllShouldReturnAllCarbonEmissions()
    {
        var carbonEmissions = await _service.GetAll();
        
        Assert.NotNull(carbonEmissions);
        Assert.True(carbonEmissions.Count > 0);
    }

    [Fact]
    public async Task GetByIdShouldReturnTheCorrectCarbonEmission()
    {
        var carbonEmission = await _service.GetById(1);
        
        Assert.NotNull(carbonEmission);
        Assert.Equal(2, carbonEmission.CompanyId);
        Assert.Equal(DateTime.Today, carbonEmission.Date);
        Assert.Equal(5, carbonEmission.Quantity);
        Assert.Equal("TEST", carbonEmission.Type);
        Assert.Equal("Test Description", carbonEmission.Description);
    }

    [Fact]
    public async Task GetByCompanyIdShouldReturnTheCorrectCarbonEmissionsByCompanyId()
    {
        var carbonEmissions = await _service.GetByCompanyId(7);
        var carbonEmission = carbonEmissions.FirstOrDefault();
        
        Assert.NotNull(carbonEmissions);
        Assert.True(carbonEmissions.Count > 0);
        Assert.Equal(7, carbonEmission.CompanyId);
        Assert.Equal(DateTime.Today.AddDays(2), carbonEmission.Date);
        Assert.Equal(1, carbonEmission.Quantity);
        Assert.Equal("TEST 2", carbonEmission.Type);
        Assert.Equal("Test Description 2", carbonEmission.Description);
    }

    [Fact]
    public async Task AddShouldAddCarbonEmission()
    {
        var newCarbonEmission = new CarbonEmissionDto
        {
            CompanyId = 3,
            Description = "Test Description 3",
            Type = "TEST 3",
            Date = DateTime.Today
        };
        
        var idCarbonEmission = await _service.Add(newCarbonEmission);
        var carbonEmission = await _service.GetById(idCarbonEmission);
        
        Assert.NotNull(carbonEmission);
        Assert.True(idCarbonEmission > 0);
        Assert.Equal(newCarbonEmission.CompanyId, carbonEmission.CompanyId);
        Assert.Equal(newCarbonEmission.Date, carbonEmission.Date);
        Assert.Equal(newCarbonEmission.Quantity, carbonEmission.Quantity);
        Assert.Equal(newCarbonEmission.Type, carbonEmission.Type);
        Assert.Equal(newCarbonEmission.Description, carbonEmission.Description);
    }

    [Fact]
    public async Task UpdateShouldUpdateCarbonEmissionById()
    {
        var newCarbonEmission = new CarbonEmissionDto
        {
            CompanyId = 10,
            Description = "Test Description 4",
            Type = "TEST 4",
            Date = DateTime.Today,
            Quantity = 3
        };
        
        var idCarbonEmission = await _service.Add(newCarbonEmission);
        
        var updatedCarbonEmission =  new CarbonEmissionDto
        {
            CompanyId = 11,
            Description = "Update Description",
            Type = "Update",
            Date = DateTime.Today.AddDays(9)
        };

        await _service.Update(idCarbonEmission, updatedCarbonEmission);
        
        var carbonEmission = await _service.GetById(idCarbonEmission);
        
        Assert.NotNull(carbonEmission);
        Assert.True(idCarbonEmission > 0);
        Assert.Equal(updatedCarbonEmission.CompanyId, carbonEmission.CompanyId);
        Assert.Equal(updatedCarbonEmission.Date, carbonEmission.Date);
        Assert.Equal(newCarbonEmission.Quantity, carbonEmission.Quantity);
        Assert.Equal(updatedCarbonEmission.Type, carbonEmission.Type);
        Assert.Equal(updatedCarbonEmission.Description, carbonEmission.Description);
    }

    [Fact]
    public async Task DeleteShouldDeleteCarbonEmissionById()
    {
        var newCarbonEmission = new CarbonEmissionDto
        {
            CompanyId = 12,
            Description = "Test Description 5",
            Type = "TEST 5",
            Date = DateTime.Today
        };
        
        var idCarbonEmission = await _service.Add(newCarbonEmission);
        
        await _service.Delete(idCarbonEmission);
        
        var carbonEmission = await _service.GetById(idCarbonEmission);
        
        Assert.Null(carbonEmission);
    }
}