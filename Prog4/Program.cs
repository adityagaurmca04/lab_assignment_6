using Microsoft.Data.SqlClient;

const string ConnectionString =
    "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

Console.WriteLine("Program 4: Connected Architecture - Delete Student");
Console.Write("Student ID to delete: ");
int.TryParse(Console.ReadLine(), out int studentId);

try
{
    using SqlConnection connection = new(ConnectionString);
    using SqlCommand command = new(
        "DELETE FROM dbo.Student WHERE StudentId=@StudentId",
        connection);
    command.Parameters.AddWithValue("@StudentId", studentId);
    connection.Open();
    int affected = command.ExecuteNonQuery();
    Console.WriteLine(affected > 0 ? "Student deleted successfully." : "Student ID not found.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
