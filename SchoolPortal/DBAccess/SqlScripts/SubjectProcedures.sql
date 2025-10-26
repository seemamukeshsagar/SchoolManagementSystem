-- Subject_GetAll
IF OBJECT_ID(N'[dbo].[Subject_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Subject_GetAll];
GO
CREATE PROCEDURE [dbo].[Subject_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        SubjectName,
        IsScholastic,
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
    FROM [dbo].[SubjectMaster]
    WHERE IsDeleted = 0;
END
GO

-- Subject_GetById
IF OBJECT_ID(N'[dbo].[Subject_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Subject_GetById];
GO
CREATE PROCEDURE [dbo].[Subject_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        SubjectName,
        IsScholastic,
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
    FROM [dbo].[SubjectMaster]
    WHERE Id = @Id;
END
GO

-- Subject_Create
IF OBJECT_ID(N'[dbo].[Subject_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Subject_Create];
GO
CREATE PROCEDURE [dbo].[Subject_Create]
    @SubjectName NVARCHAR(100),
    @IsScholastic BIT,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[SubjectMaster]
    (
        Id,
        SubjectName,
        IsScholastic,
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
        @SubjectName,
        @IsScholastic,
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

-- Subject_Update
IF OBJECT_ID(N'[dbo].[Subject_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Subject_Update];
GO
CREATE PROCEDURE [dbo].[Subject_Update]
    @Id UNIQUEIDENTIFIER,
    @SubjectName NVARCHAR(100),
    @IsScholastic BIT,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SubjectMaster]
    SET 
        SubjectName = @SubjectName,
        IsScholastic = @IsScholastic,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- Subject_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[Subject_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Subject_Delete];
GO
CREATE PROCEDURE [dbo].[Subject_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SubjectMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
