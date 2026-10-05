namespace Engineering.Application.Services.EmployerContracts.Contracts.GetEContractById;

public record GetEContractByIdRequest(
    long Id
     ) : IHttpRequest;
