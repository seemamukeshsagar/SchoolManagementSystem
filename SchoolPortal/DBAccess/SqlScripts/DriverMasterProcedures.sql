-- DriverMaster_GetAll
IF OBJECT_ID(N'[dbo].[DriverMaster_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[DriverMaster_GetAll];
GO
CREATE PROCEDURE [dbo].[DriverMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        FirstName,
        LastName,
        DateOfBirth,
        FathersName,
        MothersName,
        QualificationId,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        MobileNumber,
        PhoneNumber,
        DriverImage,
        LicenceNumber,
        LicenceIssueDate,
        LicenceValidUptoDate,
        LicenceDescription,
        LicenceImage,
        LicenceType,
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
    FROM [dbo].[DriverMaster]
    WHERE IsDeleted = 0;
END
GO

-- DriverMaster_GetById
IF OBJECT_ID(N'[dbo].[DriverMaster_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[DriverMaster_GetById];
GO
CREATE PROCEDURE [dbo].[DriverMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        FirstName,
        LastName,
        DateOfBirth,
        FathersName,
        MothersName,
        QualificationId,
        Address1,
        Address2,
        CityId,
        StateId,
        CountryId,
        ZipCode,
        MobileNumber,
        PhoneNumber,
        DriverImage,
        LicenceNumber,
        LicenceIssueDate,
        LicenceValidUptoDate,
        LicenceDescription,
        LicenceImage,
        LicenceType,
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
    FROM [dbo].[DriverMaster]
    WHERE Id = @Id;
END
GO
