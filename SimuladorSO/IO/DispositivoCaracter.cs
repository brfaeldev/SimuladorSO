using System.Collections.Generic;
using SimuladorSO.Model;

namespace SimuladorSO.IO
{
    public class DispositivoCaracter
    {
        public string Nome { get; set; }
        public Queue<PedidoEs> FilaPedidos { get; set; }

        public DispositivoCaracter()
        {
            FilaPedidos = new Queue<PedidoEs>();
        }
    }
}
