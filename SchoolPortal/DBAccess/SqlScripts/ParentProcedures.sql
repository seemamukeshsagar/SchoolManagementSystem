-- Parent Stored Procedures

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Parent_Create
IF OBJECT_ID(N'[dbo].[Parent_Create]', N'P') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[Parent_Create] AS SET NOCOUNT ON;');
GO
ALTER PROCEDURE [dbo].[Parent_Create]
    @StudentGUID uniqueidentifier,
    @ParentFirstName varchar(50),
    @ParentLastName varchar(50) = '',
    @ParentDOB datetime = NULL,
    @QualificationId uniqueidentifier,
    @Occupation varchar(100) = '',
    @AnnualIncome decimal(18,2) = NULL,
    @DesignationId uniqueidentifier,
    @Phone varchar(50) = '',
    @Mobile varchar(50) = '',
    @Email varchar(100) = '',
    @Address1 varchar(250) = '',
    @Address2 varchar(250) = '',
    @CityId uniqueidentifier,
    @StateId uniqueidentifier,
    @CountryId uniqueidentifier,
    @ZipCode varchar(50) = '',
    @OfficeAddress1 varchar(250) = '',
    @OfficeAddress2 varchar(250) = '',
    @OfficeCityId uniqueidentifier,
    @OfficeStateId uniqueidentifier,
    @OfficeCountryId uniqueidentifier,
    @OfficeZipCode varchar(50) = '',
    @OfficePhone varchar(50) = '',
    @Image varchar(255) = '',
    @RelationTypeId uniqueidentifier,
    @SchoolId uniqueidentifier,
    @CompanyId uniqueidentifier,
    @IsActive bit = 1,
    @IsDeleted bit = 0,
    @CreatedBy uniqueidentifier,
    @CreatedDate datetime = NULL,
    @Status varchar(10) = 'INC',
    @StatusMessage nvarchar(255) = N'In Process....'
AS
BEGIN
    SET NOCOUNT ON;
    IF @CreatedDate IS NULL SET @CreatedDate = GETUTCDATE();

    DECLARE @NewId uniqueidentifier = NEWID();
    INSERT INTO dbo.ParentMaster
    (
        Id, StudentGUID, ParentFirstName, ParentLastName, ParentDOB,
        QualificationId, Occupation, AnnualIncome, DesignationId,
        Phone, Mobile, Email,
        Address1, Address2, CityId, StateId, CountryId, ZipCode,
        OfficeAddress1, OfficeAddress2, OfficeCityId, OfficeStateId, OfficeCountryId, OfficeZipCode, OfficePhone,
        Image, RelationTypeId, SchoolId, CompanyId, IsActive, IsDeleted, CreatedBy, CreatedDate, Status, StatusMessage
    )
    VALUES
    (
        @NewId, @StudentGUID, @ParentFirstName, @ParentLastName, @ParentDOB,
        @QualificationId, @Occupation, @AnnualIncome, @DesignationId,
        @Phone, @Mobile, @Email,
        @Address1, @Address2, @CityId, @StateId, @CountryId, @ZipCode,
        @OfficeAddress1, @OfficeAddress2, @OfficeCityId, @OfficeStateId, @OfficeCountryId, @OfficeZipCode, @OfficePhone,
        @Image, @RelationTypeId, @SchoolId, @CompanyId, @IsActive, @IsDeleted, @CreatedBy, @CreatedDate, @Status, @StatusMessage
    );

    SELECT @NewId AS Id;
END
GO
