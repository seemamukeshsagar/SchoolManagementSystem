-- TeacherClassDetails_GetAll
IF OBJECT_ID(N'[dbo].[TeacherClassDetails_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[TeacherClassDetails_GetAll];
GO
CREATE PROCEDURE [dbo].[TeacherClassDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TeacherId,
        ClassId,
        SectionId,
        SubjectId,
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
    FROM [dbo].[TeacherClassDetails]
    WHERE IsDeleted = 0;
END
GO

-- TeacherClassDetails_GetById
IF OBJECT_ID(N'[dbo].[TeacherClassDetails_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[TeacherClassDetails_GetById];
GO
CREATE PROCEDURE [dbo].[TeacherClassDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        TeacherId,
        ClassId,
        SectionId,
        SubjectId,
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
    FROM [dbo].[TeacherClassDetails]
    WHERE Id = @Id;
END
GO

-- TeacherClassDetails_Create
IF OBJECT_ID(N'[dbo].[TeacherClassDetails_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[TeacherClassDetails_Create];
GO
CREATE PROCEDURE [dbo].[TeacherClassDetails_Create]
    @TeacherId UNIQUEIDENTIFIER,
    @ClassId UNIQUEIDENTIFIER,
    @SectionId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[TeacherClassDetails]
    (
        Id,
        TeacherId,
        ClassId,
        SectionId,
        SubjectId,
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
        @ClassId,
        @SectionId,
        @SubjectId,
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

-- TeacherClassDetails_Update
IF OBJECT_ID(N'[dbo].[TeacherClassDetails_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[TeacherClassDetails_Update];
GO
CREATE PROCEDURE [dbo].[TeacherClassDetails_Update]
    @Id UNIQUEIDENTIFIER,
    @TeacherId UNIQUEIDENTIFIER,
    @ClassId UNIQUEIDENTIFIER,
    @SectionId UNIQUEIDENTIFIER,
    @SubjectId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherClassDetails]
    SET 
        TeacherId = @TeacherId,
        ClassId = @ClassId,
        SectionId = @SectionId,
        SubjectId = @SubjectId,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- TeacherClassDetails_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[TeacherClassDetails_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[TeacherClassDetails_Delete];
GO
CREATE PROCEDURE [dbo].[TeacherClassDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[TeacherClassDetails]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
