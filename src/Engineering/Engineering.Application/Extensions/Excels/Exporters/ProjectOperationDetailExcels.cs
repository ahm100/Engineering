using Engineering.Application.Services.ProjectOperationDetails.Models.GetsByProjectOperationIdExcelEnums;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsByProjectOperationIdExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorDetailReportsExcelEnum;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorDetailReportsExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorReportsExcelEnum;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorReportsExcelExporter;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReportingExcelEnum;
using Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReportingExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class ProjectOperationDetailExcels
{
    public static byte[] ProjectOperationDetailToExcel(
        ICollection<GetsByProjectOperationIdExcelExporterResponseModel> result,
        TotalProjectOperationDetailDataExcelExporterModel total,
        List<ImplementationAssistantsExcelExporterModel>? implementationAssistants,
        List<TechnicalAssistantsExcelExporterModel>? techninalAssistants,
        List<PlannerAssistantsExcelExporterModel>? plannerAssistants,
        List<ProjectOperationDetailExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet1 = workbook.Workbook.Worksheets.Add("ریزمتره");
        var worksheet2 = workbook.Workbook.Worksheets.Add("مجموع ابعاد ریزمتره");
        var worksheet3 = workbook.Workbook.Worksheets.Add("دستیاران برنامه ریزی");
        var worksheet4 = workbook.Workbook.Worksheets.Add("دستیاران فنی");
        var worksheet5 = workbook.Workbook.Worksheets.Add("دستیاران  پیاده سازی");
        worksheet1.View.RightToLeft = true;
        worksheet2.View.RightToLeft = true;
        worksheet3.View.RightToLeft = true;
        worksheet4.View.RightToLeft = true;
        worksheet5.View.RightToLeft = true;

        var currentRow1 = 1;
        var currentRow2 = 1;
        var currentRow3 = 1;
        var currentRow4 = 1;
        var currentRow5 = 1;

        #region ریزمتره
        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<ProjectOperationDetailExcelEnum, int>();
            for (int counter = 0; counter < excelFilters.Count; counter++)
            {
                worksheet1.Cells[1, counter + 1].Value = excelFilters[counter].GetEnumDescription();
                columns[excelFilters[counter]] = counter + 1;

                ExcelStyles.SetHeaderStyle(
                    worksheet1.Cells[1, 1, 1, counter + 1],

                    ExcelBorderStyle.Medium);
            }

            foreach (var item in result)
            {
                currentRow1++;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Row))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Row]].Value = currentRow1 - 1;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Id))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.OperationLocationId))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.OperationLocationId]].Value = item.OperationLocationId;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.PublicName))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.PublicName]].Value = item.PublicName;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.PublicCode))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.PublicCode]].Value = item.PublicCode;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.ProjectOperationId))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.ProjectOperationId]].Value = item.ProjectOperationId;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.OperationInfoId))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.OperationInfoId]].Value = item.OperationInfoId;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.OperationInfoName))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.OperationInfoName]].Value = item.OperationInfoName;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.ProjectId))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.ProjectId]].Value = item.ProjectId;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.ProjectName))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.CostCenterId))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.CostCenterId]].Value = item.CostCenterId;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.CostCenterName))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.ProjectManagerId))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.ProjectManagerId]].Value = item.ProjectManagerId;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.ProjectManagerName))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.ProjectManagerName]].Value = item.ProjectManagerName;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.StartDate))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.StartDate]].Value = item.StartDate;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.EndDate))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.EndDate]].Value = item.EndDate;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Length))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Length]].Value = item.Length;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Width))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Width]].Value = item.Width;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Height))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Height]].Value = item.Height;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Weight))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Weight]].Value = item.Weight;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Number))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Number]].Value = item.Number;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.FinalAmount))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.FinalAmount]].Value = item.FinalAmount;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.StatusDescription))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Priority))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Priority]].Value = item.Priority;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Day))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Day]].Value = item.Day;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Hour))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Hour]].Value = item.Hour;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Description))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Description]].Value = item.Description;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.CreatorId))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.CreatorId]].Value = item.CreatorId;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Creator))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Creator]].Value = item.Creator;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.UpdatorId))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.UpdatorId]].Value = item.UpdatorId;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Updator))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Updator]].Value = item.Updator;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.CreateDate))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.CreateDate]].Value = item.CreateDate;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.UpdateDate))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.UpdateDate]].Value = item.ModifyDate;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.CompanyId))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.CompanyId]].Value = item.CompanyId;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.CompanyNameFa))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.CompanyNameFa]].Value = item.CompanyNameFa;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Code))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Code]].Value = item.Code;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.PrivateName))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.PrivateName]].Value = item.PrivateName;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.PrivateCode))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.PrivateCode]].Value = item.PrivateCode;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.UsedFinalAmount))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.UsedFinalAmount]].Value = item.UsedFinalAmount;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.Contractors))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.Contractors]].Value = item.Contractors;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.ContractorNicknames))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.ContractorNicknames]].Value = item.ContractorNicknames;
                if (columns.ContainsKey(ProjectOperationDetailExcelEnum.WorkLoad))
                    worksheet1.Cells[currentRow1, columns[ProjectOperationDetailExcelEnum.WorkLoad]].Value = item.WorkLoad;

                ExcelStyles.SetCellStyle(
                    worksheet1,
                    currentRow1 - 1,
                    currentRow1,
                    worksheet1.Dimension.Start.Column,
                    worksheet1.Dimension.End.Column);
            }
        }
        else
        {
            ExcelStyles.SetHeaderStyle(
                worksheet1.Cells[1, 1, 1, 42],

                ExcelBorderStyle.Medium);

            worksheet1.Cells[currentRow1, 1].Value = ProjectOperationDetailExcelEnum.Row.GetEnumDescription();
            worksheet1.Cells[currentRow1, 2].Value = ProjectOperationDetailExcelEnum.Id.GetEnumDescription();
            worksheet1.Cells[currentRow1, 3].Value = ProjectOperationDetailExcelEnum.OperationLocationId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 4].Value = ProjectOperationDetailExcelEnum.PublicName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 5].Value = ProjectOperationDetailExcelEnum.PublicCode.GetEnumDescription();
            worksheet1.Cells[currentRow1, 6].Value = ProjectOperationDetailExcelEnum.ProjectOperationId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 7].Value = ProjectOperationDetailExcelEnum.OperationInfoId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 8].Value = ProjectOperationDetailExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 9].Value = ProjectOperationDetailExcelEnum.ProjectId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 10].Value = ProjectOperationDetailExcelEnum.ProjectName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 11].Value = ProjectOperationDetailExcelEnum.CostCenterId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 12].Value = ProjectOperationDetailExcelEnum.CostCenterName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 13].Value = ProjectOperationDetailExcelEnum.ProjectManagerId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 14].Value = ProjectOperationDetailExcelEnum.ProjectManagerName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 15].Value = ProjectOperationDetailExcelEnum.StartDate.GetEnumDescription();
            worksheet1.Cells[currentRow1, 16].Value = ProjectOperationDetailExcelEnum.EndDate.GetEnumDescription();
            worksheet1.Cells[currentRow1, 17].Value = ProjectOperationDetailExcelEnum.Length.GetEnumDescription();
            worksheet1.Cells[currentRow1, 18].Value = ProjectOperationDetailExcelEnum.Width.GetEnumDescription();
            worksheet1.Cells[currentRow1, 19].Value = ProjectOperationDetailExcelEnum.Height.GetEnumDescription();
            worksheet1.Cells[currentRow1, 20].Value = ProjectOperationDetailExcelEnum.Weight.GetEnumDescription();
            worksheet1.Cells[currentRow1, 21].Value = ProjectOperationDetailExcelEnum.Number.GetEnumDescription();
            worksheet1.Cells[currentRow1, 22].Value = ProjectOperationDetailExcelEnum.FinalAmount.GetEnumDescription();
            worksheet1.Cells[currentRow1, 23].Value = ProjectOperationDetailExcelEnum.StatusDescription.GetEnumDescription();
            worksheet1.Cells[currentRow1, 24].Value = ProjectOperationDetailExcelEnum.Priority.GetEnumDescription();
            worksheet1.Cells[currentRow1, 25].Value = ProjectOperationDetailExcelEnum.Day.GetEnumDescription();
            worksheet1.Cells[currentRow1, 26].Value = ProjectOperationDetailExcelEnum.Hour.GetEnumDescription();
            worksheet1.Cells[currentRow1, 27].Value = ProjectOperationDetailExcelEnum.Description.GetEnumDescription();
            worksheet1.Cells[currentRow1, 28].Value = ProjectOperationDetailExcelEnum.CreatorId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 29].Value = ProjectOperationDetailExcelEnum.Creator.GetEnumDescription();
            worksheet1.Cells[currentRow1, 30].Value = ProjectOperationDetailExcelEnum.UpdatorId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 31].Value = ProjectOperationDetailExcelEnum.Updator.GetEnumDescription();
            worksheet1.Cells[currentRow1, 32].Value = ProjectOperationDetailExcelEnum.CreateDate.GetEnumDescription();
            worksheet1.Cells[currentRow1, 33].Value = ProjectOperationDetailExcelEnum.UpdateDate.GetEnumDescription();
            worksheet1.Cells[currentRow1, 34].Value = ProjectOperationDetailExcelEnum.CompanyId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 35].Value = ProjectOperationDetailExcelEnum.CompanyNameFa.GetEnumDescription();
            worksheet1.Cells[currentRow1, 36].Value = ProjectOperationDetailExcelEnum.Code.GetEnumDescription();
            worksheet1.Cells[currentRow1, 37].Value = ProjectOperationDetailExcelEnum.PrivateName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 38].Value = ProjectOperationDetailExcelEnum.PrivateCode.GetEnumDescription();
            worksheet1.Cells[currentRow1, 39].Value = ProjectOperationDetailExcelEnum.UsedFinalAmount.GetEnumDescription();
            worksheet1.Cells[currentRow1, 40].Value = ProjectOperationDetailExcelEnum.Contractors.GetEnumDescription();
            worksheet1.Cells[currentRow1, 41].Value = ProjectOperationDetailExcelEnum.ContractorNicknames.GetEnumDescription();
            worksheet1.Cells[currentRow1, 42].Value = ProjectOperationDetailExcelEnum.WorkLoad.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow1++;
                worksheet1.Cells[currentRow1, 1].Value = currentRow1 - 1;
                worksheet1.Cells[currentRow1, 2].Value = item.Id;
                worksheet1.Cells[currentRow1, 3].Value = item.OperationLocationId;
                worksheet1.Cells[currentRow1, 4].Value = item.PublicName;
                worksheet1.Cells[currentRow1, 5].Value = item.PublicCode;
                worksheet1.Cells[currentRow1, 6].Value = item.ProjectOperationId;
                worksheet1.Cells[currentRow1, 7].Value = item.OperationInfoId;
                worksheet1.Cells[currentRow1, 8].Value = item.OperationInfoName;
                worksheet1.Cells[currentRow1, 9].Value = item.ProjectId;
                worksheet1.Cells[currentRow1, 10].Value = item.ProjectName;
                worksheet1.Cells[currentRow1, 11].Value = item.CostCenterId;
                worksheet1.Cells[currentRow1, 12].Value = item.CostCenterName;
                worksheet1.Cells[currentRow1, 13].Value = item.ProjectManagerId;
                worksheet1.Cells[currentRow1, 14].Value = item.ProjectManagerName;
                worksheet1.Cells[currentRow1, 15].Value = item.ShamsiStartDate;
                worksheet1.Cells[currentRow1, 16].Value = item.ShamsiEndDate;
                worksheet1.Cells[currentRow1, 17].Value = item.Length;
                worksheet1.Cells[currentRow1, 18].Value = item.Width;
                worksheet1.Cells[currentRow1, 19].Value = item.Height;
                worksheet1.Cells[currentRow1, 20].Value = item.Weight;
                worksheet1.Cells[currentRow1, 21].Value = item.Number;
                worksheet1.Cells[currentRow1, 22].Value = item.FinalAmount;
                worksheet1.Cells[currentRow1, 23].Value = item.StatusDescription;
                worksheet1.Cells[currentRow1, 24].Value = item.Priority;
                worksheet1.Cells[currentRow1, 25].Value = item.Day;
                worksheet1.Cells[currentRow1, 26].Value = item.Hour;
                worksheet1.Cells[currentRow1, 27].Value = item.Description;
                worksheet1.Cells[currentRow1, 28].Value = item.CreatorId;
                worksheet1.Cells[currentRow1, 29].Value = item.Creator;
                worksheet1.Cells[currentRow1, 30].Value = item.UpdatorId;
                worksheet1.Cells[currentRow1, 31].Value = item.Updator;
                worksheet1.Cells[currentRow1, 32].Value = item.ShamsiCreateDate;
                worksheet1.Cells[currentRow1, 33].Value = item.ShamsiModifyDate;
                worksheet1.Cells[currentRow1, 34].Value = item.CompanyId;
                worksheet1.Cells[currentRow1, 35].Value = item.CompanyNameFa;
                worksheet1.Cells[currentRow1, 36].Value = item.Code;
                worksheet1.Cells[currentRow1, 37].Value = item.PrivateName;
                worksheet1.Cells[currentRow1, 38].Value = item.PrivateCode;
                worksheet1.Cells[currentRow1, 39].Value = item.UsedFinalAmount;
                worksheet1.Cells[currentRow1, 40].Value = item.Contractors;
                worksheet1.Cells[currentRow1, 41].Value = item.ContractorNicknames;
                worksheet1.Cells[currentRow1, 42].Value = item.WorkLoad;

                ExcelStyles.SetCellStyle(
                    worksheet1,
                    currentRow1 - 1,
                    currentRow1,
                    worksheet1.Dimension.Start.Column,
                    worksheet1.Dimension.End.Column);
            }
        }

        var lastColumn = worksheet1.Dimension.End.Column;

        ExcelStyles.SetSummaryCellData(
            worksheet1.Cells[2, lastColumn + 2, 5, lastColumn + 3],
            worksheet1,
            lastColumn);

        worksheet1.Cells[2, lastColumn + 2].Value = ProjectOperationDetailExcelEnum.FinalAmount.GetEnumDescription();
        worksheet1.Cells[3, lastColumn + 2].Value = "برآورد تاریخ شروع";
        worksheet1.Cells[4, lastColumn + 2].Value = "برآورد تاریخ پایان";
        worksheet1.Cells[5, lastColumn + 2].Value = "مجموع";

        worksheet1.Cells[2, lastColumn + 3].Value = result.Select(s => s.FinalAmount).Count();
        worksheet1.Cells[3, lastColumn + 3].Value = result.Select(s => s.ShamsiStartDate).Min();
        worksheet1.Cells[4, lastColumn + 3].Value = result.Select(s => s.ShamsiEndDate).Max();
        worksheet1.Cells[5, lastColumn + 3].Value = result.Count;

        #endregion

        #region مجموع ابعاد ریزمتره
        ExcelStyles.SetHeaderStyle(
            worksheet2.Cells[1, 1, 1, 11],

            ExcelBorderStyle.Medium);

        worksheet2.Cells[currentRow2, 1].Value = ProjectOperationDetailExcelEnum.Row.GetEnumDescription();
        worksheet2.Cells[currentRow2, 2].Value = "جمع طول";
        worksheet2.Cells[currentRow2, 3].Value = "جمع عرض";
        worksheet2.Cells[currentRow2, 4].Value = "جمع ارتفاع";
        worksheet2.Cells[currentRow2, 5].Value = "جمع وزن";
        worksheet2.Cells[currentRow2, 6].Value = "جمع تعداد";
        worksheet2.Cells[currentRow2, 7].Value = "جمع احجام ریزمتره کم شده از کسورات";
        worksheet2.Cells[currentRow2, 8].Value = "حجم شرح عملیات ریزمتره";
        worksheet2.Cells[currentRow2, 9].Value = "حجم کسورات";
        worksheet2.Cells[currentRow2, 10].Value = "مجموع احجام کارکرد ها";
        worksheet2.Cells[currentRow2, 11].Value = "حجم شرح عملیات";

        currentRow2++;
        worksheet2.Cells[currentRow2, 1].Value = currentRow2 - 1;
        worksheet2.Cells[currentRow2, 2].Value = total.TotalLengths;
        worksheet2.Cells[currentRow2, 3].Value = total.TotalWidths;
        worksheet2.Cells[currentRow2, 4].Value = total.TotalHeights;
        worksheet2.Cells[currentRow2, 5].Value = total.TotalWeights;
        worksheet2.Cells[currentRow2, 6].Value = total.TotalNumbers;
        worksheet2.Cells[currentRow2, 7].Value = total.TotalAmounts;
        worksheet2.Cells[currentRow2, 8].Value = total.TotalFinalAmounts;
        worksheet2.Cells[currentRow2, 9].Value = total.TotalDeductionAmounts;
        worksheet2.Cells[currentRow2, 10].Value = total.DailyFinalAmounts;
        worksheet2.Cells[currentRow2, 11].Value = total.ProjectOperationWorkload;

        ExcelStyles.SetCellStyle(
            worksheet2,
            currentRow2 - 1,
            currentRow2,
            worksheet2.Dimension.Start.Column,
            worksheet2.Dimension.End.Column);
        #endregion

        #region دستیاران برنامه ریزی
        if (plannerAssistants != null && plannerAssistants.Count > 0)
        {
            ExcelStyles.SetHeaderStyle(
                worksheet3.Cells[1, 1, 1, 5],

                ExcelBorderStyle.Medium);

            worksheet3.Cells[currentRow3, 1].Value = ProjectOperationDetailExcelEnum.Row.GetEnumDescription();
            worksheet3.Cells[currentRow3, 2].Value = "شناسه ریزمتره";
            worksheet3.Cells[currentRow3, 3].Value = "شناسه دستیار";
            worksheet3.Cells[currentRow3, 4].Value = "نام";
            worksheet3.Cells[currentRow3, 5].Value = "نام مستعار";

            foreach (var item in plannerAssistants)
            {
                currentRow3++;
                worksheet3.Cells[currentRow3, 1].Value = currentRow3 - 1;
                worksheet3.Cells[currentRow3, 2].Value = item.ProjectOpertationDetailId;
                worksheet3.Cells[currentRow3, 3].Value = item.PlannerAssistantId;
                worksheet3.Cells[currentRow3, 4].Value = item.PlannerAssistantName;
                worksheet3.Cells[currentRow3, 5].Value = item.PlannerAssistantNickName;

                ExcelStyles.SetCellStyle(
                    worksheet3,
                    currentRow3 - 1,
                    currentRow3,
                    worksheet3.Dimension.Start.Column,
                    worksheet3.Dimension.End.Column);
            }
        }
        #endregion

        #region دستیاران فنی
        if (techninalAssistants != null && techninalAssistants.Count > 0)
        {
            ExcelStyles.SetHeaderStyle(
                worksheet4.Cells[1, 1, 1, 5],

                ExcelBorderStyle.Medium);

            worksheet4.Cells[currentRow4, 1].Value = ProjectOperationDetailExcelEnum.Row.GetEnumDescription();
            worksheet4.Cells[currentRow4, 2].Value = "شناسه ریزمتره";
            worksheet4.Cells[currentRow4, 3].Value = "شناسه دستیار";
            worksheet4.Cells[currentRow4, 4].Value = "نام";
            worksheet4.Cells[currentRow4, 5].Value = "نام مستعار";

            foreach (var item in techninalAssistants)
            {
                currentRow4++;
                worksheet4.Cells[currentRow4, 1].Value = currentRow4 - 1;
                worksheet4.Cells[currentRow4, 2].Value = item.ProjectOpertationDetailId;
                worksheet4.Cells[currentRow4, 3].Value = item.TechnicalAssistantId;
                worksheet4.Cells[currentRow4, 4].Value = item.TechnicalAssistantName;
                worksheet4.Cells[currentRow4, 5].Value = item.TechnicalAssistantNickName;

                ExcelStyles.SetCellStyle(
                    worksheet4,
                    currentRow4 - 1,
                    currentRow4,
                    worksheet4.Dimension.Start.Column,
                    worksheet4.Dimension.End.Column);
            }
        }
        #endregion

        #region دستیاران  پیاده سازی
        if (implementationAssistants != null && implementationAssistants.Count > 0)
        {
            ExcelStyles.SetHeaderStyle(
                worksheet5.Cells[1, 1, 1, 5],

                ExcelBorderStyle.Medium);

            worksheet5.Cells[currentRow5, 1].Value = ProjectOperationDetailExcelEnum.Row.GetEnumDescription();
            worksheet5.Cells[currentRow5, 2].Value = "شناسه ریزمتره";
            worksheet5.Cells[currentRow5, 3].Value = "شناسه دستیار";
            worksheet5.Cells[currentRow5, 4].Value = "نام";
            worksheet5.Cells[currentRow5, 5].Value = "نام مستعار";

            foreach (var item in implementationAssistants)
            {
                currentRow5++;
                worksheet5.Cells[currentRow5, 1].Value = currentRow5 - 1;
                worksheet5.Cells[currentRow5, 2].Value = item.ImplementationAssistantId;
                worksheet5.Cells[currentRow5, 3].Value = item.ImplementationAssistantName;
                worksheet5.Cells[currentRow5, 4].Value = item.ImplementationAssistantNickName;
                worksheet5.Cells[currentRow5, 5].Value = item.ImplementationAssistantNickName;

                ExcelStyles.SetCellStyle(
                    worksheet5,
                    currentRow5 - 1,
                    currentRow5,
                    worksheet5.Dimension.Start.Column,
                    worksheet5.Dimension.End.Column);
            }
        }
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static byte[] ProjectOperationDetailReportingToExcel(
        ICollection<GetsProjectOperationDetailReportingExcelExporterModel> result,
        List<ProjectOperationDetailReportingExcelEnum>? excelFilters)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("گزارش ریزمتره");
        var currentRow = 1;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var counter = 0;
            var id = 0;
            var code = 0;
            var projectOperationId = 0;
            var operationInfoId = 0;
            var operationInfoName = 0;
            var operationInfoCode = 0;
            var measurementId = 0;
            var measurementName = 0;
            var projectId = 0;
            var projectName = 0;
            var projectCode = 0;
            var costCenterId = 0;
            var costCenterName = 0;
            var costCenterCode = 0;
            var operationLocationId = 0;
            var privateName = 0;
            var privateCode = 0;
            var publicName = 0;
            var publicCode = 0;
            var contractors = 0;
            var contractorsNickName = 0;
            var length = 0;
            var width = 0;
            var height = 0;
            var weight = 0;
            var number = 0;
            var finalAmount = 0;
            var doneFinalAmount = 0;
            var remaindedFinalAmount = 0;
            var statusDescription = 0;
            var startDate = 0;
            var endDate = 0;
            var day = 0;
            var hour = 0;
            var priority = 0;
            var creatorId = 0;
            var creatorName = 0;
            var creatorNickname = 0;
            var created = 0;
            var updaterId = 0;
            var updaterName = 0;
            var updaterNickname = 0;
            var updated = 0;
            var description = 0;

            foreach (var item in excelFilters)
            {
                counter++;
                switch (item)
                {
                    case ProjectOperationDetailReportingExcelEnum.Id:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Id.GetEnumDescription();
                        id = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Code:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Code.GetEnumDescription();
                        code = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.ProjectOperationId:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.ProjectOperationId.GetEnumDescription();
                        projectOperationId = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.OperationInfoId:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.OperationInfoId.GetEnumDescription();
                        operationInfoId = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.OperationInfoName:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.OperationInfoName.GetEnumDescription();
                        operationInfoName = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.OperationInfoCode:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.OperationInfoCode.GetEnumDescription();
                        operationInfoCode = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.MeasurementId:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.MeasurementId.GetEnumDescription();
                        measurementId = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.MeasurementName:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.MeasurementName.GetEnumDescription();
                        measurementName = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.ProjectId:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.ProjectId.GetEnumDescription();
                        projectId = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.ProjectName:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.ProjectName.GetEnumDescription();
                        projectName = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.ProjectCode:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.ProjectCode.GetEnumDescription();
                        projectCode = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.CostCenterId:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.CostCenterId.GetEnumDescription();
                        costCenterId = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.CostCenterName:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.CostCenterName.GetEnumDescription();
                        costCenterName = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.CostCenterCode:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.CostCenterCode.GetEnumDescription();
                        costCenterCode = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.OperationLocationId:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.OperationLocationId.GetEnumDescription();
                        operationLocationId = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.PrivateName:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.PrivateName.GetEnumDescription();
                        privateName = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.PrivateCode:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.PrivateCode.GetEnumDescription();
                        privateCode = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.PublicName:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.PublicName.GetEnumDescription();
                        publicName = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.PublicCode:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.PublicCode.GetEnumDescription();
                        publicCode = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Contractors:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Contractors.GetEnumDescription();
                        contractors = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.ContractorsNickName:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.ContractorsNickName.GetEnumDescription();
                        contractorsNickName = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Length:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Length.GetEnumDescription();
                        length = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Width:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Width.GetEnumDescription();
                        width = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Height:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Height.GetEnumDescription();
                        height = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Weight:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Weight.GetEnumDescription();
                        weight = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Number:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Number.GetEnumDescription();
                        number = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.FinalAmount:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.FinalAmount.GetEnumDescription();
                        finalAmount = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.DoneFinalAmount:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.DoneFinalAmount.GetEnumDescription();
                        doneFinalAmount = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.RemaindedFinalAmount:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.RemaindedFinalAmount.GetEnumDescription();
                        remaindedFinalAmount = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.StatusDescription:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.StatusDescription.GetEnumDescription();
                        statusDescription = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.StartDate:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.StartDate.GetEnumDescription();
                        startDate = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.EndDate:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.EndDate.GetEnumDescription();
                        endDate = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Day:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Day.GetEnumDescription();
                        day = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Hour:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Hour.GetEnumDescription();
                        hour = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Priority:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Priority.GetEnumDescription();
                        priority = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.CreatorId:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.CreatorId.GetEnumDescription();
                        creatorId = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.CreatorName:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.CreatorName.GetEnumDescription();
                        creatorName = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.CreatorNickname:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.CreatorNickname.GetEnumDescription();
                        creatorNickname = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Created:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Created.GetEnumDescription();
                        created = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.UpdaterId:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.UpdaterId.GetEnumDescription();
                        updaterId = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.UpdaterName:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.UpdaterName.GetEnumDescription();
                        updaterName = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.UpdaterNickname:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.UpdaterNickname.GetEnumDescription();
                        updaterNickname = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Updated:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Updated.GetEnumDescription();
                        updated = counter;
                        break;
                    case ProjectOperationDetailReportingExcelEnum.Description:
                        worksheet.Cell(currentRow, counter).Value = ProjectOperationDetailReportingExcelEnum.Description.GetEnumDescription();
                        description = counter;
                        break;


                }
            }

            foreach (var item in result)
            {
                currentRow++;
                if (id > 0)
                    worksheet.Cell(currentRow, id).Value = item.Id;
                if (code > 0)
                    worksheet.Cell(currentRow, code).Value = item.Code;
                if (projectOperationId > 0)
                    worksheet.Cell(currentRow, projectOperationId).Value = item.ProjectOperationId;
                if (operationInfoId > 0)
                    worksheet.Cell(currentRow, operationInfoId).Value = item.OperationInfoId;
                if (operationInfoName > 0)
                    worksheet.Cell(currentRow, operationInfoName).Value = item.OperationInfoName;
                if (operationInfoCode > 0)
                    worksheet.Cell(currentRow, operationInfoCode).Value = item.OperationInfoCode;
                if (measurementId > 0)
                    worksheet.Cell(currentRow, measurementId).Value = item.MeasurementId;
                if (measurementName > 0)
                    worksheet.Cell(currentRow, measurementName).Value = item.MeasurementName;
                if (projectId > 0)
                    worksheet.Cell(currentRow, projectId).Value = item.ProjectId;
                if (projectName > 0)
                    worksheet.Cell(currentRow, projectName).Value = item.ProjectName;
                if (projectCode > 0)
                    worksheet.Cell(currentRow, projectCode).Value = item.ProjectCode;
                if (costCenterId > 0)
                    worksheet.Cell(currentRow, costCenterId).Value = item.CostCenterId;
                if (costCenterName > 0)
                    worksheet.Cell(currentRow, costCenterName).Value = item.CostCenterName;
                if (costCenterCode > 0)
                    worksheet.Cell(currentRow, costCenterCode).Value = item.CostCenterCode;
                if (operationLocationId > 0)
                    worksheet.Cell(currentRow, operationLocationId).Value = item.OperationLocationId;
                if (privateName > 0)
                    worksheet.Cell(currentRow, privateName).Value = item.PrivateName;
                if (privateCode > 0)
                    worksheet.Cell(currentRow, privateCode).Value = item.PrivateCode;
                if (publicName > 0)
                    worksheet.Cell(currentRow, publicName).Value = item.PublicName;
                if (publicCode > 0)
                    worksheet.Cell(currentRow, publicCode).Value = item.PublicCode;
                if (contractors > 0)
                    worksheet.Cell(currentRow, contractors).Value = item.Contractors;
                if (contractorsNickName > 0)
                    worksheet.Cell(currentRow, contractorsNickName).Value = item.ContractorsNickName;
                if (length > 0)
                    worksheet.Cell(currentRow, length).Value = item.Length;
                if (width > 0)
                    worksheet.Cell(currentRow, width).Value = item.Width;
                if (height > 0)
                    worksheet.Cell(currentRow, height).Value = item.Height;
                if (weight > 0)
                    worksheet.Cell(currentRow, weight).Value = item.Weight;
                if (number > 0)
                    worksheet.Cell(currentRow, number).Value = item.Number;
                if (finalAmount > 0)
                    worksheet.Cell(currentRow, finalAmount).Value = item.FinalAmount;
                if (doneFinalAmount > 0)
                    worksheet.Cell(currentRow, doneFinalAmount).Value = item.DoneFinalAmount;
                if (remaindedFinalAmount > 0)
                    worksheet.Cell(currentRow, remaindedFinalAmount).Value = item.RemaindedFinalAmount;
                if (statusDescription > 0)
                    worksheet.Cell(currentRow, statusDescription).Value = item.StatusDescription;
                if (startDate > 0)
                    worksheet.Cell(currentRow, startDate).Value = item.ShamsiStartDate;
                if (endDate > 0)
                    worksheet.Cell(currentRow, endDate).Value = item.ShamsiEndDate;
                if (day > 0)
                    worksheet.Cell(currentRow, day).Value = item.Day;
                if (hour > 0)
                    worksheet.Cell(currentRow, hour).Value = item.Hour;
                if (priority > 0)
                    worksheet.Cell(currentRow, priority).Value = item.Priority;
                if (creatorId > 0)
                    worksheet.Cell(currentRow, creatorId).Value = item.CreatorId;
                if (creatorName > 0)
                    worksheet.Cell(currentRow, creatorName).Value = item.CreatorName;
                if (creatorNickname > 0)
                    worksheet.Cell(currentRow, creatorNickname).Value = item.CreatorNickname;
                if (created > 0)
                    worksheet.Cell(currentRow, created).Value = item.ShamsiCreated;
                if (updaterId > 0)
                    worksheet.Cell(currentRow, updaterId).Value = item.UpdaterId;
                if (updaterName > 0)
                    worksheet.Cell(currentRow, updaterName).Value = item.UpdaterName;
                if (updaterNickname > 0)
                    worksheet.Cell(currentRow, updaterNickname).Value = item.UpdaterNickname;
                if (updated > 0)
                    worksheet.Cell(currentRow, updated).Value = item.ShamsiUpdated;
                if (description > 0)
                    worksheet.Cell(currentRow, description).Value = item.Description;
            }
        }
        else
        {
            worksheet.Cell(currentRow, 1).Value = ProjectOperationDetailReportingExcelEnum.Id.GetEnumDescription();
            worksheet.Cell(currentRow, 2).Value = ProjectOperationDetailReportingExcelEnum.Code.GetEnumDescription();
            worksheet.Cell(currentRow, 3).Value = ProjectOperationDetailReportingExcelEnum.ProjectOperationId.GetEnumDescription();
            worksheet.Cell(currentRow, 4).Value = ProjectOperationDetailReportingExcelEnum.OperationInfoId.GetEnumDescription();
            worksheet.Cell(currentRow, 5).Value = ProjectOperationDetailReportingExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet.Cell(currentRow, 6).Value = ProjectOperationDetailReportingExcelEnum.OperationInfoCode.GetEnumDescription();
            worksheet.Cell(currentRow, 7).Value = ProjectOperationDetailReportingExcelEnum.MeasurementId.GetEnumDescription();
            worksheet.Cell(currentRow, 8).Value = ProjectOperationDetailReportingExcelEnum.MeasurementName.GetEnumDescription();
            worksheet.Cell(currentRow, 9).Value = ProjectOperationDetailReportingExcelEnum.ProjectId.GetEnumDescription();
            worksheet.Cell(currentRow, 10).Value = ProjectOperationDetailReportingExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cell(currentRow, 11).Value = ProjectOperationDetailReportingExcelEnum.ProjectCode.GetEnumDescription();
            worksheet.Cell(currentRow, 12).Value = ProjectOperationDetailReportingExcelEnum.CostCenterId.GetEnumDescription();
            worksheet.Cell(currentRow, 13).Value = ProjectOperationDetailReportingExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cell(currentRow, 14).Value = ProjectOperationDetailReportingExcelEnum.CostCenterCode.GetEnumDescription();
            worksheet.Cell(currentRow, 15).Value = ProjectOperationDetailReportingExcelEnum.OperationLocationId.GetEnumDescription();
            worksheet.Cell(currentRow, 16).Value = ProjectOperationDetailReportingExcelEnum.PrivateName.GetEnumDescription();
            worksheet.Cell(currentRow, 17).Value = ProjectOperationDetailReportingExcelEnum.PrivateCode.GetEnumDescription();
            worksheet.Cell(currentRow, 18).Value = ProjectOperationDetailReportingExcelEnum.PublicName.GetEnumDescription();
            worksheet.Cell(currentRow, 19).Value = ProjectOperationDetailReportingExcelEnum.PublicCode.GetEnumDescription();
            worksheet.Cell(currentRow, 20).Value = ProjectOperationDetailReportingExcelEnum.Contractors.GetEnumDescription();
            worksheet.Cell(currentRow, 21).Value = ProjectOperationDetailReportingExcelEnum.ContractorsNickName.GetEnumDescription();
            worksheet.Cell(currentRow, 22).Value = ProjectOperationDetailReportingExcelEnum.Length.GetEnumDescription();
            worksheet.Cell(currentRow, 23).Value = ProjectOperationDetailReportingExcelEnum.Width.GetEnumDescription();
            worksheet.Cell(currentRow, 24).Value = ProjectOperationDetailReportingExcelEnum.Height.GetEnumDescription();
            worksheet.Cell(currentRow, 25).Value = ProjectOperationDetailReportingExcelEnum.Weight.GetEnumDescription();
            worksheet.Cell(currentRow, 26).Value = ProjectOperationDetailReportingExcelEnum.Number.GetEnumDescription();
            worksheet.Cell(currentRow, 27).Value = ProjectOperationDetailReportingExcelEnum.FinalAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 28).Value = ProjectOperationDetailReportingExcelEnum.DoneFinalAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 29).Value = ProjectOperationDetailReportingExcelEnum.RemaindedFinalAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 30).Value = ProjectOperationDetailReportingExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 31).Value = ProjectOperationDetailReportingExcelEnum.StartDate.GetEnumDescription();
            worksheet.Cell(currentRow, 32).Value = ProjectOperationDetailReportingExcelEnum.EndDate.GetEnumDescription();
            worksheet.Cell(currentRow, 33).Value = ProjectOperationDetailReportingExcelEnum.Day.GetEnumDescription();
            worksheet.Cell(currentRow, 34).Value = ProjectOperationDetailReportingExcelEnum.Hour.GetEnumDescription();
            worksheet.Cell(currentRow, 35).Value = ProjectOperationDetailReportingExcelEnum.Priority.GetEnumDescription();
            worksheet.Cell(currentRow, 36).Value = ProjectOperationDetailReportingExcelEnum.CreatorId.GetEnumDescription();
            worksheet.Cell(currentRow, 37).Value = ProjectOperationDetailReportingExcelEnum.CreatorName.GetEnumDescription();
            worksheet.Cell(currentRow, 38).Value = ProjectOperationDetailReportingExcelEnum.CreatorNickname.GetEnumDescription();
            worksheet.Cell(currentRow, 39).Value = ProjectOperationDetailReportingExcelEnum.Created.GetEnumDescription();
            worksheet.Cell(currentRow, 40).Value = ProjectOperationDetailReportingExcelEnum.UpdaterId.GetEnumDescription();
            worksheet.Cell(currentRow, 41).Value = ProjectOperationDetailReportingExcelEnum.UpdaterName.GetEnumDescription();
            worksheet.Cell(currentRow, 42).Value = ProjectOperationDetailReportingExcelEnum.UpdaterNickname.GetEnumDescription();
            worksheet.Cell(currentRow, 43).Value = ProjectOperationDetailReportingExcelEnum.Updated.GetEnumDescription();
            worksheet.Cell(currentRow, 44).Value = ProjectOperationDetailReportingExcelEnum.Description.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = item.Id;
                worksheet.Cell(currentRow, 2).Value = item.Code;
                worksheet.Cell(currentRow, 3).Value = item.ProjectOperationId;
                worksheet.Cell(currentRow, 4).Value = item.OperationInfoId;
                worksheet.Cell(currentRow, 5).Value = item.OperationInfoName;
                worksheet.Cell(currentRow, 6).Value = item.OperationInfoCode;
                worksheet.Cell(currentRow, 7).Value = item.MeasurementId;
                worksheet.Cell(currentRow, 8).Value = item.MeasurementName;
                worksheet.Cell(currentRow, 9).Value = item.ProjectId;
                worksheet.Cell(currentRow, 10).Value = item.ProjectName;
                worksheet.Cell(currentRow, 11).Value = item.ProjectCode;
                worksheet.Cell(currentRow, 12).Value = item.CostCenterId;
                worksheet.Cell(currentRow, 13).Value = item.CostCenterName;
                worksheet.Cell(currentRow, 14).Value = item.CostCenterCode;
                worksheet.Cell(currentRow, 15).Value = item.OperationLocationId;
                worksheet.Cell(currentRow, 16).Value = item.PrivateName;
                worksheet.Cell(currentRow, 17).Value = item.PrivateCode;
                worksheet.Cell(currentRow, 18).Value = item.PublicName;
                worksheet.Cell(currentRow, 19).Value = item.PublicCode;
                worksheet.Cell(currentRow, 20).Value = item.Contractors;
                worksheet.Cell(currentRow, 21).Value = item.ContractorsNickName;
                worksheet.Cell(currentRow, 22).Value = item.Length;
                worksheet.Cell(currentRow, 23).Value = item.Width;
                worksheet.Cell(currentRow, 24).Value = item.Height;
                worksheet.Cell(currentRow, 25).Value = item.Weight;
                worksheet.Cell(currentRow, 26).Value = item.Number;
                worksheet.Cell(currentRow, 27).Value = item.FinalAmount;
                worksheet.Cell(currentRow, 28).Value = item.DoneFinalAmount;
                worksheet.Cell(currentRow, 29).Value = item.RemaindedFinalAmount;
                worksheet.Cell(currentRow, 30).Value = item.StatusDescription;
                worksheet.Cell(currentRow, 31).Value = item.ShamsiStartDate;
                worksheet.Cell(currentRow, 32).Value = item.ShamsiEndDate;
                worksheet.Cell(currentRow, 33).Value = item.Day;
                worksheet.Cell(currentRow, 34).Value = item.Hour;
                worksheet.Cell(currentRow, 35).Value = item.Priority;
                worksheet.Cell(currentRow, 36).Value = item.CreatorId;
                worksheet.Cell(currentRow, 37).Value = item.CreatorName;
                worksheet.Cell(currentRow, 38).Value = item.CreatorNickname;
                worksheet.Cell(currentRow, 39).Value = item.ShamsiCreated;
                worksheet.Cell(currentRow, 40).Value = item.UpdaterId;
                worksheet.Cell(currentRow, 41).Value = item.UpdaterName;
                worksheet.Cell(currentRow, 42).Value = item.UpdaterNickname;
                worksheet.Cell(currentRow, 43).Value = item.ShamsiUpdated;
                worksheet.Cell(currentRow, 44).Value = item.Description;
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }


    public static byte[] ContractorReportsToExcel(
        ICollection<GetsContractorReportsExcelExporterModel> result,
        List<ContractorReportsExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("گزارش قرارداد پیمانکاری");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<ContractorReportsExcelEnum, int>();
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
                if (columns.ContainsKey(ContractorReportsExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(ContractorReportsExcelEnum.ContractorId))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.ContractorId]].Value = item.ContractorId;
                if (columns.ContainsKey(ContractorReportsExcelEnum.Contractor))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.Contractor]].Value = item.Contractor;
                if (columns.ContainsKey(ContractorReportsExcelEnum.Id))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(ContractorReportsExcelEnum.ContractorContractTypeId))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.ContractorContractTypeId]].Value = item.ContractorContractTypeId;
                if (columns.ContainsKey(ContractorReportsExcelEnum.ContractorContractTypeName))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.ContractorContractTypeName]].Value = item.ContractorContractTypeName;
                if (columns.ContainsKey(ContractorReportsExcelEnum.ContractorContractTypeCode))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.ContractorContractTypeCode]].Value = item.ContractorContractTypeCode;
                if (columns.ContainsKey(ContractorReportsExcelEnum.StatusDescription))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(ContractorReportsExcelEnum.CurrencyId))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.CurrencyId]].Value = item.CurrencyId;
                if (columns.ContainsKey(ContractorReportsExcelEnum.Currency))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.Currency]].Value = item.Currency;
                if (columns.ContainsKey(ContractorReportsExcelEnum.StartDate))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.StartDate]].Value = item.StartDate;
                if (columns.ContainsKey(ContractorReportsExcelEnum.EndDate))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.EndDate]].Value = item.EndDate;
                if (columns.ContainsKey(ContractorReportsExcelEnum.TotalAmount))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.TotalAmount]].Value = item.TotalAmount;
                if (columns.ContainsKey(ContractorReportsExcelEnum.PercentageDoingJobWell))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.PercentageDoingJobWell]].Value = item.PercentageDoingJobWell;
                if (columns.ContainsKey(ContractorReportsExcelEnum.DoingJobWellAmount))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.DoingJobWellAmount]].Value = item.DoingJobWellAmount;
                if (columns.ContainsKey(ContractorReportsExcelEnum.PercentageAdvancePayment))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.PercentageAdvancePayment]].Value = item.PercentageAdvancePayment;
                if (columns.ContainsKey(ContractorReportsExcelEnum.AdvancePaymentAmount))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.AdvancePaymentAmount]].Value = item.AdvancePaymentAmount;
                if (columns.ContainsKey(ContractorReportsExcelEnum.DailyLatenessPenalty))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.DailyLatenessPenalty]].Value = item.DailyLatenessPenalty;
                if (columns.ContainsKey(ContractorReportsExcelEnum.WorkDonePercent))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.WorkDonePercent]].Value = item.WorkDonePercent;
                if (columns.ContainsKey(ContractorReportsExcelEnum.WorkDeliveryPercent))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.WorkDeliveryPercent]].Value = item.WorkDeliveryPercent;
                if (columns.ContainsKey(ContractorReportsExcelEnum.WorkCompletionPercent))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.WorkCompletionPercent]].Value = item.WorkCompletionPercent;
                if (columns.ContainsKey(ContractorReportsExcelEnum.Description))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.Description]].Value = item.Description;
                if (columns.ContainsKey(ContractorReportsExcelEnum.CreatorId))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.CreatorId]].Value = item.CreatorId;
                if (columns.ContainsKey(ContractorReportsExcelEnum.Creator))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.Creator]].Value = item.Creator;
                if (columns.ContainsKey(ContractorReportsExcelEnum.Created))
                    worksheet.Cells[currentRow, columns[ContractorReportsExcelEnum.Created]].Value = item.Created;

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

            worksheet.Cells[currentRow, 1].Value = ContractorReportsExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = ContractorReportsExcelEnum.ContractorId.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = ContractorReportsExcelEnum.Contractor.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = ContractorReportsExcelEnum.Id.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = ContractorReportsExcelEnum.ContractorContractTypeId.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = ContractorReportsExcelEnum.ContractorContractTypeName.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = ContractorReportsExcelEnum.ContractorContractTypeCode.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = ContractorReportsExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = ContractorReportsExcelEnum.CurrencyId.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = ContractorReportsExcelEnum.Currency.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = ContractorReportsExcelEnum.StartDate.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = ContractorReportsExcelEnum.EndDate.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = ContractorReportsExcelEnum.TotalAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = ContractorReportsExcelEnum.PercentageDoingJobWell.GetEnumDescription();
            worksheet.Cells[currentRow, 15].Value = ContractorReportsExcelEnum.DoingJobWellAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 16].Value = ContractorReportsExcelEnum.PercentageAdvancePayment.GetEnumDescription();
            worksheet.Cells[currentRow, 17].Value = ContractorReportsExcelEnum.AdvancePaymentAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 18].Value = ContractorReportsExcelEnum.DailyLatenessPenalty.GetEnumDescription();
            worksheet.Cells[currentRow, 19].Value = ContractorReportsExcelEnum.WorkDonePercent.GetEnumDescription();
            worksheet.Cells[currentRow, 20].Value = ContractorReportsExcelEnum.WorkDeliveryPercent.GetEnumDescription();
            worksheet.Cells[currentRow, 21].Value = ContractorReportsExcelEnum.WorkCompletionPercent.GetEnumDescription();
            worksheet.Cells[currentRow, 22].Value = ContractorReportsExcelEnum.Description.GetEnumDescription();
            worksheet.Cells[currentRow, 23].Value = ContractorReportsExcelEnum.CreatorId.GetEnumDescription();
            worksheet.Cells[currentRow, 24].Value = ContractorReportsExcelEnum.Creator.GetEnumDescription();
            worksheet.Cells[currentRow, 25].Value = ContractorReportsExcelEnum.Created.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.ContractorId;
                worksheet.Cells[currentRow, 3].Value = item.Contractor;
                worksheet.Cells[currentRow, 4].Value = item.Id;
                worksheet.Cells[currentRow, 5].Value = item.ContractorContractTypeId;
                worksheet.Cells[currentRow, 6].Value = item.ContractorContractTypeName;
                worksheet.Cells[currentRow, 7].Value = item.ContractorContractTypeCode;
                worksheet.Cells[currentRow, 8].Value = item.StatusDescription;
                worksheet.Cells[currentRow, 9].Value = item.CurrencyId;
                worksheet.Cells[currentRow, 10].Value = item.Currency;
                worksheet.Cells[currentRow, 11].Value = item.StartDate;
                worksheet.Cells[currentRow, 12].Value = item.EndDate;
                worksheet.Cells[currentRow, 13].Value = item.TotalAmount;
                worksheet.Cells[currentRow, 14].Value = item.PercentageDoingJobWell;
                worksheet.Cells[currentRow, 15].Value = item.DoingJobWellAmount;
                worksheet.Cells[currentRow, 16].Value = item.PercentageAdvancePayment;
                worksheet.Cells[currentRow, 17].Value = item.AdvancePaymentAmount;
                worksheet.Cells[currentRow, 18].Value = item.DailyLatenessPenalty;
                worksheet.Cells[currentRow, 19].Value = item.WorkDonePercent;
                worksheet.Cells[currentRow, 20].Value = item.WorkDeliveryPercent;
                worksheet.Cells[currentRow, 21].Value = item.WorkCompletionPercent;
                worksheet.Cells[currentRow, 22].Value = item.Description;
                worksheet.Cells[currentRow, 23].Value = item.CreatorId;
                worksheet.Cells[currentRow, 24].Value = item.Creator;
                worksheet.Cells[currentRow, 25].Value = item.Created;

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

    public static byte[] ContractorDetailReportsToExcel(
        ICollection<GetsContractorDetailReportsExcelExporterModel> result,
        List<ContractorDetailReportsExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = workbook.Workbook.Worksheets.Add("گزارش جزییات قرارداد پیمانکاری");
        var currentRow = 1;
        worksheet.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<ContractorDetailReportsExcelEnum, int>();
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
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.Row))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.Row]].Value = currentRow - 1;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.ContractorContractId))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.ContractorContractId]].Value = item.ContractorContractId;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.Id))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.WorkLoad))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.WorkLoad]].Value = item.WorkLoad;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.StartDate))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.StartDate]].Value = item.StartDate;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.EndDate))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.EndDate]].Value = item.EndDate;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.UnitAmount))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.UnitAmount]].Value = item.UnitAmount;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.TotalAmount))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.TotalAmount]].Value = item.TotalAmount;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.ProjectOperationId))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.ProjectOperationId]].Value = item.ProjectOperationId;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.OperationInfoId))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.OperationInfoId]].Value = item.OperationInfoId;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.OperationInfoName))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.OperationInfoName]].Value = item.OperationInfoName;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.OperationInfoCode))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.OperationInfoCode]].Value = item.OperationInfoCode;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.ProjectId))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.ProjectId]].Value = item.ProjectId;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.ProjectName))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.ProjectCode))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.ProjectCode]].Value = item.ProjectCode;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.CostCenterId))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.CostCenterId]].Value = item.CostCenterId;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.CostCenterName))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.CostCenterCode))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.CostCenterCode]].Value = item.CostCenterCode;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.ServiceInfoId))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.ServiceInfoId]].Value = item.ServiceInfoId;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.ServiceInfoName))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.ServiceInfoName]].Value = item.ServiceInfoName;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.ServiceInfoCode))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.ServiceInfoCode]].Value = item.ServiceInfoCode;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.ServiceInfoUnitOfMeasurementId))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.ServiceInfoUnitOfMeasurementId]].Value = item.ServiceInfoUnitOfMeasurementId;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.ServiceInfoUnitOfMeasurement))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.ServiceInfoUnitOfMeasurement]].Value = item.ServiceInfoUnitOfMeasurement;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.ProjectOperationDetailId))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.ProjectOperationDetailId]].Value = item.ProjectOperationDetailId;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.OperationLocationId))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.OperationLocationId]].Value = item.OperationLocationId;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.PrivateName))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.PrivateName]].Value = item.PrivateName;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.PrivateCode))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.PrivateCode]].Value = item.PrivateCode;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.PublicName))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.PublicName]].Value = item.PublicName;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.PublicCode))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.PublicCode]].Value = item.PublicCode;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.StatusDescription))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.CreatorId))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.CreatorId]].Value = item.CreatorId;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.Creator))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.Creator]].Value = item.Creator;
                if (columns.ContainsKey(ContractorDetailReportsExcelEnum.Created))
                    worksheet.Cells[currentRow, columns[ContractorDetailReportsExcelEnum.Created]].Value = item.Created;

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
                worksheet.Cells[1, 1, 1, 33],

                ExcelBorderStyle.Medium);

            worksheet.Cells[currentRow, 1].Value = ContractorDetailReportsExcelEnum.Row.GetEnumDescription();
            worksheet.Cells[currentRow, 2].Value = ContractorDetailReportsExcelEnum.ContractorContractId.GetEnumDescription();
            worksheet.Cells[currentRow, 3].Value = ContractorDetailReportsExcelEnum.Id.GetEnumDescription();
            worksheet.Cells[currentRow, 4].Value = ContractorDetailReportsExcelEnum.WorkLoad.GetEnumDescription();
            worksheet.Cells[currentRow, 5].Value = ContractorDetailReportsExcelEnum.StartDate.GetEnumDescription();
            worksheet.Cells[currentRow, 6].Value = ContractorDetailReportsExcelEnum.EndDate.GetEnumDescription();
            worksheet.Cells[currentRow, 7].Value = ContractorDetailReportsExcelEnum.UnitAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 8].Value = ContractorDetailReportsExcelEnum.TotalAmount.GetEnumDescription();
            worksheet.Cells[currentRow, 9].Value = ContractorDetailReportsExcelEnum.ProjectOperationId.GetEnumDescription();
            worksheet.Cells[currentRow, 10].Value = ContractorDetailReportsExcelEnum.OperationInfoId.GetEnumDescription();
            worksheet.Cells[currentRow, 11].Value = ContractorDetailReportsExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet.Cells[currentRow, 12].Value = ContractorDetailReportsExcelEnum.OperationInfoCode.GetEnumDescription();
            worksheet.Cells[currentRow, 13].Value = ContractorDetailReportsExcelEnum.ProjectId.GetEnumDescription();
            worksheet.Cells[currentRow, 14].Value = ContractorDetailReportsExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cells[currentRow, 15].Value = ContractorDetailReportsExcelEnum.ProjectCode.GetEnumDescription();
            worksheet.Cells[currentRow, 16].Value = ContractorDetailReportsExcelEnum.CostCenterId.GetEnumDescription();
            worksheet.Cells[currentRow, 17].Value = ContractorDetailReportsExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cells[currentRow, 18].Value = ContractorDetailReportsExcelEnum.CostCenterCode.GetEnumDescription();
            worksheet.Cells[currentRow, 19].Value = ContractorDetailReportsExcelEnum.ServiceInfoId.GetEnumDescription();
            worksheet.Cells[currentRow, 20].Value = ContractorDetailReportsExcelEnum.ServiceInfoName.GetEnumDescription();
            worksheet.Cells[currentRow, 21].Value = ContractorDetailReportsExcelEnum.ServiceInfoCode.GetEnumDescription();
            worksheet.Cells[currentRow, 22].Value = ContractorDetailReportsExcelEnum.ServiceInfoUnitOfMeasurementId.GetEnumDescription();
            worksheet.Cells[currentRow, 23].Value = ContractorDetailReportsExcelEnum.ServiceInfoUnitOfMeasurement.GetEnumDescription();
            worksheet.Cells[currentRow, 24].Value = ContractorDetailReportsExcelEnum.ProjectOperationDetailId.GetEnumDescription();
            worksheet.Cells[currentRow, 25].Value = ContractorDetailReportsExcelEnum.OperationLocationId.GetEnumDescription();
            worksheet.Cells[currentRow, 26].Value = ContractorDetailReportsExcelEnum.PrivateName.GetEnumDescription();
            worksheet.Cells[currentRow, 27].Value = ContractorDetailReportsExcelEnum.PrivateCode.GetEnumDescription();
            worksheet.Cells[currentRow, 28].Value = ContractorDetailReportsExcelEnum.PublicName.GetEnumDescription();
            worksheet.Cells[currentRow, 29].Value = ContractorDetailReportsExcelEnum.PublicCode.GetEnumDescription();
            worksheet.Cells[currentRow, 30].Value = ContractorDetailReportsExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cells[currentRow, 31].Value = ContractorDetailReportsExcelEnum.CreatorId.GetEnumDescription();
            worksheet.Cells[currentRow, 32].Value = ContractorDetailReportsExcelEnum.Creator.GetEnumDescription();
            worksheet.Cells[currentRow, 33].Value = ContractorDetailReportsExcelEnum.Created.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cells[currentRow, 1].Value = currentRow - 1;
                worksheet.Cells[currentRow, 2].Value = item.ContractorContractId;
                worksheet.Cells[currentRow, 3].Value = item.Id;
                worksheet.Cells[currentRow, 4].Value = item.WorkLoad;
                worksheet.Cells[currentRow, 5].Value = item.StartDate;
                worksheet.Cells[currentRow, 6].Value = item.EndDate;
                worksheet.Cells[currentRow, 7].Value = item.UnitAmount;
                worksheet.Cells[currentRow, 8].Value = item.TotalAmount;
                worksheet.Cells[currentRow, 9].Value = item.ProjectOperationId;
                worksheet.Cells[currentRow, 10].Value = item.OperationInfoId;
                worksheet.Cells[currentRow, 11].Value = item.OperationInfoName;
                worksheet.Cells[currentRow, 12].Value = item.OperationInfoCode;
                worksheet.Cells[currentRow, 13].Value = item.ProjectId;
                worksheet.Cells[currentRow, 14].Value = item.ProjectName;
                worksheet.Cells[currentRow, 15].Value = item.ProjectCode;
                worksheet.Cells[currentRow, 16].Value = item.CostCenterId;
                worksheet.Cells[currentRow, 17].Value = item.CostCenterName;
                worksheet.Cells[currentRow, 18].Value = item.CostCenterCode;
                worksheet.Cells[currentRow, 19].Value = item.ServiceInfoId;
                worksheet.Cells[currentRow, 20].Value = item.ServiceInfoName;
                worksheet.Cells[currentRow, 21].Value = item.ServiceInfoCode;
                worksheet.Cells[currentRow, 22].Value = item.ServiceInfoUnitOfMeasurementId;
                worksheet.Cells[currentRow, 23].Value = item.ServiceInfoUnitOfMeasurement;
                worksheet.Cells[currentRow, 24].Value = item.ProjectOperationDetailId;
                worksheet.Cells[currentRow, 25].Value = item.OperationLocationId;
                worksheet.Cells[currentRow, 26].Value = item.PrivateName;
                worksheet.Cells[currentRow, 27].Value = item.PrivateCode;
                worksheet.Cells[currentRow, 28].Value = item.PublicName;
                worksheet.Cells[currentRow, 29].Value = item.PublicCode;
                worksheet.Cells[currentRow, 30].Value = item.StatusDescription;
                worksheet.Cells[currentRow, 31].Value = item.CreatorId;
                worksheet.Cells[currentRow, 32].Value = item.Creator;
                worksheet.Cells[currentRow, 33].Value = item.Created;

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