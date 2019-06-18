using Heznek.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class FormParentsSalaryModel:PersistentModel<int>
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime? LastUpdated { get; set; }

        public string SalarySlipsFileName { get; set; }
        public string SalarySlipsDownloadName { get; set; }
        public IFormFile SalarySlips { get; set; }

        public string DisabilityFileName { get; set; }
        public string DisabilityDownloadName { get; set; }
        public IFormFile Disability { get; set; }

        public string Disability2FileName { get; set; }
        public string Disability2DownloadName { get; set; }
        public IFormFile Disability2 { get; set; }

        public bool FatherDisability { get; set; }
        public bool MotherDisability { get; set; }
    }
}
