using Prism.Mvvm;


namespace ToDoList.Models
{
    public class ToDoItem : BindableBase
    {              

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

        public ToDoItem()
        {

        }

        private bool _isComplete;
        private string _name;
    }
}
