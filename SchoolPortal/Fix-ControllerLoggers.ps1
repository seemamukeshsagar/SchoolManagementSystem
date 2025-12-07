# PowerShell script to fix logger hiding in controllers
$controllersDir = "d:\SchoolPortal\SchoolPortal\SchoolPortalApp\Controllers"
$controllerFiles = Get-ChildItem -Path $controllersDir -Filter "*Controller.cs" -File

foreach ($file in $controllerFiles) {
    $content = Get-Content -Path $file.FullName -Raw
    
    # Skip files that already have the new keyword
    if ($content -match "protected new readonly ILogger") {
        Write-Host "Skipping $($file.Name) - already updated"
        continue
    }
    
    # Get the controller name from the filename
    $controllerName = $file.BaseName
    
    # Add 'new' keyword to logger field and update type
    $content = $content -replace "protected readonly ILogger", "protected new readonly ILogger"
    $content = $content -replace "ILogger<BaseController>", "ILogger<$controllerName>"
    
    # Update the constructor parameter type
    $content = $content -replace "ILogger<BaseController>", "ILogger<$controllerName>"
    
    # Add null check in constructor
    if ($content -match "public\s+$controllerName\s*\([^)]*ILogger") {
        $content = $content -replace "(_logger\s*=\s*logger;)", "_logger = logger ?? throw new ArgumentNullException(nameof(logger));"
    }
    
    Set-Content -Path $file.FullName -Value $content -NoNewline
    Write-Host "Updated $($file.Name)"
}

Write-Host "All controller files have been processed."
