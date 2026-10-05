using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelEnums;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class RequestRewardExcels
{
    public static byte[] RequestRewardToExcel(
        ICollection<GetFilteredRequestRewardsExcelExporterResponseModel> result,
        List<RequestRewardsExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("درخواست اضافات و کسورات");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<RequestRewardsExcelEnum, int>();
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
                if (columns.ContainsKey(RequestRewardsExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(RequestRewardsExcelEnum.Id))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(RequestRewardsExcelEnum.StatusDescription))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(RequestRewardsExcelEnum.TypeDescription))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.TypeDescription]].Value = item.TypeDescription;
                if (columns.ContainsKey(RequestRewardsExcelEnum.ThirdParties))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.ThirdParties]].Value = item.ThirdParties;
                if (columns.ContainsKey(RequestRewardsExcelEnum.CostCenterId))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.CostCenterId]].Value = item.CostCenterId;
                if (columns.ContainsKey(RequestRewardsExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(RequestRewardsExcelEnum.ProjectId))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.ProjectId]].Value = item.ProjectId;
                if (columns.ContainsKey(RequestRewardsExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(RequestRewardsExcelEnum.ProjectOperationId))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.ProjectOperationId]].Value = item.ProjectOperationId;
                if (columns.ContainsKey(RequestRewardsExcelEnum.OperationInfoName))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.OperationInfoName]].Value = item.OperationInfoName;
                if (columns.ContainsKey(RequestRewardsExcelEnum.OperationInfoCode))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.OperationInfoCode]].Value = item.OperationInfoCode;
                if (columns.ContainsKey(RequestRewardsExcelEnum.ProjectOperationDetailId))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.ProjectOperationDetailId]].Value = item.ProjectOperationDetailId;
                if (columns.ContainsKey(RequestRewardsExcelEnum.PublicName))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.PublicName]].Value = item.PublicName;
                if (columns.ContainsKey(RequestRewardsExcelEnum.PublicCode))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.PublicCode]].Value = item.PublicCode;
                if (columns.ContainsKey(RequestRewardsExcelEnum.ContractCode))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.ContractCode]].Value = item.ContractCode;
                if (columns.ContainsKey(RequestRewardsExcelEnum.RegistrationDateShamsi))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.RegistrationDateShamsi]].Value = item.RegistrationDateShamsi;
                if (columns.ContainsKey(RequestRewardsExcelEnum.OfferedPrice))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.OfferedPrice]].Value = item.OfferedPrice;
                if (columns.ContainsKey(RequestRewardsExcelEnum.ConfirmedPrice))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.ConfirmedPrice]].Value = item.ConfirmedPrice;
                if (columns.ContainsKey(RequestRewardsExcelEnum.ManagerDescription))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.ManagerDescription]].Value = item.ManagerDescription;
                if (columns.ContainsKey(RequestRewardsExcelEnum.CurrencyId))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.CurrencyId]].Value = item.CurrencyId;
                if (columns.ContainsKey(RequestRewardsExcelEnum.Currency))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.Currency]].Value = item.Currency;
                if (columns.ContainsKey(RequestRewardsExcelEnum.Description))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.Description]].Value = item.Description;
                if (columns.ContainsKey(RequestRewardsExcelEnum.CompanyId))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.CompanyId]].Value = item.CompanyId;
                if (columns.ContainsKey(RequestRewardsExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[RequestRewardsExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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
                worksheet.Cells[1, 1, 1, 25],

                ExcelBorderStyle.Medium);

            worksheet.Cells[currentRow, 1].Value = RequestRewardsExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = RequestRewardsExcelEnum.Id.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = RequestRewardsExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = RequestRewardsExcelEnum.TypeDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = RequestRewardsExcelEnum.ThirdParties.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = RequestRewardsExcelEnum.CostCenterId.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = RequestRewardsExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = RequestRewardsExcelEnum.ProjectId.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = RequestRewardsExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = RequestRewardsExcelEnum.ProjectOperationId.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = RequestRewardsExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = RequestRewardsExcelEnum.OperationInfoCode.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = RequestRewardsExcelEnum.ProjectOperationDetailId.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = RequestRewardsExcelEnum.PublicName.GetEnumDescription();
            worksheet.Cells[currentRow, 15].Value = RequestRewardsExcelEnum.PublicCode.GetEnumDescription();
            worksheet.Cells[currentRow, 16].Value = RequestRewardsExcelEnum.ContractCode.GetEnumDescription();
            worksheet.Cells[currentRow, 17].Value = RequestRewardsExcelEnum.RegistrationDateShamsi.GetEnumDescription();
            worksheet.Cells[currentRow, 18].Value = RequestRewardsExcelEnum.OfferedPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 19].Value = RequestRewardsExcelEnum.ConfirmedPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 20].Value = RequestRewardsExcelEnum.ManagerDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 21].Value = RequestRewardsExcelEnum.CurrencyId.GetEnumDescription();
            worksheet.Cells[currentRow, 22].Value = RequestRewardsExcelEnum.Currency.GetEnumDescription();
            worksheet.Cells[currentRow, 23].Value = RequestRewardsExcelEnum.Description.GetEnumDescription();
            worksheet.Cells[currentRow, 24].Value = RequestRewardsExcelEnum.CompanyId.GetEnumDescription();
            worksheet.Cells[currentRow, 25].Value = RequestRewardsExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.Id;
                worksheet.Cells[currentRow, 3].Value = item.StatusDescription;
                worksheet.Cells[currentRow, 4].Value = item.TypeDescription;
                worksheet.Cells[currentRow, 5].Value = item.ThirdParties;
                worksheet.Cells[currentRow, 6].Value = item.CostCenterId;
                worksheet.Cells[currentRow, 7].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 8].Value = item.ProjectId;
                worksheet.Cells[currentRow, 9].Value = item.ProjectName;
                worksheet.Cells[currentRow, 10].Value = item.ProjectOperationId;
                worksheet.Cells[currentRow, 11].Value = item.OperationInfoName;
                worksheet.Cells[currentRow, 12].Value = item.OperationInfoCode;
                worksheet.Cells[currentRow, 13].Value = item.ProjectOperationDetailId;
                worksheet.Cells[currentRow, 14].Value = item.PublicName;
                worksheet.Cells[currentRow, 15].Value = item.PublicCode;
                worksheet.Cells[currentRow, 16].Value = item.ContractCode;
                worksheet.Cells[currentRow, 17].Value = item.RegistrationDateShamsi;
                worksheet.Cells[currentRow, 18].Value = item.OfferedPrice;
                worksheet.Cells[currentRow, 19].Value = item.ConfirmedPrice;
                worksheet.Cells[currentRow, 20].Value = item.ManagerDescription;
                worksheet.Cells[currentRow, 21].Value = item.CurrencyId;
                worksheet.Cells[currentRow, 22].Value = item.Currency;
                worksheet.Cells[currentRow, 23].Value = item.Description;
                worksheet.Cells[currentRow, 24].Value = item.CompanyId;
                worksheet.Cells[currentRow, 25].Value = item.CompanyNameFa;

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
}