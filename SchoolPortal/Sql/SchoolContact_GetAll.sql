IF OBJECT_ID(N'[dbo].[SchoolContact_GetAll]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SchoolContact_GetAll];
GO
CREATE PROCEDURE [dbo].[SchoolContact_GetAll]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        sc.Id,
        sc.SchoolId,
        sc.FirstName,
        sc.LastName,
        sc.Email,
        sc.Phone,
        sc.MobilePhone,
        sc.AddressLine1,
        sc.AddressLine2,
        sc.CityId,
        c.CityName AS CityName,
        sc.StateId,
        s.StateName AS StateName,
        sc.CountryId,
        co.CountryName AS CountryName,
        sc.IsActive,
        sc.IsDeleted,
        sc.CreatedBy,
        LTRIM(RTRIM(CONCAT(u1.FirstName, ' ', u1.LastName))) AS CreatedByName,
        sc.CreatedDate,
        sc.ModifiedBy,
        LTRIM(RTRIM(CONCAT(u2.FirstName, ' ', u2.LastName))) AS ModifiedByName,
        sc.ModifiedDate,
        sc.Status,
        sc.StatusMessage
    FROM dbo.SchoolContactMaster AS sc WITH (NOLOCK)
    LEFT JOIN dbo.CityMaster AS c WITH (NOLOCK) ON c.Id = sc.CityId
    LEFT JOIN dbo.StateMaster AS s WITH (NOLOCK) ON s.Id = sc.StateId
    LEFT JOIN dbo.CountryMaster AS co WITH (NOLOCK) ON co.Id = sc.CountryId
    LEFT JOIN dbo.UserDetails AS u1 WITH (NOLOCK) ON u1.Id = sc.CreatedBy
    LEFT JOIN dbo.UserDetails AS u2 WITH (NOLOCK) ON u2.Id = sc.ModifiedBy
    WHERE sc.IsDeleted = 0;
END
GO
