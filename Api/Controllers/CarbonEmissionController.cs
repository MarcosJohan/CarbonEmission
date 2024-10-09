using Api.Models.Dtos;
using Api.Models.Entities;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class CarbonEmissionController(ICarbonEmissionService service) : ControllerBase
{

    [HttpGet("/emissions")]
    [ProducesResponseType(typeof(List<CarbonEmission>),200)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<ActionResult<List<CarbonEmission>>> Get()
    {
        return await service.GetAll();
    }
    
    [HttpGet("/emissions/{id}")]
    [ProducesResponseType(typeof(CarbonEmission),200)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<ActionResult<CarbonEmission>> GetById([FromRoute] int id)
    {
        return await service.GetById(id);
    }
    
    [HttpGet("/emissions/company/{companyId}")]
    [ProducesResponseType(typeof(List<CarbonEmission>),200)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<ActionResult<List<CarbonEmission>>> GetByCompanyId([FromRoute] int companyId)
    {
        return await service.GetByCompanyId(companyId);
    }
    
    [HttpPost("/emissions")]
    [ProducesResponseType(typeof(void), 201)]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), 400)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<ActionResult<int>> Add([FromBody]CarbonEmissionDto carbonEmission)
    {
        var id = await service.Add(carbonEmission);
        return Created($"/emissions/{id}", id);
    }
    
    [HttpPut("/emissions/{id}")]
    [ProducesResponseType(typeof(void),200)]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), 400)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody]CarbonEmissionDto carbonEmission)
    {
        await service.Update(id, carbonEmission);
        return Ok();
    }
 
    [HttpDelete("/emissions/{id}")]
    [ProducesResponseType(typeof(void),200)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await service.Delete(id);
        return Ok();
    }
}