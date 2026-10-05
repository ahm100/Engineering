using System.ComponentModel;

namespace Engineering.Application.Services.TelegramChats.TelegramServices;

public static class TelegramValues
{
    public const string BotUrl = "https://mapi.sattec.ir/api/Telegram";
    public const string APIKEY = "*PT7XzZ6)banHKXRpaFpBi-Jr&H)tL5$";
    public const string APIName = "X-API-KEY";
    public const string APIName2 = "Authorization";

    //ChatIds
    public const string ProjectOperationDetailChatId = "-4584394379";
    public const string DailyProjectOperationChatId = "-4584394379";
    public const string RequestGoodsSupplyChatId = "-4584394379";

    //Icons
    public const string AcceptedIcon = "\U0001f533";
    public const string RejectedIcon = "\U0001f534";
    public const string Icon1 = "\u26A0\uFE0F";
    public const string Icon2 = "\u2B05\uFE0F";
    public const string Icon3 = "\u2199\uFE0F";
    public const string Icon4 = "\u2757";
    public const string Icon5 = "\u2705";
    public const string Icon6 = "\U0001f7e2";
    public const string Icon7 = "\U0001f534";
    public const string Icon8 = "\U0001f4a0";
    public const string Icon9 = "\U0001f50a";
    public const string Icon10 = "\u260E\uFE0F";
    public const string Icon11 = "\U0001f4b5";
    public const string Icon12 = "\U0001f4b0";
    public const string Icon13 = "\U0001f4dd";
    public const string Icon14 = "\u2600\uFE0F";
    public const string Icon15 = "\u26C5";
    public const string Icon16 = "\u2601\uFE0F";
    public const string Icon17 = "\u26C8\uFE0F";
    public const string Icon18 = "\U0001f326\uFE0F";
    public const string Icon19 = "\u2744\uFE0F";
    public const string CheckIcon = "\u2714";
    public const string ClockIcon = "\U0001F553";
    public const string CalendarIcon = "\U0001F4C5";
    public const string BulletPoint = "\u2022";
    public const string BoxEmoji = "\U0001F4E6";
    public const string MemoIcon = "\U0001F4DD";
    public const string BellIcon = "\U0001F514";
    public const string AddIcon = "\u2795";
    public const string EditIcon = "\u270F";
    public const string UserIcon = "\U0001F464";
    public const string TruckIcon = "\U0001F69A";
    public const string SendBackIcon = "\U0001F4E4";
    public const string GreenCheckMarkIcon = "\u2705";

}

public enum WeatherConditionType
{
    [Description("آفتابی")]
    Sunny,
    [Description("ابری")]
    Cloudy,
    [Description("بارانی")]
    Rainy,
    [Description("طوفانی")]
    Stormy,
    [Description("برفی")]
    Snowy,
    [Description("بادی")]
    Windy,
    [Description("مه‌آلود")]
    Foggy,
    [Description("تگرگی")]
    Hail,
    [Description("رعد و برق")]
    Thunderstorm,
    [Description("نم‌نم باران")]
    Drizzle
}
