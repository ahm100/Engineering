using Gita.Backend.Shared.Domain.Enums.PaymentOrder;
namespace Engineering.Application.Services.TransportationRequests.Commands.PaymentSnapChange;

public record PaymentSnapChangeCommand(
    long RefrenceId,
    PaymentOrderStatus? Status,
    bool IsDeleted
    ) : ICommand<bool?>;
