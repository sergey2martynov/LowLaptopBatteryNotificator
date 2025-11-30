using System;
using System.Drawing;
using System.Windows.Forms;

namespace BatteryNotification
{
    public partial class WarningForm : Form
    {
        private const string FormTitle = "Low Battery Warning";
        private const string WarningMessage = "WARNING!\n\nBattery level is critically low!\n\nPlease connect the charger.";
        private const string ChargerConnectedFormat = "Charger connected. Battery: {0:F1}%";
        private const string CurrentBatteryLevelFormat = "Current battery level: {0:F1}%";
        
        private System.Windows.Forms.Timer? checkTimer;
        private Label? messageLabel;
        private Label? batteryLabel;

        public WarningForm()
        {
            InitializeComponent();
            InitializeWarningForm();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            this.Text = FormTitle;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Size = new Size(500, 300);
            this.BackColor = Color.FromArgb(255, 200, 50, 50);
            
            messageLabel = new Label();
            messageLabel.Text = WarningMessage;
            messageLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            messageLabel.ForeColor = Color.White;
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;
            messageLabel.Dock = DockStyle.Fill;
            messageLabel.AutoSize = false;
            messageLabel.Padding = new Padding(20);
            
            batteryLabel = new Label();
            batteryLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            batteryLabel.ForeColor = Color.White;
            batteryLabel.TextAlign = ContentAlignment.MiddleCenter;
            batteryLabel.Dock = DockStyle.Bottom;
            batteryLabel.Height = 50;
            batteryLabel.Padding = new Padding(10);
            
            this.Controls.Add(messageLabel);
            this.Controls.Add(batteryLabel);
            
            this.ResumeLayout(false);
        }

        private void InitializeWarningForm()
        {
            checkTimer = new System.Windows.Forms.Timer();
            checkTimer.Interval = 1000;
            checkTimer.Tick += CheckTimer_Tick;
            checkTimer.Start();
            
            UpdateBatteryInfo();
        }

        private void CheckTimer_Tick(object? sender, EventArgs e)
        {
            PowerStatus powerStatus = SystemInformation.PowerStatus;
            float batteryPercent = powerStatus.BatteryLifePercent * 100;
            bool isCharging = powerStatus.PowerLineStatus == PowerLineStatus.Online;
            
            UpdateBatteryInfo();
            
            if (isCharging)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void UpdateBatteryInfo()
        {
            PowerStatus powerStatus = SystemInformation.PowerStatus;
            float batteryPercent = powerStatus.BatteryLifePercent * 100;
            bool isCharging = powerStatus.PowerLineStatus == PowerLineStatus.Online;
            
            if (isCharging)
            {
                batteryLabel!.Text = string.Format(ChargerConnectedFormat, batteryPercent);
            }
            else
            {
                batteryLabel!.Text = string.Format(CurrentBatteryLevelFormat, batteryPercent);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            PowerStatus powerStatus = SystemInformation.PowerStatus;
            bool isCharging = powerStatus.PowerLineStatus == PowerLineStatus.Online;
            
            if (!isCharging)
            {
                e.Cancel = true;
                return;
            }
            
            checkTimer?.Stop();
            checkTimer?.Dispose();
            base.OnFormClosing(e);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.F4))
            {
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
