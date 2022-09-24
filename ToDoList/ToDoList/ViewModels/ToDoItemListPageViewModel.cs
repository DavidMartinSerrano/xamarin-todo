using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ToDoList.Models;
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
        public ToDoItemListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            AddToDoItemCommand = new DelegateCommand(OnAddItem);
            ChangeStateCommand = new DelegateCommand<ToDoItem>(OnChangeState);
            EditItemCommand = new DelegateCommand(OnEditItem);
            DeleteCommand = new DelegateCommand<ToDoItem>(OnDeleteItem);
            RefreshListViewCommand = new DelegateCommand(OnListViewRefreshing);
        }
        #endregion

        #region Methods        
        private async void OnAddItem()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("PageTitle", "Create new item");

            await _navigationService.NavigateAsync("ToDoItemPage", navParameters);
        }

        private void OnChangeState(ToDoItem item)
        {
            if (item != null)
            {
                item.IsComplete = !item.IsComplete;
                //_toDoItemsRepository.SaveItemAsync(item);
            }
        }

        private async void OnEditItem()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("ToDoItem", SelectedItem);
            navParameters.Add("PageTitle", "Edit item");

            await _navigationService.NavigateAsync("ToDoItemPage", navParameters);            
        }

        private async void OnDeleteItem(ToDoItem item)
        {
            if (item != null)
            {
                ToDoItems.Remove(item);
              //  await _toDoItemsRepository.DeleteItemAsync(item);
            }
        }

        private async void OnListViewRefreshing()
        {
            ToDoItems = new ObservableCollection<ToDoItem>();//await _toDoItemsRepository.GetAllAsync());
            IsListViewRefreshing = false;
        }

        private async void OnGoinngToAppSettings()
        {
            var navParameters = new NavigationParameters();
            navParameters.Add("PageTitle", "Settings");

            await _navigationService.NavigateAsync("AppSettingsPage", navParameters);
        }
    

        public async void OnNavigatingTo(NavigationParameters parameters)
        {
            ToDoItems = new ObservableCollection<ToDoItem>();//await _toDoItemsRepository.GetAllAsync());
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Fields        
        private readonly INavigationService _navigationService;
        private ObservableCollection<ToDoItem> _toDoItems;
        private bool _isListViewRefreshing;
        private ToDoItem _selectedItem;
        #endregion
    }
}
