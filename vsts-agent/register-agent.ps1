param(
    $projecturl = "https://geuze.visualstudio.com",
    $pattoken = "rlwykzcv5ot65d4umlub6byc3ucmgxkk43ef52jlv5g5tolcitwa",
    $agentfolder = "D:\vstsagent",
    $agentworkfolder = "_work"
)

$ErrorActionPreference="Stop"
$agentZip="$PSScriptRoot\agent.zip"

If(-NOT ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator"))
{
    throw "Run command in Administrator PowerShell Prompt"
}

If(Test-Path $agentfolder\config.cmd)
{
    cd "$agentfolder"
    .\config.cmd remove --auth PAT --token $pattoken
    cd "$PSScriptRoot"
    Remove-Item -Path "$agentfolder" -Recurse -Force -Confirm:$false

	Add-Type -AssemblyName System.IO.Compression.FileSystem
    [System.IO.Compression.ZipFile]::ExtractToDirectory($agentZip, "$agentfolder")
}
Else
{
	Add-Type -AssemblyName System.IO.Compression.FileSystem
    [System.IO.Compression.ZipFile]::ExtractToDirectory($agentZip, "$agentfolder")
}

#Remove-Item $agentZip

cd "$agentfolder"

.\config.cmd `
    --replace `
    --unattended `
    --runasservice `
    --auth PAT `
    --token $pattoken `
    --url "$projecturl" `
    --agent $env:COMPUTERNAME `
    --work "$agentworkfolder"