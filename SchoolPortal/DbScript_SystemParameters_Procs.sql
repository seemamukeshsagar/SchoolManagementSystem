SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/* Drop and recreate SystemParameters procedures */

IF OBJECT_ID(N'[dbo].[SystemParameters_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SystemParameters_GetAll]
GO
CREATE PROCEDURE [dbo].[SystemParameters_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [ParameterName],
        [ParameterValue],
        [Description],
        [CompanyId],
        [SchoolId],
        [IsActive],
        [IsDeleted],
        [CreatedBy],
        [CreatedDate],
        [ModifiedBy],
        [ModifiedDate],
        [Status],
        [StatusMessage]
    FROM [dbo].[SystemParameters]
    WHERE ISNULL([IsDeleted], 0) = 0
    ORDER BY [ParameterName];
END
GO

IF OBJECT_ID(N'[dbo].[SystemParameters_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SystemParameters_GetById]
GO
CREATE PROCEDURE [dbo].[SystemParameters_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        [Id],
        [ParameterName],
        [ParameterValue],
        [Description],
        [CompanyId],
        [SchoolId],
        [IsActive],
        [IsDeleted],
        [CreatedBy],
        [CreatedDate],
        [ModifiedBy],
        [ModifiedDate],
        [Status],
        [StatusMessage]
    FROM [dbo].[SystemParameters]
    WHERE [Id] = @Id AND ISNULL([IsDeleted], 0) = 0;
END
GO

IF OBJECT_ID(N'[dbo].[SystemParameters_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SystemParameters_Create]
GO
CREATE PROCEDURE [dbo].[SystemParameters_Create]
    @ParameterName      VARCHAR(50),
    @ParameterValue     VARCHAR(255) = NULL,
    @Description        VARCHAR(1000) = NULL,
    @CompanyId          UNIQUEIDENTIFIER,
    @SchoolId           UNIQUEIDENTIFIER,
    @IsActive           BIT,
    @CreatedBy          UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[SystemParameters]
    (
        [Id],
        [ParameterName],
        [ParameterValue],
        [Description],
        [CompanyId],
        [SchoolId],
        [IsActive],
        [IsDeleted],
        [CreatedBy],
        [CreatedDate]
    )
    VALUES
    (
        @NewId,
        @ParameterName,
        @ParameterValue,
        @Description,
        @CompanyId,
        @SchoolId,
        @IsActive,
        0,
        @CreatedBy,
        GETUTCDATE()
    );

    SELECT @NewId AS [Id];
END
GO

IF OBJECT_ID(N'[dbo].[SystemParameters_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SystemParameters_Update]
GO
CREATE PROCEDURE [dbo].[SystemParameters_Update]
    @Id                 UNIQUEIDENTIFIER,
    @ParameterName      VARCHAR(50),
    @ParameterValue     VARCHAR(255) = NULL,
    @Description        VARCHAR(1000) = NULL,
    @CompanyId          UNIQUEIDENTIFIER,
    @SchoolId           UNIQUEIDENTIFIER,
    @IsActive           BIT,
    @ModifiedBy         UNIQUEIDENTIFIER = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SystemParameters]
    SET 
        [ParameterName] = @ParameterName,
        [ParameterValue] = @ParameterValue,
        [Description]    = @Description,
        [CompanyId]      = @CompanyId,
        [SchoolId]       = @SchoolId,
        [IsActive]       = @IsActive,
        [ModifiedBy]     = @ModifiedBy,
        [ModifiedDate]   = GETUTCDATE()
    WHERE [Id] = @Id AND ISNULL([IsDeleted], 0) = 0;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO

IF OBJECT_ID(N'[dbo].[SystemParameters_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SystemParameters_Delete]
GO
CREATE PROCEDURE [dbo].[SystemParameters_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[SystemParameters]
    SET [IsDeleted] = 1,
        [ModifiedDate] = GETUTCDATE()
    WHERE [Id] = @Id AND ISNULL([IsDeleted], 0) = 0;

    IF @@ROWCOUNT = 1 RETURN 1;
    RETURN 0;
END
GO
