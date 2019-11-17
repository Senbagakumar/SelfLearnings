#Verify and delete existing certs

If (Get-ChildItem -Path cert:\LocalMachine\my -DnsName rootauthority )
{
     Get-ChildItem -Path cert:\LocalMachine\my -DnsName rootauthority | Remove-Item
}

If (Get-ChildItem -Path cert:\LocalMachine\my -DnsName secondary-secrets-kek_onebox)
{
     Get-ChildItem -Path cert:\LocalMachine\my -DnsName secondary-secrets-kek_onebox | Remove-Item
}

If (Get-ChildItem -Path cert:\LocalMachine\my -DnsName SslCert)
{
    Get-ChildItem -Path cert:\LocalMachine\my -DnsName SslCert | Remove-Item
}

If (Get-ChildItem -Path cert:\LocalMachine\my -DnsName secrets-kek_onebox)
{
    Get-ChildItem -Path cert:\LocalMachine\my -DnsName secrets-kek_onebox | Remove-Item
}


#Create new root cert

$Newcert = new-SelfSignedCertificate -certstorelocation cert:\localmachine\my -dnsname "rootauthority"

$NewCertthumbprint = $Newcert.Thumbprint

$password = ConvertTo-SecureString -String "Password1" -Force -AsPlainText

Export-PfxCertificate -cert cert:\localMachine\my\$NewCertthumbprint  -FilePath $env:Temp\root-authority.pfx -Password $password

Export-Certificate -Cert cert:\localMachine\my\$NewCertthumbprint -FilePath $env:Temp\root-authority.crt

$rootcert = ( Get-ChildItem -Path cert:\LocalMachine\My\$NewCertthumbprint )

#Create certificates

$sslcert = New-SelfSignedCertificate -certstorelocation cert:\localmachine\my -dnsname "SslCert" -Signer $rootcert
$secrets_kek_onebox = New-SelfSignedCertificate -certstorelocation cert:\localmachine\my -dnsname "secrets-kek_onebox" -Signer $rootcert
$secondary_secrets_kek_onebox = New-SelfSignedCertificate -certstorelocation cert:\localmachine\my -dnsname "secondary-secrets-kek_onebox" -Signer $rootcert

$password1 = ConvertTo-SecureString -String "Password!" -Force -AsPlainText

$sslcertThumbprint = $sslcert.Thumbprint
$secondary_secrets_kek_oneboxThumbprint = $secondary_secrets_kek_onebox.Thumbprint 
$secrets_kek_oneboxThumbprint  = $secrets_kek_onebox.Thumbprint

#Export PFX

Export-PfxCertificate -cert cert:\localMachine\my\$sslcertThumbprint -FilePath $env:Temp\sslcert.pfx -Password $password1
Export-PfxCertificate -cert cert:\localMachine\my\$secondary_secrets_kek_oneboxThumbprint  -FilePath $env:Temp\secondary_secrets_kek_onebox.pfx -Password $password1
Export-PfxCertificate -cert cert:\localMachine\my\$secrets_kek_oneboxThumbprint  -FilePath $env:Temp\secrets_kek_onebox.pfx -Password $password1

#Export Public Key

Export-Certificate -Cert cert:\localMachine\my\$sslcertThumbprint -FilePath $env:Temp\sslcert.crt
Export-Certificate -Cert cert:\localMachine\my\$secondary_secrets_kek_oneboxThumbprint -FilePath $env:Temp\secondary_secrets_kek_onebox.crt
Export-Certificate -Cert cert:\localMachine\my\$secrets_kek_oneboxThumbprint -FilePath $env:Temp\secrets_kek_onebox.crt

