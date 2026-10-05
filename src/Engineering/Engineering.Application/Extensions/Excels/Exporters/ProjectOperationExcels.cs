using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReportingExcelEnum;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReportingExcelExporter;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReportingExcelEnum;
using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReportingExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class ProjectOperationExcels
{
    public static byte[] ProjectOperationReportingToExcel(
        ICollection<GetsProjectOperationReportingExcelExporterModel> result,
        List<ProjectOperationExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("گزارش شرح عملیات ها");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<ProjectOperationExcelEnum, int>();
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
                if (columns.ContainsKey(ProjectOperationExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(ProjectOperationExcelEnum.Id))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(ProjectOperationExcelEnum.OperationInfoId))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.OperationInfoId]].Value = item.OperationInfoId;
                if (columns.ContainsKey(ProjectOperationExcelEnum.OperationInfoName))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.OperationInfoName]].Value = item.OperationInfoName;
                if (columns.ContainsKey(ProjectOperationExcelEnum.OperationInfoCode))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.OperationInfoCode]].Value = item.OperationInfoCode;
                if (columns.ContainsKey(ProjectOperationExcelEnum.OperationInfoMeasurementId))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.OperationInfoMeasurementId]].Value = item.OperationInfoMeasurementId;
                if (columns.ContainsKey(ProjectOperationExcelEnum.OperationInfoMeasurementName))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.OperationInfoMeasurementName]].Value = item.OperationInfoMeasurementName;
                if (columns.ContainsKey(ProjectOperationExcelEnum.ProjectId))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.ProjectId]].Value = item.ProjectId;
                if (columns.ContainsKey(ProjectOperationExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(ProjectOperationExcelEnum.ProjectCode))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.ProjectCode]].Value = item.ProjectCode;
                if (columns.ContainsKey(ProjectOperationExcelEnum.CostCenterId))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.CostCenterId]].Value = item.CostCenterId;
                if (columns.ContainsKey(ProjectOperationExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(ProjectOperationExcelEnum.CostCenterCode))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.CostCenterCode]].Value = item.CostCenterCode;
                if (columns.ContainsKey(ProjectOperationExcelEnum.CategoryName))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.CategoryName]].Value = item.CategoryName;
                if (columns.ContainsKey(ProjectOperationExcelEnum.BranchName))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.BranchName]].Value = item.BranchName;
                if (columns.ContainsKey(ProjectOperationExcelEnum.SeasonName))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.SeasonName]].Value = item.SeasonName;
                if (columns.ContainsKey(ProjectOperationExcelEnum.Contractors))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.Contractors]].Value = item.Contractors;
                if (columns.ContainsKey(ProjectOperationExcelEnum.ContractorsNickName))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.ContractorsNickName]].Value = item.ContractorsNickName;
                if (columns.ContainsKey(ProjectOperationExcelEnum.ImplementationAssistants))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.ImplementationAssistants]].Value = item.ImplementationAssistants;
                if (columns.ContainsKey(ProjectOperationExcelEnum.ImplementationAssistantsNickName))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.ImplementationAssistantsNickName]].Value = item.ImplementationAssistantsNickName;
                if (columns.ContainsKey(ProjectOperationExcelEnum.TechnicalAssistants))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.TechnicalAssistants]].Value = item.TechnicalAssistants;
                if (columns.ContainsKey(ProjectOperationExcelEnum.TechnicalAssistantsNickName))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.TechnicalAssistantsNickName]].Value = item.TechnicalAssistantsNickName;
                if (columns.ContainsKey(ProjectOperationExcelEnum.DailyProjectOperationCreators))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.DailyProjectOperationCreators]].Value = item.DailyProjectOperationCreators;
                if (columns.ContainsKey(ProjectOperationExcelEnum.StartDate))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.StartDate]].Value = item.StartDate;
                if (columns.ContainsKey(ProjectOperationExcelEnum.EndDate))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.EndDate]].Value = item.EndDate;
                if (columns.ContainsKey(ProjectOperationExcelEnum.MeasurementId))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.MeasurementId]].Value = item.MeasurementId;
                if (columns.ContainsKey(ProjectOperationExcelEnum.MeasurementName))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.MeasurementName]].Value = item.MeasurementName;
                if (columns.ContainsKey(ProjectOperationExcelEnum.TolerancePercentage))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.TolerancePercentage]].Value = item.TolerancePercentage;
                if (columns.ContainsKey(ProjectOperationExcelEnum.Price))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.Price]].Value = item.Price;
                if (columns.ContainsKey(ProjectOperationExcelEnum.Priority))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.Priority]].Value = item.Priority;
                if (columns.ContainsKey(ProjectOperationExcelEnum.StatusDescription))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(ProjectOperationExcelEnum.Workload))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.Workload]].Value = item.Workload;
                if (columns.ContainsKey(ProjectOperationExcelEnum.DoneWorkload))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.DoneWorkload]].Value = item.DoneWorkload;
                if (columns.ContainsKey(ProjectOperationExcelEnum.RemaindedWorkload))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.RemaindedWorkload]].Value = item.RemaindedWorkload;
                if (columns.ContainsKey(ProjectOperationExcelEnum.CreatorId))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.CreatorId]].Value = item.CreatorId;
                if (columns.ContainsKey(ProjectOperationExcelEnum.CreatorName))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.CreatorName]].Value = item.CreatorName;
                if (columns.ContainsKey(ProjectOperationExcelEnum.Created))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.Created]].Value = item.Created;
                if (columns.ContainsKey(ProjectOperationExcelEnum.Description))
                    worksheet.Cells[currentRow, columns[ProjectOperationExcelEnum.Description]].Value = item.Description;

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
                worksheet.Cells[1, 1, 1, 38],

                ExcelBorderStyle.Medium);

            worksheet.Cells[currentRow, 1].Value = ProjectOperationExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = ProjectOperationExcelEnum.Id.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = ProjectOperationExcelEnum.OperationInfoId.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = ProjectOperationExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = ProjectOperationExcelEnum.OperationInfoCode.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = ProjectOperationExcelEnum.OperationInfoMeasurementId.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = ProjectOperationExcelEnum.OperationInfoMeasurementName.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = ProjectOperationExcelEnum.ProjectId.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = ProjectOperationExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = ProjectOperationExcelEnum.ProjectCode.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = ProjectOperationExcelEnum.CostCenterId.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = ProjectOperationExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = ProjectOperationExcelEnum.CostCenterCode.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = ProjectOperationExcelEnum.CategoryName.GetEnumDescription();
            worksheet.Cells[currentRow, 15].Value = ProjectOperationExcelEnum.BranchName.GetEnumDescription();
            worksheet.Cells[currentRow, 16].Value = ProjectOperationExcelEnum.SeasonName.GetEnumDescription();
            worksheet.Cells[currentRow, 17].Value = ProjectOperationExcelEnum.Contractors.GetEnumDescription();
            worksheet.Cells[currentRow, 18].Value = ProjectOperationExcelEnum.ContractorsNickName.GetEnumDescription();
            worksheet.Cells[currentRow, 19].Value = ProjectOperationExcelEnum.ImplementationAssistants.GetEnumDescription();
            worksheet.Cells[currentRow, 20].Value = ProjectOperationExcelEnum.ImplementationAssistantsNickName.GetEnumDescription();
            worksheet.Cells[currentRow, 21].Value = ProjectOperationExcelEnum.TechnicalAssistants.GetEnumDescription();
            worksheet.Cells[currentRow, 22].Value = ProjectOperationExcelEnum.TechnicalAssistantsNickName.GetEnumDescription();
            worksheet.Cells[currentRow, 23].Value = ProjectOperationExcelEnum.DailyProjectOperationCreators.GetEnumDescription();
            worksheet.Cells[currentRow, 24].Value = ProjectOperationExcelEnum.StartDate.GetEnumDescription();
            worksheet.Cells[currentRow, 25].Value = ProjectOperationExcelEnum.EndDate.GetEnumDescription();
            worksheet.Cells[currentRow, 26].Value = ProjectOperationExcelEnum.MeasurementId.GetEnumDescription();
            worksheet.Cells[currentRow, 27].Value = ProjectOperationExcelEnum.MeasurementName.GetEnumDescription();
            worksheet.Cells[currentRow, 28].Value = ProjectOperationExcelEnum.TolerancePercentage.GetEnumDescription();
            worksheet.Cells[currentRow, 29].Value = ProjectOperationExcelEnum.Price.GetEnumDescription();
            worksheet.Cells[currentRow, 30].Value = ProjectOperationExcelEnum.Priority.GetEnumDescription();
            worksheet.Cells[currentRow, 31].Value = ProjectOperationExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 32].Value = ProjectOperationExcelEnum.Workload.GetEnumDescription();
            worksheet.Cells[currentRow, 33].Value = ProjectOperationExcelEnum.DoneWorkload.GetEnumDescription();
            worksheet.Cells[currentRow, 34].Value = ProjectOperationExcelEnum.RemaindedWorkload.GetEnumDescription();
            worksheet.Cells[currentRow, 35].Value = ProjectOperationExcelEnum.CreatorId.GetEnumDescription();
            worksheet.Cells[currentRow, 36].Value = ProjectOperationExcelEnum.CreatorName.GetEnumDescription();
            worksheet.Cells[currentRow, 37].Value = ProjectOperationExcelEnum.Created.GetEnumDescription();
            worksheet.Cells[currentRow, 38].Value = ProjectOperationExcelEnum.Description.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.Id;
                worksheet.Cells[currentRow, 3].Value = item.OperationInfoId;
                worksheet.Cells[currentRow, 4].Value = item.OperationInfoName;
                worksheet.Cells[currentRow, 5].Value = item.OperationInfoCode;
                worksheet.Cells[currentRow, 6].Value = item.OperationInfoMeasurementId;
                worksheet.Cells[currentRow, 7].Value = item.OperationInfoMeasurementName;
                worksheet.Cells[currentRow, 8].Value = item.ProjectId;
                worksheet.Cells[currentRow, 9].Value = item.ProjectName;
                worksheet.Cells[currentRow, 10].Value = item.ProjectCode;
                worksheet.Cells[currentRow, 11].Value = item.CostCenterId;
                worksheet.Cells[currentRow, 12].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 13].Value = item.CostCenterCode;
                worksheet.Cells[currentRow, 14].Value = item.CategoryName;
                worksheet.Cells[currentRow, 15].Value = item.BranchName;
                worksheet.Cells[currentRow, 16].Value = item.SeasonName;
                worksheet.Cells[currentRow, 17].Value = item.Contractors;
                worksheet.Cells[currentRow, 18].Value = item.ContractorsNickName;
                worksheet.Cells[currentRow, 19].Value = item.ImplementationAssistants;
                worksheet.Cells[currentRow, 20].Value = item.ImplementationAssistantsNickName;
                worksheet.Cells[currentRow, 21].Value = item.TechnicalAssistants;
                worksheet.Cells[currentRow, 22].Value = item.TechnicalAssistantsNickName;
                worksheet.Cells[currentRow, 23].Value = item.DailyProjectOperationCreators;
                worksheet.Cells[currentRow, 24].Value = item.ShamsiStartDate;
                worksheet.Cells[currentRow, 25].Value = item.ShamsiEndDate;
                worksheet.Cells[currentRow, 26].Value = item.MeasurementId;
                worksheet.Cells[currentRow, 27].Value = item.MeasurementName;
                worksheet.Cells[currentRow, 28].Value = item.TolerancePercentage;
                worksheet.Cells[currentRow, 29].Value = item.Price;
                worksheet.Cells[currentRow, 30].Value = item.Priority;
                worksheet.Cells[currentRow, 31].Value = item.StatusDescription;
                worksheet.Cells[currentRow, 32].Value = item.Workload;
                worksheet.Cells[currentRow, 33].Value = item.DoneWorkload;
                worksheet.Cells[currentRow, 34].Value = item.RemaindedWorkload;
                worksheet.Cells[currentRow, 35].Value = item.CreatorId;
                worksheet.Cells[currentRow, 36].Value = item.CreatorName;
                worksheet.Cells[currentRow, 37].Value = item.ShamsiCreated;
                worksheet.Cells[currentRow, 38].Value = item.Description;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        var lastColumn = worksheet.Dimension.End.Column;

        ExcelStyles.SetSummaryCellData(
            worksheet.Cells[2, lastColumn + 2, 3, lastColumn + 3],
            worksheet,
            lastColumn);

        worksheet.Cells[2, lastColumn + 2].Value = ProjectOperationExcelEnum.DoneWorkload.GetEnumDescription();
        worksheet.Cells[3, lastColumn + 2].Value = "مجموع";

        worksheet.Cells[2, lastColumn + 3].Value = result.Select(s => s.DoneWorkload).Count();
        worksheet.Cells[3, lastColumn + 3].Value = result.Count;

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static byte[] GetsProjectOperationDailyReportingToExcel(
    List<GetsProjectOperationDailyReportingExcelExporterModel> result,
    List<ProjectOperationDailyExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();

        #region روکش صورت وضعیت
        var worksheet = workbook.Workbook.Worksheets.Add("روکش صورت وضعیت");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<ProjectOperationDailyExcelEnum, int>();
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
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.Id))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.CostCenterId))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.CostCenterId]].Value = item.CostCenterId;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.CostCenterCode))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.CostCenterCode]].Value = item.CostCenterCode;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.ProjectId))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.ProjectId]].Value = item.ProjectId;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.ProjectCode))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.ProjectCode]].Value = item.ProjectCode;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.OperationInfoId))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.OperationInfoId]].Value = item.OperationInfoId;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.OperationInfoName))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.OperationInfoName]].Value = item.OperationInfoName;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.OperationInfoCode))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.OperationInfoCode]].Value = item.OperationInfoCode;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.OperationInfoMeasurementId))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.OperationInfoMeasurementId]].Value = item.OperationInfoMeasurementId;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.OperationInfoMeasurementName))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.OperationInfoMeasurementName]].Value = item.OperationInfoMeasurementName;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.RialPrice))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.RialPrice]].Value = "0";
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.DollarPrice))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.DollarPrice]].Value = "0";
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.TotalRialPrice))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.TotalRialPrice]].Value = "0";
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.TotalDollarPrice))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.TotalDollarPrice]].Value = "0";
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.Workload))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.Workload]].Value = item.Workload;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.DoneWorkload))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.DoneWorkload]].Value = item.DoneWorkload;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.RemaindedWorkload))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.RemaindedWorkload]].Value = item.RemaindedWorkload;
                if (columns.ContainsKey(ProjectOperationDailyExcelEnum.Description))
                    worksheet.Cells[currentRow, columns[ProjectOperationDailyExcelEnum.Description]].Value = item.Description;

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
            worksheet.Cells[currentRow, 1].Value = ProjectOperationDailyExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = ProjectOperationDailyExcelEnum.Id.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = ProjectOperationDailyExcelEnum.CostCenterId.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = ProjectOperationDailyExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = ProjectOperationDailyExcelEnum.CostCenterCode.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = ProjectOperationDailyExcelEnum.ProjectId.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = ProjectOperationDailyExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = ProjectOperationDailyExcelEnum.ProjectCode.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = ProjectOperationDailyExcelEnum.OperationInfoId.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = ProjectOperationDailyExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = ProjectOperationDailyExcelEnum.OperationInfoCode.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = ProjectOperationDailyExcelEnum.OperationInfoMeasurementId.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = ProjectOperationDailyExcelEnum.OperationInfoMeasurementName.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = ProjectOperationDailyExcelEnum.RialPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 15].Value = ProjectOperationDailyExcelEnum.DollarPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 16].Value = ProjectOperationDailyExcelEnum.TotalRialPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 17].Value = ProjectOperationDailyExcelEnum.TotalDollarPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 18].Value = ProjectOperationDailyExcelEnum.Workload.GetEnumDescription();
            worksheet.Cells[currentRow, 19].Value = ProjectOperationDailyExcelEnum.DoneWorkload.GetEnumDescription();
            worksheet.Cells[currentRow, 20].Value = ProjectOperationDailyExcelEnum.RemaindedWorkload.GetEnumDescription();
            worksheet.Cells[currentRow, 21].Value = ProjectOperationDailyExcelEnum.Description.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.Id;
                worksheet.Cells[currentRow, 3].Value = item.CostCenterId;
                worksheet.Cells[currentRow, 4].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 5].Value = item.CostCenterCode;
                worksheet.Cells[currentRow, 6].Value = item.ProjectId;
                worksheet.Cells[currentRow, 7].Value = item.ProjectName;
                worksheet.Cells[currentRow, 8].Value = item.ProjectCode;
                worksheet.Cells[currentRow, 9].Value = item.OperationInfoId;
                worksheet.Cells[currentRow, 10].Value = item.OperationInfoName;
                worksheet.Cells[currentRow, 11].Value = item.OperationInfoCode;
                worksheet.Cells[currentRow, 12].Value = item.OperationInfoMeasurementId;
                worksheet.Cells[currentRow, 13].Value = item.OperationInfoMeasurementName;
                worksheet.Cells[currentRow, 14].Value = "0";
                worksheet.Cells[currentRow, 15].Value = "0";
                worksheet.Cells[currentRow, 16].Value = "0";
                worksheet.Cells[currentRow, 17].Value = "0";
                worksheet.Cells[currentRow, 18].Value = item.Workload;
                worksheet.Cells[currentRow, 19].Value = item.DoneWorkload;
                worksheet.Cells[currentRow, 20].Value = item.RemaindedWorkload;
                worksheet.Cells[currentRow, 21].Value = item.Description;
            }
        }
        #endregion

        #region ریزمتره
        var worksheet2 = workbook.Workbook.Worksheets.Add("ریزمتره");
        var currentRow2 = 1;
        worksheet2.View.RightToLeft = true;

        worksheet2.Cells[currentRow2, 1].Value = ProjectOperationDailyDetailExcelEnum.Row.GetEnumDescription();
        worksheet2.Cells[currentRow2, 2].Value = ProjectOperationDailyDetailExcelEnum.Id.GetEnumDescription();
        worksheet2.Cells[currentRow2, 3].Value = ProjectOperationDailyDetailExcelEnum.ProjectOperationId.GetEnumDescription();
        worksheet2.Cells[currentRow2, 4].Value = ProjectOperationDailyDetailExcelEnum.OperationInfoName.GetEnumDescription();
        worksheet2.Cells[currentRow2, 5].Value = ProjectOperationDailyDetailExcelEnum.OperationInfoCode.GetEnumDescription();
        worksheet2.Cells[currentRow2, 6].Value = ProjectOperationDailyDetailExcelEnum.OperationInfoMeasurementName.GetEnumDescription();
        worksheet2.Cells[currentRow2, 7].Value = ProjectOperationDailyDetailExcelEnum.ProjectName.GetEnumDescription();
        worksheet2.Cells[currentRow2, 8].Value = ProjectOperationDailyDetailExcelEnum.ProjectCode.GetEnumDescription();
        worksheet2.Cells[currentRow2, 9].Value = ProjectOperationDailyDetailExcelEnum.Location.GetEnumDescription();
        worksheet2.Cells[currentRow2, 10].Value = ProjectOperationDailyDetailExcelEnum.Length.GetEnumDescription();
        worksheet2.Cells[currentRow2, 11].Value = ProjectOperationDailyDetailExcelEnum.Height.GetEnumDescription();
        worksheet2.Cells[currentRow2, 12].Value = ProjectOperationDailyDetailExcelEnum.Width.GetEnumDescription();
        worksheet2.Cells[currentRow2, 13].Value = ProjectOperationDailyDetailExcelEnum.Weight.GetEnumDescription();
        worksheet2.Cells[currentRow2, 14].Value = ProjectOperationDailyDetailExcelEnum.Number.GetEnumDescription();
        worksheet2.Cells[currentRow2, 15].Value = ProjectOperationDailyDetailExcelEnum.Description.GetEnumDescription();

        foreach (var item in result)
        {
            currentRow2++;
            worksheet2.Cells[currentRow2, 1].Value = currentRow2 - 1;
            worksheet2.Cells[currentRow2, 2].Value = item.ProjectOperationDailyDetail!.Select(s => s.Id);
            worksheet2.Cells[currentRow2, 3].Value = item.ProjectOperationDailyDetail!.Select(s => s.ProjectOperationId);
            worksheet2.Cells[currentRow2, 4].Value = item.ProjectOperationDailyDetail!.Select(s => s.OperationInfoName);
            worksheet2.Cells[currentRow2, 5].Value = item.ProjectOperationDailyDetail!.Select(s => s.OperationInfoCode);
            worksheet2.Cells[currentRow2, 6].Value = item.ProjectOperationDailyDetail!.Select(s => s.OperationInfoMeasurementName);
            worksheet2.Cells[currentRow2, 7].Value = item.ProjectOperationDailyDetail!.Select(s => s.ProjectName);
            worksheet2.Cells[currentRow2, 8].Value = item.ProjectOperationDailyDetail!.Select(s => s.ProjectCode);
            worksheet2.Cells[currentRow2, 9].Value = item.ProjectOperationDailyDetail!.Select(s => s.Location);
            worksheet2.Cells[currentRow2, 10].Value = item.ProjectOperationDailyDetail!.Select(s => s.Length);
            worksheet2.Cells[currentRow2, 11].Value = item.ProjectOperationDailyDetail!.Select(s => s.Height);
            worksheet2.Cells[currentRow2, 12].Value = item.ProjectOperationDailyDetail!.Select(s => s.Width);
            worksheet2.Cells[currentRow2, 13].Value = item.ProjectOperationDailyDetail!.Select(s => s.Weight);
            worksheet2.Cells[currentRow2, 14].Value = item.ProjectOperationDailyDetail!.Select(s => s.Number);
            worksheet2.Cells[currentRow2, 15].Value = item.ProjectOperationDailyDetail!.Select(s => s.Description);
        }
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }
}