namespace Engineering.Application.Services.EmployerContracts.Contracts.DeleteEContractHeader;

public record DeleteEContractHeaderRequest(
    long Id
     ) : IHttpRequest;
