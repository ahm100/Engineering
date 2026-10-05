using Engineering.Domain.Entities.RequestGoodsSupplies;
using Gita.Backend.Shared.Domain.Enums.Commerces;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductCommercialStatusChenger;

public record GoodsSupplyProductCommercialStatusChengerCommand(
    long Id,
    long InvoiceId,
    CommerceRequestStatus Status,
    decimal? ConfirmedRequestCount,
    string? Description,
    string? LastDescription,
    long? UserId,
    long? OperatorAppointmentId
    ) : ICommand<RequestGoodsSupplyProduct>;

