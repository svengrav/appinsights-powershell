---
external help file: AppInsights.dll-Help.xml
Module Name: AppInsights
online version:
schema: 2.0.0
---

# Send-AppInsightsRequest

## SYNOPSIS
PowerShell command used to track requests in application insights.

## SYNTAX

```
Send-AppInsightsRequest [-Name] <String> [-Id <String>] -Duration <TimeSpan> [-Timestamp <DateTimeOffset>]
 -ResponseCode <String> [-Source <String>] [-Url <String>] [-Success <Boolean>] [-Metrics <Hashtable>]
 [[-InstrumentationKey] <Guid>] [[-Properties] <Hashtable>] [-RoleName <String>] [-RoleInstance <String>]
 [-ContextLevel <Int32>] [-DisableContext] [<CommonParameters>]
```

## DESCRIPTION
PowerShell command used to track requests in application insights.

## EXAMPLES

### Example 1
```powershell
Send-AppInsightsRequest -Name "AppleRequested" -Duration (New-TimeSpan -Seconds 5) -StartTime (Get-Date) -ResponseCode OK -Success $true -InstrumentationKey $env:AI_INSTRUMENTATION_KEY
```

## PARAMETERS

### -Duration
Request duration.

```yaml
Type: TimeSpan
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -InstrumentationKey
The Application Insights Instrumentation Key.

```yaml
Type: Guid
Parameter Sets: (All)
Aliases:

Required: False
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
Name of the request represents code path taken to process the request.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Properties
Optional dictionary with custom properties.

```yaml
Type: Hashtable
Parameter Sets: (All)
Aliases:

Required: False
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResponseCode
Result of a request execution.
HTTP status code for HTTP requests.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RoleInstance
Defines whether the process was successfully processed.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RoleName
Defines whether the process was successfully processed.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Success
Defines whether the process was successfully processed.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Metrics
Optional dictionary with custom metrics.

```yaml
Type: Hashtable
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Id
The Request ID. Default is emtpy.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Timestamp
The datetime when telemetry was recorded. Default is UTC.Now.

```yaml
Type: DateTimeOffset
Parameter Sets: (All)
Aliases: StartTime

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ContextLevel
Defines which level in the call stack is taken into account for the command context.

```yaml
Type: Int32
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DisableContext
Disables the capturing for the PowerShell command context. For instance, if sensitive data would be captured.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Source
The source for the request telemetry.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Url
The url for the request telemetry.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS
