. .\create-metaconfig.ps1

$url = "<url>"
$key = "<key>"

# Create the metaconfigurations
$Params = @{
    RegistrationUrl = $url;
    RegistrationKey = $key;
    ComputerName = @($env:COMPUTERNAME);
    NodeConfigurationName = 'SimpleConfig.webserver';
    RefreshFrequencyMins = 300;
    ConfigurationModeFrequencyMins = 300;
    RebootNodeIfNeeded = $True;
    AllowModuleOverwrite = $False;
    ConfigurationMode = 'ApplyAndMonitor';
    ActionAfterReboot = 'ContinueConfiguration';
    ReportOnly = $False;  # Set to $True to have machines only report to AA DSC but not pull from it
}
DscMetaConfigs @Params

Set-DscLocalConfigurationManager -Path "$PSScriptRoot\DscMetaConfigs" -ComputerName $env:COMPUTERNAME -Force -Verbose