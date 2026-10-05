using Financial.Application.Abstractions.Interfaces;
using Financial.Application.WebServices.PdfMaker.Report.Models.RequestMachineryBillReport;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Financial.Application.WebServices.PdfMaker.Report.Commands.RequestMachineryBillReport;
public class RequestMachineryBillReportCommandHandler : ICommandHandler<RequestMachineryBillReportCommand, byte[]>
{
    private readonly ILogger<RequestMachineryBillReportCommandHandler> _logger;
    private readonly IPdfMakerService _service;

    public RequestMachineryBillReportCommandHandler(ILogger<RequestMachineryBillReportCommandHandler> logger,
        IPdfMakerService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<Result<byte[]?>> Handle(RequestMachineryBillReportCommand command,
        CT ct)
    {
        try
        {
            var request = command.Adapt<RequestMachineryBillReportPrintRequest>();
            var result = await _service.RequestMachineryBillReport(request, ct);
            if (result.IsFailure)
            {
                return Result.Failure<byte[]>(PdfMakerErrors.ProviderError(result.Error));
            }

            return result.Value!.Data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<byte[]>(SharedErrors.UnknownError);
        }
    }
}
