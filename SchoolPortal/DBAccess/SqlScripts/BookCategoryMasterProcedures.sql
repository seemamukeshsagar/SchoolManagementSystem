-- BookCategory_GetAll
IF OBJECT_ID(N'[dbo].[BookCategory_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[BookCategory_GetAll];
GO
CREATE PROCEDURE [dbo].[BookCategory_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
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
    FROM [dbo].[BookCategoryMaster]
    WHERE IsDeleted = 0;
END
GO

-- BookCategory_GetById
IF OBJECT_ID(N'[dbo].[BookCategory_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[BookCategory_GetById];
GO
CREATE PROCEDURE [dbo].[BookCategory_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
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
    FROM [dbo].[BookCategoryMaster]
    WHERE Id = @Id;
END
GO

-- BookCategory_Create
IF OBJECT_ID(N'[dbo].[BookCategory_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[BookCategory_Create];
GO
CREATE PROCEDURE [dbo].[BookCategory_Create]
    @Name NVARCHAR(50),
    @Description NVARCHAR(150) = NULL,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[BookCategoryMaster]
    (
        Id,
        Name,
        Description,
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
        @Description,
        @IsActive,
        0,
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'ACT',
        N'Active'
    );

    SELECT Id = @NewId;
END
GO

-- BookCategory_Update
IF OBJECT_ID(N'[dbo].[BookCategory_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[BookCategory_Update];
GO
CREATE PROCEDURE [dbo].[BookCategory_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(50),
    @Description NVARCHAR(150) = NULL,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[BookCategoryMaster]
    SET 
        Name = @Name,
        Description = @Description,
        IsActive = @IsActive,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- BookCategory_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[BookCategory_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[BookCategory_Delete];
GO
CREATE PROCEDURE [dbo].[BookCategory_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[BookCategoryMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO