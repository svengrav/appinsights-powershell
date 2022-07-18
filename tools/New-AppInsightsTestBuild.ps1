# Script for creating the assemblies and help
# 1. ----------------------------------------------------------
Set-Location D:\Repositories\powershell-app-insights\

$Module = 'AppInsights'

# Build module
dotnet build .\src\AppInsights -o .\output\$Module\bin

# 2. ----------------------------------------------------------
# Run tests
dotnet test .\src\AppInsights.Test 

# 3. ----------------------------------------------------------
# Create module
New-Item -Path .\output\$Module\ -ItemType Directory -Force
Copy-Item .\src\$Module.psd1 .\output\$Module\ -Force
Copy-Item .\src\$Module.psm1 .\output\$Module\ -Force

# 4. ----------------------------------------------------------
# Import module and invoke samples in a new powershell instance

$Script =  { 
	Import-Module ".\output\AppInsights\AppInsights.psd1" -Force 
    . ".\samples\Invoke-CommandSamples.ps1"
 }

Start-Process powershell -ArgumentList "-NoExit $Script"
