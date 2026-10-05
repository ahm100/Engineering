namespace Engineering.Application.WebServices.WarehouseServices.InvoiceProducts.Models.GetByInvoiceId;

public record GetAllActiveInvoiceProductsRequest(long InvoiceId,
                                                 string? Name,
                                                 string? Code,
                                                 string? FilterData,
                                                 long? BrandId,
                                                 long? BrandModelId,
                                                 int PageIndex,
                                                 int PageSize);
