using Azure.Storage.Blobs;
using Bookstore.App.Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.App.Service.Repository
{
    public class BlobRepository :IBlob
    {
        private readonly string Container = "textimages";
        private readonly BlobServiceClient _blobServiceClient;
        public BlobRepository(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }


        public async Task<string> Upload(IFormFile model)
        {
            var blobClient = GetBlobServiceClient(model.FileName);
            await blobClient.UploadAsync(model.OpenReadStream(), overwrite: true);
            return blobClient.Uri.AbsoluteUri;

        }
        private BlobClient GetBlobServiceClient(string name)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(Container);
            var blobClient = blobContainer.GetBlobClient(name);
            return blobClient;
        }
    }
}
