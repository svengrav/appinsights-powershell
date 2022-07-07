# Script for creating the assemblies and help
# 1. ----------------------------------------------------------
Set-Location D:\Repositories\powershell-app-insights\

$Module = 'AppInsights'
$Version = [System.Version]::new(1,0,0)

# Build module
dotnet build .\src\AppInsights -o .\output\$Module\bin

# Update version and manifest
New-Item -Path .\output\$Module\ -ItemType Directory -Force
Copy-Item .\src\$Module.psd1 .\output\$Module\ -Force
Copy-Item .\src\$Module.psm1 .\output\$Module\ -Force

Update-ModuleManifest .\Output\$Module\$Module.psd1 -ModuleVersion $Version

# 2. ----------------------------------------------------------
# Run tests
dotnet test .\src\AppInsights.Test 

# 3. ----------------------------------------------------------
# Create documentation an help files with platyps
# https://docs.microsoft.com/de-de/powershell/scripting/dev-cross-plat/create-help-using-platyps?view=powershell-7.1
# Install-Module -Name platyPS -Scope CurrentUser
Import-Module -Name platyPS

Import-Module ".\output\AppInsights\AppInsights.psd1" -Force 

# New-MarkdownHelp -Module AppInsights -OutputFolder .\docs
Update-MarkdownHelp -Path .\docs

New-ExternalHelp -Path .\docs -OutputPath .\output\AppInsights\bin -Force

# 4. ----------------------------------------------------------
# Publish module

Publish-Module -Path .\output\$Module -NuGetApiKey $NuGetApiKey -Verbose

$Script =  { 
	Import-Module ".\output\AppInsights\AppInsights.psd1" -Force 
 }

Start-Process powershell -ArgumentList "-NoExit $Script"