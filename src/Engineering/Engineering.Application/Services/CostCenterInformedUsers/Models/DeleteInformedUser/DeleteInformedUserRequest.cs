namespace Engineering.Application.Services.CostCenterInformedUsers.Models.DeleteInformedUser;

public record DeleteInformedUserRequest(
    long EmployeeId,
    long CostCenterId
     ) : IHttpRequest;
