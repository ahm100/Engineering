using Engineering.Application.Services.Advertisements.Contracts.ChangeAdvertisementState;
using Engineering.Application.Services.Advertisements.Contracts.CreateAdvertisement;
using Engineering.Application.Services.Advertisements.Contracts.DeleteAdvertisement;
using Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementById;
using Engineering.Application.Services.Advertisements.Contracts.GetFltrAdvertisement;
using Engineering.Application.Services.Advertisements.Contracts.UpdateAdvertisement;

namespace Engineering.Application.Services.Advertisements;

public interface IAdvertisementLogic
{
    Task<Result<CreateAdvertisementResponse?>> CreateAdvertisement(
        CreateAdvertisementRequest request, CT ct);

    Task<Result<UpdateAdvertisementResponse?>> UpdateAdvertisement(
        UpdateAdvertisementRequest request, CT ct);

    Task<Result<DeleteAdvertisementResponse?>> DeleteAdvertisement(
        DeleteAdvertisementRequest request, CT ct);

    Task<Result<ChangeAdvertisementStateResponse?>> ChangeAdvertisementState(
        ChangeAdvertisementStateRequest request, CT ct);

    Task<Result<GetAdvertisementByIdResponse?>> GetAdvertisementById(
        GetAdvertisementByIdRequest request, CT ct);

    Task<Result<GetFltrAdvertisementResponse?>> GetFltrAdvertisement(
        GetFltrAdvertisementRequest request, CT ct);
}
