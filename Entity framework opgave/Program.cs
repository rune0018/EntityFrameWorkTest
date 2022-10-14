using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    public static void Main()
    {
        //SeedTasks();
        //using(var db = new ProjectmanegerContext())
        //{
        //    foreach(Task task in db.Tasks)
        //    {
        //        Console.WriteLine(task.Name);
        //        try
        //        {
        //            Console.WriteLine(task.Todos.Count());
        //        }
        //        catch (Exception)
        //        {
        //            Console.WriteLine("No list");
        //        }
        //    }
        //}

        //using(ProjectmanegerContext db = new ProjectmanegerContext())
        //{
        //    var tasks = db.Tasks.Include(tasks=>tasks.Todos)
        //        .Where(x=> x.Name == "Produce software");
        //    Console.WriteLine(tasks.First().Todos.Count());
        //}
        //SeedWorkers();
        PrintTeamsWithoutTasks();
        PrintTeamCurrentTask();
        PrintTeamProgress();
    }

    public static void PrintTeamsWithoutTasks()
    {
        using( var db = new ProjectmanegerContext())
        {
            foreach( var team in db.Teams)
            {
                Console.WriteLine($"{team.TeamID}: {team.Name}");
            }
        }
    }

    public static void PrintTeamCurrentTask()
    {
        var db = new ProjectmanegerContext();
        foreach (var team in db.Teams.Include(o => o.CurrentTask))
        {
            if(team.CurrentTask is null)
            {
                Console.WriteLine($"{team.TeamID}: {team.Name} has no tasks");
                continue;
            }
            Console.WriteLine($"{team.TeamID}: {team.Name} {team.CurrentTask.Name}");
        }
    }

    public static void PrintTeamProgress()
    {
        var db = new ProjectmanegerContext();
        foreach (var team in db.Teams.Include(o => o.CurrentTask).Include(o=> o.CurrentTask.Todos))
        {
            if(team.CurrentTask is null)
            {
                Console.WriteLine($"{team.TeamID}: {team.Name} has no tasks");
                continue;
            }
            int total = 0;
            int totalTasks =team.CurrentTask.Todos.Count();
            foreach(var task in team.CurrentTask.Todos)
            {
                if (task.Complete)
                {
                    total ++;
                }
            }
            Console.WriteLine($"{team.TeamID}: {team.Name} {team.CurrentTask.Name}: {total} of {totalTasks} done");
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

    public static void SeedWorkers()
    {
        var db = new ProjectmanegerContext();
        var SteenSecher = new Worker { Name = "Steen Secher" };
        var EjvindMøller = new Worker { Name = "Ejvind Møller" };
        var KonradSommer = new Worker { Name = "Konrad Sommer" };
        var SofusLotus = new Worker { Name = "Sofus Lotus" };
        var RemoLademann = new Worker { Name = "Remo Lademann" };
        var EllaFanth = new Worker { Name = "Ella Fanth" };
        var AnneDam = new Worker { Name = "Anne Dam" };
        var Frontend = new Team { Name = "Frontend" };
        var Backend = new Team { Name = "Backend" };
        var Testere = new Team { Name = "Testere" };
        db.TeamWorkers.Add(new TeamWorker { Team = Frontend, Worker = SteenSecher });
        db.TeamWorkers.Add(new TeamWorker { Team = Frontend, Worker = EjvindMøller });
        db.TeamWorkers.Add(new TeamWorker { Team = Frontend, Worker = KonradSommer });
        db.TeamWorkers.Add(new TeamWorker { Team = Backend, Worker = KonradSommer });
        db.TeamWorkers.Add(new TeamWorker { Team = Backend, Worker = SofusLotus });
        db.TeamWorkers.Add(new TeamWorker { Team = Backend, Worker = RemoLademann });
        db.TeamWorkers.Add(new TeamWorker { Team = Testere, Worker = EllaFanth });
        db.TeamWorkers.Add(new TeamWorker { Team = Testere, Worker = AnneDam });
        db.TeamWorkers.Add(new TeamWorker { Team = Testere, Worker = SteenSecher });
        db.SaveChanges();
    }
}

public class ProjectmanegerContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }
    public DbSet<Task> Tasks { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamWorker> TeamWorkers { get; set; }
    public DbSet<Worker> Workers { get; set; }

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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeamWorker>()
            .HasKey(o => new { o.TeamID, o.WorkerID });

        modelBuilder.Entity<Team>()
            .HasOne(o => o.CurrentTask);

        modelBuilder.Entity<Worker>()
            .HasOne(o => o.CurrentTodo);
    }
}
