using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.App.Service.Interface
{
    public interface IBlob
    {
        Task<string> Upload(IFormFile model);
    }
}
