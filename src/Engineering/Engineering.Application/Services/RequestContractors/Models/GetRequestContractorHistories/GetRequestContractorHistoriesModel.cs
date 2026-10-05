using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractors.Models.GetRequestContractorHistories;

public record GetRequestContractorHistoriesModel
{
    public long? Id { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public long? ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public decimal? Volume { get; set; }
    public RequestContractorStatus? Status { get; set; }
    public string? StatusDescription => Status?.GetEnumDescription();
    public string? Description { get; set; } = string.Empty;
    public string? DescriptionStatus { get; set; } = string.Empty;
    public DateTime? Created { get; set; }
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
}
