namespace Engineering.Application.Services.Adjustments.Contracts.DeleteAdjustmentIndex;


public record DeleteAdjustmentIndexRequest(long Id);

public record DeleteAdjustmentIndexResponse(
    bool IsSuccess);