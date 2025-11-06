-- EmpTypeMaster Stored Procedures

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- EmpTypeMaster_GetAll
IF OBJECT_ID(N'[dbo].[EmpTypeMaster_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[EmpTypeMaster_GetAll];
GO
CREATE PROCEDURE [dbo].[EmpTypeMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TypeName,
        Description,
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
    FROM [dbo].[EmpTypeMaster]
    WHERE IsDeleted = 0;
END
GO

-- EmpTypeMaster_GetById
IF OBJECT_ID(N'[dbo].[EmpTypeMaster_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[EmpTypeMaster_GetById];
GO
CREATE PROCEDURE [dbo].[EmpTypeMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TypeName,
        Description,
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
    FROM [dbo].[EmpTypeMaster]
    WHERE Id = @Id;
END
GO

-- EmpTypeMaster_Create
IF OBJECT_ID(N'[dbo].[EmpTypeMaster_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[EmpTypeMaster_Create];
GO
CREATE PROCEDURE [dbo].[EmpTypeMaster_Create]
    @TypeName VARCHAR(50),
    @Description VARCHAR(150),
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[EmpTypeMaster]
    (
        Id,
        TypeName,
        Description,
        CompanyId,
        SchoolId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @TypeName,
        @Description,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        GETUTCDATE(),
        'INC',
        'In Process....'
    );

    SELECT Id = @NewId;
END
GO

-- EmpTypeMaster_Update
IF OBJECT_ID(N'[dbo].[EmpTypeMaster_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[EmpTypeMaster_Update];
GO
CREATE PROCEDURE [dbo].[EmpTypeMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @TypeName VARCHAR(50),
    @Description VARCHAR(150),
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[EmpTypeMaster]
    SET 
        TypeName = @TypeName,
        Description = @Description,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = GETUTCDATE()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- EmpTypeMaster_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[EmpTypeMaster_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[EmpTypeMaster_Delete];
GO
CREATE PROCEDURE [dbo].[EmpTypeMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[EmpTypeMaster]
    SET IsDeleted = 1,
        IsActive = 0
    WHERE Id = @Id;

    RETURN 1;
END
GO

