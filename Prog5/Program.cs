using System.Data;
using Microsoft.Data.SqlClient;

const string ConnectionString =
    "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

Console.WriteLine("Program 5: Disconnected Architecture - Retrieve Students");

try
{
    using SqlConnection connection = new(ConnectionString);
    using SqlDataAdapter adapter = new(
        "SELECT StudentId, Name, Course, Marks FROM dbo.Student ORDER BY StudentId",
        connection);
    DataSet dataSet = new("StudentData");
    adapter.Fill(dataSet, "Student");
    Console.WriteLine("\nStudentId\tName\tCourse\tMarks");
    foreach (DataRow row in dataSet.Tables["Student"]!.Rows)
    {
        Console.WriteLine($"{row["StudentId"]}\t{row["Name"]}\t{row["Course"]}\t{row["Marks"]}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
