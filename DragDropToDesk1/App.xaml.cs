using System.Windows;
using Prism.Modularity;
using Prism.Unity;
using Prism.Ioc;

namespace DragDropToDesk
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
        }
    }
}
