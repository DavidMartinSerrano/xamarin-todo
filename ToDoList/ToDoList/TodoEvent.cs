using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public class TodoEvent :
     PubSubEvent<TodoMessage>
    { }
}
