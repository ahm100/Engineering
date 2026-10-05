using Engineering.Domain.Entities.FiduciaryProducts.Enums;
using Gita.Backend.Shared.Application.WebServices.IdentityServices.Users.Queries.GetsUserById;

namespace Engineering.Application.Services.FiduciaryProductManages;

public partial class FiduciaryProductManageLogic : IFiduciaryProductManageLogic
{
    private async Task<string> DescriptionMacker(
        string? requestDescription,
        FiduciaryProductStatus status, CT ct)
    {
        var subSystem = "مهندسی";

        var getUsers = await _mediator.Send(new GetsUserByIdQuery([_currenctUserId]), ct);
        var user = getUsers.Value?.Data?.FirstOrDefault();

        return $"{subSystem} - {user?.FullName} - {status.GetEnumDescription()} - {requestDescription}";
    }

    private async Task<string> DetailDescriptionMacker(
        string? requestDescription,
        FiduciaryProductDetailStatus detailStatus, CT ct)
    {
        var subSystem = "مهندسی";

        var getUsers = await _mediator.Send(new GetsUserByIdQuery([_currenctUserId]), ct);
        var user = getUsers.Value?.Data?.FirstOrDefault();

        return $"{subSystem} - {user?.FullName} - {detailStatus.GetEnumDescription()} - {requestDescription}";
    }

}
