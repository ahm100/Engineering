using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProducts;

public record GetFilteredFiduciaryProductsRequest(long? CostCenterId,
                                                  long? ProjectId,
                                                  FiduciaryProductStatus? Status,
                                                  List<long>? ProjectOperationIds,
                                                  long? ThirdPartyId,
                                                  DateTime? FromDate,
                                                  DateTime? ToDate,
                                                  string? FilterData,
                                                  string[]? OrderBy,
                                                  int PageIndex,
                                                  int PageSize) : IHttpRequest;
