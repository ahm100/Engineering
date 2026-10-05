using Engineering.Application.WebServices.WarehouseServices.InvoiceProducts.Models;

namespace Engineering.Application.WebServices.WarehouseServices.InvoiceProducts.Queries;

public record GetByInvoiceIdQuery(long InvoiceId,
                                  string? Name,
                                  string? Code,
                                  string? FilterData,
                                  long? BrandId,
                                  long? BrandModelId,
                                  int PageIndex,
                                  int PageSize) : IQuery<DataResult<List<GetAllActiveInvoiceProductsModel>?>?>;