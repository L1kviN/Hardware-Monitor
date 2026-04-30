using System;
using System.Drawing;
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
    private Label lblThreshold;
    private Label lblSound;
    private Label lblVolume;
    private Label lblIndicator;
    private Label lblHotkey;
    private TrackBar trackBarVolume;
    private GroupBox groupBoxMonitor;
    private GroupBox groupBoxSettings;
    private GroupBox groupBoxHotkey;
    private Panel panelStatus;
    private Panel panelTitle;
    private TextBox txtHotkeyDisplay;
    private Label lblTitle;
    private Label lblVolumePercent;
    private CheckBox chkStartMinimized;

    private void InitializeComponent()
    {
      dataGridView1 = new DataGridView();
      groupBoxMonitor = new GroupBox();
      groupBoxSettings = new GroupBox();
      lblThreshold = new Label();
      numThreshold = new NumericUpDown();
      btnApplyThreshold = new Button();
      lblSound = new Label();
      txtSoundPath = new TextBox();
      btnBrowseSound = new Button();
      btnTestSound = new Button();
      btnStopSound = new Button();
      lblVolume = new Label();
      trackBarVolume = new TrackBar();
      lblVolumePercent = new Label();
      chkAutoStart = new CheckBox();
      groupBoxHotkey = new GroupBox();
      lblHotkey = new Label();
      btnCaptureHotkey = new Button();
      btnClearHotkey = new Button();
      txtHotkeyDisplay = new TextBox();
      panelStatus = new Panel();
      lblIndicator = new Label();
      panelTitle = new Panel();
      lblTitle = new Label();

      ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
      groupBoxMonitor.SuspendLayout();
      groupBoxSettings.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)numThreshold).BeginInit();
      ((System.ComponentModel.ISupportInitialize)trackBarVolume).BeginInit();
      groupBoxHotkey.SuspendLayout();
      panelStatus.SuspendLayout();
      panelTitle.SuspendLayout();
      SuspendLayout();

      // ====== ЦВЕТОВАЯ ПАЛИТРА (Banking Dark) ======
      Color bgMain = Color.FromArgb(30, 32, 38);        // основной фон
      Color bgPanel = Color.FromArgb(38, 40, 48);       // фон панелей
      Color bgInput = Color.FromArgb(50, 52, 60);       // фон полей ввода
      Color bgGrid = Color.FromArgb(38, 40, 48);        // фон таблицы
      Color accentPrimary = Color.FromArgb(70, 100, 180); // тёмно-синий акцент
      Color accentHover = Color.FromArgb(90, 120, 200);   // при наведении
      Color accentText = Color.FromArgb(180, 190, 210);   // светло-серый текст
      Color textWhite = Color.FromArgb(235, 238, 245);    // белый текст
      Color textLabel = Color.FromArgb(160, 168, 180);    // серый для лейблов
      Color gridHeader = Color.FromArgb(48, 50, 58);      // шапка таблицы
      Color gridLine = Color.FromArgb(55, 58, 66);        // линии таблицы
      Color statusGreen = Color.FromArgb(80, 180, 130);   // зелёный индикатор
      Color statusYellow = Color.FromArgb(200, 170, 80);  // жёлтый индикатор
      Color btnSecondary = Color.FromArgb(55, 58, 68);    // второстепенная кнопка
      Color btnSecondaryHover = Color.FromArgb(70, 73, 83);
      Color rowWarning = Color.FromArgb(65, 55, 25);
      Color rowDanger = Color.FromArgb(70, 30, 30);

      // ====== PANEL TITLE ======
      panelTitle.Dock = DockStyle.Top;
      panelTitle.Height = 50;
      panelTitle.BackColor = bgPanel;
      panelTitle.Padding = new Padding(0);
      panelTitle.Controls.Add(lblTitle);

      lblTitle.Text = "HARDWARE MONITOR";
      lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Regular);
      lblTitle.ForeColor = textWhite;
      lblTitle.Dock = DockStyle.Fill;
      lblTitle.TextAlign = ContentAlignment.MiddleLeft;
      lblTitle.Padding = new Padding(20, 0, 0, 0);

      // ====== DATAGRIDVIEW ======
      dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridView1.ColumnHeadersHeight = 34;
      dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
      dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = textLabel;
      dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = gridHeader;
      dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(12, 0, 0, 0);
      dataGridView1.Columns.AddRange(new DataGridViewColumn[]
      {
                new DataGridViewTextBoxColumn { Name = "Hardware", HeaderText = "Устройство", Width = 170 },
                new DataGridViewTextBoxColumn { Name = "Sensor", HeaderText = "Датчик", Width = 160 },
                new DataGridViewTextBoxColumn { Name = "Temperature", HeaderText = "Температура", Width = 120 },
                new DataGridViewTextBoxColumn { Name = "Max", HeaderText = "Максимум", Width = 120 }
      });
      dataGridView1.Dock = DockStyle.Fill;
      dataGridView1.ReadOnly = true;
      dataGridView1.AllowUserToAddRows = false;
      dataGridView1.AllowUserToDeleteRows = false;
      dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      dataGridView1.RowHeadersVisible = false;
      dataGridView1.BorderStyle = BorderStyle.None;
      dataGridView1.RowTemplate.Height = 32;
      dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
      dataGridView1.DefaultCellStyle.ForeColor = textWhite;
      dataGridView1.DefaultCellStyle.BackColor = bgGrid;
      dataGridView1.DefaultCellStyle.SelectionBackColor = accentPrimary;
      dataGridView1.DefaultCellStyle.SelectionForeColor = textWhite;
      dataGridView1.DefaultCellStyle.Padding = new Padding(12, 0, 0, 0);
      dataGridView1.GridColor = gridLine;
      dataGridView1.EnableHeadersVisualStyles = false;
      dataGridView1.BackgroundColor = bgGrid;
      dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

      // ====== GROUPS ======
      foreach (var gb in new[] { groupBoxMonitor, groupBoxSettings, groupBoxHotkey })
      {
        gb.BackColor = bgPanel;
        gb.ForeColor = textLabel;
        gb.Font = new Font("Segoe UI", 9, FontStyle.Regular);
        gb.Padding = new Padding(16, 20, 16, 12);
      }

      groupBoxMonitor.Text = "Мониторинг";
      groupBoxMonitor.Dock = DockStyle.Top;
      groupBoxMonitor.Height = 200;
      groupBoxMonitor.Controls.Add(dataGridView1);

      groupBoxSettings.Text = "Настройки";
      groupBoxSettings.Dock = DockStyle.Top;
      groupBoxSettings.Height = 175;

      groupBoxHotkey.Text = "Управление";
      groupBoxHotkey.Dock = DockStyle.Top;
      groupBoxHotkey.Height = 65;

      // ====== SETTINGS CONTROLS ======
      // Threshold
      lblThreshold.Text = "Порог температуры";
      lblThreshold.ForeColor = textLabel;
      lblThreshold.Font = new Font("Segoe UI", 9);
      lblThreshold.Location = new Point(18, 32);
      lblThreshold.AutoSize = true;

      numThreshold.Location = new Point(175, 30);
      numThreshold.Size = new Size(62, 26);
      numThreshold.Minimum = 30;
      numThreshold.Maximum = 120;
      numThreshold.Value = 80;
      numThreshold.BackColor = bgInput;
      numThreshold.ForeColor = textWhite;
      numThreshold.BorderStyle = BorderStyle.None;
      numThreshold.Font = new Font("Segoe UI", 10);
      numThreshold.TextAlign = HorizontalAlignment.Center;

      btnApplyThreshold.Text = "Применить";
      btnApplyThreshold.Location = new Point(248, 29);
      btnApplyThreshold.Size = new Size(110, 28);
      btnApplyThreshold.FlatStyle = FlatStyle.Flat;
      btnApplyThreshold.BackColor = accentPrimary;
      btnApplyThreshold.ForeColor = textWhite;
      btnApplyThreshold.Font = new Font("Segoe UI", 9);
      btnApplyThreshold.FlatAppearance.BorderSize = 0;
      btnApplyThreshold.Cursor = Cursors.Hand;
      btnApplyThreshold.Click += btnApplyThreshold_Click;

      // Sound file
      lblSound.Text = "Звуковой файл";
      lblSound.ForeColor = textLabel;
      lblSound.Font = new Font("Segoe UI", 9);
      lblSound.Location = new Point(18, 74);
      lblSound.AutoSize = true;

      txtSoundPath.Location = new Point(175, 72);
      txtSoundPath.Size = new Size(270, 26);
      txtSoundPath.ReadOnly = true;
      txtSoundPath.BackColor = bgInput;
      txtSoundPath.ForeColor = accentText;
      txtSoundPath.BorderStyle = BorderStyle.None;
      txtSoundPath.Font = new Font("Segoe UI", 9);

      btnBrowseSound.Text = "Обзор";
      btnBrowseSound.Location = new Point(455, 71);
      btnBrowseSound.Size = new Size(80, 28);
      btnBrowseSound.FlatStyle = FlatStyle.Flat;
      btnBrowseSound.BackColor = btnSecondary;
      btnBrowseSound.ForeColor = textWhite;
      btnBrowseSound.Font = new Font("Segoe UI", 9);
      btnBrowseSound.FlatAppearance.BorderSize = 0;
      btnBrowseSound.Cursor = Cursors.Hand;
      btnBrowseSound.Click += btnBrowseSound_Click;

      // Test / Stop buttons
      btnTestSound.Text = "▶ Тест";
      btnTestSound.Location = new Point(175, 112);
      btnTestSound.Size = new Size(85, 28);
      btnTestSound.FlatStyle = FlatStyle.Flat;
      btnTestSound.BackColor = accentPrimary;
      btnTestSound.ForeColor = textWhite;
      btnTestSound.Font = new Font("Segoe UI", 9);
      btnTestSound.FlatAppearance.BorderSize = 0;
      btnTestSound.Cursor = Cursors.Hand;
      btnTestSound.Click += btnTestSound_Click;

      btnStopSound.Text = "■ Стоп";
      btnStopSound.Location = new Point(270, 112);
      btnStopSound.Size = new Size(85, 28);
      btnStopSound.FlatStyle = FlatStyle.Flat;
      btnStopSound.BackColor = btnSecondary;
      btnStopSound.ForeColor = textWhite;
      btnStopSound.Font = new Font("Segoe UI", 9);
      btnStopSound.FlatAppearance.BorderSize = 0;
      btnStopSound.Cursor = Cursors.Hand;
      btnStopSound.Click += btnStopSound_Click;

      // Volume
      lblVolume.Text = "Громкость";
      lblVolume.ForeColor = textLabel;
      lblVolume.Font = new Font("Segoe UI", 9);
      lblVolume.Location = new Point(400, 108);
      lblVolume.AutoSize = true;

      trackBarVolume.Location = new Point(465, 108);
      trackBarVolume.Size = new Size(120, 36);
      trackBarVolume.Minimum = 0;
      trackBarVolume.Maximum = 100;
      trackBarVolume.Value = 80;
      trackBarVolume.TickFrequency = 25;
      trackBarVolume.BackColor = bgPanel;
      trackBarVolume.Scroll += trackBarVolume_Scroll;

      // Auto-start
      chkAutoStart.Text = "Автозапуск с Windows";
      chkAutoStart.Location = new Point(400, 25);
      chkAutoStart.AutoSize = true;
      chkAutoStart.ForeColor = textLabel;
      chkAutoStart.Font = new Font("Segoe UI", 9);
      chkAutoStart.BackColor = bgPanel;
      chkAutoStart.CheckedChanged += chkAutoStart_CheckedChanged;
     
      // chkStartMinimized
      chkStartMinimized = new CheckBox();
      chkStartMinimized.Text = "Запускать свёрнуто";
      chkStartMinimized.Location = new Point(400, 45);
      chkStartMinimized.AutoSize = true;
      chkStartMinimized.ForeColor = textLabel;
      chkStartMinimized.Font = new Font("Segoe UI", 9);
      chkStartMinimized.BackColor = bgPanel;
      chkStartMinimized.CheckedChanged += chkStartMinimized_CheckedChanged;
      groupBoxSettings.Controls.Add(chkStartMinimized);
      
      // Add controls to groupBoxSettings
      groupBoxSettings.Controls.AddRange(new Control[]
      {
                lblThreshold, numThreshold, btnApplyThreshold,
                lblSound, txtSoundPath, btnBrowseSound,
                btnTestSound, btnStopSound,
                lblVolume, trackBarVolume,
                chkAutoStart
      });

      // ====== HOTKEY CONTROLS ======
      lblHotkey.Text = "Вкл / Выкл мониторинг";
      lblHotkey.ForeColor = textLabel;
      lblHotkey.Font = new Font("Segoe UI", 9);
      lblHotkey.Location = new Point(18, 28);
      lblHotkey.AutoSize = true;

      btnCaptureHotkey.Text = "Записать клавишу";
      btnCaptureHotkey.Location = new Point(200, 24);
      btnCaptureHotkey.Size = new Size(140, 28);
      btnCaptureHotkey.FlatStyle = FlatStyle.Flat;
      btnCaptureHotkey.BackColor = accentPrimary;
      btnCaptureHotkey.ForeColor = textWhite;
      btnCaptureHotkey.Font = new Font("Segoe UI", 9);
      btnCaptureHotkey.FlatAppearance.BorderSize = 0;
      btnCaptureHotkey.Cursor = Cursors.Hand;
      btnCaptureHotkey.Click += btnCaptureHotkey_Click;

      btnClearHotkey.Text = "Сброс";
      btnClearHotkey.Location = new Point(350, 24);
      btnClearHotkey.Size = new Size(70, 28);
      btnClearHotkey.FlatStyle = FlatStyle.Flat;
      btnClearHotkey.BackColor = btnSecondary;
      btnClearHotkey.ForeColor = textWhite;
      btnClearHotkey.Font = new Font("Segoe UI", 9);
      btnClearHotkey.FlatAppearance.BorderSize = 0;
      btnClearHotkey.Cursor = Cursors.Hand;
      btnClearHotkey.Click += btnClearHotkey_Click;

      txtHotkeyDisplay.Location = new Point(435, 25);
      txtHotkeyDisplay.Size = new Size(130, 26);
      txtHotkeyDisplay.ReadOnly = true;
      txtHotkeyDisplay.BackColor = bgInput;
      txtHotkeyDisplay.ForeColor = accentText;
      txtHotkeyDisplay.BorderStyle = BorderStyle.None;
      txtHotkeyDisplay.Font = new Font("Segoe UI", 10, FontStyle.Bold);
      txtHotkeyDisplay.TextAlign = HorizontalAlignment.Center;
      txtHotkeyDisplay.Text = "Ctrl + F8";

      groupBoxHotkey.Controls.AddRange(new Control[]
      {
                lblHotkey, btnCaptureHotkey, btnClearHotkey, txtHotkeyDisplay
      });

      // ====== STATUS PANEL ======
      panelStatus.Dock = DockStyle.Bottom;
      panelStatus.Height = 36;
      panelStatus.BackColor = bgPanel;
      panelStatus.Controls.Add(lblIndicator);

      lblIndicator.Text = "● Активен";
      lblIndicator.Font = new Font("Segoe UI", 10);
      lblIndicator.ForeColor = statusGreen;
      lblIndicator.Dock = DockStyle.Fill;
      lblIndicator.TextAlign = ContentAlignment.MiddleLeft;
      lblIndicator.Padding = new Padding(20, 0, 0, 0);

      // ====== MAIN FORM ======
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(620, 510);
      BackColor = bgMain;
      Controls.Add(panelStatus);
      Controls.Add(groupBoxHotkey);
      Controls.Add(groupBoxSettings);
      Controls.Add(groupBoxMonitor);
      Controls.Add(panelTitle);
      Text = "Hardware Monitor";
      StartPosition = FormStartPosition.CenterScreen;
      FormBorderStyle = FormBorderStyle.FixedSingle;
      MaximizeBox = false;
      KeyPreview = true;
      Font = new Font("Segoe UI", 9);

      ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
      groupBoxMonitor.ResumeLayout(false);
      groupBoxSettings.ResumeLayout(false);
      groupBoxSettings.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)numThreshold).EndInit();
      ((System.ComponentModel.ISupportInitialize)trackBarVolume).EndInit();
      groupBoxHotkey.ResumeLayout(false);
      groupBoxHotkey.PerformLayout();
      panelStatus.ResumeLayout(false);
      panelTitle.ResumeLayout(false);
      ResumeLayout(false);
    }
  }
}