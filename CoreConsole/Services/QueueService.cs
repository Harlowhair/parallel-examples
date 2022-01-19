using CoreConsole.Config;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreConsole.Services
{
    public class QueueService : IQueueService
    {
        private readonly IOptions<QueueConfig> _config;


        public QueueService(IOptions<QueueConfig> config)
        {
            _config = config;
        }

        public async Task SendMessage(string newMessage)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_config.Value.ConnectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(_config.Value.DefaultQueue);

            bool createdQueue = queue.CreateIfNotExists();

            if (createdQueue)
            {
                Console.WriteLine("The queue was created.");
            }

            CloudQueueMessage message = new CloudQueueMessage(newMessage);
            await queue.AddMessageAsync(message);
        }

        public async Task<string> ReceiveMessage()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_config.Value.ConnectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference(_config.Value.DefaultQueue);

            bool exists = queue.Exists();

            if (exists)
            {
                CloudQueueMessage retrievedMessage = await queue.GetMessageAsync();

                if (retrievedMessage != null)
                {
                    string theMessage = retrievedMessage.AsString;
                    await queue.DeleteMessageAsync(retrievedMessage);
                    return theMessage;
                }
            }
            return null;
        }
    }
}
