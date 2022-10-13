using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("hejsa");
    }
}

public class TodosContext : DbContext
{
    public DbSet<Todo> Blogs { get; set; }
    public DbSet<Task> Posts { get; set; }

    public string DbPath { get; }

    public TodosContext()
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

    public List<Todo> Blog { get; set; }
}