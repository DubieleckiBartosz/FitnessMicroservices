CREATE OR ALTER PROCEDURE event_AppendEvent_IU
	@id UNIQUEIDENTIFIER,
	@data VARCHAR(MAX),
	@type VARCHAR(MAX),
	@streamId UNIQUEIDENTIFIER,
	@streamType VARCHAR(MAX),
	@expectedStreamVersion BIGINT = NULL,
	@resultVersion BIT OUTPUT
AS
BEGIN
	BEGIN TRANSACTION;
		    DECLARE @streamVersion BIGINT;

		    SELECT @streamVersion = [Version]
		    FROM [Streams] WHERE Id = @streamId

	    IF(@streamVersion IS NULL)
	    BEGIN
		    SET @streamVersion = 0;

		    INSERT INTO [Streams](Id, [Type], [Version])
		    VALUES (@streamId, @streamType, @streamVersion);
	    END
	    
        --optimistic concurrency
	    IF(@expectedStreamVersion IS NOT NULL AND @streamVersion != @expectedStreamVersion)
		    BEGIN
			    SET @resultVersion = 0;
		    END
	    ELSE
		    BEGIN
			    SET @streamVersion = @streamVersion + 1;

			    INSERT INTO [Events](Id, StreamId, [Data], [Type], [Version])
			    VALUES(@id, @streamId, @data, @type, @streamVersion)

			    UPDATE [Streams] SET [Version] = @streamVersion
			    WHERE Id = @streamId

			    SET @resultVersion = 1;
		    END

	    SELECT @resultVersion;
	COMMIT;
END
GO

CREATE OR ALTER PROCEDURE event_GetBySearch_S
	@streamVersion BIGINT = NULL,
	@atTimestamp DATETIME = NULL,
	@streamId UNIQUEIDENTIFIER
AS
BEGIN
	SELECT 
	Id as Id,
	StreamId as StreamId,
	[Data] as [StreamData], 
	[Type] as EventType, 
	[Version] as [Version] 
	FROM [Events]
	WHERE StreamId = @streamId
	AND (@atTimestamp IS NULL OR Created <= @atTimestamp)
	AND (@streamVersion IS NULL OR [Version] = @streamVersion)
    ORDER BY [Version]
END
GO