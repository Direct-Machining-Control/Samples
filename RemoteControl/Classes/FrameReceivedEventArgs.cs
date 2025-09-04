using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Classes
{
    /// <summary>
    /// Class containing Frame Received event arguments
    /// </summary>
    public class FrameReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Received frame data
        /// </summary>
        public byte[] FrameData { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="frameData">Received frame data</param>
        public FrameReceivedEventArgs(byte[] frameData)
        {
            FrameData = frameData;
        }
    }
}
