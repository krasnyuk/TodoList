﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLT.Models;

namespace NLT.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private NLTContext _db;
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
        }

        public _Task GetTask(int id)
        {
            return _db._Task.Find(id);
        }

        public IEnumerable<_Task> GetTaskList()
        {
            return _db._Task.ToList();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(_Task item)
        {
            _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}