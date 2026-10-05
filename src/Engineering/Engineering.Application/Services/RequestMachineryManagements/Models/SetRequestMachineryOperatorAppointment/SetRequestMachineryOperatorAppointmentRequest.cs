namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryOperatorAppointment;

public record SetRequestMachineryOperatorAppointmentRequest(long RequestMachineryId,
                                                  string? Description) : IHttpRequest;
