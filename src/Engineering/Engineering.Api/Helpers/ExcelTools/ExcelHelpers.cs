using ClosedXML.Excel;
using Gita.Backend.Shared.Domain.Extensions;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Engineering.Api.Helpers.ExcelTools;

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
    public static (ExcelWorksheet Sheet1, ExcelWorksheet Sheet2) CreateTwoWorksheets(ExcelPackage workbook, string sheetName1, string sheetName2)
    {
        var sheet1 = workbook.Workbook.Worksheets.Add(sheetName1);
        sheet1.View.RightToLeft = true;

        var sheet2 = workbook.Workbook.Worksheets.Add(sheetName2);
        sheet2.View.RightToLeft = true;

        return (sheet1, sheet2);
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

                if (property.PropertyType == typeof(bool?) || property.PropertyType == typeof(bool))
                {
                    if (value is bool boolValue)
                    {
                        worksheet.Cells[currentRow, column.Value].Value = boolValue ? "فعال" : "غیرفعال";
                    }
                    else if (value == null)
                    {
                        worksheet.Cells[currentRow, column.Value].Value = "غیرفعال";
                    }
                }
                else
                {
                    worksheet.Cells[currentRow, column.Value].Value = value;
                }
            }
        }

        ExcelStyles.SetCellStyle(
            worksheet,
            currentRow - 1,
            currentRow,
            worksheet.Dimension.Start.Column,
            worksheet.Dimension.End.Column);
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

    public class ErrorFileResult : IResult
    {
        private readonly byte[] _fileBytes;
        private readonly string _fileName;
        private readonly int _statusCode;

        public ErrorFileResult(
            byte[] fileBytes,
            string fileName,
            int statusCode = StatusCodes.Status422UnprocessableEntity)
        {
            _fileBytes = fileBytes;
            _fileName = fileName;
            _statusCode = statusCode;
        }

        public async Task ExecuteAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = _statusCode;
            httpContext.Response.ContentType =
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpContext.Response.Headers.ContentDisposition =
                $"attachment; filename=\"{_fileName}\"; filename*=UTF-8''{_fileName}";

            await httpContext.Response.Body.WriteAsync(_fileBytes);
        }
    }
}

