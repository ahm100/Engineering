using Engineering.Application.Services.Categories;
using Engineering.Application.Services.Categories.Models.ActiveCategory;
using Engineering.Application.Services.Categories.Models.CategoryGroupDelete;
using Engineering.Application.Services.Categories.Models.CodeCreator;
using Engineering.Application.Services.Categories.Models.CreateCategory;
using Engineering.Application.Services.Categories.Models.DisableCategory;
using Engineering.Application.Services.Categories.Models.GetCategoryByCode;
using Engineering.Application.Services.Categories.Models.GetCategoryById;
using Engineering.Application.Services.Categories.Models.GetCategoryByName;
using Engineering.Application.Services.Categories.Models.GetsActiveCategories;
using Engineering.Application.Services.Categories.Models.GetsByFilterData;
using Engineering.Application.Services.Categories.Models.GetsCategories;
using Engineering.Application.Services.Categories.Models.GetsCategoryExcelEnum;
using Engineering.Application.Services.Categories.Models.GetsCategoryExcelExporter;
using Engineering.Application.Services.Categories.Models.InactiveCategory;
using Engineering.Application.Services.Categories.Models.StateChangerCategories;
using Engineering.Application.Services.Categories.Models.UpdateCategory;

[Authorize]
[Route("api/engineering/v1/Category")]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryLogic _logic;

    public CategoryController(
        ILogger<CategoryController> logger,
        ICategoryLogic logic) : base()
    {
        _logger = logger;
        _logic = logic;
    }

    [HttpPost("AddCategory")]
    [ResponseSchema<CreateCategoryResponse>]
    public async Task<IResult> AddCategory(
        [FromBody] CreateCategoryRequest request, CT ct)
    {
        _logger.LogInformation("AddCategory");
        var result = await _logic.CreateCategory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CategoryCodeCreator")]
    [ResponseSchema<CategoryCodeCreatorResponse>]
    public async Task<IResult> CategoryCodeCreator(
        [FromBody] CategoryCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("CategoryCodeCreator");
        var result = await _logic.CodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CategoryGroupDelete")]
    [ResponseSchema<CategoryGroupDeleteResponse>]
    public async Task<IResult> CategoryGroupDelete(
        [FromBody] CategoryGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("CategoryGroupDelete");
        var result = await _logic.CategoryGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateCategories")]
    [ResponseSchema<StateChangerCategoriesResponse>]
    public async Task<IResult> ActivateCategories(
        [FromBody] ActivateCategoriesRequest request, CT ct)
    {
        _logger.LogInformation("ActivateCategories");
        var result = await _logic.StateChangerCategories(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateCategories")]
    [ResponseSchema<StateChangerCategoriesResponse>]
    public async Task<IResult> InactivateCategories(
        [FromBody] InactivateCategoriesRequest request, CT ct)
    {
        _logger.LogInformation("InactivateCategories");
        var result = await _logic.StateChangerCategories(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditCategory")]
    [ResponseSchema<UpdateCategoryResponse>]
    public async Task<IResult> EditCategory(
        [FromBody] UpdateCategoryRequest request, CT ct)
    {
        _logger.LogInformation("EditCategory");
        var result = await _logic.UpdateCategory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveCategory")]
    [ResponseSchema<ActiveCategoryResponse>]
    public async Task<IResult> ActiveCategory(
        [FromBody] ActiveCategoryRequest request, CT ct)
    {
        _logger.LogInformation("ActiveCategory");
        var result = await _logic.ActiveCategory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveCategory")]
    [ResponseSchema<InactiveCategoryResponse>]
    public async Task<IResult> InactiveCategory(
        [FromBody] InactiveCategoryRequest request, CT ct)
    {
        _logger.LogInformation("InactiveCategory");
        var result = await _logic.InactiveCategory(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCategoryById")]
    [ResponseSchema<GetCategoryByIdResponse>]
    public async Task<IResult> GetCategoryById(
        [FromQuery] GetCategoryByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetCategoryById");
        var result = await _logic.GetCategoryById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCategoryByName")]
    [ResponseSchema<GetCategoryByNameResponse>]
    public async Task<IResult> GetCategoryByName(
        [FromQuery] GetCategoryByNameRequest request, CT ct)
    {
        _logger.LogInformation("GetCategoryByName");
        var result = await _logic.GetCategoryByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCategoryByCode")]
    [ResponseSchema<GetCategoryByCodeResponse>]
    public async Task<IResult> GetCategoryByCode(
        [FromQuery] GetCategoryByCodeRequest request, CT ct)
    {
        _logger.LogInformation("GetCategoryByCode");
        var result = await _logic.GetCategoryByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveCategory")]
    [ResponseSchema<GetsActiveCategoriesResponse>]
    public async Task<IResult> GetsActiveCategory(
        [FromQuery] GetsActiveCategoriesRequest request, CT ct)
    {
        _logger.LogInformation("GetsActiveCategory");
        var result = await _logic.GetsActiveCategories(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCategory")]
    [ResponseSchema<GetsCategoriesResponse>]
    public async Task<IResult> GetsCategory(
        [FromQuery] GetsCategoriesRequest request, CT ct)
    {
        _logger.LogInformation("GetsCategory");
        var result = await _logic.GetsCategories(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByFilterData")]
    [ResponseSchema<GetsByFilterDataResponse>]
    public async Task<IResult> GetsByFilterData(
        [FromQuery] GetsByFilterDataRequest request, CT ct)
    {
        _logger.LogInformation("GetsByFilterData");
        var result = await _logic.GetsByFilterData(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCategoryExcelEnum")]
    [ResponseSchema<GetsCategoryExcelEnumResponse>]
    public async Task<IResult> GetsCategoryExcelEnum(
        [FromQuery] GetsCategoryExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("GetsCategoryExcelEnum");
        var result = await _logic.GetsCategoryExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsCategoryExcelExporter")]
    [ResponseSchema<GetsCategoryExcelExporterResponse>]
    public async Task<IResult> GetsCategoryExcelExporter(
        [FromBody] GetsCategoryExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("GetsCategoryExcelExporter");
        var result = await _logic.GetsCategoryExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableCategory")]
    [ResponseSchema<DisableCategoryResponse>]
    public async Task<IResult> DisableCategory(
        [FromQuery] DisableCategoryRequest request, CT ct)
    {
        _logger.LogInformation("DisableCategory");
        var result = await _logic.DisableCategory(request, ct);
        return result.GetHttpResponse();
    }
}