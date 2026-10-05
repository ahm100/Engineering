using Engineering.Application.Services.ProjectOperations.Models.Models;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsByEmployerContract;

public record GetsByEmployerContractResponse(
    List<GetsEmployerContractModel> Data,
    int RowCount
    );
