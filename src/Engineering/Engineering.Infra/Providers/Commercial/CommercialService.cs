using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCommerce;
using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCRHeader;
using Engineering.Application.WebServices.Commercial.Commerces.Models.RemoveCommerce;

namespace Engineering.Infra.Providers.Commercial;

public class CommercialService : ICommercialService
{
    private readonly ICommercialProvider _commercialProvider;

    public CommercialService(ICommercialProvider CommercialProvider)
    {
        _commercialProvider = CommercialProvider;
    }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public async Task<Result<CreateCommerceResponse?>> CreateCommerce(CreateCommerceRequest request, CT ct)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    {
        var result = await _commercialProvider.CreateCommerce(request, ct);
        return result;
    }
    public async Task<Result<CreateCRHeaderResponse?>> CreateCRHeader(
        CreateCRHeaderRequest request, CT ct)
    {
        var result = await _commercialProvider.CreateCRHeader(request, ct);
        return result;
    }
#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public Task<Result<CreateCommerceResponse?>> CreateCommerceRequestProcess(CreateCommerceRequestProcessRequest request, CT ct)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    {
        var result = _commercialProvider.CreateCommerceRequestProcess(request, ct);
        return result;
    }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public async Task<Result<RemoveCommerceResponse?>> RemoveCommerce(RemoveCommerceRequest request, CT ct)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    {
        return await _commercialProvider.RemoveCommerce(request.Id, ct);
    }

}