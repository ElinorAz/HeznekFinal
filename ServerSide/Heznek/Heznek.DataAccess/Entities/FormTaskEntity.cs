using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class FormTaskEntity:PersistentEntity<int>
    {
        public string Name { get; set; }

        public DateTime? LastUpdated { get; set; }
        public string FileName { get; set; }
        public string DownloadName { get; set; }

        public int FormId { get; set; }
        public FormEntity Form { get; set; }
    }
}
