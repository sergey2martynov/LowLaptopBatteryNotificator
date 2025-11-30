using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace BatteryNotification
{
    public partial class BatteryMonitorForm : Form
    {
        private const string NotifyIconText = "Battery Notification Monitor";
        private const string ExitMenuItemText = "Exit";
        
        private System.Windows.Forms.Timer? batteryCheckTimer;
        private bool isWarningShown = false;
        private WarningForm? warningForm = null;
        private SystemSound? warningSound;
        private NotifyIcon? notifyIcon;

        public BatteryMonitorForm()
        {
            InitializeComponent();
            InitializeBatteryMonitoring();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(-2000, -2000);
            this.Size = new Size(1, 1);
            
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.Text = NotifyIconText;
            notifyIcon.Visible = true;
            
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add(ExitMenuItemText, null, (s, e) => Application.Exit());
            notifyIcon.ContextMenuStrip = contextMenu;
            
            this.ResumeLayout(false);
        }

        private void InitializeBatteryMonitoring()
        {
            warningSound = SystemSounds.Exclamation;
            
            batteryCheckTimer = new System.Windows.Forms.Timer();
            batteryCheckTimer.Interval = 5000;
            batteryCheckTimer.Tick += BatteryCheckTimer_Tick;
            batteryCheckTimer.Start();
            
            CheckBatteryStatus();
        }

        private void BatteryCheckTimer_Tick(object? sender, EventArgs e)
        {
            CheckBatteryStatus();
        }

        private void CheckBatteryStatus()
        {
            PowerStatus powerStatus = SystemInformation.PowerStatus;
            
            if (powerStatus.PowerLineStatus == PowerLineStatus.Unknown)
            {
                return;
            }

            float batteryPercent = powerStatus.BatteryLifePercent * 100;
            bool isCharging = powerStatus.PowerLineStatus == PowerLineStatus.Online;

            if (batteryPercent < 80 && !isCharging)
            {
                if (!isWarningShown)
                {
                    ShowWarning();
                }
            }
            else if (isCharging && isWarningShown)
            {
                HideWarning();
            }
        }

        private void ShowWarning()
        {
            if (warningForm == null || warningForm.IsDisposed)
            {
                isWarningShown = true;
                
                warningSound?.Play();
                
                warningForm = new WarningForm();
                warningForm.FormClosed += (s, e) => 
                {
                    isWarningShown = false;
                    warningForm = null;
                };
                warningForm.Show();
            }
        }

        private void HideWarning()
        {
            if (warningForm != null && !warningForm.IsDisposed)
            {
                isWarningShown = false;
                warningForm.Close();
                warningForm.Dispose();
                warningForm = null;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            batteryCheckTimer?.Stop();
            batteryCheckTimer?.Dispose();
            notifyIcon?.Dispose();
            base.OnFormClosing(e);
        }
    }
}

