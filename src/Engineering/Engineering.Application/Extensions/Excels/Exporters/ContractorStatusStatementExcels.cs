using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts.Enum;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts.Enum;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementExcelEnum;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class ContractorStatusStatementExcels
{
    public static byte[] ContractorStatusStatementToExcel(
        ICollection<GetsContractorStatusStatementExcelExporterModel> result,
        List<ContractorStatusStatementExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("صورت وضعیت");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<ContractorStatusStatementExcelEnum, int>();
            for (int counter = 0; counter < excelFilters.Count; counter++)
            {
                worksheet.Cells[1, counter + 1].Value = excelFilters[counter].GetEnumDescription();
                columns[excelFilters[counter]] = counter + 1;

                ExcelStyles.SetHeaderStyle(
                    worksheet.Cells[1, 1, 1, counter + 1],
                    ExcelBorderStyle.Medium);
            }

            foreach (var item in result)
            {
                currentRow++;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.Id))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.Project))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.Project]].Value = item.Project;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.Contractor))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.Contractor]].Value = item.Contractor;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.Nickname))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.Nickname]].Value = item.Nickname;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.Currency))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.Currency]].Value = item.Currency;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.Code))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.Code]].Value = item.Code;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.StartDate))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.StartDate]].Value = item.StartDate;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.EndDate))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.EndDate]].Value = item.EndDate;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.StatusDescription))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.FinalTotalAmount))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.FinalTotalAmount]].Value = item.FinalTotalAmount;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.TotalPercentageDoingJobWell))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.TotalPercentageDoingJobWell]].Value = item.TotalPercentageDoingJobWell;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.TotalDoingJobWellAmount))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.TotalDoingJobWellAmount]].Value = item.TotalDoingJobWellAmount;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.TotalAdvancePaymentAmount))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.TotalAdvancePaymentAmount]].Value = item.TotalAdvancePaymentAmount;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.TotalPercentageAdvancePayment))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.TotalPercentageAdvancePayment]].Value = item.TotalPercentageAdvancePayment;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.TotalDailyLatenessPenalty))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.TotalDailyLatenessPenalty]].Value = item.TotalDailyLatenessPenalty;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.TotalWorkDonePercent))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.TotalWorkDonePercent]].Value = item.TotalWorkDonePercent;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.TotalWorkDeliveryPercent))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.TotalWorkDeliveryPercent]].Value = item.TotalWorkDeliveryPercent;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.TotalWorkCompletionPercent))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.TotalWorkCompletionPercent]].Value = item.TotalWorkCompletionPercent;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.ProductsAmount))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.ProductsAmount]].Value = item.ProductsAmount;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.FinesAmount))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.FinesAmount]].Value = item.FinesAmount;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.RewardsAmount))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.RewardsAmount]].Value = item.RewardsAmount;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.ThirdPartiesAmount))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.ThirdPartiesAmount]].Value = item.ThirdPartiesAmount;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.PaymentedAmount))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.PaymentedAmount]].Value = item.PaymentedAmount;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.Description))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.Description]].Value = item.Description;
                if (columns.ContainsKey(ContractorStatusStatementExcelEnum.ManagmentDescription))
                    worksheet.Cells[currentRow, columns[ContractorStatusStatementExcelEnum.ManagmentDescription]].Value = item.ManagmentDescription;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }
        else
        {
            ExcelStyles.SetHeaderStyle(
                worksheet.Cells[1, 1, 1, 26],
                ExcelBorderStyle.Medium);

            worksheet.Cells[currentRow, 1].Value = ContractorStatusStatementExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = ContractorStatusStatementExcelEnum.Id.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = ContractorStatusStatementExcelEnum.Project.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = ContractorStatusStatementExcelEnum.Contractor.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = ContractorStatusStatementExcelEnum.Nickname.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = ContractorStatusStatementExcelEnum.Currency.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = ContractorStatusStatementExcelEnum.Code.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = ContractorStatusStatementExcelEnum.StartDate.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = ContractorStatusStatementExcelEnum.EndDate.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = ContractorStatusStatementExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = ContractorStatusStatementExcelEnum.FinalTotalAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = ContractorStatusStatementExcelEnum.TotalPercentageDoingJobWell.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = ContractorStatusStatementExcelEnum.TotalDoingJobWellAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = ContractorStatusStatementExcelEnum.TotalAdvancePaymentAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 15].Value = ContractorStatusStatementExcelEnum.TotalPercentageAdvancePayment.GetEnumDescription();
            worksheet.Cells[currentRow, 16].Value = ContractorStatusStatementExcelEnum.TotalDailyLatenessPenalty.GetEnumDescription();
            worksheet.Cells[currentRow, 17].Value = ContractorStatusStatementExcelEnum.TotalWorkDonePercent.GetEnumDescription();
            worksheet.Cells[currentRow, 18].Value = ContractorStatusStatementExcelEnum.TotalWorkDeliveryPercent.GetEnumDescription();
            worksheet.Cells[currentRow, 19].Value = ContractorStatusStatementExcelEnum.TotalWorkCompletionPercent.GetEnumDescription();
            worksheet.Cells[currentRow, 20].Value = ContractorStatusStatementExcelEnum.ProductsAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 21].Value = ContractorStatusStatementExcelEnum.FinesAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 22].Value = ContractorStatusStatementExcelEnum.RewardsAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 23].Value = ContractorStatusStatementExcelEnum.ThirdPartiesAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 24].Value = ContractorStatusStatementExcelEnum.PaymentedAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 25].Value = ContractorStatusStatementExcelEnum.Description.GetEnumDescription();
            worksheet.Cells[currentRow, 26].Value = ContractorStatusStatementExcelEnum.ManagmentDescription.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.Id;
                worksheet.Cells[currentRow, 3].Value = item.Project;
                worksheet.Cells[currentRow, 4].Value = item.Contractor;
                worksheet.Cells[currentRow, 5].Value = item.Nickname;
                worksheet.Cells[currentRow, 6].Value = item.Currency;
                worksheet.Cells[currentRow, 7].Value = item.Code;
                worksheet.Cells[currentRow, 8].Value = item.StartDate;
                worksheet.Cells[currentRow, 9].Value = item.EndDate;
                worksheet.Cells[currentRow, 10].Value = item.StatusDescription;
                worksheet.Cells[currentRow, 11].Value = item.FinalTotalAmount;
                worksheet.Cells[currentRow, 12].Value = item.TotalPercentageDoingJobWell;
                worksheet.Cells[currentRow, 13].Value = item.TotalDoingJobWellAmount;
                worksheet.Cells[currentRow, 14].Value = item.TotalAdvancePaymentAmount;
                worksheet.Cells[currentRow, 15].Value = item.TotalPercentageAdvancePayment;
                worksheet.Cells[currentRow, 16].Value = item.TotalDailyLatenessPenalty;
                worksheet.Cells[currentRow, 17].Value = item.TotalWorkDonePercent;
                worksheet.Cells[currentRow, 18].Value = item.TotalWorkDeliveryPercent;
                worksheet.Cells[currentRow, 19].Value = item.TotalWorkCompletionPercent;
                worksheet.Cells[currentRow, 20].Value = item.ProductsAmount;
                worksheet.Cells[currentRow, 21].Value = item.FinesAmount;
                worksheet.Cells[currentRow, 22].Value = item.RewardsAmount;
                worksheet.Cells[currentRow, 23].Value = item.ThirdPartiesAmount;
                worksheet.Cells[currentRow, 24].Value = item.PaymentedAmount;
                worksheet.Cells[currentRow, 25].Value = item.Description;
                worksheet.Cells[currentRow, 26].Value = item.ManagmentDescription;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static byte[] GetCStatementFContractsToExcel(
        List<GetCStatementFContractsModel>? data,
        List<GetCStatementFContractsEnum>? cStatementFilters = null,
        List<GetCStatementFContractsDailiesEnum>? cStatementDailyFilters = null)
    {
        using var workbook = new ExcelPackage();
        var dataList = data ?? new List<GetCStatementFContractsModel>();

        {
            var worksheet = ExcelHelpers.CreateWorksheet(workbook, "قراردادهای پیمانکار (FixCCs)");

            var newFilters = cStatementFilters != null && cStatementFilters.Count > 0
                ? cStatementFilters
                : Enum.GetValues(typeof(GetCStatementFContractsEnum))
                      .Cast<GetCStatementFContractsEnum>()
                      .ToList();

            var defaultHeaders = ExcelHelpers.GetDefaultHeaders<GetCStatementFContractsEnum>();
            var columns = ExcelHelpers.SetupHeaders<GetCStatementFContractsEnum>(worksheet, newFilters, defaultHeaders);

            var currentRow = 1;
            foreach (var item in dataList)
            {
                currentRow++;
                ExcelHelpers.FillRow<GetCStatementFContractsEnum, GetCStatementFContractsModel>(
                    worksheet, item, currentRow, columns);
            }
        }

        {
            var worksheet = ExcelHelpers.CreateWorksheet(workbook, "خدمات روزانه (FixCCs)");

            var newFilters = cStatementDailyFilters != null && cStatementDailyFilters.Count > 0
                ? cStatementDailyFilters
                : Enum.GetValues(typeof(GetCStatementFContractsDailiesEnum))
                      .Cast<GetCStatementFContractsDailiesEnum>()
                      .ToList();

            var defaultHeaders = ExcelHelpers.GetDefaultHeaders<GetCStatementFContractsDailiesEnum>();
            var columns = ExcelHelpers.SetupHeaders<GetCStatementFContractsDailiesEnum>(worksheet, newFilters, defaultHeaders);

            var currentRow = 1;
            foreach (var parent in dataList)
            {
                if (parent.DailyServices == null || parent.DailyServices.Count == 0)
                    continue;

                foreach (var item in parent.DailyServices)
                {
                    currentRow++;
                    ExcelHelpers.FillRow<GetCStatementFContractsDailiesEnum, GetCStatementFContractsDailiesModel>(
                        worksheet, item, currentRow, columns);
                }
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static byte[] GetCStatementSContractsToExcel(
        List<GetCStatementSContractsModel>? data,
        List<GetCStatementSContractsEnum>? cStatementFilters = null,
        List<GetCStatementSContractsDailiesEnum>? dailyFilters = null)
    {
        using var workbook = new ExcelPackage();
        var dataList = data ?? new List<GetCStatementSContractsModel>();

        {
            var worksheet = ExcelHelpers.CreateWorksheet(workbook, "قراردادهای خدمات پیمانکار");

            var newFilters = cStatementFilters != null && cStatementFilters.Count > 0
                ? cStatementFilters
                : Enum.GetValues(typeof(GetCStatementSContractsEnum))
                      .Cast<GetCStatementSContractsEnum>()
                      .ToList();

            var defaultHeaders = ExcelHelpers.GetDefaultHeaders<GetCStatementSContractsEnum>();
            var columns = ExcelHelpers.SetupHeaders<GetCStatementSContractsEnum>(worksheet, newFilters, defaultHeaders);

            var currentRow = 1;
            foreach (var item in dataList)
            {
                currentRow++;
                ExcelHelpers.FillRow<GetCStatementSContractsEnum, GetCStatementSContractsModel>(
                    worksheet, item, currentRow, columns);
            }
        }

        {
            var worksheet = ExcelHelpers.CreateWorksheet(workbook, "خدمات روزانه (ServiceCCs)");

            var newFilters = dailyFilters != null && dailyFilters.Count > 0
                ? dailyFilters
                : Enum.GetValues(typeof(GetCStatementSContractsDailiesEnum))
                      .Cast<GetCStatementSContractsDailiesEnum>()
                      .ToList();

            var defaultHeaders = ExcelHelpers.GetDefaultHeaders<GetCStatementSContractsDailiesEnum>();
            var columns = ExcelHelpers.SetupHeaders<GetCStatementSContractsDailiesEnum>(worksheet, newFilters, defaultHeaders);

            var currentRow = 1;
            foreach (var parent in dataList)
            {
                if (parent.DailyServices == null || parent.DailyServices.Count == 0)
                    continue;

                foreach (var item in parent.DailyServices)
                {
                    currentRow++;
                    ExcelHelpers.FillRow<GetCStatementSContractsDailiesEnum, GetCStatementSContractsDailiesModel>(
                        worksheet, item, currentRow, columns);
                }
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

}