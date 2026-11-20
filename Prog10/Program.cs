using System.Data;
using Microsoft.Data.SqlClient;

const string ConnectionString =
    "Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

Console.WriteLine("Program 10: Connected and Disconnected ADO.NET CRUD");
Console.WriteLine("1. Connected CRUD");
Console.WriteLine("2. Disconnected CRUD");
Console.Write("Architecture: ");

try
{
    if (Console.ReadLine() == "1")
    {
        using SqlConnection connection = new(ConnectionString);
        connection.Open();
        using (SqlCommand insert = new(
            "INSERT INTO dbo.Student (Name, Course, Marks) VALUES (@Name, @Course, @Marks)",
            connection))
        {
            insert.Parameters.AddWithValue("@Name", "Connected Demo");
            insert.Parameters.AddWithValue("@Course", "MCA");
            insert.Parameters.AddWithValue("@Marks", 80);
            Console.WriteLine($"Connected Insert: {insert.ExecuteNonQuery()} row");
        }
        using SqlCommand read = new("SELECT COUNT(*) FROM dbo.Student", connection);
        Console.WriteLine($"Connected Read count: {read.ExecuteScalar()}");
        using SqlCommand update = new(
            "UPDATE dbo.Student SET Marks=81 WHERE Name='Connected Demo'", connection);
        Console.WriteLine($"Connected Update: {update.ExecuteNonQuery()} row(s)");
        using SqlCommand delete = new(
            "DELETE FROM dbo.Student WHERE Name='Connected Demo'", connection);
        Console.WriteLine($"Connected Delete: {delete.ExecuteNonQuery()} row(s)");
    }
    else
    {
        using SqlConnection connection = new(ConnectionString);
        using SqlDataAdapter adapter = new(
            "SELECT StudentId, Name, Course, Marks FROM dbo.Student", connection);
        using SqlCommandBuilder builder = new(adapter);
        DataSet dataSet = new();
        adapter.Fill(dataSet, "Student");
        DataTable table = dataSet.Tables["Student"]!;
        DataRow row = table.NewRow();
        row["Name"] = "Disconnected Demo";
        row["Course"] = "MCA";
        row["Marks"] = 70;
        table.Rows.Add(row);
        int inserted = adapter.Update(dataSet, "Student");
        Console.WriteLine($"Disconnected Insert: {inserted} row");

        DataRow demoRow = table.Rows.Cast<DataRow>()
            .Single(r => r.RowState != DataRowState.Deleted &&
                         r["Name"].ToString() == "Disconnected Demo");
        demoRow["Marks"] = 71;
        int updated = adapter.Update(dataSet, "Student");
        Console.WriteLine($"Disconnected Update: {updated} row");
        demoRow.Delete();
        int deleted = adapter.Update(dataSet, "Student");
        Console.WriteLine($"Disconnected Delete: {deleted} row");
    }

    Console.WriteLine("\nComparison:");
    Console.WriteLine("- Connected: connection remains open; SqlCommand sends operations directly.");
    Console.WriteLine("- Disconnected: data is loaded into memory; Update applies all changes.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
