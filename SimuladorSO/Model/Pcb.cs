namespace SimuladorSO.Model
{
    public class Pcb
    {
        public int Pid { get; set; }
        public EstadoProcesso Estado { get; set; }
        public int Prioridade { get; set; }
        public int ContadorProgramaLogico { get; set; }
        // Nesse estado de projeto, só um inteiro para representar "registradores"
        public int RegistradorSimulado { get; set; }

        // Melhoria: ligar com arquivos abertos
        // public List<int> ArquivosAbertosIds { get; set; }
    }
}
