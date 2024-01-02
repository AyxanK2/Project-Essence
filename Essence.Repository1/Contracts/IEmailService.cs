using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essence.Repository1.Contracts
{
    public interface IEmailService
    {
        public Task SendEmail(string email, string subject, string message);
    }
}
