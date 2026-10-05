using Engineering.Application.Services.GoodsManagerAssignments.Contracts.CreateGoodsManagerAssignment;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.DeleteGoodsManagerAssignment;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetFilteredGoodsManagerAssignments;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetGoodsManagerAssignmentHistory;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetGoodsManagerAssignmentsById;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.UpdateGoodsManagerAssignment;

namespace Engineering.Application.Services.GoodsManagerAssignments;

public interface IGoodsManagerAssignmentLogic
{
    Task<Result<CreateGoodsManagerAssignmentResponse?>> CreateGoodsManagerAssignment(
        CreateGoodsManagerAssignmentRequest request, CT ct);

    Task<Result<DeleteGoodsManagerAssignmentResponse?>> DeleteGoodsManagerAssignment(
        DeleteGoodsManagerAssignmentRequest request, CT ct);

    Task<Result<GetFilteredGoodsManagerAssignmentsResponse?>> GetFilteredGoodsManagerAssignments(
        GetFilteredGoodsManagerAssignmentsRequest request, CT ct);

    Task<Result<UpdateGoodsManagerAssignmentResponse?>> UpdateGoodsManagerAssignment(
    UpdateGoodsManagerAssignmentRequest request, CT ct);

    Task<Result<GetGoodsManagerAssignmentHistoryResponse?>> GetGoodsManagerAssignmentHistory(
    GetGoodsManagerAssignmentHistoryRequest request, CT ct);

    Task<Result<GetGoodsManagerAssignmentsByIdResponse?>> GetGoodsManagerAssignmentsById(
    GetGoodsManagerAssignmentsByIdRequest request, CT ct);
}