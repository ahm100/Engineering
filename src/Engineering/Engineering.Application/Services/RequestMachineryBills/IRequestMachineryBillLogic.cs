using Engineering.Application.Services.RequestMachineries.Models.GetOperationContractors;
using Engineering.Application.Services.RequestMachineryBills.Models.CreateRequestMachineryBill;
using Engineering.Application.Services.RequestMachineryBills.Models.DeleteRequestMachineryBill;
using Engineering.Application.Services.RequestMachineryBills.Models.GetFilteredRequestMachineryBills;
using Engineering.Application.Services.RequestMachineryBills.Models.GetRequestMachineryBillById;
using Engineering.Application.Services.RequestMachineryBills.Models.GetSupplierDrivers;
using Engineering.Application.Services.RequestMachineryBills.Models.UpdateRequestMachineryBill;
using Financial.Application.AccountingDocuments.Models.PrintAccountingDocument;

namespace Engineering.Application.Services.RequestMachineryBills;

public partial interface IRequestMachineryBillLogic
{
    Task<Result<CreateRequestMachineryBillResponse?>> CreateRequestMachineryBill(CreateRequestMachineryBillRequest request, CT ct);
    Task<Result<UpdateRequestMachineryBillResponse?>> UpdateRequestMachineryBill(UpdateRequestMachineryBillRequest request, CT ct);
    Task<Result<DeleteRequestMachineryBillResponse?>> DeleteRequestMachineryBill(DeleteRequestMachineryBillRequest request, CT ct);
    Task<Result<GetFilteredRequestMachineryBillsResponse?>> GetFilteredRequestMachineryBills(GetFilteredRequestMachineryBillsRequest request, CT ct);
    Task<Result<GetSupplierDriversResponse?>> GetSupplierDrivers(GetSupplierDriversRequest request, CT ct);
    Task<Result<GetRequestMachineryBillByIdResponse?>> GetRequestMachineryBillById(GetRequestMachineryBillByIdRequest request, CT ct);
    Task<Result<RequestMachineryBillReportResponse?>> RequestMachineryBillReport(RequestMachineryBillReportRequest request, CT ct);
    Task<Result<GetOperationContractorsResponse?>> GetOperationContractors(GetOperationContractorsRequest request, CT ct);

}
