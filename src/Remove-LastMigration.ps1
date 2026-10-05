# Run for delete => .\Remove-LastMigration.ps1

param (
    [string]$ProjectPath = "Engineering/Engineering.Persistence/Engineering.Persistence.csproj",
    [string]$StartupProjectPath = "Engineering/Engineering.Api/Engineering.Api.csproj",
    [string]$Context = "Engineering.Persistence.EngineeringDBContext",
    [string]$ScriptFolder = "Engineering/Engineering.Persistence/Scripts",
    [string]$MigrationsFolder = "Engineering/Engineering.Persistence/Migrations"
)

$migrationFiles = Get-ChildItem -Path $MigrationsFolder -Filter "*_*.cs" | Sort-Object LastWriteTime -Descending
if ($migrationFiles.Count -eq 0) {
    Write-Error "No migration files found to remove."
    exit 1
}

$latestMigrationFile = $migrationFiles[0]
$migrationName = $latestMigrationFile.BaseName -replace '^\d+_', ''
Write-Host "Last migration detected: $migrationName"

$removeArgs = "ef migrations remove --project `"$ProjectPath`" --startup-project `"$StartupProjectPath`" --context $Context --configuration Debug --force"
Write-Host "Removing migration using EF CLI..."
$removeProcess = Start-Process -NoNewWindow -FilePath "dotnet" -ArgumentList $removeArgs -Wait -PassThru

if ($removeProcess.ExitCode -ne 0) {
    Write-Error "Failed to remove migration."
    exit 1
}

$sqlFile = Get-ChildItem -Path $ScriptFolder -Filter "*-$migrationName.sql" | Select-Object -First 1
if ($sqlFile) {
    Remove-Item $sqlFile.FullName
    Write-Host "Removed SQL script: $($sqlFile.Name)"
} else {
    Write-Warning "No matching SQL file found for migration '$migrationName'"
}

Write-Host "Migration and corresponding SQL script removed successfully."
