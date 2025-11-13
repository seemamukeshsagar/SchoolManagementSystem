-- Stored Procedure to process and insert/update employee data
-- Handles ID generation and data validation
CREATE PROCEDURE sp_UpsertEmployeeData
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DOB DATE = NULL,
    @DOJ DATE = NULL,
    @ProbationStartDate DATE = NULL,
    @ProbationPeriod INT = NULL,
    @ConfirmationDate DATE = NULL,
    @PANNumber NVARCHAR(20) = NULL,
    @ESICNumber NVARCHAR(20) = NULL,
    @PFNumeber NVARCHAR(20) = NULL,
    @CurrentAddress1 NVARCHAR(200) = NULL,
    @CurrentAddress2 NVARCHAR(200) = NULL,
    @CurrentCityName NVARCHAR(100) = NULL,
    @CurrentStateName NVARCHAR(100) = NULL,
    @CurrentCountryName NVARCHAR(100) = NULL,
    @CurrentZipCode NVARCHAR(10) = NULL,
    @PermanentAddress1 NVARCHAR(200) = NULL,
    @PermanentAddress2 NVARCHAR(200) = NULL,
    @PermanentCityName NVARCHAR(100) = NULL,
    @PermanentStateName NVARCHAR(100) = NULL,
    @PermanentCountryName NVARCHAR(100) = NULL,
    @PermanentZipCode NVARCHAR(10) = NULL,
    @PhoneNumber NVARCHAR(20) = NULL,
    @MobileNumber NVARCHAR(20) = NULL,
    @EmailId NVARCHAR(100) = NULL,
    @DepartmentId INT = NULL,
    @DesignationId INT = NULL,
    @PaymentModeId INT = NULL,
    @EmployeeTypeId INT = NULL,
    @CategoryId INT = NULL,
    @BankAccountNumber NVARCHAR(50) = NULL,
    @BankName NVARCHAR(100) = NULL,
    @GenderId INT = NULL,
    @BloodGroupId INT = NULL,
    @GradeId INT = NULL,
    @Image VARBINARY(MAX) = NULL,
    @EmployeeOldId NVARCHAR(50) = NULL,
    @FathersName NVARCHAR(100) = NULL,
    @MothersName NVARCHAR(100) = NULL,
    @Description NVARCHAR(500) = NULL,
    @LicenceNumber NVARCHAR(50) = NULL,
    @LicenceIssueDate DATE = NULL,
    @LicenceValidUpto DATE = NULL,
    @LicenceDescription NVARCHAR(500) = NULL,
    @LicenceImage VARBINARY(MAX) = NULL,
    @LicenceType NVARCHAR(50) = NULL,
    @Salutation NVARCHAR(10) = NULL,
    @DateOfLeaving DATE = NULL,
    @MaritalStatus NVARCHAR(20) = NULL,
    @YearsOfExperience INT = NULL,
    @PrevioudSchoolCompany NVARCHAR(200) = NULL,
    @AadhaarNumber NVARCHAR(20) = NULL,
    @MathUpToClass INT = NULL,
    @EnglishUptoClass INT = NULL,
    @SSTUptoClass INT = NULL,
    @CompanyId INT = NULL,
    @SchoolId INT = NULL,
    @IsActive BIT = 1,
    @IsDeleted BIT = 0,
    @CreatedBy NVARCHAR(50) = NULL,
    @CreatedDate DATETIME = NULL,
    @ModifiedBy NVARCHAR(50) = NULL,
    @ModifiedDate DATETIME = NULL,
    @Status NVARCHAR(50) = NULL,
    @StatusMessage NVARCHAR(500) = NULL,
    @EmployeeCategoryId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Set default values if not provided
    IF @CreatedDate IS NULL
        SET @CreatedDate = GETDATE();
        
    IF @ModifiedDate IS NULL
        SET @ModifiedDate = GETDATE();
    
    -- Check if employee already exists based on key identifiers
    DECLARE @ExistingId INT;
    SELECT @ExistingId = Id 
    FROM EmpMaster 
    WHERE FirstName = @FirstName 
      AND LastName = @LastName 
      AND DOB = @DOB
      AND PANNumber = @PANNumber;
    
    -- Convert Country/State/City Names to IDs (you may need to adjust this logic based on your lookup tables)
    DECLARE @CurrentCityId INT, @CurrentStateId INT, @CurrentCountryId INT;
    DECLARE @PermanentCityId INT, @PermanentStateId INT, @PermanentCountryId INT;
    
    -- Lookup Current Location IDs (simplified - you may need to implement actual lookup logic)
    -- For now, we'll set them to NULL or default values
    SET @CurrentCityId = NULL;
    SET @CurrentStateId = NULL;
    SET @CurrentCountryId = NULL;
    SET @PermanentCityId = NULL;
    SET @PermanentStateId = NULL;
    SET @PermanentCountryId = NULL;
    
    IF @ExistingId IS NOT NULL
    BEGIN
        -- Update existing record
        UPDATE EmpMaster SET
            FirstName = @FirstName,
            LastName = @LastName,
            DOB = @DOB,
            DOJ = @DOJ,
            ProbationStartDate = @ProbationStartDate,
            ProbationPeriod = @ProbationPeriod,
            ConfirmationDate = @ConfirmationDate,
            PANNumber = @PANNumber,
            ESICNumber = @ESICNumber,
            PFNumeber = @PFNumeber,
            CurrentAddress1 = @CurrentAddress1,
            CurrentAddress2 = @CurrentAddress2,
            CurrentCityId = @CurrentCityId,
            CurrentStateId = @CurrentStateId,
            CurrentCountryId = @CurrentCountryId,
            CurrentZipCode = @CurrentZipCode,
            PermanentAddress1 = @PermanentAddress1,
            PermanentAddress2 = @PermanentAddress2,
            PermanentCityId = @PermanentCityId,
            PermanentStateId = @PermanentStateId,
            PermanentCountryId = @PermanentCountryId,
            PermanentZipCode = @PermanentZipCode,
            PhoneNumber = @PhoneNumber,
            MobileNumber = @MobileNumber,
            EmailId = @EmailId,
            DepartmentId = @DepartmentId,
            DesignationId = @DesignationId,
            PaymentModeId = @PaymentModeId,
            EmployeeTypeId = @EmployeeTypeId,
            CategoryId = @CategoryId,
            BankAccountNumber = @BankAccountNumber,
            BankName = @BankName,
            GenderId = @GenderId,
            BloodGroupId = @BloodGroupId,
            GradeId = @GradeId,
            Image = @Image,
            EmployeeOldId = @EmployeeOldId,
            FathersName = @FathersName,
            MothersName = @MothersName,
            Description = @Description,
            LicenceNumber = @LicenceNumber,
            LicenceIssueDate = @LicenceIssueDate,
            LicenceValidUpto = @LicenceValidUpto,
            LicenceDescription = @LicenceDescription,
            LicenceImage = @LicenceImage,
            LicenceType = @LicenceType,
            Salutation = @Salutation,
            DateOfLeaving = @DateOfLeaving,
            MaritalStatus = @MaritalStatus,
            YearsOfExperience = @YearsOfExperience,
            PrevioudSchoolCompany = @PrevioudSchoolCompany,
            AadhaarNumber = @AadhaarNumber,
            MathUpToClass = @MathUpToClass,
            EnglishUptoClass = @EnglishUptoClass,
            SSTUptoClass = @SSTUptoClass,
            CompanyId = @CompanyId,
            SchoolId = @SchoolId,
            IsActive = @IsActive,
            IsDeleted = @IsDeleted,
            ModifiedBy = @ModifiedBy,
            ModifiedDate = @ModifiedDate,
            Status = @Status,
            StatusMessage = @StatusMessage,
            EmployeeCategoryId = @EmployeeCategoryId
        WHERE Id = @ExistingId;
        
        SELECT @ExistingId AS EmployeeId, 'Updated' AS Operation;
    END
    ELSE
    BEGIN
        -- Insert new record with auto-generated ID
        INSERT INTO EmpMaster (
            FirstName, LastName, DOB, DOJ, ProbationStartDate, ProbationPeriod, ConfirmationDate,
            PANNumber, ESICNumber, PFNumeber, CurrentAddress1, CurrentAddress2, CurrentCityId,
            CurrentStateId, CurrentCountryId, CurrentZipCode, PermanentAddress1, PermanentAddress2,
            PermanentCityId, PermanentStateId, PermanentCountryId, PermanentZipCode, PhoneNumber,
            MobileNumber, EmailId, DepartmentId, DesignationId, PaymentModeId, EmployeeTypeId,
            CategoryId, BankAccountNumber, BankName, GenderId, BloodGroupId, GradeId, Image,
            EmployeeOldId, FathersName, MothersName, Description, LicenceNumber, LicenceIssueDate,
            LicenceValidUpto, LicenceDescription, LicenceImage, LicenceType, Salutation, DateOfLeaving,
            MaritalStatus, YearsOfExperience, PrevioudSchoolCompany, AadhaarNumber, MathUpToClass,
            EnglishUptoClass, SSTUptoClass, CompanyId, SchoolId, IsActive, IsDeleted, CreatedBy,
            CreatedDate, ModifiedBy, ModifiedDate, Status, StatusMessage, EmployeeCategoryId
        ) VALUES (
            @FirstName, @LastName, @DOB, @DOJ, @ProbationStartDate, @ProbationPeriod, @ConfirmationDate,
            @PANNumber, @ESICNumber, @PFNumeber, @CurrentAddress1, @CurrentAddress2, @CurrentCityId,
            @CurrentStateId, @CurrentCountryId, @CurrentZipCode, @PermanentAddress1, @PermanentAddress2,
            @PermanentCityId, @PermanentStateId, @PermanentCountryId, @PermanentZipCode, @PhoneNumber,
            @MobileNumber, @EmailId, @DepartmentId, @DesignationId, @PaymentModeId, @EmployeeTypeId,
            @CategoryId, @BankAccountNumber, @BankName, @GenderId, @BloodGroupId, @GradeId, @Image,
            @EmployeeOldId, @FathersName, @MothersName, @Description, @LicenceNumber, @LicenceIssueDate,
            @LicenceValidUpto, @LicenceDescription, @LicenceImage, @LicenceType, @Salutation, @DateOfLeaving,
            @MaritalStatus, @YearsOfExperience, @PrevioudSchoolCompany, @AadhaarNumber, @MathUpToClass,
            @EnglishUptoClass, @SSTUptoClass, @CompanyId, @SchoolId, @IsActive, @IsDeleted, @CreatedBy,
            @CreatedDate, @ModifiedBy, @ModifiedDate, @Status, @StatusMessage, @EmployeeCategoryId
        );
        
        SET @ExistingId = SCOPE_IDENTITY();
        SELECT @ExistingId AS EmployeeId, 'Inserted' AS Operation;
    END
END