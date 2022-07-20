---
external help file: AppInsights.dll-Help.xml
Module Name: AppInsights
online version:
schema: 2.0.0
---

# Send-AppInsightsException

## SYNOPSIS
PowerShell command used to track exceptions in application insights.

## SYNTAX

### Exception
```
Send-AppInsightsException [-Exception] <Exception> [[-Message] <String>] [[-Metrics] <Hashtable>]
 [[-Timestamp] <DateTimeOffset>] [[-Severity] <SeverityLevel>] [[-ProblemId] <String>]
 [[-InstrumentationKey] <Guid>] [[-Properties] <Hashtable>] [[-RoleName] <String>] [[-RoleInstance] <String>]
 [-DeveloperMode] [<CommonParameters>]
```

### ErrorRecord
```
Send-AppInsightsException -ErrorRecord <ErrorRecord> [[-Message] <String>] [[-Metrics] <Hashtable>]
 [[-Timestamp] <DateTimeOffset>] [[-Severity] <SeverityLevel>] [[-ProblemId] <String>]
 [[-InstrumentationKey] <Guid>] [[-Properties] <Hashtable>] [[-RoleName] <String>] [[-RoleInstance] <String>]
 [-DeveloperMode] [<CommonParameters>]
```

### CaptureCommand
```
Send-AppInsightsException [[-Message] <String>] [[-Metrics] <Hashtable>] [[-Timestamp] <DateTimeOffset>]
 [[-Severity] <SeverityLevel>] [[-ProblemId] <String>] [[-InstrumentationKey] <Guid>]
 [[-Properties] <Hashtable>] [[-RoleName] <String>] [[-RoleInstance] <String>] [[-CaptureLevel] <Int32>]
 [-CaptureCommand] [-DeveloperMode] [<CommonParameters>]
```

## DESCRIPTION
PowerShell command used to track exceptions in application insights.

## EXAMPLES

### Example 1
```powershell
Send-AppInsightsException -Exception ([Exception]::new("Fruit is rotten")) -Message "It is an apple" -Properties @{ "Fruit" = "Apple" } -Metrics @{ "Weight" = 12 } -Severity Information
```

## PARAMETERS

### -CaptureCommand
Disables the capturing for the PowerShell command context. For instance, if sensitive data would be captured.

```yaml
Type: SwitchParameter
Parameter Sets: CaptureCommand
Aliases:

Required: False
Position: Benannt
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CaptureLevel
Defines which level in the call stack is taken into account for the command context.

```yaml
Type: Int32
Parameter Sets: CaptureCommand
Aliases:

Required: False
Position: Benannt
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Exception
The exception that is transmitted.

```yaml
Type: Exception
Parameter Sets: Exception
Aliases:

Required: True
Position: 0
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

### -Message
Set optional message.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Benannt
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
Position: Benannt
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ProblemId
The exception problem ID.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Benannt
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

### -RoleInstance
Defines whether the process was successfully processed.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Benannt
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
Position: Benannt
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Severity
The message severity (Verbose, Information, Warning, Error, Critical). Default is Information.

```yaml
Type: SeverityLevel
Parameter Sets: (All)
Aliases:
Accepted values: Verbose, Information, Warning, Error, Critical

Required: False
Position: Benannt
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
Position: Benannt
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DeveloperMode
Enables the application insights developer mode.

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

### -ErrorRecord
The error record that is transmitted.

```yaml
Type: ErrorRecord
Parameter Sets: ErrorRecord
Aliases:

Required: True
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
