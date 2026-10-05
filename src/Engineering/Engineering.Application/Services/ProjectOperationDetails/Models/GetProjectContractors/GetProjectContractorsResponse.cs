namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectContractors;

public record GetProjectContractorsResponse(
    List<GetProjectContractorsModel> Data,
    int RowCount);

public class GetProjectContractorsModel
{
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; }
}