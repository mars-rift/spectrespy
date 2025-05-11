using System.Windows;

namespace spectrespy
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Create and show main window explicitly rather than using StartupUri
            var mainWindow = new spectrespy.Views.MainWindow();
            mainWindow.Show();
        }
    }
}