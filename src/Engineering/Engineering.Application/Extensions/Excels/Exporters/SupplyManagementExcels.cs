using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailsExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailsExcelExporter;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProductExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProductExcelExporter;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsSupplyManagementExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsSupplyManagementExcelExporter;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class SupplyManagementExcels
{
    public static byte[] SupplyManagementToExcel(ICollection<GetsSupplyManagementExcelExporterResponseModel> result,
        List<SupplyManagementExcelEnum>? excelFilters)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("درخواست تامین کالا");
        var currentRow = 1;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var counter = 0;
            var id = 0;
            var requestNumber = 0;
            var typeDescription = 0;
            var maxImportanceDescription = 0;
            var statusDescription = 0;
            var costCenterName = 0;
            var projectName = 0;
            var operationInfoName = 0;
            var measurementId = 0;
            var measurementName = 0;
            var workload = 0;
            var operationLocationName = 0;
            var finalAmount = 0;
            var createdOn = 0;
            var creatorId = 0;
            var creator = 0;
            var percent = 0;
            var rejectedNumber = 0;
            var allInStock = 0;
            var inStockNumber = 0;
            var allBetweenStock = 0;
            var betweenStockNumber = 0;
            var allCommerce = 0;
            var commerceNuber = 0;
            var companyId = 0;
            var companyNameFa = 0;

            foreach (var item in excelFilters)
            {
                counter++;
                switch (item)
                {
                    case SupplyManagementExcelEnum.Id:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.Id.GetEnumDescription();
                        id = counter;
                        break;
                    case SupplyManagementExcelEnum.RequestNumber:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.RequestNumber.GetEnumDescription();
                        requestNumber = counter;
                        break;
                    case SupplyManagementExcelEnum.TypeDescription:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.TypeDescription.GetEnumDescription();
                        typeDescription = counter;
                        break;
                    case SupplyManagementExcelEnum.MaxImportanceDescription:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.MaxImportanceDescription.GetEnumDescription();
                        maxImportanceDescription = counter;
                        break;
                    case SupplyManagementExcelEnum.StatusDescription:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.StatusDescription.GetEnumDescription();
                        statusDescription = counter;
                        break;
                    case SupplyManagementExcelEnum.CostCenterName:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.CostCenterName.GetEnumDescription();
                        costCenterName = counter;
                        break;
                    case SupplyManagementExcelEnum.ProjectName:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.ProjectName.GetEnumDescription();
                        projectName = counter;
                        break;
                    case SupplyManagementExcelEnum.OperationInfoName:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.OperationInfoName.GetEnumDescription();
                        operationInfoName = counter;
                        break;
                    case SupplyManagementExcelEnum.MeasurementId:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.MeasurementId.GetEnumDescription();
                        measurementId = counter;
                        break;
                    case SupplyManagementExcelEnum.MeasurementName:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.MeasurementName.GetEnumDescription();
                        measurementName = counter;
                        break;
                    case SupplyManagementExcelEnum.Workload:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.Workload.GetEnumDescription();
                        workload = counter;
                        break;
                    case SupplyManagementExcelEnum.OperationLocationName:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.OperationLocationName.GetEnumDescription();
                        operationLocationName = counter;
                        break;
                    case SupplyManagementExcelEnum.FinalAmount:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.FinalAmount.GetEnumDescription();
                        finalAmount = counter;
                        break;
                    case SupplyManagementExcelEnum.CreatedOn:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.CreatedOn.GetEnumDescription();
                        createdOn = counter;
                        break;
                    case SupplyManagementExcelEnum.CreatorId:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.CreatorId.GetEnumDescription();
                        creatorId = counter;
                        break;
                    case SupplyManagementExcelEnum.Creator:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.Creator.GetEnumDescription();
                        creator = counter;
                        break;
                    case SupplyManagementExcelEnum.Percent:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.Percent.GetEnumDescription();
                        percent = counter;
                        break;
                    case SupplyManagementExcelEnum.RejectedNumber:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.RejectedNumber.GetEnumDescription();
                        rejectedNumber = counter;
                        break;
                    case SupplyManagementExcelEnum.AllInStock:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.AllInStock.GetEnumDescription();
                        allInStock = counter;
                        break;
                    case SupplyManagementExcelEnum.InStockNumber:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.InStockNumber.GetEnumDescription();
                        inStockNumber = counter;
                        break;
                    case SupplyManagementExcelEnum.AllBetweenStock:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.AllBetweenStock.GetEnumDescription();
                        allBetweenStock = counter;
                        break;
                    case SupplyManagementExcelEnum.BetweenStockNumber:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.BetweenStockNumber.GetEnumDescription();
                        betweenStockNumber = counter;
                        break;
                    case SupplyManagementExcelEnum.AllCommerce:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.AllCommerce.GetEnumDescription();
                        allCommerce = counter;
                        break;
                    case SupplyManagementExcelEnum.CommerceNuber:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.CommerceNuber.GetEnumDescription();
                        commerceNuber = counter;
                        break;
                    case SupplyManagementExcelEnum.CompanyId:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.CompanyId.GetEnumDescription();
                        companyId = counter;
                        break;
                    case SupplyManagementExcelEnum.CompanyNameFa:
                        worksheet.Cell(currentRow, counter).Value = SupplyManagementExcelEnum.CompanyNameFa.GetEnumDescription();
                        companyNameFa = counter;
                        break;
                }
            }

            foreach (var item in result)
            {
                currentRow++;
                if (id > 0)
                    worksheet.Cell(currentRow, id).Value = item.Id;
                if (statusDescription > 0)
                    worksheet.Cell(currentRow, statusDescription).Value = item.StatusDescription;
                if (typeDescription > 0)
                    worksheet.Cell(currentRow, typeDescription).Value = item.TypeDescription;
                if (maxImportanceDescription > 0)
                    worksheet.Cell(currentRow, maxImportanceDescription).Value = item.MaxImportanceDescription;
                if (requestNumber > 0)
                    worksheet.Cell(currentRow, requestNumber).Value = item.RequestNumber;
                if (measurementId > 0)
                    worksheet.Cell(currentRow, measurementId).Value = item.MeasurementId;
                if (costCenterName > 0)
                    worksheet.Cell(currentRow, costCenterName).Value = item.CostCenterName;
                if (measurementName > 0)
                    worksheet.Cell(currentRow, measurementName).Value = item.MeasurementName;
                if (projectName > 0)
                    worksheet.Cell(currentRow, projectName).Value = item.ProjectName;
                if (operationInfoName > 0)
                    worksheet.Cell(currentRow, operationInfoName).Value = item.OperationInfoName;
                if (workload > 0)
                    worksheet.Cell(currentRow, workload).Value = item.Workload;
                if (operationLocationName > 0)
                    worksheet.Cell(currentRow, operationLocationName).Value = item.PrivateName;
                if (finalAmount > 0)
                    worksheet.Cell(currentRow, finalAmount).Value = item.FinalAmount;
                if (createdOn > 0)
                    worksheet.Cell(currentRow, createdOn).Value = item.CreatedOn;
                if (creatorId > 0)
                    worksheet.Cell(currentRow, creatorId).Value = item.CreatorId;
                if (creator > 0)
                    worksheet.Cell(currentRow, creator).Value = item.Creator;
                if (percent > 0)
                    worksheet.Cell(currentRow, percent).Value = item.Percent;
                if (rejectedNumber > 0)
                    worksheet.Cell(currentRow, rejectedNumber).Value = item.RejectedNumber;
                if (allInStock > 0)
                    worksheet.Cell(currentRow, allInStock).Value = item.AllInStock;
                if (inStockNumber > 0)
                    worksheet.Cell(currentRow, inStockNumber).Value = item.InStockNumber;
                if (allBetweenStock > 0)
                    worksheet.Cell(currentRow, allBetweenStock).Value = item.AllBetweenStock;
                if (betweenStockNumber > 0)
                    worksheet.Cell(currentRow, betweenStockNumber).Value = item.BetweenStockNumber;
                if (allCommerce > 0)
                    worksheet.Cell(currentRow, allCommerce).Value = item.AllCommerce;
                if (commerceNuber > 0)
                    worksheet.Cell(currentRow, commerceNuber).Value = item.CommerceNuber;
                if (companyId > 0)
                    worksheet.Cell(currentRow, companyId).Value = item.CompanyId;
                if (companyNameFa > 0)
                    worksheet.Cell(currentRow, companyNameFa).Value = item.CompanyNameFa;
            }
        }
        else
        {
            worksheet.Cell(currentRow, 1).Value = SupplyManagementExcelEnum.Id.GetEnumDescription();
            worksheet.Cell(currentRow, 2).Value = SupplyManagementExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 3).Value = SupplyManagementExcelEnum.TypeDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 4).Value = SupplyManagementExcelEnum.MaxImportanceDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 5).Value = SupplyManagementExcelEnum.RequestNumber.GetEnumDescription();
            worksheet.Cell(currentRow, 6).Value = SupplyManagementExcelEnum.MeasurementId.GetEnumDescription();
            worksheet.Cell(currentRow, 7).Value = SupplyManagementExcelEnum.CostCenterName.GetEnumDescription();
            worksheet.Cell(currentRow, 8).Value = SupplyManagementExcelEnum.MeasurementName.GetEnumDescription();
            worksheet.Cell(currentRow, 9).Value = SupplyManagementExcelEnum.ProjectName.GetEnumDescription();
            worksheet.Cell(currentRow, 10).Value = SupplyManagementExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet.Cell(currentRow, 11).Value = SupplyManagementExcelEnum.Workload.GetEnumDescription();
            worksheet.Cell(currentRow, 12).Value = SupplyManagementExcelEnum.OperationLocationName.GetEnumDescription();
            worksheet.Cell(currentRow, 13).Value = SupplyManagementExcelEnum.FinalAmount.GetEnumDescription();
            worksheet.Cell(currentRow, 14).Value = SupplyManagementExcelEnum.CreatedOn.GetEnumDescription();
            worksheet.Cell(currentRow, 15).Value = SupplyManagementExcelEnum.CreatorId.GetEnumDescription();
            worksheet.Cell(currentRow, 16).Value = SupplyManagementExcelEnum.Creator.GetEnumDescription();
            worksheet.Cell(currentRow, 17).Value = SupplyManagementExcelEnum.Percent.GetEnumDescription();
            worksheet.Cell(currentRow, 18).Value = SupplyManagementExcelEnum.RejectedNumber.GetEnumDescription();
            worksheet.Cell(currentRow, 19).Value = SupplyManagementExcelEnum.AllInStock.GetEnumDescription();
            worksheet.Cell(currentRow, 20).Value = SupplyManagementExcelEnum.InStockNumber.GetEnumDescription();
            worksheet.Cell(currentRow, 21).Value = SupplyManagementExcelEnum.AllBetweenStock.GetEnumDescription();
            worksheet.Cell(currentRow, 22).Value = SupplyManagementExcelEnum.BetweenStockNumber.GetEnumDescription();
            worksheet.Cell(currentRow, 23).Value = SupplyManagementExcelEnum.AllCommerce.GetEnumDescription();
            worksheet.Cell(currentRow, 24).Value = SupplyManagementExcelEnum.CommerceNuber.GetEnumDescription();
            worksheet.Cell(currentRow, 25).Value = SupplyManagementExcelEnum.CompanyId.GetEnumDescription();
            worksheet.Cell(currentRow, 26).Value = SupplyManagementExcelEnum.CompanyNameFa.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = item.Id;
                worksheet.Cell(currentRow, 2).Value = item.StatusDescription;
                worksheet.Cell(currentRow, 3).Value = item.TypeDescription;
                worksheet.Cell(currentRow, 4).Value = item.MaxImportanceDescription;
                worksheet.Cell(currentRow, 5).Value = item.StatusDescription;
                worksheet.Cell(currentRow, 6).Value = item.CostCenterName;
                worksheet.Cell(currentRow, 7).Value = item.ProjectName;
                worksheet.Cell(currentRow, 8).Value = item.OperationInfoName;
                worksheet.Cell(currentRow, 9).Value = item.MeasurementId;
                worksheet.Cell(currentRow, 10).Value = item.MeasurementName;
                worksheet.Cell(currentRow, 11).Value = item.Workload;
                worksheet.Cell(currentRow, 12).Value = item.PrivateName;
                worksheet.Cell(currentRow, 13).Value = item.FinalAmount;
                worksheet.Cell(currentRow, 14).Value = item.CreatedOn;
                worksheet.Cell(currentRow, 15).Value = item.CreatorId;
                worksheet.Cell(currentRow, 16).Value = item.Creator;
                worksheet.Cell(currentRow, 17).Value = item.Percent;
                worksheet.Cell(currentRow, 18).Value = item.RejectedNumber;
                worksheet.Cell(currentRow, 19).Value = item.AllInStock;
                worksheet.Cell(currentRow, 20).Value = item.InStockNumber;
                worksheet.Cell(currentRow, 21).Value = item.AllBetweenStock;
                worksheet.Cell(currentRow, 22).Value = item.BetweenStockNumber;
                worksheet.Cell(currentRow, 23).Value = item.AllCommerce;
                worksheet.Cell(currentRow, 24).Value = item.CommerceNuber;
                worksheet.Cell(currentRow, 25).Value = item.CompanyId;
                worksheet.Cell(currentRow, 26).Value = item.CompanyNameFa;
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static byte[] SupplyDetailToExcel(ICollection<GetRequestGoodsSupplyDetailsExcelExporterResponseModel> result,
        List<GoodsSupplyDetailExcelEnum>? excelFilters)
    {
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("کارشناس ارشد درخواست تامین");
        var currentRow = 1;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var counter = 0;
            var id = 0;
            var requestNumber = 0;
            var createdShamsi = 0;
            var productNumber = 0;
            var groupName = 0;
            var groupCode = 0;
            var groupMeasure = 0;
            var productName = 0;
            var productCode = 0;
            var brand = 0;
            var brandModel = 0;
            var privateName = 0;
            var privateCode = 0;
            var publicName = 0;
            var publicCode = 0;
            var projectOperationDetailDescription = 0;
            var costCenter = 0;
            var project = 0;
            var projectOperation = 0;
            var tolerancePercentage = 0;
            var toleranceCount = 0;
            var totalEstimatedCount = 0;
            var totalRequestedCount = 0;
            var totalSupplyCount = 0;
            var totalRemainedCount = 0;
            var requestedCount = 0;
            var supplyCount = 0;
            var remainedCount = 0;
            var delivaryDeadLine = 0;
            var currency = 0;
            var statusDescription = 0;
            var requestTypeDescription = 0;
            var creator = 0;
            var description = 0;
            var managementDescription = 0;
            var destinationWarehouse = 0;
            var contractor = 0;
            var nickName = 0;
            var customerInvoiceNumber = 0;
            var quantity = 0;
            var title = 0;
            var packageCount = 0;
            var supplier = 0;
            var lastDescription = 0;

            foreach (var item in excelFilters)
            {
                counter++;
                switch (item)
                {
                    case GoodsSupplyDetailExcelEnum.Id:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.Id.GetEnumDescription();
                        id = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.RequestNumber:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.RequestNumber.GetEnumDescription();
                        requestNumber = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.CreatedShamsi:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.CreatedShamsi.GetEnumDescription();
                        createdShamsi = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.ProductNumber:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.ProductNumber.GetEnumDescription();
                        productNumber = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.GroupName:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.GroupName.GetEnumDescription();
                        groupName = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.GroupCode:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.GroupCode.GetEnumDescription();
                        groupCode = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.GroupMeasure:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.GroupMeasure.GetEnumDescription();
                        groupMeasure = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.ProductName:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.ProductName.GetEnumDescription();
                        productName = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.ProductCode:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.ProductCode.GetEnumDescription();
                        productCode = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.Brand:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.Brand.GetEnumDescription();
                        brand = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.BrandModel:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.BrandModel.GetEnumDescription();
                        brandModel = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.PrivateName:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.PrivateName.GetEnumDescription();
                        privateName = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.PrivateCode:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.PrivateCode.GetEnumDescription();
                        privateCode = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.PublicName:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.PublicName.GetEnumDescription();
                        publicName = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.PublicCode:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.PublicCode.GetEnumDescription();
                        publicCode = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.ProjectOperationDetailDescription:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.ProjectOperationDetailDescription.GetEnumDescription();
                        projectOperationDetailDescription = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.CostCenter:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.CostCenter.GetEnumDescription();
                        costCenter = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.Project:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.Project.GetEnumDescription();
                        project = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.ProjectOperation:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.ProjectOperation.GetEnumDescription();
                        projectOperation = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.TolerancePercentage:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.TolerancePercentage.GetEnumDescription();
                        tolerancePercentage = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.ToleranceCount:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.ToleranceCount.GetEnumDescription();
                        toleranceCount = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.TotalEstimatedCount:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.TotalEstimatedCount.GetEnumDescription();
                        totalEstimatedCount = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.TotalRequestedCount:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.TotalRequestedCount.GetEnumDescription();
                        totalRequestedCount = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.TotalSupplyCount:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.TotalSupplyCount.GetEnumDescription();
                        totalSupplyCount = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.TotalRemainedCount:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.TotalRemainedCount.GetEnumDescription();
                        totalRemainedCount = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.RequestedCount:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.RequestedCount.GetEnumDescription();
                        requestedCount = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.SupplyCount:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.SupplyCount.GetEnumDescription();
                        supplyCount = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.RemainedCount:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.RemainedCount.GetEnumDescription();
                        remainedCount = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.DelivaryDeadLine:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.DelivaryDeadLine.GetEnumDescription();
                        delivaryDeadLine = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.Currency:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.Currency.GetEnumDescription();
                        currency = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.StatusDescription:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.StatusDescription.GetEnumDescription();
                        statusDescription = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.RequestTypeDescription:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.RequestTypeDescription.GetEnumDescription();
                        requestTypeDescription = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.Creator:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.Creator.GetEnumDescription();
                        creator = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.Description:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.Description.GetEnumDescription();
                        description = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.ManagementDescription:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.ManagementDescription.GetEnumDescription();
                        managementDescription = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.DestinationWarehouse:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.DestinationWarehouse.GetEnumDescription();
                        destinationWarehouse = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.Contractor:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.Contractor.GetEnumDescription();
                        contractor = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.NickName:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.NickName.GetEnumDescription();
                        nickName = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.CustomerInvoiceNumber:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.CustomerInvoiceNumber.GetEnumDescription();
                        customerInvoiceNumber = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.Quantity:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.Quantity.GetEnumDescription();
                        quantity = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.Title:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.Title.GetEnumDescription();
                        title = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.PackageCount:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.PackageCount.GetEnumDescription();
                        packageCount = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.Supplier:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.Supplier.GetEnumDescription();
                        supplier = counter;
                        break;
                    case GoodsSupplyDetailExcelEnum.LastDescription:
                        worksheet.Cell(currentRow, counter).Value = GoodsSupplyDetailExcelEnum.LastDescription.GetEnumDescription();
                        lastDescription = counter;
                        break;
                }
            }

            foreach (var item in result)
            {
                currentRow++;
                if (id > 0)
                    worksheet.Cell(currentRow, id).Value = item.Id;
                if (requestNumber > 0)
                    worksheet.Cell(currentRow, requestNumber).Value = item.RequestNumber;
                if (createdShamsi > 0)
                    worksheet.Cell(currentRow, createdShamsi).Value = item.CreatedShamsi;
                if (productNumber > 0)
                    worksheet.Cell(currentRow, productNumber).Value = item.ProductNumber;
                if (groupName > 0)
                    worksheet.Cell(currentRow, groupName).Value = item.GroupName;
                if (groupCode > 0)
                    worksheet.Cell(currentRow, groupCode).Value = item.GroupCode;
                if (groupMeasure > 0)
                    worksheet.Cell(currentRow, groupMeasure).Value = item.GroupMeasure;
                if (productName > 0)
                    worksheet.Cell(currentRow, productName).Value = item.ProductName;
                if (productCode > 0)
                    worksheet.Cell(currentRow, productCode).Value = item.ProductCode;
                if (brand > 0)
                    worksheet.Cell(currentRow, brand).Value = item.Brand;
                if (brandModel > 0)
                    worksheet.Cell(currentRow, brandModel).Value = item.BrandModel;
                if (privateName > 0)
                    worksheet.Cell(currentRow, privateName).Value = item.PrivateName;
                if (privateCode > 0)
                    worksheet.Cell(currentRow, privateCode).Value = item.PrivateCode;
                if (publicName > 0)
                    worksheet.Cell(currentRow, publicName).Value = item.PublicName;
                if (publicCode > 0)
                    worksheet.Cell(currentRow, publicCode).Value = item.PublicCode;
                if (projectOperationDetailDescription > 0)
                    worksheet.Cell(currentRow, projectOperationDetailDescription).Value = item.ProjectOperationDetailDescription;
                if (costCenter > 0)
                    worksheet.Cell(currentRow, costCenter).Value = item.CostCenter;
                if (project > 0)
                    worksheet.Cell(currentRow, project).Value = item.Project;
                if (projectOperation > 0)
                    worksheet.Cell(currentRow, projectOperation).Value = item.ProjectOperation;
                if (tolerancePercentage > 0)
                    worksheet.Cell(currentRow, tolerancePercentage).Value = item.TolerancePercentage;
                if (toleranceCount > 0)
                    worksheet.Cell(currentRow, toleranceCount).Value = item.ToleranceCount;
                if (totalEstimatedCount > 0)
                    worksheet.Cell(currentRow, totalEstimatedCount).Value = item.TotalEstimatedCount;
                if (totalRequestedCount > 0)
                    worksheet.Cell(currentRow, totalRequestedCount).Value = item.TotalRequestedCount;
                if (totalSupplyCount > 0)
                    worksheet.Cell(currentRow, totalSupplyCount).Value = item.TotalSupplyCount;
                if (totalRemainedCount > 0)
                    worksheet.Cell(currentRow, totalRemainedCount).Value = item.TotalRemainedCount;
                if (requestedCount > 0)
                    worksheet.Cell(currentRow, requestedCount).Value = item.RequestedCount;
                if (supplyCount > 0)
                    worksheet.Cell(currentRow, supplyCount).Value = item.SupplyCount;
                if (remainedCount > 0)
                    worksheet.Cell(currentRow, remainedCount).Value = item.RemainedCount;
                if (delivaryDeadLine > 0)
                    worksheet.Cell(currentRow, delivaryDeadLine).Value = item.DelivaryDeadLine;
                if (currency > 0)
                    worksheet.Cell(currentRow, currency).Value = item.Currency;
                if (statusDescription > 0)
                    worksheet.Cell(currentRow, statusDescription).Value = item.StatusDescription;
                if (requestTypeDescription > 0)
                    worksheet.Cell(currentRow, requestTypeDescription).Value = item.RequestTypeDescription;
                if (creator > 0)
                    worksheet.Cell(currentRow, creator).Value = item.Creator;
                if (description > 0)
                    worksheet.Cell(currentRow, description).Value = item.Description;
                if (managementDescription > 0)
                    worksheet.Cell(currentRow, managementDescription).Value = item.ManagementDescription;
                if (destinationWarehouse > 0)
                    worksheet.Cell(currentRow, destinationWarehouse).Value = item.DestinationWarehouse;
                if (contractor > 0)
                    worksheet.Cell(currentRow, contractor).Value = item.Contractor;
                if (nickName > 0)
                    worksheet.Cell(currentRow, nickName).Value = item.NickName;
                if (customerInvoiceNumber > 0)
                    worksheet.Cell(currentRow, customerInvoiceNumber).Value = item.CustomerInvoiceNumber;
                if (quantity > 0)
                    worksheet.Cell(currentRow, quantity).Value = item.Quantity;
                if (title > 0)
                    worksheet.Cell(currentRow, title).Value = item.Title;
                if (packageCount > 0)
                    worksheet.Cell(currentRow, packageCount).Value = item.PackageCount;
                if (supplier > 0)
                    worksheet.Cell(currentRow, supplier).Value = item.Supplier;
                if (lastDescription > 0)
                    worksheet.Cell(currentRow, lastDescription).Value = item.LastDescription;
            }
        }
        else
        {
            worksheet.Cell(currentRow, 1).Value = GoodsSupplyDetailExcelEnum.Id.GetEnumDescription();
            worksheet.Cell(currentRow, 2).Value = GoodsSupplyDetailExcelEnum.RequestNumber.GetEnumDescription();
            worksheet.Cell(currentRow, 3).Value = GoodsSupplyDetailExcelEnum.CreatedShamsi.GetEnumDescription();
            worksheet.Cell(currentRow, 4).Value = GoodsSupplyDetailExcelEnum.ProductNumber.GetEnumDescription();
            worksheet.Cell(currentRow, 5).Value = GoodsSupplyDetailExcelEnum.GroupName.GetEnumDescription();
            worksheet.Cell(currentRow, 6).Value = GoodsSupplyDetailExcelEnum.GroupCode.GetEnumDescription();
            worksheet.Cell(currentRow, 7).Value = GoodsSupplyDetailExcelEnum.GroupMeasure.GetEnumDescription();
            worksheet.Cell(currentRow, 8).Value = GoodsSupplyDetailExcelEnum.ProductName.GetEnumDescription();
            worksheet.Cell(currentRow, 9).Value = GoodsSupplyDetailExcelEnum.ProductCode.GetEnumDescription();
            worksheet.Cell(currentRow, 10).Value = GoodsSupplyDetailExcelEnum.Brand.GetEnumDescription();
            worksheet.Cell(currentRow, 11).Value = GoodsSupplyDetailExcelEnum.BrandModel.GetEnumDescription();
            worksheet.Cell(currentRow, 12).Value = GoodsSupplyDetailExcelEnum.PrivateName.GetEnumDescription();
            worksheet.Cell(currentRow, 13).Value = GoodsSupplyDetailExcelEnum.PrivateCode.GetEnumDescription();
            worksheet.Cell(currentRow, 14).Value = GoodsSupplyDetailExcelEnum.PublicName.GetEnumDescription();
            worksheet.Cell(currentRow, 15).Value = GoodsSupplyDetailExcelEnum.PublicCode.GetEnumDescription();
            worksheet.Cell(currentRow, 16).Value = GoodsSupplyDetailExcelEnum.ProjectOperationDetailDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 17).Value = GoodsSupplyDetailExcelEnum.CostCenter.GetEnumDescription();
            worksheet.Cell(currentRow, 18).Value = GoodsSupplyDetailExcelEnum.Project.GetEnumDescription();
            worksheet.Cell(currentRow, 19).Value = GoodsSupplyDetailExcelEnum.ProjectOperation.GetEnumDescription();
            worksheet.Cell(currentRow, 20).Value = GoodsSupplyDetailExcelEnum.TolerancePercentage.GetEnumDescription();
            worksheet.Cell(currentRow, 21).Value = GoodsSupplyDetailExcelEnum.ToleranceCount.GetEnumDescription();
            worksheet.Cell(currentRow, 22).Value = GoodsSupplyDetailExcelEnum.TotalEstimatedCount.GetEnumDescription();
            worksheet.Cell(currentRow, 23).Value = GoodsSupplyDetailExcelEnum.TotalRequestedCount.GetEnumDescription();
            worksheet.Cell(currentRow, 24).Value = GoodsSupplyDetailExcelEnum.TotalSupplyCount.GetEnumDescription();
            worksheet.Cell(currentRow, 25).Value = GoodsSupplyDetailExcelEnum.TotalRemainedCount.GetEnumDescription();
            worksheet.Cell(currentRow, 26).Value = GoodsSupplyDetailExcelEnum.RequestedCount.GetEnumDescription();
            worksheet.Cell(currentRow, 27).Value = GoodsSupplyDetailExcelEnum.SupplyCount.GetEnumDescription();
            worksheet.Cell(currentRow, 28).Value = GoodsSupplyDetailExcelEnum.RemainedCount.GetEnumDescription();
            worksheet.Cell(currentRow, 29).Value = GoodsSupplyDetailExcelEnum.DelivaryDeadLine.GetEnumDescription();
            worksheet.Cell(currentRow, 30).Value = GoodsSupplyDetailExcelEnum.Currency.GetEnumDescription();
            worksheet.Cell(currentRow, 31).Value = GoodsSupplyDetailExcelEnum.StatusDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 32).Value = GoodsSupplyDetailExcelEnum.RequestTypeDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 33).Value = GoodsSupplyDetailExcelEnum.Creator.GetEnumDescription();
            worksheet.Cell(currentRow, 34).Value = GoodsSupplyDetailExcelEnum.Description.GetEnumDescription();
            worksheet.Cell(currentRow, 35).Value = GoodsSupplyDetailExcelEnum.ManagementDescription.GetEnumDescription();
            worksheet.Cell(currentRow, 36).Value = GoodsSupplyDetailExcelEnum.DestinationWarehouse.GetEnumDescription();
            worksheet.Cell(currentRow, 37).Value = GoodsSupplyDetailExcelEnum.Contractor.GetEnumDescription();
            worksheet.Cell(currentRow, 38).Value = GoodsSupplyDetailExcelEnum.NickName.GetEnumDescription();
            worksheet.Cell(currentRow, 39).Value = GoodsSupplyDetailExcelEnum.CustomerInvoiceNumber.GetEnumDescription();
            worksheet.Cell(currentRow, 40).Value = GoodsSupplyDetailExcelEnum.Quantity.GetEnumDescription();
            worksheet.Cell(currentRow, 41).Value = GoodsSupplyDetailExcelEnum.Title.GetEnumDescription();
            worksheet.Cell(currentRow, 42).Value = GoodsSupplyDetailExcelEnum.PackageCount.GetEnumDescription();
            worksheet.Cell(currentRow, 43).Value = GoodsSupplyDetailExcelEnum.Supplier.GetEnumDescription();
            worksheet.Cell(currentRow, 44).Value = GoodsSupplyDetailExcelEnum.LastDescription.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = item.Id;
                worksheet.Cell(currentRow, 2).Value = item.RequestNumber;
                worksheet.Cell(currentRow, 3).Value = item.CreatedShamsi;
                worksheet.Cell(currentRow, 4).Value = item.ProductNumber;
                worksheet.Cell(currentRow, 5).Value = item.GroupName;
                worksheet.Cell(currentRow, 6).Value = item.GroupCode;
                worksheet.Cell(currentRow, 7).Value = item.GroupMeasure;
                worksheet.Cell(currentRow, 8).Value = item.ProductName;
                worksheet.Cell(currentRow, 9).Value = item.ProductCode;
                worksheet.Cell(currentRow, 10).Value = item.Brand;
                worksheet.Cell(currentRow, 11).Value = item.BrandModel;
                worksheet.Cell(currentRow, 12).Value = item.PrivateName;
                worksheet.Cell(currentRow, 13).Value = item.PrivateCode;
                worksheet.Cell(currentRow, 14).Value = item.PublicName;
                worksheet.Cell(currentRow, 15).Value = item.PublicCode;
                worksheet.Cell(currentRow, 16).Value = item.ProjectOperationDetailDescription;
                worksheet.Cell(currentRow, 17).Value = item.CostCenter;
                worksheet.Cell(currentRow, 18).Value = item.Project;
                worksheet.Cell(currentRow, 19).Value = item.ProjectOperation;
                worksheet.Cell(currentRow, 20).Value = item.TolerancePercentage;
                worksheet.Cell(currentRow, 21).Value = item.ToleranceCount;
                worksheet.Cell(currentRow, 22).Value = item.TotalEstimatedCount;
                worksheet.Cell(currentRow, 23).Value = item.TotalRequestedCount;
                worksheet.Cell(currentRow, 24).Value = item.TotalSupplyCount;
                worksheet.Cell(currentRow, 25).Value = item.TotalRemainedCount;
                worksheet.Cell(currentRow, 26).Value = item.RequestedCount;
                worksheet.Cell(currentRow, 27).Value = item.SupplyCount;
                worksheet.Cell(currentRow, 28).Value = item.RemainedCount;
                worksheet.Cell(currentRow, 29).Value = item.DelivaryDeadLine;
                worksheet.Cell(currentRow, 30).Value = item.Currency;
                worksheet.Cell(currentRow, 31).Value = item.StatusDescription;
                worksheet.Cell(currentRow, 32).Value = item.RequestTypeDescription;
                worksheet.Cell(currentRow, 33).Value = item.Creator;
                worksheet.Cell(currentRow, 34).Value = item.Description;
                worksheet.Cell(currentRow, 35).Value = item.ManagementDescription;
                worksheet.Cell(currentRow, 36).Value = item.DestinationWarehouse;
                worksheet.Cell(currentRow, 37).Value = item.Contractor;
                worksheet.Cell(currentRow, 38).Value = item.NickName;
                worksheet.Cell(currentRow, 39).Value = item.CustomerInvoiceNumber;
                worksheet.Cell(currentRow, 40).Value = item.Quantity;
                worksheet.Cell(currentRow, 41).Value = item.Title;
                worksheet.Cell(currentRow, 42).Value = item.PackageCount;
                worksheet.Cell(currentRow, 43).Value = item.Supplier;
                worksheet.Cell(currentRow, 44).Value = item.LastDescription;
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }



    public static byte[] RequestGoodsSupplyProductToExcel(
        ICollection<GetsRequestGoodsSupplyProductExcelExporterResponseModel> result,
        ICollection<GetsRequestGoodsSupplyProductManagementExcelExporterModel> managements,
        List<RequestGoodsSupplyProductExcelEnum>? excelFilters)
    {
        using var workbook = new XLWorkbook();

        var worksheet1 = workbook.Worksheets.Add("گزارش درخواست تامین کالا");
        var currentRow1 = 1;
        var worksheet2 = workbook.Worksheets.Add("گزارش کارشناس ارشد هر درخواست");
        var currentRow2 = 1;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var counter1 = 0;
            var id = 0;
            var requestGoodsSupplyId = 0;
            var serialNumber = 0;
            var requestNumber = 0;
            var costCenterName = 0;
            var projectName = 0;
            var projectManager = 0;
            var projectOperationName = 0;
            var projectOperationCode = 0;
            var measurementName = 0;
            var workload = 0;
            var projectOperationDetailNames = 0;
            var projectOperationDetailCodes = 0;
            var importanceDescription = 0;
            var statusDescription = 0;
            var typeDescription = 0;
            var productName = 0;
            var productCode = 0;
            var productBrand = 0;
            var productBrandModel = 0;
            var productGroupName = 0;
            var productGroupCode = 0;
            var requestedCount = 0;
            var supplyCount = 0;
            var remainedCount = 0;
            var delivaryDeadLine = 0;
            var requestedDate = 0;
            var created = 0;
            var unitPrice = 0;
            var totalPrice = 0;
            var taxPercentage = 0;
            var taxNumber = 0;
            var discountByPercentage = 0;
            var discountByNumber = 0;
            var discountedPrice = 0;
            var packingPrice = 0;
            var transferPrice = 0;
            var otherPrice = 0;
            var finalPrice = 0;
            var contractorFullName = 0;
            var supplyerFullName = 0;
            var currencyName = 0;
            var destinationWarehouseName = 0;
            var customerInvoiceNumber = 0;
            var creator = 0;
            var description = 0;
            var managementDescription = 0;
            var lastDescription = 0;
            var packageName = 0;
            var packageQuantity = 0;
            var packageCount = 0;
            var packageUnitPrice = 0;
            var operatorAppointmentName = 0;
            var projectOperationDetailDescription = 0;

            foreach (var item in excelFilters)
            {
                counter1++;
                switch (item)
                {
                    case RequestGoodsSupplyProductExcelEnum.Id:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.Id.GetEnumDescription();
                        id = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.RequestGoodsSupplyId:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.RequestGoodsSupplyId.GetEnumDescription();
                        requestGoodsSupplyId = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.SerialNumber:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.SerialNumber.GetEnumDescription();
                        serialNumber = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.RequestNumber:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.RequestNumber.GetEnumDescription();
                        requestNumber = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.CostCenterName:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.CostCenterName.GetEnumDescription();
                        costCenterName = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProjectName:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProjectName.GetEnumDescription();
                        projectName = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProjectManager:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProjectManager.GetEnumDescription();
                        projectManager = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProjectOperationName:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProjectOperationName.GetEnumDescription();
                        projectOperationName = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProjectOperationCode:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProjectOperationCode.GetEnumDescription();
                        projectOperationCode = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.MeasurementName:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.MeasurementName.GetEnumDescription();
                        measurementName = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.Workload:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.Workload.GetEnumDescription();
                        workload = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProjectOperationDetailNames:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProjectOperationDetailNames.GetEnumDescription();
                        projectOperationDetailNames = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProjectOperationDetailCodes:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProjectOperationDetailCodes.GetEnumDescription();
                        projectOperationDetailCodes = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ImportanceDescription:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ImportanceDescription.GetEnumDescription();
                        importanceDescription = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.StatusDescription:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.StatusDescription.GetEnumDescription();
                        statusDescription = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.TypeDescription:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.TypeDescription.GetEnumDescription();
                        typeDescription = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProductName:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProductName.GetEnumDescription();
                        productName = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProductCode:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProductCode.GetEnumDescription();
                        productCode = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProductBrand:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProductBrand.GetEnumDescription();
                        productBrand = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProductBrandModel:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProductBrandModel.GetEnumDescription();
                        productBrandModel = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProductGroupName:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProductGroupName.GetEnumDescription();
                        productGroupName = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProductGroupCode:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProductGroupCode.GetEnumDescription();
                        productGroupCode = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.RequestedCount:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.RequestedCount.GetEnumDescription();
                        requestedCount = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.SupplyCount:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.SupplyCount.GetEnumDescription();
                        supplyCount = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.RemainedCount:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.RemainedCount.GetEnumDescription();
                        remainedCount = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.DelivaryDeadLine:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.DelivaryDeadLine.GetEnumDescription();
                        delivaryDeadLine = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.RequestedDate:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.RequestedDate.GetEnumDescription();
                        requestedDate = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.Created:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.Created.GetEnumDescription();
                        created = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.UnitPrice:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.UnitPrice.GetEnumDescription();
                        unitPrice = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.TotalPrice:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.TotalPrice.GetEnumDescription();
                        totalPrice = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.TaxPercentage:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.TaxPercentage.GetEnumDescription();
                        taxPercentage = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.TaxNumber:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.TaxNumber.GetEnumDescription();
                        taxNumber = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.DiscountByPercentage:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.DiscountByPercentage.GetEnumDescription();
                        discountByPercentage = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.DiscountByNumber:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.DiscountByNumber.GetEnumDescription();
                        discountByNumber = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.DiscountedPrice:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.DiscountedPrice.GetEnumDescription();
                        discountedPrice = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.PackingPrice:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.PackingPrice.GetEnumDescription();
                        packingPrice = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.TransferPrice:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.TransferPrice.GetEnumDescription();
                        transferPrice = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.OtherPrice:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.OtherPrice.GetEnumDescription();
                        otherPrice = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.FinalPrice:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.FinalPrice.GetEnumDescription();
                        finalPrice = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ContractorFullName:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ContractorFullName.GetEnumDescription();
                        contractorFullName = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.SupplyerFullName:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.SupplyerFullName.GetEnumDescription();
                        supplyerFullName = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.CurrencyName:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.CurrencyName.GetEnumDescription();
                        currencyName = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.DestinationWarehouseName:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.DestinationWarehouseName.GetEnumDescription();
                        destinationWarehouseName = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.CustomerInvoiceNumber:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.CustomerInvoiceNumber.GetEnumDescription();
                        customerInvoiceNumber = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.Creator:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.Creator.GetEnumDescription();
                        creator = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.Description:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.Description.GetEnumDescription();
                        description = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ManagementDescription:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ManagementDescription.GetEnumDescription();
                        managementDescription = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.LastDescription:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.LastDescription.GetEnumDescription();
                        lastDescription = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.PackageName:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.PackageName.GetEnumDescription();
                        packageName = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.PackageQuantity:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.PackageQuantity.GetEnumDescription();
                        packageQuantity = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.PackageCount:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.PackageCount.GetEnumDescription();
                        packageCount = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.PackageUnitPrice:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.PackageUnitPrice.GetEnumDescription();
                        packageUnitPrice = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.OperatorAppointmentName:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.OperatorAppointmentName.GetEnumDescription();
                        operatorAppointmentName = counter1;
                        break;
                    case RequestGoodsSupplyProductExcelEnum.ProjectOperationDetailDescriptions:
                        worksheet1.Cell(currentRow1, counter1).Value = RequestGoodsSupplyProductExcelEnum.ProjectOperationDetailDescriptions.GetEnumDescription();
                        projectOperationDetailDescription = counter1;
                        break;
                }
            }

            foreach (var item in result)
            {
                currentRow1++;
                if (id > 0)
                    worksheet1.Cell(currentRow1, id).Value = item.Id;
                if (requestGoodsSupplyId > 0)
                    worksheet1.Cell(currentRow1, requestGoodsSupplyId).Value = item.RequestGoodsSupplyId;
                if (serialNumber > 0)
                    worksheet1.Cell(currentRow1, serialNumber).Value = item.SerialNumber;
                if (requestNumber > 0)
                    worksheet1.Cell(currentRow1, requestNumber).Value = item.RequestNumber;
                if (costCenterName > 0)
                    worksheet1.Cell(currentRow1, costCenterName).Value = item.CostCenterName;
                if (projectName > 0)
                    worksheet1.Cell(currentRow1, projectName).Value = item.ProjectName;
                if (projectManager > 0)
                    worksheet1.Cell(currentRow1, projectManager).Value = item.ProjectManager;
                if (projectOperationName > 0)
                    worksheet1.Cell(currentRow1, projectOperationName).Value = item.ProjectOperationName;
                if (projectOperationCode > 0)
                    worksheet1.Cell(currentRow1, projectOperationCode).Value = item.ProjectOperationCode;
                if (measurementName > 0)
                    worksheet1.Cell(currentRow1, measurementName).Value = item.MeasurementName;
                if (workload > 0)
                    worksheet1.Cell(currentRow1, workload).Value = item.Workload;
                if (projectOperationDetailNames > 0)
                    worksheet1.Cell(currentRow1, projectOperationDetailNames).Value = item.ProjectOperationDetailNames;
                if (projectOperationDetailCodes > 0)
                    worksheet1.Cell(currentRow1, projectOperationDetailCodes).Value = item.ProjectOperationDetailCodes;
                if (importanceDescription > 0)
                    worksheet1.Cell(currentRow1, importanceDescription).Value = item.ImportanceDescription;
                if (statusDescription > 0)
                    worksheet1.Cell(currentRow1, statusDescription).Value = item.StatusDescription;
                if (typeDescription > 0)
                    worksheet1.Cell(currentRow1, typeDescription).Value = item.TypeDescription;
                if (productName > 0)
                    worksheet1.Cell(currentRow1, productName).Value = item.ProductName;
                if (productCode > 0)
                    worksheet1.Cell(currentRow1, productCode).Value = item.ProductCode;
                if (productBrand > 0)
                    worksheet1.Cell(currentRow1, productBrand).Value = item.ProductBrand;
                if (productBrandModel > 0)
                    worksheet1.Cell(currentRow1, productBrandModel).Value = item.ProductBrandModel;
                if (productGroupName > 0)
                    worksheet1.Cell(currentRow1, productGroupName).Value = item.ProductGroupName;
                if (productGroupCode > 0)
                    worksheet1.Cell(currentRow1, productGroupCode).Value = item.ProductGroupCode;
                if (requestedCount > 0)
                    worksheet1.Cell(currentRow1, requestedCount).Value = item.RequestedCount;
                if (supplyCount > 0)
                    worksheet1.Cell(currentRow1, supplyCount).Value = item.SupplyCount;
                if (remainedCount > 0)
                    worksheet1.Cell(currentRow1, remainedCount).Value = item.RemainedCount;
                if (delivaryDeadLine > 0)
                    worksheet1.Cell(currentRow1, delivaryDeadLine).Value = item.DelivaryDeadLineShamsi;
                if (requestedDate > 0)
                    worksheet1.Cell(currentRow1, requestedDate).Value = item.RequestedDateShamsi;
                if (created > 0)
                    worksheet1.Cell(currentRow1, created).Value = item.CreatedShamsi;
                if (unitPrice > 0)
                    worksheet1.Cell(currentRow1, unitPrice).Value = item.UnitPrice;
                if (totalPrice > 0)
                    worksheet1.Cell(currentRow1, totalPrice).Value = item.TotalPrice;
                if (taxPercentage > 0)
                    worksheet1.Cell(currentRow1, taxPercentage).Value = item.TaxPercentage;
                if (taxNumber > 0)
                    worksheet1.Cell(currentRow1, taxNumber).Value = item.TaxNumber;
                if (discountByPercentage > 0)
                    worksheet1.Cell(currentRow1, discountByPercentage).Value = item.DiscountByPercentage;
                if (discountByNumber > 0)
                    worksheet1.Cell(currentRow1, discountByNumber).Value = item.DiscountByNumber;
                if (discountedPrice > 0)
                    worksheet1.Cell(currentRow1, discountedPrice).Value = item.DiscountedPrice;
                if (packingPrice > 0)
                    worksheet1.Cell(currentRow1, packingPrice).Value = item.PackingPrice;
                if (transferPrice > 0)
                    worksheet1.Cell(currentRow1, transferPrice).Value = item.TransferPrice;
                if (otherPrice > 0)
                    worksheet1.Cell(currentRow1, otherPrice).Value = item.OtherPrice;
                if (finalPrice > 0)
                    worksheet1.Cell(currentRow1, finalPrice).Value = item.FinalPrice;
                if (contractorFullName > 0)
                    worksheet1.Cell(currentRow1, contractorFullName).Value = item.ContractorFullName;
                if (supplyerFullName > 0)
                    worksheet1.Cell(currentRow1, supplyerFullName).Value = item.SupplyerFullName;
                if (currencyName > 0)
                    worksheet1.Cell(currentRow1, currencyName).Value = item.CurrencyName;
                if (destinationWarehouseName > 0)
                    worksheet1.Cell(currentRow1, destinationWarehouseName).Value = item.DestinationWarehouseName;
                if (customerInvoiceNumber > 0)
                    worksheet1.Cell(currentRow1, customerInvoiceNumber).Value = item.CustomerInvoiceNumber;
                if (creator > 0)
                    worksheet1.Cell(currentRow1, creator).Value = item.Creator;
                if (description > 0)
                    worksheet1.Cell(currentRow1, description).Value = item.Description;
                if (managementDescription > 0)
                    worksheet1.Cell(currentRow1, managementDescription).Value = item.ManagementDescription;
                if (lastDescription > 0)
                    worksheet1.Cell(currentRow1, lastDescription).Value = item.LastDescription;
                if (packageName > 0)
                    worksheet1.Cell(currentRow1, packageName).Value = item.PackageName;
                if (packageQuantity > 0)
                    worksheet1.Cell(currentRow1, packageQuantity).Value = item.PackageQuantity;
                if (packageCount > 0)
                    worksheet1.Cell(currentRow1, packageCount).Value = item.PackageCount;
                if (packageUnitPrice > 0)
                    worksheet1.Cell(currentRow1, packageUnitPrice).Value = item.PackageUnitPrice;
                if (operatorAppointmentName > 0)
                    worksheet1.Cell(currentRow1, operatorAppointmentName).Value = item.OperatorAppointmentName;
                if (projectOperationDetailDescription > 0)
                    worksheet1.Cell(currentRow1, projectOperationDetailDescription).Value = item.ProjectOperationDetailDescriptions;
            }
        }
        else
        {
            worksheet1.Cell(currentRow1, 1).Value = RequestGoodsSupplyProductExcelEnum.Id.GetEnumDescription();
            worksheet1.Cell(currentRow1, 2).Value = RequestGoodsSupplyProductExcelEnum.RequestGoodsSupplyId.GetEnumDescription();
            worksheet1.Cell(currentRow1, 3).Value = RequestGoodsSupplyProductExcelEnum.SerialNumber.GetEnumDescription();
            worksheet1.Cell(currentRow1, 4).Value = RequestGoodsSupplyProductExcelEnum.RequestNumber.GetEnumDescription();
            worksheet1.Cell(currentRow1, 5).Value = RequestGoodsSupplyProductExcelEnum.CostCenterName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 6).Value = RequestGoodsSupplyProductExcelEnum.ProjectName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 7).Value = RequestGoodsSupplyProductExcelEnum.ProjectManager.GetEnumDescription();
            worksheet1.Cell(currentRow1, 8).Value = RequestGoodsSupplyProductExcelEnum.ProjectOperationName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 9).Value = RequestGoodsSupplyProductExcelEnum.ProjectOperationCode.GetEnumDescription();
            worksheet1.Cell(currentRow1, 10).Value = RequestGoodsSupplyProductExcelEnum.MeasurementName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 11).Value = RequestGoodsSupplyProductExcelEnum.Workload.GetEnumDescription();
            worksheet1.Cell(currentRow1, 12).Value = RequestGoodsSupplyProductExcelEnum.ProjectOperationDetailNames.GetEnumDescription();
            worksheet1.Cell(currentRow1, 13).Value = RequestGoodsSupplyProductExcelEnum.ProjectOperationDetailCodes.GetEnumDescription();
            worksheet1.Cell(currentRow1, 14).Value = RequestGoodsSupplyProductExcelEnum.ImportanceDescription.GetEnumDescription();
            worksheet1.Cell(currentRow1, 15).Value = RequestGoodsSupplyProductExcelEnum.StatusDescription.GetEnumDescription();
            worksheet1.Cell(currentRow1, 16).Value = RequestGoodsSupplyProductExcelEnum.TypeDescription.GetEnumDescription();
            worksheet1.Cell(currentRow1, 17).Value = RequestGoodsSupplyProductExcelEnum.ProductName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 18).Value = RequestGoodsSupplyProductExcelEnum.ProductCode.GetEnumDescription();
            worksheet1.Cell(currentRow1, 19).Value = RequestGoodsSupplyProductExcelEnum.ProductBrand.GetEnumDescription();
            worksheet1.Cell(currentRow1, 20).Value = RequestGoodsSupplyProductExcelEnum.ProductBrandModel.GetEnumDescription();
            worksheet1.Cell(currentRow1, 21).Value = RequestGoodsSupplyProductExcelEnum.ProductGroupName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 22).Value = RequestGoodsSupplyProductExcelEnum.ProductGroupCode.GetEnumDescription();
            worksheet1.Cell(currentRow1, 23).Value = RequestGoodsSupplyProductExcelEnum.RequestedCount.GetEnumDescription();
            worksheet1.Cell(currentRow1, 24).Value = RequestGoodsSupplyProductExcelEnum.SupplyCount.GetEnumDescription();
            worksheet1.Cell(currentRow1, 25).Value = RequestGoodsSupplyProductExcelEnum.RemainedCount.GetEnumDescription();
            worksheet1.Cell(currentRow1, 26).Value = RequestGoodsSupplyProductExcelEnum.DelivaryDeadLine.GetEnumDescription();
            worksheet1.Cell(currentRow1, 27).Value = RequestGoodsSupplyProductExcelEnum.RequestedDate.GetEnumDescription();
            worksheet1.Cell(currentRow1, 28).Value = RequestGoodsSupplyProductExcelEnum.Created.GetEnumDescription();
            worksheet1.Cell(currentRow1, 29).Value = RequestGoodsSupplyProductExcelEnum.UnitPrice.GetEnumDescription();
            worksheet1.Cell(currentRow1, 30).Value = RequestGoodsSupplyProductExcelEnum.TotalPrice.GetEnumDescription();
            worksheet1.Cell(currentRow1, 31).Value = RequestGoodsSupplyProductExcelEnum.TaxPercentage.GetEnumDescription();
            worksheet1.Cell(currentRow1, 32).Value = RequestGoodsSupplyProductExcelEnum.TaxNumber.GetEnumDescription();
            worksheet1.Cell(currentRow1, 33).Value = RequestGoodsSupplyProductExcelEnum.DiscountByPercentage.GetEnumDescription();
            worksheet1.Cell(currentRow1, 34).Value = RequestGoodsSupplyProductExcelEnum.DiscountByNumber.GetEnumDescription();
            worksheet1.Cell(currentRow1, 35).Value = RequestGoodsSupplyProductExcelEnum.DiscountedPrice.GetEnumDescription();
            worksheet1.Cell(currentRow1, 36).Value = RequestGoodsSupplyProductExcelEnum.PackingPrice.GetEnumDescription();
            worksheet1.Cell(currentRow1, 37).Value = RequestGoodsSupplyProductExcelEnum.TransferPrice.GetEnumDescription();
            worksheet1.Cell(currentRow1, 38).Value = RequestGoodsSupplyProductExcelEnum.OtherPrice.GetEnumDescription();
            worksheet1.Cell(currentRow1, 39).Value = RequestGoodsSupplyProductExcelEnum.FinalPrice.GetEnumDescription();
            worksheet1.Cell(currentRow1, 40).Value = RequestGoodsSupplyProductExcelEnum.ContractorFullName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 41).Value = RequestGoodsSupplyProductExcelEnum.SupplyerFullName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 42).Value = RequestGoodsSupplyProductExcelEnum.CurrencyName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 43).Value = RequestGoodsSupplyProductExcelEnum.DestinationWarehouseName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 44).Value = RequestGoodsSupplyProductExcelEnum.CustomerInvoiceNumber.GetEnumDescription();
            worksheet1.Cell(currentRow1, 45).Value = RequestGoodsSupplyProductExcelEnum.Creator.GetEnumDescription();
            worksheet1.Cell(currentRow1, 46).Value = RequestGoodsSupplyProductExcelEnum.Description.GetEnumDescription();
            worksheet1.Cell(currentRow1, 47).Value = RequestGoodsSupplyProductExcelEnum.ManagementDescription.GetEnumDescription();
            worksheet1.Cell(currentRow1, 48).Value = RequestGoodsSupplyProductExcelEnum.LastDescription.GetEnumDescription();
            worksheet1.Cell(currentRow1, 49).Value = RequestGoodsSupplyProductExcelEnum.PackageName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 50).Value = RequestGoodsSupplyProductExcelEnum.PackageQuantity.GetEnumDescription();
            worksheet1.Cell(currentRow1, 51).Value = RequestGoodsSupplyProductExcelEnum.PackageCount.GetEnumDescription();
            worksheet1.Cell(currentRow1, 52).Value = RequestGoodsSupplyProductExcelEnum.PackageUnitPrice.GetEnumDescription();
            worksheet1.Cell(currentRow1, 53).Value = RequestGoodsSupplyProductExcelEnum.OperatorAppointmentName.GetEnumDescription();
            worksheet1.Cell(currentRow1, 54).Value = RequestGoodsSupplyProductExcelEnum.ProjectOperationDetailDescriptions.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow1++;
                worksheet1.Cell(currentRow1, 1).Value = item.Id;
                worksheet1.Cell(currentRow1, 2).Value = item.RequestGoodsSupplyId;
                worksheet1.Cell(currentRow1, 3).Value = item.SerialNumber;
                worksheet1.Cell(currentRow1, 4).Value = item.RequestNumber;
                worksheet1.Cell(currentRow1, 5).Value = item.CostCenterName;
                worksheet1.Cell(currentRow1, 6).Value = item.ProjectName;
                worksheet1.Cell(currentRow1, 7).Value = item.ProjectManager;
                worksheet1.Cell(currentRow1, 8).Value = item.ProjectOperationName;
                worksheet1.Cell(currentRow1, 9).Value = item.ProjectOperationCode;
                worksheet1.Cell(currentRow1, 10).Value = item.MeasurementName;
                worksheet1.Cell(currentRow1, 11).Value = item.Workload;
                worksheet1.Cell(currentRow1, 12).Value = item.ProjectOperationDetailNames;
                worksheet1.Cell(currentRow1, 13).Value = item.ProjectOperationDetailCodes;
                worksheet1.Cell(currentRow1, 14).Value = item.ImportanceDescription;
                worksheet1.Cell(currentRow1, 15).Value = item.StatusDescription;
                worksheet1.Cell(currentRow1, 16).Value = item.TypeDescription;
                worksheet1.Cell(currentRow1, 17).Value = item.ProductName;
                worksheet1.Cell(currentRow1, 18).Value = item.ProductCode;
                worksheet1.Cell(currentRow1, 19).Value = item.ProductBrand;
                worksheet1.Cell(currentRow1, 20).Value = item.ProductBrandModel;
                worksheet1.Cell(currentRow1, 21).Value = item.ProductGroupName;
                worksheet1.Cell(currentRow1, 22).Value = item.ProductGroupCode;
                worksheet1.Cell(currentRow1, 23).Value = item.RequestedCount;
                worksheet1.Cell(currentRow1, 24).Value = item.SupplyCount;
                worksheet1.Cell(currentRow1, 25).Value = item.RemainedCount;
                worksheet1.Cell(currentRow1, 26).Value = item.DelivaryDeadLineShamsi;
                worksheet1.Cell(currentRow1, 27).Value = item.RequestedDateShamsi;
                worksheet1.Cell(currentRow1, 28).Value = item.CreatedShamsi;
                worksheet1.Cell(currentRow1, 29).Value = item.UnitPrice;
                worksheet1.Cell(currentRow1, 30).Value = item.TotalPrice;
                worksheet1.Cell(currentRow1, 31).Value = item.TaxPercentage;
                worksheet1.Cell(currentRow1, 32).Value = item.TaxNumber;
                worksheet1.Cell(currentRow1, 33).Value = item.DiscountByPercentage;
                worksheet1.Cell(currentRow1, 34).Value = item.DiscountByNumber;
                worksheet1.Cell(currentRow1, 35).Value = item.DiscountedPrice;
                worksheet1.Cell(currentRow1, 36).Value = item.PackingPrice;
                worksheet1.Cell(currentRow1, 37).Value = item.TransferPrice;
                worksheet1.Cell(currentRow1, 38).Value = item.OtherPrice;
                worksheet1.Cell(currentRow1, 39).Value = item.FinalPrice;
                worksheet1.Cell(currentRow1, 40).Value = item.ContractorFullName;
                worksheet1.Cell(currentRow1, 41).Value = item.SupplyerFullName;
                worksheet1.Cell(currentRow1, 42).Value = item.CurrencyName;
                worksheet1.Cell(currentRow1, 43).Value = item.DestinationWarehouseName;
                worksheet1.Cell(currentRow1, 44).Value = item.CustomerInvoiceNumber;
                worksheet1.Cell(currentRow1, 45).Value = item.Creator;
                worksheet1.Cell(currentRow1, 46).Value = item.Description;
                worksheet1.Cell(currentRow1, 47).Value = item.ManagementDescription;
                worksheet1.Cell(currentRow1, 48).Value = item.LastDescription;
                worksheet1.Cell(currentRow1, 49).Value = item.PackageName;
                worksheet1.Cell(currentRow1, 50).Value = item.PackageQuantity;
                worksheet1.Cell(currentRow1, 51).Value = item.PackageCount;
                worksheet1.Cell(currentRow1, 52).Value = item.PackageUnitPrice;
                worksheet1.Cell(currentRow1, 53).Value = item.OperatorAppointmentName;
                worksheet1.Cell(currentRow1, 54).Value = item.ProjectOperationDetailDescriptions;
            }
        }

        if (managements is not null && managements.Count > 0)
        {
            worksheet2.Cell(currentRow2, 1).Value = "شماره درخواست";
            worksheet2.Cell(currentRow2, 2).Value = "شناسه کارشناس ارشد کالا درخواست";
            worksheet2.Cell(currentRow2, 3).Value = "نوع درخواست";
            worksheet2.Cell(currentRow2, 4).Value = "وضعیت";
            worksheet2.Cell(currentRow2, 5).Value = "شناسه فاکتور";
            worksheet2.Cell(currentRow2, 6).Value = "انبار";
            worksheet2.Cell(currentRow2, 7).Value = "انبار مقصد";
            worksheet2.Cell(currentRow2, 8).Value = "نام کالا";
            worksheet2.Cell(currentRow2, 9).Value = "کد کالا";
            worksheet2.Cell(currentRow2, 10).Value = "برند";
            worksheet2.Cell(currentRow2, 11).Value = "برند مدل";
            worksheet2.Cell(currentRow2, 12).Value = "تعداد درخواست";
            worksheet2.Cell(currentRow2, 13).Value = "تعداد تایید شده";
            worksheet2.Cell(currentRow2, 14).Value = "شناسه آلترناتیو";
            worksheet2.Cell(currentRow2, 15).Value = "متصدی";
            worksheet2.Cell(currentRow2, 16).Value = "توضیحات";
            worksheet2.Cell(currentRow2, 17).Value = "آخرین توضیحات";
            worksheet2.Cell(currentRow2, 18).Value = "تاریخ آخرین تغییر";

            foreach (var item in managements)
            {
                currentRow2++;
                worksheet2.Cell(currentRow2, 1).Value = item.RequestNumber;
                worksheet2.Cell(currentRow2, 2).Value = item.Id;
                worksheet2.Cell(currentRow2, 3).Value = item.TypeDescription;
                worksheet2.Cell(currentRow2, 4).Value = item.StatusDescription;
                worksheet2.Cell(currentRow2, 5).Value = item.InvoiceId;
                worksheet2.Cell(currentRow2, 6).Value = item.WarehouseName;
                worksheet2.Cell(currentRow2, 7).Value = item.DestinationWarehouseName;
                worksheet2.Cell(currentRow2, 8).Value = item.ProductName;
                worksheet2.Cell(currentRow2, 9).Value = item.ProductCode;
                worksheet2.Cell(currentRow2, 10).Value = item.Brand;
                worksheet2.Cell(currentRow2, 11).Value = item.BrandModel;
                worksheet2.Cell(currentRow2, 12).Value = item.RequestedCount;
                worksheet2.Cell(currentRow2, 13).Value = item.ConfirmedRequestCount;
                worksheet2.Cell(currentRow2, 14).Value = item.AlternateId;
                worksheet2.Cell(currentRow2, 15).Value = item.OperatorAppointmentName;
                worksheet2.Cell(currentRow2, 16).Value = item.Description;
                worksheet2.Cell(currentRow2, 17).Value = item.LastDescription;
                worksheet2.Cell(currentRow2, 18).Value = item.AssignmentDateShamsi;
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }
}