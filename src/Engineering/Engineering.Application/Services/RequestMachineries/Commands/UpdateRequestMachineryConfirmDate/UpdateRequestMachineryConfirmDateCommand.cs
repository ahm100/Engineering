using Engineering.Domain.Entities.ContractorMachineries;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Commands.UpdateRequestMachineryConfirmDate;

public record UpdateRequestMachineryConfirmDateCommand(RequestMachinery RequestMachinery,
                                                       decimal? ConfirmTimeRequired,
                                                       string? ConfirmedDescription,
                                                       DateTime ConfirmFromDate,
                                                       TimeSpan? ConfirmFromTime,
                                                       DateTime ConfirmToDate,
                                                       TimeSpan? ConfirmToTime,
                                                       long? ContractorId,
                                                       ContractorMachinery? ContractorMachinery
                                                       ) : ICommand<RequestMachinery>;
