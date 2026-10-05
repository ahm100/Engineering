namespace Engineering.Application.Services.ProjectOperations.Models.GetCriticalPO;

public record GetCriticalPOResponse(
    List<GetCriticalPOModel> Data,
    int RowCount);

public record GetCriticalPOModel()
{
    public long Id { get; set; }
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public string? OperationLatinName { get; set; }
}