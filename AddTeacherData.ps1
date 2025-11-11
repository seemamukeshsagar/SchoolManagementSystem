# Add Teacher Data Script
# This script adds 15 sample teacher records to the SchoolManagementSystem database

Write-Host "Adding 15 teacher records to the database..." -ForegroundColor Green

# Database connection
try {
    $machineName = $env:COMPUTERNAME
    $connectionString = "Data Source=$machineName\SQl2025;Initial Catalog=SchoolManagementSystem;Application Name=Unity Enterprise;Integrated Security=True"
    $connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $connection.Open()
    Write-Host "Database connection successful!" -ForegroundColor Green
    
    # Get existing CompanyId and SchoolId
    Write-Host "Retrieving existing Company and School information..." -ForegroundColor Yellow
    
    $companyCommand = New-Object System.Data.SqlClient.SqlCommand("SELECT TOP 1 Id FROM CompanyMaster WHERE IsActive = 1 AND IsDeleted = 0", $connection)
    $companyId = $companyCommand.ExecuteScalar()
    
    if ($companyId -eq $null) {
        Write-Host "No active company found. Creating a sample company..." -ForegroundColor Yellow
        $companyId = [Guid]::NewGuid()
        $createCompanyCommand = New-Object System.Data.SqlClient.SqlCommand("
            INSERT INTO CompanyMaster (Id, CompanyName, Description, Address, CityId, StateId, CountryId, ZipCode, Email, IsActive, IsDeleted, CreatedBy, CreatedDate, EstablishmentYear, JudistrictionArea, Status, StatusMessage)
            VALUES ('$companyId', 'Sample Company', 'Sample company for testing', '123 Main St', NEWID(), NEWID(), NEWID(), '12345', 'company@example.com', 1, 0, NEWID(), GETUTCDATE(), '2020', NEWID(), 'ACT', 'Active')
        ", $connection)
        $createCompanyCommand.ExecuteNonQuery()
    }
    
    $schoolCommand = New-Object System.Data.SqlClient.SqlCommand("SELECT TOP 1 Id FROM SchoolMaster WHERE IsActive = 1 AND IsDeleted = 0", $connection)
    $schoolId = $schoolCommand.ExecuteScalar()
    
    if ($schoolId -eq $null) {
        Write-Host "No active school found. Creating a sample school..." -ForegroundColor Yellow
        $schoolId = [Guid]::NewGuid()
        $createSchoolCommand = New-Object System.Data.SqlClient.SqlCommand("
            INSERT INTO SchoolMaster (Id, Name, Description, Email, Address1, CityId, StateId, CountryId, ZipCode, Phone, EstablishmentYear, Mobile, CompanyId, CreatedBy, CreatedDate, Status, StatusMessage)
            VALUES ('$schoolId', 'Sample School', 'Sample school for testing', 'school@example.com', '456 School St', NEWID(), NEWID(), NEWID(), '54321', '555-1234', '2020', '555-5678', '$companyId', NEWID(), GETUTCDATE(), 'ACT', 'Active')
        ", $connection)
        $createSchoolCommand.ExecuteNonQuery()
    }
    
    Write-Host "Using CompanyId: $companyId" -ForegroundColor Cyan
    Write-Host "Using SchoolId: $schoolId" -ForegroundColor Cyan
    
    # Sample teacher data
    $teachers = @(
        @{ FirstName = "John"; LastName = "Anderson"; Email = "john.anderson@school.edu"; Phone = "555-0101"; DOB = "1980-05-15" },
        @{ FirstName = "Sarah"; LastName = "Johnson"; Email = "sarah.johnson@school.edu"; Phone = "555-0102"; DOB = "1982-08-22" },
        @{ FirstName = "Michael"; LastName = "Williams"; Email = "michael.williams@school.edu"; Phone = "555-0103"; DOB = "1978-11-30" },
        @{ FirstName = "Emily"; LastName = "Brown"; Email = "emily.brown@school.edu"; Phone = "555-0104"; DOB = "1985-03-12" },
        @{ FirstName = "David"; LastName = "Jones"; Email = "david.jones@school.edu"; Phone = "555-0105"; DOB = "1981-07-19" },
        @{ FirstName = "Jennifer"; LastName = "Garcia"; Email = "jennifer.garcia@school.edu"; Phone = "555-0106"; DOB = "1983-01-25" },
        @{ FirstName = "Christopher"; LastName = "Miller"; Email = "chris.miller@school.edu"; Phone = "555-0107"; DOB = "1979-09-14" },
        @{ FirstName = "Amanda"; LastName = "Davis"; Email = "amanda.davis@school.edu"; Phone = "555-0108"; DOB = "1984-12-03" },
        @{ FirstName = "James"; LastName = "Rodriguez"; Email = "james.rodriguez@school.edu"; Phone = "555-0109"; DOB = "1980-04-08" },
        @{ FirstName = "Jessica"; LastName = "Martinez"; Email = "jessica.martinez@school.edu"; Phone = "555-0110"; DOB = "1986-06-17" },
        @{ FirstName = "Robert"; LastName = "Hernandez"; Email = "robert.hernandez@school.edu"; Phone = "555-0111"; DOB = "1977-02-28" },
        @{ FirstName = "Melissa"; LastName = "Lopez"; Email = "melissa.lopez@school.edu"; Phone = "555-0112"; DOB = "1983-10-09" },
        @{ FirstName = "William"; LastName = "Gonzalez"; Email = "william.gonzalez@school.edu"; Phone = "555-0113"; DOB = "1981-12-21" },
        @{ FirstName = "Ashley"; LastName = "Wilson"; Email = "ashley.wilson@school.edu"; Phone = "555-0114"; DOB = "1985-07-13" },
        @{ FirstName = "Daniel"; LastName = "Taylor"; Email = "daniel.taylor@school.edu"; Phone = "555-0115"; DOB = "1982-09-05" }
    )
    
    # Add teachers using the Teacher_Create stored procedure
    $createdCount = 0
    foreach ($teacher in $teachers) {
        try {
            $teacherId = [Guid]::NewGuid()
            $createdBy = [Guid]::NewGuid()
            $firstName = $teacher.FirstName
            $lastName = $teacher.LastName
            $email = $teacher.Email
            $phone = $teacher.Phone
            $dob = $teacher.DOB
            $address = "123 Main Street"
            $zipCode = "12345"
            $yearsOfExperience = (Get-Random -Minimum 2 -Maximum 20).ToString()
            $previousSchool = "Previous School " + (Get-Random -Minimum 1 -Maximum 100)
            $salutation = if ($firstName -eq "Sarah" -or $firstName -eq "Emily" -or $firstName -eq "Jennifer" -or $firstName -eq "Amanda" -or $firstName -eq "Jessica" -or $firstName -eq "Melissa" -or $firstName -eq "Ashley") { "Ms." } else { "Mr." }
            
            $command = New-Object System.Data.SqlClient.SqlCommand("
                EXEC [dbo].[Teacher_Create]
                    @FirstName = '$firstName',
                    @LastName = '$lastName',
                    @DOB = '$dob',
                    @DOJ = NULL,
                    @DateOfLeaving = NULL,
                    @Address = '$address',
                    @CityId = NULL,
                    @StateId = NULL,
                    @CountryId = NULL,
                    @ZipCode = '$zipCode',
                    @Gender = NULL,
                    @MaritalStatusId = NULL,
                    @Image = '',
                    @Email = '$email',
                    @Phone = '$phone',
                    @MobilePhone = '$phone',
                    @YearsOfExperience = '$yearsOfExperience',
                    @PreviousSchool = '$previousSchool',
                    @Salutation = '$salutation',
                    @IsActive = 1,
                    @IsDeleted = 0,
                    @CompanyId = '$companyId',
                    @SchoolId = '$schoolId',
                    @CreatedBy = '$createdBy',
                    @Status = 'ACT',
                    @StatusMessage = 'Active'
            ", $connection)
            
            $result = $command.ExecuteNonQuery()
            $createdCount++
            Write-Host "Added teacher: $firstName $lastName" -ForegroundColor Green
        }
        catch {
            Write-Host "Error adding teacher $firstName $lastName : $($_.Exception.Message)" -ForegroundColor Red
        }
    }
    
    Write-Host "Successfully added $createdCount teacher records!" -ForegroundColor Green
    
    # Verify the count
    $verifyCommand = New-Object System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM TeacherMaster", $connection)
    $totalTeachers = $verifyCommand.ExecuteScalar()
    Write-Host "Total teachers in database: $totalTeachers" -ForegroundColor Cyan
    
    $connection.Close()
    Write-Host "Script completed successfully!" -ForegroundColor Green
}
catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "Exception type: $($_.Exception.GetType().FullName)" -ForegroundColor Red
    if ($connection.State -eq 'Open') {
        $connection.Close()
    }
}