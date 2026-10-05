
namespace Engineering.Application.Services.ProjectOperationDetails.Models.UpdatesProjectOperationDetailDate;

public record UpdatesProjectOperationDetailDateRequest(
    List<UpdatesProjectOperationDetailDateRequestModel> RequestModel
     ) : IHttpRequest;

public record UpdatesProjectOperationDetailDateRequestModel(
    long Id,
    DateTime? StartDate,
    DateTime? EndDate
     );
