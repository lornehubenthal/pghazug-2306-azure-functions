# Input bindings are passed in via param block.
param($QueueItem, $TriggerMetadata)

# Write out the queue message and insertion time to the information log.
Write-Host "PowerShell poison blob queue trigger function processed work item: $QueueItem"
Write-Host "Queue item insertion time: $($TriggerMetadata.InsertionTime)"

Push-OutputBinding -Name message -Value (@{
    subject = "Blob Processing Failed"
    content = @(@{
        type = 'text/plain'
        value = "The blob $($QueueItem.BlobName) failed to be processed at $($TriggerMetadata.InsertionTime)"
    })
})
