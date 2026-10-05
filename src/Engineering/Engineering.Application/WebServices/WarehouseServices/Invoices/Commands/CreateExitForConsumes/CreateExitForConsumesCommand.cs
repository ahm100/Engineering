using Engineering.Application.WebServices.WarehouseServices.Invoices.Models;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;

public record CreateExitForConsumesCommand(
    long WarehouseId,
    string? Description,
    long UserRegisterId,
    ImportanceDegree Importance,
    string CommercialRequestId,
    string CommercialRequestNo,
    List<InvoiceExitProductDtoNoManagement> Products,
    List<DocumentInvoiceModel>? Documents,
    bool IsManually,
    long? OwnerId
    ) : ICommand<CreateInvoiceResponse?>;

public record CreateExitForConsumesModel(
    long WarehouseId,
    string? Description,
    long UserRegisterId,
    ImportanceDegree Importance,
    string CommercialRequestId,
    string CommercialRequestNo,
    List<InvoiceExitProductDto> Products,
    List<DocumentInvoiceModel>? Documents,
    bool IsManually,
    long? OwnerId
    );
