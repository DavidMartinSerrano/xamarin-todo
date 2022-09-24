using Prism.Events;

namespace ToDoList
{
    public class TodoEvent :
     PubSubEvent<TodoMessage>
    { }
}
