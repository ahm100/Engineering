using Engineering.Application.Services.RequestContractorInquiries;
using Engineering.Application.Services.RequestContractorInquiries.Models.CreateRequestContractorInquiry;
using Engineering.Application.Services.RequestContractorInquiries.Models.GetRequestContractorInquiryByRequestId;
using Engineering.Application.Services.RequestContractorInquiries.Models.UpdateRequestContractorInquiry;
using Engineering.Application.Services.RequestContractors.Models.DeleteRequestContractorInquiry;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorInquiryById;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorType;

namespace Engineering.Api.Controllers.RequestContractorInquirys;

[ApiController]
[Route("api/engineering/v1/RequestContractorInquiry")]
public class RequestContractorInquiryController : ControllerBase
{
    private readonly IRequestContractorInquiryLogic _logic;

    public RequestContractorInquiryController(IRequestContractorInquiryLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateRequestContractorInquiry")]
    [ResponseSchema<CreateRequestContractorInquiryResponse>]
    public async Task<IResult> CreateRequestContractorInquiry([FromBody] CreateRequestContractorInquiryRequest request, CT ct)
    {
        var result = await _logic.CreateRequestContractorInquiry(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateRequestContractorInquiry")]
    [ResponseSchema<UpdateRequestContractorInquiryResponse>]
    public async Task<IResult> UpdateRequestContractorInquiry([FromBody] UpdateRequestContractorInquiryRequest request, CT ct)
    {
        var result = await _logic.UpdateRequestContractorInquiry(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteRequestContractorInquiry")]
    [ResponseSchema<DeleteRequestContractorInquiryResponse>]
    public async Task<IResult> DeleteRequestContractorInquiry([FromBody] DeleteRequestContractorInquiryRequest request, CT ct)
    {
        var result = await _logic.DeleteRequestContractorInquiry(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestContractorInquiryById")]
    [ResponseSchema<GetRequestContractorInquiryByIdResponse>]
    public async Task<IResult> GetRequestContractorInquiryById([FromQuery] GetRequestContractorInquiryByIdRequest request, CT ct)
    {
        var result = await _logic.GetRequestContractorInquiryById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetRequestContractorInquiryByRequestId")]
    [ResponseSchema<GetRequestContractorInquiryByRequestIdResponse>]
    public async Task<IResult> GetRequestContractorInquiryByRequestId([FromBody] GetRequestContractorInquiryByRequestIdRequest request, CT ct)
    {
        var result = await _logic.GetRequestContractorInquiryByRequestId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetRequestContractorType")]
    [ResponseSchema<GetRequestContractorTypeResponse>]
    public async Task<IResult> GetRequestContractorType([FromQuery] GetRequestContractorTypeRequest request, CT ct)
    {
        var result = await _logic.GetRequestContractorType(request, ct);
        return result.GetHttpResponse();
    }
}