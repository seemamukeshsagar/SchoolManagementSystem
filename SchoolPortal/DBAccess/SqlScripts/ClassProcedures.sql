-- Class_GetAll
IF OBJECT_ID(N'[dbo].[Class_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Class_GetAll];
GO
CREATE PROCEDURE [dbo].[Class_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        ExamAssessment,
        IsGradePointApplicable,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [OrderBy],
        [Status],
        StatusMessage
    FROM [dbo].[ClassMaster]
    WHERE IsDeleted = 0;
END
GO

-- Class_GetById
IF OBJECT_ID(N'[dbo].[Class_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Class_GetById];
GO
CREATE PROCEDURE [dbo].[Class_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        ExamAssessment,
        IsGradePointApplicable,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [OrderBy],
        [Status],
        StatusMessage
    FROM [dbo].[ClassMaster]
    WHERE Id = @Id;
END
GO

-- Class_Create
IF OBJECT_ID(N'[dbo].[Class_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Class_Create];
GO
CREATE PROCEDURE [dbo].[Class_Create]
    @Name NVARCHAR(200),
    @ExamAssessment NVARCHAR(200) = N'',
    @IsGradePointApplicable BIT = 0,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[ClassMaster]
    (
        Id,
        Name,
        ExamAssessment,
        IsGradePointApplicable,
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
        @ExamAssessment,
        @IsGradePointApplicable,
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

-- Class_Update
IF OBJECT_ID(N'[dbo].[Class_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Class_Update];
GO
CREATE PROCEDURE [dbo].[Class_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @ExamAssessment NVARCHAR(200) = N'',
    @IsGradePointApplicable BIT = 0,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ClassMaster]
    SET 
        Name = @Name,
        ExamAssessment = @ExamAssessment,
        IsGradePointApplicable = @IsGradePointApplicable,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- Class_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[Class_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Class_Delete];
GO
CREATE PROCEDURE [dbo].[Class_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ClassMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
