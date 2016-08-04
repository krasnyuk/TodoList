using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NLT.Models
{
    public class NLTContext : DbContext
    {
        public NLTContext() : base("name=NLTContext")
        {
        }

        public DbSet<TodoList> TodoList { get; set; }
        public DbSet<_Task> _Task { get; set; }
    }
}
