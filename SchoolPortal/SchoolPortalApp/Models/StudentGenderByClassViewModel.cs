using System;
using System.Collections.Generic;

namespace SchoolPortalApp.Models
{
    public class StudentGenderByClassViewModel
    {
        public string ClassName { get; set; }
        public int BoysCount { get; set; }
        public int GirlsCount { get; set; }
    }

    public class StudentGenderByClassChartViewModel
    {
        public List<StudentGenderByClassViewModel> Data { get; set; } = new List<StudentGenderByClassViewModel>();
        public bool HasData { get; set; }
    }
}