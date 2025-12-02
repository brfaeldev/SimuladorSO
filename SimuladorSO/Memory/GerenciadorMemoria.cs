using System.Collections.Generic;

namespace SimuladorSO.Memory
{
    public class GerenciadorMemoria
    {
        public int TamanhoPagina { get; set; }
        public int QuantidadeMolduras { get; set; }

        public List<Moldura> Molduras { get; set; }
        public List<TabelaPaginas> TabelasPaginas { get; set; }

        public int ContadorFaltasPagina { get; set; }

        public GerenciadorMemoria()
        {
            Molduras = new List<Moldura>();
            TabelasPaginas = new List<TabelaPaginas>();
        }

        public void InicializarMolduras()
        {
            Molduras.Clear();

            for (int i = 0; i < QuantidadeMolduras; i++)
            {
                Molduras.Add(new Moldura
                {
                    NumeroMoldura = i,
                    Ocupada = false
                });
            }
        }

        public bool AcessarPagina(int pid, int numeroPagina)
        {
            // Procura tabela de páginas do processo
            var tabela = TabelasPaginas.Find(t => t.Pid == pid);
            if (tabela == null)
            {
                tabela = new TabelaPaginas { Pid = pid };
                TabelasPaginas.Add(tabela);
            }

            // Já está mapeada? então não é falta
            if (tabela.PaginaParaMoldura.ContainsKey(numeroPagina))
            {
                return false; // sem falta de página
            }

            // Não está mapeada: falta de página
            ContadorFaltasPagina++;

            // first fit simples: primeira moldura livre
            var molduraLivre = Molduras.Find(m => !m.Ocupada);
            if (molduraLivre != null)
            {
                molduraLivre.Ocupada = true;
                molduraLivre.PidDono = pid;
                molduraLivre.NumeroPaginaDona = numeroPagina;

                tabela.PaginaParaMoldura[numeroPagina] = molduraLivre.NumeroMoldura;
            }

            return true; // houve falta
        }

    }
}
