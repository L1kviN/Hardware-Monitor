using Hardware_Monitor_dla_deda.Models;
using Hardware_Monitor_dla_deda.Services;
using HardwareMonitor.Models;
using HardwareMonitor.Services;
using System.Runtime.InteropServices;

namespace Hardware_Monitor_dla_deda;

public partial class MainForm : Form
{
    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

    private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
    private const int DWMWA_CAPTION_COLOR = 35;
    private readonly HardwareMonitorService _monitorService;
    private readonly SoundService _soundService;
    private AppSettings _settings;
    private readonly System.Windows.Forms.Timer _pollingTimer;
    private readonly NotifyIcon _notifyIcon;
    private readonly HotkeyManager _hotkeyManager;
    private bool _isAlertActive;
    private bool _isMonitoringActive = true;
    private readonly bool _startMinimized;
    private bool _isCapturingHotkey;

    private const int WM_HOTKEY = 0x0312;
    private const int HOTKEY_ID = 9001;

    public MainForm(bool startMinimized = false)
    {
        InitializeComponent();
        ApplyDarkTheme();
        _startMinimized = startMinimized;
        _monitorService = new HardwareMonitorService();
        _soundService = new SoundService();
        _settings = AppSettings.Load();
        _hotkeyManager = new HotkeyManager(Handle, HOTKEY_ID);

        _pollingTimer = new System.Windows.Forms.Timer
        {
            Interval = _settings.PollingIntervalMs
        };
        _pollingTimer.Tick += OnPollingTimerTick;

        _notifyIcon = new NotifyIcon();
        SetupNotifyIcon();

        _monitorService.OnTemperaturesUpdated += OnTemperaturesUpdated;
        _monitorService.OnError += OnMonitorError;

        Load += MainForm_Load;
        FormClosing += MainForm_FormClosing;
        KeyDown += MainForm_KeyDown;
        KeyUp += MainForm_KeyUp;
    }

    private void ApplyDarkTheme()
    {
        Color bgMain = Color.FromArgb(30, 32, 38);
        Color bgPanel = Color.FromArgb(38, 40, 48);
        Color bgInput = Color.FromArgb(50, 52, 60);
        Color bgGrid = Color.FromArgb(38, 40, 48);
        Color accentPrimary = Color.FromArgb(70, 100, 180);
        Color accentHover = Color.FromArgb(90, 120, 200);
        Color textWhite = Color.FromArgb(235, 238, 245);
        Color textLabel = Color.FromArgb(160, 168, 180);
        Color gridHeader = Color.FromArgb(48, 50, 58);
        Color gridLine = Color.FromArgb(55, 58, 66);
        Color btnSecondary = Color.FromArgb(55, 58, 68);
        Color btnSecondaryHover = Color.FromArgb(70, 73, 83);

        BackColor = bgMain;
        ForeColor = textWhite;

        dataGridView1.BackgroundColor = bgGrid;
        dataGridView1.DefaultCellStyle.BackColor = bgGrid;
        dataGridView1.DefaultCellStyle.ForeColor = textWhite;
        dataGridView1.DefaultCellStyle.SelectionBackColor = accentPrimary;
        dataGridView1.DefaultCellStyle.SelectionForeColor = textWhite;
        dataGridView1.GridColor = gridLine;
        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = gridHeader;
        dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = textLabel;
        dataGridView1.EnableHeadersVisualStyles = false;

        StyleButton(btnBrowseSound, btnSecondary, btnSecondaryHover, textWhite);
        StyleButton(btnTestSound, accentPrimary, accentHover, textWhite);
        StyleButton(btnStopSound, btnSecondary, btnSecondaryHover, textWhite);
        StyleButton(btnApplyThreshold, accentPrimary, accentHover, textWhite);
        StyleButton(btnCaptureHotkey, accentPrimary, accentHover, textWhite);
        StyleButton(btnClearHotkey, btnSecondary, btnSecondaryHover, textWhite);
    }

    private void StyleButton(Button btn, Color back, Color hover, Color fore)
    {
        btn.BackColor = back;
        btn.ForeColor = fore;
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.MouseEnter += (s, e) => { btn.BackColor = hover; };
        btn.MouseLeave += (s, e) => { btn.BackColor = back; };
    }

  

    private void SetupNotifyIcon()
    {
        _notifyIcon.Text = "Hardware Monitor";
        _notifyIcon.Icon = SystemIcons.Information;
        _notifyIcon.Visible = true;

        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Развернуть окно", null, (s, e) => ShowMainWindow());
        contextMenu.Items.Add("-");
        contextMenu.Items.Add("Выход", null, (s, e) =>
        {
            _hotkeyManager.Dispose();
            Application.Exit();
        });
        _notifyIcon.ContextMenuStrip = contextMenu;
        _notifyIcon.DoubleClick += (s, e) => ShowMainWindow();
    }

    private void ShowMainWindow()
    {
        Show();
        WindowState = FormWindowState.Normal;
        Activate();
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        numThreshold.Value = (decimal)(_settings.AlertRules.FirstOrDefault()?.Threshold ?? 80);
        chkAutoStart.Checked = _settings.AutoStartWithWindows;
        txtSoundPath.Text = _settings.SoundFilePath ?? "";
        trackBarVolume.Value = (int)(_settings.Volume * 100);
        _soundService.Volume = _settings.Volume;
        chkStartMinimized.Checked = _settings.StartMinimized;

        // Загружаем сохранённый хоткей или ставим дефолтный
        if (_settings.HotkeyKey > 0)
        {
            _hotkeyManager.Register(_settings.HotkeyModifiers, _settings.HotkeyKey);
            txtHotkeyDisplay.Text = _settings.HotkeyDisplayString ?? "Не задан";
        }
        else
        {
            // Дефолт: Ctrl+F8
            _hotkeyManager.Register(2, (int)Keys.F8);
            txtHotkeyDisplay.Text = "Ctrl + F8";
            _settings.HotkeyModifiers = 2;
            _settings.HotkeyKey = (int)Keys.F8;
            _settings.HotkeyDisplayString = "Ctrl + F8";
        }

        UpdateIndicator(true);

        _monitorService.Start();
        _pollingTimer.Start();

        if (_startMinimized)
        {
            WindowState = FormWindowState.Minimized;
            Hide();
        }
        // Перекрашиваем заголовок окна в тёмный
        if (Environment.OSVersion.Version.Build >= 22000) // Windows 11
        {
            int useDarkMode = 1;
            DwmSetWindowAttribute(Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref useDarkMode, sizeof(int));

            // Цвет заголовка: тёмно-серый как у панелей
            int captionColor = Color.FromArgb(38, 40, 48).ToArgb();
            DwmSetWindowAttribute(Handle, DWMWA_CAPTION_COLOR, ref captionColor, sizeof(int));
        }
    }

    private void chkStartMinimized_CheckedChanged(object? sender, EventArgs e)
    {
        _settings.StartMinimized = chkStartMinimized.Checked;
        _settings.Save();
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);
        if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HOTKEY_ID)
        {
            ToggleMonitoring();
        }
    }

    private bool _ctrlDown, _shiftDown, _altDown;
    private Keys _lastKeyDown;

    private void MainForm_KeyDown(object? sender, KeyEventArgs e)
    {
        if (!_isCapturingHotkey) return;

        e.SuppressKeyPress = true;
        e.Handled = true;

        _ctrlDown = e.Control;
        _shiftDown = e.Shift;
        _altDown = e.Alt;

        // Игнорируем чисто модификаторы
        if (e.KeyCode is Keys.ControlKey or Keys.ShiftKey or Keys.Menu)
            return;

        _lastKeyDown = e.KeyCode;

        var result = new HotkeyCaptureResult
        {
            Key = _lastKeyDown,
            Ctrl = _ctrlDown,
            Shift = _shiftDown,
            Alt = _altDown
        };

        ApplyCapturedHotkey(result);
    }

    private void MainForm_KeyUp(object? sender, KeyEventArgs e)
    {
        if (!_isCapturingHotkey) return;

        if (e.KeyCode is Keys.ControlKey) _ctrlDown = false;
        if (e.KeyCode is Keys.ShiftKey) _shiftDown = false;
        if (e.KeyCode is Keys.Menu) _altDown = false;
    }

    private void ApplyCapturedHotkey(HotkeyCaptureResult result)
    {
        _hotkeyManager.Register(result.ModifiersValue, result.KeyValue);
        txtHotkeyDisplay.Text = result.ToString();
        btnCaptureHotkey.Text = "ЗАПИСАТЬ ХОТКЕЙ";
        btnCaptureHotkey.BackColor = Color.FromArgb(40, 80, 160);
        _isCapturingHotkey = false;

        // Сохраняем
        _settings.HotkeyModifiers = result.ModifiersValue;
        _settings.HotkeyKey = result.KeyValue;
        _settings.HotkeyDisplayString = result.ToString();
        _settings.Save();

        UpdateIndicator(_isMonitoringActive);
    }

    private void ToggleMonitoring()
    {
        _isMonitoringActive = !_isMonitoringActive;
        UpdateIndicator(_isMonitoringActive);

        if (!_isMonitoringActive)
        {
            _soundService.Stop();
            _isAlertActive = false;
            _pollingTimer.Stop();
            _notifyIcon.Text = "Hardware Monitor - ПАУЗА";
        }
        else
        {
            _pollingTimer.Start();
            _notifyIcon.Text = "Hardware Monitor - Активен";
        }
    }

    private void UpdateIndicator(bool active)
    {
        if (active)
        {
            lblIndicator.Text = "● АКТИВЕН";
            lblIndicator.ForeColor = Color.FromArgb(0, 220, 100);
        }
        else
        {
            lblIndicator.Text = "● ПАУЗА";
            lblIndicator.ForeColor = Color.FromArgb(220, 180, 0);
        }
    }

    private void OnPollingTimerTick(object? sender, EventArgs e)
    {
        if (_isMonitoringActive)
            _monitorService.Update();
    }

    private void OnTemperaturesUpdated(List<TemperatureReading> readings)
    {
        if (IsDisposed || !_isMonitoringActive) return;

        BeginInvoke(() =>
        {
            dataGridView1.Rows.Clear();

            float highestTemp = 0;
            float threshold = _settings.AlertRules.FirstOrDefault()?.Threshold ?? 80;

            foreach (var r in readings)
            {
                var rowIndex = dataGridView1.Rows.Add(r.HardwareName, r.SensorName, $"{r.Value:F1}°C", $"{r.Max:F1}°C");
                if (r.Value > highestTemp) highestTemp = r.Value;

                if (r.Value > threshold)
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(70, 30, 30);
                else if (r.Value > threshold * 0.85f)
                    dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(65, 55, 25);
            }

            bool shouldAlert = highestTemp > threshold;

            if (shouldAlert && !_isAlertActive)
            {
                _isAlertActive = true;
                if (!string.IsNullOrEmpty(_settings.SoundFilePath) && File.Exists(_settings.SoundFilePath))
                {
                    _soundService.Play(_settings.SoundFilePath, loop: true);
                }
                _notifyIcon.Text = $"⚠️ ПЕРЕГРЕВ: {highestTemp:F1}°C";
                _notifyIcon.Icon = SystemIcons.Error;
            }
            else if (!shouldAlert && _isAlertActive)
            {
                _isAlertActive = false;
                _soundService.Stop();
                _notifyIcon.Text = "Hardware Monitor - OK";
                _notifyIcon.Icon = SystemIcons.Information;
            }
            else if (shouldAlert && _isAlertActive && !_soundService.IsPlaying)
            {
                if (!string.IsNullOrEmpty(_settings.SoundFilePath))
                {
                    _soundService.Play(_settings.SoundFilePath, loop: true);
                }
            }
        });
    }

    private void OnMonitorError(Exception ex)
    {
        BeginInvoke(() =>
        {
            MessageBox.Show(ex.Message, "Ошибка мониторинга",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        });
    }

    // === UI ===

    private void btnBrowseSound_Click(object? sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Аудиофайлы (*.wav;*.mp3)|*.wav;*.mp3|Все файлы (*.*)|*.*"
        };
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            txtSoundPath.Text = dialog.FileName;
            _settings.SoundFilePath = dialog.FileName;
            _settings.Save();
        }
    }

    private void btnTestSound_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSoundPath.Text)) return;
        _soundService.Play(txtSoundPath.Text, loop: false);
    }

    private void btnStopSound_Click(object? sender, EventArgs e)
    {
        _soundService.Stop();
        _isAlertActive = false;
    }

    private void trackBarVolume_Scroll(object? sender, EventArgs e)
    {
        float volume = trackBarVolume.Value / 100f;
        _soundService.Volume = volume;
        _settings.Volume = volume;
    }

    private void btnApplyThreshold_Click(object? sender, EventArgs e)
    {
        var rule = _settings.AlertRules.FirstOrDefault();
        if (rule == null)
        {
            rule = new AlertRule();
            _settings.AlertRules.Add(rule);
        }
        rule.Threshold = (float)numThreshold.Value;
        rule.HardwareName = "*";
        rule.SensorName = "*";
        _settings.Save();

        MessageBox.Show($"Порог установлен: {numThreshold.Value}°C", "Применено",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnCaptureHotkey_Click(object? sender, EventArgs e)
    {
        if (_isCapturingHotkey)
        {
            // Отмена захвата
            _isCapturingHotkey = false;
            btnCaptureHotkey.Text = "ЗАПИСАТЬ ХОТКЕЙ";
            btnCaptureHotkey.BackColor = Color.FromArgb(40, 80, 160);
            return;
        }

        _isCapturingHotkey = true;
        btnCaptureHotkey.Text = "НАЖМИ КЛАВИШУ...";
        btnCaptureHotkey.BackColor = Color.FromArgb(220, 140, 0);
        txtHotkeyDisplay.Text = "Ожидание...";
    }

    private void btnClearHotkey_Click(object? sender, EventArgs e)
    {
        _hotkeyManager.Unregister();
        txtHotkeyDisplay.Text = "Не задан";
        _settings.HotkeyKey = 0;
        _settings.HotkeyModifiers = 0;
        _settings.HotkeyDisplayString = "Не задан";
        _settings.Save();
        _isCapturingHotkey = false;
        btnCaptureHotkey.Text = "ЗАПИСАТЬ ХОТКЕЙ";
        btnCaptureHotkey.BackColor = Color.FromArgb(40, 80, 160);
    }

    private void chkAutoStart_CheckedChanged(object? sender, EventArgs e)
    {
        StartupManager.SetStartup(chkAutoStart.Checked);
        _settings.AutoStartWithWindows = chkAutoStart.Checked;
        _settings.Save();
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            Hide();
            _notifyIcon.ShowBalloonTip(3000, "Hardware Monitor",
                "Свёрнуто в трей. Хоткей активен.", ToolTipIcon.Info);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _pollingTimer?.Stop();
            _monitorService?.Stop();
            _soundService?.Dispose();
            _hotkeyManager?.Dispose();
            _notifyIcon?.Dispose();
            if (components != null)
                components.Dispose();
        }
        base.Dispose(disposing);
    }
}