using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;
using Engineering.Domain.Entities.ServiceInfos.Enums;

namespace Engineering.Application.Services.ServiceInfos.Models.GetServiceById;

public record GetServiceInfoByIdResponse
{
    public long Id { get; set; }
    public string ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoEnName { get; set; }
    public string ServiceInfoCode { get; set; } = string.Empty;
    public string? DescriptionFa { get; set; }
    public string? DescriptionEn { get; set; }
    public OperationInfoMeasurementModel? MeasurementData { get; set; }
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public ServiceInfoType Type { get; set; }
    public string? TypeDescription => Type.GetEnumDescription();
    public List<string>? DocumentUrls { get; set; }
}