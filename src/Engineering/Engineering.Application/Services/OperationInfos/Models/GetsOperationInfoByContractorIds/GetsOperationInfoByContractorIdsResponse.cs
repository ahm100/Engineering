
namespace Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoByContractorIds;

public record GetsOperationInfoByContractorIdsResponse(
    List<GetsOperationInfoByContractorIdsResponseModel> Data,
    int RowCount);

public record GetsOperationInfoByContractorIdsResponseModel
{
    public long Id { get; set; }
    public long? UserId { get; set; }
    public string? FullName { get; set; }
    public string? NickName { get; set; }
    public List<OperationInfoResponseModel>? operationInfos { get; set; }
};

public record OperationInfoResponseModel
{
    public long Id { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public string? OperationLatinName { get; set; } = string.Empty;
    public long? MeasurementId { get; set; }
    public string? MeasurementName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsPriceList { get; set; }
}