using System.Collections.Generic;

namespace SimuladorSO.FS
{
    public class Diretorio
    {
        public string Nome { get; set; }

        public List<Diretorio> Subdiretorios { get; set; }
        public List<ArquivoSim> Arquivos { get; set; }

        public Diretorio(string nome)
        {
            Nome = nome;
            Subdiretorios = new List<Diretorio>();
            Arquivos = new List<ArquivoSim>();
        }
    }
}
