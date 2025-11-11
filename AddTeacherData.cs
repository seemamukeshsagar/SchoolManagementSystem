using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

class Program
{
    // Connection string - update as needed for your environment
    private static readonly string ConnectionString = "Data Source=DESKTOP-L9I46P8;Initial Catalog=SchoolManagementSystem;Application Name=Unity Enterprise;Integrated Security=True;Connection Timeout=60;";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Adding 15 teacher records to the database...");
        
        try
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("Database connection successful!");
                
                // Get or create CompanyId
                var companyId = await GetOrCreateCompanyAsync(connection);
                Console.WriteLine($"Using CompanyId: {companyId}");
                
                // Get or create SchoolId
                var schoolId = await GetOrCreateSchoolAsync(connection, companyId);
                Console.WriteLine($"Using SchoolId: {schoolId}");
                
                // Add 15 teachers
                int createdCount = 0;
                var teachers = GetSampleTeachers();
                
                foreach (var teacher in teachers)
                {
                    try
                    {
                        await CreateTeacherAsync(connection, teacher, companyId, schoolId);
                        Console.WriteLine($"Added teacher: {teacher.FirstName} {teacher.LastName}");
                        createdCount++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error adding teacher {teacher.FirstName} {teacher.LastName}: {ex.Message}");
                    }
                }
                
                Console.WriteLine($"Successfully added {createdCount} teacher records!");
                
                // Verify the count
                var totalTeachers = await GetTeacherCountAsync(connection);
                Console.WriteLine($"Total teachers in database: {totalTeachers}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine($"Exception type: {ex.GetType().FullName}");
        }
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
    
    private static async Task<Guid> GetOrCreateCompanyAsync(SqlConnection connection)
    {
        // Try to get existing company
        using (var command = new SqlCommand("SELECT TOP 1 Id FROM CompanyMaster WHERE IsActive = 1 AND IsDeleted = 0", connection))
        {
            var result = await command.ExecuteScalarAsync();
            if (result != null && result != DBNull.Value)
            {
                return (Guid)result;
            }
        }
        
        // Create a new company if none exists
        Console.WriteLine("No active company found. Creating a sample company...");
        var companyId = Guid.NewGuid();
        using (var command = new SqlCommand(@"
            INSERT INTO CompanyMaster (Id, CompanyName, Description, Address, CityId, StateId, CountryId, ZipCode, Email, IsActive, IsDeleted, CreatedBy, CreatedDate, EstablishmentYear, JudistrictionArea, Status, StatusMessage)
            VALUES (@Id, @CompanyName, @Description, @Address, @CityId, @StateId, @CountryId, @ZipCode, @Email, @IsActive, @IsDeleted, @CreatedBy, @CreatedDate, @EstablishmentYear, @JudistrictionArea, @Status, @StatusMessage)", connection))
        {
            command.Parameters.AddWithValue("@Id", companyId);
            command.Parameters.AddWithValue("@CompanyName", "Sample Company");
            command.Parameters.AddWithValue("@Description", "Sample company for testing");
            command.Parameters.AddWithValue("@Address", "123 Main St");
            command.Parameters.AddWithValue("@CityId", Guid.NewGuid());
            command.Parameters.AddWithValue("@StateId", Guid.NewGuid());
            command.Parameters.AddWithValue("@CountryId", Guid.NewGuid());
            command.Parameters.AddWithValue("@ZipCode", "12345");
            command.Parameters.AddWithValue("@Email", "company@example.com");
            command.Parameters.AddWithValue("@IsActive", true);
            command.Parameters.AddWithValue("@IsDeleted", false);
            command.Parameters.AddWithValue("@CreatedBy", Guid.NewGuid());
            command.Parameters.AddWithValue("@CreatedDate", DateTime.UtcNow);
            command.Parameters.AddWithValue("@EstablishmentYear", "2020");
            command.Parameters.AddWithValue("@JudistrictionArea", Guid.NewGuid());
            command.Parameters.AddWithValue("@Status", "ACT");
            command.Parameters.AddWithValue("@StatusMessage", "Active");
            
            await command.ExecuteNonQueryAsync();
        }
        
        return companyId;
    }
    
    private static async Task<Guid> GetOrCreateSchoolAsync(SqlConnection connection, Guid companyId)
    {
        // Try to get existing school
        using (var command = new SqlCommand("SELECT TOP 1 Id FROM SchoolMaster WHERE IsActive = 1 AND IsDeleted = 0", connection))
        {
            var result = await command.ExecuteScalarAsync();
            if (result != null && result != DBNull.Value)
            {
                return (Guid)result;
            }
        }
        
        // Create a new school if none exists
        Console.WriteLine("No active school found. Creating a sample school...");
        var schoolId = Guid.NewGuid();
        using (var command = new SqlCommand(@"
            INSERT INTO SchoolMaster (Id, Name, Description, Email, Address1, CityId, StateId, CountryId, ZipCode, Phone, EstablishmentYear, Mobile, CompanyId, CreatedBy, CreatedDate, Status, StatusMessage)
            VALUES (@Id, @Name, @Description, @Email, @Address1, @CityId, @StateId, @CountryId, @ZipCode, @Phone, @EstablishmentYear, @Mobile, @CompanyId, @CreatedBy, @CreatedDate, @Status, @StatusMessage)", connection))
        {
            command.Parameters.AddWithValue("@Id", schoolId);
            command.Parameters.AddWithValue("@Name", "Sample School");
            command.Parameters.AddWithValue("@Description", "Sample school for testing");
            command.Parameters.AddWithValue("@Email", "school@example.com");
            command.Parameters.AddWithValue("@Address1", "456 School St");
            command.Parameters.AddWithValue("@CityId", Guid.NewGuid());
            command.Parameters.AddWithValue("@StateId", Guid.NewGuid());
            command.Parameters.AddWithValue("@CountryId", Guid.NewGuid());
            command.Parameters.AddWithValue("@ZipCode", "54321");
            command.Parameters.AddWithValue("@Phone", "555-1234");
            command.Parameters.AddWithValue("@EstablishmentYear", "2020");
            command.Parameters.AddWithValue("@Mobile", "555-5678");
            command.Parameters.AddWithValue("@CompanyId", companyId);
            command.Parameters.AddWithValue("@CreatedBy", Guid.NewGuid());
            command.Parameters.AddWithValue("@CreatedDate", DateTime.UtcNow);
            command.Parameters.AddWithValue("@Status", "ACT");
            command.Parameters.AddWithValue("@StatusMessage", "Active");
            
            await command.ExecuteNonQueryAsync();
        }
        
        return schoolId;
    }
    
    private static async Task CreateTeacherAsync(SqlConnection connection, TeacherInfo teacher, Guid companyId, Guid schoolId)
    {
        using (var command = new SqlCommand(@"
            EXEC [dbo].[Teacher_Create]
                @FirstName = @FirstName,
                @LastName = @LastName,
                @DOB = @DOB,
                @DOJ = @DOJ,
                @DateOfLeaving = @DateOfLeaving,
                @Address = @Address,
                @CityId = @CityId,
                @StateId = @StateId,
                @CountryId = @CountryId,
                @ZipCode = @ZipCode,
                @Gender = @Gender,
                @MaritalStatusId = @MaritalStatusId,
                @Image = @Image,
                @Email = @Email,
                @Phone = @Phone,
                @MobilePhone = @MobilePhone,
                @YearsOfExperience = @YearsOfExperience,
                @PreviousSchool = @PreviousSchool,
                @Salutation = @Salutation,
                @IsActive = @IsActive,
                @IsDeleted = @IsDeleted,
                @CompanyId = @CompanyId,
                @SchoolId = @SchoolId,
                @CreatedBy = @CreatedBy,
                @Status = @Status,
                @StatusMessage = @StatusMessage", connection))
        {
            var createdBy = Guid.NewGuid();
            
            command.Parameters.AddWithValue("@FirstName", teacher.FirstName);
            command.Parameters.AddWithValue("@LastName", teacher.LastName);
            command.Parameters.AddWithValue("@DOB", teacher.DOB);
            command.Parameters.AddWithValue("@DOJ", DBNull.Value);
            command.Parameters.AddWithValue("@DateOfLeaving", DBNull.Value);
            command.Parameters.AddWithValue("@Address", "123 Main Street");
            command.Parameters.AddWithValue("@CityId", DBNull.Value);
            command.Parameters.AddWithValue("@StateId", DBNull.Value);
            command.Parameters.AddWithValue("@CountryId", DBNull.Value);
            command.Parameters.AddWithValue("@ZipCode", "12345");
            command.Parameters.AddWithValue("@Gender", DBNull.Value);
            command.Parameters.AddWithValue("@MaritalStatusId", DBNull.Value);
            command.Parameters.AddWithValue("@Image", "");
            command.Parameters.AddWithValue("@Email", teacher.Email);
            command.Parameters.AddWithValue("@Phone", teacher.Phone);
            command.Parameters.AddWithValue("@MobilePhone", teacher.Phone);
            command.Parameters.AddWithValue("@YearsOfExperience", teacher.YearsOfExperience);
            command.Parameters.AddWithValue("@PreviousSchool", teacher.PreviousSchool);
            command.Parameters.AddWithValue("@Salutation", teacher.Salutation);
            command.Parameters.AddWithValue("@IsActive", true);
            command.Parameters.AddWithValue("@IsDeleted", false);
            command.Parameters.AddWithValue("@CompanyId", companyId);
            command.Parameters.AddWithValue("@SchoolId", schoolId);
            command.Parameters.AddWithValue("@CreatedBy", createdBy);
            command.Parameters.AddWithValue("@Status", "ACT");
            command.Parameters.AddWithValue("@StatusMessage", "Active");
            
            await command.ExecuteNonQueryAsync();
        }
    }
    
    private static async Task<int> GetTeacherCountAsync(SqlConnection connection)
    {
        using (var command = new SqlCommand("SELECT COUNT(*) FROM TeacherMaster", connection))
        {
            var result = await command.ExecuteScalarAsync();
            return result != null ? (int)result : 0;
        }
    }
    
    private static TeacherInfo[] GetSampleTeachers()
    {
        return new TeacherInfo[]
        {
            new TeacherInfo { FirstName = "Suman", LastName = "Sharma", Email = "john.anderson@gmail.com", Phone = "555-0101", DOB = new DateTime(1980, 5, 15), YearsOfExperience = "10", PreviousSchool = "Previous School 1", Salutation = "Mr." },
            new TeacherInfo { FirstName = "Anil", LastName = "Kumar", Email = "sarah.johnson@gmail.com", Phone = "555-0102", DOB = new DateTime(1982, 8, 22), YearsOfExperience = "8", PreviousSchool = "Previous School 2", Salutation = "Ms." },
            new TeacherInfo { FirstName = "Seema", LastName = "Sagar", Email = "michael.williams@gmail.com", Phone = "555-0103", DOB = new DateTime(1978, 11, 30), YearsOfExperience = "15", PreviousSchool = "Previous School 3", Salutation = "Mr." },
            new TeacherInfo { FirstName = "Sunita", LastName = "Sharma", Email = "emily.brown@gmail.com", Phone = "555-0104", DOB = new DateTime(1985, 3, 12), YearsOfExperience = "6", PreviousSchool = "Previous School 4", Salutation = "Ms." },
            new TeacherInfo { FirstName = "Manali", LastName = "Patel", Email = "david.jones@gmail.com", Phone = "555-0105", DOB = new DateTime(1981, 7, 19), YearsOfExperience = "9", PreviousSchool = "Previous School 5", Salutation = "Mr." },
            new TeacherInfo { FirstName = "Savita", LastName = "Verma", Email = "jennifer.garcia@gmail.com", Phone = "555-0106", DOB = new DateTime(1983, 1, 25), YearsOfExperience = "7", PreviousSchool = "Previous School 6", Salutation = "Ms." },
            new TeacherInfo { FirstName = "Meenu", LastName = "Sharma", Email = "chris.miller@gmail.com", Phone = "555-0107", DOB = new DateTime(1979, 9, 14), YearsOfExperience = "12", PreviousSchool = "Previous School 7", Salutation = "Mr." },
            new TeacherInfo { FirstName = "Manisha", LastName = "Sharma", Email = "amanda.davis@gmail.com", Phone = "555-0108", DOB = new DateTime(1984, 12, 3), YearsOfExperience = "5", PreviousSchool = "Previous School 8", Salutation = "Ms." },
            new TeacherInfo { FirstName = "Monika", LastName = "Verma", Email = "james.rodriguez@gmail.com", Phone = "555-0109", DOB = new DateTime(1980, 4, 8), YearsOfExperience = "11", PreviousSchool = "Previous School 9", Salutation = "Mr." },
            new TeacherInfo { FirstName = "Vinita", LastName = "Arya", Email = "jessica.martinez@gmail.com", Phone = "555-0110", DOB = new DateTime(1986, 6, 17), YearsOfExperience = "4", PreviousSchool = "Previous School 10", Salutation = "Ms." },
            new TeacherInfo { FirstName = "Sonakshi", LastName = "Sinha", Email = "robert.hernandez@gmail.com", Phone = "555-0111", DOB = new DateTime(1977, 2, 28), YearsOfExperience = "18", PreviousSchool = "Previous School 11", Salutation = "Mr." },
            new TeacherInfo { FirstName = "Ajay", LastName = "Kumar", Email = "melissa.lopez@gmail.com", Phone = "555-0112", DOB = new DateTime(1983, 10, 9), YearsOfExperience = "7", PreviousSchool = "Previous School 12", Salutation = "Ms." },
            new TeacherInfo { FirstName = "Anil", LastName = "Sharma", Email = "william.gonzalez@gmail.com", Phone = "555-0113", DOB = new DateTime(1981, 12, 21), YearsOfExperience = "9", PreviousSchool = "Previous School 13", Salutation = "Mr." },
            new TeacherInfo { FirstName = "Ashley", LastName = "Wilson", Email = "ashley.wilson@gmail.com", Phone = "555-0114", DOB = new DateTime(1985, 7, 13), YearsOfExperience = "6", PreviousSchool = "Previous School 14", Salutation = "Ms." },
            new TeacherInfo { FirstName = "Daniel", LastName = "Taylor", Email = "daniel.taylor@gmail.com", Phone = "555-0115", DOB = new DateTime(1982, 9, 5), YearsOfExperience = "8", PreviousSchool = "Previous School 15", Salutation = "Mr." }
        };
    }
}

class TeacherInfo
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime DOB { get; set; }
    public string YearsOfExperience { get; set; }
    public string PreviousSchool { get; set; }
    public string Salutation { get; set; }
}