using Engineering.Application.Services.ShippingCosts.Contracts.ChangeShippingCostState;
using Engineering.Application.Services.ShippingCosts.Contracts.CreateShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.DeleteShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsActiveShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsFilteredShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetShippingCostById;
using Engineering.Application.Services.ShippingCosts.Contracts.GetShppingCostHistory;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsShippingCostExcelEnum;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsShippingCostExcelExporter;
using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcel;
using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcelHelper;
using Engineering.Application.Services.ShippingCosts.Contracts.UpdateShippingCost;
using Engineering.Application.Services.ShippingCosts.Models.ShippingCostExcelImports;

namespace Engineering.Application.Services.ShippingCosts;

public interface IShippingCostLogic
{
    ///Commands
    Task<Result<CreateShippingCostResponse?>> CreateShippingCost(CreateShippingCostRequest request, CT ct);
    Task<Result<DeleteShippingCostResponse?>> DeleteShippingCost(DeleteShippingCostRequest request, CT ct);
    Task<Result<UpdateShippingCostResponse?>> UpdateShippingCost(UpdateShippingCostRequest request, CT ct);
    Task<Result<ChangeShippingCostStateResponse?>> ChangeShippingCostState(ChangeShippingCostStateRequest request, CT ct);
    Task<Result<ShippingCostExcelImportsResponse?>> ShippingCostExcelImports(ShippingCostExcelImportsRequest request, CT ct);

    ///Queries
    Task<Result<GetShippingCostByIdResponse?>> GetShippingCostById(GetShippingCostByIdRequest request, CT ct);
    Task<Result<GetsActiveShippingCostResponse?>> GetsActiveShippingCost(GetsActiveShippingCostRequest request, CT ct);
    Task<Result<GetsFilteredShippingCostResponse?>> GetsFilteredShippingCost(GetsFilteredShippingCostRequest request, CT ct);
    Task<Result<GetShippingCostHistoryResponse?>> GetShippingCostHistory(GetShippingCostHistoryRequest request, CT ct);
    Task<Result<ShippingCostImportExcelResponse?>> ShippingCostImportExcel(ShippingCostImportExcelRequest request, CT ct);
    Task<Result<GetsShippingCostExcelExporterResponse?>> GetsShippingCostExcelExporter(GetsShippingCostExcelExporterRequest request, CT ct);
    Task<Result<GetsShippingCostExcelEnumResponse?>> GetsShippingCostExcelEnum(GetsShippingCostExcelEnumRequest request, CT ct);
    Task<Result<ShippingCostImportExcelHelperResponse?>> ShippingCostImportExcelHelper(ShippingCostImportExcelHelperRequest request, CT ct);
}