using Engineering.Application.Services.Projects.Models.GetsProjectExcelEnum;
using Engineering.Application.Services.Projects.Models.GetsProjectExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class ProjectExcels
{
    public static byte[] ProjectToExcel(
        ICollection<GetsProjectExcelExporterModel> result,
        List<ProjectExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("پروژه");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<ProjectExcelEnum, int>();
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
                if (columns.ContainsKey(ProjectExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(ProjectExcelEnum.Id))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(ProjectExcelEnum.ProjectTypeId))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.ProjectTypeId]].Value = item.ProjectTypeId;
                if (columns.ContainsKey(ProjectExcelEnum.ProjectTypeTitle))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.ProjectTypeTitle]].Value = item.ProjectTypeTitle;
                if (columns.ContainsKey(ProjectExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(ProjectExcelEnum.ProjectCode))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.ProjectCode]].Value = item.ProjectCode;
                if (columns.ContainsKey(ProjectExcelEnum.EmployerId))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.EmployerId]].Value = item.EmployerId;
                if (columns.ContainsKey(ProjectExcelEnum.EmployerName))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.EmployerName]].Value = item.EmployerName;
                if (columns.ContainsKey(ProjectExcelEnum.CategoryId))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.CategoryId]].Value = item.CategoryId;
                if (columns.ContainsKey(ProjectExcelEnum.CategoryName))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.CategoryName]].Value = item.CategoryName;
                if (columns.ContainsKey(ProjectExcelEnum.CostCenterId))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.CostCenterId]].Value = item.CostCenterId;
                if (columns.ContainsKey(ProjectExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(ProjectExcelEnum.SupervisorEngineer))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.SupervisorEngineer]].Value = item.SupervisorEngineer;
                if (columns.ContainsKey(ProjectExcelEnum.SupervisorEngineerName))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.SupervisorEngineerName]].Value = item.SupervisorEngineerName;
                if (columns.ContainsKey(ProjectExcelEnum.AdvisorId))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.AdvisorId]].Value = item.AdvisorId;
                if (columns.ContainsKey(ProjectExcelEnum.AdvisorName))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.AdvisorName]].Value = item.AdvisorName;
                if (columns.ContainsKey(ProjectExcelEnum.ProjectManagerId))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.ProjectManagerId]].Value = item.ProjectManagerId;
                if (columns.ContainsKey(ProjectExcelEnum.ProjectManagerName))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.ProjectManagerName]].Value = item.ProjectManagerName;
                if (columns.ContainsKey(ProjectExcelEnum.PlanningAssistantId))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.PlanningAssistantId]].Value = item.PlanningAssistantId;
                if (columns.ContainsKey(ProjectExcelEnum.PlanningAssistantName))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.PlanningAssistantName]].Value = item.PlanningAssistantName;
                if (columns.ContainsKey(ProjectExcelEnum.StatusTitle))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.StatusTitle]].Value = item.StatusTitle;
                if (columns.ContainsKey(ProjectExcelEnum.IsActive))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.IsActive]].Value = item.IsActive;
                if (columns.ContainsKey(ProjectExcelEnum.CompanyId))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.CompanyId]].Value = item.CompanyId;
                if (columns.ContainsKey(ProjectExcelEnum.CompanyNameFa))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;
                if (columns.ContainsKey(ProjectExcelEnum.Contractual))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.Contractual]].Value = item.Contractual;
                if (columns.ContainsKey(ProjectExcelEnum.City))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.City]].Value = item.City;
                if (columns.ContainsKey(ProjectExcelEnum.Description))
                    worksheet.Cells[currentRow, columns[ProjectExcelEnum.Description]].Value = item.Description;

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

            worksheet.Cells[1, 1].Value = ProjectExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[1, 2].Value = ProjectExcelEnum.Id.GetEnumDescription();
            worksheet.Cells[1, 3].Value = ProjectExcelEnum.ProjectTypeId.GetEnumDescription();
            worksheet.Cells[1, 4].Value = ProjectExcelEnum.ProjectTypeTitle.GetEnumDescription();
            worksheet.Cells[1, 5].Value = ProjectExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cells[1, 6].Value = ProjectExcelEnum.ProjectCode.GetEnumDescription();
            worksheet.Cells[1, 7].Value = ProjectExcelEnum.EmployerId.GetEnumDescription();
            worksheet.Cells[1, 8].Value = ProjectExcelEnum.EmployerName.GetEnumDescription();
            worksheet.Cells[1, 9].Value = ProjectExcelEnum.CategoryId.GetEnumDescription();
            worksheet.Cells[1, 10].Value = ProjectExcelEnum.CategoryName.GetEnumDescription();
            worksheet.Cells[1, 11].Value = ProjectExcelEnum.CostCenterId.GetEnumDescription();
            worksheet.Cells[1, 12].Value = ProjectExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[1, 13].Value = ProjectExcelEnum.SupervisorEngineer.GetEnumDescription();
            worksheet.Cells[1, 14].Value = ProjectExcelEnum.SupervisorEngineerName.GetEnumDescription();
            worksheet.Cells[1, 15].Value = ProjectExcelEnum.AdvisorId.GetEnumDescription();
            worksheet.Cells[1, 16].Value = ProjectExcelEnum.AdvisorName.GetEnumDescription();
            worksheet.Cells[1, 17].Value = ProjectExcelEnum.ProjectManagerId.GetEnumDescription();
            worksheet.Cells[1, 18].Value = ProjectExcelEnum.ProjectManagerName.GetEnumDescription();
            worksheet.Cells[1, 19].Value = ProjectExcelEnum.PlanningAssistantId.GetEnumDescription();
            worksheet.Cells[1, 20].Value = ProjectExcelEnum.PlanningAssistantName.GetEnumDescription();
            worksheet.Cells[1, 21].Value = ProjectExcelEnum.StatusTitle.GetEnumDescription();
            worksheet.Cells[1, 22].Value = ProjectExcelEnum.IsActive.GetEnumDescription();
            worksheet.Cells[1, 23].Value = ProjectExcelEnum.CompanyId.GetEnumDescription();
            worksheet.Cells[1, 24].Value = ProjectExcelEnum.CompanyNameFa.GetEnumDescription();
            worksheet.Cells[1, 25].Value = ProjectExcelEnum.Contractual.GetEnumDescription();
            worksheet.Cells[1, 26].Value = ProjectExcelEnum.City.GetEnumDescription();
            worksheet.Cells[1, 27].Value = ProjectExcelEnum.Description.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.Id;
                worksheet.Cells[currentRow, 3].Value = item.ProjectTypeId;
                worksheet.Cells[currentRow, 4].Value = item.ProjectTypeTitle;
                worksheet.Cells[currentRow, 5].Value = item.ProjectName;
                worksheet.Cells[currentRow, 6].Value = item.ProjectCode;
                worksheet.Cells[currentRow, 7].Value = item.EmployerId;
                worksheet.Cells[currentRow, 8].Value = item.EmployerName;
                worksheet.Cells[currentRow, 9].Value = item.CategoryId;
                worksheet.Cells[currentRow, 10].Value = item.CategoryName;
                worksheet.Cells[currentRow, 11].Value = item.CostCenterId;
                worksheet.Cells[currentRow, 12].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 13].Value = item.SupervisorEngineer;
                worksheet.Cells[currentRow, 14].Value = item.SupervisorEngineerName;
                worksheet.Cells[currentRow, 15].Value = item.AdvisorId;
                worksheet.Cells[currentRow, 16].Value = item.AdvisorName;
                worksheet.Cells[currentRow, 17].Value = item.ProjectManagerId;
                worksheet.Cells[currentRow, 18].Value = item.ProjectManagerName;
                worksheet.Cells[currentRow, 19].Value = item.PlanningAssistantId;
                worksheet.Cells[currentRow, 20].Value = item.PlanningAssistantName;
                worksheet.Cells[currentRow, 21].Value = item.StatusTitle;
                worksheet.Cells[currentRow, 22].Value = item.IsActive;
                worksheet.Cells[currentRow, 23].Value = item.CompanyId;
                worksheet.Cells[currentRow, 24].Value = item.CompanyNameFa;
                worksheet.Cells[currentRow, 25].Value = item.Contractual;
                worksheet.Cells[currentRow, 26].Value = item.City;
                worksheet.Cells[currentRow, 27].Value = item.Description;

                ExcelStyles.SetCellStyle(
                    worksheet,
                    currentRow - 1,
                    currentRow,
                    worksheet.Dimension.Start.Column,
                    worksheet.Dimension.End.Column);
            }
        }

        ExcelStyles.SetSummaryCellStyle(
            worksheet.Cells[2, 27, 4, 28],
            worksheet,
            result.Count(c => c.IsActive),
            result.Count,
            25);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}