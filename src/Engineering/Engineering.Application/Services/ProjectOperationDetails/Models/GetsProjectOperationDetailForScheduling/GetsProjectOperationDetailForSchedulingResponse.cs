
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailForScheduling;

public record GetsProjectOperationDetailForSchedulingResponse(
    List<GetsProjectOperationDetailForSchedulingResponseModel?> Data,
    int RowCount
    );
