using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQOO7FirstWebApp.ViewModels
{
    public class FileUploadViewModel
    {
        public IFormFile Photo { get; set; }
    }
}
