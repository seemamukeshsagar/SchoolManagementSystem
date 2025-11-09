-- CleanerMaster_GetAll
IF OBJECT_ID(N'[dbo].[CleanerMaster_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[CleanerMaster_GetAll];
GO
CREATE PROCEDURE [dbo].[CleanerMaster_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Image,
        FatherName,
        Description,
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
    FROM [dbo].[CleanerMaster]
    WHERE IsDeleted = 0;
END
GO

-- CleanerMaster_GetById
IF OBJECT_ID(N'[dbo].[CleanerMaster_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[CleanerMaster_GetById];
GO
CREATE PROCEDURE [dbo].[CleanerMaster_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Name,
        Image,
        FatherName,
        Description,
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
    FROM [dbo].[CleanerMaster]
    WHERE Id = @Id;
END
GO
