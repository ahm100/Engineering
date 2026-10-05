using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineryBills.Commands.DeleteRequestMachineryBill;

public record DeleteRequestMachineryBillCommand(long RequestMachineryBillId) : ICommand<RequestMachineryBill>;
