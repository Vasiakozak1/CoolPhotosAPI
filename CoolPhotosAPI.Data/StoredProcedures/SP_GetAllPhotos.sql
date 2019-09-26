DROP PROCEDURE IF EXISTS SP_GetAllPhotos;
GO
CREATE PROCEDURE SP_GetAllPhotos @userSocnetworkId nvarchar(50)
AS
DECLARE @userId uniqueidentifier;
SET @userId = (SELECT Users.Guid FROM Users WHERE Users.SocNetworkId = @userSocnetworkId);

SELECT * FROM Photos WHERE Photos.OwnerGuid = @userId