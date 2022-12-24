IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'FitnessEventStore')
BEGIN
    CREATE DATABASE FitnessEventStore
END
    
GO 

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'FitnessEventStoreTests')
BEGIN
    CREATE DATABASE FitnessEventStoreTests
END
    
GO 