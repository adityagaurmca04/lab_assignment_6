using System.Data;
using Microsoft.Data.SqlClient;

const string ConnectionString =
    "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

Console.WriteLine("Program 7: Disconnected Architecture - Update Student");
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
    using SqlDataAdapter adapter = new(
        "SELECT StudentId, Name, Course, Marks FROM dbo.Student", connection);
    using SqlCommand update = new(
        "UPDATE dbo.Student SET Name=@Name, Course=@Course, Marks=@Marks WHERE StudentId=@StudentId",
        connection);
    update.Parameters.Add("@Name", SqlDbType.VarChar, 100, "Name");
    update.Parameters.Add("@Course", SqlDbType.VarChar, 100, "Course");
    update.Parameters.Add("@Marks", SqlDbType.Int, 0, "Marks");
    update.Parameters.Add("@StudentId", SqlDbType.Int, 0, "StudentId");
    adapter.UpdateCommand = update;
    DataSet dataSet = new();
    adapter.Fill(dataSet, "Student");
    DataRow? row = dataSet.Tables["Student"]!.Rows.Cast<DataRow>()
        .FirstOrDefault(r => (int)r["StudentId"] == studentId);
    if (row is null) { Console.WriteLine("Student ID not found."); return; }
    row["Name"] = name;
    row["Course"] = course;
    row["Marks"] = marks;
    int affected = adapter.Update(dataSet, "Student");
    Console.WriteLine(affected > 0 ? "Student updated successfully." : "Update failed.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
