using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NLT.Models
{
    public class TodoList
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public ICollection<_Task> _Tasks { get; set; }

    }
}