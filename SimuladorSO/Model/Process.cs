using SimuladorSO.Model;

public class Process
{
    public Pcb Pcb { get; set; }
    public string Nome { get; set; }
    public int MemoriaNecessaria { get; set; }
    public int TamanhoGbNoDisco { get; set; }
    public bool CarregadoNaRam { get; set; }

    public List<ThreadSim> Threads { get; set; }
    public Queue<int> PaginasParaAcessar { get; set; }

    public Process()
    {
        Threads = new List<ThreadSim>();
        PaginasParaAcessar = new Queue<int>();
    }
}
