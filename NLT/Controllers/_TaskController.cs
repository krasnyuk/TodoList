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
using NLT.Repository;

namespace NLT.Controllers
{
    public class _TaskController : ApiController
    {

        private ITaskRepository _taskRepository;

        public _TaskController(ITaskRepository repo)
        {
            _taskRepository = repo;
        }

        // GET: api/_Task
        public IQueryable<_Task> Get_Task()
        {
            return _taskRepository.GetTaskList();
        }

        // GET: api/_Task/5
        [ResponseType(typeof(_Task))]
        public IHttpActionResult Get_Task(int id)
        {
            return Ok(_taskRepository.GetTask(id));
        }

        // PUT: api/_Task/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Put_Task(int id, _Task _Task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != _Task.Id)
            {
                return BadRequest();
            }

            _taskRepository.Update(_Task);

            try
            {
                _taskRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_TaskExists(id))
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

        // POST: api/_Task
        [ResponseType(typeof(_Task))]
        public IHttpActionResult Post_Task(_Task _Task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _taskRepository.Create(_Task);
            _taskRepository.Save();

            return CreatedAtRoute("DefaultApi", new { id = _Task.Id }, _Task);
        }

        // DELETE: api/_Task/5
        [ResponseType(typeof(_Task))]
        public IHttpActionResult Delete_Task(int id)
        {
            _taskRepository.Delete(id);
            _taskRepository.Save();
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

        private bool _TaskExists(int id)
        {
            return _taskRepository.GetTaskList().Count(e => e.Id == id) > 0;
        }
    }
}