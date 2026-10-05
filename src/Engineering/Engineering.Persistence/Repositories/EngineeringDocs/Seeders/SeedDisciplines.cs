namespace Engineering.Persistence.Repositories.EngineeringDocs.Seeders;


public sealed class SeedDisciplineModel
{
    public string Code { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string EnglishName { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}

public static class SeedDisciplines
{
    public static readonly IReadOnlyCollection<SeedDisciplineModel> All =
    [
        new()
        {
            Code = "PRO",
            Name = "فرآیند",
            EnglishName = "Process",
            Description = "طراحی و مهندسی فرآیند"
        },
        new()
        {
            Code = "MEC",
            Name = "مکانیک",
            EnglishName = "Mechanical",
            Description = "تجهیزات و سیستم‌های مکانیکی"
        },
        new()
        {
            Code = "PIP",
            Name = "پایپینگ",
            EnglishName = "Piping",
            Description = "طراحی خطوط و سیستم‌های لوله‌کشی"
        },
        new()
        {
            Code = "CIV",
            Name = "عمران",
            EnglishName = "Civil",
            Description = "طراحی‌های عمرانی و زیرساخت"
        },
        new()
        {
            Code = "STR",
            Name = "سازه",
            EnglishName = "Structural",
            Description = "طراحی سازه‌های فلزی و بتنی"
        },
        new()
        {
            Code = "ELE",
            Name = "برق",
            EnglishName = "Electrical",
            Description = "سیستم‌های برق و توزیع"
        },
        new()
        {
            Code = "INS",
            Name = "ابزار دقیق",
            EnglishName = "Instrumentation",
            Description = "ابزار دقیق و کنترل"
        },
        new()
        {
            Code = "ARC",
            Name = "معماری",
            EnglishName = "Architecture",
            Description = "طراحی معماری"
        },
        new()
        {
            Code = "GEO",
            Name = "ژئوتکنیک",
            EnglishName = "Geotechnical",
            Description = "مطالعات و طراحی ژئوتکنیک"
        },
        new()
        {
            Code = "HSE",
            Name = "ایمنی",
            EnglishName = "HSE",
            Description = "مدارک ایمنی و HSE"
        },
        new()
        {
            Code = "GEN",
            Name = "عمومی",
            EnglishName = "General Engineering",
            Description = "مدارک عمومی و مشترک مهندسی"
        }
    ];
}