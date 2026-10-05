using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachinery;

public record UpdateRequestMachineryCommand(long RequestMachineryId,
                                            decimal TimeRequired,
                                            RequestMachineryUnit Unit,
                                            int RequestCount,
                                            DateTime FromDate,
                                            DateTime ToDate,
                                            string? Description,
                                            string MachineryIdentifier,
                                            Machinery Machinery,
                                            Project Project,
                                            long? CompanyId) : ICommand<RequestMachinery>;
