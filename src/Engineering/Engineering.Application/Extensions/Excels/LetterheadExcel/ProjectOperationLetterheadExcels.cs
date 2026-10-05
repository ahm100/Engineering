using Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationEmployerReporting;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System.Drawing;

namespace Engineering.Application.Extensions.Excels.LetterheadExcel;

public static class ProjectOperationLetterheadExcels
{
    public static byte[] GetsProjectOperationEmployerReportingToExcel(
        DateTime? startDate,
        DateTime? endDate,
        List<GetsProjectOperationEmployerReportingModel> projectOperations)
    {
        using var workbook = new ExcelPackage();

        #region شیت ‌ها روکش صورت وضعیت
        var projectOperationSheet = workbook.Workbook.Worksheets.Add("روکش صورت وضعیت");
        ConfigureWorksheetStyle(projectOperationSheet);

        AddHeaderCells(projectOperationSheet, startDate, endDate);
        ProjectOperationAddColumnHeaders(projectOperationSheet);
        AddRows(projectOperationSheet, projectOperations);
        CreateTable(projectOperationSheet, projectOperations.Count, 23, "ProjectOperationsTable");

        AdjustColumnWidths(projectOperationSheet);
        #endregion

        #region شیت کارکرد روزانه‌ها
        var dailySheet = workbook.Workbook.Worksheets.Add("ریزمتره");
        ConfigureWorksheetStyle(dailySheet);

        var projectOperationDailies = projectOperations
            .SelectMany(x => x.DailyProjectOperations ?? [])
            .ToList();
        AddHeader(dailySheet, startDate, endDate);
        DailyAddColumnHeaders(dailySheet);
        AddRows(dailySheet, projectOperationDailies);
        CreateTable(dailySheet, projectOperationDailies.Count, 33, "DailyTable");

        AdjustColumnWidths(dailySheet);
        #endregion

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    private static void AddRows(ExcelWorksheet sheet, List<GetsProjectOperationEmployerReportingModel> projectOperations)
    {
        for (int i = 0; i < projectOperations.Count; i++)
        {
            var row = i + 5;
            var item = projectOperations[i];
            sheet.Cells[row, 1].Value = i + 1;
            sheet.Cells[row, 2].Value = item.OperationInfoName;
            sheet.Cells[row, 3].Value = item.OperationInfoMeasurementName;
            sheet.Cells[row, 4].Value = 0;
            sheet.Cells[row, 5].Value = 0;
            var formula = "=SUMIF(DailyTable[شرح عملیات]؛[@[شرح عملیات]]؛DailyTable[مقدارپیمانکار])";
            ApplyNumberFormat(sheet, row, 6, formula);
            sheet.Cells[row, 7].Value = item.Workload;
            sheet.Cells[row, 8].Value = item.Workload;
            sheet.Cells[row, 9].Value = item.Workload;
            for (int j = 10; j <= 23; j++)
                sheet.Cells[row, j].Value = 0;

            SetCellStyle(sheet, i, row, 1, 23);
        }
    }

    private static void AddRows(ExcelWorksheet sheet, List<DailyProjectOperationModel> dailies)
    {
        for (int i = 0; i < dailies.Count; i++)
        {
            int row = i + 5;
            var d = dailies[i];

            sheet.Cells[row, 1].Value = i + 1;
            sheet.Cells[row, 2].Value = d.ProjectCode;
            sheet.Cells[row, 3].Value = d.OperationInfoName;
            sheet.Cells[row, 4].Value = d.Location;
            sheet.Cells[row, 5].Value = d.OperationInfoMeasurementName;
            ApplyNumberFormat(sheet, row, 6, d.Length);
            ApplyNumberFormat(sheet, row, 7, d.Height);
            ApplyNumberFormat(sheet, row, 8, d.Width);
            ApplyNumberFormat(sheet, row, 9, d.Weight);
            ApplyNumberFormat(sheet, row, 10, d.Number);
            ApplyNumberFormat(sheet, row, 11, $"F{row}*G{row}*H{row}*I{row}*J{row}");
            sheet.Cells[row, 12].Value = d.Description;
            ApplyNumberFormat(sheet, row, 13, $"F{row}");
            ApplyNumberFormat(sheet, row, 14, $"G{row}");
            ApplyNumberFormat(sheet, row, 15, $"H{row}");
            ApplyNumberFormat(sheet, row, 16, $"I{row}");
            ApplyNumberFormat(sheet, row, 17, $"J{row}");
            ApplyNumberFormat(sheet, row, 18, $"M{row}*N{row}*O{row}*P{row}*Q{row}");
            sheet.Cells[row, 19].Value = "-";
            ApplyNumberFormat(sheet, row, 20, $"N{row}");
            ApplyNumberFormat(sheet, row, 21, $"O{row}");
            ApplyNumberFormat(sheet, row, 22, $"P{row}");
            ApplyNumberFormat(sheet, row, 23, $"Q{row}");
            ApplyNumberFormat(sheet, row, 24, $"R{row}");
            ApplyNumberFormat(sheet, row, 25, $"T{row}*U{row}*V{row}*W{row}*X{row}");
            sheet.Cells[row, 26].Value = "-";
            ApplyNumberFormat(sheet, row, 27, $"T{row}");
            ApplyNumberFormat(sheet, row, 28, $"U{row}");
            ApplyNumberFormat(sheet, row, 29, $"V{row}");
            ApplyNumberFormat(sheet, row, 30, $"W{row}");
            ApplyNumberFormat(sheet, row, 31, $"X{row}");
            ApplyNumberFormat(sheet, row, 32, $"AA{row}*AB{row}*AC{row}*AD{row}*AE{row}");
            sheet.Cells[row, 33].Value = "-";

            SetCellStyle(sheet, i, row, 1, 33);
        }
    }

    private static void AddHeaderCells(ExcelWorksheet sheet, DateTime? startDate, DateTime? endDate)
    {
        // تنظیم فونت و استایل کلی ردیف هدر
        var fullHeaderRange = sheet.Cells["A1:W3"];
        SetHeaderStyle(fullHeaderRange, null, "B Nazanin", 14, true, System.Drawing.Color.LightGray, ExcelBorderStyle.Thin);
        fullHeaderRange.Merge = false;

        // تابعی برای اضافه کردن متن به یک محدوده و اعمال Merge
        void SetCell(string fromCell, string toCell, string text, int fontSize = 14, bool bold = true)
        {
            var range = sheet.Cells[$"{fromCell}:{toCell}"];
            range.Merge = true;
            SetHeaderStyle(range, text, fontSize, bold);
        }

        // ستون‌های هدر اصلی
        SetCell("A1", "C3", "لوگو کارفرما");
        SetCell("D1", "F1", "موضوع قرارداد:");
        SetCell("D2", "F2", "شماره قرارداد:");
        SetCell("D3", "F3", "کد پروژه کارفرما:");
        SetCell("G1", "H1", "تاریخ تحویل زمین:");
        SetCell("G2", "H2", "شماره صورت وضعیت فعلی:");
        SetCell("G3", "H3", "شماره صورت وضعیت قبلی:");
        SetCell("I1", "K1", "نام کارفرما");
        SetCell("I2", "K2", "نام کوتاه");
        SetCell("I3", "K3", "روکش صورت وضعیت");

        SetCell("L1", "P1", "نام کامل صورت وضعیت:");
        SetCell("L2", "P2", startDate.HasValue && endDate.HasValue
            ? $"دوره کارکرد:\nاز {startDate:yyyy/MM/dd} تا {endDate:yyyy/MM/dd}"
            : "دوره کارکرد:\nاز ... تا ...");
        SetCell("L3", "P3", $"تاریخ ارائه:\n{DateTime.Now:yyyy/MM/dd}");

        SetCell("Q1", "S3", "رشته:");
        SetCell("T1", "U3", "نام پیمانکار:");
        SetCell("V1", "W3", "لوگو پیمانکار");

        // تنظیم ارتفاع و عرض مناسب
        sheet.Row(1).Height = 30;
        sheet.Row(2).Height = 30;
        sheet.Row(3).Height = 30;
        sheet.Cells["A1:W3"].Style.WrapText = true;
        sheet.Cells["A1:W3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells["A1:W3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
    }

    private static void ProjectOperationAddColumnHeaders(ExcelWorksheet sheet)
    {
        string[] headers = [
        "ردیف", "شرح عملیات", "واحد", "مقدار تایید شده در صورت وضعیت قبلی",
        "مقدار تایید شده در این صورت وضعیت", "مقدار\n (پیمانکار)", "مقدار\n (ناظر)",
        "مقدار\n (مشاور)", "مقدار تجمیعی\n (نماینده کارفرما)", "قیمت\n (پیمانکار)",
        "قیمت \n (واحد بازرگانی)", "قیمت کل\n (پیمانکار)", "قیمت کل\n (واحد بازرگانی)",
        "کد پروژه", "نام قرارداد", "ردیف قرارداد", "مقدار قرارداد", "مقدار الحاقیه 1",
        "جمع قرارداد و الحاقیه", "اختلاف", "درصد اختلاف", "(>25%)\n - (out of contract)", "توضیحات"
        ];

        for (int i = 0; i < headers.Length; i++)
            sheet.Cells[4, i + 1].Value = headers[i];

        SetHeaderStyle(sheet.Cells["A4:W4"], null, "Arial", 12, false, System.Drawing.Color.LightGray, ExcelBorderStyle.Medium);
    }

    #region Helper Methods

    private static void ConfigureWorksheetStyle(ExcelWorksheet sheet)
    {
        sheet.View.RightToLeft = true;
        sheet.Cells.Style.ReadingOrder = ExcelReadingOrder.RightToLeft;
        sheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
    }

    private static void AddHeader(ExcelWorksheet sheet, DateTime? startDate, DateTime? endDate)
    {
        // استایل کلی برای کل هدر
        var fullHeaderRange = sheet.Cells["A1:W3"];
        SetHeaderStyle(fullHeaderRange, null, "B Nazanin", 14, true, System.Drawing.Color.LightGray, ExcelBorderStyle.Thin);
        fullHeaderRange.Merge = false;

        // تابع کمکی برای تنظیم متن و Merge
        void SetCell(string from, string to, string text, int fontSize = 14, bool bold = true)
        {
            var range = sheet.Cells[$"{from}:{to}"];
            range.Merge = true;
            SetHeaderStyle(range, text, fontSize, bold);
        }

        // محتواهای مختلف هدر
        SetCell("A1", "C3", "لوگو کارفرما");
        SetCell("D1", "F1", "موضوع قرارداد:");
        SetCell("D2", "F2", "شماره قرارداد:");
        SetCell("D3", "F3", "کد پروژه کارفرما:");
        SetCell("G1", "H1", "تاریخ تحویل زمین:");
        SetCell("G2", "H2", "شماره صورت وضعیت فعلی:");
        SetCell("G3", "H3", "شماره صورت وضعیت قبلی:");
        SetCell("I1", "K1", "نام کارفرما");
        SetCell("I2", "K2", "نام کوتاه");
        SetCell("I3", "K3", "روکش صورت وضعیت");
        SetCell("L1", "P1", "نام کامل صورت وضعیت:");

        // محاسبه دوره
        string period = (startDate.HasValue && endDate.HasValue)
            ? $"دوره کارکرد:\nاز تاریخ {startDate.Value:yyyy/MM/dd} تا {endDate.Value:yyyy/MM/dd}"
            : "دوره کارکرد:\nاز تاریخ ... تا تاریخ ...";

        SetCell("L2", "P2", period);
        SetCell("L3", "P3", $"تاریخ ارائه صورت وضعیت:\n{DateTime.Now:yyyy/MM/dd}");

        SetCell("Q1", "S3", "رشته:");
        SetCell("T1", "U3", "نام پیمانکار:");
        SetCell("V1", "W3", "لوگو پیمانکار");

        // استایل بیشتر
        sheet.Row(1).Height = 30;
        sheet.Row(2).Height = 30;
        sheet.Row(3).Height = 30;
        sheet.Cells["A1:W3"].Style.WrapText = true;
        sheet.Cells["A1:W3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        sheet.Cells["A1:W3"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
    }

    private static void DailyAddColumnHeaders(ExcelWorksheet sheet)
    {
        string[] headers = [
        "ردیف", "کد پروژه", "شرح عملیات", "شرح", "واحد",
        "طول \n (پیمانکار)", "ضخامت یا ارتفاع \n (پیمانکار)", "عرض \n (پیمانکار)", "وزن یا ضریب \n (پیمانکار)", "تعداد \n (پیمانکار)",
        "مقدارپیمانکار", "توضیحات \n (پیمانکار)",
        "طول \n (ناظر)", "ضخامت یا ارتفاع \n (ناظر)", "عرض \n (ناظر)", "وزن یا ضریب \n (ناظر)", "تعداد \n (ناظر)", "جزیی(ناظر)", "توضیحات \n (ناظر)",
        "طول \n (مشاور)", "ضخامت یا ارتفاع \n (مشاور)", "عرض \n (مشاور)", "وزن یا ضریب \n (مشاور)", "تعداد \n (مشاور)", "جزیی(مشاور)", "توضیحات \n (مشاور)",
        "طول \n (نماینده کارفرما)", "ضخامت یا ارتفاع \n (نماینده کارفرما)", "عرض \n (نماینده کارفرما)", "وزن یا ضریب \n (نماینده کارفرما)", "تعداد \n (نماینده کارفرما)", "جزیی(نماینده کارفرما)", "توضیحات \n (نماینده کارفرما)"
        ];

        for (int i = 0; i < headers.Length; i++)
        {
            sheet.Cells[4, i + 1].Value = headers[i];
        }

        SetHeaderStyle(sheet.Cells[$"A4:AG4"], null, "B Nazanin", 11, false, System.Drawing.Color.LightGray, ExcelBorderStyle.Medium);
    }

    private static void ApplyNumberFormat(ExcelWorksheet sheet, int row, int column, string formula)
    {
        sheet.Cells[row, column].Style.Numberformat.Format = "#,##0.00";
        sheet.Cells[row, column].Formula = formula;
    }

    private static void ApplyNumberFormat(ExcelWorksheet sheet, int row, int column, decimal value)
    {
        sheet.Cells[row, column].Value = value;
        sheet.Cells[row, column].Style.Numberformat.Format = "#,##0.00";
    }

    private static void CreateTable(ExcelWorksheet sheet, int rowCount, int count, string name)
    {
        int dataStartRow = 5;
        int dataEndRow = dataStartRow + rowCount - 1;

        var tableRange = sheet.Cells[4, 1, dataEndRow, count];
        var table = sheet.Tables.Add(tableRange, $"tbl_{name}");
        table.ShowHeader = true;
        table.TableStyle = TableStyles.Medium2;

        // فریز کردن ردیف عنوان
        sheet.View.FreezePanes(dataStartRow, 1);

        // استایل برای هدر
        var headerRange = sheet.Cells[4, 1, 4, count];
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
        headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);

        // رنگ متناوب برای ردیف‌ها
        for (int i = dataStartRow; i <= dataEndRow; i++)
        {
            if ((i - dataStartRow) % 2 == 0)
            {
                sheet.Cells[i, 1, i, count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells[i, 1, i, count].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(245, 245, 245));
            }
        }

    }

    #endregion


    public static void SetCellStyle(ExcelWorksheet worksheet, int number, int row, int startColumn, int endColumn)
    {
        using (var dataRange = worksheet.Cells[row, startColumn, row, endColumn])
        {
            dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Fill.PatternType = ExcelFillStyle.Solid;  // Ensure the fill style is set

            if (number % 2 != 0)
                dataRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            else
                dataRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
        }
    }


    static void SetHeaderStyle(ExcelRange range, string? value, string? fontName, float fontSize, bool? isMerge, System.Drawing.Color bgColor, ExcelBorderStyle borderStyle)
    {
        if (isMerge is not null)
            range.Merge = isMerge.Value;

        if (!string.IsNullOrEmpty(value))
            range.Value = value;

        range.Style.Font.Bold = true;
        range.Style.Font.Size = fontSize;

        if (fontName is not null)
            range.Style.Font.Name = fontName;

        range.Style.Border.Top.Style = borderStyle;
        range.Style.Border.Left.Style = borderStyle;
        range.Style.Border.Right.Style = borderStyle;
        range.Style.Border.Bottom.Style = borderStyle;

        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
        range.Style.Fill.BackgroundColor.SetColor(bgColor);
    }

    static void SetHeaderStyle(ExcelRange range, string? value, float fontSize, bool? isMerge)
    {
        if (isMerge is not null)
            range.Merge = isMerge.Value;

        if (!string.IsNullOrEmpty(value))
            range.Value = value;

        range.Style.Font.Bold = true;
        range.Style.Font.Size = fontSize;
    }

    public static void AdjustColumnWidths(ExcelWorksheet worksheet)
    {
        if (worksheet.Dimension == null) return;

        if (OperatingSystem.IsWindows())
        {
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            return;
        }

        // EPPlus 4 AutoFit uses System.Drawing font metrics, which is not supported
        // by .NET 8 on Linux. Estimate widths without initializing GDI/font APIs.
        for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
        {
            var maxLength = 0;
            for (int row = worksheet.Dimension.Start.Row; row <= worksheet.Dimension.End.Row; row++)
            {
                var value = worksheet.Cells[row, col].Text;
                if (!string.IsNullOrEmpty(value))
                    maxLength = Math.Max(maxLength, value.Split('\n').Max(line => line.Length));
            }

            worksheet.Column(col).Width = Math.Clamp(maxLength + 2, 10, 40);
        }
    }

    public static void AdjustWrapText(ExcelWorksheet worksheet)
    {
        worksheet.Cells.Style.WrapText = true;
        worksheet.Cells[worksheet.Dimension.Address].Style.WrapText = true;
    }

    public static void AddImageToCell(ExcelWorksheet worksheet, string imageName, string imagePath, int row, int column)
    {
        var picture = worksheet.Drawings.AddPicture($"{imageName}", new FileInfo(imagePath));
        picture.SetPosition(1, 2, 1, 3);
    }
}

//ست کردن قفل و پسورد
//projectOperationDetailWorksheet.Cells[row, 11].Style.Locked = true;
//projectOperationDetailWorksheet.Protection.IsProtected = true;
//projectOperationDetailWorksheet.Protection.SetPassword("your_password_here");
