using Microsoft.Data.SqlClient;

const string ConnectionString =
    "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

Console.WriteLine("Program 2: Connected Architecture - Insert Student");
Console.Write("Name: ");
string name = Console.ReadLine()?.Trim() ?? "";
Console.Write("Course: ");
string course = Console.ReadLine()?.Trim() ?? "";
Console.Write("Marks: ");
int.TryParse(Console.ReadLine(), out int marks);

try
{
    using SqlConnection connection = new(ConnectionString);
    using SqlCommand command = new(
        "INSERT INTO dbo.Student (Name, Course, Marks) VALUES (@Name, @Course, @Marks)",
        connection);
    command.Parameters.AddWithValue("@Name", name);
    command.Parameters.AddWithValue("@Course", course);
    command.Parameters.AddWithValue("@Marks", marks);
    connection.Open();
    int affected = command.ExecuteNonQuery();
    Console.WriteLine(affected == 1 ? "Student inserted successfully." : "Insert failed.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
