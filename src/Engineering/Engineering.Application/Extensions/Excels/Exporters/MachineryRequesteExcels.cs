using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectMachineryReportsExcelEnums;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectMachineryReportsExcelExporter;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReportsExcelEnums;
using Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReportsExcelExporter;
using Engineering.Application.Services.RequestMachineries.Models.GetsEmployerStatusStatementExcelEnums;
using Engineering.Application.Services.RequestMachineries.Models.GetSendManagerMachineryReports;
using Engineering.Application.Services.RequestMachineries.Models.GetsMachineryRequesteExcelExporter;
using Engineering.Application.Services.RequestMachineries.Models.GetsRequestMachineryDetailReportsExcelEnum;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class MachineryRequesteExcels
{
    public static byte[] RequestMachineriesToExcel(
        ICollection<GetsMachineryRequesteExcelExporterResponseModel> result,
        List<GetsRequestMachineryExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("درخواست ماشین آلات");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<GetsRequestMachineryExcelEnum, int>();
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
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.Id))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.Created))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.Created]].Value = item.Created;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.MachineryGroupName))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.MachineryGroupName]].Value = item.MachineryGroupName;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.MachineryName))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.MachineryName]].Value = item.MachineryName;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.TimeRequired))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.TimeRequired]].Value = item.TimeRequired;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.UnitDescription))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.UnitDescription]].Value = item.UnitDescription;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.StatusDescription))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.RequestCount))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.RequestCount]].Value = item.RequestCount;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.FromDate))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.FromDate]].Value = item.FromDate;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ToDate))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ToDate]].Value = item.ToDate;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.CreatorId))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.CreatorId]].Value = item.CreatorId;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.Creator))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.Creator]].Value = item.Creator;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ProjectOperations))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ProjectOperations]].Value = item.ProjectOperations;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ProjectOperationDetails))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ProjectOperationDetails]].Value = item.ProjectOperationDetails;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ConfirmDate))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ConfirmDate]].Value = item.ConfirmDate;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ConfirmUserId))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ConfirmUserId]].Value = item.ConfirmUserId;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ConfirmUser))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ConfirmUser]].Value = item.ConfirmUser;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.UnitDescription))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.UnitDescription]].Value = item.UnitDescription;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.RequestNumber))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.RequestNumber]].Value = item.RequestNumber;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.FromTime))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.FromTime]].Value = item.FromTime;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ToTime))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ToTime]].Value = item.ToTime;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ConfirmFromDate))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ConfirmFromDate]].Value = item.ConfirmFromDate;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ConfirmFromTime))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ConfirmFromTime]].Value = item.ConfirmFromTime;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ConfirmToDate))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ConfirmToDate]].Value = item.ConfirmToDate;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ConfirmToTime))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ConfirmToTime]].Value = item.ConfirmToTime;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ConfirmedTimeRequired))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ConfirmedTimeRequired]].Value = item.ConfirmedTimeRequired;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.ConfirmedDescription))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.ConfirmedDescription]].Value = item.ConfirmedDescription;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.Contractor))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.Contractor]].Value = item.Contractor;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.RequestNumber))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.RequestNumber]].Value = item.RequestNumber;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.UnitPrice))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.UnitPrice]].Value = item.UnitPrice;
                if (columns.ContainsKey(GetsRequestMachineryExcelEnum.TotalPrice))
                    worksheet.Cells[currentRow, columns[GetsRequestMachineryExcelEnum.TotalPrice]].Value = item.TotalPrice;

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
                worksheet.Cells[1, 1, 1, 32],
                ExcelBorderStyle.Medium);

            worksheet.Cells[currentRow, 1].Value = GetsRequestMachineryExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = GetsRequestMachineryExcelEnum.Id.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = GetsRequestMachineryExcelEnum.Created.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = GetsRequestMachineryExcelEnum.MachineryGroupName.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = GetsRequestMachineryExcelEnum.MachineryName.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = GetsRequestMachineryExcelEnum.TimeRequired.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = GetsRequestMachineryExcelEnum.UnitDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = GetsRequestMachineryExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = GetsRequestMachineryExcelEnum.RequestCount.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = GetsRequestMachineryExcelEnum.FromDate.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = GetsRequestMachineryExcelEnum.ToDate.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = GetsRequestMachineryExcelEnum.CreatorId.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = GetsRequestMachineryExcelEnum.Creator.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = GetsRequestMachineryExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[currentRow, 15].Value = GetsRequestMachineryExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cells[currentRow, 16].Value = GetsRequestMachineryExcelEnum.ProjectOperations.GetEnumDescription();
            worksheet.Cells[currentRow, 17].Value = GetsRequestMachineryExcelEnum.ProjectOperationDetails.GetEnumDescription();
            worksheet.Cells[currentRow, 18].Value = GetsRequestMachineryExcelEnum.ConfirmDate.GetEnumDescription();
            worksheet.Cells[currentRow, 19].Value = GetsRequestMachineryExcelEnum.ConfirmUserId.GetEnumDescription();
            worksheet.Cells[currentRow, 20].Value = GetsRequestMachineryExcelEnum.ConfirmUser.GetEnumDescription();
            worksheet.Cells[currentRow, 21].Value = GetsRequestMachineryExcelEnum.UnitDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 22].Value = GetsRequestMachineryExcelEnum.RequestNumber.GetEnumDescription();
            worksheet.Cells[currentRow, 23].Value = GetsRequestMachineryExcelEnum.FromTime.GetEnumDescription();
            worksheet.Cells[currentRow, 24].Value = GetsRequestMachineryExcelEnum.ToTime.GetEnumDescription();
            worksheet.Cells[currentRow, 25].Value = GetsRequestMachineryExcelEnum.ConfirmFromDate.GetEnumDescription();
            worksheet.Cells[currentRow, 26].Value = GetsRequestMachineryExcelEnum.ConfirmFromTime.GetEnumDescription();
            worksheet.Cells[currentRow, 27].Value = GetsRequestMachineryExcelEnum.ConfirmToDate.GetEnumDescription();
            worksheet.Cells[currentRow, 28].Value = GetsRequestMachineryExcelEnum.ConfirmToTime.GetEnumDescription();
            worksheet.Cells[currentRow, 29].Value = GetsRequestMachineryExcelEnum.ConfirmedTimeRequired.GetEnumDescription();
            worksheet.Cells[currentRow, 30].Value = GetsRequestMachineryExcelEnum.ConfirmedDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 31].Value = GetsRequestMachineryExcelEnum.Contractor.GetEnumDescription();
            worksheet.Cells[currentRow, 32].Value = GetsRequestMachineryExcelEnum.RequestNumber.GetEnumDescription();
            worksheet.Cells[currentRow, 33].Value = GetsRequestMachineryExcelEnum.UnitPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 34].Value = GetsRequestMachineryExcelEnum.TotalPrice.GetEnumDescription();


            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.Id;
                worksheet.Cells[currentRow, 3].Value = item.Created;
                worksheet.Cells[currentRow, 4].Value = item.MachineryGroupName;
                worksheet.Cells[currentRow, 5].Value = item.MachineryName;
                worksheet.Cells[currentRow, 6].Value = item.TimeRequired;
                worksheet.Cells[currentRow, 7].Value = item.UnitDescription;
                worksheet.Cells[currentRow, 8].Value = item.StatusDescription;
                worksheet.Cells[currentRow, 9].Value = item.RequestCount;
                worksheet.Cells[currentRow, 10].Value = item.FromDate;
                worksheet.Cells[currentRow, 11].Value = item.ToDate;
                worksheet.Cells[currentRow, 12].Value = item.CreatorId;
                worksheet.Cells[currentRow, 13].Value = item.Creator;
                worksheet.Cells[currentRow, 14].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 15].Value = item.ProjectName;
                worksheet.Cells[currentRow, 16].Value = item.ProjectOperations;
                worksheet.Cells[currentRow, 17].Value = item.ProjectOperationDetails;
                worksheet.Cells[currentRow, 18].Value = item.ConfirmDate;
                worksheet.Cells[currentRow, 19].Value = item.ConfirmUserId;
                worksheet.Cells[currentRow, 20].Value = item.ConfirmUser;
                worksheet.Cells[currentRow, 21].Value = item.UnitDescription;
                worksheet.Cells[currentRow, 22].Value = item.RequestNumber;
                worksheet.Cells[currentRow, 23].Value = item.FromTime;
                worksheet.Cells[currentRow, 24].Value = item.ToTime;
                worksheet.Cells[currentRow, 25].Value = item.ConfirmFromDate;
                worksheet.Cells[currentRow, 26].Value = item.ConfirmFromTime;
                worksheet.Cells[currentRow, 27].Value = item.ConfirmToDate;
                worksheet.Cells[currentRow, 28].Value = item.ConfirmToTime;
                worksheet.Cells[currentRow, 29].Value = item.ConfirmedTimeRequired;
                worksheet.Cells[currentRow, 30].Value = item.ConfirmedDescription;
                worksheet.Cells[currentRow, 31].Value = item.Contractor;
                worksheet.Cells[currentRow, 32].Value = item.RequestNumber;
                worksheet.Cells[currentRow, 33].Value = item.UnitPrice;
                worksheet.Cells[currentRow, 34].Value = item.TotalPrice;

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


    public static byte[] RequestMachineriesReportsToExcel(
        ICollection<GetOnProjectMachineryReportsExcelExporterModel> result,
        ICollection<GetOnProjectMachineryDetailReportsExcelExporterModel> detailresult,
        List<GetsOnProjectRequestMachineryExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("گزارش درخواست ماشین آلات");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<GetsOnProjectRequestMachineryExcelEnum, int>();
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
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.Contractor))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.Contractor]].Value = item.Contractor;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.MachineryName))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.MachineryName]].Value = item.MachineryName;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.Machinerycode))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.Machinerycode]].Value = item.Machinerycode;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.UnitDescription))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.UnitDescription]].Value = item.UnitDescription;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.FromDateShamsi))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.FromDateShamsi]].Value = item.FromDateShamsi;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.ToDateShamsi))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.ToDateShamsi]].Value = item.ToDateShamsi;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.TotalRequestedCount))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.TotalRequestedCount]].Value = item.TotalRequestedCount;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.TotalFinalPrice))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.TotalFinalPrice]].Value = item.TotalFinalPrice;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.TotalConfirmedTime))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.TotalConfirmedTime]].Value = item.TotalConfirmedTimeRequired;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.TotalDayWork))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.TotalDayWork]].Value = item.TotalDayWork;
                if (columns.ContainsKey(GetsOnProjectRequestMachineryExcelEnum.TotalTimeWork))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestMachineryExcelEnum.TotalTimeWork]].Value = item.TotalTimeWork;

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
                worksheet.Cells[1, 1, 1, 14],
                ExcelBorderStyle.Medium);

            worksheet.Cells[currentRow, 1].Value = GetsOnProjectRequestMachineryExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = GetsOnProjectRequestMachineryExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = GetsOnProjectRequestMachineryExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = GetsOnProjectRequestMachineryExcelEnum.Contractor.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = GetsOnProjectRequestMachineryExcelEnum.MachineryName.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = GetsOnProjectRequestMachineryExcelEnum.Machinerycode.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = GetsOnProjectRequestMachineryExcelEnum.UnitDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = GetsOnProjectRequestMachineryExcelEnum.FromDateShamsi.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = GetsOnProjectRequestMachineryExcelEnum.ToDateShamsi.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = GetsOnProjectRequestMachineryExcelEnum.TotalRequestedCount.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = GetsOnProjectRequestMachineryExcelEnum.TotalFinalPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = GetsOnProjectRequestMachineryExcelEnum.TotalConfirmedTime.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = GetsOnProjectRequestMachineryExcelEnum.TotalDayWork.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = GetsOnProjectRequestMachineryExcelEnum.TotalTimeWork.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 3].Value = item.ProjectName;
                worksheet.Cells[currentRow, 4].Value = item.Contractor;
                worksheet.Cells[currentRow, 5].Value = item.MachineryName;
                worksheet.Cells[currentRow, 6].Value = item.Machinerycode;
                worksheet.Cells[currentRow, 7].Value = item.UnitDescription;
                worksheet.Cells[currentRow, 8].Value = item.FromDateShamsi;
                worksheet.Cells[currentRow, 9].Value = item.ToDateShamsi;
                worksheet.Cells[currentRow, 10].Value = item.TotalRequestedCount;
                worksheet.Cells[currentRow, 11].Value = item.TotalFinalPrice;
                worksheet.Cells[currentRow, 12].Value = item.TotalConfirmedTimeRequired;
                worksheet.Cells[currentRow, 13].Value = item.TotalDayWork;
                worksheet.Cells[currentRow, 14].Value = item.TotalTimeWork;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        #region جزییات گزارش درخواست ماشین آلات
        var worksheetNew = workbook.Workbook.Worksheets.Add("جزئیات گزارش درخواست ماشین آلات");
        var currentRowNew = 1;
        worksheet.View.RightToLeft = true;

        ExcelStyles.SetHeaderStyle(
            worksheetNew.Cells[1, 1, 1, 32],
            ExcelBorderStyle.Medium);

        worksheetNew.Cells[currentRowNew, 1].Value = RequestMachineryDetailReportsExcelEnum.Row.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 2].Value = RequestMachineryDetailReportsExcelEnum.RequestMachineryId.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 3].Value = RequestMachineryDetailReportsExcelEnum.MachineryName.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 4].Value = RequestMachineryDetailReportsExcelEnum.MachineryCode.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 5].Value = RequestMachineryDetailReportsExcelEnum.CostCenterName.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 6].Value = RequestMachineryDetailReportsExcelEnum.ProjectName.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 7].Value = RequestMachineryDetailReportsExcelEnum.ProjectOperations.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 8].Value = RequestMachineryDetailReportsExcelEnum.ProjectOperationDetails.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 9].Value = RequestMachineryDetailReportsExcelEnum.Created.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 10].Value = RequestMachineryDetailReportsExcelEnum.MachineryGroupName.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 11].Value = RequestMachineryDetailReportsExcelEnum.TimeRequired.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 12].Value = RequestMachineryDetailReportsExcelEnum.UnitDescription.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 13].Value = RequestMachineryDetailReportsExcelEnum.StatusDescription.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 14].Value = RequestMachineryDetailReportsExcelEnum.RequestCount.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 15].Value = RequestMachineryDetailReportsExcelEnum.TotalPrice.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 16].Value = RequestMachineryDetailReportsExcelEnum.Currency.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 17].Value = RequestMachineryDetailReportsExcelEnum.Contractor.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 18].Value = RequestMachineryDetailReportsExcelEnum.Operator.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 19].Value = RequestMachineryDetailReportsExcelEnum.FromDate.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 20].Value = RequestMachineryDetailReportsExcelEnum.FromTime.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 21].Value = RequestMachineryDetailReportsExcelEnum.ToDate.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 22].Value = RequestMachineryDetailReportsExcelEnum.ToTime.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 23].Value = RequestMachineryDetailReportsExcelEnum.ConfirmFromDate.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 24].Value = RequestMachineryDetailReportsExcelEnum.ConfirmFromTime.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 25].Value = RequestMachineryDetailReportsExcelEnum.ConfirmToDate.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 26].Value = RequestMachineryDetailReportsExcelEnum.ConfirmToTime.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 27].Value = RequestMachineryDetailReportsExcelEnum.ConfirmUser.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 28].Value = RequestMachineryDetailReportsExcelEnum.ConfirmDate.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 29].Value = RequestMachineryDetailReportsExcelEnum.Creator.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 30].Value = RequestMachineryDetailReportsExcelEnum.ConfirmedTimeRequired.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 31].Value = RequestMachineryDetailReportsExcelEnum.ConfirmedDescription.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 32].Value = RequestMachineryDetailReportsExcelEnum.RequestNumber.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 33].Value = RequestMachineryDetailReportsExcelEnum.UnitPrice.GetEnumDescription();

        foreach (var item1 in detailresult)
        {
            currentRowNew++;
            worksheetNew.Cells[currentRowNew, 1].Value = currentRowNew - 1;
            worksheetNew.Cells[currentRowNew, 2].Value = item1.RequestMachineryId;
            worksheetNew.Cells[currentRowNew, 3].Value = item1.MachineryName;
            worksheetNew.Cells[currentRowNew, 4].Value = item1.MachineryCode;
            worksheetNew.Cells[currentRowNew, 5].Value = item1.CostCenterName;
            worksheetNew.Cells[currentRowNew, 6].Value = item1.ProjectName;
            worksheetNew.Cells[currentRowNew, 7].Value = item1.ProjectOperations;
            worksheetNew.Cells[currentRowNew, 8].Value = item1.ProjectOperationDetails;
            worksheetNew.Cells[currentRowNew, 9].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item1.Created);
            worksheetNew.Cells[currentRowNew, 10].Value = item1.MachineryGroupName;
            worksheetNew.Cells[currentRowNew, 11].Value = item1.TimeRequired;
            worksheetNew.Cells[currentRowNew, 12].Value = item1.UnitDescription;
            worksheetNew.Cells[currentRowNew, 13].Value = item1.StatusDescription;
            worksheetNew.Cells[currentRowNew, 14].Value = item1.RequestCount;
            worksheetNew.Cells[currentRowNew, 15].Value = item1.TotalPrice;
            worksheetNew.Cells[currentRowNew, 16].Value = item1.Currency;
            worksheetNew.Cells[currentRowNew, 17].Value = item1.Contractor;
            worksheetNew.Cells[currentRowNew, 18].Value = item1.Operator;
            worksheetNew.Cells[currentRowNew, 19].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item1.FromDate);
            worksheetNew.Cells[currentRowNew, 20].Value = item1.FromTime;
            worksheetNew.Cells[currentRowNew, 21].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item1.ToDate);
            worksheetNew.Cells[currentRowNew, 22].Value = item1.ToTime;
            worksheetNew.Cells[currentRowNew, 23].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item1.ConfirmFromDate);
            worksheetNew.Cells[currentRowNew, 24].Value = item1.ConfirmFromTime;
            worksheetNew.Cells[currentRowNew, 25].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item1.ConfirmToDate);
            worksheetNew.Cells[currentRowNew, 26].Value = item1.ConfirmToTime;
            worksheetNew.Cells[currentRowNew, 27].Value = item1.ConfirmUser;
            worksheetNew.Cells[currentRowNew, 28].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item1.ConfirmDate);
            worksheetNew.Cells[currentRowNew, 29].Value = item1.Creator;
            worksheetNew.Cells[currentRowNew, 30].Value = item1.ConfirmedTimeRequired;
            worksheetNew.Cells[currentRowNew, 31].Value = item1.ConfirmedDescription;
            worksheetNew.Cells[currentRowNew, 32].Value = item1.RequestNumber;
            worksheetNew.Cells[currentRowNew, 33].Value = item1.UnitPrice;

            ExcelStyles.SetCellStyle(
                worksheet,
                currentRow - 1,
                currentRow,
                worksheet.Dimension.Start.Column,
                worksheet.Dimension.End.Column);
        }
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static byte[] RequestMachineriesReportsToExcel(
     ICollection<GetOnProjectRequestReportsExcelExporterModel> result,
     List<GetsOnProjectRequestExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("گزارش ماشین آلات سرپروژه");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<GetsOnProjectRequestExcelEnum, int>();
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
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.RequestMachineryId))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.RequestMachineryId]].Value = item.RequestMachineryId;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.RequestNumber))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.RequestNumber]].Value = item.RequestNumber;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.MachineryGroupName))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.MachineryGroupName]].Value = item.MachineryGroupName;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.MachineryName))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.MachineryName]].Value = item.MachineryName;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.MachineryCode))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.MachineryCode]].Value = item.MachineryCode;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ProjectOperations))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ProjectOperations]].Value = item.ProjectOperations;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ProjectOperationDetails))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ProjectOperationDetails]].Value = item.ProjectOperationDetails;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.Created))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.Created]].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.Created);
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.TimeRequired))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.TimeRequired]].Value = item.TimeRequired;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.UnitDescription))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.UnitDescription]].Value = item.UnitDescription;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.StatusDescription))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.RequestCount))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.RequestCount]].Value = item.RequestCount;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.TotalPrice))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.TotalPrice]].Value = item.TotalPrice;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.Currency))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.Currency]].Value = item.Currency;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.Contractor))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.Contractor]].Value = item.Contractor;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.Operator))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.Operator]].Value = item.Operator;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.FromDate))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.FromDate]].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.FromDate);
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.FromTime))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.FromTime]].Value = item.FromTimee;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ToDate))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ToDate]].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.ToDate);
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ToTime))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ToTime]].Value = item.ToTimee;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ConfirmFromDate))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ConfirmFromDate]].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.ConfirmFromDate);
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ConfirmFromTime))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ConfirmFromTime]].Value = item.ConfirmFromTimee;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ConfirmToDate))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ConfirmToDate]].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.ConfirmToDate);
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ConfirmToTime))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ConfirmToTime]].Value = item.ConfirmToTimee;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ConfirmUser))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ConfirmUser]].Value = item.ConfirmUser;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ConfirmDate))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ConfirmDate]].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.ConfirmDate);
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.Creator))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.Creator]].Value = item.Creator;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ConfirmedTimeRequired))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ConfirmedTimeRequired]].Value = item.ConfirmedTimeRequired;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.ConfirmedDescription))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.ConfirmedDescription]].Value = item.ConfirmedDescription;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.UnitPrice))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.UnitPrice]].Value = item.UnitPrice;
                if (columns.ContainsKey(GetsOnProjectRequestExcelEnum.PaymentType))
                    worksheet.Cells[currentRow, columns[GetsOnProjectRequestExcelEnum.PaymentType]].Value = item.PaymentTypeTitle;

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
                worksheet.Cells[1, 1, 1, 32],
                ExcelBorderStyle.Medium);

            worksheet.Cells[currentRow, 1].Value = GetsOnProjectRequestExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = GetsOnProjectRequestExcelEnum.RequestMachineryId.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = GetsOnProjectRequestExcelEnum.RequestNumber.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = GetsOnProjectRequestExcelEnum.MachineryGroupName.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = GetsOnProjectRequestExcelEnum.MachineryName.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = GetsOnProjectRequestExcelEnum.MachineryCode.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = GetsOnProjectRequestExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = GetsOnProjectRequestExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = GetsOnProjectRequestExcelEnum.ProjectOperations.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = GetsOnProjectRequestExcelEnum.ProjectOperationDetails.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = GetsOnProjectRequestExcelEnum.Created.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = GetsOnProjectRequestExcelEnum.TimeRequired.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = GetsOnProjectRequestExcelEnum.UnitDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = GetsOnProjectRequestExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 15].Value = GetsOnProjectRequestExcelEnum.RequestCount.GetEnumDescription();
            worksheet.Cells[currentRow, 16].Value = GetsOnProjectRequestExcelEnum.TotalPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 17].Value = GetsOnProjectRequestExcelEnum.Currency.GetEnumDescription();
            worksheet.Cells[currentRow, 18].Value = GetsOnProjectRequestExcelEnum.Contractor.GetEnumDescription();
            worksheet.Cells[currentRow, 19].Value = GetsOnProjectRequestExcelEnum.Operator.GetEnumDescription();
            worksheet.Cells[currentRow, 20].Value = GetsOnProjectRequestExcelEnum.FromDate.GetEnumDescription();
            worksheet.Cells[currentRow, 21].Value = GetsOnProjectRequestExcelEnum.FromTime.GetEnumDescription();
            worksheet.Cells[currentRow, 22].Value = GetsOnProjectRequestExcelEnum.ToDate.GetEnumDescription();
            worksheet.Cells[currentRow, 23].Value = GetsOnProjectRequestExcelEnum.ToTime.GetEnumDescription();
            worksheet.Cells[currentRow, 24].Value = GetsOnProjectRequestExcelEnum.ConfirmFromDate.GetEnumDescription();
            worksheet.Cells[currentRow, 25].Value = GetsOnProjectRequestExcelEnum.ConfirmFromTime.GetEnumDescription();
            worksheet.Cells[currentRow, 26].Value = GetsOnProjectRequestExcelEnum.ConfirmToDate.GetEnumDescription();
            worksheet.Cells[currentRow, 27].Value = GetsOnProjectRequestExcelEnum.ConfirmToTime.GetEnumDescription();
            worksheet.Cells[currentRow, 28].Value = GetsOnProjectRequestExcelEnum.ConfirmUser.GetEnumDescription();
            worksheet.Cells[currentRow, 29].Value = GetsOnProjectRequestExcelEnum.ConfirmDate.GetEnumDescription();
            worksheet.Cells[currentRow, 30].Value = GetsOnProjectRequestExcelEnum.Creator.GetEnumDescription();
            worksheet.Cells[currentRow, 31].Value = GetsOnProjectRequestExcelEnum.ConfirmedTimeRequired.GetEnumDescription();
            worksheet.Cells[currentRow, 32].Value = GetsOnProjectRequestExcelEnum.ConfirmedDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 33].Value = GetsOnProjectRequestExcelEnum.UnitPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 34].Value = GetsOnProjectRequestExcelEnum.PaymentType.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.RequestMachineryId;
                worksheet.Cells[currentRow, 3].Value = item.RequestNumber;
                worksheet.Cells[currentRow, 4].Value = item.MachineryGroupName;
                worksheet.Cells[currentRow, 5].Value = item.MachineryName;
                worksheet.Cells[currentRow, 6].Value = item.MachineryCode;
                worksheet.Cells[currentRow, 7].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 8].Value = item.ProjectName;
                worksheet.Cells[currentRow, 9].Value = item.ProjectOperations;
                worksheet.Cells[currentRow, 10].Value = item.ProjectOperationDetails;
                worksheet.Cells[currentRow, 11].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.Created);
                worksheet.Cells[currentRow, 12].Value = item.TimeRequired;
                worksheet.Cells[currentRow, 13].Value = item.UnitDescription;
                worksheet.Cells[currentRow, 14].Value = item.StatusDescription;
                worksheet.Cells[currentRow, 15].Value = item.RequestCount;
                worksheet.Cells[currentRow, 16].Value = item.TotalPrice;
                worksheet.Cells[currentRow, 17].Value = item.Currency;
                worksheet.Cells[currentRow, 18].Value = item.Contractor;
                worksheet.Cells[currentRow, 19].Value = item.Operator;
                worksheet.Cells[currentRow, 20].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.FromDate);
                worksheet.Cells[currentRow, 21].Value = item.FromTimee;
                worksheet.Cells[currentRow, 22].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.ToDate);
                worksheet.Cells[currentRow, 23].Value = item.ToTimee;
                worksheet.Cells[currentRow, 24].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.ConfirmFromDate);
                worksheet.Cells[currentRow, 25].Value = item.ConfirmFromTimee;
                worksheet.Cells[currentRow, 26].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.ConfirmToDate);
                worksheet.Cells[currentRow, 27].Value = item.ConfirmToTimee;
                worksheet.Cells[currentRow, 28].Value = item.ConfirmUser;
                worksheet.Cells[currentRow, 29].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item.ConfirmDate);
                worksheet.Cells[currentRow, 30].Value = item.Creator;
                worksheet.Cells[currentRow, 31].Value = item.ConfirmedTimeRequired;
                worksheet.Cells[currentRow, 32].Value = item.ConfirmedDescription;
                worksheet.Cells[currentRow, 33].Value = item.UnitPrice;
                worksheet.Cells[currentRow, 34].Value = item.PaymentTypeTitle;

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

    public static byte[] SendToManagerRequestMachineriesToExcel(
        ICollection<GetSendManagerMachineryReportsModel> result,
        ICollection<GetSendManagerMachineryDetailReportsModel>? detailresult,
        List<SendManagerRequestMachineryExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("گزارش درخواست ماشین آلات");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<SendManagerRequestMachineryExcelEnum, int>();
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
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.Contractor))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.Contractor]].Value = item.Contractor;
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.MachineryName))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.MachineryName]].Value = item.MachineryName;
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.Machinerycode))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.Machinerycode]].Value = item.Machinerycode;
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.UnitDescription))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.UnitDescription]].Value = item.UnitDescription;
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.FromDateShamsi))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.FromDateShamsi]].Value = item.FromDateShamsi;
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.ToDateShamsi))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.ToDateShamsi]].Value = item.ToDateShamsi;
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.TotalRequestedCount))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.TotalRequestedCount]].Value = item.TotalRequestedCount;
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.TotalFinalPrice))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.TotalFinalPrice]].Value = item.TotalFinalPrice;
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.TotalDayWork))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.TotalDayWork]].Value = item.TotalDayWork;
                if (columns.ContainsKey(SendManagerRequestMachineryExcelEnum.TotalTimeWork))
                    worksheet.Cells[currentRow, columns[SendManagerRequestMachineryExcelEnum.TotalTimeWork]].Value = item.TotalTimeWork;

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
                worksheet.Cells[1, 1, 1, 14],
                ExcelBorderStyle.Medium);

            worksheet.Cells[currentRow, 1].Value = SendManagerRequestMachineryExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = SendManagerRequestMachineryExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = SendManagerRequestMachineryExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = SendManagerRequestMachineryExcelEnum.Contractor.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = SendManagerRequestMachineryExcelEnum.MachineryName.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = SendManagerRequestMachineryExcelEnum.Machinerycode.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = SendManagerRequestMachineryExcelEnum.UnitDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = SendManagerRequestMachineryExcelEnum.FromDateShamsi.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = SendManagerRequestMachineryExcelEnum.ToDateShamsi.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = SendManagerRequestMachineryExcelEnum.TotalRequestedCount.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = SendManagerRequestMachineryExcelEnum.TotalFinalPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = SendManagerRequestMachineryExcelEnum.TotalDayWork.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = SendManagerRequestMachineryExcelEnum.TotalTimeWork.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 3].Value = item.ProjectName;
                worksheet.Cells[currentRow, 4].Value = item.Contractor;
                worksheet.Cells[currentRow, 5].Value = item.MachineryName;
                worksheet.Cells[currentRow, 6].Value = item.Machinerycode;
                worksheet.Cells[currentRow, 7].Value = item.UnitDescription;
                worksheet.Cells[currentRow, 8].Value = item.FromDateShamsi;
                worksheet.Cells[currentRow, 9].Value = item.ToDateShamsi;
                worksheet.Cells[currentRow, 10].Value = item.TotalRequestedCount;
                worksheet.Cells[currentRow, 11].Value = item.TotalFinalPrice;
                worksheet.Cells[currentRow, 13].Value = item.TotalDayWork;
                worksheet.Cells[currentRow, 14].Value = item.TotalTimeWork;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        #region جزییات گزارش درخواست ماشین آلات
        var worksheetNew = workbook.Workbook.Worksheets.Add("جزئیات گزارش درخواست ماشین آلات");
        var currentRowNew = 1;
        worksheet.View.RightToLeft = true;

        ExcelStyles.SetHeaderStyle(
            worksheetNew.Cells[1, 1, 1, 32],
            ExcelBorderStyle.Medium);

        worksheetNew.Cells[currentRowNew, 1].Value = RequestMachineryDetailReportsExcelEnum.Row.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 2].Value = RequestMachineryDetailReportsExcelEnum.RequestMachineryId.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 3].Value = RequestMachineryDetailReportsExcelEnum.MachineryName.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 4].Value = RequestMachineryDetailReportsExcelEnum.MachineryCode.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 5].Value = RequestMachineryDetailReportsExcelEnum.CostCenterName.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 6].Value = RequestMachineryDetailReportsExcelEnum.ProjectName.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 7].Value = RequestMachineryDetailReportsExcelEnum.ProjectOperations.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 8].Value = RequestMachineryDetailReportsExcelEnum.ProjectOperationDetails.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 9].Value = RequestMachineryDetailReportsExcelEnum.Created.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 10].Value = RequestMachineryDetailReportsExcelEnum.MachineryGroupName.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 11].Value = RequestMachineryDetailReportsExcelEnum.TimeRequired.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 12].Value = RequestMachineryDetailReportsExcelEnum.UnitDescription.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 13].Value = RequestMachineryDetailReportsExcelEnum.StatusDescription.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 14].Value = RequestMachineryDetailReportsExcelEnum.RequestCount.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 15].Value = RequestMachineryDetailReportsExcelEnum.TotalPrice.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 16].Value = RequestMachineryDetailReportsExcelEnum.Currency.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 17].Value = RequestMachineryDetailReportsExcelEnum.Contractor.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 18].Value = RequestMachineryDetailReportsExcelEnum.Operator.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 19].Value = RequestMachineryDetailReportsExcelEnum.FromDate.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 20].Value = RequestMachineryDetailReportsExcelEnum.FromTime.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 21].Value = RequestMachineryDetailReportsExcelEnum.ToDate.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 22].Value = RequestMachineryDetailReportsExcelEnum.ToTime.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 23].Value = RequestMachineryDetailReportsExcelEnum.ConfirmFromDate.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 24].Value = RequestMachineryDetailReportsExcelEnum.ConfirmFromTime.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 25].Value = RequestMachineryDetailReportsExcelEnum.ConfirmToDate.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 26].Value = RequestMachineryDetailReportsExcelEnum.ConfirmToTime.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 27].Value = RequestMachineryDetailReportsExcelEnum.ConfirmUser.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 28].Value = RequestMachineryDetailReportsExcelEnum.ConfirmDate.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 29].Value = RequestMachineryDetailReportsExcelEnum.Creator.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 30].Value = RequestMachineryDetailReportsExcelEnum.ConfirmedTimeRequired.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 31].Value = RequestMachineryDetailReportsExcelEnum.ConfirmedDescription.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 32].Value = RequestMachineryDetailReportsExcelEnum.RequestNumber.GetEnumDescription();
        worksheetNew.Cells[currentRowNew, 33].Value = RequestMachineryDetailReportsExcelEnum.UnitPrice.GetEnumDescription();

        if (detailresult != null && detailresult.Count > 0)
            foreach (var item1 in detailresult)
            {
                currentRowNew++;
                worksheetNew.Cells[currentRowNew, 1].Value = currentRowNew - 1;
                worksheetNew.Cells[currentRowNew, 2].Value = item1.RequestMachineryId;
                worksheetNew.Cells[currentRowNew, 3].Value = item1.MachineryName;
                worksheetNew.Cells[currentRowNew, 4].Value = item1.MachineryCode;
                worksheetNew.Cells[currentRowNew, 5].Value = item1.CostCenterName;
                worksheetNew.Cells[currentRowNew, 6].Value = item1.ProjectName;
                worksheetNew.Cells[currentRowNew, 7].Value = item1.ProjectOperations;
                worksheetNew.Cells[currentRowNew, 8].Value = item1.ProjectOperationDetails;
                worksheetNew.Cells[currentRowNew, 9].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item1.Created);
                worksheetNew.Cells[currentRowNew, 10].Value = item1.MachineryGroupName;
                worksheetNew.Cells[currentRowNew, 11].Value = item1.TimeRequired;
                worksheetNew.Cells[currentRowNew, 12].Value = item1.UnitDescription;
                worksheetNew.Cells[currentRowNew, 13].Value = item1.StatusDescription;
                worksheetNew.Cells[currentRowNew, 14].Value = item1.RequestCount;
                worksheetNew.Cells[currentRowNew, 15].Value = item1.TotalPrice;
                worksheetNew.Cells[currentRowNew, 16].Value = item1.Currency;
                worksheetNew.Cells[currentRowNew, 17].Value = item1.Contractor;
                worksheetNew.Cells[currentRowNew, 18].Value = item1.Operator;
                worksheetNew.Cells[currentRowNew, 19].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item1.FromDate);
                worksheetNew.Cells[currentRowNew, 20].Value = item1.FromTime;
                worksheetNew.Cells[currentRowNew, 21].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item1.ToDate);
                worksheetNew.Cells[currentRowNew, 22].Value = item1.ToTime;
                worksheetNew.Cells[currentRowNew, 23].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item1.ConfirmFromDate);
                worksheetNew.Cells[currentRowNew, 24].Value = item1.ConfirmFromTime;
                worksheetNew.Cells[currentRowNew, 25].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item1.ConfirmToDate);
                worksheetNew.Cells[currentRowNew, 26].Value = item1.ConfirmToTime;
                worksheetNew.Cells[currentRowNew, 27].Value = item1.ConfirmUser;
                worksheetNew.Cells[currentRowNew, 28].Value = TimeCalculator.TimeCalculator.ConvertToShamsi(item1.ConfirmDate);
                worksheetNew.Cells[currentRowNew, 29].Value = item1.Creator;
                worksheetNew.Cells[currentRowNew, 30].Value = item1.ConfirmedTimeRequired;
                worksheetNew.Cells[currentRowNew, 31].Value = item1.ConfirmedDescription;
                worksheetNew.Cells[currentRowNew, 32].Value = item1.RequestNumber;
                worksheetNew.Cells[currentRowNew, 33].Value = item1.UnitPrice;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

}