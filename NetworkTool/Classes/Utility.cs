using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkTool
{
    // START DeleteFile Class -----------------------------------------------------------------------------------------------------------
    public static class DeleteFile
    {
        // Simple file deletion tool.
        public static bool Target(string file)
        {
            bool deleted = false;

            if (System.IO.File.Exists(file))
            {
                try
                {
                    System.IO.File.Delete(file);

                    deleted = true;
                }
                catch (Exception Ex)
                {
                    LogWriter.Exception("Error deleting file: " + file, Ex);
                }
            }
            else
            {
                deleted = true;
            }

            return deleted;
        }
    }
    // END DeleteFile Class -------------------------------------------------------------------------------------------------------------

    // START LogWriter_Class ------------------------------------------------------------------------------------------------------------
    public static class LogWriter
    {
        // This class is a simple thread safe log writer/displayer.

        private static ReaderWriterLockSlim log_locker = new ReaderWriterLockSlim();

        private static string log_file { get; set; }

        // Creates a new log file if appropriate
        // -----------------------------------------------------------------------
        public static bool GenerateLogFile()
        {
            // Log file name is defined.
            string log_name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            log_file = (Statics.ProgramPath + @"\log\" + DateTime.Now.ToString("yyyy-MM-dd") + "-" + log_name + ".log");

            // Log file is created if none exists.
            if (System.IO.File.Exists(log_file) == false)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(Statics.ProgramPath + @"\log"); // Generates the folder for the log files if non exists.
                }
                catch
                {
                    return false;
                }

                try
                {
                    System.IO.File.Create(log_file).Dispose(); // Generates a log file if non exists.
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }
        // -----------------------------------------------------------------------

        // Logs string to log file.
        // -----------------------------------------------------------------------
        public static void LogEntry(string log)
        {
            // These might be used for timeouts later. Needed at this time for proper task creation.
            var source = new CancellationTokenSource();
            var token = source.Token;

            // Creates a thread that will write to a log file.
            Task.Factory.StartNew(() =>
            {
                LogEntry_Thread_Call(log);
            },
            token, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
        }

        private static void LogEntry_Thread_Call(string log)
        {
            log_locker.EnterWriteLock();

            if (GenerateLogFile())
            {
                try
                {
                    System.IO.File.AppendAllText(log_file, (DateTime.Now.ToString("yyyy-MM-dd - HH:mm:ss") + " - " + Statics.CurrentUser + " - " + log + Environment.NewLine));
                }
                catch (Exception Ex)
                {
                    Show_Message("Error updating logfile. " + Environment.NewLine + Convert.ToString(Ex), "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            log_locker.ExitWriteLock();
        }

        // -----------------------------------------------------------------------
        public static void LogDisplay(string log, MessageBoxIcon type, Form window = null)
        {
            // These might be used for timeouts later. Needed at this time for proper task creation.
            var source = new CancellationTokenSource();
            var token = source.Token;

            // Creates a thread that will write to a log file.
            Task.Factory.StartNew(() =>
            {
                LogDisplay_Thread_Call(log, type, window);
            },
            token, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
        }

        private static void LogDisplay_Thread_Call(string log, MessageBoxIcon type, Form window = null)
        {
            log_locker.EnterWriteLock();

            if (GenerateLogFile())
            {
                try
                {
                    System.IO.File.AppendAllText(log_file, DateTime.Now.ToString("yyyy-MM-dd - HH:mm:ss") + " - " + log + Environment.NewLine);

                    switch (type)
                    {
                        default:

                            break;

                        case MessageBoxIcon.Error:

                            Show_Message(log, "Error...", MessageBoxButtons.OK, type, window);

                            break;

                        case MessageBoxIcon.Exclamation:

                            Show_Message(log, "Important...", MessageBoxButtons.OK, type, window);

                            break;

                        case MessageBoxIcon.Information:

                            Show_Message(log, "Information...", MessageBoxButtons.OK, type, window);

                            break;

                        case MessageBoxIcon.None:

                            Show_Message(log, "Message...", MessageBoxButtons.OK, type, window);

                            break;

                        case MessageBoxIcon.Question:

                            Show_Message(log, "Question...", MessageBoxButtons.OK, type, window);

                            break;
                    }
                }
                catch (Exception Ex)
                {
                    Show_Message("Error updating logfile. " + Environment.NewLine + Convert.ToString(Ex), "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            log_locker.ExitWriteLock();
        }

        // -----------------------------------------------------------------------
        public static void Exception(string log, Exception Ex, bool show = false, bool exception = false, Form window = null)
        {
            // These might be used for timeouts later. Needed at this time for proper task creation.
            var source = new CancellationTokenSource();
            var token = source.Token;

            // Creates a thread that will write to a log file.
            Task.Factory.StartNew(() =>
            {
                Exception_Thread_Call(log, Ex, show, exception, window);
            },
            token, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
        }

        private static void Exception_Thread_Call(string log, Exception Ex, bool show = false, bool exception = false, Form window = null)
        {
            LogEntry(log + Environment.NewLine + Convert.ToString(Ex));

            if (show)
            {
                if (exception)
                {
                    Show_Message(log + Environment.NewLine + Convert.ToString(Ex), "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error, window);
                }
                else
                {
                    Show_Message(log, "Error...", MessageBoxButtons.OK, MessageBoxIcon.Error, window);
                }
            }
        }

        private static void Show_Message(string log, string title, MessageBoxButtons button, MessageBoxIcon type, Form window = null)
        {
            if (window == null && Statics.MainWindow != null)
            {
                window = Statics.MainWindow;
            }

            if (window == null)
            {
                MessageBox.Show(log, title, button, type);
            }
            else
            {
                if (window.InvokeRequired)
                {
                    window.Invoke(new Action(() =>
                    {
                        MessageBox.Show(window, log, title, button, type);
                    }));
                    return;
                }
            }
        }

        // More for this later.
        // -----------------------------------------------------------------------
        //public static void ErrorLog()
        //{

        //}
    }
    // END LogWriter_Class --------------------------------------------------------------------------------------------------------------
}
