using Api.Models.Entities;
using Api.Repositories;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Api.Services;

public class ReportService(IReportRepository repository) : IReportService
{
    public async Task<byte[]> GetByType(string type)
    {
        var carbonEmissions = await repository.GetByType(type);
        return GetReport(carbonEmissions);
    }

    public async Task<byte[]> GetByRangeDate(DateTime startDate, DateTime endDate)
    {
        var carbonEmissions = await repository.GetByRangeDate(startDate, endDate);
        return GetReport(carbonEmissions);
    }

    private byte[] GetReport(List<CarbonEmission> carbonEmissions)
    {
         var report = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30); 
                
                page.Header().ShowOnce().Row(row =>
                {
                    var iconPath = GetPathImage();
                    var imageData = System.IO.File.ReadAllBytes(iconPath);
                    
                    row.ConstantItem(150).Image(imageData);
                    
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().AlignCenter().Text("PULPOLINE").Bold().FontSize(14);
                        col.Item().AlignCenter().Text("Pérez Zeledón, San José, Costa Rica").Bold().FontSize(9);
                        col.Item().AlignCenter().Text("+506 8485 9039").Bold().FontSize(9);
                        col.Item().AlignCenter().Text("hola@pulpoline.com").Bold().FontSize(9);
                    });
                    
                });
                
                page.Content().Padding(10).Column(col1 =>
                {
                    col1.Item().LineHorizontal(0.5f);
                    
                    col1.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                           columns.RelativeColumn(1.2f); 
                           columns.RelativeColumn(1.2f); 
                           columns.RelativeColumn(2); 
                           columns.RelativeColumn(); 
                           columns.RelativeColumn(2); 
                           columns.RelativeColumn(); 
                        });

                        table.Cell().Background("#B64AEB")
                            .Padding(2).AlignCenter().Text("Identificador").FontColor("#fff");
                        
                        table.Cell().Background("#B64AEB")
                            .Padding(2).AlignCenter().Text("Compañia").FontColor("#fff");
                        
                        table.Cell().Background("#B64AEB")
                            .Padding(2).AlignCenter().Text("Descripción").FontColor("#fff");
                        
                        table.Cell().Background("#B64AEB")
                            .Padding(2).AlignCenter().Text("Cantidad").FontColor("#fff");
                        
                        table.Cell().Background("#B64AEB")
                            .Padding(2).AlignCenter().Text("Fecha").FontColor("#fff");
                        
                        table.Cell().Background("#B64AEB")
                            .Padding(2).AlignCenter().Text("Tipo").FontColor("#fff");

                        foreach (var item in carbonEmissions)
                        {
                            table.Cell().BorderBottom(0.5f).Padding(4).BorderColor("D9D9D9")
                                .Padding(2).AlignCenter().Text(item.Id).FontSize(10);
                            table.Cell().BorderBottom(0.5f).Padding(4).BorderColor("D9D9D9")
                                .Padding(2).AlignCenter().Text(item.CompanyId).FontSize(10);
                            table.Cell().BorderBottom(0.5f).Padding(4).BorderColor("D9D9D9")
                                .Padding(2).AlignCenter().Text(item.Description).FontSize(10);
                            table.Cell().BorderBottom(0.5f).Padding(4).BorderColor("D9D9D9")
                                .Padding(2).AlignCenter().Text(item.Quantity).FontSize(10);
                            table.Cell().BorderBottom(0.5f).Padding(4).BorderColor("D9D9D9")
                                .Padding(2).AlignCenter().Text(item.Date).FontSize(10);
                            table.Cell().BorderBottom(0.5f).Padding(4).BorderColor("D9D9D9")
                                .Padding(2).AlignCenter().Text(item.Type).FontSize(10);
                        }
                    });
                    if (carbonEmissions.Count > 0)
                    {
                        col1.Item().Background(Colors.Grey.Lighten3).Padding(10)
                            .Column(column =>
                            {
                                column.Item().Text("Copyright \u00a9 2024 PulpoLine Themes. All Rights Reserved.").FontSize(9).AlignCenter();
                            });    
                    }
                    
                    col1.Spacing(10);
                });

                page.Footer().AlignRight().Text(txt =>
                {
                    txt.Span("Pagina ").FontSize(10);
                    txt.CurrentPageNumber().FontSize(10);
                    txt.Span("de ").FontSize(10);
                    txt.TotalPages().FontSize(10);
                });
            });
        });    
    
        return report.GeneratePdf();
    }
    private string GetPathImage()
    {
       return Path.Combine(Directory.GetCurrentDirectory(), "Resources\\pulpoline.webp");
    }
}