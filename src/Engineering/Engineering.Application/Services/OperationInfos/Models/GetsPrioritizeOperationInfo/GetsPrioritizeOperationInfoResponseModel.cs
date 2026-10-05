
namespace Engineering.Application.Services.OperationInfos.Models.GetsPrioritizeOperationInfo;

public class GetsPrioritizeOperationInfoResponseModel
{
    public long Id { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public string? OperationLatinName { get; set; }
    public int? Priority { get; set; }
    public bool IsActive { get; set; }

}
