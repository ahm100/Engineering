namespace Engineering.Application.WebServices.MetaDataServices.Skills.Models;

public record SkillDto(long Id,
                    string? Name,
                    string? Code,
                    bool IsActive);
