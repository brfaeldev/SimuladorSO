using System.Collections.Generic;
using SimuladorSO.Model;

namespace SimuladorSO.IO
{
    public class DispositivoBloco
    {
        public string Nome { get; set; }
        public Queue<PedidoEs> FilaPedidos { get; set; }

        public DispositivoBloco()
        {
            FilaPedidos = new Queue<PedidoEs>();
        }
    }
}
