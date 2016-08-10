using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using NLT.Models;

namespace NLT.Controllers
{
    public class TaskInListController : ApiController
    {
        private NLTContext _db = new NLTContext();

      
        public IHttpActionResult GetTaskInList(int id)
        {
            List<_Task> result;
            try
            {
                result = _db._Task.Where(x => x.TodoListId == id).ToList();
            }
            catch (Exception)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
