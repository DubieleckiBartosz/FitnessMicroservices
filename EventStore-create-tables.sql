IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Streams' and xtype='U')
	BEGIN
		CREATE TABLE [Streams](
			Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL, 
			[Type] VARCHAR(MAX) NOT NULL,
			[Version] BIGINT NOT NULL
		)
	END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Events' and xtype='U')
	BEGIN
		CREATE TABLE [Events](
			Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
			StreamId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [Streams](Id),
			[Data] VARCHAR(MAX) NOT NULL,
			[Type] VARCHAR(MAX) NOT NULL,
			[Version] BIGINT NOT NULL,
			Created DATETIME DEFAULT GETDATE(),
			CONSTRAINT Events_Stream_Version UNIQUE (StreamId, [Version])
		)
	END