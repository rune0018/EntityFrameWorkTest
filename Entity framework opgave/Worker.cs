public class Worker
{
    public int WorkerID { get; set; }
    public string Name { get; set; }
    public Todo? CurrentTodo { get; set; }
    public List<TeamWorker> TeamWorkers = new List<TeamWorker>();
}