namespace Engineering.Application.WebServices.WarehouseServices.Invoices.Models;

public record InvoiceProductDto(long ProductId,
                                long PackageId,
                                double PackageQuantity,
                                DateTime? ManufactureDate,
                                DateTime? ExpirationDate,
                                List<InvoiceItemDto>? Items);

public record InvoiceItemDto(string? SerialNo,
                                DateTime? ExpirationDate,
                                DateTime? ManufactureDate);
