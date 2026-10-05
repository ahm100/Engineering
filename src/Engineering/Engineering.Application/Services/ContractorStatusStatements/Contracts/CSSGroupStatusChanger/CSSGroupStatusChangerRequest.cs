using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Application.Services.ContractorStatusStatements.Models.CSSGroupStatusChanger;

public class CSSGroupStatusChangerRequest : IHttpRequest
{
    public CSSStatus Status { get; set; }
    public required List<CSSGroupStatusChangerModel> Items { get; set; }
};

public record CSSGroupStatusChangerModel(
    long Id,
    string? Description,
    decimal? ConfirmedAmount
    );
