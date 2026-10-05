namespace Engineering.Application.Services.EmployerContracts.Contracts.DeleteEContract;

public record DeleteEContractRequest(
    long Id
     ) : IHttpRequest;
