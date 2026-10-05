
namespace Engineering.Application.Extensions;

public static class CompanyValidator
{

    public static long? GetCompanyId(
        IUserInfoService userInfoService)
    {
        return userInfoService.UserCompanyId > 0 ? userInfoService.UserCompanyId : null;
    }

    public static async Task<bool> IsCompanyValid(
        long? companyId,
        IMediator _mediator,
        CT ct)
    {
        if (companyId is not null && companyId >= 1)
        {
            var companyResponse = await _mediator.Send(new GetCompanyByIdQuery((long)companyId), ct);
            return companyResponse.IsSuccess;
        }
        return true;
    }

}
