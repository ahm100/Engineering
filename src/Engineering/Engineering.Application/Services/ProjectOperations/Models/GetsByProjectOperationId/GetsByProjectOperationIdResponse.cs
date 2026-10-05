
namespace Engineering.Application.Services.ProjectOperations.Models.GetsByProjectOperationId;

public record GetsByProjectOperationIdResponse(
    List<GetsByProjectOperationIdModel> Data,
    int RowCount);
