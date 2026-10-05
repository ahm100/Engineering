using Engineering.Application.WebServices.WarehouseServices.Invoices.Models;
using Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForConsumes;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;

namespace Engineering.Application.WebServices.WarehouseServices.Products.Commands.CreateExitForRelocation;

public record CreateExitForRelocationCommand(
    long SourceWarehouseId,
    long DestinationWarehouseId,
    string? Description,
    ImportanceDegree Importance,
    string CommercialRequestId,
    string CommercialRequestNo,
    List<InvoiceExitProductDtoNoManagement> Products,
    List<DocumentInvoiceModel>? Documents,
    long UserRegisterId,
    bool IsManually,
    long? OwnerId
    ) : ICommand<CreateInvoiceResponse?>;

public record CreateExitForRelocationModel(
    long SourceWarehouseId,
    long DestinationWarehouseId,
    string? Description,
    ImportanceDegree Importance,
    string CommercialRequestId,
    string CommercialRequestNo,
    List<InvoiceExitProductDto> Products,
    List<DocumentInvoiceModel>? Documents,
    long UserRegisterId,
    bool IsManually,
    long? OwnerId
    );