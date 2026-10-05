using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCommerce;
using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCRHeader;
using Engineering.Application.WebServices.Commercial.Commerces.Models.RemoveCommerce;

namespace Engineering.Application.Abstractions.Interfaces;

public interface ICommercialService
{
    Task<Result<CreateCommerceResponse>> CreateCommerce(
        CreateCommerceRequest request, CT ct);

    Task<Result<CreateCRHeaderResponse?>> CreateCRHeader(
        CreateCRHeaderRequest request, CT ct);

    Task<Result<CreateCommerceResponse>> CreateCommerceRequestProcess(
        CreateCommerceRequestProcessRequest request, CT ct);

    Task<Result<RemoveCommerceResponse>> RemoveCommerce(
        RemoveCommerceRequest request, CT ct);

}