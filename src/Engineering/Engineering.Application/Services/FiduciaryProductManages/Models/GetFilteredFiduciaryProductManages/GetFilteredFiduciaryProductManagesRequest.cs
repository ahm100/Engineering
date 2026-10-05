using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFilteredFiduciaryProductManages;

public record GetFilteredFiduciaryProductManagesRequest(long CostCenterId,
                                                        long ProjectId,
                                                        FiduciaryProductStatus? Status,
                                                        List<long>? ProjectOperationIds,
                                                        long? ThirdPartyId,
                                                        DateTime? FromDate,
                                                        DateTime? ToDate,
                                                        string? FilterData,
                                                        int PageIndex,
                                                        int PageSize) : IHttpRequest;
