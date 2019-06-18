using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class FormParentsSalaryEntity:PersistentEntity<int>
    {
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }

        public string SalarySlips { get; set; }
        public string SalarySlipsDownloadName { get; set; }
        public string Disability { get; set; }
        public string DisabilityDownloadName { get; set; }

        public string Disability2 { get; set; }
        public string Disability2DownloadName { get; set; }

        //public ParentEnum? Parent { get; set; }
        public bool FatherDisability { get; set; }
        public bool MotherDisability { get; set; }
        public DateTime? LastUpdated { get; set; }

        public int FormId { get; set; }
        public FormEntity Form { get; set; }
    }
}
