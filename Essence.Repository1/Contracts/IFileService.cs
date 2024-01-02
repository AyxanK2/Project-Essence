using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Contracts
{
    public interface IFileService
    {
        public void FileDelete(string folder, string file);
        public Task<string> FileUpload(IFormFile file, string folder);
    }
}
