using System.Data;
using Microsoft.Data.SqlClient;

const string ConnectionString =
    "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

Console.WriteLine("Program 9: SqlCommandBuilder - Insert, Update and Delete");
Console.WriteLine("1. Insert  2. Update  3. Delete");
Console.Write("Choice: ");

try
{
    using SqlConnection connection = new(ConnectionString);
    using SqlDataAdapter adapter = new(
        "SELECT StudentId, Name, Course, Marks FROM dbo.Student", connection);
    using SqlCommandBuilder builder = new(adapter);
    DataSet dataSet = new();
    adapter.Fill(dataSet, "Student");
    DataTable table = dataSet.Tables["Student"]!;

    switch (Console.ReadLine())
    {
        case "1":
            Console.Write("Name: ");
            string name = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Course: ");
            string course = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Marks: ");
            int.TryParse(Console.ReadLine(), out int insertMarks);
            DataRow insertRow = table.NewRow();
            insertRow["Name"] = name;
            insertRow["Course"] = course;
            insertRow["Marks"] = insertMarks;
            table.Rows.Add(insertRow);
            Console.WriteLine($"Affected: {adapter.Update(dataSet, "Student")}");
            break;
        case "2":
            Console.Write("Student ID: ");
            int.TryParse(Console.ReadLine(), out int updateId);
            DataRow? updateRow = table.Rows.Cast<DataRow>().FirstOrDefault(r => (int)r["StudentId"] == updateId);
            if (updateRow is null) { Console.WriteLine("Student ID not found."); break; }
            Console.Write("New Marks: ");
            int.TryParse(Console.ReadLine(), out int newMarks);
            updateRow["Marks"] = newMarks;
            Console.WriteLine($"Affected: {adapter.Update(dataSet, "Student")}");
            break;
        case "3":
            Console.Write("Student ID: ");
            int.TryParse(Console.ReadLine(), out int deleteId);
            DataRow? deleteRow = table.Rows.Cast<DataRow>().FirstOrDefault(r => (int)r["StudentId"] == deleteId);
            if (deleteRow is null) { Console.WriteLine("Student ID not found."); break; }
            deleteRow.Delete();
            Console.WriteLine($"Affected: {adapter.Update(dataSet, "Student")}");
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
