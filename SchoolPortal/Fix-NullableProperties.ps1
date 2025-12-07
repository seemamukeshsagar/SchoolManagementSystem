# PowerShell script to fix non-nullable string properties in C# model classes

# Define the root directory to search for .cs files
$rootDir = "d:\SchoolPortal\SchoolPortal\SchoolPortalApp\Models"

# Get all .cs files in the directory and subdirectories
$files = Get-ChildItem -Path $rootDir -Filter "*.cs" -Recurse -File

foreach ($file in $files) {
    $content = Get-Content -Path $file.FullName -Raw
    
    # Skip files that already have nullable reference types enabled
    if ($content -match "#nullable enable") {
        continue
    }
    
    # Add nullable enable directive at the top of the file
    $content = "#nullable enable`n`n" + $content
    
    # Find all public string properties that don't have a default value and aren't already nullable
    $pattern = 'public\s+string\s+(\w+)\s*\{\s*get;\s*set;\s*\}'
    $matches = [regex]::Matches($content, $pattern)
    
    foreach ($match in $matches) {
        $propertyName = $match.Groups[1].Value
        $newProperty = "public string {0} {{ get; set; }} = string.Empty;" -f $propertyName
        $content = $content.Replace($match.Value, $newProperty)
    }
    
    # Save the modified content back to the file
    Set-Content -Path $file.FullName -Value $content -NoNewline
    Write-Host "Updated $($file.Name)"
}

Write-Host "All model files have been processed."
