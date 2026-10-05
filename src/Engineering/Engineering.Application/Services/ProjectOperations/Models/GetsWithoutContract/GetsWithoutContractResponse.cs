
namespace Engineering.Application.Services.ProjectOperations.Models.GetsWithoutContract;

public record GetsWithoutContractResponse(
    List<GetsWithoutContractModel> Data,
    int RowCount
    );
