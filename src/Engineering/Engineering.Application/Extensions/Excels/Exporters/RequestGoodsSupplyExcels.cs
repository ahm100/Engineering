using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyDetailExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyDetailReportsExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyExcelExporter;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyManagementExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyReportsExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyReportsExcelExporter;
using OfficeOpenXml.Style;
using ExcelStyles = Engineering.Application.Extensions.Excels.Helpers.ExcelStyles;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class RequestGoodsSupplyExcels
{
    public static byte[] RequestGoodsSupplyToExcel(
        ICollection<GetsRequestGoodsSupplyExcelExporterResponseModel> result,
        ICollection<GetsRequestGoodsSupplyDetailExcelExporterResponseModel>? detailResult,
        ICollection<GetsRequestGoodsSupplyManagementExcelExporterResponseModel>? managementResult,
        List<RequestGoodsSupplyExcelEnum>? excelFilters,
        List<RequestGoodsSupplyDetailExcelEnum>? detailExcelFilters,
        List<RequestGoodsSupplyManagementExcelEnum>? managementExcelFilters)
    {
        using var workbook = new ExcelPackage();

        #region درخواست تامین کالا
        var worksheet1 = workbook.Workbook.Worksheets.Add("درخواست تامین کالا");
        var currentRow1 = 1;
        worksheet1.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<RequestGoodsSupplyExcelEnum, int>();
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
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.Row))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.Row]].Value = currentRow1 - 1;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.Id))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.StatusDescription))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.TypeDescription))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.TypeDescription]].Value = item.TypeDescription;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.MaxImportanceDescription))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.MaxImportanceDescription]].Value = item.MaxImportanceDescription;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.RequestNumber))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.RequestNumber]].Value = item.RequestNumber;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.MeasurementId))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.MeasurementId]].Value = item.MeasurementId;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.CostCenterName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.MeasurementName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.MeasurementName]].Value = item.MeasurementName;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.ProjectName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.OperationInfoName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.OperationInfoName]].Value = item.OperationInfoName;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.Workload))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.Workload]].Value = item.Workload;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.OperationLocationName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.OperationLocationName]].Value = item.FinalAmount;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.CreatedOn))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.CreatedOn]].Value = item.CreatedOn;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.CreatorId))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.CreatorId]].Value = item.CreatorId;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.Creator))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.Creator]].Value = item.Creator;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.PrivateName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.PrivateName]].Value = item.PrivateName;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.PrivateCode))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.PrivateCode]].Value = item.PrivateCode;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.PublicName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.PublicName]].Value = item.PublicName;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.PublicCode))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.PublicCode]].Value = item.PublicCode;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.ProjectOperationDetailDescription))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.ProjectOperationDetailDescription]].Value = item.ProjectOperationDetailDescription;
                if (columns.ContainsKey(RequestGoodsSupplyExcelEnum.RequestedDate))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyExcelEnum.RequestedDate]].Value = item.RequestedDate;

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
                worksheet1.Cells[1, 1, 1, 23],

                ExcelBorderStyle.Medium);

            worksheet1.Cells[currentRow1, 1].Value = RequestGoodsSupplyExcelEnum.Row.GetEnumDescription();
            worksheet1.Cells[currentRow1, 2].Value = RequestGoodsSupplyExcelEnum.Id.GetEnumDescription();
            worksheet1.Cells[currentRow1, 3].Value = RequestGoodsSupplyExcelEnum.StatusDescription.GetEnumDescription();
            worksheet1.Cells[currentRow1, 4].Value = RequestGoodsSupplyExcelEnum.TypeDescription.GetEnumDescription();
            worksheet1.Cells[currentRow1, 5].Value = RequestGoodsSupplyExcelEnum.MaxImportanceDescription.GetEnumDescription();
            worksheet1.Cells[currentRow1, 6].Value = RequestGoodsSupplyExcelEnum.RequestNumber.GetEnumDescription();
            worksheet1.Cells[currentRow1, 7].Value = RequestGoodsSupplyExcelEnum.MeasurementId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 8].Value = RequestGoodsSupplyExcelEnum.CostCenterName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 9].Value = RequestGoodsSupplyExcelEnum.MeasurementName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 10].Value = RequestGoodsSupplyExcelEnum.ProjectName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 11].Value = RequestGoodsSupplyExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 12].Value = RequestGoodsSupplyExcelEnum.Workload.GetEnumDescription();
            worksheet1.Cells[currentRow1, 14].Value = RequestGoodsSupplyExcelEnum.FinalAmount.GetEnumDescription();
            worksheet1.Cells[currentRow1, 15].Value = RequestGoodsSupplyExcelEnum.CreatedOn.GetEnumDescription();
            worksheet1.Cells[currentRow1, 16].Value = RequestGoodsSupplyExcelEnum.CreatorId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 17].Value = RequestGoodsSupplyExcelEnum.Creator.GetEnumDescription();
            worksheet1.Cells[currentRow1, 18].Value = RequestGoodsSupplyExcelEnum.PrivateName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 19].Value = RequestGoodsSupplyExcelEnum.PrivateCode.GetEnumDescription();
            worksheet1.Cells[currentRow1, 20].Value = RequestGoodsSupplyExcelEnum.PublicName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 21].Value = RequestGoodsSupplyExcelEnum.PublicCode.GetEnumDescription();
            worksheet1.Cells[currentRow1, 22].Value = RequestGoodsSupplyExcelEnum.ProjectOperationDetailDescription.GetEnumDescription();
            worksheet1.Cells[currentRow1, 23].Value = RequestGoodsSupplyExcelEnum.RequestedDate.GetEnumDescription();

            foreach (var item in result)
            {
                currentRow1++;
                worksheet1.Cells[currentRow1, 1].Value = currentRow1 - 1;
                worksheet1.Cells[currentRow1, 2].Value = item.Id;
                worksheet1.Cells[currentRow1, 3].Value = item.StatusDescription;
                worksheet1.Cells[currentRow1, 4].Value = item.TypeDescription;
                worksheet1.Cells[currentRow1, 5].Value = item.MaxImportanceDescription;
                worksheet1.Cells[currentRow1, 6].Value = item.StatusDescription;
                worksheet1.Cells[currentRow1, 7].Value = item.CostCenterName;
                worksheet1.Cells[currentRow1, 8].Value = item.ProjectName;
                worksheet1.Cells[currentRow1, 9].Value = item.OperationInfoName;
                worksheet1.Cells[currentRow1, 10].Value = item.MeasurementId;
                worksheet1.Cells[currentRow1, 11].Value = item.MeasurementName;
                worksheet1.Cells[currentRow1, 12].Value = item.Workload;
                worksheet1.Cells[currentRow1, 14].Value = item.FinalAmount;
                worksheet1.Cells[currentRow1, 15].Value = item.CreatedOn;
                worksheet1.Cells[currentRow1, 16].Value = item.CreatorId;
                worksheet1.Cells[currentRow1, 17].Value = item.Creator;
                worksheet1.Cells[currentRow1, 18].Value = item.PrivateName;
                worksheet1.Cells[currentRow1, 19].Value = item.PrivateCode;
                worksheet1.Cells[currentRow1, 20].Value = item.PublicName;
                worksheet1.Cells[currentRow1, 21].Value = item.PublicCode;
                worksheet1.Cells[currentRow1, 22].Value = item.ProjectOperationDetailDescription;
                worksheet1.Cells[currentRow1, 23].Value = item.RequestedDate;


                ExcelStyles.SetCellStyle(
                    worksheet1,
                    currentRow1 - 1,
                    currentRow1,
                    worksheet1.Dimension.Start.Column,
                    worksheet1.Dimension.End.Column);
            }
        }
        #endregion

        #region جزییات درخواست تامین کالا
        var worksheet2 = workbook.Workbook.Worksheets.Add("جزییات درخواست تامین کالا");
        var currentRow2 = 1;
        worksheet2.View.RightToLeft = true;

        if (detailResult is not null && detailResult.Count > 0)
        {
            if (detailExcelFilters != null && detailExcelFilters.Count > 0)
            {
                var columns = new Dictionary<RequestGoodsSupplyDetailExcelEnum, int>();
                for (int counter = 0; counter < detailExcelFilters.Count; counter++)
                {
                    worksheet2.Cells[1, counter + 1].Value = detailExcelFilters[counter].GetEnumDescription();
                    columns[detailExcelFilters[counter]] = counter + 1;

                    ExcelStyles.SetHeaderStyle(
                        worksheet2.Cells[1, 1, 1, counter + 1],

                        ExcelBorderStyle.Medium);
                }

                foreach (var item in detailResult)
                {
                    currentRow2++;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.Row))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.Row]].Value = currentRow2 - 1;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.Id))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.Id]].Value = item.Id;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.RequestGoodsSupplyId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.RequestGoodsSupplyId]].Value = item.RequestGoodsSupplyId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.ConsumableVolumeProductId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.ConsumableVolumeProductId]].Value = item.ConsumableVolumeProductId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.ProductNumber))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.ProductNumber]].Value = item.ProductNumber;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.GroupId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.GroupId]].Value = item.GroupId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.GroupName))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.GroupName]].Value = item.GroupName;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.GroupCode))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.GroupCode]].Value = item.GroupCode;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.GroupMeasure))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.GroupMeasure]].Value = item.GroupMeasure;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.ProductId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.ProductId]].Value = item.ProductId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.ProductName))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.ProductName]].Value = item.ProductName;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.ProductCode))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.ProductCode]].Value = item.ProductCode;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.Brand))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.Brand]].Value = item.Brand;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.BrandModel))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.BrandModel]].Value = item.BrandModel;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.PrivateName))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.PrivateName]].Value = item.PrivateName;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.PrivateCode))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.PrivateCode]].Value = item.PrivateCode;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.PublicName))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.PublicName]].Value = item.PublicName;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.PublicCode))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.PublicCode]].Value = item.PublicCode;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.TolerancePercentage))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.TolerancePercentage]].Value = item.TolerancePercentage;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.ToleranceCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.ToleranceCount]].Value = item.ToleranceCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.TotalEstimatedCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.TotalEstimatedCount]].Value = item.TotalEstimatedCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.TotalRequestedCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.TotalRequestedCount]].Value = item.TotalRequestedCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.TotalSupplyCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.TotalSupplyCount]].Value = item.TotalSupplyCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.TotalRemainedCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.TotalRemainedCount]].Value = item.TotalRemainedCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.RequestedCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.RequestedCount]].Value = item.RequestedCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.SupplyCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.SupplyCount]].Value = item.SupplyCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.RemainedCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.RemainedCount]].Value = item.RemainedCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.DelivaryDeadLine))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.DelivaryDeadLine]].Value = item.DelivaryDeadLine;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.TotalPrice))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.TotalPrice]].Value = item.TotalPrice;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.ImportanceDescription))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.ImportanceDescription]].Value = item.ImportanceDescription;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.StatusDescription))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.StatusDescription]].Value = item.StatusDescription;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.Creator))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.Creator]].Value = item.Creator;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.Description))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.Description]].Value = item.Description;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.ManagementDescription))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.ManagementDescription]].Value = item.ManagementDescription;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.DestinationWarehouseId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.DestinationWarehouseId]].Value = item.DestinationWarehouseId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.DestinationWarehouse))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.DestinationWarehouse]].Value = item.DestinationWarehouse;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.ContractorId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.ContractorId]].Value = item.ContractorId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.FullName))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.FullName]].Value = item.FullName;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.PackageId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.PackageId]].Value = item.PackageId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.PackageQuantity))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.PackageQuantity]].Value = item.PackageQuantity;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.PackageTitle))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.PackageTitle]].Value = item.PackageTitle;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.PackageCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.PackageCount]].Value = item.PackageCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.ProjectOperationDetailDescription))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.ProjectOperationDetailDescription]].Value = item.ProjectOperationDetailDescription;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.CustomerInvoiceNumber))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.CustomerInvoiceNumber]].Value = item.CustomerInvoiceNumber;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailExcelEnum.LastDescription))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailExcelEnum.LastDescription]].Value = item.LastDescription;

                    ExcelStyles.SetCellStyle(
                        worksheet2,
                        currentRow2 - 1,
                        currentRow2,
                        worksheet2.Dimension.Start.Column,
                        worksheet2.Dimension.End.Column);
                }
            }
            else
            {
                ExcelStyles.SetHeaderStyle(
                    worksheet2.Cells[1, 1, 1, 45],

                    ExcelBorderStyle.Medium);

                worksheet2.Cells[currentRow2, 1].Value = RequestGoodsSupplyDetailExcelEnum.Row.GetEnumDescription();
                worksheet2.Cells[currentRow2, 2].Value = RequestGoodsSupplyDetailExcelEnum.Id.GetEnumDescription();
                worksheet2.Cells[currentRow2, 3].Value = RequestGoodsSupplyDetailExcelEnum.RequestGoodsSupplyId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 4].Value = RequestGoodsSupplyDetailExcelEnum.ConsumableVolumeProductId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 5].Value = RequestGoodsSupplyDetailExcelEnum.ProductNumber.GetEnumDescription();
                worksheet2.Cells[currentRow2, 6].Value = RequestGoodsSupplyDetailExcelEnum.GroupId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 7].Value = RequestGoodsSupplyDetailExcelEnum.GroupName.GetEnumDescription();
                worksheet2.Cells[currentRow2, 8].Value = RequestGoodsSupplyDetailExcelEnum.GroupCode.GetEnumDescription();
                worksheet2.Cells[currentRow2, 9].Value = RequestGoodsSupplyDetailExcelEnum.GroupMeasure.GetEnumDescription();
                worksheet2.Cells[currentRow2, 10].Value = RequestGoodsSupplyDetailExcelEnum.ProductId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 11].Value = RequestGoodsSupplyDetailExcelEnum.ProductName.GetEnumDescription();
                worksheet2.Cells[currentRow2, 12].Value = RequestGoodsSupplyDetailExcelEnum.ProductCode.GetEnumDescription();
                worksheet2.Cells[currentRow2, 13].Value = RequestGoodsSupplyDetailExcelEnum.Brand.GetEnumDescription();
                worksheet2.Cells[currentRow2, 14].Value = RequestGoodsSupplyDetailExcelEnum.BrandModel.GetEnumDescription();
                worksheet2.Cells[currentRow2, 15].Value = RequestGoodsSupplyDetailExcelEnum.PrivateName.GetEnumDescription();
                worksheet2.Cells[currentRow2, 16].Value = RequestGoodsSupplyDetailExcelEnum.PrivateCode.GetEnumDescription();
                worksheet2.Cells[currentRow2, 17].Value = RequestGoodsSupplyDetailExcelEnum.PublicName.GetEnumDescription();
                worksheet2.Cells[currentRow2, 18].Value = RequestGoodsSupplyDetailExcelEnum.PublicCode.GetEnumDescription();
                worksheet2.Cells[currentRow2, 19].Value = RequestGoodsSupplyDetailExcelEnum.TolerancePercentage.GetEnumDescription();
                worksheet2.Cells[currentRow2, 20].Value = RequestGoodsSupplyDetailExcelEnum.ToleranceCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 21].Value = RequestGoodsSupplyDetailExcelEnum.TotalEstimatedCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 22].Value = RequestGoodsSupplyDetailExcelEnum.TotalRequestedCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 23].Value = RequestGoodsSupplyDetailExcelEnum.TotalSupplyCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 24].Value = RequestGoodsSupplyDetailExcelEnum.TotalRemainedCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 25].Value = RequestGoodsSupplyDetailExcelEnum.RequestedCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 26].Value = RequestGoodsSupplyDetailExcelEnum.SupplyCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 27].Value = RequestGoodsSupplyDetailExcelEnum.RemainedCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 28].Value = RequestGoodsSupplyDetailExcelEnum.DelivaryDeadLine.GetEnumDescription();
                worksheet2.Cells[currentRow2, 29].Value = RequestGoodsSupplyDetailExcelEnum.TotalPrice.GetEnumDescription();
                worksheet2.Cells[currentRow2, 30].Value = RequestGoodsSupplyDetailExcelEnum.ImportanceDescription.GetEnumDescription();
                worksheet2.Cells[currentRow2, 31].Value = RequestGoodsSupplyDetailExcelEnum.StatusDescription.GetEnumDescription();
                worksheet2.Cells[currentRow2, 32].Value = RequestGoodsSupplyDetailExcelEnum.Creator.GetEnumDescription();
                worksheet2.Cells[currentRow2, 33].Value = RequestGoodsSupplyDetailExcelEnum.Description.GetEnumDescription();
                worksheet2.Cells[currentRow2, 34].Value = RequestGoodsSupplyDetailExcelEnum.ManagementDescription.GetEnumDescription();
                worksheet2.Cells[currentRow2, 35].Value = RequestGoodsSupplyDetailExcelEnum.DestinationWarehouseId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 36].Value = RequestGoodsSupplyDetailExcelEnum.DestinationWarehouse.GetEnumDescription();
                worksheet2.Cells[currentRow2, 37].Value = RequestGoodsSupplyDetailExcelEnum.ContractorId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 38].Value = RequestGoodsSupplyDetailExcelEnum.FullName.GetEnumDescription();
                worksheet2.Cells[currentRow2, 39].Value = RequestGoodsSupplyDetailExcelEnum.PackageId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 40].Value = RequestGoodsSupplyDetailExcelEnum.PackageQuantity.GetEnumDescription();
                worksheet2.Cells[currentRow2, 41].Value = RequestGoodsSupplyDetailExcelEnum.PackageTitle.GetEnumDescription();
                worksheet2.Cells[currentRow2, 42].Value = RequestGoodsSupplyDetailExcelEnum.PackageCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 43].Value = RequestGoodsSupplyDetailExcelEnum.ProjectOperationDetailDescription.GetEnumDescription();
                worksheet2.Cells[currentRow2, 44].Value = RequestGoodsSupplyDetailExcelEnum.CustomerInvoiceNumber.GetEnumDescription();
                worksheet2.Cells[currentRow2, 45].Value = RequestGoodsSupplyDetailExcelEnum.LastDescription.GetEnumDescription();

                foreach (var item in detailResult)
                {
                    currentRow2++;
                    worksheet2.Cells[currentRow2, 1].Value = currentRow2 - 1;
                    worksheet2.Cells[currentRow2, 2].Value = item.Id;
                    worksheet2.Cells[currentRow2, 3].Value = item.RequestGoodsSupplyId;
                    worksheet2.Cells[currentRow2, 4].Value = item.ConsumableVolumeProductId;
                    worksheet2.Cells[currentRow2, 5].Value = item.ProductNumber;
                    worksheet2.Cells[currentRow2, 6].Value = item.GroupId;
                    worksheet2.Cells[currentRow2, 7].Value = item.GroupName;
                    worksheet2.Cells[currentRow2, 8].Value = item.GroupCode;
                    worksheet2.Cells[currentRow2, 9].Value = item.GroupMeasure;
                    worksheet2.Cells[currentRow2, 10].Value = item.ProductId;
                    worksheet2.Cells[currentRow2, 11].Value = item.ProductName;
                    worksheet2.Cells[currentRow2, 12].Value = item.ProductCode;
                    worksheet2.Cells[currentRow2, 13].Value = item.Brand;
                    worksheet2.Cells[currentRow2, 14].Value = item.BrandModel;
                    worksheet2.Cells[currentRow2, 15].Value = item.PrivateName;
                    worksheet2.Cells[currentRow2, 16].Value = item.PrivateCode;
                    worksheet2.Cells[currentRow2, 17].Value = item.PublicName;
                    worksheet2.Cells[currentRow2, 18].Value = item.PublicCode;
                    worksheet2.Cells[currentRow2, 19].Value = item.TolerancePercentage;
                    worksheet2.Cells[currentRow2, 20].Value = item.ToleranceCount;
                    worksheet2.Cells[currentRow2, 21].Value = item.TotalEstimatedCount;
                    worksheet2.Cells[currentRow2, 22].Value = item.TotalRequestedCount;
                    worksheet2.Cells[currentRow2, 23].Value = item.TotalSupplyCount;
                    worksheet2.Cells[currentRow2, 24].Value = item.TotalRemainedCount;
                    worksheet2.Cells[currentRow2, 25].Value = item.RequestedCount;
                    worksheet2.Cells[currentRow2, 26].Value = item.SupplyCount;
                    worksheet2.Cells[currentRow2, 27].Value = item.RemainedCount;
                    worksheet2.Cells[currentRow2, 28].Value = item.DelivaryDeadLine;
                    worksheet2.Cells[currentRow2, 29].Value = item.TotalPrice;
                    worksheet2.Cells[currentRow2, 30].Value = item.ImportanceDescription;
                    worksheet2.Cells[currentRow2, 31].Value = item.StatusDescription;
                    worksheet2.Cells[currentRow2, 32].Value = item.Creator;
                    worksheet2.Cells[currentRow2, 33].Value = item.Description;
                    worksheet2.Cells[currentRow2, 34].Value = item.ManagementDescription;
                    worksheet2.Cells[currentRow2, 35].Value = item.DestinationWarehouseId;
                    worksheet2.Cells[currentRow2, 36].Value = item.DestinationWarehouse;
                    worksheet2.Cells[currentRow2, 37].Value = item.ContractorId;
                    worksheet2.Cells[currentRow2, 38].Value = item.FullName;
                    worksheet2.Cells[currentRow2, 39].Value = item.PackageId;
                    worksheet2.Cells[currentRow2, 40].Value = item.PackageQuantity;
                    worksheet2.Cells[currentRow2, 41].Value = item.PackageTitle;
                    worksheet2.Cells[currentRow2, 42].Value = item.PackageCount;
                    worksheet2.Cells[currentRow2, 43].Value = item.ProjectOperationDetailDescription;
                    worksheet2.Cells[currentRow2, 44].Value = item.CustomerInvoiceNumber;
                    worksheet2.Cells[currentRow2, 45].Value = item.LastDescription;

                    ExcelStyles.SetCellStyle(
                        worksheet2,
                        currentRow2 - 1,
                        currentRow2,
                        worksheet2.Dimension.Start.Column,
                        worksheet2.Dimension.End.Column);
                }
            }
        }
        #endregion

        #region کارشناس ارشد درخواست تامین کالا
        var worksheet3 = workbook.Workbook.Worksheets.Add("کارشناس ارشد درخواست تامین کالا");
        var currentRow3 = 1;
        worksheet3.View.RightToLeft = true;

        if (managementResult is not null && managementResult.Count > 0)
        {
            if (managementExcelFilters != null && managementExcelFilters.Count > 0)
            {
                var columns = new Dictionary<RequestGoodsSupplyManagementExcelEnum, int>();
                for (int counter = 0; counter < managementExcelFilters.Count; counter++)
                {
                    worksheet3.Cells[1, counter + 1].Value = managementExcelFilters[counter].GetEnumDescription();
                    columns[managementExcelFilters[counter]] = counter + 1;

                    ExcelStyles.SetHeaderStyle(
                        worksheet3.Cells[1, 1, 1, counter + 1],

                        ExcelBorderStyle.Medium);
                }

                foreach (var item in managementResult)
                {
                    currentRow3++;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.Row))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.Row]].Value = currentRow3 - 1;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.Id))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.Id]].Value = item.Id;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.RequestGoodsSupplyDetailId))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.RequestGoodsSupplyDetailId]].Value = item.RequestGoodsSupplyDetailId;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.ProductId))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.ProductId]].Value = item.ProductId;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.WarehouseId))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.WarehouseId]].Value = item.WarehouseId;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.Warehouse))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.Warehouse]].Value = item.Warehouse;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.DestinationWarehouseId))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.DestinationWarehouseId]].Value = item.DestinationWarehouseId;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.DestinationWarehouse))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.DestinationWarehouse]].Value = item.DestinationWarehouse;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.InvoiceId))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.InvoiceId]].Value = item.InvoiceId;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.RequestedCount))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.RequestedCount]].Value = item.RequestedCount;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.ConfirmedRequestCount))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.ConfirmedRequestCount]].Value = item.ConfirmedRequestCount;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.AlternateId))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.AlternateId]].Value = item.AlternateId;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.Description))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.Description]].Value = item.Description;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.TypeDescription))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.TypeDescription]].Value = item.TypeDescription;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.StatusDescription))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.StatusDescription]].Value = item.StatusDescription;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.AssignmentDate))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.AssignmentDate]].Value = item.AssignmentDate;
                    if (columns.ContainsKey(RequestGoodsSupplyManagementExcelEnum.LastDescription))
                        worksheet3.Cells[currentRow3, columns[RequestGoodsSupplyManagementExcelEnum.LastDescription]].Value = item.LastDescription;

                    ExcelStyles.SetCellStyle(
                        worksheet3,
                        currentRow3 - 1,
                        currentRow3,
                        worksheet3.Dimension.Start.Column,
                        worksheet3.Dimension.End.Column);
                }
            }
            else
            {
                ExcelStyles.SetHeaderStyle(
                    worksheet3.Cells[1, 1, 1, 17],

                    ExcelBorderStyle.Medium);

                worksheet3.Cells[currentRow3, 1].Value = RequestGoodsSupplyManagementExcelEnum.Row.GetEnumDescription();
                worksheet3.Cells[currentRow3, 2].Value = RequestGoodsSupplyManagementExcelEnum.Id.GetEnumDescription();
                worksheet3.Cells[currentRow3, 3].Value = RequestGoodsSupplyManagementExcelEnum.RequestGoodsSupplyDetailId.GetEnumDescription();
                worksheet3.Cells[currentRow3, 4].Value = RequestGoodsSupplyManagementExcelEnum.ProductId.GetEnumDescription();
                worksheet3.Cells[currentRow3, 5].Value = RequestGoodsSupplyManagementExcelEnum.WarehouseId.GetEnumDescription();
                worksheet3.Cells[currentRow3, 6].Value = RequestGoodsSupplyManagementExcelEnum.Warehouse.GetEnumDescription();
                worksheet3.Cells[currentRow3, 7].Value = RequestGoodsSupplyManagementExcelEnum.DestinationWarehouseId.GetEnumDescription();
                worksheet3.Cells[currentRow3, 8].Value = RequestGoodsSupplyManagementExcelEnum.DestinationWarehouse.GetEnumDescription();
                worksheet3.Cells[currentRow3, 9].Value = RequestGoodsSupplyManagementExcelEnum.InvoiceId.GetEnumDescription();
                worksheet3.Cells[currentRow3, 10].Value = RequestGoodsSupplyManagementExcelEnum.RequestedCount.GetEnumDescription();
                worksheet3.Cells[currentRow3, 11].Value = RequestGoodsSupplyManagementExcelEnum.ConfirmedRequestCount.GetEnumDescription();
                worksheet3.Cells[currentRow3, 12].Value = RequestGoodsSupplyManagementExcelEnum.AlternateId.GetEnumDescription();
                worksheet3.Cells[currentRow3, 13].Value = RequestGoodsSupplyManagementExcelEnum.Description.GetEnumDescription();
                worksheet3.Cells[currentRow3, 14].Value = RequestGoodsSupplyManagementExcelEnum.TypeDescription.GetEnumDescription();
                worksheet3.Cells[currentRow3, 15].Value = RequestGoodsSupplyManagementExcelEnum.StatusDescription.GetEnumDescription();
                worksheet3.Cells[currentRow3, 16].Value = RequestGoodsSupplyManagementExcelEnum.AssignmentDate.GetEnumDescription();
                worksheet3.Cells[currentRow3, 17].Value = RequestGoodsSupplyManagementExcelEnum.LastDescription.GetEnumDescription();

                foreach (var item in managementResult)
                {
                    currentRow3++;
                    worksheet3.Cells[currentRow3, 1].Value = currentRow3 - 1;
                    worksheet3.Cells[currentRow3, 2].Value = item.Id;
                    worksheet3.Cells[currentRow3, 3].Value = item.RequestGoodsSupplyDetailId;
                    worksheet3.Cells[currentRow3, 4].Value = item.ProductId;
                    worksheet3.Cells[currentRow3, 5].Value = item.WarehouseId;
                    worksheet3.Cells[currentRow3, 6].Value = item.Warehouse;
                    worksheet3.Cells[currentRow3, 7].Value = item.DestinationWarehouseId;
                    worksheet3.Cells[currentRow3, 8].Value = item.DestinationWarehouse;
                    worksheet3.Cells[currentRow3, 9].Value = item.InvoiceId;
                    worksheet3.Cells[currentRow3, 10].Value = item.RequestedCount;
                    worksheet3.Cells[currentRow3, 11].Value = item.ConfirmedRequestCount;
                    worksheet3.Cells[currentRow3, 12].Value = item.AlternateId;
                    worksheet3.Cells[currentRow3, 13].Value = item.Description;
                    worksheet3.Cells[currentRow3, 14].Value = item.TypeDescription;
                    worksheet3.Cells[currentRow3, 15].Value = item.StatusDescription;
                    worksheet3.Cells[currentRow3, 16].Value = item.AssignmentDate;
                    worksheet3.Cells[currentRow3, 17].Value = item.LastDescription;

                    ExcelStyles.SetCellStyle(
                        worksheet3,
                        currentRow3 - 1,
                        currentRow3,
                        worksheet3.Dimension.Start.Column,
                        worksheet3.Dimension.End.Column);
                }
            }
        }
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static byte[] RequestGoodsSupplyReportsToExcel(
        ICollection<GetsRequestGoodsSupplyReportsExcelExporterResponseModel> requestGoodsSupplyReports,
        ICollection<GetsRequestGoodsSupplyDetailReportsExcelExporterModel>? requestGoodsSupplyDetailReports,
        List<RequestGoodsSupplyReportsExcelEnum>? excelFilters,
        List<RequestGoodsSupplyDetailReportsExcelEnum>? detailExcelFilters)
    {

        using var workbook = new ExcelPackage();

        #region گزارش درخواست تامین کالا
        var worksheet1 = workbook.Workbook.Worksheets.Add("گزارش درخواست تامین کالا");
        var currentRow1 = 1;
        worksheet1.View.RightToLeft = true;

        if (excelFilters != null && excelFilters.Count > 0)
        {
            var columns = new Dictionary<RequestGoodsSupplyReportsExcelEnum, int>();
            for (int counter = 0; counter < excelFilters.Count; counter++)
            {
                worksheet1.Cells[1, counter + 1].Value = excelFilters[counter].GetEnumDescription();
                columns[excelFilters[counter]] = counter + 1;

                ExcelStyles.SetHeaderStyle(
                    worksheet1.Cells[1, 1, 1, counter + 1],

                    ExcelBorderStyle.Medium);
            }

            foreach (var item in requestGoodsSupplyReports)
            {
                currentRow1++;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.Row))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.Row]].Value = currentRow1 - 1;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.Id))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.Id]].Value = item.Id;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.RequestNumber))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.RequestNumber]].Value = item.RequestNumber;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.CostCenterId))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.CostCenterId]].Value = item.CostCenterId;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.CostCenterName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.CostCenterName]].Value = item.CostCenterName;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.ProjectId))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.ProjectId]].Value = item.ProjectId;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.ProjectName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.ProjectName]].Value = item.ProjectName;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.OperationInfoName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.OperationInfoName]].Value = item.OperationInfoName;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.OperationInfoCode))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.OperationInfoCode]].Value = item.OperationInfoCode;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.MeasurementName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.MeasurementName]].Value = item.MeasurementName;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.Workload))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.Workload]].Value = item.Workload;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.PrivateName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.PrivateName]].Value = item.PrivateName;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.PrivateCode))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.PrivateCode]].Value = item.PrivateCode;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.PublicName))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.PublicName]].Value = item.PublicName;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.PublicCode))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.PublicCode]].Value = item.PublicCode;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.FinalAmount))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.FinalAmount]].Value = item.FinalAmount;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.TypeDescription))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.TypeDescription]].Value = item.TypeDescription;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.StatusDescription))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.StatusDescription]].Value = item.StatusDescription;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.RejectedNumber))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.RejectedNumber]].Value = item.RejectedNumber;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.AllInStock))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.AllInStock]].Value = item.AllInStock;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.InStockNumber))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.InStockNumber]].Value = item.InStockNumber;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.AllBetweenStock))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.AllBetweenStock]].Value = item.AllBetweenStock;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.BetweenStockNumber))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.BetweenStockNumber]].Value = item.BetweenStockNumber;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.AllCommerce))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.AllCommerce]].Value = item.AllCommerce;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.CommerceNumber))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.CommerceNumber]].Value = item.CommerceNumber;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.CreatedOn))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.CreatedOn]].Value = item.CreatedOn;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.CreatorId))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.CreatorId]].Value = item.CreatorId;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.Creator))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.Creator]].Value = item.Creator;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.MaxImportanceDescription))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.MaxImportanceDescription]].Value = item.MaxImportanceDescription;
                if (columns.ContainsKey(RequestGoodsSupplyReportsExcelEnum.ProjectOperationDetailDescription))
                    worksheet1.Cells[currentRow1, columns[RequestGoodsSupplyReportsExcelEnum.ProjectOperationDetailDescription]].Value = item.ProjectOperationDetailDescription;

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
                worksheet1.Cells[1, 1, 1, 7],

                ExcelBorderStyle.Medium);

            worksheet1.Cells[currentRow1, 1].Value = RequestGoodsSupplyReportsExcelEnum.Row.GetEnumDescription();
            worksheet1.Cells[currentRow1, 2].Value = RequestGoodsSupplyReportsExcelEnum.Id.GetEnumDescription();
            worksheet1.Cells[currentRow1, 3].Value = RequestGoodsSupplyReportsExcelEnum.RequestNumber.GetEnumDescription();
            worksheet1.Cells[currentRow1, 4].Value = RequestGoodsSupplyReportsExcelEnum.CostCenterId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 5].Value = RequestGoodsSupplyReportsExcelEnum.CostCenterName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 6].Value = RequestGoodsSupplyReportsExcelEnum.ProjectId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 7].Value = RequestGoodsSupplyReportsExcelEnum.ProjectName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 8].Value = RequestGoodsSupplyReportsExcelEnum.OperationInfoName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 9].Value = RequestGoodsSupplyReportsExcelEnum.OperationInfoCode.GetEnumDescription();
            worksheet1.Cells[currentRow1, 10].Value = RequestGoodsSupplyReportsExcelEnum.MeasurementName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 11].Value = RequestGoodsSupplyReportsExcelEnum.Workload.GetEnumDescription();
            worksheet1.Cells[currentRow1, 12].Value = RequestGoodsSupplyReportsExcelEnum.PrivateName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 13].Value = RequestGoodsSupplyReportsExcelEnum.PrivateCode.GetEnumDescription();
            worksheet1.Cells[currentRow1, 14].Value = RequestGoodsSupplyReportsExcelEnum.PublicName.GetEnumDescription();
            worksheet1.Cells[currentRow1, 15].Value = RequestGoodsSupplyReportsExcelEnum.PublicCode.GetEnumDescription();
            worksheet1.Cells[currentRow1, 16].Value = RequestGoodsSupplyReportsExcelEnum.FinalAmount.GetEnumDescription();
            worksheet1.Cells[currentRow1, 17].Value = RequestGoodsSupplyReportsExcelEnum.TypeDescription.GetEnumDescription();
            worksheet1.Cells[currentRow1, 18].Value = RequestGoodsSupplyReportsExcelEnum.StatusDescription.GetEnumDescription();
            worksheet1.Cells[currentRow1, 19].Value = RequestGoodsSupplyReportsExcelEnum.RejectedNumber.GetEnumDescription();
            worksheet1.Cells[currentRow1, 20].Value = RequestGoodsSupplyReportsExcelEnum.AllInStock.GetEnumDescription();
            worksheet1.Cells[currentRow1, 21].Value = RequestGoodsSupplyReportsExcelEnum.InStockNumber.GetEnumDescription();
            worksheet1.Cells[currentRow1, 22].Value = RequestGoodsSupplyReportsExcelEnum.AllBetweenStock.GetEnumDescription();
            worksheet1.Cells[currentRow1, 23].Value = RequestGoodsSupplyReportsExcelEnum.BetweenStockNumber.GetEnumDescription();
            worksheet1.Cells[currentRow1, 24].Value = RequestGoodsSupplyReportsExcelEnum.AllCommerce.GetEnumDescription();
            worksheet1.Cells[currentRow1, 25].Value = RequestGoodsSupplyReportsExcelEnum.CommerceNumber.GetEnumDescription();
            worksheet1.Cells[currentRow1, 26].Value = RequestGoodsSupplyReportsExcelEnum.CreatedOn.GetEnumDescription();
            worksheet1.Cells[currentRow1, 27].Value = RequestGoodsSupplyReportsExcelEnum.CreatorId.GetEnumDescription();
            worksheet1.Cells[currentRow1, 28].Value = RequestGoodsSupplyReportsExcelEnum.Creator.GetEnumDescription();
            worksheet1.Cells[currentRow1, 29].Value = RequestGoodsSupplyReportsExcelEnum.MaxImportanceDescription.GetEnumDescription();
            worksheet1.Cells[currentRow1, 30].Value = RequestGoodsSupplyReportsExcelEnum.ProjectOperationDetailDescription.GetEnumDescription();


            foreach (var item in requestGoodsSupplyReports)
            {
                currentRow1++;
                worksheet1.Cells[currentRow1, 1].Value = currentRow1 - 1;
                worksheet1.Cells[currentRow1, 2].Value = item.Id;
                worksheet1.Cells[currentRow1, 3].Value = item.RequestNumber;
                worksheet1.Cells[currentRow1, 4].Value = item.CostCenterId;
                worksheet1.Cells[currentRow1, 5].Value = item.CostCenterName;
                worksheet1.Cells[currentRow1, 6].Value = item.ProjectId;
                worksheet1.Cells[currentRow1, 7].Value = item.ProjectName;
                worksheet1.Cells[currentRow1, 8].Value = item.OperationInfoName;
                worksheet1.Cells[currentRow1, 9].Value = item.OperationInfoCode;
                worksheet1.Cells[currentRow1, 10].Value = item.MeasurementName;
                worksheet1.Cells[currentRow1, 11].Value = item.Workload;
                worksheet1.Cells[currentRow1, 12].Value = item.PrivateName;
                worksheet1.Cells[currentRow1, 13].Value = item.PrivateCode;
                worksheet1.Cells[currentRow1, 14].Value = item.PublicName;
                worksheet1.Cells[currentRow1, 15].Value = item.PublicCode;
                worksheet1.Cells[currentRow1, 16].Value = item.FinalAmount;
                worksheet1.Cells[currentRow1, 17].Value = item.TypeDescription;
                worksheet1.Cells[currentRow1, 18].Value = item.StatusDescription;
                worksheet1.Cells[currentRow1, 19].Value = item.RejectedNumber;
                worksheet1.Cells[currentRow1, 20].Value = item.AllInStock;
                worksheet1.Cells[currentRow1, 21].Value = item.InStockNumber;
                worksheet1.Cells[currentRow1, 22].Value = item.AllBetweenStock;
                worksheet1.Cells[currentRow1, 23].Value = item.BetweenStockNumber;
                worksheet1.Cells[currentRow1, 24].Value = item.AllCommerce;
                worksheet1.Cells[currentRow1, 25].Value = item.CommerceNumber;
                worksheet1.Cells[currentRow1, 26].Value = item.CreatedOn;
                worksheet1.Cells[currentRow1, 27].Value = item.CreatorId;
                worksheet1.Cells[currentRow1, 28].Value = item.Creator;
                worksheet1.Cells[currentRow1, 29].Value = item.MaxImportanceDescription;
                worksheet1.Cells[currentRow1, 30].Value = item.ProjectOperationDetailDescription;

                ExcelStyles.SetCellStyle(
                    worksheet1,
                    currentRow1 - 1,
                    currentRow1,
                    worksheet1.Dimension.Start.Column,
                    worksheet1.Dimension.End.Column);
            }
        }
        #endregion

        #region جزئیات درخواست ها
        if (requestGoodsSupplyDetailReports is not null && requestGoodsSupplyDetailReports.Count > 0)
        {
            var worksheet2 = workbook.Workbook.Worksheets.Add("جزئیات درخواست ها");
            var currentRow2 = 1;
            var result = requestGoodsSupplyDetailReports.OrderByDescending(x => x.RequestGoodsSupplyId).ToList();
            worksheet2.View.RightToLeft = true;


            if (detailExcelFilters != null && detailExcelFilters.Count > 0)
            {
                var columns = new Dictionary<RequestGoodsSupplyDetailReportsExcelEnum, int>();

                for (int counter = 0; counter < detailExcelFilters.Count; counter++)
                {
                    worksheet2.Cells[1, counter + 1].Value = detailExcelFilters[counter].GetEnumDescription();
                    columns[detailExcelFilters[counter]] = counter + 1;

                    ExcelStyles.SetHeaderStyle(
                        worksheet2.Cells[1, 1, 1, counter + 1],

                        ExcelBorderStyle.Medium);
                }

                foreach (var item in result)
                {
                    currentRow2++;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.Row))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.Row]].Value = currentRow2 - 1;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.Id))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.Id]].Value = item.Id;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.RequestGoodsSupplyId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.RequestGoodsSupplyId]].Value = item.RequestGoodsSupplyId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.ConsumableVolumeProductId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.ConsumableVolumeProductId]].Value = item.ConsumableVolumeProductId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.ProductNumber))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.ProductNumber]].Value = item.ProductNumber;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.GroupId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.GroupId]].Value = item.GroupId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.GroupName))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.GroupName]].Value = item.GroupName;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.GroupCode))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.GroupCode]].Value = item.GroupCode;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.GroupMeasure))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.GroupMeasure]].Value = item.GroupMeasure;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.ProductId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.ProductId]].Value = item.ProductId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.ProductName))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.ProductName]].Value = item.ProductName;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.ProductCode))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.ProductCode]].Value = item.ProductCode;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.Brand))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.Brand]].Value = item.Brand;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.BrandModel))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.BrandModel]].Value = item.BrandModel;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.PrivateName))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.PrivateName]].Value = item.PrivateName;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.PrivateCode))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.PrivateCode]].Value = item.PrivateCode;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.PublicName))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.PublicName]].Value = item.PublicName;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.PublicCode))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.PublicCode]].Value = item.PublicCode;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.TolerancePercentage))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.TolerancePercentage]].Value = item.TolerancePercentage;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.ToleranceCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.ToleranceCount]].Value = item.ToleranceCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.TotalEstimatedCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.TotalEstimatedCount]].Value = item.TotalEstimatedCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.TotalRequestedCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.TotalRequestedCount]].Value = item.TotalRequestedCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.TotalSupplyCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.TotalSupplyCount]].Value = item.TotalSupplyCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.TotalRemainedCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.TotalRemainedCount]].Value = item.TotalRemainedCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.RequestedCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.RequestedCount]].Value = item.RequestedCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.SupplyCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.SupplyCount]].Value = item.SupplyCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.RemainedCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.RemainedCount]].Value = item.RemainedCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.DelivaryDeadLine))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.DelivaryDeadLine]].Value = item.DelivaryDeadLine;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.CurrencyId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.CurrencyId]].Value = item.CurrencyId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.Currency))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.Currency]].Value = item.Currency;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.TotalPrice))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.TotalPrice]].Value = item.TotalPrice;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.ImportanceDescription))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.ImportanceDescription]].Value = item.ImportanceDescription;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.StatusDescription))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.StatusDescription]].Value = item.StatusDescription;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.ManagementTypeDescription))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.ManagementTypeDescription]].Value = item.ManagementTypeDescription;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.Creator))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.Creator]].Value = item.Creator;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.Description))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.Description]].Value = item.Description;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.ManagementDescription))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.ManagementDescription]].Value = item.ManagementDescription;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.CheckGroup))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.CheckGroup]].Value = item.CheckGroup;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.DestinationWarehouseId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.DestinationWarehouseId]].Value = item.DestinationWarehouseId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.ContractorId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.ContractorId]].Value = item.ContractorId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.FullName))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.FullName]].Value = item.FullName;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.PackageId))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.PackageId]].Value = item.PackageId;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.PackageQuantity))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.PackageQuantity]].Value = item.PackageQuantity;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.PackageTitle))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.PackageTitle]].Value = item.PackageTitle;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.PackagePackageCount))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.PackagePackageCount]].Value = item.PackagePackageCount;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.ProjectOperationDetailDescription))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.ProjectOperationDetailDescription]].Value = item.ProjectOperationDetailDescription;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.CustomerInvoiceNumber))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.CustomerInvoiceNumber]].Value = item.CustomerInvoiceNumber;
                    if (columns.ContainsKey(RequestGoodsSupplyDetailReportsExcelEnum.LastDescription))
                        worksheet2.Cells[currentRow2, columns[RequestGoodsSupplyDetailReportsExcelEnum.LastDescription]].Value = item.LastDescription;

                    ExcelStyles.SetCellStyle(
                        worksheet2,
                        currentRow2 - 1,
                        currentRow2,
                        worksheet2.Dimension.Start.Column,
                        worksheet2.Dimension.End.Column);
                }
            }
            else
            {

                ExcelStyles.SetHeaderStyle(
                    worksheet2.Cells[1, 1, 1, 48],

                    ExcelBorderStyle.Medium);

                worksheet2.Cells[currentRow2, 1].Value = RequestGoodsSupplyDetailReportsExcelEnum.Row.GetEnumDescription();
                worksheet2.Cells[currentRow2, 2].Value = RequestGoodsSupplyDetailReportsExcelEnum.Id.GetEnumDescription();
                worksheet2.Cells[currentRow2, 3].Value = RequestGoodsSupplyDetailReportsExcelEnum.RequestGoodsSupplyId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 4].Value = RequestGoodsSupplyDetailReportsExcelEnum.ConsumableVolumeProductId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 5].Value = RequestGoodsSupplyDetailReportsExcelEnum.ProductNumber.GetEnumDescription();
                worksheet2.Cells[currentRow2, 6].Value = RequestGoodsSupplyDetailReportsExcelEnum.GroupId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 7].Value = RequestGoodsSupplyDetailReportsExcelEnum.GroupName.GetEnumDescription();
                worksheet2.Cells[currentRow2, 8].Value = RequestGoodsSupplyDetailReportsExcelEnum.GroupCode.GetEnumDescription();
                worksheet2.Cells[currentRow2, 9].Value = RequestGoodsSupplyDetailReportsExcelEnum.GroupMeasure.GetEnumDescription();
                worksheet2.Cells[currentRow2, 10].Value = RequestGoodsSupplyDetailReportsExcelEnum.ProductId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 11].Value = RequestGoodsSupplyDetailReportsExcelEnum.ProductName.GetEnumDescription();
                worksheet2.Cells[currentRow2, 12].Value = RequestGoodsSupplyDetailReportsExcelEnum.ProductCode.GetEnumDescription();
                worksheet2.Cells[currentRow2, 13].Value = RequestGoodsSupplyDetailReportsExcelEnum.Brand.GetEnumDescription();
                worksheet2.Cells[currentRow2, 14].Value = RequestGoodsSupplyDetailReportsExcelEnum.BrandModel.GetEnumDescription();
                worksheet2.Cells[currentRow2, 15].Value = RequestGoodsSupplyDetailReportsExcelEnum.PrivateName.GetEnumDescription();
                worksheet2.Cells[currentRow2, 16].Value = RequestGoodsSupplyDetailReportsExcelEnum.PrivateCode.GetEnumDescription();
                worksheet2.Cells[currentRow2, 17].Value = RequestGoodsSupplyDetailReportsExcelEnum.PublicName.GetEnumDescription();
                worksheet2.Cells[currentRow2, 18].Value = RequestGoodsSupplyDetailReportsExcelEnum.PublicCode.GetEnumDescription();
                worksheet2.Cells[currentRow2, 19].Value = RequestGoodsSupplyDetailReportsExcelEnum.TolerancePercentage.GetEnumDescription();
                worksheet2.Cells[currentRow2, 20].Value = RequestGoodsSupplyDetailReportsExcelEnum.ToleranceCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 21].Value = RequestGoodsSupplyDetailReportsExcelEnum.TotalEstimatedCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 22].Value = RequestGoodsSupplyDetailReportsExcelEnum.TotalRequestedCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 23].Value = RequestGoodsSupplyDetailReportsExcelEnum.TotalSupplyCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 24].Value = RequestGoodsSupplyDetailReportsExcelEnum.TotalRemainedCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 25].Value = RequestGoodsSupplyDetailReportsExcelEnum.RequestedCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 26].Value = RequestGoodsSupplyDetailReportsExcelEnum.SupplyCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 27].Value = RequestGoodsSupplyDetailReportsExcelEnum.RemainedCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 28].Value = RequestGoodsSupplyDetailReportsExcelEnum.DelivaryDeadLine.GetEnumDescription();
                worksheet2.Cells[currentRow2, 29].Value = RequestGoodsSupplyDetailReportsExcelEnum.CurrencyId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 30].Value = RequestGoodsSupplyDetailReportsExcelEnum.Currency.GetEnumDescription();
                worksheet2.Cells[currentRow2, 31].Value = RequestGoodsSupplyDetailReportsExcelEnum.TotalPrice.GetEnumDescription();
                worksheet2.Cells[currentRow2, 32].Value = RequestGoodsSupplyDetailReportsExcelEnum.ImportanceDescription.GetEnumDescription();
                worksheet2.Cells[currentRow2, 33].Value = RequestGoodsSupplyDetailReportsExcelEnum.StatusDescription.GetEnumDescription();
                worksheet2.Cells[currentRow2, 34].Value = RequestGoodsSupplyDetailReportsExcelEnum.ManagementTypeDescription.GetEnumDescription();
                worksheet2.Cells[currentRow2, 35].Value = RequestGoodsSupplyDetailReportsExcelEnum.Creator.GetEnumDescription();
                worksheet2.Cells[currentRow2, 36].Value = RequestGoodsSupplyDetailReportsExcelEnum.Description.GetEnumDescription();
                worksheet2.Cells[currentRow2, 37].Value = RequestGoodsSupplyDetailReportsExcelEnum.ManagementDescription.GetEnumDescription();
                worksheet2.Cells[currentRow2, 38].Value = RequestGoodsSupplyDetailReportsExcelEnum.CheckGroup.GetEnumDescription();
                worksheet2.Cells[currentRow2, 39].Value = RequestGoodsSupplyDetailReportsExcelEnum.DestinationWarehouseId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 40].Value = RequestGoodsSupplyDetailReportsExcelEnum.ContractorId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 41].Value = RequestGoodsSupplyDetailReportsExcelEnum.FullName.GetEnumDescription();
                worksheet2.Cells[currentRow2, 42].Value = RequestGoodsSupplyDetailReportsExcelEnum.PackageId.GetEnumDescription();
                worksheet2.Cells[currentRow2, 43].Value = RequestGoodsSupplyDetailReportsExcelEnum.PackageQuantity.GetEnumDescription();
                worksheet2.Cells[currentRow2, 44].Value = RequestGoodsSupplyDetailReportsExcelEnum.PackageTitle.GetEnumDescription();
                worksheet2.Cells[currentRow2, 45].Value = RequestGoodsSupplyDetailReportsExcelEnum.PackagePackageCount.GetEnumDescription();
                worksheet2.Cells[currentRow2, 46].Value = RequestGoodsSupplyDetailReportsExcelEnum.ProjectOperationDetailDescription.GetEnumDescription();
                worksheet2.Cells[currentRow2, 47].Value = RequestGoodsSupplyDetailReportsExcelEnum.CustomerInvoiceNumber.GetEnumDescription();
                worksheet2.Cells[currentRow2, 48].Value = RequestGoodsSupplyDetailReportsExcelEnum.LastDescription.GetEnumDescription();

                foreach (var item in result)
                {
                    currentRow2++;
                    worksheet2.Cells[currentRow2, 1].Value = currentRow2 - 1;
                    worksheet2.Cells[currentRow2, 2].Value = item.Id;
                    worksheet2.Cells[currentRow2, 3].Value = item.RequestGoodsSupplyId;
                    worksheet2.Cells[currentRow2, 4].Value = item.ConsumableVolumeProductId;
                    worksheet2.Cells[currentRow2, 5].Value = item.ProductNumber;
                    worksheet2.Cells[currentRow2, 6].Value = item.GroupId;
                    worksheet2.Cells[currentRow2, 7].Value = item.GroupName;
                    worksheet2.Cells[currentRow2, 8].Value = item.GroupCode;
                    worksheet2.Cells[currentRow2, 9].Value = item.GroupMeasure;
                    worksheet2.Cells[currentRow2, 10].Value = item.ProductId;
                    worksheet2.Cells[currentRow2, 11].Value = item.ProductName;
                    worksheet2.Cells[currentRow2, 12].Value = item.ProductCode;
                    worksheet2.Cells[currentRow2, 13].Value = item.Brand;
                    worksheet2.Cells[currentRow2, 14].Value = item.BrandModel;
                    worksheet2.Cells[currentRow2, 15].Value = item.PrivateName;
                    worksheet2.Cells[currentRow2, 16].Value = item.PrivateCode;
                    worksheet2.Cells[currentRow2, 17].Value = item.PublicName;
                    worksheet2.Cells[currentRow2, 18].Value = item.PublicCode;
                    worksheet2.Cells[currentRow2, 19].Value = item.TolerancePercentage;
                    worksheet2.Cells[currentRow2, 20].Value = item.ToleranceCount;
                    worksheet2.Cells[currentRow2, 21].Value = item.TotalEstimatedCount;
                    worksheet2.Cells[currentRow2, 22].Value = item.TotalRequestedCount;
                    worksheet2.Cells[currentRow2, 23].Value = item.TotalSupplyCount;
                    worksheet2.Cells[currentRow2, 24].Value = item.TotalRemainedCount;
                    worksheet2.Cells[currentRow2, 25].Value = item.RequestedCount;
                    worksheet2.Cells[currentRow2, 26].Value = item.SupplyCount;
                    worksheet2.Cells[currentRow2, 27].Value = item.RemainedCount;
                    worksheet2.Cells[currentRow2, 28].Value = item.DelivaryDeadLine;
                    worksheet2.Cells[currentRow2, 29].Value = item.CurrencyId;
                    worksheet2.Cells[currentRow2, 30].Value = item.Currency;
                    worksheet2.Cells[currentRow2, 31].Value = item.TotalPrice;
                    worksheet2.Cells[currentRow2, 32].Value = item.ImportanceDescription;
                    worksheet2.Cells[currentRow2, 33].Value = item.StatusDescription;
                    worksheet2.Cells[currentRow2, 34].Value = item.ManagementTypeDescription;
                    worksheet2.Cells[currentRow2, 35].Value = item.Creator;
                    worksheet2.Cells[currentRow2, 36].Value = item.Description;
                    worksheet2.Cells[currentRow2, 37].Value = item.ManagementDescription;
                    worksheet2.Cells[currentRow2, 38].Value = item.CheckGroup.Value;
                    worksheet2.Cells[currentRow2, 39].Value = item.DestinationWarehouseId;
                    worksheet2.Cells[currentRow2, 40].Value = item.ContractorId;
                    worksheet2.Cells[currentRow2, 41].Value = item.FullName;
                    worksheet2.Cells[currentRow2, 42].Value = item.PackageId;
                    worksheet2.Cells[currentRow2, 43].Value = item.PackageQuantity;
                    worksheet2.Cells[currentRow2, 44].Value = item.PackageTitle;
                    worksheet2.Cells[currentRow2, 45].Value = item.PackagePackageCount;
                    worksheet2.Cells[currentRow2, 46].Value = item.ProjectOperationDetailDescription;
                    worksheet2.Cells[currentRow2, 47].Value = item.CustomerInvoiceNumber;
                    worksheet2.Cells[currentRow2, 48].Value = item.LastDescription;

                    ExcelStyles.SetCellStyle(
                        worksheet2,
                        currentRow2 - 1,
                        currentRow2,
                        worksheet2.Dimension.Start.Column,
                        worksheet2.Dimension.End.Column);
                }
            }
        }
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

}