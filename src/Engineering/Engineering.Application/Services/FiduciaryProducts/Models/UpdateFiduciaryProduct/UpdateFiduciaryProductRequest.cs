namespace Engineering.Application.Services.FiduciaryProducts.Models.UpdateFiduciaryProduct;

public record UpdateFiduciaryProductRequest(long Id,
                                            long CostCenterId,
                                            long ProjectId,
                                            long ProjectOperationId,
                                            long ThirdPartyId,
                                            string? Description,
                                            List<UpdateFiduciaryProductDetailModel> Products) : IHttpRequest;
