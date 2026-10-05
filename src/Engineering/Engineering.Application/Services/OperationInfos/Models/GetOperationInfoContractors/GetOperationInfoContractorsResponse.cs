namespace Engineering.Application.Services.OperationInfos.Models.GetOperationInfoContractors;

public record GetOperationInfoContractorsResponse(
    List<GetOperationInfoContractorsResponseModel> Data,
    int RowCount);

public record GetOperationInfoContractorsResponseModel
{
    public long? Id { get; set; }
    public long? UserId { get; set; }
    public string? FullName { get; set; } = string.Empty;
    public string? NickName { get; set; } = string.Empty;
}
