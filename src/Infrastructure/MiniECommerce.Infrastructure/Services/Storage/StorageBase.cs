using MiniECommerce.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Services.Storage
{
    public abstract class StorageBase
    {
        protected async Task<string> RenameAsync(string name, string path, Func<string, string, Task<bool>> hasFile)
        {
            return await Task.Run(async () =>
            {
                string newName = StringOperation.CharacterRegulatory(Path.GetFileNameWithoutExtension(name)) + Path.GetExtension(name);

                while (await hasFile.Invoke(path, newName))
                {
                    string beChecked = newName.Substring(newName.LastIndexOf('-') + 1, newName.LastIndexOf('.') - newName.LastIndexOf('-') - 1);

                    if (int.TryParse(beChecked, out int result))
                    {
                        result++;
                        newName = newName.Remove(newName.LastIndexOf(beChecked), beChecked.Length).Replace(".", $"{result}.");
                    }
                    else
                        newName = newName.Replace(".", "-1.");
                }

                return newName;
            });

        }
    }
}
