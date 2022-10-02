CREATE OR ALTER PROCEDURE [dbo].[user_createNewUser_I]
	@firstName VARCHAR(50),
	@lastName VARCHAR(50),
	@userName VARCHAR(50),
	@email VARCHAR(50),
	@phoneNumber VARCHAR(50),
	@passwordHash VARCHAR(MAX),
	@newIdentity INT OUTPUT
AS
BEGIN 

	INSERT INTO ApplicationUsers(FirstName, LastName,
		UserName, Email, PhoneNumber, PasswordHash) 
		VALUES (@firstName, @lastName, @userName, 
		@email, @phoneNumber, @passwordHash) 
		
    SELECT @newIdentity = CAST(SCOPE_IDENTITY() AS INT) 
END
GO

CREATE OR ALTER PROCEDURE [dbo].[user_addToRole_I] 
	@userId INT, 
	@role INT 
AS 
BEGIN 
	INSERT INTO UserRoles(UserId, RoleId) 
	VALUES (@userId, @role) 
END
GO

CREATE OR ALTER PROCEDURE [dbo].[user_clearRevokedTokens_D]
AS
BEGIN
	DELETE FROM RefreshTokens
	WHERE Revoked IS NOT NULL
END
GO

CREATE OR ALTER PROCEDURE [dbo].[user_getUserByEmail_S]
	@email VARCHAR(50)
AS
BEGIN 
	SELECT 
	au.Id AS Id,
	au.FirstName,
	au.LastName,
	au.UserName,
	au.Email,
	au.PhoneNumber,
	au.PasswordHash,
	rt.Id,
	rt.Token,
	rt.Expires,
	rt.Created,
	rt.Revoked
	FROM ApplicationUsers AS au 
	LEFT JOIN RefreshTokens AS rt ON rt.UserId = au.Id 
	WHERE au.Email = @email
END
GO


CREATE OR ALTER PROCEDURE [dbo].[user_getUserByToken_S]
	@tokenKey VARCHAR(MAX)
AS
BEGIN 
	SELECT
	au.Id,
	au.FirstName,
	au.LastName,
	au.UserName,
	au.Email,
	au.PhoneNumber,
	au.PasswordHash,
	rt.Id,
	rt.Token,
	rt.Expires,
	rt.Created,
	rt.Revoked
	FROM RefreshTokens AS rt
	INNER JOIN ApplicationUsers AS au ON au.Id = rt.UserId
	WHERE rt.Token = @tokenKey
END
GO


CREATE OR ALTER PROCEDURE [dbo].[user_getUserRoles_S]
@userId INT
AS
BEGIN
	SELECT r.RoleName FROM UserRoles AS ur
	INNER JOIN Roles AS r ON r.Id = ur.RoleId
	WHERE ur.UserId = @userId
END
GO


CREATE OR ALTER PROCEDURE [dbo].[user_updateUserData_U]
	@userId INT,
	@email VARCHAR(50) = NULL,
	@phoneNumber VARCHAR(50) = NULL,
	@refreshTokens UserRefreshTokensTableType READONLY 
AS
BEGIN 
	BEGIN TRANSACTION

	MERGE RefreshTokens AS target
	USING (SELECT Token, Expires, Created, ReplacedByToken, Revoked FROM @refreshTokens) AS source
	ON (target.UserId = @userId AND target.Token = source.Token)
	WHEN MATCHED THEN 
		UPDATE SET Token = COALESCE(source.Token, target.Token),
		           Expires = CONVERT(DATETIME, source.Expires, 120), 
		           Created = CONVERT(DATETIME, source.Created, 120),
		           ReplacedByToken = COALESCE(source.ReplacedByToken, target.ReplacedByToken),
		           Revoked = COALESCE(source.Revoked, target.Revoked)
	WHEN NOT MATCHED THEN 
		INSERT(UserId, Token, Expires, Created, ReplacedByToken, Revoked) 
		VALUES(@userId, source.Token, source.Expires, source.Created,
		source.ReplacedByToken, source.Revoked);

	UPDATE ApplicationUsers 
	SET Email = COALESCE(@email, Email) , 
	PhoneNumber = COALESCE(@phoneNumber, PhoneNumber),
	Modified = GETDATE()
	WHERE Id = @userId

	COMMIT
END
GO