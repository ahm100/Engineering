using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.ChangeTransportationContractorPersonnelState;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.CreateTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.DeleteTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsActiveTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsFilteredTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsTransportationContractorPersonnelExcelEnum;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsTransportationContractorPersonnelExcelExporter;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetTransportationContractorPersonnelById;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.UpdateTransportationContractorPersonnel;

namespace Engineering.Application.Services.TransportationContractorPersonnels;

public interface ITransportationContractorPersonnelLogic
{
    ///Commands
    Task<Result<CreateTransportationContractorPersonnelResponse?>> CreateTransportationContractorPersonnel(
        CreateTransportationContractorPersonnelRequest request, CT ct);
    Task<Result<DeleteTransportationContractorPersonnelResponse?>> DeleteTransportationContractorPersonnel(
        DeleteTransportationContractorPersonnelRequest request, CT ct);
    Task<Result<UpdateTransportationContractorPersonnelResponse?>> UpdateTransportationContractorPersonnel(
        UpdateTransportationContractorPersonnelRequest request, CT ct);
    Task<Result<ChangeTransportationContractorPersonnelStateResponse?>> ChangeTransportationContractorPersonnelState(
        ChangeTransportationContractorPersonnelStateRequest request, CT ct);

    ///Queries
    Task<Result<GetTransportationContractorPersonnelByIdResponse?>> GetTransportationContractorPersonnelById(
        GetTransportationContractorPersonnelByIdRequest request, CT ct);
    Task<Result<GetsActiveTransportationContractorPersonnelResponse?>> GetsActiveTransportationContractorPersonnel(
        GetsActiveTransportationContractorPersonnelRequest request, CT ct);
    Task<Result<GetsFilteredTransportationContractorPersonnelResponse?>> GetsFilteredTransportationContractorPersonnel(
        GetsFilteredTransportationContractorPersonnelRequest request, CT ct);
    Task<Result<GetsTransportationContractorPersonnelExcelExporterResponse?>> GetsTransportationContractorPersonnelExcelExporter(
        GetsTransportationContractorPersonnelExcelExporterRequest request, CT ct);
    Task<Result<GetsTransportationContractorPersonnelExcelEnumResponse?>> GetsTransportationContractorPersonnelExcelEnum(
        GetsTransportationContractorPersonnelExcelEnumRequest request, CT ct);
}