using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Models;
using ToDoList.Services;
using Xamarin.Forms;

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
      
            if(PageTitle.Equals("Edit item"))
            {
                await _todoService.UpdateTodoItemAsync(_itemIndex, toDoItem);
            }
            else
            {
                await _todoService.AddTodoItemAsync(toDoItem);

            }

            await _navigationService.GoBackAsync();
        }

        private async void OnGoBack()
        {
            await _navigationService.GoBackAsync();
        }             

        public void OnNavigatingTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("ToDoItem"))
            {
                var item = (ToDoItem)parameters["ToDoItem"];

                Name = item.Name;
                IsComplete = item.IsComplete;
            }
            if (parameters.ContainsKey("PageTitle"))
            {
                PageTitle = (string)parameters["PageTitle"];
            }
            if (parameters.ContainsKey("ItemIndex"))
            {
                _itemIndex = Convert.ToInt32((string)parameters["ItemIndex"]);
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
        #endregion

        #region Fields        
        private readonly INavigationService _navigationService;
        private readonly ITodoService _todoService;
        private string _pageTitle;
        private int _itemIndex = 0;
        private string _name;
        private bool _isComplete;
        #endregion
    }
}