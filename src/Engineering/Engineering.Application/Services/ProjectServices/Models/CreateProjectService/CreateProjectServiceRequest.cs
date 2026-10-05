namespace Engineering.Application.Services.ProjectServices.Models.CreateProjectService;

public record CreateProjectServiceRequest : IHttpRequest
{
    public long ProjectId { get; set; }
    public long ContractorId { get; set; }
    public long ServiceInfoId { get; set; }
    public decimal Volume { get; set; }
    public decimal DoneVolume { get; set; } = 0;
    public bool IsActive { get; set; }
    public List<CreateProjectServiceModel>? OperationInfoServices { get; set; }
};

public record CreateProjectServiceModel(
    long OperationInfoServiceId
    );
