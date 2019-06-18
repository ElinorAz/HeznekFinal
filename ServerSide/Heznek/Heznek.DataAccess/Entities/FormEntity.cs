using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class FormEntity:PersistentEntity<int>
    {
        public FormStatusEnum Status { get; set; }

        public string UserId { get; set; }
        public UserEntity User { get; set; }

        public FormParentsSalaryEntity ParentsSalary { get; set; }
        public List<FormTaskEntity> Tasks { get; set; }
    }
}
