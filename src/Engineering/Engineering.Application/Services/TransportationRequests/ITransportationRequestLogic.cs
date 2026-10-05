using Engineering.Application.Services.TransportationRequests.Models.AddTransportationRequestBill;
using Engineering.Application.Services.TransportationRequests.Models.AggregateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.CalculatePriceOfTransport;
using Engineering.Application.Services.TransportationRequests.Models.CargoReformsTransport;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToAccepted;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToPaid;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToPending;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToRequestRejection;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToRequestResended;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToSecurityConfirmTransportation;
using Engineering.Application.Services.TransportationRequests.Models.ChangeToSendDoneTransportation;
using Engineering.Application.Services.TransportationRequests.Models.Create;
using Engineering.Application.Services.TransportationRequests.Models.CreateAirplane;
using Engineering.Application.Services.TransportationRequests.Models.CreateAirPlanePaymentOrder;
using Engineering.Application.Services.TransportationRequests.Models.CreateSnap;
using Engineering.Application.Services.TransportationRequests.Models.CreateSnapPaymentOrder;
using Engineering.Application.Services.TransportationRequests.Models.CreateTransportationRequestPaymentOrder;
using Engineering.Application.Services.TransportationRequests.Models.CreateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.Disable;
using Engineering.Application.Services.TransportationRequests.Models.GetAirplaneById;
using Engineering.Application.Services.TransportationRequests.Models.GetById;
using Engineering.Application.Services.TransportationRequests.Models.GetPackingLogesticDetail;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportationById;
using Engineering.Application.Services.TransportationRequests.Models.GetsAirplaneExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsAirplaneExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetsFiltered;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredAirplane;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredRequester;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredSnap;
using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredTransportationCargo;
using Engineering.Application.Services.TransportationRequests.Models.GetSnapById;
using Engineering.Application.Services.TransportationRequests.Models.GetsSnapExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsSnapExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalAirplanePrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalSnapPrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTotalTransportationRequestPrice;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoPallet;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationCargoWithoutContractor;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationPaymentType;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelEnum;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelExporter;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestHistory;
using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestType;
using Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportation;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoById;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationCargoPallet;
using Engineering.Application.Services.TransportationRequests.Models.GetTransportationContractorCalculateType;
using Engineering.Application.Services.TransportationRequests.Models.GroupTransportationRequestStatusChanger;
using Engineering.Application.Services.TransportationRequests.Models.PackingReleaseFromTransport;
using Engineering.Application.Services.TransportationRequests.Models.PackingRivision;
using Engineering.Application.Services.TransportationRequests.Models.SnapRequestExcelImports;
using Engineering.Application.Services.TransportationRequests.Models.TransportationRequestGroupDelete;
using Engineering.Application.Services.TransportationRequests.Models.Update;
using Engineering.Application.Services.TransportationRequests.Models.UpdateAfterCargoDeclaration;
using Engineering.Application.Services.TransportationRequests.Models.UpdateAggregateTransportationWarehouse;
using Engineering.Application.Services.TransportationRequests.Models.UpdateAirplane;
using Engineering.Application.Services.TransportationRequests.Models.UpdateFreeCargosTransportInfo;
using Engineering.Application.Services.TransportationRequests.Models.UpdateMachineDriver;
using Engineering.Application.Services.TransportationRequests.Models.UpdateSnap;
using Engineering.Application.Services.TransportationRequests.Models.UpdateTransportLoadWeight;
using Engineering.Application.Services.TransportationRequests.Models.UpdateTransportPalletLoadWeight;
using Engineering.Application.Services.TransportationRequests.Models.UpdateTransportVolume;

namespace Engineering.Application.Services.TransportationRequests;

public interface ITransportationRequestLogic
{
    ///Commands
    Task<Result<CreateTransportationRequestResponse?>> CreateTransportationRequest(
        CreateTransportationRequestRequest request, CT ct);
    Task<Result<CreateWarehouseTransportationResponse?>> CreateWarehouseTransportation(
        CreateWarehouseTransportationRequest request, CT ct, bool update = false);
    Task<Result<AggregateWarehouseTransportationResponse?>> AggregateWarehouseTransportation(
        AggregateWarehouseTransportationRequest request, CT ct);
    Task<Result<CargoReformsTransportResponse?>> CargoReformsTransport(
        CargoReformsTransportRequest request, CT ct);
    Task<Result<PackingReleaseFromTransportResponse?>> PackingReleaseFromTransport(
        PackingReleaseFromTransportRequest request, CT ct);
    Task<Result<CreateTransportationRequestPaymentOrderResponse?>> CreateTransportationRequestPaymentOrder(
        CreateTransportationRequestPaymentOrderRequest request, CT ct);
    Task<Result<CreateSnapPaymentOrderResponse?>> CreateSnapPaymentOrder(
        CreateSnapPaymentOrderRequest request, CT ct);
    Task<Result<CreateAirPlanePaymentOrderResponse?>> CreateAirPlanePaymentOrder(
        CreateAirPlanePaymentOrderRequest request, CT ct);
    Task<Result<CreateSnapResponse?>> CreateSnap(
        CreateSnapRequest request, CT ct);
    Task<Result<CreateAirplaneResponse?>> CreateAirplane(
        CreateAirplaneRequest request, CT ct);
    Task<Result<SnapRequestExcelImportsResponse?>> SnapRequestExcelImports(
        SnapRequestExcelImportsRequest request, CT ct);
    Task<Result<DisableTransportationRequestResponse?>> DisableTransportationRequest(
        DisableTransportationRequestRequest request, CT ct);
    Task<Result<UpdateTransportationRequestResponse?>> UpdateTransportationRequest(
        UpdateTransportationRequestRequest request, CT ct);
    Task<Result<UpdateAggregateTransportationWarehouseResponse?>> UpdateAggregateTransportationWarehouse(
        UpdateAggregateTransportationWarehouseRequest request, CT ct);
    Task<Result<AddTransportationRequestBillResponse?>> AddTransportationRequestBill(
        AddTransportationRequestBillRequest request, CT ct);
    Task<Result<CalculatePriceOfTransportResponse?>> CalculatePriceOfTransport(
        CalculatePriceOfTransportRequest request, CT ct);
    Task<Result<PackingRivisionResponse?>> PackingRivision(
        PackingRivisionRequest request, CT ct);
    Task<Result<UpdateMachineDriverResponse?>> UpdateMachineDriver(
        UpdateMachineDriverRequest request, CT ct);
    Task<Result<UpdateTransportLoadWeightResponse?>> UpdateTransportLoadWeight(
        UpdateTransportLoadWeightRequest request, CT ct);
    Task<Result<UpdateTransportPalletLoadWeightResponse?>> UpdateTransportPalletLoadWeight(
        UpdateTransportPalletLoadWeightRequest request, CT ct);
    Task<Result<UpdateTransportVolumeResponse?>> UpdateTransportVolume(
        UpdateTransportVolumeRequest request, CT ct);
    Task<Result<UpdateAfterCargoDeclarationResponse?>> UpdateAfterCargoDeclaration(
        UpdateAfterCargoDeclarationRequest request, CT ct);
    Task<Result<UpdateFreeCargosTransportInfoResponse?>> UpdateFreeCargosTransportInfo(
        UpdateFreeCargosTransportInfoRequest request, CT ct);
    Task<Result<UpdateSnapResponse?>> UpdateSnap(
        UpdateSnapRequest request, CT ct);
    Task<Result<UpdateAirplaneResponse?>> UpdateAirplane(
        UpdateAirplaneRequest request, CT ct);
    Task<Result<ChangeToAcceptedTransportationRequestResponse?>> ChangeToAcceptedTransportationRequest(
        ChangeToAcceptedTransportationRequestRequest request, CT ct);
    Task<Result<ChangeToPaidTransportationRequestResponse?>> ChangeToPaidTransportationRequest(
        ChangeToPaidTransportationRequestRequest request, CT ct);
    Task<Result<ChangeToPendingTransportationRequestResponse?>> ChangeToPendingTransportationRequest(
        ChangeToPendingTransportationRequestRequest request, CT ct);
    Task<Result<ChangeToRequestRejectionTransportationRequestResponse?>> ChangeToRequestRejectionTransportationRequest(
        ChangeToRequestRejectionTransportationRequestRequest request, CT ct);
    Task<Result<ChangeToRequestResendedTransportationRequestResponse?>> ChangeToRequestResendedTransportationRequest(
        ChangeToRequestResendedTransportationRequestRequest request, CT ct);
    Task<Result<TransportationRequestGroupDeleteResponse?>> TransportationRequestGroupDelete(
        TransportationRequestGroupDeleteRequest request, CT ct);
    Task<Result<GroupTransportationRequestStatusChangerResponse?>> GroupTransportationRequestStatusChanger(
        GroupTransportationRequestStatusChangerRequest request, CT ct);
    Task<Result<ChangeToSendDoneTransportationResponse?>> ChangeToSendDoneTransportation(
        ChangeToSendDoneTransportationRequest request, CT ct);
    Task<Result<ChangeToSecurityConfirmTransportationResponse?>> ChangeToSecurityConfirmTransportation(
        ChangeToSecurityConfirmTransportationRequest request, CT ct);

    ///Queries
    Task<Result<GetPackingLogesticDetailResponse?>> GetPackingLogesticDetail(
        GetPackingLogesticDetailRequest request, CT ct);
    Task<Result<GetsTransportationRequestHistoryResponse?>> GetsTransportationRequestHistory(
        GetsTransportationRequestHistoryRequest request, CT ct);
    Task<Result<GetAirplaneByIdResponse?>> GetAirplaneById(
        GetAirplaneByIdRequest request, CT ct);
    Task<Result<GetSnapByIdResponse?>> GetSnapById(
        GetSnapByIdRequest request, CT ct);
    Task<Result<GetTransportationRequestByIdResponse?>> GetTransportationRequestById(
        GetTransportationRequestByIdRequest request, CT ct);
    Task<Result<GetsFilteredAirplaneResponse?>> GetsFilteredAirplane(
        GetsFilteredAirplaneRequest request, CT ct);
    Task<Result<GetsFilteredSnapResponse?>> GetsFilteredSnap(
        GetsFilteredSnapRequest request, CT ct);
    Task<Result<GetsFilteredTransportationRequestResponse?>> GetsFilteredTransportationRequest(
        GetsFilteredTransportationRequestRequest request, CT ct);
    Task<Result<GetsWarehouseTransportationResponse?>> GetsWarehouseTransportation(
        GetsWarehouseTransportationRequest request, CT ct);
    Task<Result<GetsAggregateWarehouseTransportationResponse?>> GetsAggregateWarehouseTransportation(
        GetsAggregateWarehouseTransportationRequest request, CT ct);
    Task<Result<GetsAggregateWarehouseTransportationByIdResponse?>> GetsAggregateWarehouseTransportationById(
        GetsAggregateWarehouseTransportationByIdRequest request, CT ct);
    Task<Result<GetsTransportationRequestTypeResponse?>> GetsTransportationRequestType(
        GetsTransportationRequestTypeRequest request, CT ct);
    Task<Result<GetsFilteredRequesterResponse?>> GetsFilteredRequester(
        GetsFilteredRequesterRequest request, CT ct);
    Task<Result<GetsTransportationRequestExcelExporterResponse?>> GetsTransportationRequestExcelExporter(
        GetsTransportationRequestExcelExporterRequest request, CT ct);
    Task<Result<GetsTransportationRequestExcelEnumResponse?>> GetsTransportationRequestExcelEnum(
        GetsTransportationRequestExcelEnumRequest request, CT ct);
    Task<Result<GetsSnapExcelExporterResponse?>> GetsSnapExcelExporter(
        GetsSnapExcelExporterRequest request, CT ct);
    Task<Result<GetsSnapExcelEnumResponse?>> GetsSnapExcelEnum(
        GetsSnapExcelEnumRequest request, CT ct);
    Task<Result<GetsAirplaneExcelExporterResponse?>> GetsAirplaneExcelExporter(
        GetsAirplaneExcelExporterRequest request, CT ct);
    Task<Result<GetsAirplaneExcelEnumResponse?>> GetsAirplaneExcelEnum(
        GetsAirplaneExcelEnumRequest request, CT ct);
    Task<Result<GetTransportationContractorCalculateTypeResponse?>> GetTransportationContractorCalculateType(
        GetTransportationContractorCalculateTypeRequest request, CT ct);
    Task<Result<GetsTotalAirplanePriceResponse?>> GetsTotalAirplanePrice(
        GetsTotalAirplanePriceRequest request, CT ct);
    Task<Result<GetsTotalSnapPriceResponse?>> GetsTotalSnapPrice(
        GetsTotalSnapPriceRequest request, CT ct);
    Task<Result<GetsTotalTransportationRequestPriceResponse?>> GetsTotalTransportationRequestPrice(
        GetsTotalTransportationRequestPriceRequest request, CT ct);
    Task<Result<GetsTransportationPaymentTypeResponse?>> GetsTransportationPaymentType(
        GetsTransportationPaymentTypeRequest request, CT ct);
    Task<Result<GetsTransportationCargoPalletResponse?>> GetsTransportationCargoWithoutContractor(
        GetsTransportationCargoWithoutContractorRequest request, CT ct);
    Task<Result<GetsTransportationCargoPalletResponse?>> GetsTransportationCargoPallet(
        GetsTransportationCargoPalletRequest request, CT ct);
    Task<Result<GetTransportationCargoPalletResponse?>> GetTransportationCargoPallet(
        GetTransportationCargoPalletRequest request, CT ct);
    Task<Result<GetsFilteredTransportationCargoResponse?>> GetsFilteredTransportationCargo(
        GetsFilteredTransportationCargoRequest request, CT ct);
    Task<Result<GetTransportationCargoByIdResponse?>> GetTransportationCargoById(
            GetTransportationCargoByIdRequest request, CT ct);

}