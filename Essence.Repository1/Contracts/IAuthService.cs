using Essence.Data.DTO.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Contracts
{
    public interface IAuthService
    {
        public Task<(int, string)> Login(LoginDTO model);
    }

}
