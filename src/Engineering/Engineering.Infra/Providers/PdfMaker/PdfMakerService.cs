using Financial.Application.Abstractions.Interfaces;
using Financial.Application.WebServices.PdfMaker.Report.Models.RequestMachineryBillReport;
using Financial.Infra.Providers.PdfMaker;
using Gita.Backend.Shared.Domain.Errors;
using Gita.Backend.Shared.Domain.Errors.WebServices;

namespace Engineering.Infra.Providers.PdfMaker;
public class PdfMakerService : IPdfMakerService
{
    private readonly IPdfMakerProvider _provider;

    public PdfMakerService(IPdfMakerProvider provider)
    {
        _provider = provider;
    }

    public async Task<Result<RequestMachineryBillReportPrintResponseModel?>> RequestMachineryBillReport(RequestMachineryBillReportPrintRequest request, CT ct)
    {
        var result = await _provider.RequestMachineryBillReport(request, ct);
        if (result is null || !result.IsSuccessStatusCode)
            return Result.Failure<RequestMachineryBillReportPrintResponseModel>(PdfMakerErrors.ProviderError(SharedErrors.UnknownError));

        byte[] buffer = new byte[16 * 1024];
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            while ((read = result.Content.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return new RequestMachineryBillReportPrintResponseModel(ms.ToArray());
        }
    }
}
