namespace Engineering.Application.Services.Contracts.Contracts.GetContractById;

public record GetContractByIdRequest(
    long Id) : IHttpRequest;