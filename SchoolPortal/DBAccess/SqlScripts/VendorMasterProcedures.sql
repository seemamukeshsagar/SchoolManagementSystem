-- Vendor_GetAll
IF OBJECT_ID(N'[dbo].[Vendor_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Vendor_GetAll];
GO
CREATE PROCEDURE [dbo].[Vendor_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VendorName,
        Description,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        ContactNumber,
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
    FROM [dbo].[VendorMaster]
    WHERE IsDeleted = 0;
END
GO

-- Vendor_GetById
IF OBJECT_ID(N'[dbo].[Vendor_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Vendor_GetById];
GO
CREATE PROCEDURE [dbo].[Vendor_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VendorName,
        Description,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        ContactNumber,
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
    FROM [dbo].[VendorMaster]
    WHERE Id = @Id;
END
GO

-- Vendor_Create
IF OBJECT_ID(N'[dbo].[Vendor_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Vendor_Create];
GO
CREATE PROCEDURE [dbo].[Vendor_Create]
    @VendorName NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @Address1 NVARCHAR(255) = NULL,
    @Address2 NVARCHAR(255) = NULL,
    @CityId UNIQUEIDENTIFIER,
    @StateId UNIQUEIDENTIFIER,
    @CountryId UNIQUEIDENTIFIER,
    @ZipCode NVARCHAR(50) = NULL,
    @ContactNumber NVARCHAR(50) = NULL,
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

    INSERT INTO [dbo].[VendorMaster]
    (
        Id,
        VendorName,
        Description,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        ContactNumber,
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
        @VendorName,
        @Description,
        @Address1,
        @Address2,
        @CityId,
        @StateId,
        @CountryId,
        @ZipCode,
        @ContactNumber,
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

-- Vendor_Update
IF OBJECT_ID(N'[dbo].[Vendor_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Vendor_Update];
GO
CREATE PROCEDURE [dbo].[Vendor_Update]
    @Id UNIQUEIDENTIFIER,
    @VendorName NVARCHAR(200),
    @Description NVARCHAR(500) = NULL,
    @Address1 NVARCHAR(255) = NULL,
    @Address2 NVARCHAR(255) = NULL,
    @CityId UNIQUEIDENTIFIER,
    @StateId UNIQUEIDENTIFIER,
    @CountryId UNIQUEIDENTIFIER,
    @ZipCode NVARCHAR(50) = NULL,
    @ContactNumber NVARCHAR(50) = NULL,
    @MobileNumber NVARCHAR(50) = NULL,
    @EmailId NVARCHAR(150) = NULL,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VendorMaster]
    SET 
        VendorName = @VendorName,
        Description = @Description,
        Address1 = @Address1,
        Address2 = @Address2,
        CityId = @CityId,
        StateId = @StateId,
        CountryId = @CountryId,
        ZipCode = @ZipCode,
        ContactNumber = @ContactNumber,
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

-- Vendor_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[Vendor_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[Vendor_Delete];
GO
CREATE PROCEDURE [dbo].[Vendor_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VendorMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO