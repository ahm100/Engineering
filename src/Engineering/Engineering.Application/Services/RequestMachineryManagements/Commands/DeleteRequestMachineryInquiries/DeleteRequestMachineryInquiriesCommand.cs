using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryManagements.Commands.DeleteRequestMachineryInquiries;

public record DeleteRequestMachineryInquiriesCommand(RequestMachinery RequestMachinery) : ICommand<bool?>;
