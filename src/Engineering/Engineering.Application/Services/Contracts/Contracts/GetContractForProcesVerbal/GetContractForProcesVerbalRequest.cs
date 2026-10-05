namespace Engineering.Application.Services.Contracts.Contracts.GetContractForProcesVerbal;

public record GetContractForProcesVerbalRequest(
    long? ProjectId) : IHttpRequest;
