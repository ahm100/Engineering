using Engineering.Application.Services.RequestMachineryBills.Models.GetRequestMachineryBillById;
using Engineering.Application.Services.RequestMachineryBills.Models.RequestMachineryBillModel;
using Financial.Application.Extensions;
using Financial.Application.WebServices.PdfMaker.Report.Models.RequestMachineryBillReport;

namespace Engineering.Application.Services.RequestMachineryBills;

public partial class RequestMachineryBillLogic : IRequestMachineryBillLogic
{
    private BillNumberPlatesModel? SetNumberPlates(string? numberPlates)
    {
        if (!string.IsNullOrEmpty(numberPlates))
        {
            var onlyNumbers = new String(numberPlates.Where(char.IsDigit).ToArray());
            var onlyLetters = new String(numberPlates.Where(char.IsLetter).ToArray());
            string part1 = onlyNumbers.Substring(0, 2); // "12"
            string part2 = onlyNumbers.Substring(2, 3); // "345"
            string part3 = onlyNumbers.Substring(5, 2); // "67"

            return new BillNumberPlatesModel()
            {
                Letter = onlyLetters,
                Part1 = part1,
                Part2 = part2,
                Part3 = part3,
            };
        }
        else
            return new BillNumberPlatesModel();
    }

    private (DateTime currentFrom, DateTime currentTo, TimeSpan operationWork) GetDates(DateTime current, DateTime fromDate, DateTime toDate)
    {
        var currentDateFrom = current.Date.Add(fromDate.TimeOfDay);
        var currentDateTo = current.Date.Add(toDate.TimeOfDay);

        TimeSpan totalWorkHours = TimeSpan.Zero;
        var subTime = currentDateTo.TimeOfDay - currentDateFrom.TimeOfDay;
        totalWorkHours = subTime;

        return (currentDateFrom, currentDateTo, totalWorkHours);
    }

    private async Task<Result<(List<RequestMachineryBillReportPrintData> Data, Dictionary<string, string?> Parameters)>> RequestMachineryBillReportData(List<long> ids, CT ct)
    {
        var listData = new List<GetRequestMachineryBillByIdResponse>();

        foreach (var id in ids)
        {
            var getRequestMachineryBillById = await GetRequestMachineryBillById(new GetRequestMachineryBillByIdRequest(id), ct);
            if (getRequestMachineryBillById.IsFailure || getRequestMachineryBillById.Value is null)
            {
                return Result.Failure<(List<RequestMachineryBillReportPrintData> Data, Dictionary<string, string?> Parameters)>(getRequestMachineryBillById.Error!);
            }

            var requestMachineryBill = getRequestMachineryBillById.Value;
            listData.Add(requestMachineryBill);
        }

        var result = new List<RequestMachineryBillReportPrintData>();
        foreach (var detail in listData)
        {
            result.Add(new()
            {
                BillDate = detail.BillDate.ToShortPersianDateString(),
                BillNumber = detail.BillNumber.ToString(),
                CompanyNameFa = detail.CompanyNameFa,
                Contractor = detail.Contractor,
                CostCenterName = detail.CostCenterName,
                Description = detail.Description,
                DriverFullName = detail.DriverFullName,
                MachineryGroupName = detail.MachineryGroupName,
                NumberPlates = detail.NumberPlates,
                ProjectName = detail.ProjectName,
                ProjectOperations = detail.ProjectOperations,
                RequestCreator = detail.RequestCreator,
                FromTime = detail.FromTime.ToString(),
                MachineryName = detail.MachineryName,
                QrCodeUrl = detail.QRCodeUrl,
                RequestNumber = detail.RequestNumber.ToString(),
                ToTime = detail.ToTime.ToString(),
                OperationDuration = detail.OperationDuration.ToString(),
                Supplier = detail.Supplier,
                TotalPrice = detail.TotalPrice?.ToString("N0"),
                UnitPrice = detail.UnitPrice?.ToString("N0"),
            });
        }

        var currentUser = WebServiceExtensions.GetUser(_currnetUserId, _mediator);

        var parameters = new Dictionary<string, string?>()
        {
            {"PrintDate", $"{DateTime.Now.ToShortPersianDateString()}"} ,
            {"PrintUser", $"{currentUser?.FullName}"} ,
        };

        return (result, parameters);
    }
}
