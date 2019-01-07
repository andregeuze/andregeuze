Configuration SimpleConfig
{
    Import-DscResource –ModuleName 'PSDesiredStateConfiguration'
    Import-DscResource -ModuleName cChoco
    Import-DscResource -ModuleName xNetworking

    Node "webserver"
    {
        # Ensure Chocolatey is present on the node
        cChocoInstaller Choco
        {
            InstallDir = "C:\choco"
        }

        # Enable the windows IIS feature
        WindowsFeature IIS
        {
            Ensure = "Present"
            Name = "Web-Server"
        }

        # Enable the IIS Management Tools
        WindowsFeature IISTools
        {
            Ensure = "Present"
            Name = "Web-Mgmt-Tools"
        }

        # Enable port 80 traffic through the firewall
        xFirewall Firewall
        {
            Direction = "Inbound"
            Name = "Web-Server-TCP-In"
            DisplayName = "Web Server (TCP-In)"
            Description = "IIS allow incoming web site traffic."
            Group = "IIS Incoming Traffic"
            Enabled = "True"
            Protocol = "TCP"
            LocalPort = "80"
            Action = "Allow"
            Ensure = "Present"
        }
    }
}