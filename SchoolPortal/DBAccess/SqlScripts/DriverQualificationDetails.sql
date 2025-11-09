-- DriverQualificationDetails Stored Procedures
GO

CREATE OR ALTER PROCEDURE dbo.DriverQualificationDetails_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           DriverId,
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
    FROM dbo.DriverQualificationDetails WITH (NOLOCK)
    WHERE ISNULL(IsDeleted, 0) = 0
    ORDER BY CreatedDate DESC;
END
GO

CREATE OR ALTER PROCEDURE dbo.DriverQualificationDetails_GetById
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,
           DriverId,
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
    FROM dbo.DriverQualificationDetails WITH (NOLOCK)
    WHERE Id = @Id;
END
GO

CREATE OR ALTER PROCEDURE dbo.DriverQualificationDetails_Create
    @DriverId UNIQUEIDENTIFIER,
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
    INSERT INTO dbo.DriverQualificationDetails
    (
        Id, DriverId, QualificationId,
        CompanyId, SchoolId, IsActive, IsDeleted,
        CreatedBy, CreatedDate, Status, StatusMessage
    )
    VALUES
    (
        @NewId, @DriverId, @QualificationId,
        @CompanyId, @SchoolId, ISNULL(@IsActive, 0), 0,
        @CreatedBy, SYSUTCDATETIME(), ISNULL(@Status, ''), ISNULL(@StatusMessage, '')
    );
    SELECT Id = @NewId;
END
GO

CREATE OR ALTER PROCEDURE dbo.DriverQualificationDetails_Update
    @Id UNIQUEIDENTIFIER,
    @DriverId UNIQUEIDENTIFIER,
    @QualificationId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.DriverQualificationDetails
    SET DriverId = @DriverId,
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

CREATE OR ALTER PROCEDURE dbo.DriverQualificationDetails_Delete
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.DriverQualificationDetails
    SET IsDeleted = 1,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
