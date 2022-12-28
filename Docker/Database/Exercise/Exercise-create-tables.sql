IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Exercises' and xtype='U')
	BEGIN
		CREATE TABLE [Exercises](
			Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL, 
			CreatedBy VARCHAR(MAX) NOT NULL, 
			[Name] VARCHAR(50) NOT NULL,
			[Video] VARCHAR(MAX) NOT NULL,
			[ExerciseDescription] VARCHAR(MAX) NOT NULL, 
			Created DATETIME DEFAULT GETDATE(),
			Modified DATETIME DEFAULT GETDATE(),
		)
	END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ExerciseImages' and xtype='U')
	BEGIN
		CREATE TABLE [ExerciseImages](
			Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
			ExerciseId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [Exercises](Id),
			ImagePath VARCHAR(MAX) NOT NULL,
			ImageTitle VARCHAR(50) NOT NULL,
			IsMain BIT NOT NULL,
			ImageDescription VARCHAR(MAX) NULL,
			Created DATETIME DEFAULT GETDATE(),
			Modified DATETIME DEFAULT GETDATE(),
			CONSTRAINT Events_Stream_Version UNIQUE (StreamId, [Version])
		)
	END


	 

	 