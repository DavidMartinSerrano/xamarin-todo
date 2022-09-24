using Prism.Unity;
using Prism;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Prism.Ioc;
using ToDoList.Views;
using ToDoList.Services;

namespace ToDoList
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();        

            NavigationService.NavigateAsync("MainNavigationPage/ToDoItemListPage");
        }

        private void RegisterTypesForNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ToDoItemListPage>();
            containerRegistry.RegisterForNavigation<ToDoItemPage>();
            containerRegistry.RegisterForNavigation<MainNavigationPage>();

        }

        private void RegisterInterfaces(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ITodoService, TodoService>(); 
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterTypesForNavigation(containerRegistry);
            RegisterInterfaces(containerRegistry);
        }
    }
}
