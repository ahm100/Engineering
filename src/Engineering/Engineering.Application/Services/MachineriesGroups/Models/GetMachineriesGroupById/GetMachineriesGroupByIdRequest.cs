namespace Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupById;

public record GetMachineriesGroupByIdRequest(
    long Id
     ) : IHttpRequest;
