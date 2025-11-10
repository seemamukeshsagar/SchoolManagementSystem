-- VehicleMaster_GetAll
IF OBJECT_ID(N'[dbo].[VehicleMaster_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleMaster_GetAll];
GO
CREATE PROCEDURE [dbo].[VehicleMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleNumber,
        VehicleModel,
        VehicleMake,
        VehicleTypeId,
        RegistrationNumber,
        InsuranceCompany,
        InsurancePremium,
        SeatingCapacity,
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
    FROM [dbo].[VehicleMaster]
    WHERE IsDeleted = 0;
END
GO

-- VehicleMaster_GetById
IF OBJECT_ID(N'[dbo].[VehicleMaster_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleMaster_GetById];
GO
CREATE PROCEDURE [dbo].[VehicleMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleNumber,
        VehicleModel,
        VehicleMake,
        VehicleTypeId,
        RegistrationNumber,
        InsuranceCompany,
        InsurancePremium,
        SeatingCapacity,
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
    FROM [dbo].[VehicleMaster]
    WHERE Id = @Id;
END
GO

-- VehicleMaster_Create
IF OBJECT_ID(N'[dbo].[VehicleMaster_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleMaster_Create];
GO
CREATE PROCEDURE [dbo].[VehicleMaster_Create]
    @VehicleNumber NVARCHAR(50),
    @VehicleModel NVARCHAR(100),
    @VehicleMake NVARCHAR(100),
    @VehicleTypeId UNIQUEIDENTIFIER,
    @RegistrationNumber NVARCHAR(50),
    @InsuranceCompany NVARCHAR(100) = NULL,
    @InsurancePremium DECIMAL(18, 2) = NULL,
    @SeatingCapacity INT = NULL,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[VehicleMaster]
    (
        Id,
        VehicleNumber,
        VehicleModel,
        VehicleMake,
        VehicleTypeId,
        RegistrationNumber,
        InsuranceCompany,
        InsurancePremium,
        SeatingCapacity,
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
        @VehicleNumber,
        @VehicleModel,
        @VehicleMake,
        @VehicleTypeId,
        @RegistrationNumber,
        @InsuranceCompany,
        @InsurancePremium,
        @SeatingCapacity,
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

-- VehicleMaster_Update
IF OBJECT_ID(N'[dbo].[VehicleMaster_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleMaster_Update];
GO
CREATE PROCEDURE [dbo].[VehicleMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @VehicleNumber NVARCHAR(50),
    @VehicleModel NVARCHAR(100),
    @VehicleMake NVARCHAR(100),
    @VehicleTypeId UNIQUEIDENTIFIER,
    @RegistrationNumber NVARCHAR(50),
    @InsuranceCompany NVARCHAR(100) = NULL,
    @InsurancePremium DECIMAL(18, 2) = NULL,
    @SeatingCapacity INT = NULL,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VehicleMaster]
    SET 
        VehicleNumber = @VehicleNumber,
        VehicleModel = @VehicleModel,
        VehicleMake = @VehicleMake,
        VehicleTypeId = @VehicleTypeId,
        RegistrationNumber = @RegistrationNumber,
        InsuranceCompany = @InsuranceCompany,
        InsurancePremium = @InsurancePremium,
        SeatingCapacity = @SeatingCapacity,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- VehicleMaster_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[VehicleMaster_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleMaster_Delete];
GO
CREATE PROCEDURE [dbo].[VehicleMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VehicleMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO