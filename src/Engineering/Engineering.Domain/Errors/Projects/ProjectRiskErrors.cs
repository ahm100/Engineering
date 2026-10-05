namespace Engineering.Domain.Errors.Projects;

public static class ProjectRiskErrors
{

    public static Error ProjectRiskWithFilterNotFound = new("NotFound", "هیچ ریسک پروژه ای با این فیلترها یافت نشد.", 204);
    public static Error ProjectRiskWithIdNotFound = new("NotFound", "هیچ ریسک پروژه ای با این شناسه یافت نشد.", 204);
    public static Error ProjectRiskWithProjectIdNotFound = new("NotFound", "هیچ ریسک پروژه ای با این شناسه پروژه یافت نشد.", 204);
}