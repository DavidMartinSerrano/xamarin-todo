using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TodoService : ITodoService
    {
        HttpClient client = new HttpClient();
        public TodoService()
        {
        }

        /// <summary>
        /// Gets the todo items async.
        /// </summary>
        /// <returns>The todo items async.</returns>
        public async Task<List<ToDoItem>> GetTodoItemsAsync()
        {
            var response = await client.GetStringAsync("http://localhost:8080/api/todo/items");
            var todoItems = JsonConvert.DeserializeObject<List<ToDoItem>>(response);
            return todoItems;
        }

        /// <summary>
        /// Adds the todo item async.
        /// </summary>
        /// <returns>The todo item async.</returns>
        /// <param name="itemToAdd">Item to add.</param>
        public async Task<int> AddTodoItemAsync(ToDoItem itemToAdd)
        {
            var data = JsonConvert.SerializeObject(itemToAdd);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:8080/api/todo/item", content);
            var result = JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        /// <summary>
        /// Updates the todo item async.
        /// </summary>
        /// <returns>The todo item async.</returns>
        /// <param name="itemIndex">Item index.</param>
        /// <param name="itemToUpdate">Item to update.</param>
        public async Task<int> UpdateTodoItemAsync(int itemIndex, ToDoItem itemToUpdate)
        {
            var data = JsonConvert.SerializeObject(itemToUpdate);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(string.Concat("http://localhost:8080/api/todo/", itemIndex), content);
            return JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Deletes the todo item async.
        /// </summary>
        /// <returns>The todo item async.</returns>
        /// <param name="itemIndex">Item index.</param>
        public async Task DeleteTodoItemAsync(int itemIndex)
        {
            await client.DeleteAsync(string.Concat("http://localhost:8080/api/todo/", itemIndex));
        }
    }
}
