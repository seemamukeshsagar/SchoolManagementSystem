-- HolidayMaster_GetAll
IF OBJECT_ID(N'[dbo].[HolidayMaster_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[HolidayMaster_GetAll];
GO
CREATE PROCEDURE [dbo].[HolidayMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        [Name],
        [Description],
        TypeId,
        FromDate,
        ToDate,
        [Year],
        CompanyId,
        SchoolId,
        IsStaffApplicable,
        SessionId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[HolidayMaster]
    WHERE IsDeleted = 0;
END
GO

-- HolidayMaster_GetById
IF OBJECT_ID(N'[dbo].[HolidayMaster_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[HolidayMaster_GetById];
GO
CREATE PROCEDURE [dbo].[HolidayMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        [Name],
        [Description],
        TypeId,
        FromDate,
        ToDate,
        [Year],
        CompanyId,
        SchoolId,
        IsStaffApplicable,
        SessionId,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[HolidayMaster]
    WHERE Id = @Id;
END
GO

-- HolidayMaster_Create
IF OBJECT_ID(N'[dbo].[HolidayMaster_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[HolidayMaster_Create];
GO
CREATE PROCEDURE [dbo].[HolidayMaster_Create]
    @Name NVARCHAR(150),
    @Description NVARCHAR(250),
    @TypeId UNIQUEIDENTIFIER,
    @FromDate DATETIME,
    @ToDate DATETIME,
    @Year UNIQUEIDENTIFIER,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsStaffApplicable BIT,
    @SessionId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[HolidayMaster]
    (
        Id,
        [Name],
        [Description],
        TypeId,
        FromDate,
        ToDate,
        [Year],
        CompanyId,
        SchoolId,
        IsStaffApplicable,
        SessionId,
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
        @TypeId,
        @FromDate,
        @ToDate,
        @Year,
        @CompanyId,
        @SchoolId,
        @IsStaffApplicable,
        @SessionId,
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

-- HolidayMaster_Update
IF OBJECT_ID(N'[dbo].[HolidayMaster_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[HolidayMaster_Update];
GO
CREATE PROCEDURE [dbo].[HolidayMaster_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(150),
    @Description NVARCHAR(250),
    @TypeId UNIQUEIDENTIFIER,
    @FromDate DATETIME,
    @ToDate DATETIME,
    @Year UNIQUEIDENTIFIER,
    @IsStaffApplicable BIT,
    @SessionId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[HolidayMaster]
    SET 
        [Name] = @Name,
        [Description] = @Description,
        TypeId = @TypeId,
        FromDate = @FromDate,
        ToDate = @ToDate,
        [Year] = @Year,
        IsStaffApplicable = @IsStaffApplicable,
        SessionId = @SessionId,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- HolidayMaster_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[HolidayMaster_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[HolidayMaster_Delete];
GO
CREATE PROCEDURE [dbo].[HolidayMaster_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[HolidayMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
