using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Hardware_Monitor_dla_deda
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView1;
        private NumericUpDown numThreshold;
        private TextBox txtSoundPath;
        private Button btnBrowseSound;
        private Button btnTestSound;
        private Button btnStopSound;
        private Button btnApplyThreshold;
        private Button btnCaptureHotkey;
        private Button btnClearHotkey;
        private CheckBox chkAutoStart;
        private CheckBox chkStartMinimized;
        private Label lblIndicator;
        private TrackBar trackBarVolume;
        private TextBox txtHotkeyDisplay;
        private Label lblTitle;
        private Label lblVersion;
        private Label lblThresholdLabel;
        private Label lblSoundLabel;
        private Label lblVolumeLabel;
        private Label lblHotkeyLabel;
        private Label lblVolumePercent;
        private Panel panelHeader;
        private Panel panelContent;
        private Panel panelGrid;
        private Panel panelAlertConfig;
        private Panel panelSoundConfig;
        private Panel panelHotkey;
        private Panel panelStatusBar;
        private System.Windows.Forms.Timer _statusAnimTimer;
        private PictureBox iconBox;

        private void InitializeComponent()
        {
            // ====== PALETTE ======
            var cBg = Color.FromArgb(18, 20, 26);
            var cPanel = Color.FromArgb(26, 28, 36);
            var cPanelAlt = Color.FromArgb(30, 33, 42);
            var cInput = Color.FromArgb(36, 38, 48);
            var cInputBorder = Color.FromArgb(50, 53, 65);
            var cAccent = Color.FromArgb(82, 130, 255);
            var cAccentHover = Color.FromArgb(105, 148, 255);
            var cAccentDim = Color.FromArgb(60, 100, 210);
            var cText = Color.FromArgb(228, 232, 240);
            var cTextDim = Color.FromArgb(148, 156, 170);
            var cTextMuted = Color.FromArgb(100, 108, 122);
            var cDanger = Color.FromArgb(255, 80, 80);
            var cWarning = Color.FromArgb(255, 180, 50);
            var cSuccess = Color.FromArgb(60, 210, 120);
            var cGridLine = Color.FromArgb(40, 43, 54);
            var cGridHeaderBg = Color.FromArgb(32, 35, 45);
            var cGridRowAlt = Color.FromArgb(28, 30, 38);
            var cBtnSecondary = Color.FromArgb(42, 45, 55);
            var cBtnSecondaryHover = Color.FromArgb(55, 58, 70);
            var cSeparator = Color.FromArgb(40, 43, 54);

            // ====== CONTROLS ======
            dataGridView1 = new DataGridView();
            panelHeader = new Panel();
            panelContent = new Panel();
            panelGrid = new Panel();
            panelAlertConfig = new Panel();
            panelSoundConfig = new Panel();
            panelHotkey = new Panel();
            panelStatusBar = new Panel();
            lblTitle = new Label();
            lblVersion = new Label();
            lblIndicator = new Label();
            lblThresholdLabel = new Label();
            lblSoundLabel = new Label();
            lblVolumeLabel = new Label();
            lblHotkeyLabel = new Label();
            lblVolumePercent = new Label();
            numThreshold = new NumericUpDown();
            txtSoundPath = new TextBox();
            txtHotkeyDisplay = new TextBox();
            btnBrowseSound = new Button();
            btnTestSound = new Button();
            btnStopSound = new Button();
            btnApplyThreshold = new Button();
            btnCaptureHotkey = new Button();
            btnClearHotkey = new Button();
            trackBarVolume = new TrackBar();
            chkAutoStart = new CheckBox();
            chkStartMinimized = new CheckBox();
            _statusAnimTimer = new System.Windows.Forms.Timer();
            iconBox = new PictureBox();

            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numThreshold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconBox).BeginInit();
            SuspendLayout();

            // ====== MAIN FORM ======
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(720, 600);
            BackColor = cBg;
            MinimumSize = new Size(680, 520);
            Text = "Hardware Monitor";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            KeyPreview = true;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            Padding = new Padding(0);

            // ====== HEADER PANEL ======
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 64;
            panelHeader.BackColor = cPanel;
            panelHeader.Padding = new Padding(20, 0, 20, 0);
            panelHeader.Paint += (s, e) =>
            {
                var rect = panelHeader.ClientRectangle;
                using var brush = new LinearGradientBrush(rect, Color.FromArgb(38, 41, 54), cPanel, LinearGradientMode.Vertical);
                e.Graphics.FillRectangle(brush, rect);
                using var pen = new Pen(Color.FromArgb(50, 53, 65), 1);
                e.Graphics.DrawLine(pen, rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
            };

            // App icon placeholder (colored circle)
            iconBox.Size = new Size(36, 36);
            iconBox.Location = new Point(4, (panelHeader.Height - 36) / 2);
            iconBox.BackColor = Color.Transparent;
            iconBox.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var r = iconBox.ClientRectangle;
                r.Inflate(-2, -2);
                using var brush = new LinearGradientBrush(r, cAccent, Color.FromArgb(100, 80, 220), 45f);
                e.Graphics.FillEllipse(brush, r);
                using var font = new Font("Segoe UI", 14, FontStyle.Bold);
                using var textBrush = new SolidBrush(Color.White);
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString("HM", font, textBrush, r, sf);
            };
            panelHeader.Controls.Add(iconBox);

            lblTitle.Text = "HARDWARE MONITOR";
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = cText;
            lblTitle.Location = new Point(50, 8);
            lblTitle.AutoSize = true;

            lblVersion.Text = "v1.0";
            lblVersion.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            lblVersion.ForeColor = cTextMuted;
            lblVersion.Location = new Point(52, 30);
            lblVersion.AutoSize = true;

            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblVersion);

            // ====== CONTENT PANEL (scrollable area) ======
            panelContent.Dock = DockStyle.Fill;
            panelContent.BackColor = cBg;
            panelContent.AutoScroll = true;
            panelContent.Padding = new Padding(16, 16, 16, 8);

            // ====== GRID PANEL ======
            panelGrid.Dock = DockStyle.Top;
            panelGrid.Height = 210;
            panelGrid.BackColor = cPanel;
            panelGrid.Padding = new Padding(2);
            panelGrid.Paint += (s, e) =>
            {
                var rect = panelGrid.ClientRectangle;
                using var pen = new Pen(cSeparator, 1);
                var r = rect; r.Width--; r.Height--;
                e.Graphics.DrawRectangle(pen, r);
            };

            var lblGridTitle = new Label
            {
                Text = "ТЕМПЕРАТУРА КОМПОНЕНТОВ",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = cTextDim,
                Location = new Point(14, 10),
                AutoSize = true
            };
            panelGrid.Controls.Add(lblGridTitle);

            // DataGridView
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = 36;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = cTextDim;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = cGridHeaderBg;
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(14, 0, 0, 0);
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.AdvancedColumnHeadersBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView1.AdvancedColumnHeadersBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView1.AdvancedColumnHeadersBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
            dataGridView1.AdvancedColumnHeadersBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.Single;

            dataGridView1.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "Hardware", HeaderText = "  Устройство", Width = 180 },
                new DataGridViewTextBoxColumn { Name = "Sensor", HeaderText = "Датчик", Width = 170 },
                new DataGridViewTextBoxColumn { Name = "Temperature", HeaderText = "Температура", Width = 130 },
                new DataGridViewTextBoxColumn { Name = "Max", HeaderText = "Максимум", Width = 130 }
            });
            dataGridView1.Location = new Point(2, 32);
            dataGridView1.Size = new Size(panelGrid.Width - 4, panelGrid.Height - 36);
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.RowTemplate.Height = 36;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dataGridView1.DefaultCellStyle.ForeColor = cText;
            dataGridView1.DefaultCellStyle.BackColor = cPanel;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(50, 55, 75);
            dataGridView1.DefaultCellStyle.SelectionForeColor = cText;
            dataGridView1.DefaultCellStyle.Padding = new Padding(14, 0, 0, 0);
            dataGridView1.GridColor = cGridLine;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.BackgroundColor = cPanel;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = cGridRowAlt;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.ShowCellToolTips = false;
            panelGrid.Controls.Add(dataGridView1);

            // ====== ALERT CONFIG PANEL ======
            panelAlertConfig.Dock = DockStyle.Top;
            panelAlertConfig.Height = 52;
            panelAlertConfig.BackColor = cPanelAlt;
            panelAlertConfig.Padding = new Padding(16, 0, 16, 0);
            panelAlertConfig.Paint += (s, e) =>
            {
                var rect = panelAlertConfig.ClientRectangle;
                using var pen = new Pen(cSeparator, 1);
                e.Graphics.DrawLine(pen, rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
            };

            lblThresholdLabel.Text = "Порог тревоги";
            lblThresholdLabel.Font = new Font("Segoe UI", 9F);
            lblThresholdLabel.ForeColor = cTextDim;
            lblThresholdLabel.Location = new Point(16, 15);
            lblThresholdLabel.AutoSize = true;

            numThreshold.Location = new Point(130, 13);
            numThreshold.Size = new Size(60, 26);
            numThreshold.Minimum = 30;
            numThreshold.Maximum = 120;
            numThreshold.Value = 80;
            numThreshold.BackColor = cInput;
            numThreshold.ForeColor = cText;
            numThreshold.BorderStyle = BorderStyle.FixedSingle;
            numThreshold.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            numThreshold.TextAlign = HorizontalAlignment.Center;

            btnApplyThreshold.Text = "Применить";
            btnApplyThreshold.Location = new Point(200, 12);
            btnApplyThreshold.Size = new Size(110, 28);
            btnApplyThreshold.FlatStyle = FlatStyle.Flat;
            btnApplyThreshold.BackColor = cAccent;
            btnApplyThreshold.ForeColor = Color.White;
            btnApplyThreshold.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnApplyThreshold.FlatAppearance.BorderSize = 0;
            btnApplyThreshold.Cursor = Cursors.Hand;
            btnApplyThreshold.Click += btnApplyThreshold_Click;
            btnApplyThreshold.MouseEnter += (s, e) => btnApplyThreshold.BackColor = cAccentHover;
            btnApplyThreshold.MouseLeave += (s, e) => btnApplyThreshold.BackColor = cAccent;

            chkAutoStart.Text = "Автозапуск с Windows";
            chkAutoStart.Location = new Point(350, 15);
            chkAutoStart.AutoSize = true;
            chkAutoStart.ForeColor = cTextDim;
            chkAutoStart.Font = new Font("Segoe UI", 9F);
            chkAutoStart.BackColor = Color.Transparent;
            chkAutoStart.CheckedChanged += chkAutoStart_CheckedChanged;

            chkStartMinimized.Text = "Сворачивать при старте";
            chkStartMinimized.Location = new Point(530, 15);
            chkStartMinimized.AutoSize = true;
            chkStartMinimized.ForeColor = cTextDim;
            chkStartMinimized.Font = new Font("Segoe UI", 9F);
            chkStartMinimized.BackColor = Color.Transparent;
            chkStartMinimized.CheckedChanged += chkStartMinimized_CheckedChanged;

            panelAlertConfig.Controls.AddRange(new Control[] {
                lblThresholdLabel, numThreshold, btnApplyThreshold, chkAutoStart, chkStartMinimized
            });

            // ====== SOUND CONFIG PANEL ======
            panelSoundConfig.Dock = DockStyle.Top;
            panelSoundConfig.Height = 90;
            panelSoundConfig.BackColor = cPanel;
            panelSoundConfig.Padding = new Padding(16, 0, 16, 0);
            panelSoundConfig.Paint += (s, e) =>
            {
                var rect = panelSoundConfig.ClientRectangle;
                using var pen = new Pen(cSeparator, 1);
                e.Graphics.DrawLine(pen, rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
            };

            lblSoundLabel.Text = "Звук оповещения";
            lblSoundLabel.Font = new Font("Segoe UI", 9F);
            lblSoundLabel.ForeColor = cTextDim;
            lblSoundLabel.Location = new Point(16, 14);
            lblSoundLabel.AutoSize = true;

            txtSoundPath.Location = new Point(16, 36);
            txtSoundPath.Size = new Size(380, 26);
            txtSoundPath.ReadOnly = true;
            txtSoundPath.BackColor = cInput;
            txtSoundPath.ForeColor = cTextDim;
            txtSoundPath.BorderStyle = BorderStyle.FixedSingle;
            txtSoundPath.Font = new Font("Segoe UI", 9F);

            btnBrowseSound.Text = "Обзор...";
            btnBrowseSound.Location = new Point(405, 35);
            btnBrowseSound.Size = new Size(80, 28);
            btnBrowseSound.FlatStyle = FlatStyle.Flat;
            btnBrowseSound.BackColor = cBtnSecondary;
            btnBrowseSound.ForeColor = cText;
            btnBrowseSound.Font = new Font("Segoe UI", 9F);
            btnBrowseSound.FlatAppearance.BorderSize = 0;
            btnBrowseSound.Cursor = Cursors.Hand;
            btnBrowseSound.Click += btnBrowseSound_Click;
            btnBrowseSound.MouseEnter += (s, e) => btnBrowseSound.BackColor = cBtnSecondaryHover;
            btnBrowseSound.MouseLeave += (s, e) => btnBrowseSound.BackColor = cBtnSecondary;

            btnTestSound.Text = "▶ Тест";
            btnTestSound.Location = new Point(495, 35);
            btnTestSound.Size = new Size(75, 28);
            btnTestSound.FlatStyle = FlatStyle.Flat;
            btnTestSound.BackColor = cAccent;
            btnTestSound.ForeColor = Color.White;
            btnTestSound.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnTestSound.FlatAppearance.BorderSize = 0;
            btnTestSound.Cursor = Cursors.Hand;
            btnTestSound.Click += btnTestSound_Click;
            btnTestSound.MouseEnter += (s, e) => btnTestSound.BackColor = cAccentHover;
            btnTestSound.MouseLeave += (s, e) => btnTestSound.BackColor = cAccent;

            btnStopSound.Text = "■ Стоп";
            btnStopSound.Location = new Point(578, 35);
            btnStopSound.Size = new Size(75, 28);
            btnStopSound.FlatStyle = FlatStyle.Flat;
            btnStopSound.BackColor = cBtnSecondary;
            btnStopSound.ForeColor = cText;
            btnStopSound.Font = new Font("Segoe UI", 9F);
            btnStopSound.FlatAppearance.BorderSize = 0;
            btnStopSound.Cursor = Cursors.Hand;
            btnStopSound.Click += btnStopSound_Click;
            btnStopSound.MouseEnter += (s, e) => btnStopSound.BackColor = cBtnSecondaryHover;
            btnStopSound.MouseLeave += (s, e) => btnStopSound.BackColor = cBtnSecondary;

            lblVolumeLabel.Text = "Громкость";
            lblVolumeLabel.Font = new Font("Segoe UI", 9F);
            lblVolumeLabel.ForeColor = cTextDim;
            lblVolumeLabel.Location = new Point(16, 70);
            lblVolumeLabel.AutoSize = true;

            trackBarVolume.Location = new Point(90, 64);
            trackBarVolume.Size = new Size(140, 30);
            trackBarVolume.Minimum = 0;
            trackBarVolume.Maximum = 100;
            trackBarVolume.Value = 80;
            trackBarVolume.TickFrequency = 25;
            trackBarVolume.BackColor = cPanel;
            trackBarVolume.Scroll += trackBarVolume_Scroll;

            lblVolumePercent.Text = "80%";
            lblVolumePercent.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVolumePercent.ForeColor = cAccent;
            lblVolumePercent.Location = new Point(235, 70);
            lblVolumePercent.AutoSize = true;

            panelSoundConfig.Controls.AddRange(new Control[] {
                lblSoundLabel, txtSoundPath, btnBrowseSound,
                btnTestSound, btnStopSound,
                lblVolumeLabel, trackBarVolume, lblVolumePercent
            });

            // ====== HOTKEY PANEL ======
            panelHotkey.Dock = DockStyle.Top;
            panelHotkey.Height = 60;
            panelHotkey.BackColor = cPanelAlt;
            panelHotkey.Padding = new Padding(16, 0, 16, 0);
            panelHotkey.Paint += (s, e) =>
            {
                var rect = panelHotkey.ClientRectangle;
                using var pen = new Pen(cSeparator, 1);
                e.Graphics.DrawLine(pen, rect.Left, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
            };

            lblHotkeyLabel.Text = "Горячая клавиша (вкл/выкл)";
            lblHotkeyLabel.Font = new Font("Segoe UI", 9F);
            lblHotkeyLabel.ForeColor = cTextDim;
            lblHotkeyLabel.Location = new Point(16, 18);
            lblHotkeyLabel.AutoSize = true;

            btnCaptureHotkey.Text = "Записать";
            btnCaptureHotkey.Location = new Point(210, 15);
            btnCaptureHotkey.Size = new Size(110, 28);
            btnCaptureHotkey.FlatStyle = FlatStyle.Flat;
            btnCaptureHotkey.BackColor = cAccent;
            btnCaptureHotkey.ForeColor = Color.White;
            btnCaptureHotkey.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCaptureHotkey.FlatAppearance.BorderSize = 0;
            btnCaptureHotkey.Cursor = Cursors.Hand;
            btnCaptureHotkey.Click += btnCaptureHotkey_Click;
            btnCaptureHotkey.MouseEnter += (s, e) => { if (!_isCapturingHotkey) btnCaptureHotkey.BackColor = cAccentHover; };
            btnCaptureHotkey.MouseLeave += (s, e) => { if (!_isCapturingHotkey) btnCaptureHotkey.BackColor = cAccent; };

            btnClearHotkey.Text = "Сброс";
            btnClearHotkey.Location = new Point(328, 15);
            btnClearHotkey.Size = new Size(70, 28);
            btnClearHotkey.FlatStyle = FlatStyle.Flat;
            btnClearHotkey.BackColor = cBtnSecondary;
            btnClearHotkey.ForeColor = cText;
            btnClearHotkey.Font = new Font("Segoe UI", 9F);
            btnClearHotkey.FlatAppearance.BorderSize = 0;
            btnClearHotkey.Cursor = Cursors.Hand;
            btnClearHotkey.Click += btnClearHotkey_Click;
            btnClearHotkey.MouseEnter += (s, e) => btnClearHotkey.BackColor = cBtnSecondaryHover;
            btnClearHotkey.MouseLeave += (s, e) => btnClearHotkey.BackColor = cBtnSecondary;

            txtHotkeyDisplay.Location = new Point(415, 15);
            txtHotkeyDisplay.Size = new Size(150, 28);
            txtHotkeyDisplay.ReadOnly = true;
            txtHotkeyDisplay.BackColor = cInput;
            txtHotkeyDisplay.ForeColor = cAccent;
            txtHotkeyDisplay.BorderStyle = BorderStyle.FixedSingle;
            txtHotkeyDisplay.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            txtHotkeyDisplay.TextAlign = HorizontalAlignment.Center;
            txtHotkeyDisplay.Text = "Ctrl + F8";

            panelHotkey.Controls.AddRange(new Control[] {
                lblHotkeyLabel, btnCaptureHotkey, btnClearHotkey, txtHotkeyDisplay
            });

            // ====== STATUS BAR ======
            panelStatusBar.Dock = DockStyle.Bottom;
            panelStatusBar.Height = 36;
            panelStatusBar.BackColor = cPanel;
            panelStatusBar.Padding = new Padding(20, 0, 20, 0);
            panelStatusBar.Paint += (s, e) =>
            {
                var rect = panelStatusBar.ClientRectangle;
                using var pen = new Pen(cSeparator, 1);
                e.Graphics.DrawLine(pen, rect.Left, 0, rect.Right, 0);
            };

            lblIndicator.Text = "● АКТИВЕН";
            lblIndicator.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblIndicator.ForeColor = cSuccess;
            lblIndicator.Dock = DockStyle.Left;
            lblIndicator.TextAlign = ContentAlignment.MiddleLeft;
            lblIndicator.AutoSize = false;
            lblIndicator.Width = 120;
            panelStatusBar.Controls.Add(lblIndicator);

            // Status animation timer
            _statusAnimTimer.Interval = 80;
            _statusAnimTimer.Tick += StatusAnimTimer_Tick;
            _statusAnimTimer.Start();

            // ====== LAYOUT ASSEMBLY ======
            panelContent.Controls.Add(panelGrid);
            panelContent.Controls.Add(panelAlertConfig);
            panelContent.Controls.Add(panelSoundConfig);
            panelContent.Controls.Add(panelHotkey);

            Controls.Add(panelContent);
            Controls.Add(panelStatusBar);
            Controls.Add(panelHeader);

            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numThreshold).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconBox).EndInit();
            ResumeLayout(false);
        }
    }
}