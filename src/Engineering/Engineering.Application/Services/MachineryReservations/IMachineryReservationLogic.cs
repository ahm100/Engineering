using Engineering.Application.Services.MachineryReservations.Models.CreateMachineryReservation;
using Engineering.Application.Services.MachineryReservations.Models.DisableMachineryReservation;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservationById;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservations;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservationStatus;
using Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservationUnit;
using Engineering.Application.Services.MachineryReservations.Models.MachineryReservationGroupDelete;
using Engineering.Application.Services.MachineryReservations.Models.UpdateMachineryReservation;
using Engineering.Application.Services.MachineryReservations.Models.UpdateMachineryReservationStatus;

namespace Engineering.Application.Services.MachineryReservations;

public partial interface IMachineryReservationLogic
{
    ///Commands
    Task<Result<CreateMachineryReservationResponse?>> CreateMachineryReservation(
        CreateMachineryReservationRequest request, CT ct);

    Task<Result<DisableMachineryReservationResponse?>> DisableMachineryReservation(
        DisableMachineryReservationRequest request, CT ct);

    Task<Result<UpdateMachineryReservationResponse?>> UpdateMachineryReservation(
        UpdateMachineryReservationRequest request, CT ct);

    Task<Result<UpdateMachineryReservationStatusResponse?>> MachineryReservationStatusChanger(
        UpdateMachineryReservationStatusRequest request, CT ct);

    Task<Result<MachineryReservationGroupDeleteResponse?>> MachineryReservationGroupDelete(
        MachineryReservationGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetMachineryReservationByIdResponse?>> GetMachineryReservationById(
        GetMachineryReservationByIdRequest request, CT ct);

    Task<Result<GetMachineryReservationsResponse?>> GetsMachineryReservation(
        GetMachineryReservationsRequest request, CT ct);

    Task<Result<GetMachineryReservationUnitResponse?>> GetMachineryReservationUnit(
        GetMachineryReservationUnitRequest request, CT ct);

    Task<Result<GetMachineryReservationStatusResponse?>> GetMachineryReservationStatus(
        GetMachineryReservationStatusRequest request, CT ct);
}