
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelEnums;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelExporter;

public record GetFilteredFiduciaryProductsExcelExporterRequest(
                                                  List<long>? Ids,
                                                  long? CostCenterId,
                                                  long? ProjectId,
                                                  FiduciaryProductStatus? Status,
                                                  List<long>? ProjectOperationIds,
                                                  long? ThirdPartyId,
                                                  DateTime? FromDate,
                                                  DateTime? ToDate,
                                                  string? FilterData,
                                                  List<FiduciaryProductsExcelEnum>? ExcelFilters,
                                                  string[]? OrderBy,
                                                  int PageIndex,
                                                  int PageSize) : IHttpRequest;
