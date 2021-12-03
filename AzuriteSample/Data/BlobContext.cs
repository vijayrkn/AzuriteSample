using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzuriteSample.Data
{
    public class BlobContext
    {
        public const string BlobContainerName = "work-items";

        private BlobContainerClient _containerClient;
        public BlobContext(BlobServiceClient blobServiceClient)
        {
            var serviceClient = blobServiceClient;
            _containerClient = serviceClient.GetBlobContainerClient(BlobContainerName);
            _containerClient.CreateIfNotExistsAsync();
        }

        public async Task<bool> UploadBlobAsync(string fileName, byte[] fileContents)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(fileName);
            using Stream stream = new MemoryStream(fileContents);
            await blobClient.UploadAsync(stream, overwrite: true);
            return true;
        }

        public async Task<bool> DeleteBlobAsync(string fileName)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();

            return true;
        }

        public async Task<byte[]?> DownloadBlobAsync(string fileName)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(fileName);
            if (await blobClient.ExistsAsync())
            {
                using MemoryStream stream = new MemoryStream();
                await blobClient.DownloadToAsync(stream);
                return stream.ToArray();
            }

            return null;
        }

        public async Task<IList<string>> ListBlobsAsync()
        {
            List<string> names = new List<string>();
            await foreach (BlobItem blob in _containerClient.GetBlobsAsync())
            {
                names.Add(blob.Name);
            }

            return names;
        }
    }
}
