using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class FormTaskModel: PersistentModel<int>
    {
        public string Name { get; set; }

        public DateTime? LastUpdated { get; set; }

        public string FileName { get; set; }
        public string DownloadName { get; set; }
        public IFormFile File { get; set; }
    }
}
