using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        //SeedTasks();
        using(var db = new ProjectmanegerContext())
        {
            foreach(Task task in db.Tasks)
            {
                Console.WriteLine(task.Name);
            }
        }
    }

    public static void SeedTasks()
    {
        using(var db = new ProjectmanegerContext())
        {
            Task task = new Task()
            {
                Name = "Produce software",
                Todos = new List<Todo>() 
                {
                    new Todo() {Title= "WriteCode"},
                    new Todo() {Title= "Compile source" },
                    new Todo() {Title= "Test program"}
                }
            };
            Task task2 = new Task()
            {
                Name = "Brew Coffe",
                Todos= new List<Todo>()
                {
                    new Todo() {Title= "Pour water"},
                    new Todo() {Title= "Pour coffee"},
                    new Todo() {Title= "Turn on"}
                }
            };
            db.Tasks.Add(task);
            db.Tasks.Add(task2);
            db.SaveChanges();
        }
    }
}

public class ProjectmanegerContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<Task> Tasks { get; set; }

    public string DbPath { get; }

    public ProjectmanegerContext()
    {
        //var folder = Environment.SpecialFolder.LocalApplicationData;
        //var path = Environment.GetFolderPath(folder);
        //DbPath = System.IO.Path.Join(path, "blogging.db");
        DbPath = "C:/SqliteDbs/Test.db";
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class Todo
{
    public int ID { get; set; }
    public string Title { get; set; }
    public bool Complete { get; set; }
}

public class Task
{
    public int ID { get; set; }
    public string Name { get; set; }

    public List<Todo> Todos { get; set; }
}