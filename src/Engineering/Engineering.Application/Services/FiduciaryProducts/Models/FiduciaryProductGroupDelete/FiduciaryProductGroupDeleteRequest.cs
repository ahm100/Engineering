
namespace Engineering.Application.Services.FiduciaryProducts.Models.FiduciaryProductGroupDelete;

public record FiduciaryProductGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
