namespace Engineering.Application.Services.CostCenterVirtualGroups.Models.UpdateCostCenterVirtualGroup;

public record UpdateCostCenterVirtualGroupRequest(long CostCenterVirtualGroupId,
                                                  string Title,
                                                  string Link,
                                                  string Identifier,
                                                  string? Description,
                                                  bool SendToday,
                                                  TimeSpan? TodayTime,
                                                  bool SendYesterday,
                                                  TimeSpan? YesterdayTime) : IHttpRequest;
