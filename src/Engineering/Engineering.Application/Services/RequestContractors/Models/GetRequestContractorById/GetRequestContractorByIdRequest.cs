namespace Engineering.Application.Services.RequestContractors.Models.GetRequestContractorById;

public record GetRequestContractorByIdRequest(long RequestContractorId) : IHttpRequest;
