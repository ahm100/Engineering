namespace Engineering.Application.Services.Machineries.Models.GetsByMachineriesGroupId;

public record GetsByMachineriesGroupIdRequest(
    long MachineriesGroupId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
