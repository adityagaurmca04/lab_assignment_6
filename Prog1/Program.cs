using Microsoft.Data.SqlClient;

const string ConnectionString =
    "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

Console.WriteLine("Program 1: Connected Architecture - Display Students");

try
{
    using SqlConnection connection = new(ConnectionString);
    using SqlCommand command = new(
        "SELECT StudentId, Name, Course, Marks FROM dbo.Student ORDER BY StudentId",
        connection);

    connection.Open();
    using SqlDataReader reader = command.ExecuteReader();
    Console.WriteLine("\nStudentId\tName\tCourse\tMarks");
    while (reader.Read())
    {
        Console.WriteLine($"{reader["StudentId"]}\t{reader["Name"]}\t{reader["Course"]}\t{reader["Marks"]}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
