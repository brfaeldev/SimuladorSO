using SimuladorSO.Model;

namespace SimuladorSO.IO
{
    public class PedidoEs
    {
        public Process Processo { get; set; }
        public string Operacao { get; set; }  // "leitura" ou "escrita"
        public int TempoRestante { get; set; } // ticks até terminar
    }
}
