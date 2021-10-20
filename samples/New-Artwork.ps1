# --------------------------------------------------------------
# The script is an example of how to use the module.
# --------------------------------------------------------------

# An Azure Application Insights account is required. 
# To auth you need the instrumentation key as guid.
$InstrumentationKey = "80b0f5c3-0b08-4e97-9e6b-0f2df19cc01d"

# Installs the module from powershell gallery.
Install-Module AppInsight

# Optionally the key can be loaded into the session during the import. 
# Attention: The key is only valid as long as the session exists.
Import-Module AppInsights -ArgumentList $InstrumentationKey 

# As alternative its possible to set the key with a environment variable directly.
$env:AI_INSTRUMENTATION_KEY = "80b0f5c3-0b08-4e97-9e6b-0f2df19cc01d"

function New-Artwork {
    param (
        [String] $Title,
        [String] $Author
    )
    $StartTime = (Get-Date)

    # This sample sends a message with two properties to application insights. 
    # The command "New-Artwork" as source is also recorded.
    Send-AppInsightsTrace -Message "New Artwork $Title by $Author." -Properties @{ Title = $Title; Author = $Author }

    Write-Host "Title = $Title"
    Write-Host "Author = $Author"

    Get-Brush -Type "Coal"
    Set-Color -Color "Blue"

    Start-Sleep 5

    $Duration = ((Get-Date) - $StartTime).Seconds
    Send-AppInsightsEvent -Name "ArtworkCreated" -Properties @{ Title = $Title; Author = $Author } -Metrics { Duration = $Duration }

    Send-AppInsightsException -Exception ([System.InvalidOperationException]::("Work of art has gone bad.")) -Message "Work of art has gone bad."
}

function Get-Brush {
    param (
        [String] $Type
    )
    $StartTime = (Get-Date)

    Send-AppInsightsTrace -Message "Using Brush $Type." -Properties @{ BrushType = $Type; } -Severity Information 
    
    Start-Sleep 2

    $Duration = ((Get-Date) - $StartTime).Seconds
    Send-AppInsightsEvent -Name "BrushRetrieved" -Properties @{ Type = $Type } -Metrics { Duration = $Duration }
}

function Set-Color {
    param (
        [String] $Color
    )
    $StartTime = (Get-Date)
    Send-AppInsightsTrace -Message "Set Color $Color." -Properties @{ Color = $Color } -Severity Information 

    Write-Host "Color = $Color"

    Start-Sleep 2

    $Duration = ((Get-Date) - $StartTime).Seconds
    Send-AppInsightsEvent -Name "ColorSelected" -Properties @{ Color = $Color } -Metrics { Duration = $Duration }
}

function Start-Paint {
    $StartTime = (Get-Date)
    Send-AppInsightsTrace -Message "Start Paint." -Severity Critical 

    Write-Host "Start Paint."

    Start-Sleep 2

    $Duration = ((Get-Date) - $StartTime).Seconds
    Send-AppInsightsEvent -Name "PaintingStarted" -Properties @{ Color = $Color } -Metrics { Duration = $Duration }
}

New-Artwork -Title "MyArtwork" -Author "Sven" 