using System.Data;
using Microsoft.Data.SqlClient;

const string ConnectionString =
    "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

Console.WriteLine("Program 6: Disconnected Architecture - Insert Student");
Console.Write("Name: ");
string name = Console.ReadLine()?.Trim() ?? "";
Console.Write("Course: ");
string course = Console.ReadLine()?.Trim() ?? "";
Console.Write("Marks: ");
int.TryParse(Console.ReadLine(), out int marks);

try
{
    using SqlConnection connection = new(ConnectionString);
    using SqlDataAdapter adapter = new(
        "SELECT StudentId, Name, Course, Marks FROM dbo.Student", connection);
    using SqlCommand insert = new(
        "INSERT INTO dbo.Student (Name, Course, Marks) VALUES (@Name, @Course, @Marks)", connection);
    insert.Parameters.Add("@Name", SqlDbType.VarChar, 100, "Name");
    insert.Parameters.Add("@Course", SqlDbType.VarChar, 100, "Course");
    insert.Parameters.Add("@Marks", SqlDbType.Int, 0, "Marks");
    adapter.InsertCommand = insert;
    DataSet dataSet = new();
    adapter.Fill(dataSet, "Student");
    DataTable table = dataSet.Tables["Student"]!;
    DataRow row = table.NewRow();
    row["Name"] = name;
    row["Course"] = course;
    row["Marks"] = marks;
    table.Rows.Add(row);
    int affected = adapter.Update(dataSet, "Student");
    Console.WriteLine(affected == 1 ? "Student inserted successfully." : "Insert failed.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
