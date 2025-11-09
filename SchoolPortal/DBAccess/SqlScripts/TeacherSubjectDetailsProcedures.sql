-- TeacherSubjectDetails_GetAll
IF OBJECT_ID(N'[dbo].[TeacherSubjectDetails_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[TeacherSubjectDetails_GetAll];
GO
CREATE PROCEDURE [dbo].[TeacherSubjectDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TeacherId,
        SubjectId,
        ClassId,
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
    FROM [dbo].[TeacherSubjectDetails]
    WHERE IsDeleted = 0;
END
GO

-- TeacherSubjectDetails_GetById
IF OBJECT_ID(N'[dbo].[TeacherSubjectDetails_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[TeacherSubjectDetails_GetById];
GO
CREATE PROCEDURE [dbo].[TeacherSubjectDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TeacherId,
        SubjectId,
        ClassId,
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
    FROM [dbo].[TeacherSubjectDetails]
    WHERE Id = @Id;
END
GO

-- TeacherSubjectDetails_Create
IF OBJECT_ID(N'[dbo].[TeacherSubjectDetails_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[TeacherSubjectDetails_Create];
GO
CREATE PROCEDURE [dbo].[TeacherSubjectDetails_Create]
    @TeacherId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @ClassId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[TeacherSubjectDetails]
    (
        Id,
        TeacherId,
        SubjectId,
        ClassId,
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
        @TeacherId,
        @SubjectId,
        @ClassId,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO

-- TeacherSubjectDetails_Update
IF OBJECT_ID(N'[dbo].[TeacherSubjectDetails_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[TeacherSubjectDetails_Update];
GO
CREATE PROCEDURE [dbo].[TeacherSubjectDetails_Update]
    @Id UNIQUEIDENTIFIER,
    @TeacherId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @ClassId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherSubjectDetails]
    SET 
        TeacherId = @TeacherId,
        SubjectId = @SubjectId,
        ClassId = @ClassId,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- TeacherSubjectDetails_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[TeacherSubjectDetails_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[TeacherSubjectDetails_Delete];
GO
CREATE PROCEDURE [dbo].[TeacherSubjectDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherSubjectDetails]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
