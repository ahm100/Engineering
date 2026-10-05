using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCommerce;
using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCRHeader;
using Engineering.Application.WebServices.Commercial.Commerces.Models.RemoveCommerce;

namespace Engineering.Infra.Providers.Commercial;

public interface ICommercialProvider
{
    // دریافت اطلاعات کاربران 
    [Post("/v1/CommerceRequestManagement/CreateCommerceRequest")]
    Task<Result<CreateCommerceResponse?>> CreateCommerce(
        [Body] CreateCommerceRequest request, CT ct);

    [Post("/v1/CommerceRequestManagement/CreateCRHeader")]
    Task<Result<CreateCRHeaderResponse?>> CreateCRHeader(
     [Body] CreateCRHeaderRequest request,
     CT ct);

    // دریافت اطلاعات کاربران 
    [Post("/v1/CommerceRequestManagement/CreateCommerceRequestProcess")]
    Task<Result<CreateCommerceResponse?>> CreateCommerceRequestProcess(
        [Body] CreateCommerceRequestProcessRequest request, CT ct);

    // دریافت اطلاعات کاربران 
    [Delete("/v1/CommerceRequestManagement/Remove")]
    Task<Result<RemoveCommerceResponse?>> RemoveCommerce(
        [AliasAs("Id")] long Id, CT ct);
}