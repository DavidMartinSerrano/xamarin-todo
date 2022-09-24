using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface ITodoService
    {
        Task<int> AddTodoItemAsync(ToDoItem itemToAdd);
        Task DeleteTodoItemAsync(int itemIndex);
        Task<List<ToDoItem>> GetTodoItemsAsync();
        Task<int> UpdateTodoItemAsync(int itemIndex, ToDoItem itemToUpdate);
    }
}