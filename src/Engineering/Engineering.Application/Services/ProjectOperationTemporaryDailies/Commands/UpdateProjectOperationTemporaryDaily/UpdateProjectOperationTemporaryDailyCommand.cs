using Engineering.Domain.Entities.CostCenters;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.UpdateProjectOperationTemporaryDaily;

public record UpdateProjectOperationTemporaryDailyCommand(long Id,
                                            DateTime StartDate,
                                            DateTime EndDate,
                                            TemporaryDailyStatus? Status,
                                            string? Description,
                                            CostCenter CostCenter,
                                            Project Project,
                                            ProjectOperation? ProjectOperation,
                                            List<string>? Urls) : ICommand<ProjectOperationTemporaryDaily>;
