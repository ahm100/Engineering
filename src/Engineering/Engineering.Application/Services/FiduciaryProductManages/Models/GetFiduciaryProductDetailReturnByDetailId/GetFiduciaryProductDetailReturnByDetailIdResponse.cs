namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnByDetailId;

public record GetFiduciaryProductDetailReturnByDetailIdResponse
{
    public long FiduciaryProductDetailId { get; set; }
    public long? ProductId { get; set; }
    public string? ProductCode { get; set; } = string.Empty;
    public string? ProductName { get; set; } = string.Empty;
    public string? MeasureUnitName { get; set; } = string.Empty;
    public long? MeasureUnitId { get; set; }
    public DateTime? DeliverDate { get; set; }
    public string? DeliverDateShamsi => TimeCalculator.ConvertToShamsi(DeliverDate);
    public List<GetFiduciaryProductDetailReturnByDetailIdModel>? Returns { get; set; }
}
