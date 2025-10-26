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
    @Email NVARCHAR(150),
    @Phone NVARCHAR(50),
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
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
        Email,
        Phone,
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
        @Email,
        @Phone,
        @IsActive,
        0,
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

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
    @Email NVARCHAR(150),
    @Phone NVARCHAR(50),
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherMaster]
    SET 
        FirstName = @FirstName,
        LastName = @LastName,
        DOB = @DOB,
        Email = @Email,
        Phone = @Phone,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

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

    RETURN 1;
END
GO
