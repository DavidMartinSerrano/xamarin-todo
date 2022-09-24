using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface ITodoService
    {
        Task AddTodoItemAsync(ToDoItem itemToAdd);
        Task DeleteTodoItemAsync(string itemkey);
        Task<List<ToDoItem>> GetTodoItemsAsync();
        Task UpdateTodoItemAsync(string itemkey, ToDoItem itemToUpdate);
    }
}