using Microsoft.Practices.Unity;
using Acr.UserDialogs;
using Prism.Unity;
using PrismDeUserDialogs.Views;

namespace PrismDeUserDialogs
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
            Container.RegisterTypeForNavigation<MainPage>();
        }
    }
}
