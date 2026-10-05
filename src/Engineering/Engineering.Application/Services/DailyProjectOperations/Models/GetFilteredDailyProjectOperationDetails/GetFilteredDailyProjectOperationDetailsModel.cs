using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredDailyProjectOperationDetails;

public record GetFilteredDailyProjectOperationDetailsModel
{
    public long ProjectOperationDetailId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string PrivateName { get; set; } = string.Empty;
    public string PrivateCode { get; set; } = string.Empty;
    public string PublicName { get; set; } = string.Empty;
    public string PublicCode { get; set; } = string.Empty;
    public decimal FinalAmount { get; set; }
    public decimal DoneAmount { get; set; }
    public decimal RemainingAmount => FinalAmount - DoneAmount;
    public ProjectOperationDetailStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public decimal Length { get; set; }
    public bool LengthChangeable { get; set; }
    public decimal Width { get; set; }
    public bool WidthChangeable { get; set; }
    public decimal Height { get; set; }
    public bool HeightChangeable { get; set; }
    public decimal Weight { get; set; }
    public bool WeightChangeable { get; set; }
    public decimal Number { get; set; }
    public bool NumberChangeable { get; set; }
    public string? Description { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public string? Contractors { get; set; } = string.Empty;
    public string? ContractorNickNames { get; set; } = string.Empty;
}
