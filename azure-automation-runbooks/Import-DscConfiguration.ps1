# login to Azure Resource Manager
try { Get-AzureRmContext } catch { Login-AzureRmAccount }
Select-AzureRmSubscription -SubscriptionName "Containers"

# Settings for this script
$automationResourceGroup = "rg-ADP-2018-Demos"
$automationAccountName = "ADP-Automation"

# Gather the config files
$files = @("SimpleConfig")

foreach ($configFileName in $files)
{
    # Upload the config file to azure and publish it
    Import-AzureRmAutomationDscConfiguration `
        -ResourceGroupName $automationResourceGroup –AutomationAccountName $automationAccountName `
        -SourcePath "$PSScriptRoot\dsc\$configFileName.ps1" `
        -Published –Force

    # Compile the new config
    $jobData = Start-AzureRmAutomationDscCompilationJob `
        -ResourceGroupName $automationResourceGroup –AutomationAccountName $automationAccountName `
        -ConfigurationName $configFileName

    $status = ""
    do {
        Start-Sleep -Seconds 1
        # Get the job status
        $job = Get-AzureRmAutomationDscCompilationJob `
            -ResourceGroupName $automationResourceGroup -AutomationAccountName $automationAccountName `
            -Id $jobData.Id

        if ($status -ne $job.Status)
        {
            $status = $job.Status
            $name = $job.ConfigurationName
            "Job '$name' status: $status"
        }
    }
    while ($status -ne "Completed")
}
