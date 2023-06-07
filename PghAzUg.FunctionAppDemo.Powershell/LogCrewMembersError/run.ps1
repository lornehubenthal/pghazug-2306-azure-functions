param([PSObject] $mySbMsg, $TriggerMetadata)

Write-Host "PowerShell ServiceBus dead letter queue trigger function processed message for job id: $($mySbMsg.JobId)"

$mySbMsg.Data.EatenByArmus = [System.Convert]::ToBoolean($mySbMsg.Data.EatenByArmus)

Push-OutputBinding -Name myOutputBlob -Value $mySbMsg.Data