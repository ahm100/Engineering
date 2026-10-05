namespace Engineering.Persistence.Repositories.EngineeringDocs.Seeders;

public sealed class SeedDisciplineDocTypeModel
{
    public string DisciplineCode { get; init; } = string.Empty;

    public string DisciplineDocCode { get; init; } = string.Empty;

    public string Code { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}


public static class SeedDisciplineDocTypes
{
    public static readonly IReadOnlyCollection<SeedDisciplineDocTypeModel> All =
    [
        // Process
        new()
        {
            DisciplineCode = "PRO",
            DisciplineDocCode = "PFD",
            Code = "PFD",
            Description = "نمودار جریان فرآیند"
        },
        new()
        {
            DisciplineCode = "PRO",
            DisciplineDocCode = "PID",
            Code = "PID",
            Description = "نمودار لوله‌کشی و ابزار دقیق"
        },
        new()
        {
            DisciplineCode = "PRO",
            DisciplineDocCode = "DS",
            Code = "DS",
            Description = "مشخصات فنی"
        },
        new()
        {
            DisciplineCode = "PRO",
            DisciplineDocCode = "CAL",
            Code = "CAL",
            Description = "محاسبات فرآیندی"
        },
        new()
        {
            DisciplineCode = "PRO",
            DisciplineDocCode = "RPT",
            Code = "RPT",
            Description = "گزارش مهندسی"
        },
        new()
        {
            DisciplineCode = "PRO",
            DisciplineDocCode = "SPC",
            Code = "SPC",
            Description = "مشخصات فنی"
        },

        // Mechanical
        new()
        {
            DisciplineCode = "MEC",
            DisciplineDocCode = "DRW",
            Code = "DRW",
            Description = "نقشه مهندسی"
        },
        new()
        {
            DisciplineCode = "MEC",
            DisciplineDocCode = "GA",
            Code = "GA",
            Description = "نقشه کلی و جانمایی تجهیز"
        },
        new()
        {
            DisciplineCode = "MEC",
            DisciplineDocCode = "EQP",
            Code = "EQP",
            Description = "نقشه فنی تجهیز"
        },
        new()
        {
            DisciplineCode = "MEC",
            DisciplineDocCode = "DS",
            Code = "DS",
            Description = "مشخصات فنی تجهیز"
        },
        new()
        {
            DisciplineCode = "MEC",
            DisciplineDocCode = "CAL",
            Code = "CAL",
            Description = "محاسبات مکانیکی"
        },
        new()
        {
            DisciplineCode = "MEC",
            DisciplineDocCode = "SPC",
            Code = "SPC",
            Description = "مشخصات فنی"
        },
        new()
        {
            DisciplineCode = "MEC",
            DisciplineDocCode = "RPT",
            Code = "RPT",
            Description = "گزارش مهندسی"
        },

        // Piping
        new()
        {
            DisciplineCode = "PIP",
            DisciplineDocCode = "DRW",
            Code = "DRW",
            Description = "نقشه مهندسی"
        },
        new()
        {
            DisciplineCode = "PIP",
            DisciplineDocCode = "LAY",
            Code = "LAY",
            Description = "جانمایی خطوط"
        },
        new()
        {
            DisciplineCode = "PIP",
            DisciplineDocCode = "ISO",
            Code = "ISO",
            Description = "نقشه ایزومتریک"
        },
        new()
        {
            DisciplineCode = "PIP",
            DisciplineDocCode = "GA",
            Code = "GA",
            Description = "نقشه کلی پایپینگ"
        },
        new()
        {
            DisciplineCode = "PIP",
            DisciplineDocCode = "MTO",
            Code = "MTO",
            Description = "متره و مقادیر متریال"
        },
        new()
        {
            DisciplineCode = "PIP",
            DisciplineDocCode = "LST",
            Code = "LST",
            Description = "فهرست خطوط و اقلام"
        },
        new()
        {
            DisciplineCode = "PIP",
            DisciplineDocCode = "CAL",
            Code = "CAL",
            Description = "محاسبات پایپینگ"
        },
        new()
        {
            DisciplineCode = "PIP",
            DisciplineDocCode = "SPC",
            Code = "SPC",
            Description = "مشخصات فنی"
        },

        // Civil
        new()
        {
            DisciplineCode = "CIV",
            DisciplineDocCode = "DRW",
            Code = "DRW",
            Description = "نقشه مهندسی"
        },
        new()
        {
            DisciplineCode = "CIV",
            DisciplineDocCode = "PLN",
            Code = "PLN",
            Description = "پلان عمرانی"
        },
        new()
        {
            DisciplineCode = "CIV",
            DisciplineDocCode = "LAY",
            Code = "LAY",
            Description = "جانمایی"
        },
        new()
        {
            DisciplineCode = "CIV",
            DisciplineDocCode = "GA",
            Code = "GA",
            Description = "نقشه کلی"
        },
        new()
        {
            DisciplineCode = "CIV",
            DisciplineDocCode = "CAL",
            Code = "CAL",
            Description = "محاسبات عمرانی"
        },
        new()
        {
            DisciplineCode = "CIV",
            DisciplineDocCode = "RPT",
            Code = "RPT",
            Description = "گزارش مهندسی"
        },
        new()
        {
            DisciplineCode = "CIV",
            DisciplineDocCode = "SPC",
            Code = "SPC",
            Description = "مشخصات فنی"
        },

        // Structural
        new()
        {
            DisciplineCode = "STR",
            DisciplineDocCode = "DRW",
            Code = "DRW",
            Description = "نقشه مهندسی"
        },
        new()
        {
            DisciplineCode = "STR",
            DisciplineDocCode = "PLN",
            Code = "PLN",
            Description = "پلان سازه"
        },
        new()
        {
            DisciplineCode = "STR",
            DisciplineDocCode = "GA",
            Code = "GA",
            Description = "نقشه کلی سازه"
        },
        new()
        {
            DisciplineCode = "STR",
            DisciplineDocCode = "CAL",
            Code = "CAL",
            Description = "محاسبات سازه"
        },
        new()
        {
            DisciplineCode = "STR",
            DisciplineDocCode = "RPT",
            Code = "RPT",
            Description = "گزارش مهندسی"
        },

        // Electrical
        new()
        {
            DisciplineCode = "ELE",
            DisciplineDocCode = "DRW",
            Code = "DRW",
            Description = "نقشه مهندسی"
        },
        new()
        {
            DisciplineCode = "ELE",
            DisciplineDocCode = "SLD",
            Code = "SLD",
            Description = "دیاگرام تک‌خطی"
        },
        new()
        {
            DisciplineCode = "ELE",
            DisciplineDocCode = "PLN",
            Code = "PLN",
            Description = "پلان برق"
        },
        new()
        {
            DisciplineCode = "ELE",
            DisciplineDocCode = "LAY",
            Code = "LAY",
            Description = "جانمایی تجهیزات"
        },
        new()
        {
            DisciplineCode = "ELE",
            DisciplineDocCode = "DS",
            Code = "DS",
            Description = "مشخصات تجهیزات"
        },
        new()
        {
            DisciplineCode = "ELE",
            DisciplineDocCode = "CAL",
            Code = "CAL",
            Description = "محاسبات برق"
        },
        new()
        {
            DisciplineCode = "ELE",
            DisciplineDocCode = "SPC",
            Code = "SPC",
            Description = "مشخصات فنی"
        },
        new()
        {
            DisciplineCode = "ELE",
            DisciplineDocCode = "LST",
            Code = "LST",
            Description = "فهرست تجهیزات و کابل‌ها"
        },

        // Instrumentation
        new()
        {
            DisciplineCode = "INS",
            DisciplineDocCode = "DRW",
            Code = "DRW",
            Description = "نقشه مهندسی"
        },
        new()
        {
            DisciplineCode = "INS",
            DisciplineDocCode = "LAY",
            Code = "LAY",
            Description = "جانمایی ابزار دقیق"
        },
        new()
        {
            DisciplineCode = "INS",
            DisciplineDocCode = "DS",
            Code = "DS",
            Description = "مشخصات ابزار دقیق"
        },
        new()
        {
            DisciplineCode = "INS",
            DisciplineDocCode = "LPD",
            Code = "LPD",
            Description = "نمودار لوپ"
        },
        new()
        {
            DisciplineCode = "INS",
            DisciplineDocCode = "CAL",
            Code = "CAL",
            Description = "محاسبات ابزار دقیق"
        },
        new()
        {
            DisciplineCode = "INS",
            DisciplineDocCode = "SPC",
            Code = "SPC",
            Description = "مشخصات فنی"
        },
        new()
        {
            DisciplineCode = "INS",
            DisciplineDocCode = "LST",
            Code = "LST",
            Description = "فهرست ابزار دقیق و I/O"
        },

        // Architecture
        new()
        {
            DisciplineCode = "ARC",
            DisciplineDocCode = "DRW",
            Code = "DRW",
            Description = "نقشه معماری"
        },
        new()
        {
            DisciplineCode = "ARC",
            DisciplineDocCode = "PLN",
            Code = "PLN",
            Description = "پلان معماری"
        },
        new()
        {
            DisciplineCode = "ARC",
            DisciplineDocCode = "ELV",
            Code = "ELV",
            Description = "نما"
        },
        new()
        {
            DisciplineCode = "ARC",
            DisciplineDocCode = "SEC",
            Code = "SEC",
            Description = "مقطع"
        },
        new()
        {
            DisciplineCode = "ARC",
            DisciplineDocCode = "LAY",
            Code = "LAY",
            Description = "جانمایی"
        },
        new()
        {
            DisciplineCode = "ARC",
            DisciplineDocCode = "DTL",
            Code = "DTL",
            Description = "جزئیات معماری"
        },
        new()
        {
            DisciplineCode = "ARC",
            DisciplineDocCode = "SCH",
            Code = "SCH",
            Description = "جداول معماری"
        },
        new()
        {
            DisciplineCode = "ARC",
            DisciplineDocCode = "SPC",
            Code = "SPC",
            Description = "مشخصات فنی"
        },

        // Geotechnical
        new()
        {
            DisciplineCode = "GEO",
            DisciplineDocCode = "RPT",
            Code = "RPT",
            Description = "گزارش مطالعات ژئوتکنیک"
        },
        new()
        {
            DisciplineCode = "GEO",
            DisciplineDocCode = "PLN",
            Code = "PLN",
            Description = "پلان مطالعات"
        },
        new()
        {
            DisciplineCode = "GEO",
            DisciplineDocCode = "DRW",
            Code = "DRW",
            Description = "نقشه‌های ژئوتکنیک"
        },
        new()
        {
            DisciplineCode = "GEO",
            DisciplineDocCode = "CAL",
            Code = "CAL",
            Description = "محاسبات ژئوتکنیک"
        },

        // HSE
        new()
        {
            DisciplineCode = "HSE",
            DisciplineDocCode = "PLN",
            Code = "PLN",
            Description = "برنامه و پلان HSE"
        },
        new()
        {
            DisciplineCode = "HSE",
            DisciplineDocCode = "DRW",
            Code = "DRW",
            Description = "نقشه‌های ایمنی"
        },
        new()
        {
            DisciplineCode = "HSE",
            DisciplineDocCode = "PRC",
            Code = "PRC",
            Description = "دستورالعمل"
        },
        new()
        {
            DisciplineCode = "HSE",
            DisciplineDocCode = "RPT",
            Code = "RPT",
            Description = "گزارش HSE"
        },

        // General Engineering
        new()
        {
            DisciplineCode = "GEN",
            DisciplineDocCode = "DB",
            Code = "DB",
            Description = "مبانی طراحی"
        },
        new()
        {
            DisciplineCode = "GEN",
            DisciplineDocCode = "PHY",
            Code = "PHY",
            Description = "فلسفه مهندسی"
        },
        new()
        {
            DisciplineCode = "GEN",
            DisciplineDocCode = "DRW",
            Code = "DRW",
            Description = "نقشه عمومی"
        },
        new()
        {
            DisciplineCode = "GEN",
            DisciplineDocCode = "LST",
            Code = "LST",
            Description = "فهرست مدارک"
        },
        new()
        {
            DisciplineCode = "GEN",
            DisciplineDocCode = "RPT",
            Code = "RPT",
            Description = "گزارش مهندسی"
        },
        new()
        {
            DisciplineCode = "GEN",
            DisciplineDocCode = "SPC",
            Code = "SPC",
            Description = "مشخصات عمومی"
        }
    ];
}


