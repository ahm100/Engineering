using Engineering.Application.Services.Trips.Models.Active;
using Engineering.Application.Services.Trips.Models.CodeCreator;
using Engineering.Application.Services.Trips.Models.Create;
using Engineering.Application.Services.Trips.Models.Disable;
using Engineering.Application.Services.Trips.Models.GetByCode;
using Engineering.Application.Services.Trips.Models.GetById;
using Engineering.Application.Services.Trips.Models.GetByName;
using Engineering.Application.Services.Trips.Models.GetsActive;
using Engineering.Application.Services.Trips.Models.GetsFiltered;
using Engineering.Application.Services.Trips.Models.GetsTripExcelEnum;
using Engineering.Application.Services.Trips.Models.GetsTripExcelExporter;
using Engineering.Application.Services.Trips.Models.Inactive;
using Engineering.Application.Services.Trips.Models.StateChangerTrips;
using Engineering.Application.Services.Trips.Models.TripExcelImports;
using Engineering.Application.Services.Trips.Models.TripGroupDelete;
using Engineering.Application.Services.Trips.Models.Update;

namespace Engineering.Application.Services.Trips;

public interface ITripLogic
{
    ///Commands
    Task<Result<CreateTripResponse?>> CreateTrip(CreateTripRequest request, CT ct);
    Task<Result<TripExcelImportsResponse?>> TripExcelImports(TripExcelImportsRequest request, CT ct);
    Task<Result<DisableTripResponse?>> DisableTrip(DisableTripRequest request, CT ct);
    Task<Result<UpdateTripResponse?>> UpdateTrip(UpdateTripRequest request, CT ct);
    Task<Result<InactiveTripResponse?>> InactiveTrip(InactiveTripRequest request, CT ct);
    Task<Result<ActiveTripResponse?>> ActiveTrip(ActiveTripRequest request, CT ct);
    Task<Result<TripCodeCreatorResponse?>> TripCodeCreator(TripCodeCreatorRequest request, CT ct);
    Task<Result<StateChangerTripsResponse?>> StateChangerTrips(StateChangerTripsRequest request, CT ct);
    Task<Result<TripGroupDeleteResponse?>> TripGroupDelete(TripGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetTripByIdResponse?>> GetTripById(GetTripByIdRequest request, CT ct);
    Task<Result<GetTripByNameResponse?>> GetTripByName(GetTripByNameRequest request, CT ct);
    Task<Result<GetTripByCodeResponse?>> GetTripByCode(GetTripByCodeRequest request, CT ct);
    Task<Result<GetsActiveTripResponse?>> GetsActiveTrip(GetsActiveTripRequest request, CT ct);
    Task<Result<GetsFilteredTripResponse?>> GetsFilteredTrip(GetsFilteredTripRequest request, CT ct);
    Task<Result<GetsTripExcelEnumResponse?>> GetsTripExcelEnum(GetsTripExcelEnumRequest request, CT ct);
    Task<Result<GetsTripExcelExporterResponse?>> GetsTripExcelExporter(GetsTripExcelExporterRequest request, CT ct);

}