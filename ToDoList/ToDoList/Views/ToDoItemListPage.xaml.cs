using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToDoList.Views
{
    public partial class ToDoItemListPage : ContentPage
    {
        public ToDoItemListPage()
        {
            InitializeComponent();
            listView.IsPullToRefreshEnabled = true; 
        }
         
    }
}
