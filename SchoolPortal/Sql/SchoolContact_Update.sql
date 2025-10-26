IF OBJECT_ID(N'[dbo].[SchoolContact_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SchoolContact_Update];
GO
CREATE PROCEDURE [dbo].[SchoolContact_Update]
    @Id             UNIQUEIDENTIFIER,
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
    @ModifiedBy     UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.SchoolContactMaster WITH (NOLOCK) WHERE Id = @Id AND IsDeleted = 0)
        RETURN 0;

    UPDATE dbo.SchoolContactMaster
    SET 
        SchoolId = @SchoolId,
        FirstName = ISNULL(@FirstName, ''),
        LastName = ISNULL(@LastName, ''),
        Email = ISNULL(@Email, ''),
        Phone = ISNULL(@Phone, ''),
        MobilePhone = ISNULL(@MobilePhone, ''),
        AddressLine1 = ISNULL(@AddressLine1, ''),
        AddressLine2 = ISNULL(@AddressLine2, ''),
        CityId = @CityId,
        StateId = @StateId,
        CountryId = @CountryId,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = GETUTCDATE()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 0 RETURN 0;
    RETURN 1;
END
GO
