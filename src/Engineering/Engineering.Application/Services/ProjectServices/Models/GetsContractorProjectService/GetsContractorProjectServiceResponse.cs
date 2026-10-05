
namespace Engineering.Application.Services.ProjectServices.Models.GetsContractorProjectService;

public record GetsContractorProjectServiceResponse(
    List<GetsContractorProjectServiceModel> Data,
    int RowCount
    );

public record GetsContractorProjectServiceModel
{
    public long ContractorId { get; set; }
    public string? ContractorName { get; set; } = string.Empty;
    public string? ContractorNickName { get; set; } = string.Empty;
}
