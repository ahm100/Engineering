namespace Engineering.Application.Services.ProjectServices.Models.DeleteProjectService;

public record DeleteProjectServiceRequest : IHttpRequest
{
    public long Id { get; set; }
};
