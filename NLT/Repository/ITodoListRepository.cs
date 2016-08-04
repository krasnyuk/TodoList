using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLT.Models;
namespace NLT.Repository
{
    public interface ITodoListRepository
    {
        IEnumerable<TodoList> GetTodoLists();
        TodoList GetTodoList(int id);
        void Create(TodoList item);
        void Update(TodoList item);
        void Delete(int id);
        void Save();
    }
}
