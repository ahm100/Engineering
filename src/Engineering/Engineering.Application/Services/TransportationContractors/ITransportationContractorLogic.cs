using Engineering.Application.Services.TransportationContractors.Contracts.ChangeTransportationContractorState;
using Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.DeleteTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsActiveTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsDeliveryMethod;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsDeliveryType;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsFilteredTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsPriceWeightHistory;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsTransportationContractorExcelEnum;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsTransportationContractorExcelExporter;
using Engineering.Application.Services.TransportationContractors.Contracts.GetTransportationContractorById;
using Engineering.Application.Services.TransportationContractors.Contracts.PriceWeightImportExcel;
using Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Models.PriceWeightExcelImports;

namespace Engineering.Application.Services.TransportationContractors;

public interface ITransportationContractorLogic
{
    ///Commands
    Task<Result<CreateTransportationContractorResponse?>> CreateTransportationContractor(CreateTransportationContractorRequest request, CT ct);
    Task<Result<DeleteTransportationContractorResponse?>> DeleteTransportationContractor(DeleteTransportationContractorRequest request, CT ct);
    Task<Result<UpdateTransportationContractorResponse?>> UpdateTransportationContractor(UpdateTransportationContractorRequest request, CT ct);
    Task<Result<ChangeTransportationContractorStateResponse?>> ChangeTransportationContractorState(ChangeTransportationContractorStateRequest request, CT ct);
    Task<Result<PriceWeightExcelImportsResponse?>> PriceWeightExcelImports(PriceWeightExcelImportsRequest request, CT ct);

    ///Queries
    Task<Result<GetTransportationContractorByIdResponse?>> GetTransportationContractorById(GetTransportationContractorByIdRequest request, CT ct);
    Task<Result<GetsActiveTransportationContractorResponse?>> GetsActiveTransportationContractor(GetsActiveTransportationContractorRequest request, CT ct);
    Task<Result<GetsFilteredTransportationContractorResponse?>> GetsFilteredTransportationContractor(GetsFilteredTransportationContractorRequest request, CT ct);
    Task<Result<GetsPriceWeightHistoryResponse?>> GetsPriceWeightHistory(GetsPriceWeightHistoryRequest request, CT ct);
    Task<Result<PriceWeightImportExcelResponse?>> PriceWeightImportExcel(PriceWeightImportExcelRequest request, CT ct);
    Task<Result<GetsTransportationContractorExcelExporterResponse?>> GetsTransportationContractorExcelExporter(GetsTransportationContractorExcelExporterRequest request, CT ct);
    Task<Result<GetsTransportationContractorExcelEnumResponse?>> GetsTransportationContractorExcelEnum(GetsTransportationContractorExcelEnumRequest request, CT ct);
    Task<Result<GetsDeliveryMethodResponse?>> GetsDeliveryMethod(GetsDeliveryMethodRequest request, CT ct);
    Task<Result<GetsDeliveryTypeResponse?>> GetsDeliveryType(GetsDeliveryTypeRequest request, CT ct);
}