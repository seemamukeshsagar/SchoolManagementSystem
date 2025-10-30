-- Student_GetAll
IF OBJECT_ID(N'[dbo].[Student_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Student_GetAll];
GO
CREATE PROCEDURE [dbo].[Student_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        RollNumber,
        FirstName,
        LastName,
        Email,
        Phone,
        DOB,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM 
        StudentMaster
    WHERE 
        IsDeleted = 0;
END
GO

-- Student_GetById
IF OBJECT_ID(N'[dbo].[Student_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Student_GetById];
GO
CREATE PROCEDURE [dbo].[Student_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        RollNumber,
        FirstName,
        LastName,
        Email,
        Phone,
        DOB,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        Status,
        StatusMessage
    FROM 
        StudentMaster
    WHERE 
        Id = @Id
        AND IsDeleted = 0;
END
GO

-- Student_Create
IF OBJECT_ID(N'[dbo].[Student_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Student_Create];
GO
CREATE PROCEDURE [dbo].[Student_Create]
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100) = NULL,
    @DOB DATE = NULL,
    @Email NVARCHAR(100) = NULL,
    @Phone NVARCHAR(20) = NULL,
    @IsActive BIT = 1,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();
    DECLARE @CurrentDate DATETIME = GETUTCDATE();
    
    -- Generate a roll number (you can modify this logic as needed)
    DECLARE @RollNumber UNIQUEIDENTIFIER = NEWID();
    
    INSERT INTO StudentMaster (
        Id,
        RollNumber,
        FirstName,
        LastName,
        Email,
        Phone,
        DOB,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        Status,
        StatusMessage
    ) 
    VALUES (
        @NewId,
        @RollNumber,
        @FirstName,
        @LastName,
        @Email,
        @Phone,
        @DOB,
        @IsActive,
        0, -- IsDeleted
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        @CurrentDate,
        'Active',
        'Student created successfully'
    );
    
    -- Return the new student ID
    SELECT @NewId AS Id;
END
GO

-- Student_Update
IF OBJECT_ID(N'[dbo].[Student_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Student_Update];
GO
CREATE PROCEDURE [dbo].[Student_Update]
    @Id UNIQUEIDENTIFIER,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100) = NULL,
    @DOB DATE = NULL,
    @Email NVARCHAR(100) = NULL,
    @Phone NVARCHAR(20) = NULL,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @CurrentDate DATETIME = GETUTCDATE();
    DECLARE @Result INT = 0;
    
    IF EXISTS (SELECT 1 FROM StudentMaster WHERE Id = @Id AND IsDeleted = 0)
    BEGIN
        UPDATE StudentMaster
        SET 
            FirstName = @FirstName,
            LastName = @LastName,
            DOB = @DOB,
            Email = @Email,
            Phone = @Phone,
            IsActive = @IsActive,
            SchoolId = @SchoolId,
            ModifiedBy = @ModifiedBy,
            ModifiedDate = @CurrentDate,
            Status = 'Updated',
            StatusMessage = 'Student updated successfully'
        WHERE 
            Id = @Id;
            
        SET @Result = 1;
    END
    
    RETURN @Result;
END
GO

-- Student_Delete
IF OBJECT_ID(N'[dbo].[Student_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Student_Delete];
GO
CREATE PROCEDURE [dbo].[Student_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Result INT = 0;
    
    IF EXISTS (SELECT 1 FROM StudentMaster WHERE Id = @Id AND IsDeleted = 0)
    BEGIN
        -- Soft delete the student
        UPDATE StudentMaster
        SET 
            IsDeleted = 1,
            Status = 'Inactive',
            StatusMessage = 'Student marked as deleted'
        WHERE 
            Id = @Id;
            
        SET @Result = 1;
    END
    
    RETURN @Result;
END
GO
