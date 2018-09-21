using System;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;

namespace AzureStorageDemo.Common
{
    public class CloudStorageAccountHelper
    {
        public static CloudStorageAccount Instance
        {
            get { return GetAccount().Value; }
        }

        static Lazy<CloudStorageAccount> GetAccount()
        {
            return new Lazy<CloudStorageAccount>(()=>
            {
                return CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            });
        }
    }
}
