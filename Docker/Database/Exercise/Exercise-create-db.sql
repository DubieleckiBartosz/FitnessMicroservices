IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'FitnessExercises')
BEGIN
    CREATE DATABASE FitnessExercises
END
    
GO 