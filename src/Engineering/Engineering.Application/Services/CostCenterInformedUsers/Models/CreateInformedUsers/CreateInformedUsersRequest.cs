namespace Engineering.Application.Services.CostCenterInformedUsers.Models.CreateInformedUsers;

public record CreateInformedUsersRequest(
    long CostCenterId,
    List<long?>? EmployeeIds
     ) : IHttpRequest;
