@echo off
REM Batch script to remove BatteryNotification from Windows startup
REM Run: double-click the file or run from command line

echo Removing BatteryNotification from Windows startup...
echo.

reg delete "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run" /v BatteryNotification /f >nul 2>&1

if %errorlevel% equ 0 (
    echo [OK] BatteryNotification successfully removed from startup
) else (
    echo [INFO] BatteryNotification not found in startup or already removed
)

echo.
echo Done!
pause

