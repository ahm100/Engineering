using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistories;

public record GetDailyProjectOperationHistoriesDetailModel
{
    public long DailyProjectOperationId { get; set; }
    public string? Contractors { get; set; } = string.Empty;
    public string? Nicknames { get; set; } = string.Empty;
    public string? Services { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectOperationDetailStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
    public DailyProjectOperationType Type { get; set; }
    public string? TypeDescription => Type.GetEnumDescription();
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public decimal FinalAmount { get; set; }
    public long? UnitOfMeasurementId { get; set; }
    public string? UnitOfMeasurement { get; set; } = string.Empty;
    public List<GetDailyProjectOperationHistoriesDetailDocumentModel> DocumentUrls { get; set; } = new();
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public long CreatorId { get; set; }
    public string? CreatorName { get; set; } = string.Empty;
    public string? CreatorNickname { get; set; } = string.Empty;
    public string? Created { get; set; }
    public string? Description { get; set; } = string.Empty;
    public long ProjectOperationDetailId { get; set; }
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? ProjectOperationDetailDescription { get; set; } = string.Empty;
    public decimal? Volume { get; set; }
}

public record GetTotalDailyProjectOperationHistoriesDetailModel
{
    public decimal? TotalLengths { get; set; } = 0;
    public decimal? TotalWidths { get; set; } = 0;
    public decimal? TotalHeights { get; set; } = 0;
    public decimal? TotalWeights { get; set; } = 0;
    public decimal? TotalNumbers { get; set; } = 0;
    public decimal? TotalAmounts { get; set; } = 0;
    public decimal? ProjectOperationDetailFinalAmount { get; set; } = 0;
    public decimal? TotalDeductionFinalAmount { get; set; } = 0;
    public decimal? TotalProjectOperationDetailFinalAmount { get; set; } = 0;
    public decimal? ProjectOperationWorkload { get; set; } = 0;
}
