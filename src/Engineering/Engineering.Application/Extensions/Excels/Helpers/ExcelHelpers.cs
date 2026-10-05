using Engineering.Application.Services.Adjustments.Contracts;
using Engineering.Application.Services.OperationInfos.Models.RasteReshteExcelImporter;
using OfficeOpenXml.Style;
using System.Reflection;

namespace Engineering.Application.Extensions.Excels.Helpers
{
    public class EnumHelpers
    {
        public static int GetEnumCount<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum)).Length;
        }
    }

    public static class ExcelHelpers
    {
        public static List<TEnum> GetDefaultHeaders<TEnum>() where TEnum : struct, Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Where(e => typeof(TEnum)
                    .GetField(e.ToString())!
                    .GetCustomAttributes(typeof(DefaultHeaderAttribute), false)
                    .Any())
                .ToList();
        }

        public static IXLRange ApplyStyle(
       this IXLRange range,
       bool isBold,
       XLAlignmentHorizontalValues horizontalAlign,
       XLAlignmentVerticalValues verticalAlign,
       XLColor backgroundColor,
       XLBorderStyleValues borderStyle)
        {
            range.Style.Alignment.Horizontal = horizontalAlign;
            range.Style.Alignment.Vertical = verticalAlign;
            range.Style.Font.Bold = isBold;

            if (backgroundColor != null)
            {
                range.Style.Fill.BackgroundColor = backgroundColor;
            }

            if (borderStyle != XLBorderStyleValues.None)
            {
                range.Style.Border.OutsideBorder = borderStyle;
            }

            return range;
        }

        public static Dictionary<TEnum, int> SetupHeaders<TEnum>(
            ExcelWorksheet worksheet,
            List<TEnum>? filters,
            List<TEnum> defaultHeaders) where TEnum : struct, Enum
        {
            var headers = filters ?? defaultHeaders;
            var columns = new Dictionary<TEnum, int>();

            for (int i = 0; i < headers.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = headers[i].GetEnumDescription();
                columns[headers[i]] = i + 1;
            }

            ExcelStyles.SetHeaderStyle(
                worksheet.Cells[1, 1, 1, headers.Count],
                ExcelBorderStyle.Medium);

            return columns;
        }

        public static ExcelWorksheet CreateWorksheet(ExcelPackage workbook, string sheetName)
        {
            var worksheet = workbook.Workbook.Worksheets.Add(sheetName);
            worksheet.View.RightToLeft = true;
            return worksheet;
        }

        public static void FillRow<TEnum, TModel>(
            ExcelWorksheet worksheet,
            TModel item,
            int currentRow,
            Dictionary<TEnum, int> columns) where TEnum : Enum
        {
            foreach (var column in columns)
            {
                var enumValue = column.Key.ToString();
                var property = typeof(TModel).GetProperty(enumValue);

                if (property != null)
                {
                    var value = property.GetValue(item);
                    worksheet.Cells[currentRow, column.Value].Value = value;
                }
            }

            ExcelStyles.SetCellStyle(
                worksheet,
                currentRow - 1,
                currentRow,
                worksheet.Dimension.Start.Column,
                worksheet.Dimension.End.Column);
        }

        public static void ApplySummaryStyles(
            ExcelWorksheet worksheet,
            int activeCount,
            int totalCount,
            int startColumn)
        {
            ExcelStyles.SetSummaryCellStyle(
                worksheet.Cells[2, startColumn + 2, 4, startColumn + 3],
                worksheet,
                activeCount,
                totalCount,
                startColumn);
        }

        public static void ApplyOtherDataSummaryStyles(
            ExcelWorksheet worksheet,
            decimal volume,
            decimal price,
            decimal totalPrice,
            int startColumn)
        {
            ExcelStyles.SetOtherDataSummaryCellStyle(
                worksheet,
                volume,
                price,
                totalPrice,
                startColumn);
        }
        public static List<string> GetMissingHeaders<T>(Stream excelStream)
        {
            var expectedHeaders = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name)
                .ToList();

            using var package = new ExcelPackage(excelStream);
            var worksheet = package.Workbook.Worksheets[0];

            int colCount = worksheet.Dimension.End.Column;
            var actualHeaders = new List<string>();
            for (int col = 1; col <= colCount; col++)
            {
                var header = worksheet.Cells[1, col].Text.Trim();
                if (!string.IsNullOrEmpty(header))
                    actualHeaders.Add(header);
            }

            var missingHeaders = expectedHeaders
                .Where(expected => !actualHeaders.Any(actual =>
                    string.Equals(actual, expected, StringComparison.Ordinal)))
                .ToList();

            return missingHeaders;
        }

        public static Dictionary<string, List<string>> GetMissingHeaders(
            Stream excelStream,
            Dictionary<string, Type> sheetMappings)
        {
            using var package = new ExcelPackage(excelStream);

            var result = new Dictionary<string, List<string>>();

            foreach (var map in sheetMappings)
            {
                var sheetName = map.Key;
                var sheet = package.Workbook.Worksheets[sheetName];

                if (sheet == null)
                {
                    result[sheetName] = new List<string> { "Sheet not found" };
                    continue;
                }

                var expectedHeaders = map.Value
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(p => p.Name)
                    .ToList();

                int colCount = sheet.Dimension?.End.Column ?? 0;

                var actualHeaders = new List<string>();

                for (int col = 1; col <= colCount; col++)
                {
                    var header = sheet.Cells[1, col].Text.Trim();
                    if (!string.IsNullOrEmpty(header))
                        actualHeaders.Add(header);
                }

                var missingHeaders = expectedHeaders
                    .Where(expected => !actualHeaders.Any(actual =>
                        string.Equals(actual, expected, StringComparison.Ordinal)))
                    .ToList();

                if (missingHeaders.Any())
                {
                    result[sheetName] = missingHeaders;
                }
            }

            return result;
        }
        public static string GetColRef(int row, int col)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(row);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(col);

            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            col--;

            if (col < alphabet.Length)
            {
                return $"{alphabet[col]}{row}";
            }

            if (col >= Math.Pow(alphabet.Length, 2))
            {
                throw new ArgumentOutOfRangeException(nameof(col), "Column index out of supported range.");
            }

            return $"{alphabet[col / alphabet.Length - 1]}{alphabet[col % alphabet.Length]}{row}";
        }
        public static void SetCell(
        IXLWorksheet ws,
        int rowIndex,
        int rowIndexTo,
        int startCol,
        int endCol,
        string? value,
        bool isBold = true,
        XLAlignmentHorizontalValues horizontalAlign = XLAlignmentHorizontalValues.Center,
        XLAlignmentVerticalValues verticalAlign = XLAlignmentVerticalValues.Center,
        XLColor? bg = null,
        XLBorderStyleValues borderStyle = XLBorderStyleValues.Medium,
        bool wrapText = false)
        {
            var range = ws.Range(
                $"{ExcelHelpers.GetColRef(rowIndex, startCol)}:{ExcelHelpers.GetColRef(rowIndexTo, endCol)}");

            range.Merge();
            ws.Cell($"{ExcelHelpers.GetColRef(rowIndex, startCol)}").Value = value ?? string.Empty;
            range.ApplyStyle(
                isBold,
                horizontalAlign,
                verticalAlign,
                bg ?? XLColor.White,
                borderStyle
            );

            range.Style.Alignment.WrapText = wrapText;
        }
    }


    public static class RasteReshteExcelImporter
    {
        public static (
            List<RasteReshteCategoryExcelModel> Categories,
            List<RasteReshteBranchExcelModel> Branches,
            List<RasteReshteSeasonExcelModel> Seasons)
            Import(IFormFile documentFile)
        {
            using var stream = documentFile.OpenReadStream();
            using var package = new ExcelPackage(stream);

            var categorySheet = package.Workbook.Worksheets["Category"];
            var branchSheet = package.Workbook.Worksheets["Branch"];

            var seasonSheet =
                package.Workbook.Worksheets["Seasen"]
                ?? package.Workbook.Worksheets["Season"];

            if (categorySheet == null)
                throw new InvalidOperationException(
                    "Sheet 'Category' not found.");

            if (branchSheet == null)
                throw new InvalidOperationException(
                    "Sheet 'Branch' not found.");

            if (seasonSheet == null)
                throw new InvalidOperationException(
                    "Sheet 'Seasen' or 'Season' not found.");

            var categories = ReadCategories(categorySheet);
            var branches = ReadBranches(branchSheet);
            var seasons = ReadSeasons(seasonSheet);

            return (categories, branches, seasons);
        }

        private static List<RasteReshteCategoryExcelModel> ReadCategories(
            ExcelWorksheet worksheet)
        {
            var result = new List<RasteReshteCategoryExcelModel>();

            if (worksheet.Dimension == null)
                return result;

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var title = worksheet.Cells[row, 1].Text.Trim();
                var code = worksheet.Cells[row, 2].Text.Trim();

                if (string.IsNullOrWhiteSpace(title) &&
                    string.IsNullOrWhiteSpace(code))
                    continue;

                result.Add(new RasteReshteCategoryExcelModel
                {
                    Title = title,
                    Code = code
                });
            }

            return result;
        }

        private static List<RasteReshteBranchExcelModel> ReadBranches(
            ExcelWorksheet worksheet)
        {
            var result = new List<RasteReshteBranchExcelModel>();

            if (worksheet.Dimension == null)
                return result;

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var title = worksheet.Cells[row, 1].Text.Trim();
                var code = worksheet.Cells[row, 2].Text.Trim();
                var categoryCode = worksheet.Cells[row, 3].Text.Trim();

                if (string.IsNullOrWhiteSpace(title) &&
                    string.IsNullOrWhiteSpace(code) &&
                    string.IsNullOrWhiteSpace(categoryCode))
                    continue;

                result.Add(new RasteReshteBranchExcelModel
                {
                    Title = title,
                    Code = code,
                    CategoryCode = categoryCode
                });
            }

            return result;
        }

        private static List<RasteReshteSeasonExcelModel> ReadSeasons(
            ExcelWorksheet worksheet)
        {
            var result = new List<RasteReshteSeasonExcelModel>();

            if (worksheet.Dimension == null)
                return result;

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var title = worksheet.Cells[row, 1].Text.Trim();
                var code = worksheet.Cells[row, 2].Text.Trim();
                var branchCode = worksheet.Cells[row, 3].Text.Trim();
                var categoryCode = worksheet.Cells[row, 4].Text.Trim();

                if (string.IsNullOrWhiteSpace(title) &&
                    string.IsNullOrWhiteSpace(code) &&
                    string.IsNullOrWhiteSpace(branchCode) &&
                    string.IsNullOrWhiteSpace(categoryCode))
                    continue;

                result.Add(new RasteReshteSeasonExcelModel
                {
                    Title = title,
                    Code = code,
                    BranchCode = branchCode,
                    CategoryCode = categoryCode
                });
            }

            return result;
        }
    }


    public static class FileExtensions
    {
        public static IFormFile ToIFormFile(this string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            var fileInfo = new FileInfo(filePath);

            return new FormFile(
                baseStream: File.OpenRead(filePath),
                baseStreamOffset: 0,
                length: fileInfo.Length,
                name: "file",
                fileName: fileInfo.Name);
        }
    }


    public static class AdjustmentExcelImporter
    {
        public static (List<AdjustmentIndexExcelModel> Indexes, List<AdjustmentIndexValueExcelModel> Values) Import(IFormFile documentFile)
        {
            using var stream = documentFile.OpenReadStream();
            using var package = new ExcelPackage(stream);
            var indexesSheet = package.Workbook.Worksheets["Indexes"];
            var valuesSheet = package.Workbook.Worksheets["Values"];
            if (indexesSheet == null)
                throw new InvalidOperationException("Sheet 'Indexes' not found.");
            if (valuesSheet == null)
                throw new InvalidOperationException("Sheet 'Values' not found.");
            var indexes = ReadIndexes(indexesSheet);
            var values = ReadValues(valuesSheet);
            return (indexes, values);
        }

        private static List<AdjustmentIndexExcelModel> ReadIndexes(ExcelWorksheet worksheet)
        {
            var result = new List<AdjustmentIndexExcelModel>();
            if (worksheet.Dimension == null)
                return result;
            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var indexName = worksheet.Cells[row, 1].Text.Trim();

                var categoryCode = worksheet.Cells[row, 2].Text.Trim();

                var branchCode = worksheet.Cells[row, 3].Text.Trim();

                var seasonCode = worksheet.Cells[row, 4].Text.Trim();

                var isActiveText = worksheet.Cells[row, 5].Text.Trim();

                if (string.IsNullOrWhiteSpace(indexName))
                    continue;

                result.Add(new AdjustmentIndexExcelModel
                {
                    IndexName = indexName,
                    CategoryCode = categoryCode,
                    BranchCode = branchCode,
                    SeasonCode = string.IsNullOrWhiteSpace(seasonCode) || seasonCode == "0"
                            ? null : seasonCode,
                    IsActive = string.IsNullOrWhiteSpace(isActiveText) ||
                        isActiveText == "1" ||
                        isActiveText.Equals("true", StringComparison.OrdinalIgnoreCase)
                });
            }

            return result;
        }

        private static List<AdjustmentIndexValueExcelModel> ReadValues(ExcelWorksheet worksheet)
        {
            var result = new List<AdjustmentIndexValueExcelModel>();

            if (worksheet.Dimension == null)
                return result;

            for (var row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var indexName =
                    worksheet.Cells[row, 1].Text.Trim();

                if (string.IsNullOrWhiteSpace(indexName))
                    continue;

                result.Add(new AdjustmentIndexValueExcelModel
                {
                    IndexName = indexName,
                    YearName = worksheet.Cells[row, 2].Text.Trim(),
                    Period = int.Parse(worksheet.Cells[row, 3].Text.Trim()),
                    Type = worksheet.Cells[row, 4].Text.Trim(),

                    Value = decimal.Parse(worksheet.Cells[row, 5].Text.Trim())
                });
            }

            return result;
        }
    }
    //
}
