IF OBJECT_ID(N'[dbo].[SchoolContact_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SchoolContact_Create];
GO
CREATE PROCEDURE [dbo].[SchoolContact_Create]
    @SchoolId       UNIQUEIDENTIFIER,
    @FirstName      NVARCHAR(200),
    @LastName       NVARCHAR(200) = NULL,
    @Email          NVARCHAR(256) = NULL,
    @Phone          NVARCHAR(50) = NULL,
    @MobilePhone    NVARCHAR(50) = NULL,
    @AddressLine1   NVARCHAR(300) = NULL,
    @AddressLine2   NVARCHAR(300) = NULL,
    @CityId         UNIQUEIDENTIFIER,
    @StateId        UNIQUEIDENTIFIER,
    @CountryId      UNIQUEIDENTIFIER,
    @IsActive       BIT,
    @CreatedBy      UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO dbo.SchoolContactMaster
    (
        Id,
        SchoolId,
        FirstName,
        LastName,
        Email,
        Phone,
        MobilePhone,
        AddressLine1,
        AddressLine2,
        CityId,
        StateId,
        CountryId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        Status,
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @SchoolId,
        ISNULL(@FirstName, ''),
        ISNULL(@LastName, ''),
        ISNULL(@Email, ''),
        ISNULL(@Phone, ''),
        ISNULL(@MobilePhone, ''),
        ISNULL(@AddressLine1, ''),
        ISNULL(@AddressLine2, ''),
        @CityId,
        @StateId,
        @CountryId,
        @IsActive,
        0,
        @CreatedBy,
        GETUTCDATE(),
        '',
        ''
    );

    SELECT @NewId AS Id;
END
GO
