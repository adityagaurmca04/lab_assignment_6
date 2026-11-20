CREATE DATABASE StudentDB;
GO

USE StudentDB;
GO

CREATE TABLE dbo.Student (
    StudentId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Course VARCHAR(100) NOT NULL,
    Marks INT NOT NULL
);
GO

INSERT INTO dbo.Student (Name, Course, Marks) VALUES
    ('Aarav Sharma', 'MCA', 85),
    ('Priya Patel', 'MCA', 92);
GO
