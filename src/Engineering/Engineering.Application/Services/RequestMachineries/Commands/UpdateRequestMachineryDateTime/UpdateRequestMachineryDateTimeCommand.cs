using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryDateTime;

public record UpdateRequestMachineryDateTimeCommand(long RequestMachineryId,
                                                    decimal? TimeRequired,
                                                    DateTime FromDate,
                                                    DateTime ToDate,
                                                    TimeSpan FromTime,
                                                    TimeSpan ToTime) : ICommand<RequestMachinery>;
