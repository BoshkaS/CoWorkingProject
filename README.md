# CoWorkingProject

A proof-of-concept web application for booking coworking spaces, built using ASP.NET Core Web API (backend) and Angular (frontend). The app allows users to reserve desks, private rooms, and meeting rooms, ensuring no double bookings and enforcing booking rules.

## Installation

Open the solution in Visual Studio and press F5.

Then update the appsettings.Development.json in your ASP.NET Core project:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=mydb;Username=myuser;Password=mypassword"
}
```
Create database in pgAdmin and make migrations

```bash
cd ..\CoWorkingProject.Server\
dotnet ef migrations add Initial
dotnet ef database update
dotnet run
```
