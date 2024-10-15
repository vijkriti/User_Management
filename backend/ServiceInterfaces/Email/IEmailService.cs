using DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInterfaces.Email
{
    public interface IEmailService
    {
        void SendEmail(EmailDto emailDto);
    }
}
