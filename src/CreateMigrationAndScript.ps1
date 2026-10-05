#Run to create a migration => .\CreateMigrationAndScript.ps1 -MigrationName "MigName"

param (
    [string]$MigrationName,
    [string]$ProjectPath = "Engineering/Engineering.Persistence/Engineering.Persistence.csproj",
    [string]$StartupProjectPath = "Engineering/Engineering.Api/Engineering.Api.csproj",
    [string]$Context = "Engineering.Persistence.EngineeringDBContext"
)

function Show-ProgressBar($currentStep, $totalSteps) {
    $percent = [math]::Round(($currentStep / $totalSteps) * 100)
    $completed = "#" * ($percent / 2)
    $remaining = "-" * (50 - ($percent / 2))
    $bar = "$completed$remaining"
    Write-Host "Doing job well: $bar $percent%"
}

function Execute-DotnetCommand($arguments, $errorMessage) {
    $output = & dotnet $arguments 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Host ""
        Write-Host "$errorMessage" -ForegroundColor Red
        Write-Host "----------------------------------------" -ForegroundColor Red
        $output | ForEach-Object { Write-Host $_ -ForegroundColor Yellow }
        Write-Host "----------------------------------------" -ForegroundColor Red
        exit 1
    }
    return $output
}

$totalSteps = 1
$currentStep = 0
Show-ProgressBar $currentStep $totalSteps
# =============================
# Step 1 - Add Migration
# =============================

Write-Host "Adding new migration: $MigrationName ..."

$addMigrationArgs = @(
    "ef", "migrations", "add", $MigrationName,
    "--project", $ProjectPath,
    "--startup-project", $StartupProjectPath,
    "--context", $Context,
    "--configuration", "Debug"
)

Execute-DotnetCommand $addMigrationArgs "Failed to add migration."

$currentStep++
Show-ProgressBar $currentStep $totalSteps
Write-Host "Migration added successfully."
