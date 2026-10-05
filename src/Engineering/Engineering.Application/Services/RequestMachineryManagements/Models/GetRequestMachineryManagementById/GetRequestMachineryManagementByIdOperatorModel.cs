namespace Engineering.Application.Services.RequestMachineryManagements.Models.GetRequestMachineryManagementById;

public record GetRequestMachineryManagementByIdOperatorModel
{
    public long? AppointmentId { get; set; }
    public long? AppointmentUserId { get; set; }
    public string? AppointmentFullName { get; set; }
}
