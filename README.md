# Lab Assignment 7 - ADO.NET with SQL Server

This solution contains ten console applications:

- `Prog1`: Connected read using `SqlConnection`, `SqlCommand`, and `SqlDataReader`
- `Prog2`: Connected insert using `SqlCommand`
- `Prog3`: Connected update using `SqlCommand`
- `Prog4`: Connected delete using `SqlCommand`
- `Prog5`: Disconnected read using `SqlDataAdapter` and `DataSet`
- `Prog6`: Disconnected insert using `DataSet` and `SqlDataAdapter`
- `Prog7`: Disconnected update using `DataSet` and `SqlDataAdapter`
- `Prog8`: Disconnected delete using `DataSet` and `SqlDataAdapter`
- `Prog9`: Insert, update, and delete using `SqlCommandBuilder`
- `Prog10`: Complete connected/disconnected CRUD comparison

## Database setup

1. Open SQL Server Management Studio and run `DatabaseSetup.sql`.
2. Confirm that `StudentDB.dbo.Student` exists.
3. The default connection string uses Windows authentication:

   `Server=localhost;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;`

4. Change the connection string in each program if SQL Server uses another server name or SQL authentication.

## Run a program

```powershell
dotnet run --project Prog1
```

Build the complete solution:

```powershell
dotnet build LabAssignment7.slnx
```
