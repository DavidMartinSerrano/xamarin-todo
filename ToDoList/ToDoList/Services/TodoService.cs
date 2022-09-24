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
        private const string localIpAddressAndPort = "http://10.0.2.2:8080";
        public TodoService()
        {
        }

        /// <summary>
        /// Gets the todo items async.
        /// </summary>
        /// <returns>The todo items async.</returns>
        public async Task<List<ToDoItem>> GetTodoItemsAsync()
        {
            var response = await client.GetStringAsync($"{localIpAddressAndPort}/api/todo");
            var todoItems = JsonConvert.DeserializeObject<List<ToDoItem>>(response);
            return todoItems;
        }

        /// <summary>
        /// Adds the todo item async.
        /// </summary>
        /// <returns>The todo item async.</returns>
        /// <param name="itemToAdd">Item to add.</param>
        public async Task AddTodoItemAsync(ToDoItem itemToAdd)
        {
            var data = JsonConvert.SerializeObject(itemToAdd);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PostAsync($"{localIpAddressAndPort}/api/todo/create", content);            
        }

        /// <summary>
        /// Updates the todo item async.
        /// </summary>
        /// <returns>The todo item async.</returns>
        /// <param name="itemkey">Item key.</param>
        /// <param name="itemToUpdate">Item to update.</param>
        public async Task UpdateTodoItemAsync(string itemkey, ToDoItem itemToUpdate)
        {
            var data = JsonConvert.SerializeObject(itemToUpdate);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var url = $"{localIpAddressAndPort}/api/todo/{itemkey}";
            await client.PutAsync(url, content);           
        }

        /// <summary>
        /// Deletes the todo item async.
        /// </summary>
        /// <returns>The todo item async.</returns>
        /// <param name="itemkey">Item key.</param>
        public async Task DeleteTodoItemAsync(string itemkey)
        {
            await client.DeleteAsync($"{localIpAddressAndPort}/api/todo/{itemkey}");
        }
    }
}
