
namespace Engineering.Application.Services.EmployerStatusStatements.Models.ChangeESSStatus;

public record ChangeESSStatusToReturnForReviewRequest(
    long Id,
    string? Discription
     ) : IHttpRequest;
