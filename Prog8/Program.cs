using System.Data;
using Microsoft.Data.SqlClient;

const string ConnectionString =
    "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

Console.WriteLine("Program 8: Disconnected Architecture - Delete Student");
Console.Write("Student ID to delete: ");
int.TryParse(Console.ReadLine(), out int studentId);

try
{
    using SqlConnection connection = new(ConnectionString);
    using SqlDataAdapter adapter = new(
        "SELECT StudentId, Name, Course, Marks FROM dbo.Student", connection);
    using SqlCommand delete = new(
        "DELETE FROM dbo.Student WHERE StudentId=@StudentId", connection);
    delete.Parameters.Add("@StudentId", SqlDbType.Int, 0, "StudentId");
    adapter.DeleteCommand = delete;
    DataSet dataSet = new();
    adapter.Fill(dataSet, "Student");
    DataRow? row = dataSet.Tables["Student"]!.Rows.Cast<DataRow>()
        .FirstOrDefault(r => (int)r["StudentId"] == studentId);
    if (row is null) { Console.WriteLine("Student ID not found."); return; }
    row.Delete();
    int affected = adapter.Update(dataSet, "Student");
    Console.WriteLine(affected > 0 ? "Student deleted successfully." : "Delete failed.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
