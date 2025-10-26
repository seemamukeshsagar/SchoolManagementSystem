-- SubjectCategory_GetAll
IF OBJECT_ID(N'[dbo].[SubjectCategory_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SubjectCategory_GetAll];
GO
CREATE PROCEDURE [dbo].[SubjectCategory_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
        ParentId,
        SubjectId,
        SessionId,
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
    FROM [dbo].[SubjectCategoryDetails]
    WHERE IsDeleted = 0;
END
GO

-- SubjectCategory_GetById
IF OBJECT_ID(N'[dbo].[SubjectCategory_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SubjectCategory_GetById];
GO
CREATE PROCEDURE [dbo].[SubjectCategory_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
        ParentId,
        SubjectId,
        SessionId,
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
    FROM [dbo].[SubjectCategoryDetails]
    WHERE Id = @Id;
END
GO

-- SubjectCategory_Create
IF OBJECT_ID(N'[dbo].[SubjectCategory_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SubjectCategory_Create];
GO
CREATE PROCEDURE [dbo].[SubjectCategory_Create]
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @ParentId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER,
    @SessionId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    -- Fallback to latest active session if @SessionId isn't provided
    IF (@SessionId IS NULL)
    BEGIN
        SELECT TOP 1 @SessionId = Id
        FROM [dbo].[SessionMaster]
        WHERE IsActive = 1
        ORDER BY CreatedDate DESC;

        IF (@SessionId IS NULL)
        BEGIN
            SELECT TOP 1 @SessionId = Id
            FROM [dbo].[SessionMaster]
            ORDER BY CreatedDate DESC;
        END
    END

    INSERT INTO [dbo].[SubjectCategoryDetails]
    (
        Id,
        Name,
        Description,
        ParentId,
        SubjectId,
        SessionId,
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
        @ParentId,
        @SubjectId,
        @SessionId,
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

-- SubjectCategory_Update
IF OBJECT_ID(N'[dbo].[SubjectCategory_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SubjectCategory_Update];
GO
CREATE PROCEDURE [dbo].[SubjectCategory_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @ParentId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER,
    @SessionId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- If session not provided, keep existing stored value
    IF (@SessionId IS NULL)
    BEGIN
        SELECT @SessionId = SessionId FROM [dbo].[SubjectCategoryDetails] WHERE Id = @Id;
    END

    UPDATE [dbo].[SubjectCategoryDetails]
    SET 
        Name = @Name,
        Description = @Description,
        ParentId = @ParentId,
        SubjectId = @SubjectId,
        SessionId = @SessionId,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- SubjectCategory_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[SubjectCategory_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SubjectCategory_Delete];
GO
CREATE PROCEDURE [dbo].[SubjectCategory_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SubjectCategoryDetails]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
