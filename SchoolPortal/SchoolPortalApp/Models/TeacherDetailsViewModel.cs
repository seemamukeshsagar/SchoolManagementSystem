#nullable enable

using System.Collections.Generic;
using SchoolPortal.Entities.Models;

namespace SchoolPortalApp.Models
{
    public class TeacherDetailsViewModel
    {
        public TeacherMaster Master { get; set; } = new TeacherMaster();
        public List<TeacherDocumentDetails> Documents { get; set; } = new List<TeacherDocumentDetails>();
        public List<TeacherQualificationDetails> Qualifications { get; set; } = new List<TeacherQualificationDetails>();
    }
}
