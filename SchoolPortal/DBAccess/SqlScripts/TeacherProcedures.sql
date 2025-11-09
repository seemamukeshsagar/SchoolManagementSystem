-- Teacher_GetAll
IF OBJECT_ID(N'[dbo].[Teacher_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Teacher_GetAll];
GO
CREATE PROCEDURE [dbo].[Teacher_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        FirstName,
        LastName,
        DOB,
        DOJ,
        DateOfLeaving,
        Address,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        Gender,
        MaritalStatusId,
        Image,
        Phone,
        MobilePhone,
        YearsOfExperience,
        PreviousSchool,
        Salutation,
        Email,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TeacherMaster]
    WHERE IsDeleted = 0;
END
GO

-- Teacher_GetById
IF OBJECT_ID(N'[dbo].[Teacher_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Teacher_GetById];
GO
CREATE PROCEDURE [dbo].[Teacher_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        FirstName,
        LastName,
        DOB,
        DOJ,
        DateOfLeaving,
        Address,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        Gender,
        MaritalStatusId,
        Image,
        Phone,
        MobilePhone,
        YearsOfExperience,
        PreviousSchool,
        Salutation,
        Email,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[TeacherMaster]
    WHERE Id = @Id;
END
GO

-- Teacher_Create
IF OBJECT_ID(N'[dbo].[Teacher_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Teacher_Create];
GO
CREATE PROCEDURE [dbo].[Teacher_Create]
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DOB DATETIME,
    @DOJ DATETIME NULL,
    @DateOfLeaving DATETIME NULL,
    @Address NVARCHAR(250),
    @CityId UNIQUEIDENTIFIER NULL,
    @StateId UNIQUEIDENTIFIER NULL,
    @CountryId UNIQUEIDENTIFIER NULL,
    @ZipCode NVARCHAR(20),
    @Gender UNIQUEIDENTIFIER NULL,
    @MaritalStatusId UNIQUEIDENTIFIER NULL,
    @Image NVARCHAR(500),
    @Email NVARCHAR(150),
    @Phone NVARCHAR(50),
    @MobilePhone NVARCHAR(50),
    @YearsOfExperience NVARCHAR(50),
    @PreviousSchool NVARCHAR(150),
    @Salutation NVARCHAR(50),
    @IsActive BIT,
    @IsDeleted BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER,
    @Status NVARCHAR(50),
    @StatusMessage NVARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[TeacherMaster]
    (
        Id,
        FirstName,
        LastName,
        DOB,
        DOJ,
        DateOfLeaving,
        Address,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        Gender,
        MaritalStatusId,
        Image,
        Email,
        Phone,
        MobilePhone,
        YearsOfExperience,
        PreviousSchool,
        Salutation,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @FirstName,
        @LastName,
        @DOB,
        @DOJ,
        @DateOfLeaving,
        @Address,
        @CityId,
        @StateId,
        @CountryId,
        @ZipCode,
        @Gender,
        @MaritalStatusId,
        @Image,
        @Email,
        @Phone,
        @MobilePhone,
        @YearsOfExperience,
        @PreviousSchool,
        @Salutation,
        @IsActive,
        @IsDeleted,
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        SYSUTCDATETIME(),
        @Status,
        @StatusMessage
    );

    -- Sync EmpMaster on create
    IF NOT EXISTS (SELECT 1 FROM [dbo].[EmpMaster] WHERE Id = @NewId)
    BEGIN
        INSERT INTO [dbo].[EmpMaster]
        (
            Id,
            FirstName,
            LastName,
            Email,
            Phone,
            DOB,
            CompanyId,
            SchoolId,
            IsActive,
            IsDeleted,
            CreatedBy,
            CreatedDate
        )
        VALUES
        (
            @NewId,
            @FirstName,
            @LastName,
            @Email,
            @Phone,
            @DOB,
            @CompanyId,
            @SchoolId,
            @IsActive,
            @IsDeleted,
            @CreatedBy,
            SYSUTCDATETIME()
        );
    END
    ELSE
    BEGIN
        UPDATE [dbo].[EmpMaster]
        SET FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            Phone = @Phone,
            DOB = @DOB,
            CompanyId = @CompanyId,
            SchoolId = @SchoolId,
            IsActive = @IsActive,
            IsDeleted = @IsDeleted,
            ModifiedBy = @CreatedBy,
            ModifiedDate = SYSUTCDATETIME()
        WHERE Id = @NewId;
    END

    SELECT Id = @NewId;
END
GO

-- Teacher_Update
IF OBJECT_ID(N'[dbo].[Teacher_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Teacher_Update];
GO
CREATE PROCEDURE [dbo].[Teacher_Update]
    @Id UNIQUEIDENTIFIER,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @DOB DATETIME,
    @DOJ DATETIME NULL,
    @DateOfLeaving DATETIME NULL,
    @Address NVARCHAR(250),
    @CityId UNIQUEIDENTIFIER NULL,
    @StateId UNIQUEIDENTIFIER NULL,
    @CountryId UNIQUEIDENTIFIER NULL,
    @ZipCode NVARCHAR(20),
    @Gender UNIQUEIDENTIFIER NULL,
    @MaritalStatusId UNIQUEIDENTIFIER NULL,
    @Image NVARCHAR(500),
    @Email NVARCHAR(150),
    @Phone NVARCHAR(50),
    @MobilePhone NVARCHAR(50),
    @YearsOfExperience NVARCHAR(50),
    @PreviousSchool NVARCHAR(150),
    @Salutation NVARCHAR(50),
    @IsActive BIT,
    @IsDeleted BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER,
    @Status NVARCHAR(50),
    @StatusMessage NVARCHAR(250)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherMaster]
    SET 
        FirstName = @FirstName,
        LastName = @LastName,
        DOB = @DOB,
        DOJ = @DOJ,
        DateOfLeaving = @DateOfLeaving,
        Address = @Address,
        CityId = @CityId,
        StateId = @StateId,
        CountryId = @CountryId,
        ZipCode = @ZipCode,
        Gender = @Gender,
        MaritalStatusId = @MaritalStatusId,
        Image = @Image,
        Email = @Email,
        Phone = @Phone,
        MobilePhone = @MobilePhone,
        YearsOfExperience = @YearsOfExperience,
        PreviousSchool = @PreviousSchool,
        Salutation = @Salutation,
        IsActive = @IsActive,
        IsDeleted = @IsDeleted,
        SchoolId = @SchoolId,
        [Status] = @Status,
        StatusMessage = @StatusMessage,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    -- Sync EmpMaster on update (upsert)
    IF EXISTS (SELECT 1 FROM [dbo].[EmpMaster] WHERE Id = @Id)
    BEGIN
        UPDATE [dbo].[EmpMaster]
        SET FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            Phone = @Phone,
            DOB = @DOB,
            CompanyId = CompanyId, -- keep existing unless you want to change via teacher
            SchoolId = @SchoolId,
            IsActive = @IsActive,
            IsDeleted = @IsDeleted,
            ModifiedBy = @ModifiedBy,
            ModifiedDate = SYSUTCDATETIME()
        WHERE Id = @Id;
    END
    ELSE
    BEGIN
        INSERT INTO [dbo].[EmpMaster]
        (
            Id,
            FirstName,
            LastName,
            Email,
            Phone,
            DOB,
            CompanyId,
            SchoolId,
            IsActive,
            IsDeleted,
            CreatedBy,
            CreatedDate
        )
        VALUES
        (
            @Id,
            @FirstName,
            @LastName,
            @Email,
            @Phone,
            @DOB,
            (SELECT CompanyId FROM [dbo].[TeacherMaster] WHERE Id = @Id),
            @SchoolId,
            @IsActive,
            @IsDeleted,
            @ModifiedBy,
            SYSUTCDATETIME()
        );
    END

    RETURN 1;
END
GO

-- Teacher_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[Teacher_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Teacher_Delete];
GO
CREATE PROCEDURE [dbo].[Teacher_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    -- Soft delete in EmpMaster as well
    IF EXISTS (SELECT 1 FROM [dbo].[EmpMaster] WHERE Id = @Id)
    BEGIN
        UPDATE [dbo].[EmpMaster]
        SET IsDeleted = 1,
            ModifiedDate = SYSUTCDATETIME()
        WHERE Id = @Id;
    END

    RETURN 1;
END
GO
