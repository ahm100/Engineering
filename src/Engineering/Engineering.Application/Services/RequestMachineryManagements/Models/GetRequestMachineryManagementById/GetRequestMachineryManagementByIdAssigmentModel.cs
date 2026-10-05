namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryManagementById;

public record GetRequestMachineryManagementByIdAssigmentModel
{
    public long Id { get; set; }
    public string MachineryIdentifier { get; set; } = string.Empty;
}