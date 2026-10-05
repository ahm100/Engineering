namespace Engineering.Application.Services.FiduciaryProductManages.Models.SetFiduciaryProductRejected;

public record SetFiduciaryProductRejectedRequest(long FiduciaryProductId,
                                                 string? ProductDescription) : IHttpRequest;
