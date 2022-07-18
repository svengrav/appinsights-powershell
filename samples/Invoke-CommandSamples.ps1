# Traces
Send-AppInsightsTrace -Message "Fruit selected"
Send-AppInsightsTrace -Message "Fruit selected" -Severity Critical -Properties @{ "Fruit" = "Apple" } -RoleName "My Custom Role"
Send-AppInsightsTrace -Message "Apples are delicious" -Severity Information -InstrumentationKey $env:AI_INSTRUMENTATION_KEY
Send-AppInsightsTrace -Message "Apples are delicious" -Severity Information -InstrumentationKey $env:AI_INSTRUMENTATION_KEY -CaptureCommand

# Exceptions
Send-AppInsightsException  -Exception ([Exception]::new("Fruit is rotten")) 
Send-AppInsightsException  -Exception ([Exception]::new("Fruit is rotten")) -Message "It is an apple" -Properties @{ "Fruit" = "Apple" } -Metrics @{ "Weight" = 12 } 
Send-AppInsightsException  -Exception ([Exception]::new("Fruit is rotten")) -Message "Carefull" -Properties @{ "Fruit" = "Apple";  "Type" = "Granny Smith" } -InstrumentationKey $env:AI_INSTRUMENTATION_KEY

# Events
Send-AppInsightsEvent "AppleOrderd" 
Send-AppInsightsEvent -EventName "AppleOrderd" -Properties @{ "Fruit" = "Apple";  "Type" = "Granny Smith" } -InstrumentationKey $env:AI_INSTRUMENTATION_KEY
Send-AppInsightsEvent -EventName "OrangeOrderd" -Properties @{ "Fruit" = "Orange";  "Type" = "Granny Smith" } -Metrics @{ "Weight" = 12 }  -Timestamp (Get-Date)
Send-AppInsightsEvent -EventName "OrangeOrderd" -CaptureLevel 2 -CaptureCommand

# Request
Send-AppInsightsRequest -Name "AppleRequested" -Duration (New-TimeSpan -Seconds 5) -StartTime (Get-Date) -ResponseCode OK -Success $true -InstrumentationKey $env:AI_INSTRUMENTATION_KEY
Send-AppInsightsRequest -Name "AppleRequested" -Duration (New-TimeSpan -Hours 1) -StartTime (Get-Date) -ResponseCode OK -Success $true -Metrics @{ "Weight" = 12 } 
Send-AppInsightsRequest -Name "AppleRequested" -Duration (New-TimeSpan -Seconds 30) -StartTime (Get-Date) -ResponseCode OK -Success $false -Properties @{ "Fruit" = "Apple" } 

# Dependencies
Send-AppInsightsDependency -Type "FruitDependency" -Name "Apple" -Target "AppleStore" -Data "Apple Order" -StartTime (Get-Date) -Duration (New-TimeSpan -Seconds 30) -ResultCode OK
Send-AppInsightsDependency -Type "FruitDependency" -Name "Apple" -Target "AppleStore" -Data "Apple Order" -StartTime (Get-Date) -Success $true -Duration (New-TimeSpan -Seconds 30) -ResultCode OK
Send-AppInsightsDependency -Type "FruitDependency" -Name "Apple" -Target "AppleStore" -Data "Apple Order" -StartTime (Get-Date) -Success $false -Duration (New-TimeSpan -Seconds 30) -ResultCode OK

# Availability
Send-AppInsightsAvailability -Name "Apple Availability" -Id "AppleID" -Timestamp (Get-Date) -Duration (New-TimeSpan -Seconds 5) -RunLocation $env:COMPUTERNAME -Message "Apple!" -Success $true 
Send-AppInsightsAvailability -Name "Apple Availability" -Id "AppleID" -Timestamp (Get-Date) -Duration (New-TimeSpan -Seconds 5) -RunLocation $env:COMPUTERNAME -Message "Apple!" -Success $true 