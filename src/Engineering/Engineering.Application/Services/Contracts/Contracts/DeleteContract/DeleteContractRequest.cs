namespace Engineering.Application.Services.Contracts.Contracts.DeleteContract;

public record DeleteContractRequest(
    long Id) : IHttpRequest;