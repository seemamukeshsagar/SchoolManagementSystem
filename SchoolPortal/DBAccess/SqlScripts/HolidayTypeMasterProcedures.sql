-- HolidayTypeMaster_GetAll
IF OBJECT_ID(N'[dbo].[HolidayTypeMaster_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[HolidayTypeMaster_GetAll];
GO
CREATE PROCEDURE [dbo].[HolidayTypeMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        HolidayTypeName,
        HolidayTypeDescription,
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
    FROM [dbo].[HolidayTypeMaster]
    WHERE IsDeleted = 0;
END
GO

-- HolidayTypeMaster_GetById
IF OBJECT_ID(N'[dbo].[HolidayTypeMaster_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[HolidayTypeMaster_GetById];
GO
CREATE PROCEDURE [dbo].[HolidayTypeMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        HolidayTypeName,
        HolidayTypeDescription,
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
    FROM [dbo].[HolidayTypeMaster]
    WHERE Id = @Id;
END
GO

-- HolidayTypeMaster_Create
IF OBJECT_ID(N'[dbo].[HolidayTypeMaster_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[HolidayTypeMaster_Create];
GO
CREATE PROCEDURE [dbo].[HolidayTypeMaster_Create]
    @HolidayTypeName NVARCHAR(200),
    @HolidayTypeDescription NVARCHAR(500),
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[HolidayTypeMaster]
    (
        Id,
        HolidayTypeName,
        HolidayTypeDescription,
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
        @HolidayTypeName,
        @HolidayTypeDescription,
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

-- HolidayTypeMaster_Update
IF OBJECT_ID(N'[dbo].[HolidayTypeMaster_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[HolidayTypeMaster_Update];
GO
CREATE PROCEDURE [dbo].[HolidayTypeMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @HolidayTypeName NVARCHAR(200),
    @HolidayTypeDescription NVARCHAR(500),
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[HolidayTypeMaster]
    SET 
        HolidayTypeName = @HolidayTypeName,
        HolidayTypeDescription = @HolidayTypeDescription,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- HolidayTypeMaster_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[HolidayTypeMaster_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[HolidayTypeMaster_Delete];
GO
CREATE PROCEDURE [dbo].[HolidayTypeMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[HolidayTypeMaster]
    SET IsDeleted = 1,
        IsActive = 0
    WHERE Id = @Id;

    RETURN 1;
END
GO
