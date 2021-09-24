using System;
using System.Threading;
using System.Windows.Forms;

namespace PinIt
{
    static class Program
    {
        // Only allow a single instance of the app
        private static Mutex mutex = null;

        [STAThread]
        static void Main()
        {
            // Application name
            const string appName = "PinIt";

            // Set the mutex based on the app name
            mutex = new Mutex(true, appName, out bool createdNew);

            // If this is a unique mutex, create the app.
            if (createdNew == true)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new HandleForm());
            }
            // Otherwise prompt an error and exit
            else
            {
                MessageBox.Show("There an instance of PinIt already running", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}
