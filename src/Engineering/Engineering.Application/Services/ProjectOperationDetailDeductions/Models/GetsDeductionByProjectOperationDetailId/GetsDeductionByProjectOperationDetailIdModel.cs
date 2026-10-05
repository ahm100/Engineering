
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetsDeductionByProjectOperationDetailId;

public record GetsDeductionByProjectOperationDetailIdModel
{
    public long Id { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public string? PrivateName { get; set; }
    public string? PrivateCode { get; set; }
    public string? PublicName { get; set; }
    public string? PublicCode { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public decimal Number { get; set; }
    public decimal FinalAmount { get; set; }
}
