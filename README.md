# Battery Notification

Application for monitoring laptop battery charge with low battery notifications.

## Features

- **Battery monitoring**: Continuous tracking of battery charge level
- **Low battery warning**: When charge is below 20%, a modal warning window appears
- **Non-closable notification**: The warning window cannot be closed until the charger is connected
- **Sound notification**: A warning sound plays when the notification appears
- **Auto-start**: The application automatically adds itself to Windows startup

## Requirements

- .NET 8.0 or higher
- Windows 10/11
- Laptop with battery

## Build

```bash
dotnet build
```

## Run

```bash
dotnet run
```

Or build an executable file:

```bash
dotnet publish -c Release -r win-x64 --self-contained false
```

## Auto-start Setup

The application automatically adds itself to Windows startup on first launch. The entry is created in the Windows registry at:
`HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run`

## Remove from Auto-start

To remove the application from auto-start, you can use one of the following methods:

### Method 1: Use script (recommended)
- **PowerShell**: Run `RemoveFromRegistry.ps1` (right-click → "Run with PowerShell")
- **BAT file**: Double-click `RemoveFromRegistry.bat`

### Method 2: Through Registry Editor
1. Open Registry Editor (regedit)
2. Navigate to: `HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Run`
3. Delete the `BatteryNotification` key

### Method 3: Through Task Manager
1. Open Task Manager (Ctrl+Shift+Esc)
2. Go to the "Startup" tab
3. Find "BatteryNotification" and disable it

## Usage

After launch, the application runs in the background (in the system tray). When battery charge is below 20% and no charger is connected:

1. A modal warning window appears in the center of the screen
2. A warning sound plays
3. The window cannot be closed until the charger is connected
4. After connecting the charger, the window automatically closes

## Project Structure

- `Program.cs` - Application entry point
- `BatteryMonitorForm.cs` - Main form with battery monitoring
- `WarningForm.cs` - Low battery warning form
- `AutoStartManager.cs` - Auto-start management
- `RemoveFromRegistry.ps1` - PowerShell script to remove from auto-start
- `RemoveFromRegistry.bat` - BAT file to remove from auto-start

## License

Free to use.

