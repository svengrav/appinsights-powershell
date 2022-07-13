---
external help file: AppInsights.dll-Help.xml
Module Name: AppInsights
online version:
schema: 2.0.0
---

# Send-AppInsightsAvailability

## SYNOPSIS
PowerShell command used to track availability events in application insights.

## SYNTAX

```
Send-AppInsightsAvailability [-Name] <String> [-Id] <String> [-Duration] <TimeSpan> [-RunLocation] <String>
 [[-Timestamp] <DateTimeOffset>] [[-Message] <String>] [-Metrics <Hashtable>] [[-Success] <Boolean>]
 [[-InstrumentationKey] <Guid>] [[-Properties] <Hashtable>] [[-RoleName] <String>] [[-RoleInstance] <String>]
 [-ContextLevel <Int32>] [-DisableContext] [<CommonParameters>]
```

## DESCRIPTION
PowerShell command used to track availability events in application insights.

## EXAMPLES

### Example 1
```powershell
Send-AppInsightsAvailability -Name "AppleAvailable" -Id  "Apple1" -Duration 2 -RunLocation $env:COMPUTERNAME -Success $true -Timestamp (Get-Date)
```

## PARAMETERS

### -Duration
The availability test duration.

```yaml
Type: TimeSpan
Parameter Sets: (All)
Aliases:

Required: True
Position: Benannt
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Id
The availability test run id.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Benannt
Default value: None
Accept pipeline input: True (ByValue)
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
Sets optional availability message.

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

### -Name
The availability test name.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -Properties
Hashtables with custom properties.
Default is a empty hashtable.

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
The role instance.
Default is the machine name.

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
The role name.
Default is the machine name.

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

### -RunLocation
The location where the availability test was run.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Benannt
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Success
Defines whether the process was successfully processed.
Default is true.

```yaml
Type: Boolean
Parameter Sets: (All)
Aliases:

Required: False
Position: Benannt
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Timestamp
The datetime when telemetry was recorded.
Default is UTC.Now.

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

### -Metrics
Optional dictionary with custom request metrics.

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

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

## OUTPUTS

## NOTES

## RELATED LINKS
