param([PSObject] $mySbMsg, $TriggerMetadata)

Write-Host "PowerShell ServiceBus queue trigger function processed message for job id: $($mySbMsg.JobId)"

$mySbMsg.Data.EatenByArmus = [System.Convert]::ToBoolean($mySbMsg.Data.EatenByArmus)

if ($mySbMsg.Data.EatenByArmus -eq $true)
{
    throw "Crew Member not available."
}

Push-OutputBinding -Name myOutputBlob -Value $mySbMsg.Data