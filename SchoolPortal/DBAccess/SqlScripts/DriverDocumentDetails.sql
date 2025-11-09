-- DriverDocumentDetails Stored Procedures
GO

CREATE OR ALTER PROCEDURE dbo.DriverDocumentDetails_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           DriverId,
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
    FROM dbo.DriverDocumentDetails WITH (NOLOCK)
    WHERE ISNULL(IsDeleted, 0) = 0
    ORDER BY CreatedDate DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.DriverDocumentDetails_GetById
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           DriverId,
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
    FROM dbo.DriverDocumentDetails WITH (NOLOCK)
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE dbo.DriverDocumentDetails_Create
    @DriverId UNIQUEIDENTIFIER,
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
    INSERT INTO dbo.DriverDocumentDetails
    (
        Id, DriverId, Name, Description, FileName,
        CompanyId, SchoolId, IsActive, IsDeleted,
        CreatedBy, CreatedDate, Status, StatusMessage
    )
    VALUES
    (
        @NewId, @DriverId, ISNULL(@Name, ''), ISNULL(@Description, ''), ISNULL(@FileName, ''),
        @CompanyId, @SchoolId, ISNULL(@IsActive, 0), 0,
        @CreatedBy, SYSUTCDATETIME(), ISNULL(@Status, ''), ISNULL(@StatusMessage, '')
    );
    SELECT Id = @NewId;
END
GO

CREATE OR ALTER PROCEDURE dbo.DriverDocumentDetails_Update
    @Id UNIQUEIDENTIFIER,
    @DriverId UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @Description NVARCHAR(500),
    @FileName NVARCHAR(500),
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.DriverDocumentDetails
    SET DriverId = @DriverId,
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

CREATE OR ALTER PROCEDURE dbo.DriverDocumentDetails_Delete
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.DriverDocumentDetails
    SET IsDeleted = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
