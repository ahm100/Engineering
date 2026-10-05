namespace Engineering.Application.Services.RequestContractors.Models.CreateRequestContractor;

public record CreateRequestContractorRequest(
    List<ProjectOperationDetailContractorRequestModel> ProjectOperationDetails
    ) : IHttpRequest;

public record ProjectOperationDetailContractorRequestModel(
    long ProjectOperationDetailId,
    List<ServiceInfoContractorRequestModel> ServiceInfos
    );

public record ServiceInfoContractorRequestModel(
    long ServiceInfoId,
    decimal Volume,
    string? Description
    );