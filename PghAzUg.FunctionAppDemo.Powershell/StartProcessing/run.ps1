# Input bindings are passed in via param block.
param([string] $InputBlob, $TriggerMetadata)

# Write out the blob name and size to the information log.
Write-Host "PowerShell Blob trigger function Processed blob! Name: $($TriggerMetadata.Name).$($TriggerMetadata.Extension)"

if ($TriggerMetadata.Extension -ne "csv")
{
    throw "Only CSV Allowed"
}

$jobId = "$((New-Guid).Guid)"
$obj = convertfrom-csv $InputBlob

$outputObj = $obj | ForEach-Object {
    New-Object PSObject -Property @{
        JobId = $jobId
        Data = $_
    }
}

Push-OutputBinding -Name outputSbMsg -Value $outputObj