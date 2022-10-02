IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'FitnessIdentity')
BEGIN
    CREATE DATABASE FitnessIdentity
END
    
GO

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'FitnessIdentityTests')
BEGIN
    CREATE DATABASE FitnessIdentityTests
END
    
GO