-- sp_Privilege_GetAll
IF OBJECT_ID(N'[dbo].[sp_Privilege_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[sp_Privilege_GetAll];
GO
CREATE PROCEDURE [dbo].[sp_Privilege_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        PrivilegeName,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage,
        PrivilegeParentId
    FROM [dbo].[Privileges]
    WHERE IsDeleted = 0;
END
GO

-- sp_Privilege_GetById
IF OBJECT_ID(N'[dbo].[sp_Privilege_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[sp_Privilege_GetById];
GO
CREATE PROCEDURE [dbo].[sp_Privilege_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        PrivilegeName,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        ModifiedBy,
        ModifiedDate,
        [Status],
        StatusMessage,
        PrivilegeParentId
    FROM [dbo].[Privileges]
    WHERE Id = @Id;
END
GO

-- sp_Privilege_Create
IF OBJECT_ID(N'[dbo].[sp_Privilege_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[sp_Privilege_Create];
GO
CREATE PROCEDURE [dbo].[sp_Privilege_Create]
    @Id UNIQUEIDENTIFIER,
    @PrivilegeName NVARCHAR(100),
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER,
    @PrivilegeParentId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[Privileges]
    (
        Id,
        PrivilegeName,
        IsActive,
        IsDeleted,
        CreatedBy,
        CreatedDate,
        [Status],
        StatusMessage,
        PrivilegeParentId
    )
    VALUES
    (
        @Id,
        @PrivilegeName,
        @IsActive,
        0,
        @CreatedBy,
        SYSUTCDATETIME(),
        N'',
        N'',
        @PrivilegeParentId
    );

    SELECT Id = @Id;
END
GO

-- sp_Privilege_Update
IF OBJECT_ID(N'[dbo].[sp_Privilege_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[sp_Privilege_Update];
GO
CREATE PROCEDURE [dbo].[sp_Privilege_Update]
    @Id UNIQUEIDENTIFIER,
    @PrivilegeName NVARCHAR(100),
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER,
    @PrivilegeParentId UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Privileges]
    SET 
        PrivilegeName = @PrivilegeName,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME(),
        PrivilegeParentId = @PrivilegeParentId
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- sp_Privilege_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[sp_Privilege_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[sp_Privilege_Delete];
GO
CREATE PROCEDURE [dbo].[sp_Privilege_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[Privileges]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO