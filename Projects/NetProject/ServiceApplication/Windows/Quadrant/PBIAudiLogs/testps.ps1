Set-ExecutionPolicy RemoteSigned

$UserCredential = Get-Credential

$Session = New-PSSession -ConfigurationName Microsoft.Exchange -ConnectionUri https://outlook.office365.com/powershell-liveid/ -Credential $UserCredential -Authentication Basic -AllowRedirection

#$Session = Get-PSSession -ConfigurationName Microsoft.Exchange -ConnectionUri https://outlook.office365.com/powershell-liveid/ -Credential $UserCredential -Authentication Basic -AllowRedirection

Import-PSSession $Session 

#Import-PSSession $Session -DisableNameChecking
$Session

$sessionName = (get-date -Format 'u')+'pbiauditlog'

$sessionName
$startDate=01/01/2019
$enddate=01/31/2019

#Import-PSSession $Session
#Search-UnifiedAuditLog -StartDate 01/01/2019 -EndDate 01/31/2019 -RecordType PowerBI -ResultSize 1000 | Format-Table | More

$currentResults = Search-UnifiedAuditLog -StartDate 01/04/2019 -EndDate 03/31/2019 -SessionId $sessionName -SessionCommand ReturnLargeSet -ResultSize 1000 -RecordType PowerBI

$datestring = (get-date).ToString("yyyyMMddHH:mm:ss")
$datestring

$fileName = ("C:\Users\v-sesiga\Desktop\PBIAudiLogs\PBIAuditLogs\" + $datestring + ".csv")
$fileName

if ($currentResults.Count -gt 0) 
{
    Write-Host ("Finished {3} search #{1}, {2} records: {0} min" -f [math]::Round((New-TimeSpan -Start $scriptStart).TotalMinutes,4), $i, $currentResults.Count, $user.UserPrincipalName)
    $currentResults | Format-Table
    $currentResults | Export-Csv -Path "C:\Users\v-sesiga\Desktop\sample.csv"
    #ConvertTo-Csv -InputObject currentResults -Delimiter ':' -NoTypeInformation
}
else
{
    Write-host "No Records"
}

Remove-PSSession $Session