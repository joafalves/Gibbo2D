#region Copyrights
/*
Gibbo2D License - Version 1.0
Copyright (c) 2013 - Gibbo2D Team
Founders Joao Alves <joao.cpp.sca@gmail.com> & Luis Fernandes <luisapidcloud@hotmail.com>

Permission is granted to use this software and associated documentation files (the "Software") free of charge, 
to any person or company. The code can be used, modified and merged without restrictions, but you cannot sell 
the software itself and parts where this license applies. Still, permission is granted for anyone to sell 
applications made using this software (for example, a game). This software cannot be claimed as your own, 
except for copyright holders. This license notes should also be available on any of the changed or added files.

The software is provided "as is", without warranty of any kind, express or implied, including but not limited to 
the warranties of merchantability, fitness for a particular purpose and non-infrigement. In no event shall the 
authors or copyright holders be liable for any claim, damages or other liability.

*/
#endregion

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
