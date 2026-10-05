namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHeaderById;

public record GetEContractHeaderByIdRequest(
    long Id
     ) : IHttpRequest;
