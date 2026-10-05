using Engineering.Application.Services.TelegramMessageHistorys;
using Engineering.Application.Services.TelegramMessageHistorys.Models.ExecuteSendTelegramMessage;
using Engineering.Application.Services.TelegramMessageHistorys.Models.GetsFiltered;

namespace Engineering.Api.Controllers.TelegramMessageHistorys;

[ApiController]
[Route("api/engineering/v1/TelegramMessageHistory")]
public class TelegramMessageHistoryController : ControllerBase
{
    private readonly ITelegramMessageHistoryLogic _logic;

    public TelegramMessageHistoryController(ITelegramMessageHistoryLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("ExecuteSendTelegramMessage")]
    [ResponseSchema<ExecuteSendTelegramMessageResponse>]
    public async Task<IResult> ExecuteSendTelegramMessage(
    [FromBody] ExecuteSendTelegramMessageRequest request,
    CT ct)
    {
        var result = await _logic.ExecuteSendTelegramMessage(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredTelegramMessageHistory")]
    [ResponseSchema<GetsFilteredTelegramMessageHistoryResponse>]
    public async Task<IResult> GetsFilteredTelegramMessageHistory(
        [FromBody] GetsFilteredTelegramMessageHistoryRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredTelegramMessageHistory(request, ct);
        return result.GetHttpResponse();
    }
}