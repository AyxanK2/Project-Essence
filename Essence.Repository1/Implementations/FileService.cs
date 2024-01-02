using Essence.Repository1.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Essence.Repository1.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void FileDelete(string folder, string file)
        {
            string fullPath = Path.Combine(_env.WebRootPath, "uploads", folder, file);
            if (File.Exists(fullPath)) File.Delete(fullPath);
        }

        public async Task<string> FileUpload(IFormFile file, string folder)
        {
            string folderPath = Path.Combine(_env.WebRootPath,"uploads", folder);
            string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string fullPath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;

        }
    }

}
