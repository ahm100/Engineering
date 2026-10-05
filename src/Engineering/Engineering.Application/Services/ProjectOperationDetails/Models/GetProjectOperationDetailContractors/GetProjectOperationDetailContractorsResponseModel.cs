namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectOperationDetailContractors;

public record GetProjectOperationDetailContractorsResponseModel
{
    public long? Id { get; set; }
    public long? UserId { get; set; }
    public string? FullName { get; set; }
    public string? Nickname { get; set; }
};
