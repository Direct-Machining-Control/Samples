using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteControl.Classes
{
    /// <summary>
    /// Class containing Connection Status event arguments
    /// </summary>
    public class ConnectionStatusEventArgs : EventArgs
    {
        /// <summary>
        /// Flag telling if client is connected
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// Gets or sets the error message associated with the current operation or state.
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isConnected">Connection status</param>
        /// <param name="errorMessage">Error message</param>
        public ConnectionStatusEventArgs(bool isConnected, string errorMessage = null)
        {
            IsConnected = isConnected;
            ErrorMessage = errorMessage;
        }
    }
}
