namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryInquiryAppointment;

public record SetRequestMachineryInquiryAppointmentRequest(long RequestMachineryId,
                                                           long RequestMachineryInquiryId,
                                                           string? Description) : IHttpRequest;
