using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace NetworkTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Statics.initialization();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Networking());
        }
    }

    // START Statics_Class --------------------------------------------------------------------------------------------------------------
    public static class Statics
    {
        public static Form MainWindow = null;

        public static readonly string CurrentUser = Environment.UserName.ToLower();

        public static string ProgramPath = "";

        // Method to load start up global vars.
        public static void initialization()
        {
            LoadDLLs load_dlls = new LoadDLLs();
            load_dlls.init();

            try
            {
                if (System.AppDomain.CurrentDomain.BaseDirectory[System.AppDomain.CurrentDomain.BaseDirectory.Length - 1] == '\\')
                {
                    ProgramPath = System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
                }
                else
                {
                    ProgramPath = Environment.CurrentDirectory;
                }
            }
            catch (Exception Ex)
            {
                LogWriter.Exception("Error attempting to find program local path.", Ex);
            }
        }
    }

    // START LoadDLLs_Class -------------------------------------------------------------------------------------------------------------
    public class LoadDLLs
    {
        public void init()
        {
            // NECESSARY for loading embedded resources. Cannot be static class.
            // -----------------------------------------------------------------------------------
            try
            {
                AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
                {
                    string resourceName = new AssemblyName(args.Name).Name + ".dll";
                    string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                    {
                        Byte[] assemblyData = new Byte[stream.Length];
                        stream.Read(assemblyData, 0, assemblyData.Length);
                        return Assembly.Load(assemblyData);
                    }
                };
            }
            catch (Exception Ex)
            {
                LogWriter.Exception("Error loading embedded assembly resource. Application is about to crash.", Ex, true);
            }
            // -----------------------------------------------------------------------------------
        }
    }
    // END LoadDLLs_Class ---------------------------------------------------------------------------------------------------------------
}
