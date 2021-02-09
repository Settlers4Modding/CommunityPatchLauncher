using System;
using System.Diagnostics;
using System.IO;

namespace CommunityPatchLauncher.Commands.Os
{
    /// <summary>
    /// Open a specific program on the pc
    /// </summary>
    internal class StartProgramCommand : BaseCommand
    {
        /// <summary>
        /// The program path to start
        /// </summary>
        protected string programPath;

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        public StartProgramCommand() : this(null)
        {
        }

        /// <summary>
        /// Create a new instance of this class
        /// </summary>
        /// <param name="programPath">Path to use ignoring parameter</param>
        public StartProgramCommand(string programPath)
        {
            this.programPath = File.Exists(programPath) ? programPath : null;
        }

        /// <inheritdoc/>
        public override bool CanExecute(object parameter)
        {
            if (programPath != null)
            {
                return true;
            }
            if (parameter is string filePath)
            {
                return File.Exists(filePath);
            }
            return false;
        }

        /// <inheritdoc/>
        public override void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            string pathToStart = parameter == null ? programPath : parameter as string;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            FileInfo fileInfo = new FileInfo(pathToStart);
            startInfo.FileName = pathToStart;
            startInfo.WorkingDirectory = fileInfo.DirectoryName;
            try
            {
                Process.Start(startInfo);
            }
            catch (Exception)
            {
                //Everything is fine, user just click on no for the uac
            }
        }
    }
}
