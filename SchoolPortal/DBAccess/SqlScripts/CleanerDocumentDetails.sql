-- CleanerDocumentDetails Stored Procedures
GO

CREATE OR ALTER PROCEDURE dbo.CleanerDocumentDetails_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           CleanerId,
           Name,
           Description,
           FileName,
           CompanyId,
           SchoolId,
           IsActive,
           IsDeleted,
           CreatedBy,
           CreatedDate,
           ModifiedBy,
           ModifiedDate,
           Status,
           StatusMessage
    FROM dbo.CleanerDocumentDetails WITH (NOLOCK)
    WHERE ISNULL(IsDeleted, 0) = 0
    ORDER BY CreatedDate DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.CleanerDocumentDetails_GetById
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           CleanerId,
           Name,
           Description,
           FileName,
           CompanyId,
           SchoolId,
           IsActive,
           IsDeleted,
           CreatedBy,
           CreatedDate,
           ModifiedBy,
           ModifiedDate,
           Status,
           StatusMessage
    FROM dbo.CleanerDocumentDetails WITH (NOLOCK)
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE dbo.CleanerDocumentDetails_Create
    @CleanerId UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @FileName NVARCHAR(500),
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
    INSERT INTO dbo.CleanerDocumentDetails
    (
        Id, CleanerId, Name, Description, FileName,
        CompanyId, SchoolId, IsActive, IsDeleted,
        CreatedBy, CreatedDate, Status, StatusMessage
    )
    VALUES
    (
        @NewId, @CleanerId, ISNULL(@Name, ''), ISNULL(@Description, ''), ISNULL(@FileName, ''),
        @CompanyId, @SchoolId, ISNULL(@IsActive, 0), 0,
        @CreatedBy, SYSUTCDATETIME(), ISNULL(@Status, ''), ISNULL(@StatusMessage, '')
    );
    SELECT Id = @NewId;
END
GO

CREATE OR ALTER PROCEDURE dbo.CleanerDocumentDetails_Update
    @Id UNIQUEIDENTIFIER,
    @CleanerId UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @FileName NVARCHAR(500),
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.CleanerDocumentDetails
    SET CleanerId = @CleanerId,
        Name = ISNULL(@Name, ''),
        Description = ISNULL(@Description, ''),
        FileName = ISNULL(@FileName, ''),
        IsActive = ISNULL(@IsActive, 0),
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO

CREATE OR ALTER PROCEDURE dbo.CleanerDocumentDetails_Delete
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.CleanerDocumentDetails
    SET IsDeleted = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
