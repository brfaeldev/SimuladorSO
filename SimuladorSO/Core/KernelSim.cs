using System;
using System.Collections.Generic;
using SimuladorSO.Memory;
using SimuladorSO.Model;
using SimuladorSO.Scheduler;
using SimuladorSO.IO;


namespace SimuladorSO.Core
{
    public class KernelSim
    {
        public int Clock { get; private set; }

        public List<Process> Processos { get; private set; }
        public IEscalonador Escalonador { get; private set; }
        public GerenciadorMemoria GerenciadorMemoria { get; private set; }
        public DispositivoBloco Disco { get; private set; }
        public DispositivoCaracter Terminal { get; private set; }


        public KernelSim()
        {
            Clock = 0;
            Processos = new List<Process>();
            Escalonador = new EscalonadorFcfs();
            Disco = new DispositivoBloco { Nome = "Disco" };
            Terminal = new DispositivoCaracter { Nome = "Terminal" };


            GerenciadorMemoria = new GerenciadorMemoria
            {
                TamanhoPagina = 4,
                QuantidadeMolduras = 8
            };
            GerenciadorMemoria.InicializarMolduras();
        }

        public void AdicionarProcesso(Process processo)
        {
            Processos.Add(processo);
            Escalonador.AdicionarProcesso(processo);
        }

        public void Tick()
        {
            Clock++;

            var processo = Escalonador.ObterProximoProcesso();

            if (processo == null)
            {
                Console.WriteLine($"[Clock {Clock}] Nenhum processo pronto.");
                return;
            }

            processo.Pcb.Estado = EstadoProcesso.Executando;

            if (processo.PaginasParaAcessar.Count > 0)
            {
                int pagina = processo.PaginasParaAcessar.Dequeue();
                bool falta = GerenciadorMemoria.AcessarPagina(processo.Pcb.Pid, pagina);

                string textoFalta = falta ? "FALTA de página" : "sem falta de página";
                Console.WriteLine($"[Clock {Clock}] PID={processo.Pcb.Pid}, página lógica {pagina}: {textoFalta}.");

                // Regra simples: se acessou página 1, pede E/S de 2 ticks
                if (pagina == 1)
                {
                    Console.WriteLine($"[Clock {Clock}] PID={processo.Pcb.Pid} solicitou E/S em disco.");
                    SolicitarEsDisco(processo, "leitura", 2);
                }
                else if (processo.PaginasParaAcessar.Count > 0)
                {
                    processo.Pcb.Estado = EstadoProcesso.Pronto;
                    Escalonador.AdicionarProcesso(processo);
                }
                else
                {
                    processo.Pcb.Estado = EstadoProcesso.Finalizado;
                }
            }
            else
            {
                Console.WriteLine($"[Clock {Clock}] PID={processo.Pcb.Pid} não tem mais páginas para acessar.");
                processo.Pcb.Estado = EstadoProcesso.Finalizado;
            }

            ProcessarDispositivosEs();
        }



        public void MostrarEstadoMemoria()
        {
            Console.WriteLine("=== Molduras ===");
            foreach (var m in GerenciadorMemoria.Molduras)
            {
                string dono = m.Ocupada
                    ? $"PID={m.PidDono}, Pagina={m.NumeroPaginaDona}"
                    : "livre";
                Console.WriteLine($"Moldura {m.NumeroMoldura}: {dono}");
            }

            Console.WriteLine($"Total de faltas de página: {GerenciadorMemoria.ContadorFaltasPagina}");
        }

        public void SolicitarEsDisco(Process processo, string operacao, int tempoServico)
        {
            var pedido = new PedidoEs
            {
                Processo = processo,
                Operacao = operacao,
                TempoRestante = tempoServico
            };

            Disco.FilaPedidos.Enqueue(pedido);
            processo.Pcb.Estado = EstadoProcesso.Bloqueado;
        }

        private void ProcessarDispositivosEs()
        {
            if (Disco.FilaPedidos.Count > 0)
            {
                var pedido = Disco.FilaPedidos.Peek();
                pedido.TempoRestante--;

                if (pedido.TempoRestante <= 0)
                {
                    // E/S terminou, processo volta para PRONTO
                    Disco.FilaPedidos.Dequeue();
                    pedido.Processo.Pcb.Estado = EstadoProcesso.Pronto;
                    Escalonador.AdicionarProcesso(pedido.Processo);

                    Console.WriteLine($"[Clock {Clock}] E/S em disco concluída para PID={pedido.Processo.Pcb.Pid}.");
                }
            }
        }

    }
}
