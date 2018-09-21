using AzureStorageDemo.Blob;
using AzureStorageDemo.Common;
using AzureStorageDemo.Queue;
using AzureStorageDemo.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            #region blob
            //BlobDemo.CallAllMethods();

            //Advanced.CallBlobAdvancedSamples().Wait();
            #endregion

            #region queue
            //QueueDemo.CallAllMethods();

            #endregion

            #region tables
            TablesDemo.CallAllMethods();

            #endregion
        }
    }
}
