using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Windows.Input;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.ViewModels
{
    public class ToDoItemPageViewModel : BindableBase, INavigationAware
    {
        #region Properties
        public ICommand SaveItemCommand { get; set; }
        public ICommand GoBackCommand { get; set; }

        public string PageTitle
        {
            get { return _pageTitle; }
            set { SetProperty(ref _pageTitle, value); }
        }


        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }


        public bool IsComplete
        {
            get { return _isComplete; }
            set { SetProperty(ref _isComplete, value); }
        }
        #endregion

        #region Constructor        
        public ToDoItemPageViewModel(INavigationService navigationService, ITodoService todoService)
        {
            _navigationService = navigationService;
            _todoService = todoService;

            SaveItemCommand = new DelegateCommand(OnSaveItem);
            GoBackCommand = new DelegateCommand(OnGoBack);
        }
        #endregion

        #region Methods        
        private async void OnSaveItem()
        {
            var toDoItem = new ToDoItem()
            {
                Name = _name,
                IsComplete = _isComplete
            };
          
            await _todoService.AddTodoItemAsync(toDoItem);
            EventSystem.Current.GetEvent<TodoEvent>().Publish(
              new TodoMessage { });

            await _navigationService.GoBackAsync();
        }

        private async void OnGoBack()
        {
            await _navigationService.GoBackAsync();
        }


        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var item = parameters.GetValue<ToDoItem>("ToDoItem");
            if (item != null) IsComplete = item.IsComplete;

            PageTitle = parameters.GetValue<string>("PageTitle");
        }
        #endregion

        #region Fields        
        private readonly INavigationService _navigationService;
        private readonly ITodoService _todoService;
        private string _pageTitle = String.Empty;
        private string _name = String.Empty;
        private bool _isComplete;
        #endregion
    }
}