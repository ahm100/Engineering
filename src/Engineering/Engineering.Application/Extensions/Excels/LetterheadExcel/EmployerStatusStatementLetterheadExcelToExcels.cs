using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementLetterheadExcel;
using MathNet.Numerics;
using OfficeOpenXml.Style;

namespace Engineering.Application.Extensions.Excels.LetterheadExcel;

public static class EmployerStatusStatementLetterheadExcelToExcels
{
    public static byte[] EmployerStatusStatementLetterheadExcelToExcel(
        GetEmployerStatusStatementLetterheadExcelModel employerStatusStatement)
    {
        using var workbook = new ExcelPackage();

        #region شیت روکش صورت وضعیت

        var projectOperationWorksheet = workbook.Workbook.Worksheets.Add("روکش صورت وضعیت");
        projectOperationWorksheet.View.RightToLeft = true;
        // تنظیم جهت متن‌ها به راست به چپ
        projectOperationWorksheet.Cells.Style.ReadingOrder = ExcelReadingOrder.RightToLeft;
        // تنظیم جهت متن‌ها به مرکزیت
        projectOperationWorksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        projectOperationWorksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //تصویر کارفرما
        SetHeaderStyle(projectOperationWorksheet.Cells["A1:C3"],
            $"لوگو کارفرما", 14, true);
        //AddImageToCell(projectOperationWorksheet, "Employer", @"D:\Employer.png", 1, 1);

        //موضوع قرارداد
        SetHeaderStyle(projectOperationWorksheet.Cells["D1:F1"],
            $"موضوع قرارداد:\n {employerStatusStatement.Description}.", 14, true);
        //شماره قرارداد
        SetHeaderStyle(projectOperationWorksheet.Cells["D2:F2"],
            $"شماره قرارداد:\n {employerStatusStatement.EmployerContractCode}.", 14, true);
        //کد پروژه کارفرما
        SetHeaderStyle(projectOperationWorksheet.Cells["D3:F3"],
            $"کد پروژه کارفرما:\n {employerStatusStatement.ProjectCode}.", 14, true);

        //تاریخ تحویل زمین
        SetHeaderStyle(projectOperationWorksheet.Cells["G1:H1"],
            $"تاریخ تحویل زمین:\n ", 14, true);
        //شماره صورت وضعیت فعلی
        SetHeaderStyle(projectOperationWorksheet.Cells["G2:H2"],
            $"شماره صورت وضعیت فعلی:\n {employerStatusStatement.StatusStatementCode}", 14, true);
        //شماره صورت وضعیت قبلی
        SetHeaderStyle(projectOperationWorksheet.Cells["G3:H3"],
            $"شماره صورت وضعیت قبلی:\n {employerStatusStatement.LastEmployerStatusStatementCode}", 14, true);

        //نام کارفرما
        SetHeaderStyle(projectOperationWorksheet.Cells["I1:K1"],
            $"{employerStatusStatement?.EmployerName}", 14, true);
        //نام کوتاه یا انگلیسی کارفرما
        SetHeaderStyle(projectOperationWorksheet.Cells["I2:K2"],
            $"{employerStatusStatement?.EmployerName}", 14, true);
        //عنوان
        SetHeaderStyle(projectOperationWorksheet.Cells["I3:K3"],
            $"روکش صورت وضعیت", 14, true);

        //نام کامل صورت وضعیت
        SetHeaderStyle(projectOperationWorksheet.Cells["L1:P1"],
            $"نام کامل صورت وضعیت:\n {employerStatusStatement?.StatusStatementCode}", 14, true);
        //دوره کارکرد
        SetHeaderStyle(projectOperationWorksheet.Cells["L2:P2"],
            $"دوره کارکرد:\n از تاریخ {employerStatusStatement?.StartDate.ToString("yyyy/mm/dd")}\n تا تاریخ {employerStatusStatement?.EndDate.ToString("yyyy/mm/dd")}", 14, true);
        //تاریخ ارائه صورت وضعیت
        SetHeaderStyle(projectOperationWorksheet.Cells["L3:P3"],
            $"تاریخ ارائه صورت وضعیت:\n {DateTime.Now.Date}", 14, true);

        //رشته
        SetHeaderStyle(projectOperationWorksheet.Cells["Q1:S3"],
            $"رشته: ", 14, true);

        //نام پیمانکار
        SetHeaderStyle(projectOperationWorksheet.Cells["T1:U3"],
            $"نام پیمانکار:\n ", 14, true);

        //لوگو پیمانکار
        SetHeaderStyle(projectOperationWorksheet.Cells["V1:W3"], $"لوگو پیمانکار", 14, true);
        //AddImageToCell(projectOperationWorksheet, "Contractor", @"D:\Contractor.png", 23, 3);

        ////هدر باقی مونده خالی
        //SetHeaderStyle(projectOperationWorksheet.Cells["R1:W3"], null, 14, true);


        SetHeaderStyle(projectOperationWorksheet.Cells["A1:W1"],
            null, 14, null, System.Drawing.Color.White, ExcelBorderStyle.Thin);
        SetHeaderStyle(projectOperationWorksheet.Cells["A2:W2"],
            null, 14, null, System.Drawing.Color.White, ExcelBorderStyle.Thin);
        SetHeaderStyle(projectOperationWorksheet.Cells["A3:W3"],
            null, 14, null, System.Drawing.Color.White, ExcelBorderStyle.Thin);

        //سر ستون های دیتاها
        projectOperationWorksheet.Cells["A4"].Value = $"ردیف";
        projectOperationWorksheet.Cells["B4"].Value = $"شرح عملیات";
        projectOperationWorksheet.Cells["C4"].Value = $"واحد";
        projectOperationWorksheet.Cells["D4"].Value = $"مقدار تایید شده در صورت وضعیت قبلی";
        projectOperationWorksheet.Cells["E4"].Value = $"مقدار تایید شده در این صورت وضعیت";
        projectOperationWorksheet.Cells["F4"].Value = $"مقدار\n (پیمانکار)";
        projectOperationWorksheet.Cells["G4"].Value = $"مقدار\n (ناظر)";
        projectOperationWorksheet.Cells["H4"].Value = $"مقدار\n (مشاور)";
        projectOperationWorksheet.Cells["I4"].Value = $"مقدار تجمیعی\n (نماینده کارفرما)";
        projectOperationWorksheet.Cells["J4"].Value = $"قیمت\n (پیمانکار)";
        projectOperationWorksheet.Cells["K4"].Value = $"قیمت \n (واحد بازرگانی)";
        projectOperationWorksheet.Cells["L4"].Value = $"قیمت کل\n (پیمانکار)";
        projectOperationWorksheet.Cells["M4"].Value = $"قیمت کل\n (واحد بازرگانی)";
        projectOperationWorksheet.Cells["N4"].Value = $"کد پروژه";
        projectOperationWorksheet.Cells["O4"].Value = $"نام قرارداد";
        projectOperationWorksheet.Cells["P4"].Value = $"ردیف قرارداد";
        projectOperationWorksheet.Cells["Q4"].Value = $"مقدار قرارداد";
        projectOperationWorksheet.Cells["R4"].Value = $"مقدار الحاقیه 1";
        projectOperationWorksheet.Cells["S4"].Value = $"جمع قرارداد و الحاقیه";
        projectOperationWorksheet.Cells["T4"].Value = $"اختلاف";
        projectOperationWorksheet.Cells["U4"].Value = $"درصد اختلاف";
        projectOperationWorksheet.Cells["V4"].Value = $"(>25%)\n - (out of contract)";
        projectOperationWorksheet.Cells["W4"].Value = $"توضیحات";

        SetHeaderStyle(projectOperationWorksheet.Cells["A4:W4"], null, 12, false, System.Drawing.Color.LightGray, ExcelBorderStyle.Medium);

        //// قرار دادن فرمول
        //worksheet.Cells["D2"].Formula = "SUM(A2:C2)";
        //SetHeaderStyle(worksheet.Cells["A3:F3"], "", 12, false, System.Drawing.Color.LightGray);

        var projectOperations = employerStatusStatement?.ProjectOperations.ToList();
        // افزودن داده‌ها و تنظیم استایل آن‌ها
        for (int i = 0; i < projectOperations!.Count; i++)
        {
            projectOperationWorksheet.Cells[i + 5, 1].Value = i + 1;
            projectOperationWorksheet.Cells[i + 5, 2].Value = projectOperations[i].OperationInfoName;
            projectOperationWorksheet.Cells[i + 5, 3].Value = projectOperations[i].UnitOfMeasurementName;
            projectOperationWorksheet.Cells[i + 5, 4].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 5].Value = projectOperations[i].DoneWorkVolume;
            projectOperationWorksheet.Cells[i + 5, 6].Value = projectOperations[i].Workload;
            projectOperationWorksheet.Cells[i + 5, 7].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 8].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 9].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 10].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 11].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 12].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 14].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 14].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 14].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 16].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 17].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 18].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 19].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 20].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 21].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 22].Value = 0;
            projectOperationWorksheet.Cells[i + 5, 23].Value = 0;

            // تنظیم بوردر برای داده‌ها
            SetCellStyle(projectOperationWorksheet, i, i + 5, 1, 23);
        }

        // اضافه کردن جدول
        int dataStartRow = 5;
        int dataEndRow = dataStartRow + projectOperations.Count - 1;
        var tableRange = projectOperationWorksheet.Cells[4, 1, dataEndRow, 23];
        var table = projectOperationWorksheet.Tables.Add(tableRange, "ProjectOperationsTable");
        table.ShowHeader = true;
        table.TableStyle = OfficeOpenXml.Table.TableStyles.Medium9;

        #endregion

        #region شیت کارکرد روازنه ها

        var projectOperationDetailWorksheet = workbook.Workbook.Worksheets.Add("ریزمتره");
        projectOperationDetailWorksheet.View.RightToLeft = true;
        projectOperationDetailWorksheet.Cells.Style.ReadingOrder = ExcelReadingOrder.RightToLeft;
        projectOperationDetailWorksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        projectOperationDetailWorksheet.Cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        var projectOperationDetails = employerStatusStatement?.ProjectOperations.SelectMany(x => x.DailyProjectOperationDetails!).ToList();

        //تصویر کارفرما
        SetHeaderStyle(projectOperationWorksheet.Cells["A1:C3"],
            $"لوگو کارفرما", 14, true);
        //AddImageToCell(projectOperationWorksheet, "Employer", @"D:\Employer.png", 1, 1);

        //موضوع قرارداد
        SetHeaderStyle(projectOperationWorksheet.Cells["D1:F1"],
            $"موضوع قرارداد:\n {employerStatusStatement?.Description}.", 14, true);
        //شماره قرارداد
        SetHeaderStyle(projectOperationWorksheet.Cells["D2:F2"],
            $"شماره قرارداد:\n {employerStatusStatement?.EmployerContractCode}.", 14, true);
        //کد پروژه کارفرما
        SetHeaderStyle(projectOperationWorksheet.Cells["D3:F3"],
            $"کد پروژه کارفرما:\n {employerStatusStatement?.ProjectCode}.", 14, true);

        //تاریخ تحویل زمین
        SetHeaderStyle(projectOperationWorksheet.Cells["G1:H1"],
            $"تاریخ تحویل زمین:\n ", 14, true);
        //شماره صورت وضعیت فعلی
        SetHeaderStyle(projectOperationWorksheet.Cells["G2:H2"],
            $"شماره صورت وضعیت فعلی:\n {employerStatusStatement?.StatusStatementCode}", 14, true);
        //شماره صورت وضعیت قبلی
        SetHeaderStyle(projectOperationWorksheet.Cells["G3:H3"],
            $"شماره صورت وضعیت قبلی:\n {employerStatusStatement?.LastEmployerStatusStatementCode}", 14, true);

        //نام کارفرما
        SetHeaderStyle(projectOperationWorksheet.Cells["I1:K1"],
            $"{employerStatusStatement?.EmployerName}", 14, true);
        //نام کوتاه یا انگلیسی کارفرما
        SetHeaderStyle(projectOperationWorksheet.Cells["I2:K2"],
            $"{employerStatusStatement?.EmployerName}", 14, true);
        //عنوان
        SetHeaderStyle(projectOperationWorksheet.Cells["I3:K3"],
            $"روکش صورت وضعیت", 14, true);

        //نام کامل صورت وضعیت
        SetHeaderStyle(projectOperationWorksheet.Cells["L1:P1"],
            $"نام کامل صورت وضعیت:\n {employerStatusStatement?.StatusStatementCode}", 14, true);
        //دوره کارکرد
        SetHeaderStyle(projectOperationWorksheet.Cells["L2:P2"],
            $"دوره کارکرد:\n از تاریخ {employerStatusStatement?.StartDate.ToString("yyyy/mm/dd")}\n تا تاریخ {employerStatusStatement?.EndDate.ToString("yyyy/mm/dd")}", 14, true);
        //تاریخ ارائه صورت وضعیت
        SetHeaderStyle(projectOperationWorksheet.Cells["L3:P3"],
            $"تاریخ ارائه صورت وضعیت:\n {DateTime.Now.Date}", 14, true);

        //رشته
        SetHeaderStyle(projectOperationWorksheet.Cells["Q1:S3"],
            $"رشته: ", 14, true);

        //نام پیمانکار
        SetHeaderStyle(projectOperationWorksheet.Cells["T1:U3"],
            $"نام پیمانکار:\n ", 14, true);

        //لوگو پیمانکار
        SetHeaderStyle(projectOperationWorksheet.Cells["V1:W3"], $"لوگو پیمانکار", 14, true);
        //AddImageToCell(projectOperationWorksheet, "Contractor", @"D:\Contractor.png", 23, 3);

        //سر ستون های دیتاها
        projectOperationWorksheet.Cells["A4"].Value = $"ردیف";
        projectOperationWorksheet.Cells["B4"].Value = $"کد پروژه";
        projectOperationWorksheet.Cells["C4"].Value = $"شرح عملیات";
        projectOperationWorksheet.Cells["D4"].Value = $"شرح";
        projectOperationWorksheet.Cells["E4"].Value = $"واحد";
        projectOperationWorksheet.Cells["F4"].Value = $"طول \n (پیمانکار)";
        projectOperationWorksheet.Cells["G4"].Value = $"ضخامت یا ارتفاع \n (پیمانکار)";
        projectOperationWorksheet.Cells["H4"].Value = $"عرض \n (پیمانکار)";
        projectOperationWorksheet.Cells["I4"].Value = $"وزن یا ضریب \n (پیمانکار)";
        projectOperationWorksheet.Cells["J4"].Value = $"تعداد \n (پیمانکار)";
        projectOperationWorksheet.Cells["K4"].Value = $"جزئی \n (پیمانکار)";
        projectOperationWorksheet.Cells["L4"].Value = $"توضیحات \n (پیمانکار)";
        projectOperationWorksheet.Cells["M4"].Value = $"طول \n (ناظر)";
        projectOperationWorksheet.Cells["N4"].Value = $"ضخامت یا ارتفاع \n (ناظر)";
        projectOperationWorksheet.Cells["O4"].Value = $"عرض \n (ناظر)";
        projectOperationWorksheet.Cells["P4"].Value = $"وزن یا ضریب \n (ناظر)";
        projectOperationWorksheet.Cells["Q4"].Value = $"تعداد \n (ناظر)";
        projectOperationWorksheet.Cells["R4"].Value = $"جزئی \n (ناظر)";
        projectOperationWorksheet.Cells["S4"].Value = $"توضیحات \n (ناظر)";
        projectOperationWorksheet.Cells["T4"].Value = $"طول \n (مشاور)";
        projectOperationWorksheet.Cells["U4"].Value = $"ضخامت یا ارتفاع \n (مشاور)";
        projectOperationWorksheet.Cells["V4"].Value = $"عرض \n (مشاور)";
        projectOperationWorksheet.Cells["W4"].Value = $"وزن یا ضریب \n (مشاور)";
        projectOperationWorksheet.Cells["U4"].Value = $"تعداد \n (مشاور)";
        projectOperationWorksheet.Cells["V4"].Value = $"جزئی \n (مشاور)";
        projectOperationWorksheet.Cells["W4"].Value = $"توضیحات \n (مشاور)";
        projectOperationWorksheet.Cells["T4"].Value = $"طول \n (نماینده کارفرما)";
        projectOperationWorksheet.Cells["U4"].Value = $"ضخامت یا ارتفاع \n (نماینده کارفرما)";
        projectOperationWorksheet.Cells["V4"].Value = $"عرض \n (نماینده کارفرما)";
        projectOperationWorksheet.Cells["W4"].Value = $"وزن یا ضریب \n (نماینده کارفرما)";
        projectOperationWorksheet.Cells["U4"].Value = $"تعداد \n (نماینده کارفرما)";
        projectOperationWorksheet.Cells["V4"].Value = $"جزئی \n (نماینده کارفرما)";
        projectOperationWorksheet.Cells["W4"].Value = $"توضیحات \n (نماینده کارفرما)";

        SetHeaderStyle(projectOperationWorksheet.Cells["A4:W4"], null, 12, false, System.Drawing.Color.LightGray, ExcelBorderStyle.Medium);

        #endregion

        AdjustWrapText(projectOperationWorksheet);
        AdjustColumnWidths(projectOperationWorksheet);

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return content;
    }

    public static void SetCellStyle(ExcelWorksheet worksheet, int number, int row, int startColumn, int endColumn)
    {
        using (var dataRange = worksheet.Cells[row, startColumn, row, endColumn])
        {
            dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Fill.PatternType = ExcelFillStyle.Solid;  // Ensure the fill style is set

            if (number.IsOdd())
                dataRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
            else
                dataRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White);
        }
    }


    static void SetHeaderStyle(ExcelRange range, string? value, float fontSize, bool? isMerge, System.Drawing.Color bgColor, ExcelBorderStyle borderStyle)
    {
        if (isMerge is not null)
            range.Merge = isMerge.Value;

        if (!string.IsNullOrEmpty(value))
            range.Value = value;
        range.Style.Font.Bold = true;
        range.Style.Font.Size = fontSize;
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

        range.AutoFitColumns();
    }

    public static void AdjustColumnWidths(ExcelWorksheet worksheet)
    {
        // اطمینان از این که Dimension مقدار داره
        if (worksheet.Dimension == null) return;

        // رفتن روی هر ستون و AutoFit کردن جداگانه
        for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
        {
            worksheet.Column(col).AutoFit();

            // اگه WrapText فعال باشه، گاهی بهتره حداقل عرض هم بدیم
            if (worksheet.Column(col).Width < 10)
                worksheet.Column(col).Width = 10;
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
        //picture.SetSize(50, 50); // تنظیم اندازه تصویر به دلخواه
    }


}
