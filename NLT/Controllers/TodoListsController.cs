using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NLT.Models;

namespace NLT.Controllers
{
    public class TodoListsController : ApiController
    {
        private NLTContext db = new NLTContext();

        // GET: api/TodoLists
        public IQueryable<TodoList> GetTodoLists()
        {
            return db.TodoList;
        }

        // GET: api/TodoLists/5
        [ResponseType(typeof(TodoList))]
        public IHttpActionResult GetTodoList(int id)
        {
            TodoList todoList = db.TodoList.Find(id);
            if (todoList == null)
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

            db.Entry(todoList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
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

            db.TodoList.Add(todoList);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = todoList.Id }, todoList);
        }

        // DELETE: api/TodoLists/5
        [ResponseType(typeof(TodoList))]
        public IHttpActionResult DeleteTodoList(int id)
        {
            TodoList todoList = db.TodoList.Find(id);
            if (todoList == null)
            {
                return NotFound();
            }

            db.TodoList.Remove(todoList);
            db.SaveChanges();

            return Ok(todoList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TodoListExists(int id)
        {
            return db.TodoList.Count(e => e.Id == id) > 0;
        }
    }
}