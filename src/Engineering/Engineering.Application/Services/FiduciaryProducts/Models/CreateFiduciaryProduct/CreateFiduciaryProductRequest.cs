namespace Engineering.Application.Services.FiduciaryProducts.Models.CreateFiduciaryProduct;

public record CreateFiduciaryProductRequest(long CostCenterId,
                                            long ProjectId,
                                            long ProjectOperationId,
                                            long ThirdPartyId,
                                            string? Description,
                                            List<CreateFiduciaryProductDetailModel> Products) : IHttpRequest;
