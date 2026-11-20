using Microsoft.Data.SqlClient;

const string ConnectionString =
    "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

Console.WriteLine("Program 3: Connected Architecture - Update Student");
Console.Write("Student ID: ");
int.TryParse(Console.ReadLine(), out int studentId);
Console.Write("New Name: ");
string name = Console.ReadLine()?.Trim() ?? "";
Console.Write("New Course: ");
string course = Console.ReadLine()?.Trim() ?? "";
Console.Write("New Marks: ");
int.TryParse(Console.ReadLine(), out int marks);

try
{
    using SqlConnection connection = new(ConnectionString);
    using SqlCommand command = new(
        "UPDATE dbo.Student SET Name=@Name, Course=@Course, Marks=@Marks WHERE StudentId=@StudentId",
        connection);
    command.Parameters.AddWithValue("@StudentId", studentId);
    command.Parameters.AddWithValue("@Name", name);
    command.Parameters.AddWithValue("@Course", course);
    command.Parameters.AddWithValue("@Marks", marks);
    connection.Open();
    int affected = command.ExecuteNonQuery();
    Console.WriteLine(affected > 0 ? "Student updated successfully." : "Student ID not found.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
