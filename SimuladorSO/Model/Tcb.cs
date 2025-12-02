namespace SimuladorSO.Model
{
    public class Tcb
    {
        public int Tid { get; set; }
        public EstadoThread Estado { get; set; }

        // Processo ao qual a thread pertence
        public Process ProcessoPai { get; set; }

        public int ContadorProgramaLogico { get; set; }

        // "pilha" simples
        public int[] PilhaLogicaSimulada { get; set; }
    }
}
