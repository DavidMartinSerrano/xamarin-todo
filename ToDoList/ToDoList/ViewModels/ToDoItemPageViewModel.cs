using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Models;
using Xamarin.Forms;

namespace ToDoList.ViewModels
{
    public class ToDoItemPageViewModel : BindableBase, INavigationAware
    {
        #region Properties
        public ICommand SaveItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
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
        public ToDoItemPageViewModel(INavigationService navigationService)
        {
            //Inject services
            _navigationService = navigationService;       


            SaveItemCommand = new DelegateCommand(OnSaveItem);
            DeleteItemCommand = new DelegateCommand(OnDeleteItem);
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

            //if (AppSettings.VoiceLanguage == Enums.Language.English)
            //    _itemAnnouncementService.SayItemSavedInEnglish();

            //else if (AppSettings.VoiceLanguage == Enums.Language.Ukrainian)
            //    _itemAnnouncementService.SayItemSavedInUkrainian();
                        
            //await _toDoItemsRepository.SaveItemAsync(toDoItem);
            
            await _navigationService.GoBackAsync();
        }

        private async void OnDeleteItem()
        {
            var toDoItem = new ToDoItem()
            {
                Name = _name,
                IsComplete = _isComplete
            };
       
           // await _toDoItemsRepository.DeleteItemAsync(toDoItem);
            
            await _navigationService.GoBackAsync();
        }

        private async void OnGoBack()
        {
            await _navigationService.GoBackAsync();
        }             

        public void OnNavigatingTo(NavigationParameters parameters)
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
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Fields        
        private readonly INavigationService _navigationService;
        private string _pageTitle;
        private int _itemId = 0;
        private string _name;
        private string _notes;
        private bool _isComplete;
        #endregion
    }
}