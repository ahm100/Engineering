using Engineering.Domain.Entities.FiduciaryProducts;
using Gita.Backend.Shared.Domain.Enums.Invoice;
using Gita.Backend.Shared.Domain.Messages;

namespace Engineering.Application.Services.FiduciaryProducts.Commands.FiduciaryProductWarehouseStatusChanger;

public record FiduciaryProductWarehouseStatusChangerCommand(
    long Id,
    long InvoiceId,
    WarehouseInvoiceStatus Status,
    WarehouseInvoiceType Type,
    string? Description,
    long? UserId,
    List<WarehouseInvoiceProductResponse> Products,
    List<WarehouseInvoiceRequestProductResponse> RequestProducts,
    IMediator mediator,
    long CurrentUser
    ) : ICommand<FiduciaryProduct>;

