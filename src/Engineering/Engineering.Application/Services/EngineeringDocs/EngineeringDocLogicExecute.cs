using Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDoc;
using Engineering.Application.Services.EngineeringDocs.Contracts.CreateProjectDocHistory;
using Engineering.Application.Services.EngineeringDocs.Contracts.DeleteProjectDoc;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDiscipline;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDoc;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetDisciplineDocType;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocById;
using Engineering.Application.Services.EngineeringDocs.Contracts.GetProjectDocs;
using Engineering.Application.Services.EngineeringDocs.Contracts.ProjectDocCodeGenerator;
using Engineering.Application.Services.EngineeringDocs.Contracts.UpdateProjectDoc;
using Engineering.Domain.Entities.EngineeringDocs;

namespace Engineering.Application.Services.EngineeringDocs;

public partial class EngineeringDocLogic
{
    public async Task<Result> DocTypeSeederCommand(
        CT ct)
    {
        try
        {
            await _disciplineRepository.Seeder(ct);
            await _unitOfWork.CommitAsync(ct); // because needed to save for their usage inside SeedEngineeringDocs
            await _disciplineDocRepository.Seeder(ct);
            await _unitOfWork.CommitAsync(ct);
            await _disciplineDocTypeRepository.SeedEngineeringDocs(ct);
            await _unitOfWork.CommitAsync(ct);
            return Result.Success();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DisciplineDocType>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetDisciplineResponse?>> GetDisciplineCommand(
        GetDisciplineRequest request, CT ct)
    {
        try
        {
            var result = await _disciplineRepository.GetDiscipline(
                request.PageIndex,
                request.PageSize,
                ct);

            return new GetDisciplineResponse(
                result.Data,
                result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetDisciplineResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetDisciplineDocResponse?>> GetDisciplineDocCommand(
        GetDisciplineDocRequest request, CT ct)
    {
        try
        {
            var result = await _disciplineDocRepository.GetDisciplineDoc(
                request.PageIndex,
                request.PageSize,
                ct);

            return new GetDisciplineDocResponse(
                result.Data,
                result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetDisciplineDocResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetDisciplineDocTypeResponse?>> GetDisciplineDocTypeCommand(
        GetDisciplineDocTypeRequest request, CT ct)
    {
        try
        {
            var result = await _disciplineDocTypeRepository.GetDisciplineDocType(
                request.DisciplineId,
                request.PageIndex,
                request.PageSize,
                ct);

            return new GetDisciplineDocTypeResponse(
                result.Data,
                result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetDisciplineDocTypeResponse>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<ProjectDoc?>> CreateProjectDocCommand(
        CreateProjectDocRequest request, CT ct)
    {
        try
        {
            var sequence = await _projectDocRepository.GetNextSequence(
                request.ProjectId,
                request.DisciplineId,
                request.DisciplineDocId,
                ct);

            var codeData = await _projectDocRepository.GetProjectDocCodeData(
                request.ProjectId,
                request.DisciplineId,
                request.DisciplineDocId,
                ct);

            if (codeData is null)
                return Result.Failure<ProjectDoc>(GlobalErrors.InValidRequest);

            var create = new ProjectDoc(
                request.ProjectId,
                request.DisciplineId,
                request.DisciplineDocId,
                request.ThirdPartyId,
                request.Url);

            create.SetRevision(1);
            create.SetSequence(sequence);

            create.SetCode(
                codeData.Value.ProjectCode,
                codeData.Value.DisciplineCode,
                codeData.Value.DisciplineDocCode,
                sequence);

            await _projectDocRepository.Create(create, ct);
            await _unitOfWork.CommitAsync(ct);

            return create;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectDoc>(SharedErrors.UnknownError);
        }
    }

    public async Task<Result<UpdateProjectDocResponse?>> UpdateProjectDocCommand(
        UpdateProjectDocRequest request, CT ct)
    {
        try
        {
            var entity = await _projectDocRepository
             .GetProjectDocEntityById(request.Id, ct);

            if (entity is null)
                return Result.Failure<UpdateProjectDocResponse>(
                    GlobalErrors.InValidRequest);

            entity.Update(
                request.DisciplineId,
                request.DisciplineDocId,
                request.ThirdPartyId,
                request.Url);

            entity.SetRevision(entity.Revision + 1);

            var codeData = await _projectDocRepository.GetProjectDocCodeData(
               entity.ProjectId,
               request.DisciplineId,
               request.DisciplineDocId,
               ct);

            if (codeData is null)
                return Result.Failure<UpdateProjectDocResponse>(
                    GlobalErrors.InValidRequest);

            entity.SetCode(
                codeData.Value.ProjectCode,
                codeData.Value.DisciplineCode,
                codeData.Value.DisciplineDocCode,
                entity.Sequence);

            await _projectDocRepository.Update(entity);
            var history = new ProjectDocHistory(entity);
            await _projectDocHistoryRepository.Create(history, ct);
            await _unitOfWork.CommitAsync(ct);
            return new UpdateProjectDocResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<UpdateProjectDocResponse>(
                SharedErrors.UnknownError);
        }
    }

    public async Task<Result<DeleteProjectDocResponse?>> DeleteProjectDocCommand(
        DeleteProjectDocRequest request, CT ct)
    {
        try
        {
            var entity = await _projectDocRepository.GetProjectDocEntityById(request.Id, ct);
            if (entity is null)
                return Result.Failure<DeleteProjectDocResponse>(
                    GlobalErrors.InValidRequest);

            entity.SoftDelete();

            await _projectDocRepository.Update(entity);
            return new DeleteProjectDocResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DeleteProjectDocResponse>(
                SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetProjectDocByIdResponse?>> GetProjectDocByIdCommand(
        GetProjectDocByIdRequest request, CT ct)
    {
        try
        {
            var result = await _projectDocRepository.GetProjectDocById(
                request.Id,
                ct);

            if (result is null)
                return Result.Failure<GetProjectDocByIdResponse>(
                    GlobalErrors.InValidRequest);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectDocByIdResponse>(
                SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetProjectDocsResponse>> GetProjectDocsCommand(
        GetProjectDocsRequest request, CT ct)
    {
        try
        {
            var result = await _projectDocRepository.GetProjectDocs(
                request.ProjectId,
                request.DisciplineId,
                request.DisciplineDocId,
                request.PageIndex,
                request.PageSize,
                ct);

            if (!result.Data.Any())
                return Result.Failure<GetProjectDocsResponse>(
                    GlobalErrors.InValidRequest)!;

            return new GetProjectDocsResponse(
                result.Data,
                result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetProjectDocsResponse>(
                SharedErrors.UnknownError)!;
        }
    }

    public async Task<Result<ProjectDocCodeGeneratorResponse?>> ProjectDocCodeGeneratorCommand(
      ProjectDocCodeGeneratorRequest request,
      CT ct)
    {
        try
        {
            var sequence = await _projectDocRepository.GetNextSequence(
                request.ProjectId,
                request.DisciplineId,
                request.DisciplineDocId,
                ct);

            var result = await _projectDocRepository.GetProjectDocCodeData(
                request.ProjectId,
                request.DisciplineId,
                request.DisciplineDocId,
                ct);

            if (result is null)
                return Result.Failure<ProjectDocCodeGeneratorResponse>(
                    GlobalErrors.InValidRequest);

            var code = $"{result.Value.ProjectCode}-{result.Value.DisciplineCode}-{result.Value.DisciplineDocCode}-{sequence:D3}-R001";

            return new ProjectDocCodeGeneratorResponse(code);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return Result.Failure<ProjectDocCodeGeneratorResponse>(
                SharedErrors.UnknownError);
        }
    }

    public async Task<Result<GetProjectDocHistoriesResponse>> GetProjectDocHistoriesCommand(
    GetProjectDocHistoriesRequest request,
    CT ct)
    {
        try
        {
            var result = await _projectDocHistoryRepository.GetProjectDocHistories(
                request.ProjectDocId,
                request.PageIndex,
                request.PageSize,
                ct);

            return new GetProjectDocHistoriesResponse(
                result.Data,
                result.RowCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            return Result.Failure<GetProjectDocHistoriesResponse>(
                SharedErrors.UnknownError);
        }
    }

}
