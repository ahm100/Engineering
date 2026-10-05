
using Engineering.Application.Services.ContractorMachineries.Models.ContractorMachineryModel;
using Engineering.Domain.Entities.ContractorMachineries.Enums;

namespace Engineering.Application.Services.ContractorMachineries.Models.GetsContractorMachineryExcelExporter;

public record GetsContractorMachineryExcelExporterModel
{
    public long Id { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; } = string.Empty;
    public string? ContractorNickName { get; set; } = string.Empty;
    public long? MachineryGroupId { get; set; }
    public string? MachineryGroupName { get; set; } = string.Empty;
    public string? MachineryGroupCode { get; set; } = string.Empty;
    public long? MachineryId { get; set; }
    public string? MachineryName { get; set; } = string.Empty;
    public string? MachineryCode { get; set; } = string.Empty;
    public ContractorMachineryUnit? Unit { get; set; }
    public string? UnitDesctiption => Unit?.GetEnumDescription();
    public decimal? MachineryPrice { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
    public string? MachineryIdentifier { get; set; } = string.Empty;
    public string? NumberPlates { get; set; } = string.Empty;
    public ContractorMachineryNumberPlatesModel? NumberPlatesModel { get; set; } = new();
    public bool? IsActive { get; set; }
    public string? Description { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public long? CompanyId { get; set; }
    public string? CompanyName { get; set; } = string.Empty;
};