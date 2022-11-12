using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MiniECommerce.Application.Services;
using MiniECommerce.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<bool> CopyAsync(string path, IFormFile file)
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

        public async Task<string> RenameAsync(string name, string? path)
        {
            return await Task.Run(() =>
            {

                string newName = StringOperation.CharacterRegulatory(Path.GetFileNameWithoutExtension(name)) + Path.GetExtension(name);

                string fullPath = Path.Combine(path, newName);

                while (File.Exists(fullPath))
                {
                    string beChecked = newName.Substring(newName.LastIndexOf('-') + 1, newName.LastIndexOf('.') - newName.LastIndexOf('-') - 1);

                    if (int.TryParse(beChecked, out int result))
                    {
                        result++;
                        newName = newName.Remove(newName.LastIndexOf(beChecked), beChecked.Length).Replace(".", $"{result}.");
                    }
                    else
                        newName = newName.Replace(".", "-1.");

                    fullPath = Path.Combine(path, newName);
                }

                return newName;
            });

        }

        public async Task<List<(string path, string name)>> UploadAsync(string directoryPath, IFormFileCollection files)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, directoryPath);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            List<bool> results = new();
            List<(string path, string name)> datas = new();

            foreach (IFormFile file in files)
            {
                string newName = await RenameAsync(file.FileName, path);
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
    }
}
