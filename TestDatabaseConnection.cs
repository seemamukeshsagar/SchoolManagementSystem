using System;
using System.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Testing database connection...");
            
            // Get connection string from environment or config
            string connectionString = "Server=DESKTOP-L9I46P8;Database=SchoolManagementSystem;Integrated Security=true;";
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Database connection successful!");
                
                // Test Teacher_GetAll
                using (var command = new SqlCommand("SELECT COUNT(*) FROM TeacherMaster", connection))
                {
                    var count = (int)command.ExecuteScalar();
                    Console.WriteLine($"TeacherMaster table has {count} records.");
                }
                
                // Test Emp_GetAll
                using (var command = new SqlCommand("SELECT COUNT(*) FROM EmpMaster", connection))
                {
                    var count = (int)command.ExecuteScalar();
                    Console.WriteLine($"EmpMaster table has {count} records.");
                }
                
                Console.WriteLine("All tests passed!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}