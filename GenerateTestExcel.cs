using System;
using ClosedXML.Excel;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Teachers");
                var currentRow = 1;
                
                // Header row
                worksheet.Cell(currentRow, 1).Value = "First Name";
                worksheet.Cell(currentRow, 2).Value = "Last Name";
                worksheet.Cell(currentRow, 3).Value = "Date of Birth (YYYY-MM-DD)";
                worksheet.Cell(currentRow, 4).Value = "Date of Joining (YYYY-MM-DD)";
                worksheet.Cell(currentRow, 5).Value = "Email";
                worksheet.Cell(currentRow, 6).Value = "Phone";
                worksheet.Cell(currentRow, 7).Value = "Mobile Phone";
                worksheet.Cell(currentRow, 8).Value = "Address";
                worksheet.Cell(currentRow, 9).Value = "City";
                worksheet.Cell(currentRow, 10).Value = "State";
                worksheet.Cell(currentRow, 11).Value = "Country";
                worksheet.Cell(currentRow, 12).Value = "Zip Code";
                worksheet.Cell(currentRow, 13).Value = "Gender";
                worksheet.Cell(currentRow, 14).Value = "Marital Status";
                worksheet.Cell(currentRow, 15).Value = "Years of Experience";
                worksheet.Cell(currentRow, 16).Value = "Previous School";
                worksheet.Cell(currentRow, 17).Value = "Salutation";
                worksheet.Cell(currentRow, 18).Value = "Is Active";
                worksheet.Cell(currentRow, 19).Value = "School Name";
                
                // Sample data rows
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "John";
                worksheet.Cell(currentRow, 2).Value = "Doe";
                worksheet.Cell(currentRow, 3).Value = "1980-01-15";
                worksheet.Cell(currentRow, 4).Value = "2020-06-01";
                worksheet.Cell(currentRow, 5).Value = "john.doe@school.edu";
                worksheet.Cell(currentRow, 6).Value = "123-456-7890";
                worksheet.Cell(currentRow, 7).Value = "987-654-3210";
                worksheet.Cell(currentRow, 8).Value = "123 Main St";
                worksheet.Cell(currentRow, 9).Value = "New York";
                worksheet.Cell(currentRow, 10).Value = "NY";
                worksheet.Cell(currentRow, 11).Value = "USA";
                worksheet.Cell(currentRow, 12).Value = "10001";
                worksheet.Cell(currentRow, 13).Value = "Male";
                worksheet.Cell(currentRow, 14).Value = "Married";
                worksheet.Cell(currentRow, 15).Value = "10";
                worksheet.Cell(currentRow, 16).Value = "Previous School";
                worksheet.Cell(currentRow, 17).Value = "Mr.";
                worksheet.Cell(currentRow, 18).Value = "Yes";
                worksheet.Cell(currentRow, 19).Value = "Test School";
                
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = "Jane";
                worksheet.Cell(currentRow, 2).Value = "Smith";
                worksheet.Cell(currentRow, 3).Value = "1985-03-22";
                worksheet.Cell(currentRow, 4).Value = "2019-09-15";
                worksheet.Cell(currentRow, 5).Value = "jane.smith@school.edu";
                worksheet.Cell(currentRow, 6).Value = "234-567-8901";
                worksheet.Cell(currentRow, 7).Value = "876-543-2109";
                worksheet.Cell(currentRow, 8).Value = "456 Oak Ave";
                worksheet.Cell(currentRow, 9).Value = "Los Angeles";
                worksheet.Cell(currentRow, 10).Value = "CA";
                worksheet.Cell(currentRow, 11).Value = "USA";
                worksheet.Cell(currentRow, 12).Value = "90210";
                worksheet.Cell(currentRow, 13).Value = "Female";
                worksheet.Cell(currentRow, 14).Value = "Single";
                worksheet.Cell(currentRow, 15).Value = "8";
                worksheet.Cell(currentRow, 16).Value = "Another School";
                worksheet.Cell(currentRow, 17).Value = "Ms.";
                worksheet.Cell(currentRow, 18).Value = "Yes";
                worksheet.Cell(currentRow, 19).Value = "Test School";
                
                // Add data validation for "Is Active" column
                var activeColumn = worksheet.Column(18);
                activeColumn.Cells(2, 100).DataValidation.List("\"Yes,No\"", true);
                
                // Auto-fit columns
                worksheet.Columns().AdjustToContents();
                
                // Save the file
                workbook.SaveAs("TestTeacherImport.xlsx");
                Console.WriteLine("Test Excel file created successfully!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating Excel file: {ex.Message}");
        }
    }
}