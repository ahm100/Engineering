using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Commands.UpdateProjectOperationDetailInspection;

public record UpdateProjectOperationDetailInspectionCommand(long Id,
                                            decimal Length,
                                            decimal Width,
                                            decimal Height,
                                            decimal Weight,
                                            decimal Number,
                                            DateTime? InspectionDate,
                                            string? Description,
                                            Project Project,
                                            OperationInfo? OperationInfo,
                                            OperationLocation? OperationLocation,
                                            ProjectOperation? ProjectOperation,
                                            ProjectOperationDetail? ProjectOperationDetail,
                                            List<string>? Documents) : ICommand<ProjectOperationDetailInspection>;
