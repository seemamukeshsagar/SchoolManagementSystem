# Add Teacher Data Scripts

This repository contains scripts to add 15 sample teacher records to the School Management System database.

## Prerequisites

- SQL Server with the SchoolManagementSystem database
- PowerShell (for the PowerShell script)
- .NET 6 SDK (for the C# script)

## Option 1: PowerShell Script

### File
- `AddTeacherData.ps1`

### Usage
1. Open PowerShell as Administrator
2. Navigate to the script directory
3. Run the script:
```powershell
.\AddTeacherData.ps1
```

### What it does
- Connects to the database
- Checks for existing Company and School records
- Creates sample Company and School records if they don't exist
- Adds 15 sample teacher records using the `Teacher_Create` stored procedure

## Option 2: C# Console Application

### Files
- `AddTeacherData.cs`
- `AddTeacherData.csproj`

### Usage
1. Open a command prompt or terminal
2. Navigate to the script directory
3. Build the project:
```bash
dotnet build
```
4. Run the application:
```bash
dotnet run
```

### What it does
- Connects to the database
- Checks for existing Company and School records
- Creates sample Company and School records if they don't exist
- Adds 15 sample teacher records using the `Teacher_Create` stored procedure

## Notes

- The scripts will automatically create sample Company and School records if none exist
- All teachers are created with active status
- The connection string in both scripts may need to be adjusted based on your SQL Server configuration
- The scripts use the existing stored procedures to ensure data consistency

## Customization

You can modify the teacher data in either script:
- In the PowerShell script, edit the `$teachers` array
- In the C# script, edit the `GetSampleTeachers()` method