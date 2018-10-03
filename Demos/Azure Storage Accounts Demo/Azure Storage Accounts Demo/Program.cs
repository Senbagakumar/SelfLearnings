using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;

namespace Azure_Storage_Accounts_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Azure Blob storage - .NET Quickstart sample");

           
            //Console.WriteLine();

            // string token = GetAccountSASToken();

            // UseAccountSAS(token);

            //ProcessAsync().GetAwaiter().GetResult();
            //PageBlobProcessAsync().GetAwaiter().GetResult();
            //AppendBlobProcessAsync().GetAwaiter().GetResult();

            // Console.WriteLine("Press any key to exit the sample application.");

            //var token = GetSASToken();
            Console.ReadLine();
        }


        static string GetAccountSASToken()
        {
            // To create the account SAS, you need to use your shared key credentials. Modify for your account.
            const string ConnectionString = "UseDevelopmentStorage=true;";
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            // Create a new access policy for the account.
            SharedAccessAccountPolicy policy = new SharedAccessAccountPolicy()
            {
                Permissions = SharedAccessAccountPermissions.Read | SharedAccessAccountPermissions.Write | SharedAccessAccountPermissions.List,
                Services = SharedAccessAccountServices.Blob | SharedAccessAccountServices.File,
                ResourceTypes = SharedAccessAccountResourceTypes.Service,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                Protocols = SharedAccessProtocol.HttpsOnly
            };

            // Return the SAS token.
            return storageAccount.GetSharedAccessSignature(policy);
        }

        static string GetSASToken()
        {
            const string ConnectionString = "UseDevelopmentStorage=true;";
            string blobName = "1.txt"; //just for an example

            CloudStorageAccount account = CloudStorageAccount.Parse(ConnectionString);
            CloudBlobClient client = account.CreateCloudBlobClient();


            // Build shared access signature
            CloudBlobContainer container = client.GetContainerReference("testcont");
            container.CreateIfNotExists();


            ////Get a reference to a blob within the container.

           

            //Create a new stored access policy and define its constraints.
            SharedAccessBlobPolicy sharedPolicy = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(5),
                Permissions = SharedAccessBlobPermissions.Read
            };


            //Get the container's existing permissions.
            BlobContainerPermissions permissions = new BlobContainerPermissions();

            // The public access setting explicitly specifies that the container is private, 
            // so that it can't be accessed anonymously.
            permissions.PublicAccess = BlobContainerPublicAccessType.Off;

            //Add the new policy to the container's permissions.
            string policyName = "myPolicy";

            permissions.SharedAccessPolicies.Clear();
            permissions.SharedAccessPolicies.Add(policyName, sharedPolicy);
            container.SetPermissions(permissions);

            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
            string sasBlobToken = blob.GetSharedAccessSignature(null, policyName);
            string downloadLink = blob.Uri + sasBlobToken;

            return downloadLink;
        }

        static void UseAccountSAS(string sasToken)
        {
            // Create new storage credentials using the SAS token.
            StorageCredentials accountSAS = new StorageCredentials(sasToken);
            // Use these credentials and the account name to create a Blob service client.
            CloudStorageAccount accountWithSAS = new CloudStorageAccount(accountSAS, "devstoreaccount1", endpointSuffix: null, useHttps: true);
            CloudBlobClient blobClientWithSAS = accountWithSAS.CreateCloudBlobClient();


            CloudBlobContainer cloudBlobContainer = blobClientWithSAS.GetContainerReference("testcont");// "quickstartblobs" + Guid.NewGuid().ToString());

            cloudBlobContainer.CreateIfNotExistsAsync();

            Console.WriteLine("Created container '{0}'", cloudBlobContainer.Name);

            Console.WriteLine();


            // Create a file in your local MyDocuments folder to upload to a blob.

            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string localFileName = "QuickStart_" + Guid.NewGuid().ToString() + ".txt";

            string sourceFile = Path.Combine(localPath, localFileName);

            // Write text to the file.

            File.WriteAllText(sourceFile, "Hello, World!");



            Console.WriteLine("Temp file = {0}", sourceFile);

            Console.WriteLine("Uploading to Blob storage as blob '{0}'", localFileName);

            Console.WriteLine();



            // Get a reference to the blob address, then upload the file to the blob.

            // Use the value of localFileName for the blob name.

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);

            cloudBlockBlob.UploadFromFile(sourceFile);


        }

        private static async Task ProcessAsync()

        {

            CloudStorageAccount storageAccount = null;

            CloudBlobContainer cloudBlobContainer = null;

            string sourceFile = null;

            string destinationFile = null;



            // Retrieve the connection string for use with the application. The storage connection string is stored

            // in an environment variable on the machine running the application called storageconnectionstring.

            // If the environment variable is created after the application is launched in a console or with Visual

            // Studio, the shell needs to be closed and reloaded to take the environment variable into account.

            string storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"]; //Environment.GetEnvironmentVariable("StorageConnectionString");


            //storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

            // Check whether the connection string can be parsed.

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {

                try

                {

                    // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                    
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    //127.0.0.1:10000/devstoreaccount1/testcont

                    // Create a container called 'quickstartblobs' and append a GUID value to it to make the name unique. 

                    cloudBlobContainer = cloudBlobClient.GetContainerReference("testcont");// "quickstartblobs" + Guid.NewGuid().ToString());

                    await cloudBlobContainer.CreateIfNotExistsAsync();

                    Console.WriteLine("Created container '{0}'", cloudBlobContainer.Name);

                    Console.WriteLine();



                    // Set the permissions so the blobs are public. 

                    BlobContainerPermissions permissions = new BlobContainerPermissions

                    {

                        PublicAccess = BlobContainerPublicAccessType.Blob

                    };

                    await cloudBlobContainer.SetPermissionsAsync(permissions);



                    // Create a file in your local MyDocuments folder to upload to a blob.

                    string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);                    

                    string localFileName = "QuickStart_" + Guid.NewGuid().ToString() + ".txt";                    

                    sourceFile = Path.Combine(localPath, localFileName);

                    // Write text to the file.

                    File.WriteAllText(sourceFile, "Hello, World!");



                    Console.WriteLine("Temp file = {0}", sourceFile);

                    Console.WriteLine("Uploading to Blob storage as blob '{0}'", localFileName);

                    Console.WriteLine();



                    // Get a reference to the blob address, then upload the file to the blob.

                    // Use the value of localFileName for the blob name.

                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);

                    await cloudBlockBlob.UploadFromFileAsync(sourceFile);



                    // List the blobs in the container.

                    Console.WriteLine("Listing blobs in container.");

                    BlobContinuationToken blobContinuationToken = null;

                    do

                    {

                        var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);

                        // Get the value of the continuation token returned by the listing call.

                        blobContinuationToken = results.ContinuationToken;

                        foreach (IListBlobItem item in results.Results)

                        {

                            Console.WriteLine(item.Uri);

                        }

                    } while (blobContinuationToken != null); // Loop while the continuation token is not null.

                    Console.WriteLine();



                    // Download the blob to a local file, using the reference created earlier. 

                    // Append the string "_DOWNLOADED" before the .txt extension so that you can see both files in MyDocuments.

                    destinationFile = sourceFile.Replace(".txt", "_DOWNLOADED.txt");

                    Console.WriteLine("Downloading blob to {0}", destinationFile);

                    Console.WriteLine();

                    await cloudBlockBlob.DownloadToFileAsync(destinationFile, FileMode.Create);

                    //await cloudBlockBlob.DeleteAsync();

                }

                catch (StorageException ex)

                {

                    Console.WriteLine("Error returned from the service: {0}", ex.Message);

                }

                finally

                {

                    Console.WriteLine("Press any key to delete the sample files and example container.");

                    Console.ReadLine();

                    // Clean up resources. This includes the container and the two temp files.

                    Console.WriteLine("Deleting the container and any blobs it contains");

                    if (cloudBlobContainer != null)

                    {                       

                       // await cloudBlobContainer.DeleteIfExistsAsync();

                    }

                    Console.WriteLine("Deleting the local source file and local downloaded files");

                    Console.WriteLine();

                    File.Delete(sourceFile);

                    File.Delete(destinationFile);

                }

            }

            else

            {

                Console.WriteLine(

                    "A connection string has not been defined in the system environment variables. " +

                    "Add a environment variable named 'storageconnectionstring' with your storage " +

                    "connection string as a value.");

            }

        }
        private static async Task PageBlobProcessAsync()
        {

            CloudStorageAccount storageAccount = null;

            CloudBlobContainer cloudBlobContainer = null;

           

            // Retrieve the connection string for use with the application. The storage connection string is stored

            // in an environment variable on the machine running the application called storageconnectionstring.

            // If the environment variable is created after the application is launched in a console or with Visual

            // Studio, the shell needs to be closed and reloaded to take the environment variable into account.

            string storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"]; //Environment.GetEnvironmentVariable("StorageConnectionString");


            //storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

            // Check whether the connection string can be parsed.

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {

                try

                {

                    // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.

                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    //127.0.0.1:10000/devstoreaccount1/testcont

                    // Create a container called 'quickstartblobs' and append a GUID value to it to make the name unique. 

                    cloudBlobContainer = cloudBlobClient.GetContainerReference("testcontpageblob");// "quickstartblobs" + Guid.NewGuid().ToString());

                    await cloudBlobContainer.CreateIfNotExistsAsync();

                    Console.WriteLine("Created container '{0}'", cloudBlobContainer.Name);

                    Console.WriteLine();



                    // Set the permissions so the blobs are public. 

                    BlobContainerPermissions permissions = new BlobContainerPermissions

                    {

                        PublicAccess = BlobContainerPublicAccessType.Blob

                    };

                    await cloudBlobContainer.SetPermissionsAsync(permissions);



                    byte[] data = new byte[512];
                    Random rnd = new Random();
                    rnd.NextBytes(data);


                    // Get a reference to the blob address, then upload the file to the blob.

                    // Use the value of localFileName for the blob name.

                    CloudPageBlob cloudPageBlob = cloudBlobContainer.GetPageBlobReference("SamplePage");
                    cloudPageBlob.Create(512);


                    cloudPageBlob.WritePages(new MemoryStream(data), 0);


                    // List the blobs in the container.

                    Console.WriteLine("Listing blobs in container.");

                    BlobContinuationToken blobContinuationToken = null;

                    do

                    {

                        var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);

                        // Get the value of the continuation token returned by the listing call.

                        blobContinuationToken = results.ContinuationToken;

                        foreach (IListBlobItem item in results.Results)

                        {

                            Console.WriteLine(item.Uri);

                        }

                    } while (blobContinuationToken != null); // Loop while the continuation token is not null.

                    Console.WriteLine("Read the PageRanges");

                    cloudPageBlob.FetchAttributes();
                    Console.WriteLine("Blob length = {0}", cloudPageBlob.Properties.Length);

                    IEnumerable<PageRange> ranges = cloudPageBlob.GetPageRanges();
                    Console.Write("{0}:<", "Writing Data From PageBlob");
                    foreach (PageRange range in ranges)
                    {
                        Console.Write("[{0}-{1}]", range.StartOffset, range.EndOffset);
                    }
                    Console.WriteLine(">");

                    
                    Console.WriteLine("Delete the PageBlob");
                    await cloudPageBlob.DeleteAsync();


                }

                catch (StorageException ex)

                {

                    Console.WriteLine("Error returned from the service: {0}", ex.Message);

                }

                finally

                {

                   // Console.WriteLine("Press any key to delete the sample files and example container.");

                    Console.ReadLine();

                    // Clean up resources. This includes the container and the two temp files.

                    //Console.WriteLine("Deleting the container and any blobs it contains");

                    if (cloudBlobContainer != null)
                    {

                        // await cloudBlobContainer.DeleteIfExistsAsync();

                    }
                }

            }

            else

            {

                Console.WriteLine(

                    "A connection string has not been defined in the system environment variables. " +

                    "Add a environment variable named 'storageconnectionstring' with your storage " +

                    "connection string as a value.");

            }

        }

        private static async Task AppendBlobProcessAsync()
        {

            CloudStorageAccount storageAccount = null;

            CloudBlobContainer cloudBlobContainer = null;

            string sourceFile = null;

            string destinationFile = null;


            // Retrieve the connection string for use with the application. The storage connection string is stored

            // in an environment variable on the machine running the application called storageconnectionstring.

            // If the environment variable is created after the application is launched in a console or with Visual

            // Studio, the shell needs to be closed and reloaded to take the environment variable into account.

            string storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"]; //Environment.GetEnvironmentVariable("StorageConnectionString");


            //storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

            // Check whether the connection string can be parsed.

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {

                try

                {

                    // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.

                    ////CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

                    //127.0.0.1:10000/devstoreaccount1/testcont

                    // Create a container called 'quickstartblobs' and append a GUID value to it to make the name unique. 

                    ////cloudBlobContainer = cloudBlobClient.GetContainerReference("testcontappendblob");// "quickstartblobs" + Guid.NewGuid().ToString());

                    ////await cloudBlobContainer.CreateIfNotExistsAsync();

                    ////Console.WriteLine("Created container '{0}'", cloudBlobContainer.Name);

                    ////Console.WriteLine();



                    ////// Set the permissions so the blobs are public. 

                    ////BlobContainerPermissions permissions = new BlobContainerPermissions

                    ////{

                    ////    PublicAccess = BlobContainerPublicAccessType.Blob

                    ////};

                    ////await cloudBlobContainer.SetPermissionsAsync(permissions);

                    // Get a reference to the blob address, then upload the file to the blob.

                    // Use the value of localFileName for the blob name.

                   
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    cloudBlobContainer = cloudBlobClient.GetContainerReference("testcont");
                    cloudBlobContainer.CreateIfNotExists();

                    CloudAppendBlob cloudAppendBlob = cloudBlobContainer.GetAppendBlobReference("1.txt");
                    cloudAppendBlob.CreateOrReplace();
                    cloudAppendBlob.AppendText("Content added");
                    cloudAppendBlob.AppendText("More content added");
                    cloudAppendBlob.AppendText("Even more content added");

                    string appendBlobContent = cloudAppendBlob.DownloadText();


                    //cloudAppendBlob.UploadFromFile(sourceFile);





                    // List the blobs in the container.

                    Console.WriteLine("Listing blobs in container.");

                    BlobContinuationToken blobContinuationToken = null;

                    do

                    {

                        var results = await cloudBlobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);

                        // Get the value of the continuation token returned by the listing call.

                        blobContinuationToken = results.ContinuationToken;

                        foreach (IListBlobItem item in results.Results)

                        {

                            Console.WriteLine(item.Uri);

                        }

                    } while (blobContinuationToken != null); // Loop while the continuation token is not null.

                    destinationFile = sourceFile.Replace(".txt", "_DOWNLOADED.txt");

                    Console.WriteLine("Downloading blob to {0}", destinationFile);

                    Console.WriteLine();

                    await cloudAppendBlob.DownloadToFileAsync(destinationFile, FileMode.Create);


                    File.WriteAllText(sourceFile, "Hello, World, I am good, how is your health!");

                    await cloudAppendBlob.UploadFromFileAsync(sourceFile);

                    await cloudAppendBlob.DeleteAsync();


                }

                catch (StorageException ex)

                {

                    Console.WriteLine("Error returned from the service: {0}", ex.Message);

                }

                finally

                {

                    // Console.WriteLine("Press any key to delete the sample files and example container.");

                    Console.ReadLine();

                    // Clean up resources. This includes the container and the two temp files.

                    //Console.WriteLine("Deleting the container and any blobs it contains");

                    if (cloudBlobContainer != null)
                    {

                        // await cloudBlobContainer.DeleteIfExistsAsync();

                    }
                    File.Delete(sourceFile);
                    File.Delete(destinationFile);
                }

            }

            else

            {

                Console.WriteLine(

                    "A connection string has not been defined in the system environment variables. " +

                    "Add a environment variable named 'storageconnectionstring' with your storage " +

                    "connection string as a value.");

            }

        }


    }
}
