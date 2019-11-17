using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System.Net.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace TestMail
{
    class Program
    {
        public class RefreshTokenClass
        {
            public string token_type { get; set; }
            public string expires_in { get; set; }
            public string resource { get; set; }
            public string scope { get; set; }
            public string access_token { get; set; }
            public string refresh_token { get; set; }

        }


        public static string GetToken()
        {
            string tokenUrl = $"https://login.microsoftonline.com/2e7d475e-3a9e-4815-ae50-1fc455f2a988/oauth2/token";
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenUrl);

            string refreshToken = "AQABAAAAAACQN9QBRU3jT6bcBQLZNUj7iL180qZZh1hvXQ70i0snBr-hnGxTyMwBwVwXDipFmXzUw_fG6VMy_2ZrKHmrwUL-L-4m8qISUZsF4pTFQdACvdIlrKaYJVS8BcNoIJRWujZ7JbLIg50UFPo7hmsfMVhGtuYTItyqnSE7eBR6ws9kx5H3aP0tRWIrYIRtnVe5f3cmCkkpG6M0brNVjw36XrJqjhErq8HOw63PyygoFRV8wDBtp_whh8CpjkrC18yfpyB5Z_oS-KBElOgYxQnc1YU7SlSSONtHqXNsZiq044oZbFiuTOvAmK2t1FN5HGFXaBZq8CmvAwudzFZldPi3aMe_bHtSxoI2fm93PNIb9oDHMwqd2nFrB6cbG2RYsSDe0vBjZEIhH25hlSOBuz8RDfusiDg4uTAbjmcWaF6wEr2EfgCdtA-fi16yyf3HRkhxynVM6I48gvZ_-3UjRQFX-UnCWDr6i2FSgtPuSJQgcQhfT037J6Jwedh_lXvGhbVKuQgMmXca_sMEOLn8nAk0PEjBlqLbNOqI4qjGV8jrUH1btT5FNOdafGT4-MDciAqy44KII5Zw6vbZAp8e9bfQbAXmzi1iNxgHvkCHSo3kPkVYXun4hdFt2_cHWOUzcVkCJE02_d6gHbAk-f-cIRk1FqyI8VcOpkLk98sOOx0HrLeKjrPJzTir1FVZM0Lq1LuTDZC_VpSO7h9H_UJLhr28yg-qkSIRWtDb3HBD_qmN2bImWvQFZ9OFPCePhaK-IyMCRiggAA";

            tokenRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = "b9180c80-f2e8-45ae-95b8-1159666df351",
                //["client_secret"] = "@-@PNzAEBUp7w]fFq7tNP6l1m8Cib5RX",
                ["refresh_token"] = refreshToken,
                //["scope"] = "https://vault.azure.net/.default"
                ["resource"] = "https://AIPSQLAAD.com"
            });

            dynamic json;
            dynamic results;

            HttpClient client = new HttpClient();

            var tokenResponse = client.SendAsync(tokenRequest).Result;

            json = tokenResponse.Content.ReadAsStringAsync().Result;
            results = JsonConvert.DeserializeObject<RefreshTokenClass>(json);

            Console.WriteLine("Your Refresh Token=>{0}", results.refresh_token);
            return string.Empty;
        }
         


        private static string GetKeyVaultRefreshToken()
        {
            AuthenticationContext authContext = new AuthenticationContext("https://login.microsoftonline.com/2e7d475e-3a9e-4815-ae50-1fc455f2a988");
            Uri redirectUri = new Uri("https://localhost7000");
            string clientId = "b379c217-a557-4cf1-a42a-a61b9c36821e";
            string clientSecret = "A6]yYyZydeVwIT]QF.klUkToVJ2bW35:";

            string userName = "admin@M365x055932.onmicrosoft.com";
            string userPassword = "64e65ApU6T";

            var pwd = new SecureString();
            foreach (char c in userPassword)
                pwd.AppendChar(c);

            var clientCredential = new ClientCredential(clientId, clientSecret);
            //var userCredential = new UserPasswordCredential(userName, pwd);

            var userCred = new UserCredential(userName);
            Task<AuthenticationResult> authTask = authContext.AcquireTokenAsync("https://vault.azure.net",clientId, redirectUri, new PlatformParameters(PromptBehavior.Auto));

            authTask.Wait();

            return authTask.Result.AccessToken;
        }

        private static string GetRefreshToken()
        {
            try
            {

                string tokenUrl = $"https://login.microsoftonline.com/2e7d475e-3a9e-4815-ae50-1fc455f2a988/oauth2/token";
                var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenUrl);

                tokenRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "password",
                    ["client_id"] = "b379c217-a557-4cf1-a42a-a61b9c36821e",
                    ["client_secret"] = "A6]yYyZydeVwIT]QF.klUkToVJ2bW35:",
                    ["resource"] = "https://vault.azure.net",
                    ["username"] = "admin@M365x055932.onmicrosoft.com",
                    ["password"] = "64e65ApU6T"

                });

                dynamic json;
                dynamic results;

                HttpClient client = new HttpClient();

                var tokenResponse = client.SendAsync(tokenRequest).Result;

                json = tokenResponse.Content.ReadAsStringAsync().Result;
                results = JsonConvert.DeserializeObject<RefreshTokenClass>(json);

                Console.WriteLine("Your Refresh Token=>{0}", results.refresh_token);

                string refreshToken = results.refresh_token;


                tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenUrl);

                tokenRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["grant_type"] = "refresh_token",
                    ["client_id"] = "b379c217-a557-4cf1-a42a-a61b9c36821e",
                    ["refresh_token"] = refreshToken,
                    ["resource"] = "https://vault.azure.net"
                });

                client = new HttpClient();

                tokenResponse = client.SendAsync(tokenRequest).Result;

                json = tokenResponse.Content.ReadAsStringAsync().Result;
                results = JsonConvert.DeserializeObject<RefreshTokenClass>(json);

                Console.WriteLine("Your Refresh Token=>{0}", results.refresh_token);
                refreshToken = results.refresh_token;

                return refreshToken;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private static string GetAccessToken()
        {
            AuthenticationContext authContext = new AuthenticationContext("https://login.microsoftonline.com/2e7d475e-3a9e-4815-ae50-1fc455f2a988");
            Uri redirectUri = new Uri("https://nativerefresh.com");

            string clientId = "b379c217-a557-4cf1-a42a-a61b9c36821e";
            //string clientSecret = "@-@PNzAEBUp7w]fFq7tNP6l1m8Cib5RX";

            //var clientCredential = new ClientCredential(clientId, clientSecret);

            //string authorizedCode = "AQABAAAAAACQN9QBRU3jT6bcBQLZNUj7iL180qZZh1hvXQ70i0snBr-hnGxTyMwBwVwXDipFmXzUw_fG6VMy_2ZrKHmrwUL-L-4m8qISUZsF4pTFQdACvdIlrKaYJVS8BcNoIJRWujZ7JbLIg50UFPo7hmsfMVhGtuYTItyqnSE7eBR6ws9kx5H3aP0tRWIrYIRtnVe5f3cmCkkpG6M0brNVjw36XrJqjhErq8HOw63PyygoFRV8wDBtp_whh8CpjkrC18yfpyB5Z_oS-KBElOgYxQnc1YU7SlSSONtHqXNsZiq044oZbFiuTOvAmK2t1FN5HGFXaBZq8CmvAwudzFZldPi3aMe_bHtSxoI2fm93PNIb9oDHMwqd2nFrB6cbG2RYsSDe0vBjZEIhH25hlSOBuz8RDfusiDg4uTAbjmcWaF6wEr2EfgCdtA-fi16yyf3HRkhxynVM6I48gvZ_-3UjRQFX-UnCWDr6i2FSgtPuSJQgcQhfT037J6Jwedh_lXvGhbVKuQgMmXca_sMEOLn8nAk0PEjBlqLbNOqI4qjGV8jrUH1btT5FNOdafGT4-MDciAqy44KII5Zw6vbZAp8e9bfQbAXmzi1iNxgHvkCHSo3kPkVYXun4hdFt2_cHWOUzcVkCJE02_d6gHbAk-f-cIRk1FqyI8VcOpkLk98sOOx0HrLeKjrPJzTir1FVZM0Lq1LuTDZC_VpSO7h9H_UJLhr28yg-qkSIRWtDb3HBD_qmN2bImWvQFZ9OFPCePhaK-IyMCRiggAA";

            //Task<AuthenticationResult> authTask = authContext.AcquireTokenByAuthorizationCodeAsync(authorizedCode, redirectUri, clientCredential);

            Task<AuthenticationResult> authTask = authContext.AcquireTokenAsync("https://vault.azure.net"
                        , clientId
                        , redirectUri
                        , new PlatformParameters(PromptBehavior.Auto));

            authTask.Wait();

            return authTask.Result.AccessToken;
        }

        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            const string CLIENTSECRET = "@-@PNzAEBUp7w]fFq7tNP6l1m8Cib5RX";
            const string CLIENTID = "b9180c80-f2e8-45ae-95b8-1159666df351";
            

            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(CLIENTID, CLIENTSECRET);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }

        private static void DoVault()
        {
            const string BASESECRETURI = "https://AKVSQL.vault.azure.net";
            const string SECRETNAME = "Test";
            var kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
            
             var result=kvc.GetSecretAsync(BASESECRETURI +@"/secrets/" + SECRETNAME).ConfigureAwait(false).GetAwaiter().GetResult();

            Console.ReadLine();

        }

        private static void UseRefreshTokenAsInput()
        {
            try
                {
                //https://community.box.com/t5/Platform-and-Development-Forum/Get-Access-Token-using-RefreshToken/td-p/55414

                string tokenUrl = $"https://login.microsoftonline.com/2e7d475e-3a9e-4815-ae50-1fc455f2a988/oauth2/token";
                    var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenUrl);

                string refreshToken = "AQABAAAAAACQN9QBRU3jT6bcBQLZNUj7_TDNcknlqnFnclU9-LyNxgGWTzqQ70_ASRW9NO5eGSUYAS3SJo069CfQWZE9kgnUwiekYPuZ1GyfX0eUBb7_bI89DPjrbbxIO2p8aJ2dheOUmM_lUeWosPiDgBOmeyTdE78MMTpLjlspPggj22HeSQFOcpbHwzEtz_17VfZJ11fcdzRFRxzkmwff5wgBwiiyR1Vux7It5mmJXwJN8sgdB7-QKHbNvpvPw1ScTaqC9bL0lMn_Irpo_fNFzx-i-AN6szGhaOz-QIY337-_7USEuWuZkV2H8BbycNgOMUdYtkAKrbgJeGzyMjlWzdXoIBzGBK3lcWNMuNXdXAOPZ9Ingg9cw4a-zHewMNeNc9UvSwgeVhLBTgROc7sE9yqGyKpkMSx8alQgBNLktZt6oiHk4DNzZaOB4JbwiCfHf6Hlctm6345Oh5m4XP2v9Up5VwjtacVdWYQ80CxAdxHpwDXaTgIIRROz9F7KWJnRiJwywiJ0ABaGg3K75FPzhZ0z99bV9h2ikUtBoiPYKjhWS9DtEu6fFIcZkKIK0hxFGR55wXKBehpCCc3JHZzmx9z8sa2rg4tX9dZeL2yW86lP9pIkK4_xOHyBS84K_LFL309g6fX-WgpazzdMWeWJ8pcioUksdGKK05xTosBHNckYkC1hUYsDaGfQiICHb6QnEGYOhnV1H6YZ1QlPQYFyIxIEuG4jPI_xMcbCUkWv6CQafyPssaRRzU-hAl3n_S3KzlVmZ_njbjixoIOE8WdFIssEpQ_Jhtl06E_G-oVUE6VvH4g7pf5kQZ4_sBAu2bPgf-nKzsFQE-Igrw1LYGTGG5NQgwqOnpMecG5s33xmnkf-itOtVthgNs0RZwE8tESArUFk8uGz5qv5cXSqhYtWWe23jekY8Bfcc1_340jy2NBQ6CNOoLcKb2OqP3ZE2A7XwM5qQdAgAA";

                tokenRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        ["grant_type"] = "refresh_token",
                        ["client_id"] = "b379c217-a557-4cf1-a42a-a61b9c36821e",
                        ["refresh_token"] = refreshToken,
                        //["resource"] = "https://vault.azure.net"
                        ["resource"] = "https://syncservice.o365syncservice.com/"
                });

                    dynamic json;
                    dynamic results;

                    HttpClient client = new HttpClient();

                    var tokenResponse = client.SendAsync(tokenRequest).Result;

                    json = tokenResponse.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<RefreshTokenClass>(json);

                var accessToken = result.AccessToken;
                    //Console.WriteLine("Your Refresh Token=>{0}", result.refresh_token);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        static void Main(string[] args)
        {
            try
            {
                // DoVault(); // Successfully read the value from the keyvault using client credentials

               var v2 = GetRefreshToken(); // Refresh Token is working fine

               //var v2 = GetAccessToken(); // Not working

              // UseRefreshTokenAsInput();
               //var v3=  GetKeyVaultRefreshToken();

            }
            catch (Exception ex)
            {

                throw;
            }

          
            //try
            //{
            //    MailMessage mail = new MailMessage();
            //    SmtpClient SmtpServer = new SmtpClient("ciibe-onlineassessment.in");

            //    mail.From = new MailAddress("info@ciibe-onlineassessment.in");
            //    mail.To.Add("senbagakumars@gmail.com");
            //    mail.Subject = "Test Mail";

            //    string emailbody = "<!DOCTYPE html> <html> <head> <title>Email Notification</title> <style> table {   font-family: arial, sans-serif;   border-collapse: collapse;   width: 100%; }  td, th {   border: 1px solid #dddddd;   text-align: left;   padding: 8px; }  tr:nth-child(even) {   background-color: #dddddd; } </style> </head> <body>  <h1>UPN violations" +
            //        "</h1> <table > <thead> <tr> <th>Alias</th> <th>Count</th> </tr> </thead> <tbody> <tr> <td> xxx@microsoft.com</td> <td>10</td> </tr> </tbody> </table>";
            //    mail.Body = emailbody + " We see below subscriptions are created from you which resulted in UPN violations(non-ALT Account) and not supposed to be done. Please Cleanup the violations and use ALT-account for subscription creation in future.";
            //    SmtpServer.Port = 587;
            //    SmtpServer.Credentials = new System.Net.NetworkCredential("info@ciibe-onlineassessment.in", "mynameisKANTH@2019");
            //    //SmtpServer.EnableSsl = true;
            //    SmtpServer.Send(mail);
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}

        }
    }
}
