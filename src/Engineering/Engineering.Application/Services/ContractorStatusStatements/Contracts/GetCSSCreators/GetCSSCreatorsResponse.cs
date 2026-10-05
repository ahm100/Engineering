namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCSSCreators;

public record GetCSSCreatorsResponse(
    List<GetCSSCreatorsModel> Data,
    int RowCount
);

public class GetCSSCreatorsModel
{
    public long CreatorId { get; set; }
    public string? CreatorName { get; set; }
}