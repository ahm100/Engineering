using Engineering.Application.Services.Transportations.Models.Active;
using Engineering.Application.Services.Transportations.Models.CodeCreator;
using Engineering.Application.Services.Transportations.Models.Create;
using Engineering.Application.Services.Transportations.Models.Disable;
using Engineering.Application.Services.Transportations.Models.GetByCode;
using Engineering.Application.Services.Transportations.Models.GetById;
using Engineering.Application.Services.Transportations.Models.GetByName;
using Engineering.Application.Services.Transportations.Models.GetsActive;
using Engineering.Application.Services.Transportations.Models.GetsFiltered;
using Engineering.Application.Services.Transportations.Models.GetsTransportationExcelEnum;
using Engineering.Application.Services.Transportations.Models.GetsTransportationExcelExporter;
using Engineering.Application.Services.Transportations.Models.GetTransportationTypes;
using Engineering.Application.Services.Transportations.Models.Inactive;
using Engineering.Application.Services.Transportations.Models.StateChangerTransportations;
using Engineering.Application.Services.Transportations.Models.TransportationExcelImports;
using Engineering.Application.Services.Transportations.Models.TransportationGroupDelete;
using Engineering.Application.Services.Transportations.Models.Update;

namespace Engineering.Application.Services.Transportations;

public interface ITransportationLogic
{
    ///Commands
    Task<Result<CreateTransportationResponse?>> CreateTransportation(CreateTransportationRequest request, CT ct);
    Task<Result<TransportationExcelImportsResponse?>> TransportationExcelImports(TransportationExcelImportsRequest request, CT ct);
    Task<Result<DisableTransportationResponse?>> DisableTransportation(DisableTransportationRequest request, CT ct);
    Task<Result<UpdateTransportationResponse?>> UpdateTransportation(UpdateTransportationRequest request, CT ct);
    Task<Result<InactiveTransportationResponse?>> InactiveTransportation(InactiveTransportationRequest request, CT ct);
    Task<Result<ActiveTransportationResponse?>> ActiveTransportation(ActiveTransportationRequest request, CT ct);
    Task<Result<TransportationCodeCreatorResponse?>> TransportationCodeCreator(TransportationCodeCreatorRequest request, CT ct);
    Task<Result<StateChangerTransportationsResponse?>> StateChangerTransportations(StateChangerTransportationsRequest request, CT ct);
    Task<Result<TransportationGroupDeleteResponse?>> TransportationGroupDelete(TransportationGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetTransportationByIdResponse?>> GetTransportationById(GetTransportationByIdRequest request, CT ct);
    Task<Result<GetTransportationByNameResponse?>> GetTransportationByName(GetTransportationByNameRequest request, CT ct);
    Task<Result<GetTransportationByCodeResponse?>> GetTransportationByCode(GetTransportationByCodeRequest request, CT ct);
    Task<Result<GetsActiveTransportationResponse?>> GetsActiveTransportation(GetsActiveTransportationRequest request, CT ct);
    Task<Result<GetsFilteredTransportationResponse?>> GetsFilteredTransportation(GetsFilteredTransportationRequest request, CT ct);
    Task<Result<GetsTransportationExcelExporterResponse?>> GetsTransportationExcelExporter(GetsTransportationExcelExporterRequest request, CT ct);
    Task<Result<GetsTransportationExcelEnumResponse?>> GetsTransportationExcelEnum(GetsTransportationExcelEnumRequest request, CT ct);
    Task<Result<GetTransportationTypeResponse?>> GetTransportationTypes(GetTransportationTypeRequest request, CT ct);

}