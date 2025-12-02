using System.Collections.Generic;
using SimuladorSO.Model;

namespace SimuladorSO.Scheduler
{
    public class EscalonadorFcfs : IEscalonador
    {
        private readonly Queue<Process> _filaProntos;

        public EscalonadorFcfs()
        {
            _filaProntos = new Queue<Process>();
        }

        public void AdicionarProcesso(Process processo)
        {
            _filaProntos.Enqueue(processo);
        }

        public Process ObterProximoProcesso()
        {
            if (_filaProntos.Count == 0)
            {
                return null;
            }

            return _filaProntos.Dequeue();
        }

        public bool TemProcessos()
        {
            return _filaProntos.Count > 0;
        }
    }
}
