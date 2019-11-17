Set-ExecutionPolicy RemoteSigned -Force



#This is better for scheduled jobs

$User = "ganesh@quadrantresource.com"

$PWord = ConvertTo-SecureString -String "Hyderabad65!" -AsPlainText -Force

$UserCredential = New-Object -TypeName "System.Management.Automation.PSCredential" -ArgumentList $User, $PWord



#This will prompt the user for credential

#$UserCredential = Get-Credential





$Session = New-PSSession -ConfigurationName Microsoft.Exchange -ConnectionUri https://outlook.office365.com/powershell-liveid/ -Credential $UserCredential -Authentication Basic -AllowRedirection

Import-PSSession $Session


$startDate=(get-date).AddDays(-1)

$endDate=(get-date)

$scriptStart=(get-date)



$sessionName = (get-date -Format 'u')+'pbiauditlog'

# Reset user audit accumulator

$aggregateResults = @()

$i = 0 # Loop counter

Do { 

	$currentResults = Search-UnifiedAuditLog -StartDate $startDate -EndDate $endDate `

								-SessionId $sessionName -SessionCommand ReturnLargeSet -ResultSize 1000 -RecordType PowerBI

	if ($currentResults.Count -gt 0) {

		Write-Host ("Finished {3} search #{1}, {2} records: {0} min" -f [math]::Round((New-TimeSpan -Start $scriptStart).TotalMinutes,4), $i, $currentResults.Count, $user.UserPrincipalName )

		# Accumulate the data

		$aggregateResults += $currentResults

		# No need to do another query if the # recs returned <1k - should save around 5-10 sec per user

		if ($currentResults.Count -lt 1000) {

			$currentResults = @()

		} else {

			$i++

		}

	}

} Until ($currentResults.Count -eq 0) # --- End of Session Search Loop --- #

Remove-PSSession -Id $Session.Id

