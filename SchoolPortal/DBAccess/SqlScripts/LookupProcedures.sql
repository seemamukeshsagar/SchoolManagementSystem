-- Lookup Stored Procedures

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Qualification_GetAll
IF OBJECT_ID(N'[dbo].[Qualification_GetAll]', N'P') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[Qualification_GetAll] AS SET NOCOUNT ON;');
GO
ALTER PROCEDURE [dbo].[Qualification_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        q.[Id],
        q.[QualificationName]
    FROM dbo.[QualificationMaster] q
    ORDER BY q.[QualificationName];
END
GO

-- RelationType_GetAll
IF OBJECT_ID(N'[dbo].[RelationType_GetAll]', N'P') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[RelationType_GetAll] AS SET NOCOUNT ON;');
GO
ALTER PROCEDURE [dbo].[RelationType_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        r.[Id],
        r.[Name]
    FROM dbo.[RelationTypeMaster] r
    ORDER BY r.[Name];
END
GO

-- Designation_GetAll
IF OBJECT_ID(N'[dbo].[Designation_GetAll]', N'P') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[Designation_GetAll] AS SET NOCOUNT ON;');
GO
ALTER PROCEDURE [dbo].[Designation_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        d.[Id],
        d.[DesignationName]
    FROM dbo.[DesigMaster] d
    ORDER BY d.[DesignationName];
END
GO

-- Country_GetAll
IF OBJECT_ID(N'[dbo].[Country_GetAll]', N'P') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[Country_GetAll] AS SET NOCOUNT ON;');
GO
ALTER PROCEDURE [dbo].[Country_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        c.[Id],
        c.[CountryName]
    FROM dbo.[CountryMaster] c
    ORDER BY c.[CountryName];
END
GO

-- State_GetByCountry
IF OBJECT_ID(N'[dbo].[State_GetByCountry]', N'P') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[State_GetByCountry] @CountryId uniqueidentifier AS SET NOCOUNT ON;');
GO
ALTER PROCEDURE [dbo].[State_GetByCountry]
    @CountryId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        s.[Id],
        s.[StateName]
    FROM dbo.[StateMaster] s
    WHERE s.[CountryId] = @CountryId
    ORDER BY s.[StateName];
END
GO

-- City_GetByState
IF OBJECT_ID(N'[dbo].[City_GetByState]', N'P') IS NULL
    EXEC('CREATE PROCEDURE [dbo].[City_GetByState] @StateId uniqueidentifier AS SET NOCOUNT ON;');
GO
ALTER PROCEDURE [dbo].[City_GetByState]
    @StateId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        c.[Id],
        c.[CityName]
    FROM dbo.[CityMaster] c
    WHERE c.[StateId] = @StateId
    ORDER BY c.[CityName];
END
GO
