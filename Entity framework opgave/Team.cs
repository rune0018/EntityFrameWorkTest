public class Team
{
    public int TeamID { get; set; }
    public string Name { get; set; }
    public Task? CurrentTask { get; set; }
    public List<TeamWorker> TeamWorkers = new List<TeamWorker>();
    public List<Task> Tasks = new List<Task>();
}
