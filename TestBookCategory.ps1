# Test BookCategoryMaster table existence
Write-Host "Testing BookCategoryMaster table existence..."

try {
    $machineName = $env:COMPUTERNAME
    $connectionString = "Data Source=$machineName\SQl2025;Initial Catalog=SchoolManagementSystem;Application Name=Unity Enterprise;Integrated Security=True"
    $connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $connection.Open()
    Write-Host "Database connection successful!" -ForegroundColor Green
    
    # Test BookCategoryMaster table
    $command = New-Object System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM BookCategoryMaster", $connection)
    $count = $command.ExecuteScalar()
    Write-Host "BookCategoryMaster table has $count records." -ForegroundColor Green
    
    $connection.Close()
    Write-Host "Test completed successfully!" -ForegroundColor Green
}
catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "Exception type: $($_.Exception.GetType().FullName)" -ForegroundColor Red
}