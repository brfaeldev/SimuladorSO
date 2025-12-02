namespace SimuladorSO.Memory
{
    public class Moldura
    {
        public int NumeroMoldura { get; set; }
        public bool Ocupada { get; set; }

        // Quem está usando esta moldura
        public int? PidDono { get; set; }
        public int? NumeroPaginaDona { get; set; }
    }
}
