using SimuladorSO.Model;

namespace SimuladorSO.Scheduler
{
    public interface IEscalonador
    {
        void AdicionarProcesso(Process processo);
        Process ObterProximoProcesso();
        bool TemProcessos();
    }
}
