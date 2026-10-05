using Engineering.Application.WebServices.WarehouseServices.Products.Models.RemoveInvoice;

namespace Engineering.Application.WebServices.WarehouseServices.Invoices.Commands.RemoveInvoice;

public record RemoveInvoiceCommand(
    long Id) : ICommand<RemoveInvoiceResponse?>;
