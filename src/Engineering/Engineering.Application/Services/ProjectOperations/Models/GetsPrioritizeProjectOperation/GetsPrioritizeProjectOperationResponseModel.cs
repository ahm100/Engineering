
namespace Engineering.Application.Services.ProjectOperations.Models.GetsPrioritizeProjectOperation;

public record GetsPrioritizeProjectOperationResponseModel(
    long Id,
    long OperationInfoId,
    string OperationInfoName,
    string OperationInfoCode,
    int? Priority
    );
