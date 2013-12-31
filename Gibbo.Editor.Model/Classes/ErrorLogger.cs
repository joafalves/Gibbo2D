using System.Collections.Generic;
using Microsoft.Build.Framework;

namespace Gibbo.Editor.Model
{
    public class ErrorLog
    {
        private string fileName;
        private int lineNumber;
        private int columnNumber;
        private string errorMessage;

        public int LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        public int ColumnNumber
        {
            get { return columnNumber; }
            set { columnNumber = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public override string ToString()
        {
            return fileName + " - Line: " + lineNumber + " - Error: " + errorMessage;
        }
    }

    /// <summary>
    /// Custom implementation of the MSBuild ILogger interface records
    /// content build errors so we can later display them to the user.
    /// </summary>
    public class ErrorLogger : ILogger
    {
        private List<ErrorLog> errors = new List<ErrorLog>();

        /// <summary> 
        /// Gets a list of all the errors that have been logged.
        /// </summary>
        public List<ErrorLog> Errors
        {
            get { return errors; }
        }

        /// <summary>
        /// Initializes the custom logger, hooking the ErrorRaised notification event.
        /// </summary>
        public void Initialize(IEventSource eventSource)
        {
            if (eventSource != null)
            {
                eventSource.ErrorRaised += ErrorRaised;
            }
        }
        /// <summary>
        /// Shuts down the custom logger.
        /// </summary>
        public void Shutdown()
        {
        }


        /// <summary>
        /// Handles error notification events by storing the error message string.
        /// </summary>
        void ErrorRaised(object sender, BuildErrorEventArgs e)
        {
            errors.Add(new ErrorLog()
            {
                ColumnNumber = e.ColumnNumber,
                LineNumber = e.LineNumber,
                ErrorMessage = e.Message,
                FileName = e.File
            });
        }

        #region ILogger Members


        /// <summary>
        /// Implement the ILogger.Parameters property.
        /// </summary>
        string ILogger.Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        string parameters;


        /// <summary>
        /// Implement the ILogger.Verbosity property.
        /// </summary>
        LoggerVerbosity ILogger.Verbosity
        {
            get { return verbosity; }
            set { verbosity = value; }
        }

        LoggerVerbosity verbosity = LoggerVerbosity.Normal;


        #endregion
    }
}
