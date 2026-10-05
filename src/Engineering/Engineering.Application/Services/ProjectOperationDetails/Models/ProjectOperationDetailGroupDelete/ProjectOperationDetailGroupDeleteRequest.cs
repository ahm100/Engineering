
namespace Engineering.Application.Services.ProjectOperationDetails.Models.ProjectOperationDetailGroupDelete;

public record ProjectOperationDetailGroupDeleteRequest(
    List<long> Ids
     ) : IHttpRequest;
