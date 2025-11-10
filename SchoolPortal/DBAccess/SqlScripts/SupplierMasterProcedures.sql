-- Supplier_GetAll
IF OBJECT_ID(N'[dbo].[Supplier_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Supplier_GetAll];
GO
CREATE PROCEDURE [dbo].[Supplier_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        PhonbeNumber,
        MobileNumber,
        EmailId,
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
    FROM [dbo].[SupplierMaster]
    WHERE IsDeleted = 0;
END
GO

-- Supplier_GetById
IF OBJECT_ID(N'[dbo].[Supplier_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Supplier_GetById];
GO
CREATE PROCEDURE [dbo].[Supplier_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Description,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        PhonbeNumber,
        MobileNumber,
        EmailId,
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
    FROM [dbo].[SupplierMaster]
    WHERE Id = @Id;
END
GO

-- Supplier_Create
IF OBJECT_ID(N'[dbo].[Supplier_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Supplier_Create];
GO
CREATE PROCEDURE [dbo].[Supplier_Create]
    @Name NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @Address1 NVARCHAR(255) = NULL,
    @Address2 NVARCHAR(255) = NULL,
    @CityId UNIQUEIDENTIFIER,
    @StateId UNIQUEIDENTIFIER,
    @CountryId UNIQUEIDENTIFIER,
    @ZipCode NVARCHAR(50) = NULL,
    @PhonbeNumber NVARCHAR(50) = NULL,
    @MobileNumber NVARCHAR(50) = NULL,
    @EmailId NVARCHAR(150) = NULL,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[SupplierMaster]
    (
        Id,
        Name,
        Description,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        PhonbeNumber,
        MobileNumber,
        EmailId,
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
        @Name,
        @Description,
        @Address1,
        @Address2,
        @CityId,
        @StateId,
        @CountryId,
        @ZipCode,
        @PhonbeNumber,
        @MobileNumber,
        @EmailId,
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

-- Supplier_Update
IF OBJECT_ID(N'[dbo].[Supplier_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Supplier_Update];
GO
CREATE PROCEDURE [dbo].[Supplier_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @Address1 NVARCHAR(255) = NULL,
    @Address2 NVARCHAR(255) = NULL,
    @CityId UNIQUEIDENTIFIER,
    @StateId UNIQUEIDENTIFIER,
    @CountryId UNIQUEIDENTIFIER,
    @ZipCode NVARCHAR(50) = NULL,
    @PhonbeNumber NVARCHAR(50) = NULL,
    @MobileNumber NVARCHAR(50) = NULL,
    @EmailId NVARCHAR(150) = NULL,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SupplierMaster]
    SET 
        Name = @Name,
        Description = @Description,
        Address1 = @Address1,
        Address2 = @Address2,
        CityId = @CityId,
        StateId = @StateId,
        CountryId = @CountryId,
        ZipCode = @ZipCode,
        PhonbeNumber = @PhonbeNumber,
        MobileNumber = @MobileNumber,
        EmailId = @EmailId,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- Supplier_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[Supplier_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Supplier_Delete];
GO
CREATE PROCEDURE [dbo].[Supplier_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SupplierMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO