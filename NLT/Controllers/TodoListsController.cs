﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using NLT.Models;
using NLT.Repository;

namespace NLT.Controllers
{
    public class TodoListsController : ApiController
    {

        private readonly ITodoListRepository _toDoRepository;
        private readonly ITaskRepository _taskRepository;

        public TodoListsController(ITodoListRepository rep, ITaskRepository taskRepository)
        {
            _toDoRepository = rep;
            _taskRepository = taskRepository;
        }

        // GET: api/TodoLists
        public IList<TodoList> GetTodoLists()
        {
            return _toDoRepository.GetTodoLists().ToList();
        }

        // GET: api/TodoLists/5
        [ResponseType(typeof(ToDoListDetailsDto))]
        public IHttpActionResult GetTodoList(int id)
        {
            TodoList todoList;
            try
            {
                todoList = _toDoRepository.GetTodoList(id);
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            return Ok(todoList);
        }

        // PUT: api/TodoLists/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTodoList(int id, TodoList todoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todoList.Id)
            {
                return BadRequest();
            }

            _toDoRepository.Update(todoList);

            try
            {
                _toDoRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoListExists(id))
                {
                    return NotFound();
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TodoLists
        [ResponseType(typeof(TodoList))]
        public IHttpActionResult PostTodoList(TodoList todoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _toDoRepository.Create(todoList);
            _toDoRepository.Save();
            return CreatedAtRoute("DefaultApi", new { id = todoList.Id }, todoList);
        }

        // DELETE: api/TodoLists/5
        [ResponseType(typeof(TodoList))]
        public IHttpActionResult DeleteTodoList(int id)
        {
            try
            {
                _toDoRepository.Delete(id);
                _taskRepository.DeleteTasksForList(id);
                _toDoRepository.Save();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            
            return Ok();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
                
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool TodoListExists(int id)
        {
            return _toDoRepository.GetTodoLists().Count(e => e.Id == id) > 0;
        }
    }
}