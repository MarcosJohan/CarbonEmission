using Api.Models.Dtos;
using Api.Models.Entities;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class CarbonEmissionController(ICarbonEmissionService service) : ControllerBase
{

    ///<summary>
    /// Obtiene todas las emisiones de carbono registradas.
    /// </summary>
    /// <returns>Una lista de emisiones de carbono.</returns>
    [HttpGet("/emissions")]
    [ProducesResponseType(typeof(List<CarbonEmission>),200)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<ActionResult<List<CarbonEmission>>> Get()
    {
        return await service.GetAll();
    }
    
    ///<summary>
    /// Obtiene una emisión de carbono por ID.
    /// </summary>
    /// <param name="id"> ID de la emisión</param>
    /// <returns>La emisión de carbono con el ID especificado.</returns>
    [HttpGet("/emissions/{id}")]
    [ProducesResponseType(typeof(CarbonEmission),200)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<ActionResult<CarbonEmission>> GetById([FromRoute] int id)
    {
        return await service.GetById(id);
    }
    
    ///<summary>
    /// Obtiene una lista de emisiones de carbono por ID de compañia.
    /// </summary>
    /// <param name="companyId"> ID de la compañia</param>
    /// <returns>Las emisiones de carbono con el ID de la compañia especificada.</returns>
    [HttpGet("/emissions/company/{companyId}")]
    [ProducesResponseType(typeof(List<CarbonEmission>),200)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<ActionResult<List<CarbonEmission>>> GetByCompanyId([FromRoute] int companyId)
    {
        return await service.GetByCompanyId(companyId);
    }
    
    ///<summary>
    /// Registra emisiones de carbono.
    /// </summary>
    /// <param name="carbonEmission">Emisión de carbon a registrar</param>
    /// <returns>=ID de la emisión creada.</returns>
    [HttpPost("/emissions")]
    [ProducesResponseType(typeof(void), 201)]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), 400)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<ActionResult<int>> Add([FromBody]CarbonEmissionDto carbonEmission)
    {
        var id = await service.Add(carbonEmission);
        return Created($"/emissions/{id}", id);
    }
    
    ///<summary>
    /// Modifica una emisión de carbono.
    /// </summary>
    /// <param name="carbonEmission">Emisión de carbon a modificar</param>
    [HttpPut("/emissions/{id}")]
    [ProducesResponseType(typeof(void),200)]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), 400)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody]CarbonEmissionDto carbonEmission)
    {
        await service.Update(id, carbonEmission);
        return Ok();
    }
 
    ///<summary>
    /// Elimina una emisión de carbono.
    /// </summary>
    /// <param name="id">ID de la emisión</param>
    [HttpDelete("/emissions/{id}")]
    [ProducesResponseType(typeof(void),200)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await service.Delete(id);
        return Ok();
    }
}