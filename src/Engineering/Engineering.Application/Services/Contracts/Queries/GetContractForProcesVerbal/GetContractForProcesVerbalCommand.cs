using Engineering.Application.Services.Contracts.Contracts.GetContractForProcesVerbal;

namespace Engineering.Application.Services.Contracts.Queries.GetContractsByProjectIdForProcesVerbal;

public record GetContractForProcesVerbalCommand(
    long? ProjectId) : IQuery<List<GetContractForProcesVerbalResponse>?>;