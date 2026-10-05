namespace Engineering.Application.Services.Machineries.Models.GetsMachineryForRequestMachinery;

public record GetsMachineryForRequestMachineryRequest(
    long? ProjectId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    long? MachineriesGroupId,
    string? FilterData,
    bool? IsActive,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
