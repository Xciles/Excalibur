using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Excalibur.AspNetCore.Utils
{
    /// <summary>
    /// Helper class for executing shell commands
    /// </summary>
    public static class ShellHelper
    {
        /// <summary>
        /// Helper utility that executes commands on a bash shell
        /// </summary>
        /// <param name="cmd">The command to execute</param>
        /// <param name="workingDirectory">The working directory to start from</param>
        /// <returns>The command execution result</returns>
        private static string Bash(string cmd, string workingDirectory)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = workingDirectory
                }
            };
            process.Start();
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Dispose();

            return result;
        }

        /// <summary>
        /// Helper utility that executes commands on a windows shell
        /// </summary>
        /// <param name="program">The program to execute</param>
        /// <param name="arguments">The program arguments</param>
        /// <param name="workingDirectory">The working directory to start from</param>
        /// <returns>The command execution result</returns>
        private static string Command(string program, string arguments, string workingDirectory)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = program,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = workingDirectory
                }
            };
            process.Start();
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Dispose();

            return result;
        }

        /// <summary>
        /// Execute a shell command on either Windows or Linux
        /// The command will start a new process and will run the program with given arguments.
        ///
        /// On Windows this will just run the provided program.
        /// On Linux this will run bash with commands.
        /// </summary>
        /// <param name="program">The program to execute</param>
        /// <param name="arguments">The program arguments</param>
        /// <param name="workingDirectory">The working directory to start from</param>
        /// <returns>The command execution result</returns>
        public static string Execute(string program, string arguments, string workingDirectory)
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Command(program, arguments, workingDirectory) : Bash($"{program} {arguments}", workingDirectory);
        }
    }
}