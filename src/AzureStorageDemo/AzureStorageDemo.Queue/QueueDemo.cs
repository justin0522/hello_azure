using AzureStorageDemo.Common;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageDemo.Queue
{
    public class QueueDemo
    {
        public static void CallAllMethods()
        {
            PushPop();
        }

        static void PushPop()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccountHelper.Instance;

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            CloudQueue queue = queueClient.GetQueueReference("myqueue");

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();

            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage("Hello, World");
            queue.AddMessage(message);

            // Update Message Content
            CloudQueueMessage updateMessage = queue.GetMessage();
            updateMessage.SetMessageContent("Updated contents.");
            queue.UpdateMessage(updateMessage,
                TimeSpan.FromSeconds(60.0),  // Make it invisible for another 60 seconds.
                MessageUpdateFields.Content | MessageUpdateFields.Visibility);

            // Peek at the next message
            CloudQueueMessage peekedMessage = queue.PeekMessage();

            // Display message.
            Console.WriteLine(peekedMessage.AsString);

            // Delete message
            CloudQueueMessage retrievedMessage = queue.GetMessage();
            queue.DeleteMessage(retrievedMessage);


        }
    }
}
