using System.Runtime.InteropServices;

namespace Hardware_Monitor_dla_deda.Services;

public class HotkeyManager : IDisposable
{
  private const int WM_HOTKEY = 0x0312;

  [DllImport("user32.dll")]
  private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

  [DllImport("user32.dll")]
  private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

  private readonly IntPtr _handle;
  private readonly int _id;
  private bool _registered;
  private int _modifiers;
  private int _key;

  public int Modifiers => _modifiers;
  public int Key => _key;
  public bool IsRegistered => _registered;

  public HotkeyManager(IntPtr handle, int id)
  {
    _handle = handle;
    _id = id;
  }

  public void Register(int modifiers, int key)
  {
    Unregister();
    _modifiers = modifiers;
    _key = key;
    _registered = RegisterHotKey(_handle, _id, modifiers, key);
  }

  public void Unregister()
  {
    if (_registered)
    {
      UnregisterHotKey(_handle, _id);
      _registered = false;
    }
  }

  public void Dispose()
  {
    Unregister();
  }
}

/// <summary>
/// Результат захвата горячей клавиши
/// </summary>
public class HotkeyCaptureResult
{
  public Keys Key { get; set; }
  public bool Ctrl { get; set; }
  public bool Shift { get; set; }
  public bool Alt { get; set; }
  public int ModifiersValue => (Ctrl ? 2 : 0) | (Shift ? 4 : 0) | (Alt ? 1 : 0);
  public int KeyValue => (int)Key;

  public override string ToString()
  {
    var parts = new List<string>();
    if (Ctrl) parts.Add("Ctrl");
    if (Shift) parts.Add("Shift");
    if (Alt) parts.Add("Alt");
    parts.Add(Key.ToString());
    return string.Join(" + ", parts);
  }
}