using Engineering.Domain.Entities.Contracts.Enums;
using System.Globalization;

namespace Engineering.Application.Services.Contracts.Contracts.GetContractChangeById;

public class GetContractChangeByIdResponse
{
    public long Id { get; set; }
    public long ContractId { get; set; }
    public ContractChangeMode Mode { get; set; }
    public ContractChangeType Type { get; set; }
    public string TypeTitle => Type.GetEnumDescription();
    public string Number { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Subject { get; set; } = string.Empty;
    public decimal FinancialChangeAmount { get; set; }
    public int? DurationChange { get; set; }
    public decimal NewContractAmount { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public DateTime BaselineEndDate { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public ContractDurationUnit DurationUnit { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public int CumulativeDurationChange { get; set; }

    public DateTime NewEndDate => DurationUnit == ContractDurationUnit.Month
        ? new PersianCalendar().AddMonths(BaselineEndDate, CumulativeDurationChange)
        : BaselineEndDate.AddDays(CumulativeDurationChange);

    public List<GetContractChangeDocumentModel> Documents { get; set; } = [];
    public List<GetContractChangeItemModel> Items { get; set; } = [];
}
