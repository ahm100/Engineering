
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.ProjectOperationDetailDeductionGroupDelete;

public record ProjectOperationDetailDeductionGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
