# Test database connection
Write-Host "Testing database connection..."

# Try to connect to the database
try {
    $machineName = $env:COMPUTERNAME
    $connectionString = "Data Source=$machineName\SQl2025;Initial Catalog=SchoolManagementSystem;Application Name=Unity Enterprise;Integrated Security=True"
    $connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $connection.Open()
    Write-Host "Database connection successful!" -ForegroundColor Green
    
    # Test TeacherMaster table
    $command = New-Object System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM TeacherMaster", $connection)
    $count = $command.ExecuteScalar()
    Write-Host "TeacherMaster table has $count records." -ForegroundColor Green
    
    # Test EmpMaster table
    $command = New-Object System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM EmpMaster", $connection)
    $count = $command.ExecuteScalar()
    Write-Host "EmpMaster table has $count records." -ForegroundColor Green
    
    $connection.Close()
    Write-Host "All tests passed!" -ForegroundColor Green
}
catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "Exception type: $($_.Exception.GetType().FullName)" -ForegroundColor Red
    Write-Host "Stack trace: $($_.Exception.StackTrace)" -ForegroundColor Red
}