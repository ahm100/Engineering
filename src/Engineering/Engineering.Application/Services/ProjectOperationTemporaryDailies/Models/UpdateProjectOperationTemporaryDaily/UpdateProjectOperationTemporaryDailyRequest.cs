using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Models.UpdateProjectOperationTemporaryDaily;

public record UpdateProjectOperationTemporaryDailyRequest(long Id,
                                            long CostCenterId,
                                            long projectId,
                                            long? ProjectOperationId,
                                            DateTime StartDate,
                                            DateTime EndDate,
                                            TemporaryDailyStatus? Status,
                                            string? Description,
                                            List<string>? DocumentUrls) : IHttpRequest;
