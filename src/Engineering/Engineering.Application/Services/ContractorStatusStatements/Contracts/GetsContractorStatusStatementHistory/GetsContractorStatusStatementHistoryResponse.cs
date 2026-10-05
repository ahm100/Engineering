using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementHistory;

public record GetsContractorStatusStatementHistoryResponse(
    List<GetsContractorStatusStatementHistoryModel> Data,
    int RowCount
    );

public record GetsContractorStatusStatementHistoryModel
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? StartDateShamsi => TimeCalculator.ConvertToShamsi(StartDate);
    public string? EndDateShamsi => TimeCalculator.ConvertToShamsi(EndDate);
    public CSSStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public string? Description { get; set; } = string.Empty;
    public long CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
}