using Engineering.Application.Services.ShippingCosts.Contracts.GetsFilteredShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsShippingCostExcelEnum;
using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcel;
using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcelHelper;

namespace Engineering.Application.Extensions.Excels.Exporters;

public class ShippingCostExcels
{
    public static byte[] ShippingCostToExcel(
        ICollection<GetsFilteredShippingCostResponseModel> result,
        List<ShippingCostExcelEnum>? excelFilters)
    {
        using var workbook = new ExcelPackage();
        var worksheet = ExcelHelpers.CreateWorksheet(workbook, "هزینه های ارسال");

        var dataExcel = result!;

        List<ShippingCostExcelEnum>? newFilters = [];
        if (excelFilters is null || excelFilters.Count <= 0)
        {
            newFilters = Enum.GetValues(typeof(ShippingCostExcelEnum))
                .Cast<ShippingCostExcelEnum>()
                .Select(x => x)
                .ToList();
        }
        else
        {
            newFilters = excelFilters;
        }

        #region ShippingCost
        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<ShippingCostExcelEnum>();
        var columns = ExcelHelpers.SetupHeaders<ShippingCostExcelEnum>(
            worksheet,
            newFilters,
            defaultHeaders);

        var currentRow = 1;
        foreach (var item in dataExcel)
        {
            currentRow++;
            ExcelHelpers.FillRow<ShippingCostExcelEnum, GetsFilteredShippingCostResponseModel>(
                worksheet,
                item,
                currentRow,
                columns);
        }
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static byte[] ShippingCostImportExcel()
    {
        using var workbook = new ExcelPackage();
        var shippingcostWorksheet = ExcelHelpers.CreateWorksheet(workbook, "نمونه ورودی اکسل");

        List<ShippingCostImportExcel>? shippingcostFilters = [];
        shippingcostFilters = Enum.GetValues(typeof(ShippingCostImportExcel))
            .Cast<ShippingCostImportExcel>()
            .Select(x => x)
            .ToList();

        #region ShippingCost
        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<ShippingCostImportExcel>();
        var columns = ExcelHelpers.SetupHeaders<ShippingCostImportExcel>(
            shippingcostWorksheet,
            shippingcostFilters,
            defaultHeaders);
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public static byte[] ShippingCostHelpImportExcel(
        ICollection<ViewCityDataModel>? viewCities,
        ICollection<ViewRegionDataModel>? viewRegions,
        ICollection<MachineTypeDataModel>? machineTypes)
    {
        using var workbook = new ExcelPackage();
        var cityWorksheet = ExcelHelpers.CreateWorksheet(workbook, "راهنمای شهر ها");

        var shippingcostWorksheet = ExcelHelpers.CreateWorksheet(workbook, "نمونه ورودی اکسل");

        List<ShippingCostImportExcelHelper>? shippingcostFilters = [];
        shippingcostFilters = Enum.GetValues(typeof(ShippingCostImportExcelHelper))
            .Cast<ShippingCostImportExcelHelper>()
            .Select(x => x)
            .ToList();

        #region ShippingCost
        var defaultHeaders = ExcelHelpers.GetDefaultHeaders<ShippingCostImportExcelHelper>();
        var columns = ExcelHelpers.SetupHeaders<ShippingCostImportExcelHelper>(
            shippingcostWorksheet,
            shippingcostFilters,
            defaultHeaders);
        #endregion


        List<CityImportExcelModel>? cityFilters = [];
        cityFilters = Enum.GetValues(typeof(CityImportExcelModel))
            .Cast<CityImportExcelModel>()
            .Select(x => x)
            .ToList();

        #region city
        var defaultCityHeaders = ExcelHelpers.GetDefaultHeaders<CityImportExcelModel>();
        var cityColumns = ExcelHelpers.SetupHeaders<CityImportExcelModel>(
            cityWorksheet,
            cityFilters,
            defaultCityHeaders);

        var currentRowCity = 1;
        if (viewCities != null && viewCities.Count > 0)
        {
            foreach (var item in viewCities)
            {
                currentRowCity++;
                ExcelHelpers.FillRow<CityImportExcelModel, ViewCityDataModel>(
                    cityWorksheet,
                    item,
                    currentRowCity,
                    cityColumns);
            }
        }
        #endregion


        var regionWorksheet = ExcelHelpers.CreateWorksheet(workbook, "راهنمای ناحیه ها");

        List<RegionsImportExcelModel>? regionFilters = [];
        regionFilters = Enum.GetValues(typeof(RegionsImportExcelModel))
            .Cast<RegionsImportExcelModel>()
            .Select(x => x)
            .ToList();

        #region region
        var defaultRegionHeaders = ExcelHelpers.GetDefaultHeaders<RegionsImportExcelModel>();
        var regionColumns = ExcelHelpers.SetupHeaders<RegionsImportExcelModel>(
            regionWorksheet,
            regionFilters,
            defaultRegionHeaders);

        var currentRowRegion = 1;
        if (viewRegions != null && viewRegions.Count > 0)
        {
            foreach (var item in viewRegions)
            {
                currentRowRegion++;
                ExcelHelpers.FillRow<RegionsImportExcelModel, ViewRegionDataModel>(
                    regionWorksheet,
                    item,
                    currentRowRegion,
                    regionColumns);
            }
        }
        #endregion


        var machinWorksheet = ExcelHelpers.CreateWorksheet(workbook, "راهنمای ماشین ها");

        List<MachineTypeImportExcelModel>? machinFilters = [];
        machinFilters = Enum.GetValues(typeof(MachineTypeImportExcelModel))
            .Cast<MachineTypeImportExcelModel>()
            .Select(x => x)
            .ToList();

        #region machine
        var defaultMachinHeaders = ExcelHelpers.GetDefaultHeaders<MachineTypeImportExcelModel>();
        var machinColumns = ExcelHelpers.SetupHeaders<MachineTypeImportExcelModel>(
            machinWorksheet,
            machinFilters,
            defaultMachinHeaders);

        var currentRowMachine = 1;
        if (machineTypes != null && machineTypes.Count > 0)
        {
            foreach (var item in machineTypes)
            {
                currentRowMachine++;
                ExcelHelpers.FillRow<MachineTypeImportExcelModel, MachineTypeDataModel>(
                    machinWorksheet,
                    item,
                    currentRowMachine,
                    machinColumns);
            }
        }
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}