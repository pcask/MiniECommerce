using Microsoft.AspNetCore.Http;
using MiniECommerce.Application.Abstractions.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName => _storage.GetType().Name;

        public async Task Delete(string pathOrContainerName, string fileName)
            => await _storage.Delete(pathOrContainerName, fileName);

        public async Task<List<string>> GetFiles(string pathOrContainerName)
         => await _storage.GetFiles(pathOrContainerName);

        public async Task<bool> HasFile(string pathOrContainerName, string fileName)
         => await _storage.HasFile(pathOrContainerName, fileName);

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
         => await _storage.UploadAsync(pathOrContainerName, files);
    }
}
