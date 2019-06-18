using Heznek.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class AcademicStudiesModel
    {
        public string AcademicDegree { get; set; }
        public string FieldOfStudy { get; set; }
        public string AcademicInstitution { get; set; }
        public ResidenceEnum? Residence { get; set; }
        public int? GraduationYear { get; set; }
        public int? BeginningDegree { get; set; }
        public string Tuition { get; set; }
        public bool OtherFundings { get; set; }
        public string FundStudies { get; set; }
        public int? StudyYear { get; set; }
        public TypeOfDegreeEnum TypeOfDegree { get; set; }

        public string AprovalFileName { get; set; }
        public IFormFile Aproval { get; set; }

        public string GradesFileName { get; set; }
        public IFormFile Grades { get; set; }

        public string AprovalDownloadName { get; set; }
        public string GradesDownloadName { get; set; }

    }
}
