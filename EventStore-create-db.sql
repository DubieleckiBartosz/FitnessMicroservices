IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'FitnessEventStore')
BEGIN
    CREATE DATABASE FitnessEventStore
END
    
GO 