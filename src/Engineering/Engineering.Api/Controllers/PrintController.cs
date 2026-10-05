using Financial.Application.AccountingDocuments.Models.PrintAccountingDocument;
using MediatR;

namespace Engineering.Api.Controllers;


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
[Route("/api/engineering/v1/[controller]/[action]")]
[ApiController]
public class PrintController : ControllerBase
{
    private readonly IMediator _mediator;

    public PrintController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<RequestMachineryBillReportResponse>), StatusCodes.Status200OK)]
    public async Task<IResult> RequestMachineryBillReport([FromQuery] List<long> ids, CT ct = default)
    {
        return await _mediator.Send(new RequestMachineryBillReportRequest(ids), ct);
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member