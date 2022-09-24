using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Models;
using ToDoList.Services;
using Xamarin.Forms;

namespace ToDoList.ViewModels
{
    public class ToDoItemListPageViewModel : BindableBase, INavigationAware
    {
        #region Properties
        public ICommand AddToDoItemCommand { get; set; }
        public ICommand ChangeStateCommand { get; set; }
        public ICommand EditItemCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshListViewCommand { get; set; }
        public ToDoItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {                
                SetProperty(ref _selectedItem, value);

                if (_selectedItem == null)
                    return;

                OnEditItem();

                SelectedItem = null;
            }
        }


        public ObservableCollection<ToDoItem> ToDoItems
        {
            get { return _toDoItems; }
            set { SetProperty(ref _toDoItems, value); }
        }

        public bool IsListViewRefreshing
        {
            get { return _isListViewRefreshing; }
            set { SetProperty(ref _isListViewRefreshing, value); }
        }
        
        #endregion

        #region Constructor        
        public ToDoItemListPageViewModel(INavigationService navigationService, ITodoService todoService)
        {
            _navigationService = navigationService;
            _todoService = todoService;
            
          
            AddToDoItemCommand = new DelegateCommand(OnAddItem);
            ChangeStateCommand = new DelegateCommand<ToDoItem>(OnChangeState);
            EditItemCommand = new DelegateCommand(OnEditItem);
            DeleteCommand = new DelegateCommand<ToDoItem>(OnDeleteItem);
            RefreshListViewCommand = new DelegateCommand(OnListViewRefreshing);
            EventSystem.Current.GetEvent<TodoEvent>().Subscribe((message) => _updateTodos = true);

            Task.Run(async () => ToDoItems = new ObservableCollection<ToDoItem>(await _todoService.GetTodoItemsAsync()));
        }
    
        #endregion

        #region Methods        
        private async void OnAddItem()
        {

            var navParameters = new NavigationParameters();
            navParameters.Add("PageTitle", "Create new item");

            await _navigationService.NavigateAsync("ToDoItemPage", navParameters);
        }

        private async void OnChangeState(ToDoItem item)
        {
            if (item != null)
            {
                item.IsComplete = !item.IsComplete;
                await _todoService.UpdateTodoItemAsync(item.Key, item);
            }
        }

        private async void OnEditItem()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("ToDoItem", SelectedItem);
            navParameters.Add("PageTitle", "Edit item");
            navParameters.Add("ItemKey", SelectedItem.Key);

            await _navigationService.NavigateAsync("ToDoItemPage", navParameters);            
        }

        private async void OnDeleteItem(ToDoItem item)
        {
            if (item != null)
            {
                ToDoItems.Remove(item);
                await _todoService.DeleteTodoItemAsync(item.Key);
            }
        }

        private async void OnListViewRefreshing()
        {
            ToDoItems = new ObservableCollection<ToDoItem>(await _todoService.GetTodoItemsAsync());
            IsListViewRefreshing = false;
        }
    

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
           
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (_updateTodos)
            {
                ToDoItems = new ObservableCollection<ToDoItem>(await _todoService.GetTodoItemsAsync());
            }
            _updateTodos = false;
        }
        #endregion

        #region Fields        
        private readonly INavigationService _navigationService;
        private readonly ITodoService _todoService;
        private ObservableCollection<ToDoItem> _toDoItems;
        private bool _isListViewRefreshing;
        private ToDoItem _selectedItem;
        private bool _updateTodos;
        #endregion
    }
}
