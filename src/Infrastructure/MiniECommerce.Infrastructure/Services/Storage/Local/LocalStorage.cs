using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MiniECommerce.Application.Abstractions.Storage.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Delete(string directoryPath, string fileName)
            => await Task.Run(() => { File.Delete(Path.Combine(directoryPath, fileName)); });

        public async Task<List<string>> GetFiles(string directoryPath)
        {
            return await Task.Run(() =>
             {
                 DirectoryInfo directoryInfo = new(directoryPath);
                 return directoryInfo.GetFiles().Select(f => f.Name).ToList();
             });
        }

        public async Task<bool> HasFile(string directoryPath, string fileName)
        {
            return await Task.Run(() =>
              {
                  return File.Exists(Path.Combine(directoryPath, fileName));
              });
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string directoryPath, IFormFileCollection files)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, directoryPath);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            List<bool> results = new();
            List<(string path, string name)> datas = new();

            foreach (IFormFile file in files)
            {
                //todo RenameAsync method'ı tüm storage mimarisinin ihtiyaçlarını karşılayacak şekilde tekrar yazılacak.
                //string newName = await RenameAsync(file.FileName, path);
                string newName = file.FileName;

                string fullPath = Path.Combine(path, newName);

                datas.Add((directoryPath, newName));

                bool result = await CopyAsync(fullPath, file);
                results.Add(result);
            }

            if (results.TrueForAll(r => r.Equals(true)))
                return datas;

            //todo log!
            throw new Exception("Dosyalar sunucuya yüklenirken beklenmedik bir hata meydana geldi. Lütfen sayfayı yenileyip tekrar deneyiniz.");
        }

        async Task<bool> CopyAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }
        }
    }
}
