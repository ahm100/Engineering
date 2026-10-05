
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetsDeductionByProjectOperationDetailId;

public record GetsDeductionByProjectOperationDetailIdResponse(
    List<GetsDeductionByProjectOperationDetailIdModel> Data,
    int RowCount
    );
