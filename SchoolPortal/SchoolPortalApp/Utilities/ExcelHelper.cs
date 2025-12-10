using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using SchoolPortalApp.Models;

namespace SchoolPortalApp.Utilities
{
    public static class ExcelHelper
    {
        public static byte[] GenerateStudentTemplate()
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Students");
            
            // Headers
            var headers = new[]
            {
                "RollNumber", "FirstName", "LastName", "Gender", "DOB (dd/MM/yyyy)", "Email", "Phone",
                "Address", "City", "State", "Country", "ZipCode",
                "ParentFirstName", "ParentLastName", "ParentEmail", "ParentPhone", "ParentOccupation",
                "ParentRelation", "ParentAddress", "ParentCity", "ParentState", "ParentCountry", "ParentZipCode"
            };

            // Add headers
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cells[1, i + 1].Value = headers[i];
            }

            // Add some sample data in row 2
            var sampleData = new[]
            {
                "1001", "John", "Doe", "Male", "01/01/2010", "john.doe@example.com", "+1234567890",
                "123 Main St", "Mumbai", "Maharashtra", "India", "400001",
                "James", "Doe", "james.doe@example.com", "+1234567891", "Business",
                "Father", "123 Main St", "Mumbai", "Maharashtra", "India", "400001"
            };

            for (int i = 0; i < sampleData.Length; i++)
            {
                worksheet.Cells[2, i + 1].Value = sampleData[i];
            }

            // Auto-fit columns
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}
