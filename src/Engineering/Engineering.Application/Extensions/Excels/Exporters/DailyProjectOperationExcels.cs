using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyHistoriesExcelEnums;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyHistoriesExcelExporter;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationServiceExcelEnums;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationServiceExcelExporter;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperationExcelEnums;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperationExcelExporter;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class DailyProjectOperationExcels
{
    public static byte[] DailyProjectOperationExcel(
        GetTotalsByProjectOperationDetailIdExcelExporterResponse total,
        ICollection<GetDailyHistoriesExcelExporterResponseModel> result,
        List<DailyProjectOperationsExcelEnum>? excelFilters)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("کارکرد روزانه");
        var currentRow = 1;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var counter = 0;
            var dailyProjectOperationId = 0;
            var startDate = 0;
            var statusDescription = 0;
            var finalAmount = 0;
            var unitOfMeasurementId = 0;
            var unitOfMeasurement = 0;
            var created = 0;
            var projectOperationDetailId = 0;
            var projectOperationDetailDescription = 0;
            var publicName = 0;
            var publicCode = 0;
            var privateName = 0;
            var privateCode = 0;
            var projectOperationId = 0;
            var operationInfoName = 0;
            var projectId = 0;
            var projectName = 0;
            var costCenterId = 0;
            var costCenterName = 0;
            var companyId = 0;
            var companyNameFa = 0;
            var description = 0;

            foreach (var item in excelFilters)
            {
                counter++;
                switch (item)
                {
                    case DailyProjectOperationsExcelEnum.DailyProjectOperationId:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.DailyProjectOperationId.GetEnumDescription();
                        dailyProjectOperationId = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.StartDate:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.StartDate.GetEnumDescription();
                        startDate = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.StatusDescription:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.StatusDescription.GetEnumDescription();
                        statusDescription = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.FinalAmount:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.FinalAmount.GetEnumDescription();
                        finalAmount = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.UnitOfMeasurementId:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.UnitOfMeasurementId.GetEnumDescription();
                        unitOfMeasurementId = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.UnitOfMeasurement:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.UnitOfMeasurement.GetEnumDescription();
                        unitOfMeasurement = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.Created:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.Created.GetEnumDescription();
                        created = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.ProjectId:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.ProjectId.GetEnumDescription();
                        projectId = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.ProjectOperationId:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.ProjectOperationId.GetEnumDescription();
                        projectOperationId = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.OperationInfoName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.OperationInfoName.GetEnumDescription();
                        operationInfoName = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.ProjectName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.ProjectName.GetEnumDescription();
                        projectName = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.CostCenterId:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.CostCenterId.GetEnumDescription();
                        costCenterId = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.CostCenterName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.CostCenterName.GetEnumDescription();
                        costCenterName = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.CompanyId:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.CompanyId.GetEnumDescription();
                        companyId = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.CompanyNameFa:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.CompanyNameFa.GetEnumDescription();
                        companyNameFa = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.ProjectOperationDetailId:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.ProjectOperationDetailId.GetEnumDescription();
                        projectOperationDetailId = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.PublicName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.PublicName.GetEnumDescription();
                        publicName = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.PublicCode:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.PublicCode.GetEnumDescription();
                        publicCode = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.PrivateName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.PrivateName.GetEnumDescription();
                        privateName = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.PrivateCode:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.PrivateCode.GetEnumDescription();
                        privateCode = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.ProjectOperationDetailDescription:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.ProjectOperationDetailDescription.GetEnumDescription();
                        projectOperationDetailDescription = counter;
                        break;
                    case DailyProjectOperationsExcelEnum.Description:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationsExcelEnum.Description.GetEnumDescription();
                        description = counter;
                        break;
                }
            }

            foreach (var item in result)
            {
                currentRow++;
                if (dailyProjectOperationId > 0)
                    worksheet.Cell(currentRow, dailyProjectOperationId).Value = item.DailyProjectOperationId;
                if (projectOperationId > 0)
                    worksheet.Cell(currentRow, projectOperationId).Value = item.ProjectOperationId;
                if (operationInfoName > 0)
                    worksheet.Cell(currentRow, operationInfoName).Value = item.OperationInfoName;
                if (projectId > 0)
                    worksheet.Cell(currentRow, projectId).Value = item.ProjectId;
                if (projectName > 0)
                    worksheet.Cell(currentRow, projectName).Value = item.ProjectName;
                if (costCenterId > 0)
                    worksheet.Cell(currentRow, costCenterId).Value = item.CostCenterId;
                if (costCenterName > 0)
                    worksheet.Cell(currentRow, costCenterName).Value = item.CostCenterName;
                if (startDate > 0)
                    worksheet.Cell(currentRow, startDate).Value = item.StartDate;
                if (statusDescription > 0)
                    worksheet.Cell(currentRow, statusDescription).Value = item.StatusDescription;
                if (finalAmount > 0)
                    worksheet.Cell(currentRow, finalAmount).Value = item.FinalAmount;
                if (unitOfMeasurementId > 0)
                    worksheet.Cell(currentRow, unitOfMeasurementId).Value = item.UnitOfMeasurementId;
                if (unitOfMeasurement > 0)
                    worksheet.Cell(currentRow, unitOfMeasurement).Value = item.UnitOfMeasurement;
                if (created > 0)
                    worksheet.Cell(currentRow, created).Value = item.Created;
                if (companyNameFa > 0)
                    worksheet.Cell(currentRow, companyNameFa).Value = item.CompanyNameFa;
                if (companyId > 0)
                    worksheet.Cell(currentRow, companyId).Value = item.CompanyId;
                if (projectOperationDetailId > 0)
                    worksheet.Cell(currentRow, projectOperationDetailId).Value = item.ProjectOperationDetailId;
                if (publicName > 0)
                    worksheet.Cell(currentRow, publicName).Value = item.PublicName;
                if (publicCode > 0)
                    worksheet.Cell(currentRow, publicCode).Value = item.PublicCode;
                if (privateName > 0)
                    worksheet.Cell(currentRow, privateName).Value = item.PrivateName;
                if (privateCode > 0)
                    worksheet.Cell(currentRow, privateCode).Value = item.PrivateCode;
                if (projectOperationDetailDescription > 0)
                    worksheet.Cell(currentRow, projectOperationDetailDescription).Value = item.ProjectOperationDetailDescription;
                if (description > 0)
                    worksheet.Cell(currentRow, description).Value = item.Description;
            }
        }
        else
        {
            worksheet.Cell(currentRow, 1).Value = DailyProjectOperationsExcelEnum.DailyProjectOperationId.GetEnumDescription();
            worksheet.Cell(currentRow, 2).Value = DailyProjectOperationsExcelEnum.ProjectOperationId.GetEnumDescription();
            worksheet.Cell(currentRow, 3).Value = DailyProjectOperationsExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet.Cell(currentRow, 4).Value = DailyProjectOperationsExcelEnum.ProjectId.GetEnumDescription();
            worksheet.Cell(currentRow, 5).Value = DailyProjectOperationsExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cell(currentRow, 6).Value = DailyProjectOperationsExcelEnum.CostCenterId.GetEnumDescription();
            worksheet.Cell(currentRow, 7).Value = DailyProjectOperationsExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cell(currentRow, 8).Value = DailyProjectOperationsExcelEnum.StartDate.GetEnumDescription();
            worksheet.Cell(currentRow, 9).Value = DailyProjectOperationsExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 10).Value = DailyProjectOperationsExcelEnum.FinalAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 11).Value = DailyProjectOperationsExcelEnum.UnitOfMeasurementId.GetEnumDescription();
            worksheet.Cell(currentRow, 12).Value = DailyProjectOperationsExcelEnum.UnitOfMeasurement.GetEnumDescription();
            worksheet.Cell(currentRow, 13).Value = DailyProjectOperationsExcelEnum.Created.GetEnumDescription();
            worksheet.Cell(currentRow, 14).Value = DailyProjectOperationsExcelEnum.CompanyId.GetEnumDescription();
            worksheet.Cell(currentRow, 15).Value = DailyProjectOperationsExcelEnum.CompanyNameFa.GetEnumDescription();
            worksheet.Cell(currentRow, 16).Value = DailyProjectOperationsExcelEnum.ProjectOperationDetailId.GetEnumDescription();
            worksheet.Cell(currentRow, 17).Value = DailyProjectOperationsExcelEnum.PublicName.GetEnumDescription();
            worksheet.Cell(currentRow, 18).Value = DailyProjectOperationsExcelEnum.PublicCode.GetEnumDescription();
            worksheet.Cell(currentRow, 19).Value = DailyProjectOperationsExcelEnum.PrivateName.GetEnumDescription();
            worksheet.Cell(currentRow, 20).Value = DailyProjectOperationsExcelEnum.PrivateCode.GetEnumDescription();
            worksheet.Cell(currentRow, 21).Value = DailyProjectOperationsExcelEnum.ProjectOperationDetailDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 22).Value = DailyProjectOperationsExcelEnum.Description.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = item.DailyProjectOperationId;
                worksheet.Cell(currentRow, 2).Value = item.ProjectOperationId;
                worksheet.Cell(currentRow, 3).Value = item.OperationInfoName;
                worksheet.Cell(currentRow, 4).Value = item.ProjectId;
                worksheet.Cell(currentRow, 5).Value = item.ProjectName;
                worksheet.Cell(currentRow, 6).Value = item.CostCenterId;
                worksheet.Cell(currentRow, 7).Value = item.CostCenterName;
                worksheet.Cell(currentRow, 8).Value = item.StartDate;
                worksheet.Cell(currentRow, 9).Value = item.StatusDescription;
                worksheet.Cell(currentRow, 10).Value = item.FinalAmount;
                worksheet.Cell(currentRow, 11).Value = item.UnitOfMeasurementId;
                worksheet.Cell(currentRow, 12).Value = item.UnitOfMeasurement;
                worksheet.Cell(currentRow, 13).Value = item.Created;
                worksheet.Cell(currentRow, 14).Value = item.CompanyId;
                worksheet.Cell(currentRow, 15).Value = item.CompanyNameFa;
                worksheet.Cell(currentRow, 16).Value = item.ProjectOperationDetailId;
                worksheet.Cell(currentRow, 17).Value = item.PublicName;
                worksheet.Cell(currentRow, 18).Value = item.PublicCode;
                worksheet.Cell(currentRow, 19).Value = item.PrivateName;
                worksheet.Cell(currentRow, 20).Value = item.PrivateCode;
                worksheet.Cell(currentRow, 21).Value = item.ProjectOperationDetailDescription;
                worksheet.Cell(currentRow, 22).Value = item.Description;
            }
        }

        var worksheet1 = workbook.Worksheets.Add("مجموع کارکرد روزانه ها");
        var currentRow1 = 1;
        worksheet1.Cell(currentRow1, 1).Value = "جمع طول";
        worksheet1.Cell(currentRow1, 2).Value = "جمع عرض";
        worksheet1.Cell(currentRow1, 3).Value = "جمع ارتفاع";
        worksheet1.Cell(currentRow1, 4).Value = "جمع وزن";
        worksheet1.Cell(currentRow1, 5).Value = "جمع تعداد";
        worksheet1.Cell(currentRow1, 6).Value = "مجموع حجم کارکرد روزانه";
        worksheet1.Cell(currentRow1, 7).Value = "حجم کسورات ریزمتره";
        worksheet1.Cell(currentRow1, 8).Value = "حجم ریزمتره کسر شده از کسورات";
        worksheet1.Cell(currentRow1, 9).Value = "حجم ریزمتره";
        worksheet1.Cell(currentRow1, 10).Value = "حجم شرح عملیات";

        worksheet1.Cell(currentRow1 + 1, 1).Value = total.TotalLengths;
        worksheet1.Cell(currentRow1 + 1, 2).Value = total.TotalWidths;
        worksheet1.Cell(currentRow1 + 1, 3).Value = total.TotalHeights;
        worksheet1.Cell(currentRow1 + 1, 4).Value = total.TotalWeights;
        worksheet1.Cell(currentRow1 + 1, 5).Value = total.TotalNumbers;
        worksheet1.Cell(currentRow1 + 1, 6).Value = total.TotalAmounts;
        worksheet1.Cell(currentRow1 + 1, 7).Value = total.TotalDeductionFinalAmount;
        worksheet1.Cell(currentRow1 + 1, 8).Value = total.ProjectOperationDetailFinalAmount;
        worksheet1.Cell(currentRow1 + 1, 9).Value = total.TotalProjectOperationDetailFinalAmount;
        worksheet1.Cell(currentRow1 + 1, 10).Value = total.ProjectOperationWorkload;


        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static byte[] DetailedDailyProjectOperationExcel(ICollection<GetsDetailedDailyProjectOperationExcelExporterResponseModel> result,
        List<DetailedDailyProjectOperationsExcelEnum>? excelFilters)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("جزییات کارکرد روزانه");
        var currentRow = 1;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var counter = 0;
            var id = 0;
            var costCenterId = 0;
            var costCenterName = 0;
            var projectId = 0;
            var projectName = 0;
            var projectOperationId = 0;
            var projectOperationName = 0;
            var projectOperationStatusDescription = 0;
            var unitOfMeasurementId = 0;
            var measurementName = 0;
            var projectOperationDetailId = 0;
            var projectOperationDetailCode = 0;
            var projectOperationDetailLength = 0;
            var projectOperationDetailWidth = 0;
            var projectOperationDetailHeight = 0;
            var projectOperationDetailWeight = 0;
            var projectOperationDetailNumber = 0;
            var projectOperationDetailFinalAmount = 0;
            var projectOperationDetailDescription = 0;
            var projectOperationDetailStatusDescription = 0;
            var projectOperationDetailStartDateShamsi = 0;
            var projectOperationDetailEndDateShamsi = 0;
            var projectOperationDetailCreateDateShamsi = 0;
            var operationLocationId = 0;
            var privateName = 0;
            var privateCode = 0;
            var publicName = 0;
            var publicCode = 0;
            var length = 0;
            var width = 0;
            var height = 0;
            var weight = 0;
            var number = 0;
            var dailyProjectOperationFinalAmount = 0;
            var description = 0;
            var statusDescription = 0;
            var startDateShamsi = 0;
            var endDateShamsi = 0;
            var contractors = 0;
            var contractorsNickName = 0;
            var creatorId = 0;
            var creatorName = 0;
            var creatorNickname = 0;
            var createdShamsi = 0;
            var serviceInfos = 0;

            foreach (var item in excelFilters)
            {
                counter++;
                switch (item)
                {
                    case DetailedDailyProjectOperationsExcelEnum.Id:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.Id.GetEnumDescription();
                        id = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.CostCenterId:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.CostCenterId.GetEnumDescription();
                        costCenterId = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.CostCenterName:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.CostCenterName.GetEnumDescription();
                        costCenterName = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectId:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectId.GetEnumDescription();
                        projectId = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectName:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectName.GetEnumDescription();
                        projectName = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationId:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationId.GetEnumDescription();
                        projectOperationId = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationName:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationName.GetEnumDescription();
                        projectOperationName = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationStatusDescription:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationStatusDescription.GetEnumDescription();
                        projectOperationStatusDescription = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.UnitOfMeasurementId:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.UnitOfMeasurementId.GetEnumDescription();
                        unitOfMeasurementId = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.MeasurementName:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.MeasurementName.GetEnumDescription();
                        measurementName = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailId:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailId.GetEnumDescription();
                        projectOperationDetailId = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailCode:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailCode.GetEnumDescription();
                        projectOperationDetailCode = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailLength:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailLength.GetEnumDescription();
                        projectOperationDetailLength = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailWidth:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailWidth.GetEnumDescription();
                        projectOperationDetailWidth = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailHeight:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailHeight.GetEnumDescription();
                        projectOperationDetailHeight = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailWeight:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailWeight.GetEnumDescription();
                        projectOperationDetailWeight = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailNumber:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailNumber.GetEnumDescription();
                        projectOperationDetailNumber = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailFinalAmount:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailFinalAmount.GetEnumDescription();
                        projectOperationDetailFinalAmount = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailDescription:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailDescription.GetEnumDescription();
                        projectOperationDetailDescription = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailStatusDescription:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailStatusDescription.GetEnumDescription();
                        projectOperationDetailStatusDescription = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailStartDateShamsi:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailStartDateShamsi.GetEnumDescription();
                        projectOperationDetailStartDateShamsi = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailEndDateShamsi:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailEndDateShamsi.GetEnumDescription();
                        projectOperationDetailEndDateShamsi = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailCreateDateShamsi:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailCreateDateShamsi.GetEnumDescription();
                        projectOperationDetailCreateDateShamsi = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.OperationLocationId:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.OperationLocationId.GetEnumDescription();
                        operationLocationId = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.PrivateName:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.PrivateName.GetEnumDescription();
                        privateName = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.PrivateCode:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.PrivateCode.GetEnumDescription();
                        privateCode = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.PublicName:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.PublicName.GetEnumDescription();
                        publicName = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.PublicCode:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.PublicCode.GetEnumDescription();
                        publicCode = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.Length:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.Length.GetEnumDescription();
                        length = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.Width:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.Width.GetEnumDescription();
                        width = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.Height:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.Height.GetEnumDescription();
                        height = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.Weight:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.Weight.GetEnumDescription();
                        weight = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.Number:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.Number.GetEnumDescription();
                        number = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.DailyProjectOperationFinalAmount:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.DailyProjectOperationFinalAmount.GetEnumDescription();
                        dailyProjectOperationFinalAmount = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.Description:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.Description.GetEnumDescription();
                        description = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.StatusDescription:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.StatusDescription.GetEnumDescription();
                        statusDescription = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.StartDateShamsi:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.StartDateShamsi.GetEnumDescription();
                        startDateShamsi = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.EndDateShamsi:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.EndDateShamsi.GetEnumDescription();
                        endDateShamsi = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.Contractors:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.Contractors.GetEnumDescription();
                        contractors = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ContractorsNickName:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ContractorsNickName.GetEnumDescription();
                        contractorsNickName = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.CreatorId:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.CreatorId.GetEnumDescription();
                        creatorId = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.CreatorName:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.CreatorName.GetEnumDescription();
                        creatorName = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.CreatorNickname:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.CreatorNickname.GetEnumDescription();
                        creatorNickname = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.CreatedShamsi:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.CreatedShamsi.GetEnumDescription();
                        createdShamsi = counter;
                        break;
                    case DetailedDailyProjectOperationsExcelEnum.ServiceInfos:
                        worksheet.Cell(currentRow, counter).Value = DetailedDailyProjectOperationsExcelEnum.ServiceInfos.GetEnumDescription();
                        serviceInfos = counter;
                        break;
                }
            }

            foreach (var item in result)
            {
                currentRow++;
                if (id > 0)
                    worksheet.Cell(currentRow, id).Value = item.Id;
                if (costCenterId > 0)
                    worksheet.Cell(currentRow, costCenterId).Value = item.CostCenterId;
                if (costCenterName > 0)
                    worksheet.Cell(currentRow, costCenterName).Value = item.CostCenterName;
                if (projectId > 0)
                    worksheet.Cell(currentRow, projectId).Value = item.ProjectId;
                if (projectName > 0)
                    worksheet.Cell(currentRow, projectName).Value = item.ProjectName;
                if (projectOperationId > 0)
                    worksheet.Cell(currentRow, projectOperationId).Value = item.ProjectOperationId;
                if (projectOperationName > 0)
                    worksheet.Cell(currentRow, projectOperationName).Value = item.ProjectOperationName;
                if (projectOperationStatusDescription > 0)
                    worksheet.Cell(currentRow, projectOperationStatusDescription).Value = item.ProjectOperationStatusDescription;
                if (unitOfMeasurementId > 0)
                    worksheet.Cell(currentRow, unitOfMeasurementId).Value = item.UnitOfMeasurementId;
                if (measurementName > 0)
                    worksheet.Cell(currentRow, measurementName).Value = item.MeasurementName;
                if (projectOperationDetailId > 0)
                    worksheet.Cell(currentRow, projectOperationDetailId).Value = item.ProjectOperationDetailId;
                if (projectOperationDetailCode > 0)
                    worksheet.Cell(currentRow, projectOperationDetailCode).Value = item.ProjectOperationDetailCode;
                if (projectOperationDetailLength > 0)
                    worksheet.Cell(currentRow, projectOperationDetailLength).Value = item.ProjectOperationDetailLength;
                if (projectOperationDetailWidth > 0)
                    worksheet.Cell(currentRow, projectOperationDetailWidth).Value = item.ProjectOperationDetailWidth;
                if (projectOperationDetailHeight > 0)
                    worksheet.Cell(currentRow, projectOperationDetailHeight).Value = item.ProjectOperationDetailHeight;
                if (projectOperationDetailWeight > 0)
                    worksheet.Cell(currentRow, projectOperationDetailWeight).Value = item.ProjectOperationDetailWeight;
                if (projectOperationDetailNumber > 0)
                    worksheet.Cell(currentRow, projectOperationDetailNumber).Value = item.ProjectOperationDetailNumber;
                if (projectOperationDetailFinalAmount > 0)
                    worksheet.Cell(currentRow, projectOperationDetailFinalAmount).Value = item.ProjectOperationDetailFinalAmount;
                if (projectOperationDetailDescription > 0)
                    worksheet.Cell(currentRow, projectOperationDetailDescription).Value = item.ProjectOperationDetailDescription;
                if (projectOperationDetailStatusDescription > 0)
                    worksheet.Cell(currentRow, projectOperationDetailStatusDescription).Value = item.ProjectOperationDetailStatusDescription;
                if (projectOperationDetailStartDateShamsi > 0)
                    worksheet.Cell(currentRow, projectOperationDetailStartDateShamsi).Value = item.ProjectOperationDetailStartDateShamsi;
                if (projectOperationDetailEndDateShamsi > 0)
                    worksheet.Cell(currentRow, projectOperationDetailEndDateShamsi).Value = item.ProjectOperationDetailEndDateShamsi;
                if (projectOperationDetailCreateDateShamsi > 0)
                    worksheet.Cell(currentRow, projectOperationDetailCreateDateShamsi).Value = item.ProjectOperationDetailCreateDateShamsi;
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
                if (dailyProjectOperationFinalAmount > 0)
                    worksheet.Cell(currentRow, dailyProjectOperationFinalAmount).Value = item.DailyProjectOperationFinalAmount;
                if (description > 0)
                    worksheet.Cell(currentRow, description).Value = item.Description;
                if (statusDescription > 0)
                    worksheet.Cell(currentRow, statusDescription).Value = item.StatusDescription;
                if (startDateShamsi > 0)
                    worksheet.Cell(currentRow, startDateShamsi).Value = item.StartDateShamsi;
                if (endDateShamsi > 0)
                    worksheet.Cell(currentRow, endDateShamsi).Value = item.EndDateShamsi;
                if (contractors > 0)
                    worksheet.Cell(currentRow, contractors).Value = item.Contractors;
                if (contractorsNickName > 0)
                    worksheet.Cell(currentRow, contractorsNickName).Value = item.ContractorsNickName;
                if (creatorId > 0)
                    worksheet.Cell(currentRow, creatorId).Value = item.CreatorId;
                if (creatorName > 0)
                    worksheet.Cell(currentRow, creatorName).Value = item.CreatorName;
                if (creatorNickname > 0)
                    worksheet.Cell(currentRow, creatorNickname).Value = item.CreatorNickname;
                if (createdShamsi > 0)
                    worksheet.Cell(currentRow, createdShamsi).Value = item.CreatedShamsi;
                if (serviceInfos > 0)
                    worksheet.Cell(currentRow, serviceInfos).Value = item.ServiceInfos;
            }
        }
        else
        {
            worksheet.Cell(currentRow, 1).Value = DetailedDailyProjectOperationsExcelEnum.Id.GetEnumDescription();
            worksheet.Cell(currentRow, 2).Value = DetailedDailyProjectOperationsExcelEnum.CostCenterId.GetEnumDescription();
            worksheet.Cell(currentRow, 3).Value = DetailedDailyProjectOperationsExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cell(currentRow, 4).Value = DetailedDailyProjectOperationsExcelEnum.ProjectId.GetEnumDescription();
            worksheet.Cell(currentRow, 5).Value = DetailedDailyProjectOperationsExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cell(currentRow, 6).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationId.GetEnumDescription();
            worksheet.Cell(currentRow, 7).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationName.GetEnumDescription();
            worksheet.Cell(currentRow, 8).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationStatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 9).Value = DetailedDailyProjectOperationsExcelEnum.UnitOfMeasurementId.GetEnumDescription();
            worksheet.Cell(currentRow, 10).Value = DetailedDailyProjectOperationsExcelEnum.MeasurementName.GetEnumDescription();
            worksheet.Cell(currentRow, 11).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailId.GetEnumDescription();
            worksheet.Cell(currentRow, 12).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailCode.GetEnumDescription();
            worksheet.Cell(currentRow, 13).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailLength.GetEnumDescription();
            worksheet.Cell(currentRow, 14).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailWidth.GetEnumDescription();
            worksheet.Cell(currentRow, 15).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailHeight.GetEnumDescription();
            worksheet.Cell(currentRow, 16).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailWeight.GetEnumDescription();
            worksheet.Cell(currentRow, 17).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailNumber.GetEnumDescription();
            worksheet.Cell(currentRow, 18).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailFinalAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 19).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 20).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailStatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 21).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailStartDateShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 22).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailEndDateShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 23).Value = DetailedDailyProjectOperationsExcelEnum.ProjectOperationDetailCreateDateShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 24).Value = DetailedDailyProjectOperationsExcelEnum.OperationLocationId.GetEnumDescription();
            worksheet.Cell(currentRow, 25).Value = DetailedDailyProjectOperationsExcelEnum.PrivateName.GetEnumDescription();
            worksheet.Cell(currentRow, 26).Value = DetailedDailyProjectOperationsExcelEnum.PrivateCode.GetEnumDescription();
            worksheet.Cell(currentRow, 27).Value = DetailedDailyProjectOperationsExcelEnum.PublicName.GetEnumDescription();
            worksheet.Cell(currentRow, 28).Value = DetailedDailyProjectOperationsExcelEnum.PublicCode.GetEnumDescription();
            worksheet.Cell(currentRow, 29).Value = DetailedDailyProjectOperationsExcelEnum.Length.GetEnumDescription();
            worksheet.Cell(currentRow, 30).Value = DetailedDailyProjectOperationsExcelEnum.Width.GetEnumDescription();
            worksheet.Cell(currentRow, 31).Value = DetailedDailyProjectOperationsExcelEnum.Height.GetEnumDescription();
            worksheet.Cell(currentRow, 32).Value = DetailedDailyProjectOperationsExcelEnum.Weight.GetEnumDescription();
            worksheet.Cell(currentRow, 33).Value = DetailedDailyProjectOperationsExcelEnum.Number.GetEnumDescription();
            worksheet.Cell(currentRow, 34).Value = DetailedDailyProjectOperationsExcelEnum.DailyProjectOperationFinalAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 35).Value = DetailedDailyProjectOperationsExcelEnum.Description.GetEnumDescription();
            worksheet.Cell(currentRow, 36).Value = DetailedDailyProjectOperationsExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 37).Value = DetailedDailyProjectOperationsExcelEnum.StartDateShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 38).Value = DetailedDailyProjectOperationsExcelEnum.EndDateShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 39).Value = DetailedDailyProjectOperationsExcelEnum.Contractors.GetEnumDescription();
            worksheet.Cell(currentRow, 40).Value = DetailedDailyProjectOperationsExcelEnum.ContractorsNickName.GetEnumDescription();
            worksheet.Cell(currentRow, 41).Value = DetailedDailyProjectOperationsExcelEnum.CreatorId.GetEnumDescription();
            worksheet.Cell(currentRow, 42).Value = DetailedDailyProjectOperationsExcelEnum.CreatorName.GetEnumDescription();
            worksheet.Cell(currentRow, 43).Value = DetailedDailyProjectOperationsExcelEnum.CreatorNickname.GetEnumDescription();
            worksheet.Cell(currentRow, 44).Value = DetailedDailyProjectOperationsExcelEnum.CreatedShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 45).Value = DetailedDailyProjectOperationsExcelEnum.ServiceInfos.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = item.Id;
                worksheet.Cell(currentRow, 2).Value = item.CostCenterId;
                worksheet.Cell(currentRow, 3).Value = item.CostCenterName;
                worksheet.Cell(currentRow, 4).Value = item.ProjectId;
                worksheet.Cell(currentRow, 5).Value = item.ProjectName;
                worksheet.Cell(currentRow, 6).Value = item.ProjectOperationId;
                worksheet.Cell(currentRow, 7).Value = item.ProjectOperationName;
                worksheet.Cell(currentRow, 8).Value = item.ProjectOperationStatusDescription;
                worksheet.Cell(currentRow, 9).Value = item.UnitOfMeasurementId;
                worksheet.Cell(currentRow, 10).Value = item.MeasurementName;
                worksheet.Cell(currentRow, 11).Value = item.ProjectOperationDetailId;
                worksheet.Cell(currentRow, 12).Value = item.ProjectOperationDetailCode;
                worksheet.Cell(currentRow, 13).Value = item.ProjectOperationDetailLength;
                worksheet.Cell(currentRow, 14).Value = item.ProjectOperationDetailWidth;
                worksheet.Cell(currentRow, 15).Value = item.ProjectOperationDetailHeight;
                worksheet.Cell(currentRow, 16).Value = item.ProjectOperationDetailWeight;
                worksheet.Cell(currentRow, 17).Value = item.ProjectOperationDetailNumber;
                worksheet.Cell(currentRow, 18).Value = item.ProjectOperationDetailFinalAmount;
                worksheet.Cell(currentRow, 19).Value = item.ProjectOperationDetailDescription;
                worksheet.Cell(currentRow, 20).Value = item.ProjectOperationDetailStatusDescription;
                worksheet.Cell(currentRow, 21).Value = item.ProjectOperationDetailStartDateShamsi;
                worksheet.Cell(currentRow, 22).Value = item.ProjectOperationDetailEndDateShamsi;
                worksheet.Cell(currentRow, 23).Value = item.ProjectOperationDetailCreateDateShamsi;
                worksheet.Cell(currentRow, 24).Value = item.OperationLocationId;
                worksheet.Cell(currentRow, 25).Value = item.PrivateName;
                worksheet.Cell(currentRow, 26).Value = item.PrivateCode;
                worksheet.Cell(currentRow, 27).Value = item.PublicName;
                worksheet.Cell(currentRow, 28).Value = item.PublicCode;
                worksheet.Cell(currentRow, 29).Value = item.Length;
                worksheet.Cell(currentRow, 30).Value = item.Width;
                worksheet.Cell(currentRow, 31).Value = item.Height;
                worksheet.Cell(currentRow, 32).Value = item.Weight;
                worksheet.Cell(currentRow, 33).Value = item.Number;
                worksheet.Cell(currentRow, 34).Value = item.DailyProjectOperationFinalAmount;
                worksheet.Cell(currentRow, 35).Value = item.Description;
                worksheet.Cell(currentRow, 36).Value = item.StatusDescription;
                worksheet.Cell(currentRow, 37).Value = item.StartDateShamsi;
                worksheet.Cell(currentRow, 38).Value = item.EndDateShamsi;
                worksheet.Cell(currentRow, 39).Value = item.Contractors;
                worksheet.Cell(currentRow, 40).Value = item.ContractorsNickName;
                worksheet.Cell(currentRow, 41).Value = item.CreatorId;
                worksheet.Cell(currentRow, 42).Value = item.CreatorName;
                worksheet.Cell(currentRow, 43).Value = item.CreatorNickname;
                worksheet.Cell(currentRow, 44).Value = item.CreatedShamsi;
                worksheet.Cell(currentRow, 45).Value = item.ServiceInfos;
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static byte[] DailyProjectOperationServiceExcel(
       GetsDailyProjectOperationServiceTotalExcelModel total,
       ICollection<GetsDailyProjectOperationServiceExcelExporterResponseModel> result,
       List<DailyProjectOperationServicesExcelEnum>? excelFilters)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("خدمات کارکرد روزانه");
        var currentRow = 1;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var counter = 0;
            var id = 0;
            var dailyProjectOperationId = 0;
            var costCenterName = 0;
            var projectName = 0;
            var projectOperationName = 0;
            var projectOperationStatusDescription = 0;
            var projectOperationDetailDescription = 0;
            var projectOperationDetailServiceInfoName = 0;
            var projectOperationDetailVolume = 0;
            var projectOperationDetailContractor = 0;
            var projectOperationDetailStatusDescription = 0;
            var projectOperationDetailStartDateShamsi = 0;
            var projectOperationDetailEndDateShamsi = 0;
            var projectOperationDetailCreateDateShamsi = 0;
            var privateName = 0;
            var privateCode = 0;
            var publicName = 0;
            var publicCode = 0;
            var serviceInfoName = 0;
            var volume = 0;
            var description = 0;
            var statusDescription = 0;
            var startDateShamsi = 0;
            var endDateShamsi = 0;
            var contractor = 0;
            var creatorName = 0;
            var createdShamsi = 0;
            var dailyProjectOperationFinalAmount = 0;
            var totalProjectOperationDetailFinalAmount = 0;
            var projectOperationDetailFinalAmount = 0;
            var projectOperationDetailId = 0;

            foreach (var item in excelFilters)
            {
                counter++;
                switch (item)
                {
                    case DailyProjectOperationServicesExcelEnum.Id:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.Id.GetEnumDescription();
                        id = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.DailyProjectOperationId:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.DailyProjectOperationId.GetEnumDescription();
                        dailyProjectOperationId = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.CostCenterName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.CostCenterName.GetEnumDescription();
                        costCenterName = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectName.GetEnumDescription();
                        projectName = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectOperationName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationName.GetEnumDescription();
                        projectOperationName = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectOperationStatusDescription:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationStatusDescription.GetEnumDescription();
                        projectOperationStatusDescription = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectOperationDetailDescription:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailDescription.GetEnumDescription();
                        projectOperationDetailDescription = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectOperationDetailServiceInfoName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailServiceInfoName.GetEnumDescription();
                        projectOperationDetailServiceInfoName = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectOperationDetailVolume:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailVolume.GetEnumDescription();
                        projectOperationDetailVolume = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectOperationDetailContractor:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailContractor.GetEnumDescription();
                        projectOperationDetailContractor = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectOperationDetailStatusDescription:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailStatusDescription.GetEnumDescription();
                        projectOperationDetailStatusDescription = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectOperationDetailStartDateShamsi:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailStartDateShamsi.GetEnumDescription();
                        projectOperationDetailStartDateShamsi = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectOperationDetailEndDateShamsi:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailEndDateShamsi.GetEnumDescription();
                        projectOperationDetailEndDateShamsi = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectOperationDetailCreateDateShamsi:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailCreateDateShamsi.GetEnumDescription();
                        projectOperationDetailCreateDateShamsi = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.PrivateName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.PrivateName.GetEnumDescription();
                        privateName = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.PrivateCode:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.PrivateCode.GetEnumDescription();
                        privateCode = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.PublicName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.PublicName.GetEnumDescription();
                        publicName = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.PublicCode:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.PublicCode.GetEnumDescription();
                        publicCode = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ServiceInfoName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ServiceInfoName.GetEnumDescription();
                        serviceInfoName = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.Volume:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.Volume.GetEnumDescription();
                        volume = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.Description:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.Description.GetEnumDescription();
                        description = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.StatusDescription:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.StatusDescription.GetEnumDescription();
                        statusDescription = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.StartDateShamsi:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.StartDateShamsi.GetEnumDescription();
                        startDateShamsi = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.EndDateShamsi:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.EndDateShamsi.GetEnumDescription();
                        endDateShamsi = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.Contractor:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.Contractor.GetEnumDescription();
                        contractor = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.CreatorName:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.CreatorName.GetEnumDescription();
                        creatorName = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.CreatedShamsi:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.CreatedShamsi.GetEnumDescription();
                        createdShamsi = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectOperationDetailFinalAmount:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailFinalAmount.GetEnumDescription();
                        projectOperationDetailFinalAmount = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.DailyProjectOperationFinalAmount:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.DailyProjectOperationFinalAmount.GetEnumDescription();
                        dailyProjectOperationFinalAmount = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.TotalProjectOperationDetailFinalAmount:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.TotalProjectOperationDetailFinalAmount.GetEnumDescription();
                        totalProjectOperationDetailFinalAmount = counter;
                        break;
                    case DailyProjectOperationServicesExcelEnum.ProjectOperationDetailId:
                        worksheet.Cell(currentRow, counter).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailId.GetEnumDescription();
                        projectOperationDetailId = counter;
                        break;
                }
            }

            foreach (var item in result)
            {
                currentRow++;
                if (id > 0)
                    worksheet.Cell(currentRow, id).Value = item.Id;
                if (dailyProjectOperationId > 0)
                    worksheet.Cell(currentRow, dailyProjectOperationId).Value = item.DailyProjectOperationId;
                if (costCenterName > 0)
                    worksheet.Cell(currentRow, costCenterName).Value = item.CostCenterName;
                if (projectName > 0)
                    worksheet.Cell(currentRow, projectName).Value = item.ProjectName;
                if (projectOperationName > 0)
                    worksheet.Cell(currentRow, projectOperationName).Value = item.ProjectOperationName + "(" + item.MeasurementName + ")";
                if (projectOperationStatusDescription > 0)
                    worksheet.Cell(currentRow, projectOperationStatusDescription).Value = item.ProjectOperationStatusDescription;
                if (projectOperationDetailDescription > 0)
                    worksheet.Cell(currentRow, projectOperationDetailDescription).Value = item.ProjectOperationDetailDescription;
                if (projectOperationDetailServiceInfoName > 0)
                    worksheet.Cell(currentRow, projectOperationDetailServiceInfoName).Value = item.ProjectOperationDetailServiceInfoName + "(" + item.ProjectOperationDetailServiceInfoMeasure + ")";
                if (projectOperationDetailVolume > 0)
                    worksheet.Cell(currentRow, projectOperationDetailVolume).Value = item.ProjectOperationDetailVolume;
                if (projectOperationDetailContractor > 0)
                    worksheet.Cell(currentRow, projectOperationDetailContractor).Value = item.ProjectOperationDetailContractor + "(" + item.ProjectOperationDetailContractorNickName + ")";
                if (projectOperationDetailStatusDescription > 0)
                    worksheet.Cell(currentRow, projectOperationDetailStatusDescription).Value = item.ProjectOperationDetailStatusDescription;
                if (projectOperationDetailStartDateShamsi > 0)
                    worksheet.Cell(currentRow, projectOperationDetailStartDateShamsi).Value = item.ProjectOperationDetailStartDateShamsi;
                if (projectOperationDetailEndDateShamsi > 0)
                    worksheet.Cell(currentRow, projectOperationDetailEndDateShamsi).Value = item.ProjectOperationDetailEndDateShamsi;
                if (projectOperationDetailCreateDateShamsi > 0)
                    worksheet.Cell(currentRow, projectOperationDetailCreateDateShamsi).Value = item.ProjectOperationDetailCreateDateShamsi;
                if (privateName > 0)
                    worksheet.Cell(currentRow, privateName).Value = item.PrivateName;
                if (privateCode > 0)
                    worksheet.Cell(currentRow, privateCode).Value = item.PrivateCode;
                if (publicName > 0)
                    worksheet.Cell(currentRow, publicName).Value = item.PublicName;
                if (publicCode > 0)
                    worksheet.Cell(currentRow, publicCode).Value = item.PublicCode;
                if (serviceInfoName > 0)
                    worksheet.Cell(currentRow, serviceInfoName).Value = item.ServiceInfoName + "(" + item.ServiceInfoMeasure + ")";
                if (volume > 0)
                    worksheet.Cell(currentRow, volume).Value = item.Volume;
                if (description > 0)
                    worksheet.Cell(currentRow, description).Value = item.Description;
                if (statusDescription > 0)
                    worksheet.Cell(currentRow, statusDescription).Value = item.StatusDescription;
                if (startDateShamsi > 0)
                    worksheet.Cell(currentRow, startDateShamsi).Value = item.StartDateShamsi;
                if (endDateShamsi > 0)
                    worksheet.Cell(currentRow, endDateShamsi).Value = item.EndDateShamsi;
                if (contractor > 0)
                    worksheet.Cell(currentRow, contractor).Value = item.Contractor + "(" + item.ContractorNickName + ")";
                if (creatorName > 0)
                    worksheet.Cell(currentRow, creatorName).Value = item.CreatorName + "(" + item.CreatorNickname + ")";
                if (createdShamsi > 0)
                    worksheet.Cell(currentRow, createdShamsi).Value = item.CreatedShamsi;
                if (projectOperationDetailFinalAmount > 0)
                    worksheet.Cell(currentRow, projectOperationDetailFinalAmount).Value = item.ProjectOperationDetailFinalAmount;
                if (dailyProjectOperationFinalAmount > 0)
                    worksheet.Cell(currentRow, dailyProjectOperationFinalAmount).Value = item.DailyProjectOperationFinalAmount;
                if (totalProjectOperationDetailFinalAmount > 0)
                    worksheet.Cell(currentRow, totalProjectOperationDetailFinalAmount).Value = item.TotalProjectOperationDetailFinalAmount;
                if (projectOperationDetailId > 0)
                    worksheet.Cell(currentRow, projectOperationDetailId).Value = item.ProjectOperationDetailId;
            }
        }
        else
        {
            worksheet.Cell(currentRow, 1).Value = DailyProjectOperationServicesExcelEnum.Id.GetEnumDescription();
            worksheet.Cell(currentRow, 2).Value = DailyProjectOperationServicesExcelEnum.DailyProjectOperationId.GetEnumDescription();
            worksheet.Cell(currentRow, 3).Value = DailyProjectOperationServicesExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cell(currentRow, 4).Value = DailyProjectOperationServicesExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cell(currentRow, 5).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationName.GetEnumDescription();
            worksheet.Cell(currentRow, 6).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationStatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 7).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 8).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailServiceInfoName.GetEnumDescription();
            worksheet.Cell(currentRow, 9).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailVolume.GetEnumDescription();
            worksheet.Cell(currentRow, 10).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailContractor.GetEnumDescription();
            worksheet.Cell(currentRow, 11).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailStatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 12).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailStartDateShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 13).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailEndDateShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 14).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailCreateDateShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 15).Value = DailyProjectOperationServicesExcelEnum.PrivateName.GetEnumDescription();
            worksheet.Cell(currentRow, 16).Value = DailyProjectOperationServicesExcelEnum.PrivateCode.GetEnumDescription();
            worksheet.Cell(currentRow, 17).Value = DailyProjectOperationServicesExcelEnum.PublicName.GetEnumDescription();
            worksheet.Cell(currentRow, 18).Value = DailyProjectOperationServicesExcelEnum.PublicCode.GetEnumDescription();
            worksheet.Cell(currentRow, 19).Value = DailyProjectOperationServicesExcelEnum.ServiceInfoName.GetEnumDescription();
            worksheet.Cell(currentRow, 20).Value = DailyProjectOperationServicesExcelEnum.Volume.GetEnumDescription();
            worksheet.Cell(currentRow, 21).Value = DailyProjectOperationServicesExcelEnum.Description.GetEnumDescription();
            worksheet.Cell(currentRow, 22).Value = DailyProjectOperationServicesExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 23).Value = DailyProjectOperationServicesExcelEnum.StartDateShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 24).Value = DailyProjectOperationServicesExcelEnum.EndDateShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 25).Value = DailyProjectOperationServicesExcelEnum.Contractor.GetEnumDescription();
            worksheet.Cell(currentRow, 26).Value = DailyProjectOperationServicesExcelEnum.CreatorName.GetEnumDescription();
            worksheet.Cell(currentRow, 27).Value = DailyProjectOperationServicesExcelEnum.CreatedShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 28).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailFinalAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 29).Value = DailyProjectOperationServicesExcelEnum.DailyProjectOperationFinalAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 30).Value = DailyProjectOperationServicesExcelEnum.TotalProjectOperationDetailFinalAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 31).Value = DailyProjectOperationServicesExcelEnum.ProjectOperationDetailId.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = item.Id;
                worksheet.Cell(currentRow, 2).Value = item.DailyProjectOperationId;
                worksheet.Cell(currentRow, 3).Value = item.CostCenterName;
                worksheet.Cell(currentRow, 4).Value = item.ProjectName;
                worksheet.Cell(currentRow, 5).Value = item.ProjectOperationName + "(" + item.MeasurementName + ")";
                worksheet.Cell(currentRow, 6).Value = item.ProjectOperationStatusDescription;
                worksheet.Cell(currentRow, 7).Value = item.ProjectOperationDetailDescription;
                worksheet.Cell(currentRow, 8).Value = item.ProjectOperationDetailServiceInfoName + "(" + item.ProjectOperationDetailServiceInfoMeasure + ")";
                worksheet.Cell(currentRow, 9).Value = item.ProjectOperationDetailVolume;
                worksheet.Cell(currentRow, 10).Value = item.ProjectOperationDetailContractor + "(" + item.ProjectOperationDetailContractorNickName + ")";
                worksheet.Cell(currentRow, 11).Value = item.ProjectOperationDetailStatusDescription;
                worksheet.Cell(currentRow, 12).Value = item.ProjectOperationDetailStartDateShamsi;
                worksheet.Cell(currentRow, 13).Value = item.ProjectOperationDetailEndDateShamsi;
                worksheet.Cell(currentRow, 14).Value = item.ProjectOperationDetailCreateDateShamsi;
                worksheet.Cell(currentRow, 15).Value = item.PrivateName;
                worksheet.Cell(currentRow, 16).Value = item.PrivateCode;
                worksheet.Cell(currentRow, 17).Value = item.PublicName;
                worksheet.Cell(currentRow, 18).Value = item.PublicCode;
                worksheet.Cell(currentRow, 19).Value = item.ServiceInfoName + "(" + item.ServiceInfoMeasure + ")";
                worksheet.Cell(currentRow, 20).Value = item.Volume;
                worksheet.Cell(currentRow, 21).Value = item.Description;
                worksheet.Cell(currentRow, 22).Value = item.StatusDescription;
                worksheet.Cell(currentRow, 23).Value = item.StartDateShamsi;
                worksheet.Cell(currentRow, 24).Value = item.EndDateShamsi;
                worksheet.Cell(currentRow, 25).Value = item.Contractor + "(" + item.ContractorNickName + ")";
                worksheet.Cell(currentRow, 26).Value = item.CreatorName + "(" + item.CreatorNickname + ")";
                worksheet.Cell(currentRow, 27).Value = item.CreatedShamsi;
                worksheet.Cell(currentRow, 28).Value = item.ProjectOperationDetailFinalAmount;
                worksheet.Cell(currentRow, 29).Value = item.DailyProjectOperationFinalAmount;
                worksheet.Cell(currentRow, 30).Value = item.TotalProjectOperationDetailFinalAmount;
                worksheet.Cell(currentRow, 31).Value = item.ProjectOperationDetailId;
            }
        }

        var worksheet1 = workbook.Worksheets.Add("مجموع کارکرد روزانه ها");
        var currentRow1 = 1;
        worksheet1.Cell(currentRow1, 1).Value = "مجموع حجم خدمات ریزمتره";
        worksheet1.Cell(currentRow1, 2).Value = "مجموع حجم خدمات کارکرد";
        worksheet1.Cell(currentRow1, 3).Value = "حجم باقی مانده";

        worksheet1.Cell(currentRow1 + 1, 1).Value = total.TotalProjectOperationDetailServiceVolume;
        worksheet1.Cell(currentRow1 + 1, 2).Value = total.TotalDailyServiceVolume;
        worksheet1.Cell(currentRow1 + 1, 3).Value = total.RemainingServiceVolume;

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }
}