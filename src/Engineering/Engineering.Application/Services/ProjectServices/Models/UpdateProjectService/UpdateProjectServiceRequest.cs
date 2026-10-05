namespace Engineering.Application.Services.ProjectServices.Models.UpdateProjectService;

public record UpdateProjectServiceRequest : IHttpRequest
{
    public long Id { get; set; }
    public long ContractorId { get; set; }
    public long ServiceInfoId { get; set; }
    public decimal Volume { get; set; }
    public decimal DoneVolume { get; set; } = 0;
    public bool IsActive { get; set; }
    public List<UpdateProjectServiceModel>? NewOperationInfoServices { get; set; }
    public List<DeleteProjectServiceModel>? DeleteOperationInfoServices { get; set; }
};

public record UpdateProjectServiceModel(
    long OperationInfoServiceId
    );

public record DeleteProjectServiceModel(
    long ProjectServiceDetailId
    );
