-- VehicleExpenseDetails_GetAll
IF OBJECT_ID(N'[dbo].[VehicleExpenseDetails_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleExpenseDetails_GetAll];
GO
CREATE PROCEDURE [dbo].[VehicleExpenseDetails_GetAll]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleId,
        VehicleTypeId,
        Name,
        Description,
        ExpenseDate,
        ExpenseAmount,
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
    FROM [dbo].[VehicleExpenseDetails]
    WHERE IsDeleted = 0;
END
GO

-- VehicleExpenseDetails_GetById
IF OBJECT_ID(N'[dbo].[VehicleExpenseDetails_GetById]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleExpenseDetails_GetById];
GO
CREATE PROCEDURE [dbo].[VehicleExpenseDetails_GetById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleId,
        VehicleTypeId,
        Name,
        Description,
        ExpenseDate,
        ExpenseAmount,
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
    FROM [dbo].[VehicleExpenseDetails]
    WHERE Id = @Id;
END
GO

-- VehicleExpenseDetails_GetByVehicle
IF OBJECT_ID(N'[dbo].[VehicleExpenseDetails_GetByVehicle]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleExpenseDetails_GetByVehicle];
GO
CREATE PROCEDURE [dbo].[VehicleExpenseDetails_GetByVehicle]
    @VehicleId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleId,
        VehicleTypeId,
        Name,
        Description,
        ExpenseDate,
        ExpenseAmount,
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
    FROM [dbo].[VehicleExpenseDetails]
    WHERE VehicleId = @VehicleId AND IsDeleted = 0;
END
GO

-- VehicleExpenseDetails_GetByCompany
IF OBJECT_ID(N'[dbo].[VehicleExpenseDetails_GetByCompany]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleExpenseDetails_GetByCompany];
GO
CREATE PROCEDURE [dbo].[VehicleExpenseDetails_GetByCompany]
    @CompanyId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleId,
        VehicleTypeId,
        Name,
        Description,
        ExpenseDate,
        ExpenseAmount,
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
    FROM [dbo].[VehicleExpenseDetails]
    WHERE CompanyId = @CompanyId AND IsDeleted = 0;
END
GO

-- VehicleExpenseDetails_GetBySchool
IF OBJECT_ID(N'[dbo].[VehicleExpenseDetails_GetBySchool]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleExpenseDetails_GetBySchool];
GO
CREATE PROCEDURE [dbo].[VehicleExpenseDetails_GetBySchool]
    @SchoolId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        VehicleId,
        VehicleTypeId,
        Name,
        Description,
        ExpenseDate,
        ExpenseAmount,
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
    FROM [dbo].[VehicleExpenseDetails]
    WHERE SchoolId = @SchoolId AND IsDeleted = 0;
END
GO

-- VehicleExpenseDetails_Create
IF OBJECT_ID(N'[dbo].[VehicleExpenseDetails_Create]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleExpenseDetails_Create];
GO
CREATE PROCEDURE [dbo].[VehicleExpenseDetails_Create]
    @VehicleId UNIQUEIDENTIFIER,
    @VehicleTypeId UNIQUEIDENTIFIER,
    @Name NVARCHAR(50),
    @Description NVARCHAR(150) = NULL,
    @ExpenseDate DATETIME = NULL,
    @ExpenseAmount DECIMAL(18, 2) = NULL,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @CreatedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewId UNIQUEIDENTIFIER = NEWID();

    INSERT INTO [dbo].[VehicleExpenseDetails]
    (
        Id,
        VehicleId,
        VehicleTypeId,
        Name,
        Description,
        ExpenseDate,
        ExpenseAmount,
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
        @VehicleId,
        @VehicleTypeId,
        @Name,
        @Description,
        @ExpenseDate,
        @ExpenseAmount,
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

-- VehicleExpenseDetails_Update
IF OBJECT_ID(N'[dbo].[VehicleExpenseDetails_Update]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleExpenseDetails_Update];
GO
CREATE PROCEDURE [dbo].[VehicleExpenseDetails_Update]
    @Id UNIQUEIDENTIFIER,
    @VehicleId UNIQUEIDENTIFIER,
    @VehicleTypeId UNIQUEIDENTIFIER,
    @Name NVARCHAR(50),
    @Description NVARCHAR(150) = NULL,
    @ExpenseDate DATETIME = NULL,
    @ExpenseAmount DECIMAL(18, 2) = NULL,
    @CompanyId UNIQUEIDENTIFIER,
    @SchoolId UNIQUEIDENTIFIER,
    @IsActive BIT,
    @ModifiedBy UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VehicleExpenseDetails]
    SET 
        VehicleId = @VehicleId,
        VehicleTypeId = @VehicleTypeId,
        Name = @Name,
        Description = @Description,
        ExpenseDate = @ExpenseDate,
        ExpenseAmount = @ExpenseAmount,
        CompanyId = @CompanyId,
        SchoolId = @SchoolId,
        IsActive = @IsActive,
        ModifiedBy = @ModifiedBy,
        ModifiedDate = SYSUTCDATETIME()
    WHERE Id = @Id;

    RETURN 1;
END
GO

-- VehicleExpenseDetails_Delete (soft delete)
IF OBJECT_ID(N'[dbo].[VehicleExpenseDetails_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[VehicleExpenseDetails_Delete];
GO
CREATE PROCEDURE [dbo].[VehicleExpenseDetails_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[VehicleExpenseDetails]
    SET IsDeleted = 1
    WHERE Id = @Id;

    RETURN 1;
END
GO