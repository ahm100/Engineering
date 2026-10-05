
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.AppointmentContractor;

public record AppointmentContractorModel
{
    public long Id { get; set; }
    public long? ProjectServiceDetailId { get; set; }
    public long? ContractorId { get; set; } = 0;
    public string? ContractorName { get; set; }
    public long OperationLocationId { get; set; }
    public string PrivateName { get; set; } = string.Empty;
    public string PrivateCode { get; set; } = string.Empty;
    public string PublicName { get; set; } = string.Empty;
    public string PublicCode { get; set; } = string.Empty;
    public long ProjectOperationDetailId { get; set; }
    public long ProjectOperationId { get; set; }
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public long? ServiceInfoId { get; set; } = 0;
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
}
