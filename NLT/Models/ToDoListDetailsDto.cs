using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NLT.Models
{
    public class ToDoListDetailsDto
    {
        
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public List<_Task> _Tasks { get; set; }
    }
}