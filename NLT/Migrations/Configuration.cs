namespace NLT.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using NLT.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<NLT.Models.NLTContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NLT.Models.NLTContext context)
        {
            context.TodoList.AddOrUpdate(x => x.Id,
                new TodoList() { Id = 1, Title = "My NLT task", Date = DateTime.Now },
                new TodoList() { Id = 2, Title = "My photography task", Date = DateTime.Now },
                new TodoList() { Id = 3, Title = "My new task", Date = DateTime.Now }
                );
            context._Task.AddOrUpdate(x => x.Id,
                new _Task()
                {
                    Id = 1,
                    Title = "IoC",
                    Description = "Set up ninject",
                    ExpireDate = DateTime.Parse("12-11-2016").Date,
                    Completed = false,
                    TodoListId = 1
                },
                new _Task()
                {
                    Id = 1,
                    Title = "Repository",
                    Description = "Set up repository",
                    ExpireDate = DateTime.Parse("12-11-2016").Date,
                    Completed = false,
                    TodoListId = 1
                },
                new _Task()
                {
                    Id = 1,
                    Title = "Wedding photo",
                    Description = "Retouch wedding photos",
                    ExpireDate = DateTime.Parse("12-10-2016").Date,
                    Completed = false,
                    TodoListId = 1
                }
                );
        }
    }
}
