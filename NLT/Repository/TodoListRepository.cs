using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLT.Models;

namespace NLT.Repository
{
    public class TodoListRepository : ITodoListRepository
    {

        private readonly NLTContext _db;
        public TodoListRepository()
        {
            _db = new NLTContext();
        }

        public void Create(TodoList item)
        {
            _db.TodoList.Add(item);
        }

        public void Delete(int id)
        {
            TodoList entry = _db.TodoList.Find(id);
            if (entry != null)
                _db.TodoList.Remove(entry);
            else
                throw  new NullReferenceException();
        }

        public TodoList GetTodoList(int id)
        {
            TodoList entry = _db.TodoList.Find(id);
            if (entry != null)
                return entry;
            throw new NullReferenceException();
        }

        public IQueryable<TodoList> GetTodoLists()
        {
            return _db.TodoList;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(TodoList item)
        {
            _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}