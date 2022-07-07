# AppInsights - Application Insights for PowerShell
<img src="./docs/images/AppInsights200px.png" width="200"/>

![AppInsights Deploy](https://github.com/svengrav/appinsights-powershell/actions/workflows/appinsights-deploy.yml/badge.svg)
![AppInsights Test](https://github.com/svengrav/appinsights-powershell/actions/workflows/appinsights-test.yml/badge.svg)

AppInsights is a PowerShell module that makes Application Insights available for PowerShell and extends it with supporting functions for PowerShell.
The module simplifies the interaction with Application Insights and offers some convenient functions for automatic log tracking.

- [AppInsights - PowerShell Gallery](https://www.powershellgallery.com/packages/AppInsights/)

## Features
- Automatic capture of the command context and its parameters.
- Natively supports PowerShell hashtables for properties and metrics.
- Flexibility in the use of the Instrumentation Key. 
  - Enables the Instrumentation Key to be stored as an Environment Variable.
- Automatic and wide-ranging module tests.

## Quickstart 

### Install and import AppInsights
```PowerShell
# Installs and imports the latest version.
Install-Module AppInsights

Import-Module AppInsights
```

### Authentication with Environment Variable
This can be implemented, for example, with a system variable to simplify the authentication of tasks and services.
```PowerShell
# Authenticate with enviroment variable "AI_INSTRUMENTATION_KEY"
$env:AI_INSTRUMENTATION_KEY = "decf0103-ed91-4880-b01d-31fe4fa12c98"

# Send trace
Send-AppInsightsTrace -Message "Message" -Properties @{ CustomProperty = "CustomProperty1" }
```

### Authentication with Parameter
Send traces with Instrumentation Key as parameter. This will overwrite the Environment Vriable if set.
```PowerShell
Send-AppInsightsTrace -Message "Message" -InstrumentationKey "decf0103-ed91-4880-b01d-31fe4fa12c98" 
```

##  Docs
Overview and documentation of currently supported commands.
- [Command Documentation](./docs/)
- [Send-AppInsightsTrace](./docs/Send-AppInsightsTrace.md)
- [Send-AppInsightsDependency](./docs/Send-AppInsightsDependency.md)
- [Send-AppInsightsEvent](./docs/Send-AppInsightsEvent.md)
- [Send-AppInsightsException](./docs/Send-AppInsightsException.md)
- [Send-AppInsightsAvailability](./docs/Send-AppInsightsAvailability.md)
- [Send-AppInsightsRequest](./docs/Send-AppInsightsRequest.md)
- [Send-AppInsightsOperation](./docs/Send-AppInsightsOperation.md) (Experimental 🚧)


## Notes
- The module works for PowerShell 5 and PowerShell 7.
- The module is based on Microsoft.ApplicationInsights (2.18.0). This version is tagged as deprecated but 
    with PowerShell 7, there is still a dependency conflict with the current versio (2.20) at the moment. Therefore, the outdated package is currently still used.

## Sample Result
The invoking script and parameters are automatically captured, so you don't have to do it yourself.

![TraceSample1](./docs/images/SampleTrace1.png)


## Application Map (Experimental 🚧)
**Please dont use this in production** 

With the command "Send-AppInsightsOperation", a dependency and a request can be combined to display an application map. In this way, the dependencies can be displayed in a module.

![OperationSample1](./docs/images/SampleOperation1.png)
