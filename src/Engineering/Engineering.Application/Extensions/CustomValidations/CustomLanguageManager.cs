using FluentValidation.Resources;
using Microsoft.Extensions.Localization;

namespace Engineering.Application.Extensions.CustomValidations;

public class CustomLanguageManager : LanguageManager
{
    public CustomLanguageManager(IStringLocalizer validationLocalizer)
    {
        AddTranslation("fa", "UniqueValidator", validationLocalizer["Validation.UniqueValidator"]);
        AddTranslation("en", "UniqueValidator", validationLocalizer["Validation.UniqueValidator"]);
    }
}