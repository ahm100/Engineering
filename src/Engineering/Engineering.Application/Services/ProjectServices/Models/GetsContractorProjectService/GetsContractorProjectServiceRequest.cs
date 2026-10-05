namespace Engineering.Application.Services.ProjectServices.Models.GetsContractorProjectService;

public record GetsContractorProjectServiceRequest(
        long? ProjectId,
        long? ServiceInfoId,
        string? FilterData,
        int PageIndex,
        int PageSize
     ) : IHttpRequest;
