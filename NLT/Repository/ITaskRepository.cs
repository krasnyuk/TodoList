using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLT.Models;

namespace NLT.Repository
{
    public interface ITaskRepository
    {
        IQueryable<_Task> GetTaskList();
        _Task GetTask(int id);
        void Create(_Task item);
        void Update(_Task item);
        void Delete(int id);
        void DeleteTasksForList(int id);
        void Save();
        IQueryable<_Task> GetTasksForList(int todoListId);



    }
}
