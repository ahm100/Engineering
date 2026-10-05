namespace Engineering.Application.Services.RequestMachineryManagements.Models.AssignMachineryForRequestMachinery;

public record AssignMachineryForRequestMachineryRequest(
    long RequestMachineryId,
    long ContractorMachineryId,
    string? ConfirmedTimeRequired,
    DateTime ConfirmFromDate,
    DateTime ConfirmToDate,
    TimeSpan? ConfirmFromTime,
    TimeSpan? ConfirmToTime,
    string? Description
    ) : IHttpRequest;
