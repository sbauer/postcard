[CmdletBinding(PositionalBinding=$false)]
param(
    [string] $Version,
    [string] $BuildNumber,
    [bool] $CreatePackages = $true,
    [bool] $RunTests = $true
)

Write-Host "Starting build process for Postcard"
$VersionFile = "$PSScriptRoot/version.txt"
$artifactsDirectory = "$PSScriptRoot/artifacts"
$projectsToBuild = 'Postcard', 'Postcard.Microsoft.DependencyInjection','Renderers\Postcard.Renderers.Razor','Senders\Postcard.Senders.Smtp'
$testProjects = 'Postcard.Tests','Postcard.Tests.Renderers.Razor'

Write-Host "$CreatePackages"
function GetVersion() {
    
    if ($Version) {
        return $Version
    }

    $versionToUse = ''

    if (Test-Path $VersionFile) {
        $versionToUse = Get-Content $VersionFile
    }

    if (!$versionToUse) {
        Write-Host "version information was not found - $VersionFile"
        Exit 1
    }

    if ($versionToUse -match "-") {
        return "$versionToUse-$BuildNumber"
    } else {
        return "$versionToUse"
    }
}

if ($BuildNumber -and $BuildNumber.Length -lt 5) {
    $BuildNumber = $BuildNumber.PadLeft(5, "0")
}

$productVersion = $(GetVersion)
Write-Host "Version: $productVersion"
Write-Host "BuildNumber: $BuildNumber"
Write-Host "CreatePackages: $CreatePackages"
Write-Host "RunTests: $RunTests" 

if ($RunTests) {   
    Write-Host "Preparing to run tests.."
    dotnet restore
    foreach ($project in $testProjects) {
        Write-Host "Running tests for project: $project "
        Push-Location ".\tests\$project"

        dotnet test

        if ($LastExitCode -ne 0) { 
            Write-Host "Test failures";
            Pop-Location
            Exit 1
        }

        Write-Host "All tests passed!" 
	    Pop-Location
    }
}

if ($CreatePackages) {
    New-Item -ItemType Directory -Force -Path $artifactsDirectory
    Get-ChildItem $artifactsDirectory | Remove-Item
}

foreach ($project in $projectsToBuild) {
    Write-Host "Building $project`:"
	
	Push-Location ".\src\$project"

    $projectVersion = $productVersion
    $targets = "Restore"
    
    if ($CreatePackages) {
        $targets += ";Pack"
    }

	Write-Host "$project... Version: $projectVersion"

	dotnet msbuild "/t:$targets" "/p:Configuration=Release" "/p:Version=$projectVersion" "/p:PackageOutputPath=$artifactsDirectory"

	Pop-Location

    Write-Host "Project built"
}

Write-Host "Finished"