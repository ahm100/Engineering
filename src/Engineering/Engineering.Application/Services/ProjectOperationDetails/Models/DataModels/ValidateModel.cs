
namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels;

/// <summary>
/// مدلی برای ولیدیت شرح عملیات پروژه و موقعیت جزئی در ثبت ویزارد ریزمتره
/// </summary>
/// <param name="ProjectOperationId">شناسه شرح عملیات</param>
/// <param name="OperationLocationId">شناسه موقعیت جزئی</param>
public record ValidateModel(
    long ProjectOperationId,
    long OperationLocationId
    );


