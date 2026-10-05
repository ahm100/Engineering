
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader.Exporter;

public record GetsContractorContractExcelExporterResponse(
    FileContentResult File
    );

public record GetsContractorContractExcelExporterModel
{
    public long Id { get; set; }
    public string? StartDate { get; set; } = string.Empty;
    public string? EndDate { get; set; } = string.Empty;
    public ContractorContractStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public string? ContractorContractType { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string Contractor { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public decimal? Price { get; set; }
    public long? CurrencyId { get; set; }
    public string? Currency { get; set; }
    public int? WorkDonePercent { get; set; }
    public int? WorkDeliveryPercent { get; set; }
    public int? WorkCompletionPercent { get; set; }
    public string? Description { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
};
