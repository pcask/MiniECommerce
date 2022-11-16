using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MiniECommerce.Application.Abstractions.Storage.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage : StorageBase, IAzureStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        private BlobContainerClient _blobContainerClient;

        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration["Storage:Azure"]);
        }

        public async Task Delete(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.DeleteBlobAsync(fileName);
        }

        public async Task<List<string>> GetFiles(string containerName)
        {
            return await Task.Run(() =>
             {
                 _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                 return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
             });
        }

        public async Task<bool> HasFile(string containerName, string fileName)
        {
            return await Task.Run(() =>
            {
                _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
            });
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string pathOrContainerName)> datas = new();

            foreach (IFormFile file in files)
            {
                string newName = await RenameAsync(file.FileName, containerName, HasFile);
                await _blobContainerClient.UploadBlobAsync(newName, file.OpenReadStream());
                datas.Add((newName, $"{containerName}/{newName}"));
            }

            return datas;
        }
    }
}
