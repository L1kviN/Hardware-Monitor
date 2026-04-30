using NAudio.Wave;

namespace Hardware_Monitor_dla_deda.Services;

public class SoundService : IDisposable
{
    private WaveOutEvent? _outputDevice;
    private AudioFileReader? _audioFileReader;
    private LoopStream? _loopStream;
    private string? _currentFile;
    private bool _isLooping;
    private bool _disposed;
    private float _volume = 1.0f;

    public bool IsPlaying => _outputDevice?.PlaybackState == PlaybackState.Playing;

    public float Volume
    {
        get => _volume;
        set
        {
            _volume = Math.Clamp(value, 0f, 1f);
            if (_audioFileReader != null)
                _audioFileReader.Volume = _volume;
        }
    }

    public void Play(string filePath, bool loop = true)
    {
        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            return;

        if (IsPlaying && _currentFile == filePath)
            return;

        Stop();

        try
        {
            _currentFile = filePath;
            _isLooping = loop;

            _audioFileReader = new AudioFileReader(filePath);
            _audioFileReader.Volume = _volume;

            if (_isLooping)
            {
                _loopStream = new LoopStream(_audioFileReader);
                _outputDevice = new WaveOutEvent();
                _outputDevice.Init(_loopStream);
            }
            else
            {
                _outputDevice = new WaveOutEvent();
                _outputDevice.Init(_audioFileReader);
            }

            _outputDevice.Play();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"SoundService error: {ex.Message}");
        }
    }

    public void Stop()
    {
        _outputDevice?.Stop();
        _outputDevice?.Dispose();
        _outputDevice = null;

        _loopStream?.Dispose();
        _loopStream = null;

        _audioFileReader?.Dispose();
        _audioFileReader = null;

        _currentFile = null;
    }

    public void Dispose()
    {
        if (_disposed) return;
        Stop();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}

public class LoopStream : WaveStream
{
    private readonly WaveStream _sourceStream;

    public LoopStream(WaveStream source)
    {
        _sourceStream = source;
        _sourceStream.Position = 0;
    }

    public override WaveFormat WaveFormat => _sourceStream.WaveFormat;
    public override long Length => _sourceStream.Length;
    public override long Position
    {
        get => _sourceStream.Position;
        set => _sourceStream.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int totalBytesRead = 0;
        while (totalBytesRead < count)
        {
            int bytesRead = _sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
            if (bytesRead == 0)
            {
                _sourceStream.Position = 0;
            }
            totalBytesRead += bytesRead;
        }
        return totalBytesRead;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _sourceStream?.Dispose();
        }
        base.Dispose(disposing);
    }
}