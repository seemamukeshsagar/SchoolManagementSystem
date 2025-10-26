-- ClassRoom_GetAll
IF OBJECT_ID(N'[dbo].[ClassRoom_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[ClassRoom_GetAll];
GO
CREATE PROCEDURE [dbo].[ClassRoom_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[ClassRoomMaster]
    WHERE IsDeleted = 0;
END
GO

-- ClassRoom_GetById
IF OBJECT_ID(N'[dbo].[ClassRoom_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[ClassRoom_GetById];
GO
CREATE PROCEDURE [dbo].[ClassRoom_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage
    FROM [dbo].[ClassRoomMaster]
    WHERE Id = @Id;
END
GO

-- ClassRoom_Create
IF OBJECT_ID(N'[dbo].[ClassRoom_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[ClassRoom_Create];
GO
CREATE PROCEDURE [dbo].[ClassRoom_Create]
    @Name NVARCHAR(200),
    @IsActive BIT,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[ClassRoomMaster]
    (
        Id,
        Name,
        IsActive,
        IsDeleted,
        CompanyId,
        SchoolId,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage
    )
    VALUES
    (
        @NewId,
        @Name,
        @IsActive,
        0,
        @CompanyId,
        @SchoolId,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N''
    );

    SELECT Id = @NewId;
END
GO

-- ClassRoom_Update
IF OBJECT_ID(N'[dbo].[ClassRoom_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[ClassRoom_Update];
GO
CREATE PROCEDURE [dbo].[ClassRoom_Update]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(200),
    @IsActive BIT,
    @SchoolId UNIQUEIDENTIFIER,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ClassRoomMaster]
    SET 
        Name = @Name,
        IsActive = @IsActive,
        SchoolId = @SchoolId,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- ClassRoom_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[ClassRoom_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[ClassRoom_Delete];
GO
CREATE PROCEDURE [dbo].[ClassRoom_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[ClassRoomMaster]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO
