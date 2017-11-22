using System.Windows;

namespace Liikennetieto
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var vm = new MainWindowModel();
            var view = new MainWindow { DataContext = vm };
            view.Show();
        }
    }
}
