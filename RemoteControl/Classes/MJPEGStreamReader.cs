using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteControl.Classes
{
    /// <summary>
    /// MJPEG Stream Reader class
    /// </summary>
    /// <remarks>
    /// <example>
    /// Example how to initialize <see cref="MJPEGStreamReader"/>
    /// <code>
    /// MJPEGStreamReader _streamReader;
    /// ...
    /// private void InitializeStreamReader()
    /// {
    ///     _streamReader = new MJPEGStreamReader();= new MJPEGStreamReader();
    ///     _streamReader.FrameReceived += OnFrameReceived;
    ///     _streamReader.ConnectionStatusChanged += OnConnectionStatusChanged;
    /// }
    /// </code>
    /// </example>
    /// </remarks>
    public class MJPEGStreamReader : IDisposable
    {
        private TcpClient _tcpClient;
        private NetworkStream _stream;
        private Thread _readerThread;
        private volatile bool _isRunning;
        private readonly object _lockObject = new object();

        /// <summary>
        /// Frame received event
        /// </summary>
        /// <remarks>
        /// <example>
        /// Example how to handle <see cref="FrameReceived"/> event in WinForms application,
        /// method should be handled inside Form or UserControl (see working example in <see cref="Form1"/> code-behind.
        /// <code>
        /// private void OnFrameReceived(object sender, FrameReceivedEventArgs e)
        /// {
        ///     if (this.InvokeRequired)
        ///     {
        ///         this.Invoke(new Action(() => OnFrameReceived(sender, e)));
        ///         return;
        ///     }
        ///
        ///     try
        ///     {
        ///         using (var ms = new MemoryStream(e.FrameData))
        ///         {
        ///             var image = Image.FromStream(ms);
        ///             
        ///             // preview_pictureBox - is a PictureBox element inside the WinForms application where the preview should be displayed.
        ///             
        ///             // Dispose previous image to prevent memory leaks
        ///             var oldImage = preview_pictureBox.Image;
        ///             preview_pictureBox.Image = image;
        ///             oldImage?.Dispose();
        ///         }
        ///     }
        ///     catch (Exception ex)
        ///     {
        ///         Console.WriteLine($"Error displaying frame: {ex.Message}");
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <example>
        /// Example how to handle <see cref="FrameReceived"/> event in WPF application,
        /// method should be handled inside Window or UserControl.
        /// <code>
        /// // property which is binded to WPF Image Source property like this:
        /// // Image Source="{Binding CurrentFrame}"
        /// // or like this in code-behind:
        /// // image.SetBinding(Image.SourceProperty, new Binding("CurrentFrame")
        /// public BitmapImage CurrentFrame
        /// {
        ///     get => _currentFrame;
        ///     set
        ///     {
        ///         _currentFrame = value;
        ///         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentFrame));
        ///     }
        /// }
        /// 
        /// private void OnFrameReceived(object sender, FrameReceivedEventArgs e)
        /// {
        ///     Application.Current.Dispatcher.Invoke(() =>
        ///     {
        ///         try
        ///         {
        ///             using (var ms = new MemoryStream(e.FrameData))
        ///             {
        ///                 var bitmap = new BitmapImage();
        ///                 bitmap.BeginInit();
        ///                 bitmap.CacheOption = BitmapCacheOption.OnLoad;
        ///                 bitmap.StreamSource = ms;
        ///                 bitmap.EndInit();
        ///                 bitmap.Freeze(); // Important for cross-thread access
        /// 
        ///                 CurrentFrame = bitmap;
        ///             }
        ///         }
        ///         catch (Exception ex)
        ///         {
        ///             Console.WriteLine($"Error displaying frame: {ex.Message}");
        ///         }
        ///     });
        /// }
        /// </code>
        /// </example>
        /// </remarks>
        public event EventHandler<FrameReceivedEventArgs> FrameReceived;

        /// <summary>
        /// Connection status changed event
        /// </summary>
        public event EventHandler<ConnectionStatusEventArgs> ConnectionStatusChanged;

        /// <summary>
        /// Connect to MJPEG Stream
        /// </summary>
        /// <param name="url">Stream URL</param>
        /// <returns></returns>
        public async Task ConnectAsync(string url)
        {
            await Task.Run(() => Connect(url));
        }

        private void Connect(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                string host = uri.Host;
                int port = uri.Port;

                _tcpClient = new TcpClient();
                _tcpClient.Connect(host, port);
                _stream = _tcpClient.GetStream();

                string httpRequest = $"GET / HTTP/1.1\r\n" +
                                   $"Host: {host}:{port}\r\n" +
                                   $"User-Agent: DMC-MJPEG-Viewer/1.0\r\n" +
                                   $"Accept: multipart/x-mixed-replace\r\n" +
                                   $"Connection: keep-alive\r\n" +
                                   $"\r\n";

                byte[] requestBytes = Encoding.UTF8.GetBytes(httpRequest);
                _stream.Write(requestBytes, 0, requestBytes.Length);

                _isRunning = true;
                _readerThread = new Thread(ReadStream)
                {
                    IsBackground = true,
                    Name = "WPF-MJPEGReaderThread"
                };
                _readerThread.Start();

                ConnectionStatusChanged?.Invoke(this, new ConnectionStatusEventArgs(true));
            }
            catch (Exception ex)
            {
                ConnectionStatusChanged?.Invoke(this, new ConnectionStatusEventArgs(false, $"Connection failed: {ex.Message}"));
                Cleanup();
                throw;
            }
        }

        private void ReadStream()
        {
            try
            {
                byte[] buffer = new byte[8192];
                MemoryStream dataStream = new MemoryStream();

                string headers = ReadHttpHeaders(_stream);

                while (_isRunning && _tcpClient.Connected)
                {
                    int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;

                    dataStream.Write(buffer, 0, bytesRead);
                    ProcessMJPEGData(dataStream);
                }
            }
            catch (Exception ex)
            {
                if (_isRunning)
                {
                    ConnectionStatusChanged?.Invoke(this, new ConnectionStatusEventArgs(false, $"Stream error: {ex.Message}"));
                }
            }
            finally
            {
                if (_isRunning)
                {
                    ConnectionStatusChanged?.Invoke(this, new ConnectionStatusEventArgs(false));
                }
            }
        }

        private string ReadHttpHeaders(NetworkStream stream)
        {
            StringBuilder headers = new StringBuilder();
            int b;
            int newlineCount = 0;

            while ((b = stream.ReadByte()) != -1)
            {
                char c = (char)b;
                headers.Append(c);

                if (c == '\n')
                {
                    newlineCount++;
                    if (newlineCount == 2)
                        break;
                }
                else if (c != '\r')
                {
                    newlineCount = 0;
                }
            }

            return headers.ToString();
        }

        private void ProcessMJPEGData(MemoryStream dataStream)
        {
            byte[] data = dataStream.ToArray();

            string dataString = Encoding.UTF8.GetString(data);
            int boundaryStart = dataString.IndexOf("--myboundary");

            if (boundaryStart == -1)
                return;

            int nextBoundaryStart = dataString.IndexOf("--myboundary", boundaryStart + 1);
            if (nextBoundaryStart == -1)
                return;

            string frameSection = dataString.Substring(boundaryStart, nextBoundaryStart - boundaryStart);

            int contentLengthIndex = frameSection.IndexOf("Content-Length:");
            if (contentLengthIndex == -1)
                return;

            int lengthStart = frameSection.IndexOf(":", contentLengthIndex) + 1;
            int lengthEnd = frameSection.IndexOf("\r\n", lengthStart);
            if (lengthEnd == -1)
                return;

            string lengthStr = frameSection.Substring(lengthStart, lengthEnd - lengthStart).Trim();
            if (!int.TryParse(lengthStr, out int contentLength))
                return;

            int imageDataStart = frameSection.IndexOf("\r\n\r\n");
            if (imageDataStart == -1)
                return;

            imageDataStart += 4;
            int imageDataStartBytes = Encoding.UTF8.GetByteCount(frameSection.Substring(0, imageDataStart)) + boundaryStart;

            if (data.Length < imageDataStartBytes + contentLength)
                return;

            byte[] imageData = new byte[contentLength];
            Array.Copy(data, imageDataStartBytes, imageData, 0, contentLength);

            FrameReceived?.Invoke(this, new FrameReceivedEventArgs(imageData));

            int remainingDataStart = imageDataStartBytes + contentLength;
            if (remainingDataStart < data.Length)
            {
                byte[] remainingData = new byte[data.Length - remainingDataStart];
                Array.Copy(data, remainingDataStart, remainingData, 0, remainingData.Length);

                dataStream.SetLength(0);
                dataStream.Write(remainingData, 0, remainingData.Length);
            }
            else
            {
                dataStream.SetLength(0);
            }
        }

        /// <summary>
        /// Disconnect from MJPEG stream
        /// </summary>
        public void Disconnect()
        {
            _isRunning = false;
            Cleanup();
            ConnectionStatusChanged?.Invoke(this, new ConnectionStatusEventArgs(false));
        }

        private void Cleanup()
        {
            lock (_lockObject)
            {
                try
                {
                    _stream?.Close();
                    _tcpClient?.Close();
                }
                catch { }

                _stream = null;
                _tcpClient = null;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Disconnect();
            _readerThread?.Join(2000);
        }
    }
}
