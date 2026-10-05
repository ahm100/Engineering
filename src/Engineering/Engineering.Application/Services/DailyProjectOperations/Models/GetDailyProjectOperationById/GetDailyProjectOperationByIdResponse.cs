using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationById;

public record GetDailyProjectOperationByIdResponse
{
    public long Id { get; set; }
    public ProjectOperationDetailStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DailyProjectOperationType Type { get; set; }
    public string? TypeDescription => Type.GetEnumDescription();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public decimal FinalAmount => Length * Width * Height * Weight * Number;
    public long ProjectOperationDetailId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public long ProjectOperationId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public decimal Volume { get; set; }
    public List<GetDailyProjectOperationByIdExpertModel>? Experts { get; set; } = new();
    public List<GetDailyProjectOperationByIdSeviceModel>? Services { get; set; } = new();
    public List<GetDailyProjectOperationByIdDocumentModel>? Documents { get; set; } = new();
    public List<GetDailyProjectOperationByIdMachineryModel>? Machineris { get; set; } = new();
    public List<GetDailyProjectOperationProduct>? Products { get; set; } = new();
    public List<GetDailyProjectOperationRequestRewardModel>? RequestRewards { get; set; } = new();
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public long CreatorId { get; set; }
    public string? CreatorName { get; set; } = string.Empty;
    public string? CreatorNickname { get; set; } = string.Empty;
    public string? Created { get; set; }
}
