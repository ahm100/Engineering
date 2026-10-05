namespace Engineering.Application.Services.MachineriesGroups.Models.GetMachineriesGroupByName;

public record GetMachineriesGroupByNameRequest(
    string GroupName
     ) : IHttpRequest;
