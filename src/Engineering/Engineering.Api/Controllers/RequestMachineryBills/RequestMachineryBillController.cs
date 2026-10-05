using Engineering.Application.Services.RequestMachineries.Models.GetOperationContractors;
using Engineering.Application.Services.RequestMachineryBills;
using Engineering.Application.Services.RequestMachineryBills.Models.CreateRequestMachineryBill;
using Engineering.Application.Services.RequestMachineryBills.Models.DeleteRequestMachineryBill;
using Engineering.Application.Services.RequestMachineryBills.Models.GetFilteredRequestMachineryBills;
using Engineering.Application.Services.RequestMachineryBills.Models.GetRequestMachineryBillById;
using Engineering.Application.Services.RequestMachineryBills.Models.GetSupplierDrivers;
using Engineering.Application.Services.RequestMachineryBills.Models.UpdateRequestMachineryBill;

namespace Engineering.Api.Controllers.RequestMachineryBills;

[ApiController]
[Route("api/engineering/v1/RequestMachineryBill")]
public class RequestMachineryBillController : ControllerBase
{
    private readonly IRequestMachineryBillLogic _logic;

    public RequestMachineryBillController(IRequestMachineryBillLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateRequestMachineryBill")]
    [ResponseSchema<CreateRequestMachineryBillResponse>]
    public async Task<IResult> CreateRequestMachineryBill(
    [FromBody] CreateRequestMachineryBillRequest request,
    CT ct)
    {
        var result = await _logic.CreateRequestMachineryBill(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredRequestMachineryBills")]
    [ResponseSchema<GetFilteredRequestMachineryBillsResponse>]
    public async Task<IResult> GetFilteredRequestMachineryBills(
        [FromBody] GetFilteredRequestMachineryBillsRequest request,
        CT ct)
    {
        var result = await _logic.GetFilteredRequestMachineryBills(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestMachineryBillById")]
    [ResponseSchema<GetRequestMachineryBillByIdResponse>]
    public async Task<IResult> GetRequestMachineryBillById(
        [FromQuery] GetRequestMachineryBillByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetRequestMachineryBillById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetSupplierDrivers")]
    [ResponseSchema<GetSupplierDriversResponse>]
    public async Task<IResult> GetSupplierDrivers(
        [FromQuery] GetSupplierDriversRequest request,
        CT ct)
    {
        var result = await _logic.GetSupplierDrivers(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetOperationContractors")]
    [ResponseSchema<GetOperationContractorsResponse>]
    public async Task<IResult> GetOperationContractors(
        [FromQuery] GetOperationContractorsRequest request,
        CT ct)
    {
        var result = await _logic.GetOperationContractors(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteRequestMachineryBill")]
    [ResponseSchema<DeleteRequestMachineryBillResponse>]
    public async Task<IResult> DeleteRequestMachineryBill(
        [FromQuery] DeleteRequestMachineryBillRequest request,
        CT ct)
    {
        var result = await _logic.DeleteRequestMachineryBill(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateRequestMachineryBill")]
    [ResponseSchema<UpdateRequestMachineryBillResponse>]
    public async Task<IResult> UpdateRequestMachineryBill(
        [FromBody] UpdateRequestMachineryBillRequest request,
        CT ct)
    {
        var result = await _logic.UpdateRequestMachineryBill(request, ct);
        return result.GetHttpResponse();
    }
}