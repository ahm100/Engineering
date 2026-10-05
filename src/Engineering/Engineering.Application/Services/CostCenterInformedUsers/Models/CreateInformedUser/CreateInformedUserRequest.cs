namespace Engineering.Application.Services.CostCenterInformedUsers.Models.CreateInformedUser;

public record CreateInformedUserRequest(
    long EmployeeId,
    long CostCenterId
     ) : IHttpRequest;
