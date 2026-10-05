
using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice;

public record GetSuggestedServicePriceResponse
{
    public List<GetSuggestedServicePriceModel> Data { get; set; }
    public PricingModel? OtherData { get; set; }
    public int RowCount { get; set; }

};

public record GetSuggestedServicePriceModel
{
    public long Id { get; set; }
    public long ContractorContractHeaderId { get; set; }
    public long ContractorId { get; set; }
    public string? ContractorName { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long ContractorContractId { get; set; }
    public string? ContractorContractType { get; set; } = string.Empty;
    public long? ProjectId { get; set; }
    public string? ProjectName { get; set; } = string.Empty;
    public long ContractorContractDetailId { get; set; }
    public DateTime? StartDate { get; set; }
    public string? StartDateShamsi => StartDate.ToShamsi();
    public DateTime? EndDate { get; set; }
    public string? EndDateShamsi => EndDate.ToShamsi();
    public decimal Price { get; set; }
    public decimal? ManagerPrice { get; set; }
    public long CurrencyId { get; set; }
    public string? Currency { get; set; } = string.Empty;
    public List<string>? Names { get; set; }
    public string? ServiceName => Names.JoinList() + $"({UnitOfMeasurement})";

    public bool IsActive { get; set; }
    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public string? CreatedShamsi => Created.ToShamsi();
    public long? UpdaterId { get; set; }
    public string? Updater { get; set; } = string.Empty;
    public DateTime? Updated { get; set; }
    public string? UpdatedShamsi => Updated.ToShamsi();
    public ContractorContractStatus Status { get; set; }
    public long? UnitOfMeasurementId { get; set; }
    public string? UnitOfMeasurement { get; set; } = string.Empty;
    public string StatusTitle => Status.GetEnumDescription();
};

public record PricingModel
{
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
    public decimal AveragePrice { get; set; }
};