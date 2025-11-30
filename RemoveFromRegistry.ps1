# Script to remove BatteryNotification from Windows startup
# Run: powershell -ExecutionPolicy Bypass -File RemoveFromRegistry.ps1

$RegistryKeyPath = "HKCU:\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"
$AppName = "BatteryNotification"

Write-Host "Removing BatteryNotification from Windows startup..." -ForegroundColor Yellow

try {
    $key = Get-ItemProperty -Path $RegistryKeyPath -Name $AppName -ErrorAction SilentlyContinue
    
    if ($key) {
        Remove-ItemProperty -Path $RegistryKeyPath -Name $AppName -Force
        Write-Host "✓ BatteryNotification successfully removed from startup" -ForegroundColor Green
    }
    else {
        Write-Host "⚠ BatteryNotification not found in startup" -ForegroundColor Yellow
    }
}
catch {
    Write-Host "✗ Error removing from registry: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host "`nDone!" -ForegroundColor Green

