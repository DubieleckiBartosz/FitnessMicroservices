CREATE OR ALTER PROCEDURE exercise_getById_S
	@exerciseId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
		e.Id,
		e.CreatedBy,
		e.[Name],
		e.Video,
		e.ExerciseDescription,
		e.Created,
		ei.Id, 
		ei.ExerciseId, 
		ei.ImagePath,
		ei.ImageTitle,
		ei.IsMain,
		ei.ImageDescription,
		ei.Created
	FROM Exercises AS e
	INNER JOIN ExerciseImages AS ei ON ei.ExerciseId = e.Id
	WHERE e.Id = @exerciseId
END
GO 

CREATE OR ALTER PROCEDURE exercise_getByName_S
	@name VARCHAR(50)
AS
BEGIN
	SELECT * FROM Exercises  
	WHERE [Name] = @name
END
GO 

CREATE OR ALTER PROCEDURE exercise_getBySearch_S 
	@id UNIQUEIDENTIFIER = NULL,
	@name VARCHAR(50) = NULL,
	@sortModelType VARCHAR(10) = NULL,
	@sortModelName VARCHAR(MAX) = NULL,
	@pageNumber INT = NULL,
	@pageSize INT = NULL
AS
BEGIN
	SELECT 
		 Id,
		 CreatedBy,
		 [Name],
		 Video,
		 ExerciseDescription,
		 Created
	FROM Exercises 
	WHERE (@id IS NULL OR Id = @id)
	AND (@name IS NULL OR [Name] = @name) 
	ORDER BY 

	CASE WHEN @sortModelName = 'Id' AND @sortModelType = 'asc' THEN Id END ASC,  
	CASE WHEN @sortModelName = 'Id' AND @sortModelType = 'desc' THEN Id END DESC, 

	CASE WHEN @sortModelName = 'Name' AND @sortModelType = 'asc' THEN [Name] END ASC,  
	CASE WHEN @sortModelName = 'Name' AND @sortModelType = 'desc' THEN [Name] END DESC,

	CASE WHEN @sortModelName = 'Created' AND @sortModelType = 'asc' THEN Created END ASC,  
	CASE WHEN @sortModelName = 'Created' AND @sortModelType = 'desc' THEN Created END DESC

	OFFSET (@pageNumber - 1)* @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY
END
GO


 
CREATE OR ALTER PROCEDURE exercise_createNewExercise_I
     @id UNIQUEIDENTIFIER,
     @name VARCHAR(50),
     @createdBy VARCHAR(MAX),
     @description VARCHAR(MAX)
AS
BEGIN 
	INSERT INTO Exercises (Id, CreatedBy, [Name], [ExerciseDescription])
	VALUES (@id, @name, @createdBy, @description)
END
GO


CREATE OR ALTER PROCEDURE exercise_updateExercise_U
	 @exerciseId UNIQUEIDENTIFIER,
     @video VARCHAR(MAX), 
     @description VARCHAR(MAX)
AS
BEGIN
	UPDATE Exercises SET Video = @video, [ExerciseDescription] = @description
	WHERE Id = @exerciseId
END
GO

 
CREATE OR ALTER PROCEDURE image_insertImage_I
	@id UNIQUEIDENTIFIER, 
	@exerciseId UNIQUEIDENTIFIER, 
	@imagePath VARCHAR(MAX), 
	@imageTitle VARCHAR(50),
	@isMain BIT, 
	@imageDescription VARCHAR(MAX)
AS
BEGIN
	INSERT INTO ExerciseImages (Id, ExerciseId, ImagePath, ImageTitle, IsMain, ImageDescription)
	VALUES (@id, @exerciseId, @imagePath, @imageTitle, @isMain, @imageDescription)
END
GO