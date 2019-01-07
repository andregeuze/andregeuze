# login to Azure Resource Manager
try { Get-AzureRmContext } catch { Login-AzureRmAccount -SubscriptionName "Visual Studio Enterprise" }

# Settings for this script
$automationResourceGroup = "doa-rg"
$automationAccountName = "DevOpsAutomation"
$runbookName = "SimpleRunbookExample-Workflow"

# Upload the config file to azure and publish it
Import-AzureRmAutomationRunbook `
    -ResourceGroupName $automationResourceGroup -AutomationAccountName $automationAccountName `
    -Path "$PSScriptRoot\runbooks\$runbookName.ps1" `
    -Type PowerShellWorkflow -Published -Force

# Start the runbook after publishing this version
$jobData = Start-AzureRmAutomationRunbook `
    -ResourceGroupName $automationResourceGroup -AutomationAccountName $automationAccountName `
    -Name $runbookName

$status = ""
do {
    Start-Sleep -Seconds 1
    # Get the job status
    $job = Get-AzureRmAutomationJob `
        -ResourceGroupName $automationResourceGroup -AutomationAccountName $automationAccountName `
        -Id $jobData.JobId

    if ($status -ne $job.Status)
    {
        $status = $job.Status
        $name = $job.RunbookName
        "Job '$name' status: $status"
    }
}
while ($status -ne "Completed")
