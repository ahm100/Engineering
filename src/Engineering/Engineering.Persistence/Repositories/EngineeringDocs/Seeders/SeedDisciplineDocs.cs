
namespace Engineering.Persistence.Repositories.EngineeringDocs.Seeders;


public sealed class SeedDisciplineDocModel
{
    public string Code { get; init; } = string.Empty;

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}

public static class SeedDisciplineDocs
{
    public static readonly IReadOnlyCollection<SeedDisciplineDocModel> All =
    [
        new()
        {
            Code = "DRW",
            Title = "Drawing",
            Description = "نقشه مهندسی"
        },
        new()
        {
            Code = "PLN",
            Title = "Plan",
            Description = "پلان یا نقشه طرح"
        },
        new()
        {
            Code = "PFD",
            Title = "PFD",
            Description = "نمودار جریان فرآیند"
        },
        new()
        {
            Code = "PID",
            Title = "P&ID",
            Description = "نمودار لوله‌کشی و ابزار دقیق"
        },
        new()
        {
            Code = "GA",
            Title = "GA",
            Description = "نقشه کلی و جانمایی"
        },
        new()
        {
            Code = "ISO",
            Title = "Isometric",
            Description = "نقشه ایزومتریک"
        },
        new()
        {
            Code = "EQP",
            Title = "Equipment Drawing",
            Description = "نقشه فنی تجهیز"
        },
        new()
        {
            Code = "DS",
            Title = "Datasheet",
            Description = "مشخصات فنی تجهیز یا آیتم"
        },
        new()
        {
            Code = "CAL",
            Title = "Calculation",
            Description = "محاسبات مهندسی"
        },
        new()
        {
            Code = "SPC",
            Title = "Specification",
            Description = "مشخصات و الزامات فنی"
        },
        new()
        {
            Code = "RPT",
            Title = "Report",
            Description = "گزارش مهندسی"
        },
        new()
        {
            Code = "LAY",
            Title = "Layout",
            Description = "نقشه جانمایی"
        },
        new()
        {
            Code = "MTO",
            Title = "MTO",
            Description = "متره و مقادیر متریال"
        },
        new()
        {
            Code = "LST",
            Title = "List",
            Description = "فهرست مهندسی"
        },
        new()
        {
            Code = "SCH",
            Title = "Schedule",
            Description = "جداول و فهرست‌های مهندسی"
        },
        new()
        {
            Code = "DB",
            Title = "Design Basis",
            Description = "مبانی طراحی"
        },
        new()
        {
            Code = "PHY",
            Title = "Philosophy",
            Description = "اصول و رویکرد طراحی"
        },
        new()
        {
            Code = "PRC",
            Title = "Procedure",
            Description = "دستورالعمل انجام فعالیت"
        },
        new()
        {
            Code = "SLD",
            Title = "SLD",
            Description = "دیاگرام تک‌خطی"
        },
        new()
        {
            Code = "LPD",
            Title = "Loop Diagram",
            Description = "نمودار لوپ"
        },
        new()
        {
            Code = "ELV",
            Title = "Elevation",
            Description = "نما"
        },
        new()
        {
            Code = "SEC",
            Title = "Section",
            Description = "مقطع"
        },
        new()
        {
            Code = "DTL",
            Title = "Detail",
            Description = "جزئیات معماری"
        }
    ];
}