using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelEnum;
using Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class OperationLocationExcels
{
    public static byte[] OperationLocationToExcel(
        ICollection<GetsOperationLocationExcelExporterModel> result,
        List<OperationLocationExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("آدرس عملیات");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<OperationLocationExcelEnum, int>();
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
                if (columns.ContainsKey(OperationLocationExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(OperationLocationExcelEnum.Id))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(OperationLocationExcelEnum.PublicName))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.PublicName]].Value = item.PublicName;
                if (columns.ContainsKey(OperationLocationExcelEnum.PrivateName))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.PrivateName]].Value = item.PrivateName;
                if (columns.ContainsKey(OperationLocationExcelEnum.Path))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.Path]].Value = item.Path;
                if (columns.ContainsKey(OperationLocationExcelEnum.PublicCode))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.PublicCode]].Value = item.PublicCode;
                if (columns.ContainsKey(OperationLocationExcelEnum.PrivateCode))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.PrivateCode]].Value = item.PrivateCode;
                if (columns.ContainsKey(OperationLocationExcelEnum.OperationLocationInfo))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.OperationLocationInfo]].Value = item.OperationLocationInfo;
                if (columns.ContainsKey(OperationLocationExcelEnum.ParentId))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.ParentId]].Value = item.ParentId;
                if (columns.ContainsKey(OperationLocationExcelEnum.ParentPublicName))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.ParentPublicName]].Value = item.ParentPublicName;
                if (columns.ContainsKey(OperationLocationExcelEnum.ParentPublicCode))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.ParentPublicCode]].Value = item.ParentPublicCode;
                if (columns.ContainsKey(OperationLocationExcelEnum.CostCenterId))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.CostCenterId]].Value = item.CostCenterId;
                if (columns.ContainsKey(OperationLocationExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(OperationLocationExcelEnum.Priority))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.Priority]].Value = item.Priority;
                if (columns.ContainsKey(OperationLocationExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.IsActive]].Value = item.IsActive;
                if (columns.ContainsKey(OperationLocationExcelEnum.HaveChild))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.HaveChild]].Value = item.HaveChild;
                if (columns.ContainsKey(OperationLocationExcelEnum.ChildCount))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.ChildCount]].Value = item.ChildCount;
                if (columns.ContainsKey(OperationLocationExcelEnum.CompanyId))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.CompanyId]].Value = item.CompanyId;
                if (columns.ContainsKey(OperationLocationExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;
                if (columns.ContainsKey(OperationLocationExcelEnum.ProjectId))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.ProjectId]].Value = item.ProjectId;
                if (columns.ContainsKey(OperationLocationExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[OperationLocationExcelEnum.ProjectName]].Value = item.ProjectName;

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
                worksheet.Cells[1, 1, 1, 21],

                ExcelBorderStyle.Medium);

            worksheet.Cells[1, 1].Value = OperationLocationExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = OperationLocationExcelEnum.Id.GetEnumDescription();
            worksheet.Cells[1, 3].Value = OperationLocationExcelEnum.PublicName.GetEnumDescription();
            worksheet.Cells[1, 4].Value = OperationLocationExcelEnum.PrivateName.GetEnumDescription();
            worksheet.Cells[1, 5].Value = OperationLocationExcelEnum.Path.GetEnumDescription();
            worksheet.Cells[1, 6].Value = OperationLocationExcelEnum.PublicCode.GetEnumDescription();
            worksheet.Cells[1, 7].Value = OperationLocationExcelEnum.PrivateCode.GetEnumDescription();
            worksheet.Cells[1, 8].Value = OperationLocationExcelEnum.OperationLocationInfo.GetEnumDescription();
            worksheet.Cells[1, 9].Value = OperationLocationExcelEnum.ParentId.GetEnumDescription();
            worksheet.Cells[1, 10].Value = OperationLocationExcelEnum.ParentPublicName.GetEnumDescription();
            worksheet.Cells[1, 11].Value = OperationLocationExcelEnum.ParentPublicCode.GetEnumDescription();
            worksheet.Cells[1, 12].Value = OperationLocationExcelEnum.CostCenterId.GetEnumDescription();
            worksheet.Cells[1, 13].Value = OperationLocationExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[1, 14].Value = OperationLocationExcelEnum.Priority.GetEnumDescription();
            worksheet.Cells[1, 15].Value = OperationLocationExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 16].Value = OperationLocationExcelEnum.HaveChild.GetEnumDescription();
            worksheet.Cells[1, 17].Value = OperationLocationExcelEnum.ChildCount.GetEnumDescription();
            worksheet.Cells[1, 18].Value = OperationLocationExcelEnum.CompanyId.GetEnumDescription();
            worksheet.Cells[1, 19].Value = OperationLocationExcelEnum.CompanyNameFa.GetEnumDescription();
            worksheet.Cells[1, 20].Value = OperationLocationExcelEnum.ProjectId.GetEnumDescription();
            worksheet.Cells[1, 21].Value = OperationLocationExcelEnum.ProjectName.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.Id;
                worksheet.Cells[currentRow, 3].Value = item.PublicName;
                worksheet.Cells[currentRow, 4].Value = item.PrivateName;
                worksheet.Cells[currentRow, 5].Value = item.Path;
                worksheet.Cells[currentRow, 6].Value = item.PublicCode;
                worksheet.Cells[currentRow, 7].Value = item.PrivateCode;
                worksheet.Cells[currentRow, 8].Value = item.OperationLocationInfo;
                worksheet.Cells[currentRow, 9].Value = item.ParentId;
                worksheet.Cells[currentRow, 10].Value = item.ParentPublicName;
                worksheet.Cells[currentRow, 11].Value = item.ParentPublicCode;
                worksheet.Cells[currentRow, 12].Value = item.CostCenterId;
                worksheet.Cells[currentRow, 13].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 14].Value = item.Priority;
                worksheet.Cells[currentRow, 15].Value = item.IsActive;
                worksheet.Cells[currentRow, 16].Value = item.HaveChild;
                worksheet.Cells[currentRow, 17].Value = item.ChildCount;
                worksheet.Cells[currentRow, 18].Value = item.CompanyId;
                worksheet.Cells[currentRow, 19].Value = item.CompanyNameFa;
                worksheet.Cells[currentRow, 20].Value = item.ProjectId;
                worksheet.Cells[currentRow, 21].Value = item.ProjectName;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        ExcelStyles.SetSummaryCellStyle(
            worksheet.Cells[2, 23, 4, 24],
            worksheet,
            result.Count(c => c.IsActive),
            result.Count,
            21);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}