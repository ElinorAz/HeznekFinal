using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class FormModel
    {
        public string UserId { get; set; }
        public FormStatusEnum Status { get; set; }

        public FormParentsSalaryModel ParentsSalary { get; set; }
        public List<FormTaskModel> Tasks { get; set; }
    }
}
