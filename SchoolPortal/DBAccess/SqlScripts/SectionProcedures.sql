-- Section_GetAll
IF OBJECT_ID(N'[dbo].[Section_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Section_GetAll];
GO
CREATE PROCEDURE [dbo].[Section_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SectionMaster]
    WHERE IsDeleted = 0;
END
GO

-- Section_GetById
IF OBJECT_ID(N'[dbo].[Section_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Section_GetById];
GO
CREATE PROCEDURE [dbo].[Section_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[SectionMaster]
    WHERE Id = @Id;
END
GO

-- Section_Create
IF OBJECT_ID(N'[dbo].[Section_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Section_Create];
GO
CREATE PROCEDURE [dbo].[Section_Create]
    @Name NVARCHAR(200),
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[SectionMaster]
    (
        Id,
        Name,
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
        @Name,
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

-- Section_Update
IF OBJECT_ID(N'[dbo].[Section_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Section_Update];
GO
CREATE PROCEDURE [dbo].[Section_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SectionMaster]
    SET 
        Name = @Name,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- Section_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[Section_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Section_Delete];
GO
CREATE PROCEDURE [dbo].[Section_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SectionMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
