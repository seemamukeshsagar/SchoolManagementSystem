IF OBJECT_ID(N'[dbo].[SchoolContact_Delete]', N'P') IS NOT NULL
    DROP PROCEDURE [dbo].[SchoolContact_Delete];
GO
CREATE PROCEDURE [dbo].[SchoolContact_Delete]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.SchoolContactMaster WITH (NOLOCK) WHERE Id = @Id AND IsDeleted = 0)
        RETURN 0;

    UPDATE dbo.SchoolContactMaster
    SET IsDeleted = 1,
        ModifiedDate = GETUTCDATE()
    WHERE Id = @Id;

    IF @@ROWCOUNT = 0 RETURN 0;
    RETURN 1;
END
GO
