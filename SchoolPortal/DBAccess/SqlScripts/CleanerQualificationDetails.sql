-- CleanerQualificationDetails Stored Procedures
GO

CREATE OR ALTER PROCEDURE dbo.CleanerQualificationDetails_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           CleanerId,
           QualificationId,
           SchoolId,
           CompanyId,
           IsActive,
           IsDeleted,
           CreatedBy,
           CreatedDate,
           ModifiedBy,
           ModifiedDate,
           Status,
           StatusMessage
    FROM dbo.CleanerQualificationDetails WITH (NOLOCK)
    WHERE ISNULL(IsDeleted, 0) = 0
    ORDER BY CreatedDate DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.CleanerQualificationDetails_GetById
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           CleanerId,
           QualificationId,
           SchoolId,
           CompanyId,
           IsActive,
           IsDeleted,
           CreatedBy,
           CreatedDate,
           ModifiedBy,
           ModifiedDate,
           Status,
           StatusMessage
    FROM dbo.CleanerQualificationDetails WITH (NOLOCK)
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE dbo.CleanerQualificationDetails_Create
    @CleanerId UNIQUEIDENTIFIER,
    @QualificationId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER,
    @Status NVARCHAR(50),
    @StatusMessage NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();
    INSERT INTO dbo.CleanerQualificationDetails
    (
        Id, CleanerId, QualificationId,
        CompanyId, SchoolId, IsActive, IsDeleted,
        CreatedBy, CreatedDate, Status, StatusMessage
    )
    VALUES
    (
        @NewId, @CleanerId, @QualificationId,
        @CompanyId, @SchoolId, ISNULL(@IsActive, 0), 0,
        @CreatedBy, SYSUTCDATETIME(), ISNULL(@Status, ''), ISNULL(@StatusMessage, '')
    );
    SELECT Id = @NewId;
END
GO

CREATE OR ALTER PROCEDURE dbo.CleanerQualificationDetails_Update
    @Id UNIQUEIDENTIFIER,
    @CleanerId UNIQUEIDENTIFIER,
    @QualificationId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.CleanerQualificationDetails
    SET CleanerId = @CleanerId,
        QualificationId = @QualificationId,
        IsActive = ISNULL(@IsActive, 0),
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO

CREATE OR ALTER PROCEDURE dbo.CleanerQualificationDetails_Delete
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.CleanerQualificationDetails
    SET IsDeleted = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
