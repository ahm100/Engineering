namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetInfoByContractorServiceId;


public record GetInfoByContractorServiceIdResponse
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    public string ServiceName { get; set; }
    public long ProjectOperationId { get; set; }
    public string ProjectOperationName { get; set; }
    public long ProjectOperationDetailId { get; set; }
    public string ProjectOperationDetailName { get; set; }
}
