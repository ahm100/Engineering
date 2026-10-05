using Engineering.Application.Services.RequestMachineryManagements.Models.GetsRequestMachineryManagementExcelExporter;
using Engineering.Application.Services.RequestMachineryManagements.Models.RequestMachineryManagementExcelEnums;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class RequestMachineryManagementExcels
{
    public static byte[] RequestMachineryManagementToExcel(
        ICollection<GetsRequestMachineryManagementExcelExporterResponseModel> result,
        List<RequestMachineryManagementExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("مدیریت درخواست ماشین آلات");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<RequestMachineryManagementExcelEnum, int>();
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
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.Id))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.Created))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.Created]].Value = item.Created;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.MachineryGroupName))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.MachineryGroupName]].Value = item.MachineryGroupName;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.MachineryName))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.MachineryName]].Value = item.MachineryName;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.TimeRequired))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.TimeRequired]].Value = item.TimeRequired;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.UnitDescription))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.UnitDescription]].Value = item.UnitDescription;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.StatusDescription))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.RequestCount))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.RequestCount]].Value = item.RequestCount;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.FromDate))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.FromDate]].Value = item.FromDate;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ToDate))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ToDate]].Value = item.ToDate;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.CreatorId))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.CreatorId]].Value = item.CreatorId;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.Creator))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.Creator]].Value = item.Creator;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ProjectOperations))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ProjectOperations]].Value = item.ProjectOperations;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ProjectOperationDetails))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ProjectOperationDetails]].Value = item.ProjectOperationDetails;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ConfirmDate))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ConfirmDate]].Value = item.ConfirmDate;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ConfirmUserId))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ConfirmUserId]].Value = item.ConfirmUserId;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ConfirmUser))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ConfirmUser]].Value = item.ConfirmUser;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.AppointmentId))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.AppointmentId]].Value = item.AppointmentId;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.AppointmentFullName))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.AppointmentFullName]].Value = item.AppointmentFullName;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.CompanyId))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.CompanyId]].Value = item.CompanyId;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.FromTime))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.FromTime]].Value = item.FromTime;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ToTime))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ToTime]].Value = item.ToTime;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ConfirmFromDate))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ConfirmFromDate]].Value = item.ConfirmFromDate;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ConfirmFromTime))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ConfirmFromTime]].Value = item.ConfirmFromTime;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ConfirmToDate))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ConfirmToDate]].Value = item.ConfirmToDate;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ConfirmToTime))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ConfirmToTime]].Value = item.ConfirmToTime;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ChangeStatusDescription))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ChangeStatusDescription]].Value = item.changeStatusDescription;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ConfirmedTimeRequired))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ConfirmedTimeRequired]].Value = item.ConfirmedTimeRequired;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.ConfirmedDescription))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.ConfirmedDescription]].Value = item.ConfirmedDescription;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.Contractor))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.Contractor]].Value = item.Contractor;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.RequestNumber))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.RequestNumber]].Value = item.RequestNumber;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.UnitPrice))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.UnitPrice]].Value = item.UnitPrice;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.TotalPrice))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.TotalPrice]].Value = item.TotalPrice;
                if (columns.ContainsKey(RequestMachineryManagementExcelEnum.PaymentType))
                    worksheet.Cells[currentRow, columns[RequestMachineryManagementExcelEnum.PaymentType]].Value = item.PaymentTypeTitle;

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
                worksheet.Cells[1, 1, 1, 35],

                ExcelBorderStyle.Medium);

            worksheet.Cells[currentRow, 1].Value = RequestMachineryManagementExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = RequestMachineryManagementExcelEnum.Id.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = RequestMachineryManagementExcelEnum.Created.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = RequestMachineryManagementExcelEnum.MachineryGroupName.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = RequestMachineryManagementExcelEnum.MachineryName.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = RequestMachineryManagementExcelEnum.TimeRequired.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = RequestMachineryManagementExcelEnum.UnitDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = RequestMachineryManagementExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = RequestMachineryManagementExcelEnum.RequestCount.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = RequestMachineryManagementExcelEnum.FromDate.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = RequestMachineryManagementExcelEnum.ToDate.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = RequestMachineryManagementExcelEnum.CreatorId.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = RequestMachineryManagementExcelEnum.Creator.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = RequestMachineryManagementExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[currentRow, 15].Value = RequestMachineryManagementExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cells[currentRow, 16].Value = RequestMachineryManagementExcelEnum.ProjectOperations.GetEnumDescription();
            worksheet.Cells[currentRow, 17].Value = RequestMachineryManagementExcelEnum.ProjectOperationDetails.GetEnumDescription();
            worksheet.Cells[currentRow, 18].Value = RequestMachineryManagementExcelEnum.ConfirmDate.GetEnumDescription();
            worksheet.Cells[currentRow, 19].Value = RequestMachineryManagementExcelEnum.ConfirmUserId.GetEnumDescription();
            worksheet.Cells[currentRow, 20].Value = RequestMachineryManagementExcelEnum.ConfirmUser.GetEnumDescription();
            worksheet.Cells[currentRow, 21].Value = RequestMachineryManagementExcelEnum.AppointmentId.GetEnumDescription();
            worksheet.Cells[currentRow, 22].Value = RequestMachineryManagementExcelEnum.AppointmentFullName.GetEnumDescription();
            worksheet.Cells[currentRow, 23].Value = RequestMachineryManagementExcelEnum.CompanyId.GetEnumDescription();
            worksheet.Cells[currentRow, 24].Value = RequestMachineryManagementExcelEnum.CompanyNameFa.GetEnumDescription();
            worksheet.Cells[currentRow, 25].Value = RequestMachineryManagementExcelEnum.FromTime.GetEnumDescription();
            worksheet.Cells[currentRow, 26].Value = RequestMachineryManagementExcelEnum.ToTime.GetEnumDescription();
            worksheet.Cells[currentRow, 27].Value = RequestMachineryManagementExcelEnum.ConfirmFromDate.GetEnumDescription();
            worksheet.Cells[currentRow, 28].Value = RequestMachineryManagementExcelEnum.ConfirmFromTime.GetEnumDescription();
            worksheet.Cells[currentRow, 29].Value = RequestMachineryManagementExcelEnum.ConfirmToDate.GetEnumDescription();
            worksheet.Cells[currentRow, 30].Value = RequestMachineryManagementExcelEnum.ConfirmToTime.GetEnumDescription();
            worksheet.Cells[currentRow, 31].Value = RequestMachineryManagementExcelEnum.ChangeStatusDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 32].Value = RequestMachineryManagementExcelEnum.ConfirmedTimeRequired.GetEnumDescription();
            worksheet.Cells[currentRow, 33].Value = RequestMachineryManagementExcelEnum.ConfirmedDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 34].Value = RequestMachineryManagementExcelEnum.Contractor.GetEnumDescription();
            worksheet.Cells[currentRow, 35].Value = RequestMachineryManagementExcelEnum.RequestNumber.GetEnumDescription();
            worksheet.Cells[currentRow, 36].Value = RequestMachineryManagementExcelEnum.UnitPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 37].Value = RequestMachineryManagementExcelEnum.TotalPrice.GetEnumDescription();
            worksheet.Cells[currentRow, 38].Value = RequestMachineryManagementExcelEnum.PaymentType.GetEnumDescription();

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
                worksheet.Cells[currentRow, 21].Value = item.AppointmentId;
                worksheet.Cells[currentRow, 22].Value = item.AppointmentFullName;
                worksheet.Cells[currentRow, 23].Value = item.CompanyId;
                worksheet.Cells[currentRow, 24].Value = item.CompanyNameFa;
                worksheet.Cells[currentRow, 25].Value = item.FromTime;
                worksheet.Cells[currentRow, 26].Value = item.ToTime;
                worksheet.Cells[currentRow, 27].Value = item.ConfirmFromDate;
                worksheet.Cells[currentRow, 28].Value = item.ConfirmFromTime;
                worksheet.Cells[currentRow, 29].Value = item.ConfirmToDate;
                worksheet.Cells[currentRow, 30].Value = item.ConfirmToTime;
                worksheet.Cells[currentRow, 31].Value = item.changeStatusDescription;
                worksheet.Cells[currentRow, 32].Value = item.ConfirmedTimeRequired;
                worksheet.Cells[currentRow, 33].Value = item.ConfirmedDescription;
                worksheet.Cells[currentRow, 34].Value = item.Contractor;
                worksheet.Cells[currentRow, 35].Value = item.RequestNumber;
                worksheet.Cells[currentRow, 36].Value = item.UnitPrice;
                worksheet.Cells[currentRow, 37].Value = item.TotalPrice;
                worksheet.Cells[currentRow, 38].Value = item.PaymentTypeTitle;

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