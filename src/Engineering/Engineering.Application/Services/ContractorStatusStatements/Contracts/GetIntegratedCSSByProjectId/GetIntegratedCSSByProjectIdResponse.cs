

namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSSByProjectId;

public record GetIntegratedCSSByProjectIdResponse(
    List<GetIntegratedCSSByProjectIdModel> Data,
    GetIntegratedCSSPriceModel OtherData,
    int RowCount
    );

public record GetIntegratedCSSByProjectIdModel
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public long? CostCenterId { get; set; }
    public Guid? CostCenterReferenceCode { get; set; }
    public string? CostCenterName { get; set; }
    public long? ProjectId { get; set; }
    public string? Project { get; set; } = string.Empty;
    public string? ProjectCode { get; set; } = string.Empty;
    public Guid? ProjectReferenceCode { get; set; }
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; }
    public Guid? ContractorReferenceCode { get; set; }
    public decimal? ManagementConfirmedAmount { get; set; }
    public string? ManagementDescription { get; set; }
    public string? PrimaryManagerDescription { get; set; }
    public decimal? PrimaryManagerConfirmedAmount { get; set; }
    public string? FinalManagerDescription { get; set; }
    public decimal? FinalManagerConfirmedAmount { get; set; }
    public decimal? Paymented { get; set; }
    public List<string>? Urls { get; set; }
    public bool HaveDocuments => Urls is not null && Urls.Any() ? true : false;
    public bool? PrimaryManagerConfirmed { get; set; }
    public bool? FinalManagerConfirmed { get; set; }
}

public record GetIntegratedCSSPriceModel
{
    public decimal? ManagementConfirmedAmount { get; set; } = 0;
    public decimal? PrimaryManagerConfirmedAmount { get; set; } = 0;
    public decimal? FinalManagerConfirmedAmount { get; set; } = 0;
}