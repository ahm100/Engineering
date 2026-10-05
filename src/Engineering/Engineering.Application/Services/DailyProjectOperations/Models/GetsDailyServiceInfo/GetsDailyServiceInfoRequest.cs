namespace Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyServiceInfo;

public class GetsDailyServiceInfoRequest : IHttpRequest
{
    public List<long>? CostCenterIds { get; set; }
    public List<long>? ProjectIds { get; set; }
    public List<long>? ProjectOperationIds { get; set; }
    public List<long>? ProjectOperationDetailIds { get; set; }
    public List<long>? ContractorIds { get; set; }
    public string? ServiceInfoName { get; set; }
    public string? FilterData { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
