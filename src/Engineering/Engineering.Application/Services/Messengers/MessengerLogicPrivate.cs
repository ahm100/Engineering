using Engineering.Application.Services.TelegramChats.Models.CommercialPackingTelegramMessage;
using Engineering.Application.Services.TelegramChats.TelegramServices;
using Engineering.Application.Services.TelegramChats.TelegramServices.Models;

namespace Engineering.Application.Services.Messengers;

public partial class MessengerLogic
{
    private static string AddHashTags(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        return $"#{string.Join("_", input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))}";
    }

    public static string CommerceRequestWarehouseStatusChangeMessageModel(string? status,
        long? requestNumber,
        string? costCenterName,
        string? projectName,
        string? description,
        string? createDate,
        string? createTime,
        string? creatorChange,
        string? creatorTelegramId,
        string? creatorRequest,
        string? productName,
        string? productCode,
        decimal? requestedCount,
        string? commerceRequestType,
        string? commercialRequestNumber, CT ct)
    {
        string message = string.Empty;
        var commercialNumber = !string.IsNullOrEmpty(commercialRequestNumber)
                ? $"{Environment.NewLine}<b>شماره درخواست تامین:</b> {commercialRequestNumber} {Environment.NewLine}"
                : string.Empty;

        message =
            $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
            $"{TelegramValues.SendBackIcon}<b>تغییر وضعیت درخواست {commerceRequestType}</b>{Environment.NewLine}{Environment.NewLine}" +
            $"<b>درخواست دهنده:</b> {creatorRequest} {Environment.NewLine}" +
            $"وضعیت درخواست {commerceRequestType} شما با شماره <b>{requestNumber}</b> به <b>{status}</b> تغییر کرد" +
            commercialNumber +
            $"{Environment.NewLine}<b>مرکز هزینه:</b> {costCenterName} {Environment.NewLine}" +
            $"<b>پروژه:</b> {projectName} {Environment.NewLine}{Environment.NewLine}" +
            $"{TelegramValues.Icon13}<b>لیست کالاها:</b>{Environment.NewLine}" +
            $"<b>نام کالا:</b> {productName} {Environment.NewLine}" +
            $"<b>کد:</b> {productCode} {Environment.NewLine}" +
            $"<b>تعداد:</b> {requestedCount} {Environment.NewLine}{Environment.NewLine}" +
            $"<b>علت {status}:</b> {description} {Environment.NewLine}" +
            $"{TelegramValues.CalendarIcon}<b>تاریخ :</b> {createTime} {createDate} {Environment.NewLine}" +
            $"{TelegramValues.UserIcon}<b>کاربر بررسی کننده درخواست: </b> {creatorChange} {Environment.NewLine}" +
            $"{creatorTelegramId}";

        return message;
    }

    public static string ConsumerExitMessageModel(string? warehouseName,
        string? documentNumber,
        List<ProductInvoiceForTelegramModel>? products,
        string? requestNumber,
        string? thirdPartyName,
        string? receiverDelivery,
        string? projectOperation,
        string? createDate,
        string? createTime,
        string? exitDate,
        string? exitTime,
        string? creator,
        string? confirmCreator, CT ct)
    {
        string message = string.Empty;

        message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.Icon7}<b>خروج مصرفی کالا از انبار {warehouseName} </b> {Environment.NewLine}{Environment.NewLine}" +
                $"<b>شماره سند : </b> {documentNumber} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت خروج :</b> {exitTime}  {exitDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.Icon13}<b>لیست کالاها :</b> {Environment.NewLine}";

        if (products != null && products.Any())
        {
            foreach (var product in products)
            {
                message +=
                    $"<b>{TelegramValues.BulletPoint}</b> {product.Name} {product.Quantity} {product.MeasureUnitName} {Environment.NewLine}";
            }
        }

        message +=
            $"{Environment.NewLine}<b>طرف حساب :</b> {thirdPartyName} {Environment.NewLine}" +
            $"<b>توضیحات تایید :</b> {receiverDelivery} {Environment.NewLine}" +
            $"<b>شماره درخواست :</b> {requestNumber} {Environment.NewLine}" +
            $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
            $"{TelegramValues.UserIcon}<b>ثبت کننده درخواست :</b> {creator} {Environment.NewLine}" +
            $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده درخواست :</b> {confirmCreator} {Environment.NewLine}";

        return message;
    }

    public static string EnteringToWarehouseMessageModel(string? warehouseName,
        List<ProductInvoiceForTelegramModel>? products,
        string? requestNumber,
        string? receiverDelivery,
        string? projectOperation,
        string? createDate,
        string? createTime,
        string? enterDate,
        string? enterTime,
        string? creator,
        string? confirmCreator, CT ct)
    {
        string message = string.Empty;

        message =
            $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
            $"{TelegramValues.Icon6}<b>ورود کالا به انبار {warehouseName} </b> {Environment.NewLine}{Environment.NewLine}" +
            $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ورود :</b>{enterTime} {enterDate} {Environment.NewLine}{Environment.NewLine}" +
            $"{TelegramValues.Icon13}<b>لیست کالاها :</b> {Environment.NewLine}";

        if (products != null && products.Any())
            foreach (var product in products)
            {
                message +=
                    $"<b>{TelegramValues.BulletPoint}</b> {product.Name} {product.Quantity} {product.MeasureUnitName} ({product.Status}) {Environment.NewLine}";
            }

        message +=
            $"{Environment.NewLine}<b>توضیحات تحویل :</b> {receiverDelivery} {Environment.NewLine}" +
            $"<b>شماره درخواست :</b> {requestNumber} {Environment.NewLine}" +
            $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ثبت :</b>{createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
            $"{TelegramValues.UserIcon}<b>ثبت کننده درخواست :</b> {creator} {Environment.NewLine}" +
            $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده درخواست :</b> {confirmCreator} {Environment.NewLine}";

        return message;
    }

    public static string ExitForRelocationMessageModel(string? sourceWarehouseName,
        string? destinationWarehouseName,
        string? documentNumber,
        List<ProductInvoiceForTelegramModel>? products,
        string? requestNumber,
        string? projectOperation,
        string? createDate,
        string? createTime,
        string? exitDate,
        string? exitTime,
        string? creator,
        string? confirmCreator, CT ct)
    {
        string message = string.Empty;

        message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.Icon7}<b>خروج بین انباری</b> {Environment.NewLine}{Environment.NewLine}" +
                $"<b>انبار مبدا :</b> {sourceWarehouseName} {Environment.NewLine}" +
                $"<b>انبار مقصد :</b> {destinationWarehouseName} {Environment.NewLine}" +
                $"<b>شماره سند :</b> {documentNumber} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت خروج :</b> {exitTime} {exitDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.Icon13}<b>لیست کالاها :</b> {Environment.NewLine}";

        if (products?.Any() == true)
        {
            foreach (var product in products)
            {
                message +=
                    $"<b>{TelegramValues.BulletPoint}</b> {product.Name} {product.Quantity} {product.MeasureUnitName} {Environment.NewLine}";
            }
        }

        message +=
            $"{Environment.NewLine}<b>شماره درخواست :</b> {requestNumber} {Environment.NewLine}" +
            $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
            $"{TelegramValues.UserIcon}<b>ثبت کننده درخواست :</b> {creator} {Environment.NewLine}" +
            $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده درخواست :</b> {confirmCreator} {Environment.NewLine}";

        return message;
    }

    public static string TemporaryDeliveryMessageModel(string? warehouseName,
        string? destinationWarehouse,
        List<ProductInvoiceForTelegramModel>? products,
        string? receiverDelivery,
        string? projectOperation,
        string? createDate,
        string? createTime,
        string? enterDate,
        string? enterTime,
        string? creator,
        string? confirmCreator, CT ct)
    {
        string message = string.Empty;

        message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.Icon6}<b> ورود کالا به انبار تحویل موقت {warehouseName} </b> {Environment.NewLine}{Environment.NewLine}" +
                $"<b>انبار هدف :</b> {destinationWarehouse} {Environment.NewLine}" +
                $"<b>تاریخ ورود :</b> {enterDate} {Environment.NewLine}" +
                $"<b>ساعت ورود :</b> {enterTime} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.Icon13}<b>لیست کالاها :</b> {Environment.NewLine}";

        if (products?.Any() == true)
        {
            foreach (var product in products)
            {
                message +=
                    $"<b>{TelegramValues.BulletPoint}</b> {product.Name} {product.Quantity} {product.MeasureUnitName} {Environment.NewLine}";
            }
        }

        message +=
            $"{Environment.NewLine}<b>توضیحات تحویل :</b> {receiverDelivery} {Environment.NewLine}" +
            $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
            $"{TelegramValues.UserIcon}<b>ثبت کننده درخواست :</b> {creator} {Environment.NewLine}" +
            $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده درخواست :</b> {confirmCreator} {Environment.NewLine}";

        return message;
    }

    public static string CommercialPaymentMessageModel(string? shabaNo,
        decimal? amount,
        string? createDate,
        string? createTime,
        string? reason,
        string? description,
        string? thirdPartyName, CT ct)
    {
        string message = string.Empty;

        message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"<b>ارسال دستور پرداخت به سیستم ساتک</b> {Environment.NewLine}" +
                $"<b>مرکزهزینه : </b> {reason} {Environment.NewLine}" +
                $"<b>طرف حساب :</b> {thirdPartyName} {Environment.NewLine}" +
                $"<b>مبلغ : </b> {amount} {Environment.NewLine}" +
                $"<b>تاریخ :</b> {createDate} {createTime} {Environment.NewLine}" +
                $"<b>توضیحات :</b> {description} {Environment.NewLine}";

        return message;
    }

    public static string CommercialPackingMessageModel(bool isUpdated,
         string? supplierName,
         string? thirdParty,
         string? followupName,
         string? destinationWarehouseName,
         long? requestNumber,
         List<WarehouseProductGroupModel>? products,
         string? description,
         string? createDate,
         string? createTime,
         string? deliveryDate,
         string? deliveryTime, CT ct)
    {
        string message = string.Empty;

        string formattedCreateDate = createDate?.Replace("/", "") ?? string.Empty;
        string formattedDeliveryDate = deliveryDate?.Replace("/", "") ?? string.Empty;

        message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.BoxEmoji} <b> مدیریت پکینگ</b> {Environment.NewLine}";

        if (isUpdated)
            message += $"<b> پکینگ اصلاحی</b>{Environment.NewLine}";

        message +=
            $"<b>شماره پکینگ :</b> {requestNumber}{Environment.NewLine}" +
            $"<b>انبار تحویل گیرنده :</b> {destinationWarehouseName}{Environment.NewLine}{Environment.NewLine}" +
            $"#Date_{formattedCreateDate} {Environment.NewLine}" +
            $"#DeliveryDate_{formattedDeliveryDate} {Environment.NewLine}{Environment.NewLine}";

        if (products != null && products.Any())
        {
            int counter = 1;

            foreach (var group in products)
            {
                message += $"انبار:{group.WarehouseName} - مرکزهزینه : {group.CostCenterName}{Environment.NewLine}";

                if (group.Products != null)
                {
                    foreach (var product in group.Products)
                    {
                        message +=
                            $"<b>{counter}- شماره درخواست ({product.RequestNumber})</b>{Environment.NewLine}" +
                            $"{TelegramValues.BulletPoint} {product.ProductName} {product.Quantity} {product.MeasureUnitName} {Environment.NewLine}";

                        counter++;
                    }
                }

                message += Environment.NewLine;
            }
        }

        message +=
            $"<b>رابط شرکت :</b>{supplierName} {Environment.NewLine}" +
            $"<b>فروشگاه :</b>{thirdParty} {Environment.NewLine}" +
            $"<b>مسئول خرید :</b>{followupName} {Environment.NewLine}{Environment.NewLine}" +
            $"{TelegramValues.MemoIcon} <b>توضیحات :</b>{description} {Environment.NewLine}{Environment.NewLine}" +
            $"{TelegramValues.CalendarIcon}<b>تاریخ ارسال :</b> {deliveryDate} {Environment.NewLine}" +
            $"{TelegramValues.CalendarIcon}<b>تاریخ ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}";

        return message;
    }

    public static string AcceptPaymentOrderMessageModel(string? referenceDetails,
        string? createDate,
        string? createTime,
        string? creator, CT ct)
    {
        string message = string.Empty;

        message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"<b>تایید دستور پرداخت</b> {Environment.NewLine}{Environment.NewLine}" +
                $"{referenceDetails} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ تایید :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده :</b> {creator} {Environment.NewLine}";

        return message;
    }

    public static string ContractorStatementPaymentMessageModel(string? date,
        string? time,
        string? number,
        string? paymentOrderNumber,
        string? thirdParty,
        string? description,
        string? defaultDescription,
        string? shabaNo,
        decimal? amount, CT ct)
    {
        string message = string.Empty;

        var thirdPartyTag = thirdParty is not null ? AddHashTags(thirdParty) : string.Empty;
        var shabaNoTag = shabaNo is not null ? AddHashTags(shabaNo) : string.Empty;
        string formattedDate = date?.Replace("/", "") ?? string.Empty;

        message =
            $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
            $"<b>پرداخت صورت وضعیت پیمانکار</b> {Environment.NewLine}{Environment.NewLine}" +
            $"#Date_{formattedDate} {Environment.NewLine}" +
            $"<b>طرف حساب :</b> {thirdParty} {thirdPartyTag} {Environment.NewLine}" +
            $"<b>شماره شبا :</b> {shabaNoTag} {Environment.NewLine}" +
            $"<b>شماره سند (شماره دستور پرداخت):</b> #PaymentOrderNumber_{paymentOrderNumber} {Environment.NewLine}" +
            $"<b>شماره پرداخت :</b> #Number_{number} {Environment.NewLine}" +
            $"<b>مبلغ :</b> {amount:#,##0.##} {Environment.NewLine}" +
            $"<b>تاریخ :</b> {time} {date} {Environment.NewLine}" +
            $"<b>بابت :</b> {defaultDescription} {Environment.NewLine}" +
            $"<b>توضیحات :</b> {description} {Environment.NewLine}";

        return message;
    }

    public static string PaymentTreasuryTelegramMessageModel(
        string? date,
        string? time,
        string? number,
        string? paymentOrderNumber,
        string? thirdParty,
        string? description,
        string? defaultDescription,
        string? shabaNo,
        decimal? amount, CT ct)
    {
        string message = string.Empty;

        var thirdPartyTag = thirdParty is not null ? AddHashTags(thirdParty) : string.Empty;
        var shabaNoTag = shabaNo is not null ? AddHashTags(shabaNo) : string.Empty;
        string formattedDate = date?.Replace("/", "") ?? string.Empty;

        message =
            $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
            $"<b>پرداخت</b> {Environment.NewLine}{Environment.NewLine}" +
            $"#Date_{formattedDate} {Environment.NewLine}" +
            $"<b>طرف حساب :</b> {thirdParty} {thirdPartyTag} {Environment.NewLine}" +
            $"<b>شماره شبا :</b> {shabaNoTag} {Environment.NewLine}" +
            $"<b>شماره سند (شماره دستور پرداخت):</b> #PaymentOrderNumber_{paymentOrderNumber} {Environment.NewLine}" +
            $"<b>شماره پرداخت :</b> #Number_{number} {Environment.NewLine}" +
            $"<b>مبلغ :</b> {amount:#,##0.##} {Environment.NewLine}" +
            $"<b>تاریخ :</b> {time} {date} {Environment.NewLine}" +
            $"<b>بابت :</b> {defaultDescription} {Environment.NewLine}" +
            $"<b>توضیحات :</b> {description} {Environment.NewLine}";

        return message;
    }

    public static string UserChangedTelegramMessageModel(
        string? date,
        string? time,
        string? description,
        string? creator, CT ct)
    {
        string message = string.Empty;

        message =
                $"<b>اپراتور :</b> {creator}{Environment.NewLine}" +
                $"<b>تاریخ :</b> {time} {date} {Environment.NewLine}" +
                $"{description}{Environment.NewLine}";

        return message;
    }

    public static string SendEntryThroughRelocationForTemporaryDeliveryMessageModel(string? sourceWarehouseName,
        string? destinationWarehouseName,
        string? documentNumber,
        List<ProductInvoiceForTelegramModel>? products,
        string? requestNumber,
        string? projectOperation,
        string? createDate,
        string? createTime,
        string? exitDate,
        string? exitTime,
        string? creator,
        string? confirmCreator, CT ct)
    {
        string message = string.Empty;

        message =
                $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
                $"{TelegramValues.Icon7}<b>ورود بین انباری برای تحویل موقت</b> {Environment.NewLine}{Environment.NewLine}" +
                $"<b>انبار مبدا :</b> {sourceWarehouseName} {Environment.NewLine}" +
                $"<b>انبار مقصد :</b> {destinationWarehouseName} {Environment.NewLine}" +
                $"<b>شماره سند :</b> {documentNumber} {Environment.NewLine}" +
                $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ورود :</b> {exitTime} {exitDate} {Environment.NewLine}{Environment.NewLine}" +
                $"{TelegramValues.Icon13}<b>لیست کالاها :</b> {Environment.NewLine}";

        if (products != null && products.Any())
        {
            foreach (var product in products)
            {
                message +=
                    $"<b>{TelegramValues.BulletPoint}</b> {product.Name} {product.Quantity} {product.MeasureUnitName} {Environment.NewLine}";
            }
        }

        message +=
            $"{Environment.NewLine}<b>شماره درخواست :</b> {requestNumber} {Environment.NewLine}" +
            $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
            $"{TelegramValues.UserIcon}<b>ثبت کننده درخواست :</b> {creator} {Environment.NewLine}" +
            $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده درخواست :</b> {confirmCreator} {Environment.NewLine}";

        return message;
    }

    public static string SendExitRelocationForTemporaryDeliveryMessageModel(string? sourceWarehouseName,
        string? destinationWarehouseName,
        string? documentNumber,
        List<ProductInvoiceForTelegramModel>? products,
        string? requestNumber,
        string? projectOperation,
        string? createDate,
        string? createTime,
        string? exitDate,
        string? exitTime,
        string? creator,
        string? confirmCreator, CT ct)
    {
        string message = string.Empty;

        message =
               $"<b>سیستم ERP تاپ تک</b> {Environment.NewLine}" +
               $"{TelegramValues.Icon7}<b>خروج بین انباری برای تحویل موقت</b> {Environment.NewLine}{Environment.NewLine}" +
               $"<b>انبار مبدا :</b> {sourceWarehouseName} {Environment.NewLine}" +
               $"<b>انبار مقصد :</b> {destinationWarehouseName} {Environment.NewLine}" +
               $"<b>شماره سند :</b> {documentNumber} {Environment.NewLine}" +
               $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت خروج :</b> {exitTime} {exitDate} {Environment.NewLine}{Environment.NewLine}" +
               $"{TelegramValues.Icon13}<b>لیست کالاها :</b> {Environment.NewLine}";

        if (products != null && products.Any())
        {
            foreach (var product in products)
            {
                message +=
                    $"<b>{TelegramValues.BulletPoint}</b> {product.Name} {product.Quantity} {product.MeasureUnitName} {Environment.NewLine}";
            }
        }

        message +=
            $"{Environment.NewLine}<b>شماره درخواست :</b> {requestNumber} {Environment.NewLine}" +
            $"{TelegramValues.CalendarIcon}<b>تاریخ و ساعت ثبت :</b> {createTime} {createDate} {Environment.NewLine}{Environment.NewLine}" +
            $"{TelegramValues.UserIcon}<b>ثبت کننده درخواست :</b> {creator} {Environment.NewLine}" +
            $"{TelegramValues.GreenCheckMarkIcon}<b>تایید کننده درخواست :</b> {confirmCreator} {Environment.NewLine}";

        return message;
    }
}
