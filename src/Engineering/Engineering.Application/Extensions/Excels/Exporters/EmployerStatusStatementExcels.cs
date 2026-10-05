using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementExcelExporter;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementExcelEnums;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementExcelExporter;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationDetailExcelEnums;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementProjectOperationExcelEnum;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class EmployerStatusStatementExcels
{
    public static byte[] EmployerStatusStatementsToExcel(
        ICollection<GetsEmployerStatusStatementExcelExporterResponseModel> result,
        List<EmployerStatusStatementExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("صورت وضعیت کارفرما");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<EmployerStatusStatementExcelEnum, int>();
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
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.Id))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.SendStatusTypeTitle))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.SendStatusTypeTitle]].Value = item.SendStatusTypeTitle;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.StartDate))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.StartDate]].Value = item.StartDate;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.EndDate))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.EndDate]].Value = item.EndDate;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.StatusStatementCode))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.StatusStatementCode]].Value = item.StatusStatementCode;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.EmployerName))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.EmployerName]].Value = item.EmployerName;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.EmployerContractCode))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.EmployerContractCode]].Value = item.EmployerContractCode;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.PercentageOfWorkDone))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.PercentageOfWorkDone]].Value = item.PercentageOfWorkDone;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.CalculatedAmount))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.CalculatedAmount]].Value = item.CalculatedAmount;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.CurrencyName))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.CurrencyName]].Value = item.CurrencyName;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.Created))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.Created]].Value = item.Created;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.CompanyId))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.CompanyId]].Value = item.CompanyId;
                if (columns.ContainsKey(EmployerStatusStatementExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[EmployerStatusStatementExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;

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
                worksheet.Cells[1, 1, 1, 16],

                ExcelBorderStyle.Medium);

            worksheet.Cells[currentRow, 1].Value = EmployerStatusStatementExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = EmployerStatusStatementExcelEnum.Id.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = EmployerStatusStatementExcelEnum.SendStatusTypeTitle.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = EmployerStatusStatementExcelEnum.StartDate.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = EmployerStatusStatementExcelEnum.EndDate.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = EmployerStatusStatementExcelEnum.StatusStatementCode.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = EmployerStatusStatementExcelEnum.EmployerName.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = EmployerStatusStatementExcelEnum.EmployerContractCode.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = EmployerStatusStatementExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = EmployerStatusStatementExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = EmployerStatusStatementExcelEnum.PercentageOfWorkDone.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = EmployerStatusStatementExcelEnum.CalculatedAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = EmployerStatusStatementExcelEnum.CurrencyName.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = EmployerStatusStatementExcelEnum.Created.GetEnumDescription();
            worksheet.Cells[currentRow, 15].Value = EmployerStatusStatementExcelEnum.CompanyId.GetEnumDescription();
            worksheet.Cells[currentRow, 16].Value = EmployerStatusStatementExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.Id;
                worksheet.Cells[currentRow, 3].Value = item.SendStatusTypeTitle;
                worksheet.Cells[currentRow, 4].Value = item.StartDate;
                worksheet.Cells[currentRow, 5].Value = item.EndDate;
                worksheet.Cells[currentRow, 6].Value = item.StatusStatementCode;
                worksheet.Cells[currentRow, 7].Value = item.EmployerName;
                worksheet.Cells[currentRow, 8].Value = item.EmployerContractCode;
                worksheet.Cells[currentRow, 9].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 10].Value = item.ProjectName;
                worksheet.Cells[currentRow, 11].Value = item.PercentageOfWorkDone;
                worksheet.Cells[currentRow, 12].Value = item.CalculatedAmount;
                worksheet.Cells[currentRow, 13].Value = item.CurrencyName;
                worksheet.Cells[currentRow, 14].Value = item.Created;
                worksheet.Cells[currentRow, 15].Value = item.CompanyId;
                worksheet.Cells[currentRow, 16].Value = item.CompanyNameFa;

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

    public static byte[] EmployerStatusStatementToExcel(
        GetEmployerStatusStatementExcelExporterResponseModel employerStatusStatement,
        ICollection<EmployerStatusStatementProjectOperations> projectOperations,
        ICollection<EmployerStatusStatementProjectOperationDetails> projectOperationDetails)
    {
        using var workbook = new ExcelPackage();

        #region صورت وضعیت کارفرما
        var worksheet1 = workbook.Workbook.Worksheets.Add("صورت وضعیت کارفرما");
        var currentRow1 = 1;
        worksheet1.View.RightToLeft = true;

        ExcelStyles.SetHeaderStyle(
            worksheet1.Cells[1, 1, 1, 16],
            ExcelBorderStyle.Medium);

        worksheet1.Cells[currentRow1, 1].Value = EmployerStatusStatementExcelEnum.Row.GetEnumDescription();
        worksheet1.Cells[currentRow1, 2].Value = EmployerStatusStatementExcelEnum.Id.GetEnumDescription();
        worksheet1.Cells[currentRow1, 3].Value = EmployerStatusStatementExcelEnum.SendStatusTypeTitle.GetEnumDescription();
        worksheet1.Cells[currentRow1, 4].Value = EmployerStatusStatementExcelEnum.StartDate.GetEnumDescription();
        worksheet1.Cells[currentRow1, 5].Value = EmployerStatusStatementExcelEnum.EndDate.GetEnumDescription();
        worksheet1.Cells[currentRow1, 6].Value = EmployerStatusStatementExcelEnum.StatusStatementCode.GetEnumDescription();
        worksheet1.Cells[currentRow1, 7].Value = EmployerStatusStatementExcelEnum.EmployerName.GetEnumDescription();
        worksheet1.Cells[currentRow1, 8].Value = EmployerStatusStatementExcelEnum.EmployerContractCode.GetEnumDescription();
        worksheet1.Cells[currentRow1, 9].Value = EmployerStatusStatementExcelEnum.CostCenterName.GetEnumDescription();
        worksheet1.Cells[currentRow1, 10].Value = EmployerStatusStatementExcelEnum.ProjectName.GetEnumDescription();
        worksheet1.Cells[currentRow1, 11].Value = EmployerStatusStatementExcelEnum.PercentageOfWorkDone.GetEnumDescription();
        worksheet1.Cells[currentRow1, 12].Value = EmployerStatusStatementExcelEnum.CalculatedAmount.GetEnumDescription();
        worksheet1.Cells[currentRow1, 13].Value = EmployerStatusStatementExcelEnum.CurrencyName.GetEnumDescription();
        worksheet1.Cells[currentRow1, 14].Value = EmployerStatusStatementExcelEnum.Created.GetEnumDescription();
        worksheet1.Cells[currentRow1, 15].Value = EmployerStatusStatementExcelEnum.CompanyId.GetEnumDescription();
        worksheet1.Cells[currentRow1, 16].Value = EmployerStatusStatementExcelEnum.CompanyNameFa.GetEnumDescription();

        currentRow1++;
        worksheet1.Cells[currentRow1, 1].Value = currentRow1 - 1;
        worksheet1.Cells[currentRow1, 2].Value = employerStatusStatement.Id;
        worksheet1.Cells[currentRow1, 3].Value = employerStatusStatement.SendStatusTypeTitle;
        worksheet1.Cells[currentRow1, 4].Value = employerStatusStatement.StartDate;
        worksheet1.Cells[currentRow1, 5].Value = employerStatusStatement.EndDate;
        worksheet1.Cells[currentRow1, 6].Value = employerStatusStatement.StatusStatementCode;
        worksheet1.Cells[currentRow1, 7].Value = employerStatusStatement.EmployerName;
        worksheet1.Cells[currentRow1, 8].Value = employerStatusStatement.EmployerContractCode;
        worksheet1.Cells[currentRow1, 9].Value = employerStatusStatement.CostCenterName;
        worksheet1.Cells[currentRow1, 10].Value = employerStatusStatement.ProjectName;
        worksheet1.Cells[currentRow1, 11].Value = employerStatusStatement.PercentageOfWorkDone;
        worksheet1.Cells[currentRow1, 12].Value = employerStatusStatement.CalculatedAmount;
        worksheet1.Cells[currentRow1, 13].Value = employerStatusStatement.CurrencyName;
        worksheet1.Cells[currentRow1, 14].Value = employerStatusStatement.Created;
        worksheet1.Cells[currentRow1, 15].Value = employerStatusStatement.CompanyId;
        worksheet1.Cells[currentRow1, 16].Value = employerStatusStatement.CompanyNameFa;

        ExcelStyles.SetCellStyle(
            worksheet1,
            currentRow1 - 1,
            currentRow1,
            worksheet1.Dimension.Start.Column,
            worksheet1.Dimension.End.Column);
        #endregion

        #region عملیات های پروژه
        var worksheet2 = workbook.Workbook.Worksheets.Add("عملیات های پروژه");
        var currentRow2 = 1;
        worksheet2.View.RightToLeft = true;

        if (projectOperations.Count > 0)
        {
            ExcelStyles.SetHeaderStyle(
                worksheet2.Cells[1, 1, 1, 12],

                ExcelBorderStyle.Medium);

            worksheet2.Cells[currentRow2, 1].Value = EmployerStatusStatementProjectOperationExcelEnum.Row.GetEnumDescription();
            worksheet2.Cells[currentRow2, 2].Value = EmployerStatusStatementProjectOperationExcelEnum.Id.GetEnumDescription();
            worksheet2.Cells[currentRow2, 3].Value = EmployerStatusStatementProjectOperationExcelEnum.ProjectOperationId.GetEnumDescription();
            worksheet2.Cells[currentRow2, 4].Value = EmployerStatusStatementProjectOperationExcelEnum.ProjectOperationStatusTitle.GetEnumDescription();
            worksheet2.Cells[currentRow2, 5].Value = EmployerStatusStatementProjectOperationExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet2.Cells[currentRow2, 6].Value = EmployerStatusStatementProjectOperationExcelEnum.TotalWorkVolume.GetEnumDescription();
            worksheet2.Cells[currentRow2, 7].Value = EmployerStatusStatementProjectOperationExcelEnum.DoneWorkVolume.GetEnumDescription();
            worksheet2.Cells[currentRow2, 8].Value = EmployerStatusStatementProjectOperationExcelEnum.StatusStatementWorkVolume.GetEnumDescription();
            worksheet2.Cells[currentRow2, 9].Value = EmployerStatusStatementProjectOperationExcelEnum.UnitOfMeasurementName.GetEnumDescription();
            worksheet2.Cells[currentRow2, 10].Value = EmployerStatusStatementProjectOperationExcelEnum.DonePercentage.GetEnumDescription();
            worksheet2.Cells[currentRow2, 11].Value = EmployerStatusStatementProjectOperationExcelEnum.TotalPercentage.GetEnumDescription();
            worksheet2.Cells[currentRow2, 12].Value = EmployerStatusStatementProjectOperationExcelEnum.CalculatedAmount.GetEnumDescription();

            foreach (var item in projectOperations)
            {
                currentRow2++;
                worksheet2.Cells[currentRow2, 1].Value = currentRow2 - 1;
                worksheet2.Cells[currentRow2, 2].Value = item.Id;
                worksheet2.Cells[currentRow2, 3].Value = item.ProjectOperationId;
                worksheet2.Cells[currentRow2, 4].Value = item.ProjectOperationStatusTitle;
                worksheet2.Cells[currentRow2, 5].Value = item.OperationInfoName;
                worksheet2.Cells[currentRow2, 6].Value = item.TotalWorkVolume;
                worksheet2.Cells[currentRow2, 7].Value = item.DoneWorkVolume;
                worksheet2.Cells[currentRow2, 8].Value = item.StatusStatementWorkVolume;
                worksheet2.Cells[currentRow2, 9].Value = item.UnitOfMeasurementName;
                worksheet2.Cells[currentRow2, 10].Value = item.DonePercentage;
                worksheet2.Cells[currentRow2, 11].Value = item.TotalPercentage;
                worksheet2.Cells[currentRow2, 12].Value = item.CalculatedAmount;

                ExcelStyles.SetCellStyle(
                    worksheet2,
                    currentRow2 - 1,
                    currentRow2,
                    worksheet2.Dimension.Start.Column,
                    worksheet2.Dimension.End.Column);
            }
        }
        #endregion

        #region ریزمتره ها
        var worksheet3 = workbook.Workbook.Worksheets.Add("ریزمتره ها");
        var currentRow3 = 1;
        worksheet3.View.RightToLeft = true;

        if (projectOperationDetails.Count > 0)
        {
            ExcelStyles.SetHeaderStyle(
                worksheet3.Cells[1, 1, 1, 13],

                ExcelBorderStyle.Medium);

            worksheet3.Cells[currentRow3, 1].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.Row.GetEnumDescription();
            worksheet3.Cells[currentRow3, 2].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.Id.GetEnumDescription();
            worksheet3.Cells[currentRow3, 3].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.ProjectOperationId.GetEnumDescription();
            worksheet3.Cells[currentRow3, 4].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.ProjectOperationDetailId.GetEnumDescription();
            worksheet3.Cells[currentRow3, 5].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.ProjectOperationDetailStatusTitle.GetEnumDescription();
            worksheet3.Cells[currentRow3, 6].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.PrivateName.GetEnumDescription();
            worksheet3.Cells[currentRow3, 7].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.PrivateCode.GetEnumDescription();
            worksheet3.Cells[currentRow3, 8].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.TotalWorkVolume.GetEnumDescription();
            worksheet3.Cells[currentRow3, 9].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.DoneWorkVolume.GetEnumDescription();
            worksheet3.Cells[currentRow3, 10].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.StatusStatementWorkVolume.GetEnumDescription();
            worksheet3.Cells[currentRow3, 11].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.DonePercentage.GetEnumDescription();
            worksheet3.Cells[currentRow3, 12].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.TotalPercentage.GetEnumDescription();
            worksheet3.Cells[currentRow3, 13].Value = EmployerStatusStatementProjectOperationDetailExcelEnum.CalculatedAmount.GetEnumDescription();

            foreach (var item in projectOperationDetails)
            {
                currentRow3++;
                worksheet3.Cells[currentRow3, 1].Value = currentRow3 - 1;
                worksheet3.Cells[currentRow3, 2].Value = item.Id;
                worksheet3.Cells[currentRow3, 3].Value = item.ProjectOperationId;
                worksheet3.Cells[currentRow3, 4].Value = item.ProjectOperationDetailId;
                worksheet3.Cells[currentRow3, 5].Value = item.ProjectOperationDetailStatusTitle;
                worksheet3.Cells[currentRow3, 6].Value = item.PrivateName;
                worksheet3.Cells[currentRow3, 7].Value = item.PrivateCode;
                worksheet3.Cells[currentRow3, 8].Value = item.TotalWorkVolume;
                worksheet3.Cells[currentRow3, 9].Value = item.DoneWorkVolume;
                worksheet3.Cells[currentRow3, 10].Value = item.StatusStatementWorkVolume;
                worksheet3.Cells[currentRow3, 11].Value = item.DonePercentage;
                worksheet3.Cells[currentRow3, 12].Value = item.TotalPercentage;
                worksheet3.Cells[currentRow3, 13].Value = item.CalculatedAmount;

                ExcelStyles.SetCellStyle(
                    worksheet3,
                    currentRow3 - 1,
                    currentRow3,
                    worksheet3.Dimension.Start.Column,
                    worksheet3.Dimension.End.Column);
            }
        }
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

}