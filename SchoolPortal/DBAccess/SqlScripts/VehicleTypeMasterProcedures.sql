-- VehicleTypeMaster_GetAll
IF OBJECT_ID(N'[dbo].[VehicleTypeMaster_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleTypeMaster_GetAll];
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleType,
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
    FROM [dbo].[VehicleTypeMaster]
    WHERE IsDeleted = 0;
END
GO

-- VehicleTypeMaster_GetById
IF OBJECT_ID(N'[dbo].[VehicleTypeMaster_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleTypeMaster_GetById];
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleType,
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
    FROM [dbo].[VehicleTypeMaster]
    WHERE Id = @Id;
END
GO

-- VehicleTypeMaster_GetByCompany
IF OBJECT_ID(N'[dbo].[VehicleTypeMaster_GetByCompany]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleTypeMaster_GetByCompany];
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_GetByCompany]
    @CompanyId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleType,
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
    FROM [dbo].[VehicleTypeMaster]
    WHERE CompanyId = @CompanyId AND IsDeleted = 0;
END
GO

-- VehicleTypeMaster_GetBySchool
IF OBJECT_ID(N'[dbo].[VehicleTypeMaster_GetBySchool]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleTypeMaster_GetBySchool];
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_GetBySchool]
    @SchoolId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleType,
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
    FROM [dbo].[VehicleTypeMaster]
    WHERE SchoolId = @SchoolId AND IsDeleted = 0;
END
GO

-- VehicleTypeMaster_Create
IF OBJECT_ID(N'[dbo].[VehicleTypeMaster_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleTypeMaster_Create];
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_Create]
    @VehicleType NVARCHAR(100),
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[VehicleTypeMaster]
    (
        Id,
        VehicleType,
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
        @VehicleType,
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

-- VehicleTypeMaster_Update
IF OBJECT_ID(N'[dbo].[VehicleTypeMaster_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleTypeMaster_Update];
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @VehicleType NVARCHAR(100),
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VehicleTypeMaster]
    SET 
        VehicleType = @VehicleType,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- VehicleTypeMaster_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[VehicleTypeMaster_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleTypeMaster_Delete];
GO
CREATE PROCEDURE [dbo].[VehicleTypeMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VehicleTypeMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO