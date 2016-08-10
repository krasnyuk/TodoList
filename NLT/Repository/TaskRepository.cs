using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLT.Models;

namespace NLT.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly NLTContext _db;
        public TaskRepository()
        {
            _db = new NLTContext();
        }

        public void Create(_Task item)
        {
            _db._Task.Add(item);
        }

        public void Delete(int id)
        {
            _Task entry = _db._Task.Find(id);
            if (entry != null)
                _db._Task.Remove(entry);
            else
                throw new NullReferenceException();
        }

        public void DeleteTasksForList(int id)
        {
            _db._Task.ToList().RemoveAll(x => x.TodoListId == id);
        }

        public _Task GetTask(int id)
        {
            _Task entry = _db._Task.Find(id);
            if (entry != null)
                return entry;
            else
                throw new NullReferenceException();
        }

        public IQueryable<_Task> GetTaskList()
        {
            return _db._Task;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public IQueryable<_Task> GetTasksForList(int todoListId)
        {
            return _db._Task.Where(x => x.TodoListId == todoListId);
        }


        public void Update(_Task item)
        {
            _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}