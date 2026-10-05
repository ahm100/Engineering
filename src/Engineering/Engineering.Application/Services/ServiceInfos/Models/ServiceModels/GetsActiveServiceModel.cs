using Engineering.Application.Services.OperationInfos.Models.OperationInfoModels;
using Engineering.Domain.Entities.ServiceInfos.Enums;

namespace Engineering.Application.Services.ServiceInfos.Models.ServiceModels;

public class GetsActiveServiceInfoModel
{
    public long Id { get; set; }
    public string ServiceInfoName { get; set; } = string.Empty;
    public string ServiceInfoCode { get; set; } = string.Empty;
    public string? ServiceInfoEnName { get; set; }
    public string? DescriptionFa { get; set; }
    public string? DescriptionEn { get; set; }
    public long MeasurementId { get; set; }
    public string? MeasurementName { get; set; }
    public OperationInfoMeasurementModel? MeasurementData { get; set; }
    public string? TimeSpant { get; set; }
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; }
    public ServiceInfoType Type { get; set; }
    public string? TypeDescription => Type.GetEnumDescription();
}