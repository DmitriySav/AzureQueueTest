using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue;
using System;

namespace AzureQueueTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
               CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("myqueue");
            queue.CreateIfNotExists();
            for (int i = 0; i < 10; i++)
            {
                queue.AddMessage(new CloudQueueMessage("Hello" + i));
            }
            queue.FetchAttributes();

            // Retrieve the cached approximate message count.
            int? cachedMessageCount = queue.ApproximateMessageCount;

            // Display number of messages.
            Console.WriteLine("Number of messages in queue: " + cachedMessageCount);
            Console.ReadKey();
        }
    }
}
