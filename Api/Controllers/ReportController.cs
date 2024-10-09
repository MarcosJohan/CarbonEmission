using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/report")]
[Authorize(Roles = "Admin")]
public class ReportController(IReportService reportService) : ControllerBase
{
    private readonly IReportService _reportService = reportService;

    ///<summary>
    /// Genera un pdf con todas las emisiones de carbono de un tipo.
    /// </summary>
    /// <param name="type"> tipo de emisión</param>
    /// <returns>Pdf.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(byte[]),200)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<IResult> GetByType([FromQuery] string type)
    {
        var result = await _reportService.GetByType(type);
        return Results.File(result, "application/pdf", $"carbon-emission-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.pdf");
    }
    
    
    ///<summary>
    /// Genera un pdf con todas las emisiones de carbono en un intervalo de tiempo.
    /// </summary>
    /// <param name="startDate">Fecha de inicio</param>
    /// <param name="endDate">Fecha final</param>
    /// <returns>Pdf.</returns>
    [HttpGet("/range")]
    [ProducesResponseType(typeof(byte[]),200)]
    [ProducesResponseType(typeof(void),401)]
    public async Task<IResult> GetByType([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var result = await _reportService.GetByRangeDate(startDate, endDate);
        return Results.File(result, "application/pdf", $"carbon-emission-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.pdf");
    }
}