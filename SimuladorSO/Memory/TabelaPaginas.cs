using System.Collections.Generic;

namespace SimuladorSO.Memory
{
    public class TabelaPaginas
    {
        public int Pid { get; set; }

        // página lógica -> número da moldura física
        public Dictionary<int, int> PaginaParaMoldura { get; set; }

        public TabelaPaginas()
        {
            PaginaParaMoldura = new Dictionary<int, int>();
        }
    }
}
